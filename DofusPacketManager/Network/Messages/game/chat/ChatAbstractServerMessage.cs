using DofusPacketManager.Networking.Messages;
using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Messages.game.chat
{
    public class ChatAbstractServerMessage : NetworkMessage
    {
        public override ushort MessageID => 2850;
        public override bool InstanceID => false;
        public uint Channel { get; private set; }
        public string Content { get; private set; }
        public int Timestamp { get; private set; }
        public string Fingerprint { get; private set; }

        protected override void DeserializeMessage(IDataReader Reader)
        {
            Channel = Reader.ReadByte();
            Content = Reader.ReadUTF();
            Timestamp = Reader.ReadInt();
            Fingerprint = Reader.ReadUTF();
        }
    }
}
