using DofusPacketManager.Utils;
using DofusPacketManager.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace DofusPacketManager.Network.Types
{
    public class TypeInitializer : Singleton<TypeInitializer>
    {
        private Dictionary<ushort, Func<object>> _typesConstructors = new Dictionary<ushort, Func<object>>();
        public TypeInitializer() => Init();
        private void Init()
        {
            foreach (Type Type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (Type.GetOldestParent() != typeof(NetworkType) || Type.IsAbstract) continue;
                RegisterConstructor(Type);
            }
        }
        private void RegisterConstructor(Type Type)
        {
            object constructorInstance = Type.GetConstructor(Type.EmptyTypes).Invoke(null);
            ushort typeId = (ushort)constructorInstance.GetType().GetProperty("TypeID").GetValue(constructorInstance);
            _typesConstructors.Add(typeId, Type.GetConstructor(Type.EmptyTypes).CreateDelegate<Func<object>>());
        }

        public ReadOnlyCollection<Func<object>> TypesConstructors => _typesConstructors.Values.ToList().AsReadOnly();
        public T GetInstance<T>(ushort typeId) where T : NetworkType
        {
            Func<object> targetedType;
            return _typesConstructors.TryGetValue(typeId, out targetedType) ? targetedType.Invoke() as T : null;
        }
    }
}
