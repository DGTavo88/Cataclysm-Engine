using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cataclysm
{
    public class Alarm
    {
        //[Alarm Class]
        //The alarm class alows you to create an alarm that can be used to delay the trigger of different events.
        //Example:
        //  Alarm alarm = new Alarm(60 * 2) //initializing an alarm and setting it to trigger on 120 frames.
        //  if (alarm.Finished()) //Check if the alarm has finished counting down.
        //  {
        //      DoSomething(); //Do some action.
        //  }

        //[Variables]
        public int AlarmTime = -1; //The alarm's current time.

        //[Alarm (Constructor)]
        //Allows you to initialize the alarm when created.
        //Requires an integer, from which the alarm will start counting down from.
        public Alarm (int time) {
            AlarmTime = time;
        }

        //[Update (Void)]
        //This function updates the alarm's time.
        public void Update() {
            if (AlarmTime > -1) { AlarmTime -= 1; }
        }

        //[Finished (Bool)]
        //This function checks if the alarm has finished counting.
        //If it has, it returns true.
        //If it hasn't, it returns false.
        public bool Finished() {
            return AlarmTime == -1;
        }

        //[Restart (void)]
        //Restarts the alarm without having to create one.
        //This allows the multiple usage of a single alarm.
        //Requires an integer.
        public void Restart(int time) {
            AlarmTime = time;
        }
    }
}
