using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Messages
{
    public class NetworkMessageInformations
    {
        private ushort _messageId;
        private int _length;
        private IDataReader _Reader;
        public NetworkMessageInformations(ushort messageId, int Length, ref IDataReader Reader) 
        {
            _messageId = messageId;
            _length = Length;
            _Reader = Reader;
        }

        public ushort MessageId => _messageId;
        public int Length => _length;
        public IDataReader Reader => _Reader;
    }
}
