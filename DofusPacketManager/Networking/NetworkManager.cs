using SharpPcap;
using DofusPacketManager.Utils;
using System.Linq;
using System.Windows.Forms;
using System;
using System.Net;
using SharpPcap.LibPcap;
using DofusPacketManager.Utils.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace DofusPacketManager.Networking
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        #region Properties
        private string _ipToSniff = string.Empty;
        private ILiveDevice _Device = null;

        #region Custom Events Declaration

        public event EventHandler OnPacketReceived;

        #endregion

        #endregion
        public NetworkManager(string ipToSniff) 
        { 
            _ipToSniff = ipToSniff;
            Init();
        }
        public NetworkManager() => Init();
        private void Init()
        {
            _Device = CaptureDeviceList.Instance.ToList().Find((ILiveDevice Device) => Device.Description.ToLower().Contains("realtek")); // Get the real network device
            this.StartSniffing();
        }
        public void StartSniffing()
        {
            if (!_Device.Started)
            {
                _Device.Open();
                _Device.Filter = "tcp port 5555";
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
            
        }

        #endregion

        #region CustomEvents
        protected virtual void OnPacketArrived(object sender, EventArgs e)
        {
            OnPacketReceived?.Invoke(this, e);
        }
        #endregion

        #region Accessors
        public string TargetedIP
        {
            get => _ipToSniff;
            set {

            }
        }
        #endregion
    }
}
