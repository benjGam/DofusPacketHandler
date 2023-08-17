using DofusPacketManager.Utils.IO;

namespace DofusPacketManager.Network.Messages.common
{
    public class BasicPingMessage : NetworkMessage
    {
        public override ushort MessageID => 4681;
        public override bool InstanceID => false;
        public bool quiet;
        protected override void DeserializeMessage(IDataReader Reader)
        {
            quiet = Reader.ReadBoolean();
        }
    }
}
