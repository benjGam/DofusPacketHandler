using DofusPacketManager.Network;
using DofusPacketManager.Utils;
using DofusPacketManager.Utils.IO;
using PacketDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DofusPacketManager.Networking.Messages
{
    public class PacketParser : Singleton<PacketParser>
    {
        private List<NetworkMessage> _deserializingPacket = new List<NetworkMessage>();

        public PacketParser()
        {
            Init();
        }

        private void Init()
        {
            NetworkManager.Instance.OnPacketReceived += Instance_OnPacketReceived;
        }

        private void Parse(TcpPacket TcpPacket)
        {
            IDataReader Reader = new BigEndianReader(TcpPacket.PayloadData);
            ushort staticHeader = GetStaticHeader(Reader);
            ushort messageId = ExtractMessageId(staticHeader);
            ushort lenType = ExtractLenType(staticHeader);
            int Length = CalculateMessageLength(Reader, lenType);
            NetworkMessage recievedMessage = MessageInitializer.Instance.GetInstance<NetworkMessage>(messageId);
            if (recievedMessage == null) return;
            recievedMessage.Deserialize(Reader);
        }

        private ushort GetStaticHeader(IDataReader Reader) => Reader.ReadUShort();
        private ushort ExtractMessageId(ushort staticHeader) => (ushort)(staticHeader >> 2);
        private ushort ExtractLenType(ushort staticHeader) => (ushort)(staticHeader & 3);
        private int CalculateMessageLength(IDataReader Reader, ushort lenType)
        {
            int calculatedLength = 0;
            while (lenType-- > 0)
                calculatedLength = (calculatedLength << 8) + Reader.ReadByte();
            return calculatedLength;
        }

        #region Events Handler
        private void Instance_OnPacketReceived(object sender, PacketArrivedEventArgs e) => Parse(e.RecievedPacket);
        #endregion
    }
}
