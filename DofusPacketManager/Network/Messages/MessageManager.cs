using DofusPacketManager.Network.Messages;
using DofusPacketManager.Utils;

namespace DofusPacketManager.Networking.Messages
{
    public class MessageManager : Singleton<MessageManager>
    {
        private MessageInitializer _messageInitializer;
        private MessageBinder _messageBinder;
        private PacketParser _packetParser;
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
