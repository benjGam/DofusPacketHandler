using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Messages.game.chat
{
    public class ChatServerMessage : ChatAbstractServerMessage
    {
        public override ushort MessageID => 2565;
        public override bool InstanceID => false;
        public double _senderId;
        public string _senderName;
        public string _prefix;
        public int _senderAccountId;

        protected new void DeserializeMessage(IDataReader Reader)
        {
            base.Deserialize(Reader);
            _senderId = Reader.ReadDouble();
            _senderName = Reader.ReadUTF();
            _prefix = Reader.ReadUTF();
            _senderAccountId = Reader.ReadInt();
        }
    }
}
