using SharpPcap;
using DofusPacketManager.Utils;
using System.Linq;
using PacketDotNet;
using System;
using System.Threading;
using System.Net;
using DofusPacketManager.Network.Messages;

namespace DofusPacketManager.Network
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        #region Internal Properties
        private string _ipToSniff = string.Empty;
        private int _portToSniff = 5555;
        private ILiveDevice _Device = null;
        private MessageManager _messageManager;
        private Thread _sniffingThread;
        #region Custom Events Declaration
        public event EventHandler<PacketReceivedEventArgs> PacketReceived;
        #endregion
        #endregion
        #region Constructors
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
        #endregion
        #region Methods
        #region Private Methods
        private void Init()
        {
            _messageManager = MessageManager.Instance;
            _Device = CaptureDeviceList.Instance.ToList().Find((ILiveDevice Device) => Device.Description.ToLower().Contains("realtek")); // Get the real network device
            _Device.OnPacketArrival += Device_OnPacketArrival;
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
        #region Public Methods
        public void StartSniffing()
        {
            _sniffingThread = new Thread(() =>
            {
                if (!_Device.Started)
                {
                    _Device.Open();
                    _Device.Filter = $"tcp port {_portToSniff}";
                    _Device.StartCapture();
                }
            });
            _sniffingThread.IsBackground = true;
            _sniffingThread.Start();
        }
        public void StopSniffing()
        {
            _Device.OnPacketArrival -= Device_OnPacketArrival;
            _Device.StopCapture();
            if (_sniffingThread != null) _sniffingThread.Abort();
            _sniffingThread = null;
        }
        #endregion
        #endregion
        #region Events
        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            TcpPacket recievedPacket = ParsePacketAsTCP(e);
            if (recievedPacket == null) return;
            NetworkManager_OnPacketReceived(this, new PacketReceivedEventArgs(recievedPacket));
        }
        #region Custom Events
        protected virtual void NetworkManager_OnPacketReceived(object sender, PacketReceivedEventArgs e)
        {
            if (PacketReceived != null) PacketReceived(this, e);
        }
        #endregion
        #endregion
        #region Fields Accessors
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
        public bool Sniffing => _Device == null ? false : _Device.Started;
        #endregion
    }
}
