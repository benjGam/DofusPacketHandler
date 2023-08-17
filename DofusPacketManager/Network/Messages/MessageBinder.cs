using DofusPacketManager.Networking.Messages;
using DofusPacketManager.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DofusPacketManager.Network.Messages
{
    public class MessageBinder : Singleton<MessageBinder>
    {
        private Dictionary<Type, EventHandler> _bindedNetworkMessagesTypes = new Dictionary<Type, EventHandler>();
        public List<NetworkMessage> _bindedOnDeserialized = new List<NetworkMessage>();

        public MessageBinder() 
        {
            Init();
        }

        private void Init()
        {
            PacketParser.Instance.OnMessageRecieved += Instance_OnMessageRecieved;
        }

        public bool Bind<T>(EventHandler handler, string eventName) where T : NetworkMessage
        {
            if (!_bindedNetworkMessagesTypes.ContainsKey(typeof(T)))
            {
                _bindedNetworkMessagesTypes.Add(typeof(T), handler);
                return true;
            }
            return false;
        }

        public bool UnbindDeserialized<T>() where T : NetworkMessage
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
            if (_bindedNetworkMessagesTypes.ContainsKey(targetedType))
            {
                EventInfo info = targetedType.GetEvent("OnDeserialized");
                foreach (NetworkMessage ne in _bindedOnDeserialized)
                    info.RemoveEventHandler(ne, _bindedNetworkMessagesTypes[targetedType]);
                info.AddEventHandler(e.RecievedMessage, _bindedNetworkMessagesTypes[targetedType]);
            }
        }
    }
}
