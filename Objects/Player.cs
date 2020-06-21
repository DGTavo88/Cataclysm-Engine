using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Cataclysm;

namespace Objects
{
    class Player : Object2D
    {
        int hsp;
        float vsp;
        int Speed = 6;
        float Gravity = 0.3f;

        public override void Init(Vector2 pos, Texture2D texture)
        {
            base.Init(pos, texture);
        }

        public override void Update()
        {
            var key_left = Input.Check(Keys.Left);
            var key_right = Input.Check(Keys.Right);

            var move = Convert.ToInt32(key_right) - Convert.ToInt32(key_left);
            hsp = move * Speed;

            Position.X += hsp;
            //Position.Y += vsp;
            /*for (int i = 0; i < Utilities.objList.Count; i++) {
                if (Utilities.objList[i].ToString() == "Objects.Enemy") {
                    if (Colliding(Utilities.objList[i])) {
                        vsp += Gravity;
                    }
                    else {
                        vsp = 0;
                    }
                }
            }
            Position.Y += vsp; */
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
