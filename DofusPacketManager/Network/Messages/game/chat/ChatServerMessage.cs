using DofusPacketManager.Utils.IO;
using System.Windows.Forms;

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

        protected override void DeserializeMessage(IDataReader Reader)
        {
            base.DeserializeMessage(Reader);
            _senderId = Reader.ReadDouble();
            _senderName = Reader.ReadUTF();
            _prefix = Reader.ReadUTF();
            _senderAccountId = Reader.ReadInt();
            MessageBox.Show("Oui");
        }
    }
}
