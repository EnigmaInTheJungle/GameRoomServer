using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameRoomsAPI.WebSockets;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GameRoomsAPI.WebSockets.Actions;
using GameRoomsAPI.DataBases.DAO;
using System.Linq;

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
        public void TestMethodWebSocketWithClient()
        {
            var ws = new WebSocket("ws://localhost:4649/UsersSocket");
            string getMsg = "";
            ws.OnOpen += (sender, e) => ws.Send("Hi, there!");
            ws.OnMessage += (sender, e) => getMsg = e.Data;
            ws.Connect();
            
            ws.Ping("ping");
            ws.Send("msg");

            Assert.AreEqual("", getMsg);
        }

        [TestMethod]
        public void TestMethodWebSocketGetUsers()
        {
            var ws = new WebSocket("ws://localhost:4649/UsersSocket");
            string getMsg = "";
            ws.OnOpen += (sender, e) => ws.Send("Hi, there!");
            ws.OnMessage += (sender, e) => getMsg = e.Data;
            ws.Connect();

            ws.Ping("ping");

            UserDao userDao = new UserDao();
            ws.Send(JsonConvert.SerializeObject(new { type = UserActions.GET_ONLINE_USERS }));

            Assert.AreEqual("", getMsg);
        }

        [TestMethod]
        public void TestMethodWebSocketSendMsg()
        {
            var ws = new WebSocket("ws://localhost:4649/UsersSocket");
            string getMsg = "";
            ws.OnOpen += (sender, e) => ws.Send("Hi, there!");
            ws.OnMessage += (sender, e) => getMsg = e.Data;
            ws.Connect();

            ws.Ping("ping");

            UserDao userDao = new UserDao();
            ws.Send(JsonConvert.SerializeObject(new { type = UserActions.UPDATE_USERS, users = userDao.All().Where(p => p.Online) }));

            Assert.AreEqual("{}", getMsg);
        }
    }
}
