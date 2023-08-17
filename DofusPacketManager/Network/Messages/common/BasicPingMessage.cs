using DofusPacketManager.Networking.Messages;
using DofusPacketManager.Utils.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
