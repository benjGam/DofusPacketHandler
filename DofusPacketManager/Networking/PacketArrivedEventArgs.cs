using System;
using PacketDotNet;

namespace DofusPacketManager.Networking
{
    public class PacketArrivedEventArgs : EventArgs
    {
        private TcpPacket _packet;

        public PacketArrivedEventArgs(TcpPacket recievedPacket)
        {
            _packet = recievedPacket;
        }

        public TcpPacket RecievedPacket => _packet;
    }
}
