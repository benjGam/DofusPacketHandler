using DofusPacketManager.Utils.IO;
using System;
using System.Threading;

namespace DofusPacketManager.Network.Messages
{
    public abstract class NetworkMessage
    {
        #region Properties
        #region Custom Events Declarations
        public event EventHandler Initialized;
        public event EventHandler Deserialized;
        #endregion 
        public abstract ushort MessageID { get; }
        public abstract bool InstanceID { get; }
        #endregion
        #region Constructors
        public NetworkMessage() {
            new Thread(() =>
            {
                Thread.Sleep(1);
                NetworkMessage_OnInitialized(this, EventArgs.Empty);
            }).Start();
        }
        #endregion
        #region Methods
        #region Public Methods
        public void Deserialize(IDataReader Reader)
        {
            DeserializeMessage(Reader);
            NetworkMessage_OnDeserialized(this, EventArgs.Empty);
        }
        #endregion
        #region Protected Abstract
        protected abstract void DeserializeMessage(IDataReader Reader);
        #endregion
        #endregion
        #region Custom Events
        protected virtual void NetworkMessage_OnDeserialized(object sender, EventArgs e)
        {
            if (Deserialized != null) Deserialized(this, e);
            Deserialized = null; //Unbind handlers
        }
        protected virtual void NetworkMessage_OnInitialized(object sender, EventArgs e)
        {
            if (Initialized != null) Initialized(this, e);
        }
        #endregion
    }
}
