using DofusPacketManager.Network;
using System;
using System.Windows.Forms;
using DofusPacketManager.Network.Debugging;

namespace DofusPacketManager
{
    internal partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            new NetworkManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkManager.Instance.StartSniffing();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new PacketLoggerForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetworkManager.Instance.StopSniffing();
        }
    }
}
