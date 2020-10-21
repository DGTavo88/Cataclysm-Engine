using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Cataclysm.Graphics;
using Cataclysm.Levels;

namespace Cataclysm
{
    public class Utilities
    {
        //Main Game Instance (the one created in Program.cs)
        public static Game mainInstance;

        //Path in %localappdata% in which several information gets saved.
        public static string savepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString() + @"\CataclysmEngine";

        //Necessary for creating new content managers.
        public static IServiceProvider gameServices;

        //Main content manager (from Game1.cs)
        public static ContentManager mainManager;

        //Main Graphics Manager (from Game1.cs)
        public static GraphicsDeviceManager mainGraphicsManager;

        //Main Camera Instance
        public static Camera mainCamera;

        //Current Level
        public static Level currentLevel; 

        //List of Object2D to store the created objects so that they can be updated and drawn.
        public static List<Object2D> objList = new List<Object2D>();

        //List of fonts to store the loaded fonts.
        public static Dictionary<string, SpriteFont> fontList = new Dictionary<string, SpriteFont>();
        
        //Logs an exception.
        public static void LogException(Exception e) {
            string[] errorlog = new string[5]; //Creates array of strings for writing the log.

            //Writing the log information, you can change it to fit your needs.
            errorlog[0] = "An exception has occurred.";
            errorlog[1] = e.GetType().Name + ": " + e.Message;
            errorlog[2] = "----------------------------------------------------------------------------------------------------------------";
            errorlog[3] = e.ToString();
            errorlog[4] = "\nCataclysm Engine 1.1\n\n";

            if (!Directory.Exists(savepath)) { Directory.CreateDirectory(savepath); } //Check if the %localappdata% directory exists. If it doesn't, create one.
            string logpath = savepath + @"\ERROR_LOG.log"; //The path where the log will be stored.
            File.WriteAllLines(logpath, errorlog); //Write the log array to the a file in the "logpath".

            System.Diagnostics.Process.Start(logpath); //Open the log file.

            mainInstance.Exit(); //Exit the program.

        }

        public static void LogException(Exception e, string msg)
        {
            Console.Error.WriteLine(msg); //Shows custom error message on the console.

            string[] errorlog = new string[5]; //Creates array of strings for writing the log.

            //Writing the log information, you can change it to fit your needs.
            errorlog[0] = "An exception has occurred.\n" + msg;
            errorlog[1] = e.GetType().Name + ": " + e.Message;
            errorlog[2] = "----------------------------------------------------------------------------------------------------------------";
            errorlog[3] = e.ToString();
            errorlog[4] = "\nCataclysm Engine 1.1\n\n";

            if (!Directory.Exists(savepath)) { Directory.CreateDirectory(savepath); } //Check if the %localappdata% directory exists. If it doesn't, create one.
            string logpath = savepath + @"\ERROR_LOG.log"; //The path where the log will be stored.
            File.WriteAllLines(logpath, errorlog); //Write the log array to the a file in the "logpath".

            System.Diagnostics.Process.Start(logpath); //Open the log file.

            mainInstance.Exit(); //Exit the program.

        }

        public static float ObjectWidth(Object2D obj) {
            Vector2 objSize = obj.sprite.GetSpriteSize();
            return objSize.X * obj.sprite.Scale.X;
        }

        public static float ObjectHeight(Object2D obj) {
            Vector2 objSize = obj.sprite.GetSpriteSize();
            return objSize.Y * obj.sprite.Scale.Y;
        }

    }

    class Depth_Sorter : IComparer<Object2D> {
        public int Compare(Object2D obj1, Object2D obj2) {
            //if (obj1.Depth == 0 || obj2.Depth == 0) { return 0; }
            return obj1.Depth.CompareTo(obj2.Depth);
        }
    }
}
