using SharpPcap;
using DofusPacketManager.Utils;
using System.Linq;
using PacketDotNet;
using System;
using System.Threading;
using System.Net;
using DofusPacketManager.Networking.Messages;

namespace DofusPacketManager.Networking
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        #region Global Variables
        private Thread _sniffingThread;
        #endregion
        #region Properties
        private string _ipToSniff = string.Empty;
        private int _portToSniff = 5555;
        private ILiveDevice _Device = null;
        private MessageManager _messageManager;

        #region Custom Events Declaration

        public event EventHandler OnPacketReceived;

        #endregion
        #endregion
        public NetworkManager() => Init();
        public NetworkManager(string ipToSniff) 
        { 
            _ipToSniff = ipToSniff;
            Init();
        }
        public NetworkManager(int portToSniff)
        {
            _portToSniff = portToSniff;
            Init();
        }
        public NetworkManager(string ipToSniff, int portToSniff) 
        { 
            _ipToSniff = ipToSniff;
            _portToSniff = portToSniff;
            Init();
        }
        private void Init()
        {
            _messageManager = MessageManager.Instance; // Link message manager instance to Network manager
            _Device = CaptureDeviceList.Instance.ToList().Find((ILiveDevice Device) => Device.Description.ToLower().Contains("realtek")); // Get the real network device
            _sniffingThread = new Thread(() => this.StartSniffing());
            _sniffingThread.IsBackground = true;
        }
        public void StartSniffing()
        {
            if (!_Device.Started)
            {
                _Device.Open();
                _Device.Filter = $"tcp port {_portToSniff}";
                _Device.OnPacketArrival += Device_OnPacketArrival;
                _Device.StartCapture();
            }
        }
        public void StopSniffing()
        {
            if (_Device.Started)
            {
                _Device.StopCapture();
                _Device.Close();
                _Device.OnPacketArrival -= Device_OnPacketArrival;
            }
        }
        #region Events
        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            TcpPacket recievedPacket = ParsePacketAsTCP(e);
            if (recievedPacket == null) return;
            OnPacketArrived(this, new PacketArrivedEventArgs(recievedPacket));
        }
        private TcpPacket ParsePacketAsTCP(PacketCapture e)
        {
            Packet Packet = Packet.ParsePacket(e.GetPacket().LinkLayerType, e.GetPacket().Data);
            IPPacket ipPacket = Packet.Extract<IPPacket>();
            if (ipPacket == null || ipPacket.SourceAddress == IPAddressUtils.GetHostV4Address()) return null;
            if (_ipToSniff != string.Empty && ipPacket.SourceAddress != IPAddress.Parse(_ipToSniff)) return null;
            TcpPacket TcpPacket = Packet.Extract<TcpPacket>();
            return TcpPacket == null || TcpPacket.PayloadData.Length <= 2 ? null : TcpPacket;
        }

        #endregion
        #region CustomEvents
        protected virtual void OnPacketArrived(object sender, PacketArrivedEventArgs e)
        {
            if (OnPacketReceived == null) return;
            OnPacketReceived(this, e);
        }
        #endregion
        #region Accessors
        public string TargetedIP
        {
            get => _ipToSniff;
            set {
                IPAddress parsedIp;
                if (IPAddress.TryParse(_ipToSniff, out parsedIp))
                {
                    _ipToSniff = value;
                    return;
                }
                _ipToSniff = string.Empty;
            }
        }
        public int TargetedPort
        {
            get => _portToSniff; 
            set {
                if (value < 333 && value > 65535) return;
                if (_Device.Started)
                {
                    StopSniffing();
                    _portToSniff = value;
                    StartSniffing();
                }
            }
        }
        public MessageManager MessageManager => _messageManager;
        #endregion
    }
}
