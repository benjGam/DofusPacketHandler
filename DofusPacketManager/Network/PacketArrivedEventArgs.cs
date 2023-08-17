using System;
using PacketDotNet;

namespace DofusPacketManager.Networking
{
    public class PacketRecievedEventArgs : EventArgs
    {
        private TcpPacket _packet;

        public PacketRecievedEventArgs(TcpPacket recievedPacket)
        {
            _packet = recievedPacket;
        }
        public TcpPacket RecievedPacket => _packet;
    }
}
