using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Cataclysm
{
    public class Input
    {

        static KeyboardState oldState; //Variable used to store the old state of the keyboard.
        static KeyboardState currentState;

        //[GetState (Keyboard State)]
        //This function is used to get the current state of the keyboard.
        //This function must be called before the use of the KeyboardCheckPressed function.
        public static KeyboardState GetState() {
            oldState = currentState;
            currentState = Keyboard.GetState();
            return currentState;
        }

        //[Check (Bool)]
        //This function is used to check if the selected key has been pressed.
        public static bool KeyboardCheck(Keys selectedKey) {
            return Keyboard.GetState().IsKeyDown(selectedKey); //Check if the key is being pressed and return the value.
        }

        //[Check_Pressed (Bool)]
        //This function is used to check if the selected key has been pressed, but only activates for one frame.
        //In order to be able to use this function, GetState must be called first.
        public static bool KeyboardCheckPressed(Keys selectedKey) {
            return currentState.IsKeyDown(selectedKey) && !oldState.IsKeyDown(selectedKey);
        }
    }
}
