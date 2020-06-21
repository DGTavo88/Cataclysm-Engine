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

        static KeyboardState oldState = Keyboard.GetState();

        public static bool Check(Keys selectedKey) {
            var currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(selectedKey)) {
                return true;
            }
            return false;
        }

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
