using System;

namespace DofusPacketManager.Network.Messages
{
    public class NetworkMessageReceivedEventArgs : EventArgs
    {
        private readonly NetworkMessage _recievedMessage;
        public NetworkMessageReceivedEventArgs(NetworkMessage recievedMessage) => _recievedMessage = recievedMessage;
        public NetworkMessage RecievedMessage => _recievedMessage;
    }
}
