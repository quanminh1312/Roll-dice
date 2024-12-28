using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    public static class Helper
    {
        public static List<int> playerIndex = new List<int>();
        public static List<string> playerNames = new List<string>();
        public static void ClearPlayers()
        {
            playerIndex.Clear();
            playerNames.Clear();
        }
        public static void AddPlayer(int index, string name)
        {
            playerIndex.Add(index);
            //get only 7 characters
            playerNames.Add(name.Length > 8 ? name.Substring(0, 8) : name);
        }
    }
}
