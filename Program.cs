using Cataclysm;
using System;

namespace CataclysmEngine //Make sure the namespace is the same as in Game1.cs
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {

        public static Game1 Game = new Game1();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //using (var game = new Game1())
                //Game = game;
            try {
                Utilities.mainInstance = Game;
                Game.Run();
            }
            catch (Exception e) {
                Utilities.LogException(e);
            }
        }
    }
#endif
}
