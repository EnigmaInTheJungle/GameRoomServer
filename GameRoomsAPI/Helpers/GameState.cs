using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameRoomsAPI.Helpers
{
    public class GameState
    {
        static public List<int[]> winnerIndexes = new List<int[]> { new int[] { 0,1,2}, new int[] { 3, 4, 5 }, new int[] { 6, 7, 8 },
                                                            new int[] { 0,3,6},new int[] { 1,4,7},new int[] { 2,5,8},
                                                            new int[] { 0,4,8},new int[] { 2,4,6}
        };
        static public Dictionary<int,GameStateLocal> gameState = new Dictionary<int, GameStateLocal>();
    }

    public class GameStateLocal
    {
        public string[] players = new string[] { "red", "blue" };
        public int whosNext = 0;
        public int numPlayers = 0;
        public bool winnerPresent = false;
        public int[] board = new int[] {
                0, 0, 0,
                0, 0, 0,
                0, 0, 0
                };
    }  
}