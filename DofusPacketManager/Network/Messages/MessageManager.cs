using DofusPacketManager.Network.Messages;
using DofusPacketManager.Utils;
using System.Threading;

namespace DofusPacketManager.Networking.Messages
{
    public class MessageManager : Singleton<MessageManager>
    {
        private MessageInitializer _messageInitializer;
        private MessageBinder _messageBinder;
        public MessageManager()
        {
            _messageInitializer = MessageInitializer.Instance;
            _messageBinder = MessageBinder.Instance;
        }
        public MessageInitializer MessageInitializer => _messageInitializer;
        public MessageBinder MessageBinder => _messageBinder;
    }
}
