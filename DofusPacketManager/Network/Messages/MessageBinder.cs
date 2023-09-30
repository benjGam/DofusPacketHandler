using DofusPacketManager.Utils;
using DofusPacketManager.Network.Messages.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DofusPacketManager.Network.Messages
{
    public class MessageBinder : Singleton<MessageBinder>
    {
        private Dictionary<Type, EventKeyPair> _bindedNetworkMessagesTypes = new Dictionary<Type, EventKeyPair>();

        public MessageBinder() => PacketParser.Instance.MessageReceived += PacketParser_OnMessageRecieved;

        public void On<T>(EventHandler handler, NetworkMessageEventEnum eventName) where T : NetworkMessage
        {
            if (!_bindedNetworkMessagesTypes.ContainsKey(typeof(T)))
                _bindedNetworkMessagesTypes.Add(typeof(T), new EventKeyPair(eventName.ToString(), handler));
            else
                _bindedNetworkMessagesTypes[typeof(T)].AddKeyPair(eventName.ToString(), handler);
        }

        public void Unbind<T>() where T : NetworkMessage
        {
            if (_bindedNetworkMessagesTypes.ContainsKey(typeof(T)))
                _bindedNetworkMessagesTypes.Remove(typeof(T));
        }
        public void Unbind<T>(NetworkMessageEventEnum eventName) where T : NetworkMessage
        {
            if (_bindedNetworkMessagesTypes.ContainsKey(typeof(T)))
                _bindedNetworkMessagesTypes[typeof(T)].RemoveKeyPair(eventName.ToString());
        }

        private void PacketParser_OnMessageRecieved(object sender, NetworkMessageReceivedEventArgs e)
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
