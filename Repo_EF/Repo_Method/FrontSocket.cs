using Microsoft.VisualBasic;
using Repo_Core.Abstract;
using Repo_Core.Models;
using System.Net.WebSockets;
using System.Security.Cryptography;

namespace Repo_EF.Repo_Method
{
    public class FrontSocket : ABCSocket
    {
        private WebSocket? ClassSocket { get; set; }
        private WebSocket? ForgienSocket { get; set; }
        private WebSocketReceiveResult ClassResult { get; set; }


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
            throw new NotImplementedException();
        }

        public override async Task RunTest()
        {
            var buffer = new byte[1024];
            //await SendBytes(new byte[] { 69, 57 }, 0, 2);
            //await ForgienSocket.SendAsync(
            //new ArraySegment<byte>(new byte[] { 69, 57 }, 0, 2),
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
        protected async void StartOnline()
        {
            byte[] DataBuffer = new byte[64];

            await AcceptBytes(DataBuffer);
            byte[] DataBuffer2 = DataBuffer;

            CommandBody body = new CommandBody();
            body.PlanID = 0;
            body.SequenceID = 0;
            body.SubSystemID = 0;
            body.CommandID = RoverMoveMap(DataBuffer2);
            body.CommandRepeat = 1;
            body.Delay = 1;
            // Serialize Data

            Header Header = new Header();
            Header.Type = FrameType.Command;
            Header.FrameLength = 16;
            Aes myAes = Aes.Create();
            Header.IV = myAes.IV;
            // Calu CRC
            // Header.CRC = CRC(DataBuffer2)
            //Serialize Header
            // Send SendBytes(DataBuffer2);

        }

        protected int RoverMoveMap(byte[] Key)
        {
            int Map = 0;
            switch(Key[0])
            {
               
            }
            return Map;
        }

    }
}
