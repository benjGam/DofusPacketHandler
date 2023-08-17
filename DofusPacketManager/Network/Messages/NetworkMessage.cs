using DofusPacketManager.Utils.IO;
using System;
using System.Threading;

namespace DofusPacketManager.Networking.Messages
{
    public abstract class NetworkMessage
    {
        #region Properties
        #region Custom Events Declarations
        public event EventHandler OnCreated;
        public event EventHandler OnDeserialized;
        #endregion 
        public abstract ushort MessageID { get; }
        public abstract bool InstanceID { get; }
        #endregion
        #region Constructors
        public NetworkMessage() {
            new Thread(() =>
            {
                Thread.Sleep(1);
                NetworkMessage_OnCreated(this, EventArgs.Empty);
            }).Start();
        }
        #endregion
        #region Methods
        #region Public Methods
        public void Deserialize(IDataReader Reader)
        {
            DeserializeMessage(Reader);
            NetworkMessage_Deserialized(this, EventArgs.Empty);
            OnDeserialized = null; //Unbind handlers
        }
        #endregion
        #region Protected Abstract
        protected abstract void DeserializeMessage(IDataReader Reader);
        #endregion
        #endregion
        #region Custom Events
        protected virtual void NetworkMessage_Deserialized(object sender, EventArgs e)
        {
            if (OnDeserialized != null)
                OnDeserialized(this, e);
        }
        protected virtual void NetworkMessage_OnCreated(object sender, EventArgs e)
        {
            if (OnCreated != null)
                OnCreated(this, e);
        }
        #endregion
    }
}
