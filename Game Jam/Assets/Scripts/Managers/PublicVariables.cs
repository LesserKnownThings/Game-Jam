using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesserKnown.Public
{
    /// <summary>
    /// This class holds alld the public static variables
    /// For now only the health
    /// </summary>
    public static class PublicVariables
    {
        public static int health = 10;
        public static int COINS;
        public static int APPLES;
        public static bool IS_FUSIONED;
        public static int MAX_COLLECTIBLES = 10;

        public static bool Lose_Health(int amount)
        {
            health -= amount;

            if (health == 0)
                return true;

            return false;
        }

        public static void Reset_Level(int amount)
        {
            health = amount;
        }
    }
}
