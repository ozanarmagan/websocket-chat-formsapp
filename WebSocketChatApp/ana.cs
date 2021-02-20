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
using WebSocketSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebSocketChatApp
{
    public partial class ana : Form
    {
        Status chat = new Status();
        WebSocket ws = new WebSocket(statics.websocketurl);
        public ana()
        {
            InitializeComponent();
        }





        void ws_OnMessage(object sender, WebSocketSharp.MessageEventArgs e)
        {
            dynamic jsonparsed = JObject.Parse(e.Data);
            if (jsonparsed["message_type"] == "online_users")
            {
                onlineusers.Invoke(new Action(() => { onlineusers.Items.Clear(); }));
                foreach (string user in jsonparsed["users"])
                {
                    onlineusers.Invoke(new Action(() => { onlineusers.Items.Add(user); }));
                }
            }
            else if(jsonparsed["message_type"] == "new_message")
            {
                string newmessage = "["+ jsonparsed.time+"]" + "[" + jsonparsed.user + "]:" + jsonparsed.message;
                richTextBox1.Invoke(new Action(() => { richTextBox1.Text += newmessage  + Environment.NewLine; }));
            }

        }

        void ws_OnOpen(object sender,EventArgs e)
        {
            string adduser = "{\"type\":\"add_user\",\"content\":\"" + statics.username + "\"}";
            ws.Send(adduser);
        }


        private void ana_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/chat.log"))
            {
                StreamReader chatfile;
                chatfile = File.OpenText(Application.ExecutablePath + "/chat.log");
                chat.chat = chatfile.ReadToEnd();
            }
            richTextBox1.Text = chat.chat;
            label1.Text = statics.username;
            ws.OnMessage += ws_OnMessage;
            ws.OnOpen += ws_OnOpen;
            ws.Connect();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = textBox1.Text;
            textBox1.Clear();
            ws.Send("{\"type\":\"new_message\",\"message\":\"" + message + "\"}");
        }

        private void ana_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
