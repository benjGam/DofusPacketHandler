using DofusPacketManager.Networking.Messages;
using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Messages.game.chat
{
    public class ChatAbstractServerMessage : NetworkMessage
    {
        public override ushort MessageID => 2850;
        public override bool InstanceID => false;
        public uint _channel;
        public string _content;
        public int _timestamp;
        public string _fingerprint;

        protected override void DeserializeMessage(IDataReader Reader)
        {
            _channel = Reader.ReadByte();
            _content = Reader.ReadUTF();
            _timestamp = Reader.ReadInt();
            _fingerprint = Reader.ReadUTF();
        }
    }
}
