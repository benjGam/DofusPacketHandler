using DofusPacketManager.Utils;
using DofusPacketManager.Utils.IO;
using DofusPacketManager.Network.Messages.CustomEventArgs;
using PacketDotNet;
using System;

namespace DofusPacketManager.Network.Messages
{
    public class PacketParser : Singleton<PacketParser>
    {
        #region Event Handler Declaration
        public event EventHandler<NetworkMessageReceivedEventArgs> MessageReceived;
        public event EventHandler<NetworkMessageInformationExtractedEventArgs> MessageInformationExtracted;
        #endregion
        #region Constructors
        public PacketParser() => Init();
        #endregion
        #region Methods
        #region Processing Packet
        private void Init() => NetworkManager.Instance.PacketReceived += NetworkManager_OnPacketReceived;
        private void ProcessPacket(TcpPacket TcpPacket)
        {
            IDataReader Reader = new BigEndianReader(TcpPacket.PayloadData);
            NetworkMessageInformations messageInformations = GetMessageInformations(Reader);
            NetworkMessage recievedMessage = ParsePacketAsMessage(messageInformations);
            PacketParser_OnMessageInformationsExtracted(this, new NetworkMessageInformationExtractedEventArgs(messageInformations, recievedMessage));
            if (recievedMessage != null)
                recievedMessage.Deserialize(Reader);
        }
        private NetworkMessage ParsePacketAsMessage(NetworkMessageInformations messageInformations)
        {
            NetworkMessage recievedMessage = MessageInitializer.Instance.GetInstance<NetworkMessage>(messageInformations.MessageId);
            if (recievedMessage == null) return null;
            PacketParser_OnPacketParsed(this, new NetworkMessageReceivedEventArgs(recievedMessage));
            return recievedMessage;
        }
        #endregion
        #region Message Informations
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
        protected virtual void PacketParser_OnPacketParsed(object sender, NetworkMessageReceivedEventArgs e) 
        {
            if (MessageReceived != null) MessageReceived(this, e);
        }
        protected virtual void PacketParser_OnMessageInformationsExtracted(object sender, NetworkMessageInformationExtractedEventArgs e)
        {
            if (MessageInformationExtracted != null) MessageInformationExtracted(this, e);
        }
        #endregion
        #region Events Handler
        private void NetworkManager_OnPacketReceived(object sender, PacketReceivedEventArgs e) => ProcessPacket(e.Packet);
        #endregion
        #endregion
    }
}
