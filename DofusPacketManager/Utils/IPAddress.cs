using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace DofusPacketManager.Utils
{
    public static class IPAddressUtils
    {
        public static IPAddress GetHostV4Address() => Dns.GetHostAddresses(Dns.GetHostName()).ToList().Find((IPAddress ipAddress) => ipAddress.AddressFamily == AddressFamily.InterNetwork);
    }
}
