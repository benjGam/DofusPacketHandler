using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Messages
{
    public class NetworkMessageInformations
    {
        private ushort _messageId;
        private int _length;

        public NetworkMessageInformations(ushort messageId, int Length, ref IDataReader Reader) 
        {
            _messageId = messageId;
            _length = Length;
        }

        public ushort MessageId => _messageId;
        public int Length => _length;
    }
}
