using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo_Core.Abstract
{
    abstract class Socket
    {
        // Data Types and Parameter will change.
        protected byte[] IV = new byte[16];
        public abstract void SetSocket();
        public abstract void CloseSocket();
        public abstract void Serialiazation();
        public abstract void Deserialiazation();
        public abstract void Run();
        public abstract void SendBytes();
        public abstract void AcceptBytes();
        public byte[] GetIV()
        {
            byte[] IV = new byte[16];
            return IV;
        }
        public string AESEncryption(byte[] Data)
        {
            return "";
        }
        public byte[] AESDecryption(string Data)
        {
            byte[] data = new byte[50];
            return data;
        }

    }
}
