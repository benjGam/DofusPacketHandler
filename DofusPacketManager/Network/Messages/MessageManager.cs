using DofusPacketManager.Utils;

namespace DofusPacketManager.Network.Messages
{
    public class MessageManager : Singleton<MessageManager>
    {
        private MessageInitializer _messageInitializer;
        private PacketParser _packetParser;
        private MessageBinder _messageBinder;

        public MessageManager()
        {
            _messageInitializer = MessageInitializer.Instance;
            _packetParser = PacketParser.Instance;
            _messageBinder = MessageBinder.Instance;
        }
        public MessageInitializer MessageInitializer => _messageInitializer;
        public MessageBinder MessageBinder => _messageBinder;
        public PacketParser PacketParser => _packetParser;
    }
}
