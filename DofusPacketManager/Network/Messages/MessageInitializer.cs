using DofusPacketManager.Utils;
using DofusPacketManager.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace DofusPacketManager.Networking.Messages
{
    public class MessageInitializer : Singleton<MessageInitializer>
    {
        private Dictionary<ushort, Func<object>> _messagesConstructors = new Dictionary<ushort, Func<object>>();

        public MessageInitializer() => Init();
        private void Init()
        {
            foreach (Type Type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (Type.GetOldestParent() != typeof(NetworkMessage) || Type.IsAbstract) continue;
                    RegisterConstructor(Type);
            }
        }
        private void RegisterConstructor(Type Type)
        {
            object constructorInstance = Type.GetConstructor(Type.EmptyTypes).Invoke(null);
            ushort messageId = (ushort)constructorInstance.GetType().GetProperty("MessageID").GetValue(constructorInstance);
            _messagesConstructors.Add(messageId, Type.GetConstructor(Type.EmptyTypes).CreateDelegate<Func<object>>());
        }

        public ReadOnlyCollection<Func<object>> MessagesConstructors => _messagesConstructors.Values.ToList().AsReadOnly();
        public T GetInstance<T>(ushort messageId) where T : NetworkMessage
        {
            Func<object> targetedMessage;
            return _messagesConstructors.TryGetValue(messageId, out targetedMessage) ? targetedMessage.Invoke() as T : null;
        }
    }
}
