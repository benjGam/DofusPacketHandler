using DofusPacketManager.Networking.Messages;
using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network
{
    public class UncompleteNetworkMessage
    {
        private BigEndianReader _Reader;
        private ushort _messageId;
        private int _Length;

        public UncompleteNetworkMessage(ushort messageId, int length)
        {
            _messageId = messageId;
            _Length = length;
        }

        #region Accessors
        public ushort MessageId => _messageId;
        public int Length => _Length;
        public IDataReader Reader => _Reader;
        #endregion
    }
}
