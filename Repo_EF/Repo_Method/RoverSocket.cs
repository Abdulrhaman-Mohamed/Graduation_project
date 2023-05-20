using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repo_Core.Abstract;
using System.Net.WebSockets;

namespace Repo_EF.Repo_Method
{
    public enum WebsocketStates
    {
        AcceptTelemetry,
        SendPlan,
        SendInvalidPlan,
        OnlineSession,
        EndSession
    }
    public class RoverSocket : ABCSocket
    {
        private WebSocket ClassSocket { get; set; }
        private WebSocket ForgienSocket { get; set; }
        private WebSocketReceiveResult ClassResult { get; set; }
        private WebsocketStates Socketstate { get; set; }

        public override async Task AcceptBytes(byte[] Buffer)
        {
            ClassResult = await ClassSocket.ReceiveAsync(
                new ArraySegment<byte>(Buffer),
                CancellationToken.None
                ); ;
        }

        public override async Task SendBytes(byte[] Buffer)
        {
            await ForgienSocket.SendAsync(
                new ArraySegment<byte>(Buffer, 0, ClassResult.Count),
                ClassResult.MessageType,
                ClassResult.EndOfMessage,
                CancellationToken.None
            );
        }

        public override void SetClassSocket(WebSocket ClassWebSocekt)
        {
            ClassSocket = ClassWebSocekt;
        }

        public override void SetForgeinSocket(WebSocket ForgienWebSocket)
        {
            ForgienSocket = ForgienWebSocket;
        }
        
        public override async void CloseSocket()
        {
            await ClassSocket.CloseAsync(
                ClassResult.CloseStatus.Value,
                ClassResult.CloseStatusDescription,
                CancellationToken.None);
            ClassSocket.Dispose();
        }

        public override void Run()
        {
            Socketstate = WebsocketStates.AcceptTelemetry;
            while(true)
            {
                switch(Socketstate)
                {
                    case WebsocketStates.AcceptTelemetry:
                        AcceptTelemetery();
                        break;
                    case WebsocketStates.SendPlan:
                        SendPlan(1);
                        break;
                    case WebsocketStates.SendInvalidPlan:
                        SendPlan(1);
                        break;
                    case WebsocketStates.OnlineSession:
                        StartOnline();
                        break;
                    case WebsocketStates.EndSession:
                        // do some stuff
                        break;
                    
                }
            }
        }


        protected async void AcceptTelemetery()
        {
            byte[] DataBuffer = new byte[64];

            await AcceptBytes(DataBuffer);
            Header header = DeserialiazationHeader(DataBuffer, 0);
            if(header.Type == FrameType.Request)
            {

            }

        }

        protected void SendPlan(int PlanID)
        {

        }

        protected void StartOnline()
        {

        }

        protected byte[] HandleTelemetryRequset(byte[] Buffer, int index )
        {
            RequestBody requestBody = DeserialiazationResponse(Buffer, index);
            if(requestBody.Type == AckType.EndTelemetry)
            {
                Socketstate = WebsocketStates.SendPlan;
                //Header SendHeader = header;
                //RequestBody request = new RequestBody();
                //byte[] EncryptData = new byte[] { 0, (byte)AckType.EndTelemetry };
                //AESData aesData = AESEncryption(EncryptData);
                //SendHeader.IV = aesData.IV;
                //SendHeader.CRC = CRC(aesData.EncryptedData);
                //SendHeader.FrameLength = aesData.EncryptedData.Length;

            }

            return new byte[]{ 0,1};
        }  
        
        protected byte[] HandleTelemetryData(byte[] Buffer, int index)
        {

            return new byte[] { 0, 1 };
        }

        protected byte[] HandleSendPlanRequset(byte[] Buffer, int index)
        {
            RequestBody requestBody = DeserialiazationResponse(Buffer, index);
            
            if(requestBody.Type == AckType.EndPlan)
            {
                Socketstate = WebsocketStates.OnlineSession;
            }
            else if(requestBody.Type == AckType.PlanReSend)
            {
                Socketstate = WebsocketStates.SendInvalidPlan;
            }
            else if(requestBody.Type == AckType.PlanReceived)
            {
                // go to next plan
            }

            return new byte[] { 0, 1 };
        }

        protected byte[] HandleOnlineRequest(byte[] Buffer, int index)
        {
            RequestBody requestBody = DeserialiazationResponse(Buffer, index);
            
            if(requestBody.Type == AckType.EndOnline)
            {
                Socketstate = WebsocketStates.EndSession;
            }
            return new byte[] { 0, 1 };
        }


    }
}
