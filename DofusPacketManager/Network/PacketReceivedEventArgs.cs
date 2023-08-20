using System;
using PacketDotNet;

namespace DofusPacketManager.Network
{
    public class PacketReceivedEventArgs : EventArgs
    {
        private TcpPacket _packet;

        public PacketReceivedEventArgs(TcpPacket recievedPacket)
        {
            _packet = recievedPacket;
        }
        public TcpPacket Packet => _packet;
    }
}
