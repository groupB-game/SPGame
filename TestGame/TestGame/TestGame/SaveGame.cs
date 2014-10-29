using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame
{
    /// <summary>
    /// Save high score and player name.
    /// </summary>
    public class  SaveGame
    {
        public string playerName;
        public int score;

        public SaveGame(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }

        public override string ToString()
        {
            return (playerName + "-"+score);
        }
    }
}
