using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CataclysmEngine;

namespace Cataclysm
{
    public class Utilities
    {
        //Path in %localappdata% in which several information gets saved.
        public static string savepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).ToString() + @"\CataclysmEngine";

        //List of Object2D to store the created objects so that they can be updated and drawn.
        public static List<Object2D> objList = new List<Object2D>();
        
        //Logs an exception.
        public static void LogException(Exception e) {
            string[] errorlog = new string[5]; //Creates array of strings for writing the log.

            //Writing the log information, you can change it to fit your needs.
            errorlog[0] = "An exception has occurred.";
            errorlog[1] = e.GetType().Name + ": " + e.Message;
            errorlog[2] = "\n------------------------------------------------------\n";
            errorlog[3] = e.ToString();
            errorlog[4] = "\nCataclysm Engine 1.1\n\n";

            if (!Directory.Exists(savepath)) { Directory.CreateDirectory(savepath); } //Check if the %localappdata% directory exists. If it doesn't, create one.
            string logpath = savepath + @"\ERROR_LOG.log"; //The path where the log will be stored.
            File.WriteAllLines(logpath, errorlog); //Write the log array to the a file in the "logpath".

            Program.Game.Exit(); //Exit the program.

        }


    }
}
