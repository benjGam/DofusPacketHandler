using DofusPacketManager.Networking.Messages;
using DofusPacketManager.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DofusPacketManager.Network.Messages
{
    public class MessageBinder : Singleton<MessageBinder>
    {
        private Dictionary<Type, EventKeyPair> _bindedNetworkMessagesTypes = new Dictionary<Type, EventKeyPair>();

        public MessageBinder() 
        {
            Init();
        }

        private void Init()
        {
            PacketParser.Instance.OnMessageRecieved += Instance_OnMessageRecieved;
        }

        public void Bind<T>(EventHandler handler, string eventName) where T : NetworkMessage
        {
            if (!_bindedNetworkMessagesTypes.ContainsKey(typeof(T)))
                _bindedNetworkMessagesTypes.Add(typeof(T), new EventKeyPair(eventName, handler));
            else
                _bindedNetworkMessagesTypes[typeof(T)].AddKeyPair(eventName, handler);
        }

        public bool Unbind<T>() where T : NetworkMessage
        {
            if (_bindedNetworkMessagesTypes.ContainsKey(typeof(T)))
            {
                _bindedNetworkMessagesTypes.Remove(typeof(T));
                return true;
            }
            return false;
        }

        private void Instance_OnMessageRecieved(object sender, NetworkMessageReceivedEventArgs e)
        {
            Type targetedType = e.RecievedMessage.GetType();
            if (!_bindedNetworkMessagesTypes.ContainsKey(targetedType)) return;
            foreach (EventInfo eventInfo in targetedType.GetEvents())
            {
                if (!_bindedNetworkMessagesTypes.ContainsKey(targetedType)) continue;
                EventKeyPair targetedKeyPair = _bindedNetworkMessagesTypes[targetedType];
                EventHandler targetedHandler = targetedKeyPair.GetKeyPair(eventInfo.Name);
                if (targetedHandler != null)
                    eventInfo.AddEventHandler(e.RecievedMessage, targetedHandler);
            }
        }
    }
}
