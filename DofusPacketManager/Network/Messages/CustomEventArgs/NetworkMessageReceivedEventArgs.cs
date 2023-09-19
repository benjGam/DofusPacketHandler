using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DofusPacketManager.Network.Messages.CustomEventArgs
{
    public class NetworkMessageReceivedEventArgs : EventArgs
    {
        private readonly NetworkMessage _recievedMessage;
        public NetworkMessageReceivedEventArgs(NetworkMessage recievedMessage) => _recievedMessage = recievedMessage;
        public NetworkMessage RecievedMessage => _recievedMessage;
    }
}
