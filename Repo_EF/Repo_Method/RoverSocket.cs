using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repo_Core.Abstract;
using System.Net.WebSockets;
using Repo_Core.Models;
using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Drawing;

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

        public override async Task RunTest()
        {
            var buffer = new byte[1024];
            //await SendBytes(new byte[] { 69, 57 });
            //await ForgienSocket.SendAsync(
            //new ArraySegment<byte>(new byte[] { 55, 57 }, 0, 2),
            //WebSocketMessageType.Text,
            //true,
            //CancellationToken.None
            //);
            await AcceptBytes(buffer);
            //var re = await ClassSocket.ReceiveAsync(
            //    new ArraySegment<byte>(buffer), CancellationToken.None);
            Console.WriteLine("Wait Rover");
            while (!ClassResult.CloseStatus.HasValue)
            {
                await SendBytes(buffer);
                await AcceptBytes(buffer);
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
            byte[] DataBuffer = new byte[10240];
            var imageBuffer = new List<byte>();
            await AcceptBytes(DataBuffer);
            Header header = DeserialiazationHeader(DataBuffer, 0);
            if (header.Type == FrameType.Image)
            {
                imageBuffer.AddRange(DataBuffer);
                while(!Encoding.UTF8.GetString(DataBuffer).Contains("done"))
                {
                    await AcceptBytes(DataBuffer);
                    imageBuffer.AddRange(DataBuffer);
                }

                //using (MemoryStream ms = new MemoryStream(imageBuffer.ToArray()))
                //{
                //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                //    image.Save($"P{Guid.NewGuid()}.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //}
                await SendBytes(imageBuffer.ToArray());
            }

            else
            {
                PlanResult Result = BodyDeserialiazation(DataBuffer, 21);
                SaveResultfromarduino(Result);
                byte[] BytePlanResult = PlanResultToByte(Result);
                await SendBytes(BytePlanResult);
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
