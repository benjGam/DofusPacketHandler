using DofusPacketManager.Network;
using DofusPacketManager.Network.Messages;
using DofusPacketManager.Utils;
using DofusPacketManager.Utils.IO;
using PacketDotNet;
using System;

namespace DofusPacketManager.Networking.Messages
{
    public class PacketParser : Singleton<PacketParser>
    {
        #region Event Handler Declaration
        public event EventHandler<NetworkMessageReceivedEventArgs> OnMessageRecieved;
        #endregion
        #region Constructors
        public PacketParser() => Init();
        #endregion
        #region Methods
        #region Processing Packet
        private void Init() => NetworkManager.Instance.OnPacketReceived += Instance_OnPacketReceived;
        private void ProcessPacket(TcpPacket TcpPacket)
        {
            IDataReader Reader = new BigEndianReader(TcpPacket.PayloadData);
            NetworkMessageInformations messageInformations = GetMessageInformations(Reader);
            ParsePacketAsMessage(messageInformations);
        }
        private NetworkMessage ParsePacketAsMessage(NetworkMessageInformations messageInformations)
        {
            NetworkMessage recievedMessage = MessageInitializer.Instance.GetInstance<NetworkMessage>(messageInformations.MessageId);
            if (recievedMessage == null) return null;
            OnPacketParsed(this, new NetworkMessageReceivedEventArgs(recievedMessage));
            return recievedMessage;
        }
        #endregion
        #region Building Message Informations
        private NetworkMessageInformations GetMessageInformations(IDataReader Reader)
        {
            ushort staticHeader = GetStaticHeader(Reader);
            ushort messageId = ExtractMessageId(staticHeader);
            ushort lenType = ExtractLenType(staticHeader);
            int Length = CalculateMessageLength(Reader, lenType);
            return new NetworkMessageInformations(messageId, Length, ref Reader);
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
        #endregion
        #endregion
        #region Events
        #region Custom Events
        protected virtual void OnPacketParsed(object sender, NetworkMessageReceivedEventArgs e) 
        {
            if (OnMessageRecieved != null)
                OnMessageRecieved(this, e);
        }
        #endregion
        #region Events Handler
        private void Instance_OnPacketReceived(object sender, PacketArrivedEventArgs e) => ProcessPacket(e.RecievedPacket);
        #endregion
        #endregion
    }
}
