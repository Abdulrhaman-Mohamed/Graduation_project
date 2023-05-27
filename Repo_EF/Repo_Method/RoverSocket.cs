using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repo_Core.Abstract;
using System.Net.WebSockets;
using Repo_Core.Models;
using System;
using System.Text;

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
        private PlanResult PlanResultInstance = new PlanResult();

        private readonly ApplicationDbContext _dbContext;
        public RoverSocket(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task AcceptBytes(byte[] Buffer)
        {
            ClassResult = await ClassSocket.ReceiveAsync(
                new ArraySegment<byte>(Buffer),
                CancellationToken.None
                );
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
            byte[] DataBuffer2 = DataBuffer;
            Header header = DeserialiazationHeader(DataBuffer2, 0);

            if (header.Type == FrameType.Request)
            {
                RequestBody requestBody = DeserialiazationResponse(DataBuffer2, 21);
                if (requestBody.Type == AckType.EndTelemetry)
                    Socketstate = WebsocketStates.SendPlan;
            }
            else
            {
                PlanResult Result = BodyDeserialiazation(DataBuffer2, 21);
                SaveResultfromarduino(Result);
            }
        }

        protected void SendPlan(int PlanID)
        {

        }

        protected async void StartOnline()
        {
            byte[] DataBuffer = new byte[64];

            await AcceptBytes(DataBuffer);
            byte[] DataBuffer2 = DataBuffer;
            Header header = DeserialiazationHeader(DataBuffer2, 0);
            if (header.Type == FrameType.Image)
            {
            }
            else
            {
                PlanResult Result = BodyDeserialiazation(DataBuffer2, 21);
                SaveResultfromarduino(Result);
                byte[] BytePlanResult = PlanResultToByte(Result);
                SendBytes(DataBuffer2);
            }
        }

        protected byte[] PlanResultToByte(PlanResult planResult)
        {
            byte[] BytePlanResult = Encoding.UTF8.GetBytes(string.Join(',', planResult.PlanSequenceNumber, planResult));
            return BytePlanResult;
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

        private PlanResult SaveResultfromarduino(PlanResult planResult)
        {
            _dbContext.PlanResults.AddAsync(planResult);
            return planResult;
        }
    }
}
