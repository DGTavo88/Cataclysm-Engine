using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cataclysm
{
    class Input
    {

        static KeyboardState oldState = Keyboard.GetState(); //Variable used to store the old state of the keyboard.

        //[Check (Bool)]
        //This function is used to check if the selected key has been pressed.
        public static bool Check(Keys selectedKey) {
            var currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(selectedKey)) {
                return true;
            }
            return false;
        }

        //[Check_Pressed (Bool)]
        //This function is used to check if the selected key has been pressed, but only activates for one frame.
        //CURRENTLY NOT WORKING. I NEED TO FIX IT.
        public static bool Check_Pressed(Keys selectedKey) {
            var currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(selectedKey) && oldState.IsKeyUp(selectedKey)) {
                oldState = currentState;
                return true;
            }
            oldState = currentState;
            return false;
        }
    }
}
