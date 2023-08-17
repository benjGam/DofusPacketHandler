using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Messages.game.chat
{
    public class ChatServerMessage : ChatAbstractServerMessage
    {
        public override ushort MessageID => 2565;
        public override bool InstanceID => false;
        public double SenderId { get; private set; }
        public string SenderName { get; private set; }
        public string Prefix { get; private set; }
        public int SenderAccountId { get; private set; }

        protected new void DeserializeMessage(IDataReader Reader)
        {
            base.Deserialize(Reader);
            SenderId = Reader.ReadDouble();
            SenderName = Reader.ReadUTF();
            Prefix = Reader.ReadUTF();
            SenderAccountId = Reader.ReadInt();
        }
    }
}
