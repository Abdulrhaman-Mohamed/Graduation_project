using Repo_Core.Abstract;
using System.Net.WebSockets;

namespace Repo_EF.Repo_Method
{
    public class RoverSocket : ABCSocket
    {
        private WebSocket ClassSocket { get; set; }
        private WebSocket ForgienSocket { get; set; }
        private WebSocketReceiveResult ClassResult { get; set; }

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
            throw new NotImplementedException();
        }


        public override SensorReadings DeserialiazationData(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public override Header DeserialiazationHeader(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public override ImageData DeserialiazationImage(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public override RequestBody DeserialiazationResponse(byte[] bytes)
        {
            throw new NotImplementedException();
        }


        public override byte[] SerialiazationCommand(CommandBody command)
        {
            throw new NotImplementedException();
        }

        public override byte[] SerialiazationHeader(Header header)
        {
            throw new NotImplementedException();
        }

        public override byte[] SerialiazationPlan(PlanBody plan)
        {
            throw new NotImplementedException();
        }

        public override byte[] SerialiazationRequests(RequestBody request)
        {
            throw new NotImplementedException();
        }

        protected void InterruptHeader()
        {

        }

        protected void BodyData()
        {

        }

    }
}
