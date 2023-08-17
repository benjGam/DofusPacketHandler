using System.Data;

namespace DofusPacketManager.Networking.Messages
{
    public class TestMessage : NetworkMessage
    {
        public override ushort MessageID => 2222;

        public override bool InstanceID => false;

        public override void Deserialize(IDataReader Reader)
        {

        }
    }
}
