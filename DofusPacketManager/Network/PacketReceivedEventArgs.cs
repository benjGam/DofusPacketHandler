using System;
using PacketDotNet;

namespace DofusPacketManager.Networking
{
    public class PacketReceivedEventArgs : EventArgs
    {
        private TcpPacket _packet;

        public PacketReceivedEventArgs(TcpPacket recievedPacket)
        {
            _packet = recievedPacket;
        }
        public TcpPacket RecievedPacket => _packet;
    }
}
