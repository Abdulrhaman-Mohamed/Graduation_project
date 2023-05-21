using Repo_Core.Interface;
using Repo_Core.Models;
using System;
using System.Collections;
using System.Net.WebSockets;
using System.Runtime.ExceptionServices;

namespace Repo_EF.Repo_Method
{
    public class SocketsFactory : ISocketsFactory
    {
        private Hashtable hashtable = new Hashtable();

        public bool IsForgienSocketExits(int SocketID)
        {
            if (hashtable.ContainsKey(SocketID))
                return true;
            return false;
        }

        public WebSocket GetForgienSocket(int SocketID)
        {
            return (WebSocket)hashtable[SocketID];
        }

        public void SetSocket(WebSocket webSocket, int SocketID)
        {
            hashtable.Add(SocketID, webSocket);       
        }


        // this is only called if the websocket doesn't exist
        // you may use lock to controller access to the hashmap
        // but the project right now only one object wait the forgien websocket to connect.
        // and the other gets it directly.
        // object that is waiting websocket to connect is responsable for deleteing the websocket from the hash after getting it.
        public async Task<WebSocket> GetForgienSocketAsync(int SocketID)
        {
            while(true)
            {
                if (IsForgienSocketExits(SocketID))
                    break;
                await Task.Delay(5000);  // Check if socket exist every 5 seconds
            }
            WebSocket webSocket = GetForgienSocket(SocketID);
            await Task.Delay(1024);  // this line wait 1 second to ensure that the other object the is waiting the socket gets it before deleting it.
            hashtable.Remove(SocketID);
            return webSocket;

        }
    }
}
