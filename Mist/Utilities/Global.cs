using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Mist.Utilities
{
    static class Global
    {
        public static Game1 game;
        public static Random random = new Random();
        public static string levelName = "m_level_1";
        public static string doorNumber = "0";

        public static bool canMove = true;

        public static int partySize = 4;

        public static void Initialize(Game1 inputGame)
        {
            game = inputGame;
        }
    }
}
