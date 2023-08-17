using DofusPacketManager.Network.Messages;
using DofusPacketManager.Network.Messages.game.chat;
using DofusPacketManager.Networking;
using DofusPacketManager.Networking.Messages;
using System;
using System.Windows.Forms;

namespace DofusPacketManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkManager nM = new NetworkManager();
            MessageManager k = MessageManager.Instance;
            k.MessageBinder.On<ChatServerMessage>(Test);
            nM.StartSniffing();
        }

        private object Test()
        {
            MessageBox.Show("J'affiche un test quand je reçoi le message");
            return null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetworkManager.Instance.StopSniffing();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBinder d = new MessageBinder();
            d.On<ChatServerMessage>(Test);
        }
    }
}
