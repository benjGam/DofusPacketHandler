using DofusPacketManager.Networking;
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
            new NetworkManager();
            NetworkManager.Instance.OnPacketReceived += Instance_OnPacketReceived;
        }

        private void Instance_OnPacketReceived(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() => richTextBox1.Text += "Packet recieved !\n"));
        }

    }
}
