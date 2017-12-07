using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameRoomsAPI.WebSockets;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsUsersSocket
    {
        WebSocketServer wssv = null;
        [TestInitialize]
        public void StartServer()
        {
            wssv = new WebSocketServer(4649);
            wssv.AddWebSocketService<UsersSocket>("/UsersSocket");
            wssv.Start();
        }

        [TestCleanup]
        public void ServerStop()
        {
            wssv.Stop();
        }

        [TestMethod]
        public void TestWebSocketWithServiceIsListening()
        {
            Assert.AreEqual(true, wssv.IsListening);
        }

        [TestMethod]
        public void TestUsersSocketStateConnecting()
        {
            var us = new UsersSocket();
            Assert.AreEqual(WebSocketState.Connecting, us.State);
        }

        [TestMethod]
        public void TestMethodWebSocketWithService()
        {
            var ws = new WebSocket("ws://localhost:4649/UsersSocket");
            string getMsg = "";
            ws.OnOpen += (sender, e) => ws.Send("Hi, there!");
            ws.OnMessage += (sender, e) => getMsg = e.ToString();
            ws.Connect();
            
            ws.Ping("ping");
            ws.Send("msg");

            Assert.AreEqual("", getMsg);
        }
    }
}
