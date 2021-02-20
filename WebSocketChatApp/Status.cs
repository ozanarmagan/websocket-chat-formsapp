using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketChatApp
{
    class Status
    {
        public string chat = "";
        public List<string> online_users = new List<string>();
        public void add_online_user(string user)
        {
            online_users.Add(user);
        }
        public void remove_online_user(string user)
        {
            online_users.Remove(user);
        }
    }
}
