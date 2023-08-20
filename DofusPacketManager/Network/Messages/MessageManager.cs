using DofusPacketManager.Network.Types;
using DofusPacketManager.Utils;

namespace DofusPacketManager.Network.Messages
{
    public class MessageManager : Singleton<MessageManager>
    {
        private MessageInitializer _messageInitializer;
        private TypeInitializer _typeInitializer;
        private PacketParser _packetParser;
        private MessageBinder _messageBinder;

        public MessageManager()
        {
            _messageInitializer = MessageInitializer.Instance;
            _typeInitializer = TypeInitializer.Instance;
            _packetParser = PacketParser.Instance;
            _messageBinder = MessageBinder.Instance;
        }
        public MessageInitializer MessageInitializer => _messageInitializer;
        public TypeInitializer TypeInitializer => _typeInitializer;
        public MessageBinder MessageBinder => _messageBinder;
        public PacketParser PacketParser => _packetParser;
    }
}
