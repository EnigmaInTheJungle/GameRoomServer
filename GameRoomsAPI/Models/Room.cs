using GameRoomsAPI.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebSocketSharp.Server;

namespace GameRoomsAPI.Models
{
    public class Room
    {
        private WebSocketServer _webSocket;
        private List<string> _currentPlayers = new List<string>();

        public int Id { get; set; }
        public string Host { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string WebSocketUrl { get; set; }
        public int MaxPlayers { get; set; }
        public int CurrentPlayers { get; set; }

        public Room()
        {

        }

        public void AddUser(string user)
        {
            //_currentPlayers.Add(user);
            CurrentPlayers++;
        }

        public void DeleteUser(string user)
        {
            //_currentPlayers.Add(user);
            CurrentPlayers--;
        }

        public void StartServer(int id)
        {
            WebSocket.WS.AddWebSocketService<ChatSocket>("/Room/" + id);
            WebSocketUrl = "ws://localhost:8888/Room/" + id;
        }

        public Room(string title, string imageUrl, int maxPlayers, string host)
        {
            Title = title;
            ImageUrl = imageUrl;
            MaxPlayers = maxPlayers;
            Host = host;       
        }

        public Room(Room room)
        {
            Title = room.Title;
            ImageUrl = room.ImageUrl;
            MaxPlayers = room.MaxPlayers;
            Host = room.Host;
        }

        public void Update(Room room)
        {
            Title = room.Title;
            ImageUrl = room.ImageUrl;
            MaxPlayers = room.MaxPlayers;
            Host = room.Host;
            //AddUser(Host);
        }
    }
}