using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace WebSocketChatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tamam_Click(object sender, EventArgs e)
        {
            statics.username = textBox1.Text;
            ana chatform = new ana();
            chatform.Show();
            this.Hide();
        }
    }
}
