using GameRoomsAPI.DAO;
using GameRoomsAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace GameRoomsAPI.Helpers
{
    public class TTTSocket : WebSocketBehavior
    {
        public TTTSocket(int id)
        {
            _roomId = id;
        }
        RoomDao roomDao = new RoomDao();
        int _roomId = 0;
        public void playersMove(string player, int cellID)
        {
            _roomId = Convert.ToInt32(Context.RequestUri.AbsolutePath.Split('/').Last());
            if (player == GameState.gameState[_roomId].players[GameState.gameState[_roomId].whosNext])
            {
                int cellValue = 0;
                if (player == "red")
                    cellValue = 1;
                else if (player == "blue")
                    cellValue = -1;
                GameState.gameState[_roomId].board[cellID] = cellValue;
                foreach(var indexes in GameState.winnerIndexes)
                {
                    if (GameState.gameState[_roomId].board[indexes[0]] * GameState.gameState[_roomId].board[indexes[1]] * GameState.gameState[_roomId].board[indexes[2]] != 0)
                    {
                        if(GameState.gameState[_roomId].board[indexes[0]] == GameState.gameState[_roomId].board[indexes[1]] && GameState.gameState[_roomId].board[indexes[2]] == GameState.gameState[_roomId].board[indexes[1]])
                        {
                            GameState.gameState[_roomId].winnerPresent = true;
                            Sessions.Broadcast(JsonConvert.SerializeObject(new
                            {
                                type = "winner",
                                winner = (GameState.gameState[_roomId].board[indexes[0]] == 1) ? "Red Player Won!" : "Blue Player Won!"
                            }));
                        }
                    }
                }
                if (GameState.gameState[_roomId].whosNext >= 1)
                    GameState.gameState[_roomId].whosNext = 0;
                else
                    GameState.gameState[_roomId].whosNext += 1;
            }
        }
        public void resetBoard()
        {
            _roomId = Convert.ToInt32(Context.RequestUri.AbsolutePath.Split('/').Last());
            GameState.gameState[_roomId].whosNext = 0;
            GameState.gameState[_roomId].board = new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0};
        }
        public void updateClientState()
        {
            _roomId = Convert.ToInt32(Context.RequestUri.AbsolutePath.Split('/').Last());
            if (GameState.gameState[_roomId].winnerPresent != true)
            {
                Sessions.Broadcast(JsonConvert.SerializeObject(new
                {
                    type = "update",
                    whosTurn = GameState.gameState[_roomId].players[GameState.gameState[_roomId].whosNext],
                    board = GameState.gameState[_roomId].board
                }));
            }
        }
     
        protected override void OnOpen()
        {
            //_roomId = Convert.ToInt32(Context.RequestUri.AbsolutePath.Split('/').Last());
            Room room = roomDao.AddUser(_roomId, null);      
            Sessions.Broadcast(JsonConvert.SerializeObject(new { type = "userJoined", players = room.CurrentPlayers }));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            JToken token = JObject.Parse(e.Data);
            if((string)token.SelectToken("type") == "setupRequest")
            {
                Send(JsonConvert.SerializeObject(new
                {
                    type= "setup",
                    playerData= new {
                        whosTurn= GameState.gameState[_roomId].players[GameState.gameState[_roomId].whosNext],
                        playerID= GameState.gameState[_roomId].players[GameState.gameState[_roomId].numPlayers++],
                        board= GameState.gameState[_roomId].board
                    }
                }));        
            }
            else if ((string)token.SelectToken("type") == "move")
            {
                playersMove((string)token.SelectToken("playerID"), (int)token.SelectToken("cellID"));
                updateClientState();
            }            
        }

        protected override void OnClose(CloseEventArgs e)
        {
            roomDao.RemoveUser(_roomId, null);
            Room room = roomDao.Get(_roomId);

            if (room.CurrentPlayers == 0)
                roomDao.Delete(_roomId);

            Sessions.Broadcast(JsonConvert.SerializeObject(new { type = "userJoined", players = room.CurrentPlayers }));
        }

        protected override void OnError(ErrorEventArgs e)
        {
            base.OnError(e);
        }
    }
}