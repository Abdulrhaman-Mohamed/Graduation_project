﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;


struct AESData
{
    byte[] IV;
    byte[] EncryptedData;
}
enum AckType
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

enum FrameType
{
    Plan,
    Command,
    Request,
    Data,
    Image
};

struct Header
{
    FrameType Type;
    byte[] IV;
    int CRC;
    int FrameLength;
}

struct PlanBody
{
    int NumberofPlans;
    int NumberofFrames;
    long Time;
}

struct CommandBody
{
    int PlanID;
    int SequenceID;
    int SubSystemID;
    int CommandID;
    int Delay;
    int CommandRepeat;
}

struct RequestBody
{
    bool ACK;
    AckType Type;
}

struct RoverData
{
    int PlanID;
    int SequenceID;
    long Time;
}

struct ImageData
{
    int PlanID;
    int SequenceID;
    long Time;
}

struct SensorReadings
{
    int X;
    int Y;
    int Z;
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
    abstract class Socket
    {
        // Data Types and Parameter will change.

        public abstract byte[] SerialiazationHeader(Header header);
        public abstract byte[] SerialiazationPlan(PlanBody plan);
        public abstract byte[] SerialiazationCommand(CommandBody command);
        public abstract byte[] SerialiazationRequests(RequestBody request);

        public abstract Header DeserialiazationHeader(byte[] bytes);
        public abstract SensorReadings DeserialiazationData(byte[] bytes);
        // this well only return (PlanID, SequenceID, Time) you can discard this method or separate image from reset of the request
        public abstract ImageData DeserialiazationImage(byte[] bytes);
        public abstract RequestBody DeserialiazationResponse(byte[] bytes);


        public abstract void SetSocket(WebSocket webSocket, int SocketID);
        public abstract void CloseSocket(int SocketID);
        public abstract void Run();
        public abstract Task SendBytes(byte[] Buffer);
        public abstract Task AcceptBytes(byte[] Buffer);

        public AESData AESEncryption(byte[] Data)
        {
            AESData aes = new AESData();
            return aes;
        }
        public byte[] AESDecryption(AESData Data)
        {
            byte[] data = new byte[50];
            return data;
        }

    }
}
