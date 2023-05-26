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
        private Hashtable ClassTable = new Hashtable();
        private Hashtable ForginTable = new Hashtable();

        public bool IsSocketExits(int SocketID)
        {
            if (ClassTable.ContainsKey(SocketID))
                return true;
            return false;
        }

        public bool SocketWait(int SocketID)
        {
            if (ForginTable.Contains(SocketID))
                return true;
            return false;
        }

        public WebSocket GetSocket(int SocketID)
        {
            WebSocket webSocket = (WebSocket)ClassTable[SocketID];
            ClassTable.Remove(SocketID);
            return webSocket;
        }

        public void SetClassSocket(WebSocket webSocket, int SocketID)
        {
            ClassTable.Add(SocketID, webSocket);       
        }

        public void SetForginSocket(WebSocket webSocket, int SocketID)
        {
            ForginTable.Add(SocketID, webSocket);
        }

        // this is only called if the websocket doesn't exist
        // you may use lock to controller access to the hashmap
        // but the project right now only one object wait the forgien websocket to connect.
        // and the other gets it directly.
        // object that is waiting websocket to connect is responsable for deleteing the websocket from the hash after getting it.
        public async Task<WebSocket> GetSocketAsync(int SocketID)
        {
            while(true)
            {
                if (SocketWait(SocketID))
                    break;
                await Task.Delay(5000);  // Check if socket exist every 5 seconds
            }
            WebSocket webSocket = (WebSocket)ForginTable[SocketID];
            await Task.Delay(1024);  // this line wait 1 second to ensure that the other object the is waiting the socket gets it before deleting it.
            ForginTable.Remove(SocketID);
            return webSocket;

        }
    }
}
