using System;

namespace DofusPacketManager.Network.Messages.CustomEventArgs
{
    public class NetworkMessageInformationExtractedEventArgs : EventArgs
    {
        private NetworkMessageInformations _informations;
        private NetworkMessage _networkMessage;

        public NetworkMessageInformationExtractedEventArgs(NetworkMessageInformations informations, NetworkMessage networkMessage)
        {
            _informations = informations;
            _networkMessage = networkMessage;
        }

        public NetworkMessageInformations Informations => _informations;
        public NetworkMessage Message => _networkMessage;
    }
}
