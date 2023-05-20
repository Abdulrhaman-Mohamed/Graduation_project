using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;



public struct AESData
{
    public byte[] IV;
    public byte[] EncryptedData;
}

public enum AckType
{
    StartSetup,
    EndSetup,
    SendTelemetry,
    EndTelemetry,
    SendPlan,
    EndPlan,
    PlanReceived,
    PlanDone,
    PlanReSend,
    StratOnline,
    EndOnline
};

public enum FrameType
{
    Plan,
    Command,
    Request,
    Data,
    Image
};

public struct Header
{
    public FrameType Type;
    public byte[] IV;
    public int CRC;
    public int FrameLength;
}

public struct PlanBody
{
    public int NumberofPlans;
    public int NumberofFrames;
    public long Time;
}

public struct CommandBody
{
    public int PlanID;
    public int SequenceID;
    public int SubSystemID;
    public int CommandID;
    public int Delay;
    public int CommandRepeat;
}

public struct RequestBody
{
    public bool ACK;
    public AckType Type;
}

public struct RoverData
{
    public int PlanID;
    public int SequenceID;
    public long Time;
}

public struct ImageData
{
    public int PlanID;
    public int SequenceID;
    public long Time;
}

public struct SensorReadings
{
    public int X;
    public int Y;
    public int Z;
}

// Use Bitmap to accept array of bytes
/* Note Chatgpt responded to me with this when i asked "how to accept array of byte that represent image in struct"
Note that using a struct to hold large arrays of data, such as image data, may not be the most efficient approach. 
It is generally more efficient to work with arrays directly or use dedicated classes like Bitmap for image manipulation. 
Structs are typically used for small, lightweight data structures. 
Consider your specific requirements and performance considerations when deciding on the appropriate approach.
*/


namespace Repo_Core.Abstract
{
    public abstract class ABCSocket
    {
        // Data Types and Parameter will change.
        readonly private byte[] key = new byte[] { 0x00, 0x01, 0xA2, 0x03, 0xB4, 0x15, 0x06, 0xF5, 0xFF, 0x09, 0xF0, 0x0B, 0x9C, 0x0D, 0x0E, 0x0F };


        public abstract void SetClassSocket(WebSocket ClassWebSocekt);
        public abstract void SetForgeinSocket(WebSocket ForgienWebSocket);
        public abstract void CloseSocket();
        public abstract void Run();
        public abstract Task SendBytes(byte[] Buffer);
        public abstract Task AcceptBytes(byte[] Buffer);

        public int CRC(byte[] bytes)
        {
            int sum = 0;
            for(int i =0; i < bytes.Length; i++)
            {
                sum += bytes[i];
            }
            return sum;
        }

        public byte[] SerialiazationHeader(Header header)
        { throw new NotImplementedException(); }
        public byte[] SerialiazationPlan(PlanBody plan)
        { throw new NotImplementedException(); }
        public byte[] SerialiazationCommand(CommandBody command)
        { throw new NotImplementedException(); }
        public byte[] SerialiazationRequests(RequestBody request)
        { throw new NotImplementedException(); }


        public Header DeserialiazationHeader(byte[] bytes, int StartIndex)
        {
            Header header = new Header();

            header.Type = (FrameType)bytes[StartIndex];
            header.IV = new byte[16];
            for (int i = StartIndex; i < (StartIndex + 16); i++)
            {
                header.IV[i] = bytes[i + 1];
            }
            header.CRC = (int)(bytes[StartIndex + 17] << 8 | bytes[StartIndex + 18]);
            header.FrameLength = (int)(bytes[StartIndex + 19] << 8 | bytes[StartIndex + 20]);
            return header;

        }
        public SensorReadings DeserialiazationSensorData(byte[] bytes, int StartIndex)
        {
            SensorReadings sensorReadings = new SensorReadings();
            sensorReadings.X = (int)(bytes[StartIndex] << 8 | bytes[StartIndex + 1]);
            sensorReadings.Y = (int)(bytes[StartIndex + 2] << 8 | bytes[StartIndex + 3]);
            sensorReadings.Z = (int)(bytes[StartIndex + 4] << 8 | bytes[StartIndex + 5]);
            return sensorReadings;
        }
        public RoverData DeserialiazationBodyData(byte[] bytes, int StartIndex)
        {
            RoverData roverData = new RoverData();
            roverData.PlanID = (int)(bytes[StartIndex] << 8 | bytes[StartIndex + 1]);
            roverData.SequenceID = (int)(bytes[StartIndex + 2] << 8 | bytes[StartIndex + 3]);
            roverData.Time = (long)((bytes[StartIndex + 4] << 24) | (bytes[StartIndex + 5] << 16) | (bytes[StartIndex + 6] << 8) | bytes[StartIndex + 7]);
            return roverData;
        }
        public RequestBody DeserialiazationResponse(byte[] bytes, int StartIndex)
        {
            RequestBody requestBody = new RequestBody();
            requestBody.ACK = bytes[StartIndex] == 0;
            requestBody.Type = (AckType)bytes[StartIndex + 1];
            return requestBody;
        }
        
        // this well only return (PlanID, SequenceID, Time) you can discard this method or separate image from reset of the request
        public ImageData DeserialiazationImage(byte[] bytes, int StartIndex)
        { throw new NotImplementedException(); }


        protected byte[] EncryptStringToBytes_Aes(byte[] plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        protected byte[] DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(cipherText, 0, cipherText.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
        
        public AESData AESEncryption(byte[] Data)
        {
            AESData aesData = new AESData();
            int paddedLength = (Data.Length / 16 + 1) * 16;
            byte[] paddedBytes = new byte[paddedLength];
            Array.Copy(Data, paddedBytes, Data.Length);
            aesData.IV = new byte[16];
            
            using (Aes myAes = Aes.Create())
            {
                myAes.KeySize = key.Length * 8;
                myAes.Key= key;
                myAes.GenerateIV();

                aesData.IV = myAes.IV;
                aesData.EncryptedData = EncryptStringToBytes_Aes(paddedBytes, myAes.Key, myAes.IV);
            }
            return aesData;
        }
        
        public byte[] AESDecryption(AESData Data)
        {
            byte[] data = DecryptStringFromBytes_Aes(Data.EncryptedData, key, Data.IV);
            return data;
        }

    }
}
