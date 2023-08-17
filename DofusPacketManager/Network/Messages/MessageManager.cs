using DofusPacketManager.Utils;

namespace DofusPacketManager.Networking.Messages
{
    public class MessageManager : Singleton<MessageManager>
    {
        private MessageInitializer _messageInitializer;
        public MessageManager()
        {
            _messageInitializer = MessageInitializer.Instance;
        }
        public MessageInitializer MessageInitializer => _messageInitializer;
    }
}
