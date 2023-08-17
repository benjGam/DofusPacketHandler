using DofusPacketManager.Networking.Messages;
using DofusPacketManager.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DofusPacketManager.Network.Messages
{
    public class MessageBinder : Singleton<MessageBinder>
    {
        private Dictionary<Type, Func<object>> _eventHookerStack = new Dictionary<Type, Func<object>>();

        public MessageBinder() 
        {
            PacketParser.Instance.OnMessageRecieved += Instance_OnMessageRecieved;
        }

        public bool On<T>(Func<object> Execute) where T : NetworkMessage
        {
            if (!_eventHookerStack.ContainsKey(typeof(T)))
            {
                _eventHookerStack.Add(typeof(T), Execute);
                return true;
            }
            return false;
        }

        public bool Unbind<T>() where T : NetworkMessage
        {
            if (_eventHookerStack.ContainsKey(typeof(T)))
            {
                _eventHookerStack.Remove(typeof(T));
                return true;
            }
            return false;
        }

        private void Instance_OnMessageRecieved(object sender, NetworkMessageReceivedEventArgs e)
        {
            Type t = e.RecievedMessage.GetType();
            if (_eventHookerStack.ContainsKey(t))
            {
                new MethodInvoker(() => _eventHookerStack[t]()).Invoke();
            }
        }
    }
}
