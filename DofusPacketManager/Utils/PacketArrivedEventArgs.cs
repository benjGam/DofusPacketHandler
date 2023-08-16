using System;
using SharpPcap;

namespace DofusPacketManager.Utils
{
    internal class PacketArrivedEventArgs : EventArgs
    {
        public RawCapture _packet = null;
        public PacketArrivedEventArgs(PacketCapture capture)
        {
            _packet = capture.GetPacket();
        }

    }
}
