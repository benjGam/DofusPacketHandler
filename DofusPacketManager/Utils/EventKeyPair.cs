using System;
using System.Collections.Generic;

namespace DofusPacketManager.Utils
{
    public class EventKeyPair
    {
        private Dictionary<string, EventHandler> _keyPair = new Dictionary<string, EventHandler>();
        public EventKeyPair(string eventName, EventHandler Handler)
        {
            AddKeyPair(eventName, Handler);
        }
        public void AddKeyPair(string eventName, EventHandler handler)
        {
            _keyPair[eventName] = handler;
        }
        public bool RemoveKeyPair(string eventName) 
        { 
            if(_keyPair.ContainsKey(eventName))
            {
                _keyPair.Remove(eventName);
                return true;
            }
            return false;
        }

        public EventHandler GetKeyPair(string eventName)
        {
            if (_keyPair.ContainsKey(eventName)) return _keyPair[eventName];
            return null;
        }

    }
}
