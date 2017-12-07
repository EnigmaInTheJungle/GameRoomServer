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
        public void TestMethodWebSocketWithService()
        {
            var us = new UsersSocket();
            var ws = new WebSocket("ws://localhost:4649");
            ws.OnOpen += (sender, e) => ws.Send("Hi, there!");
            ws.Connect();
            
            Assert.AreEqual(true, wssv.IsListening);
        }
    }
}
