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
            var key_jump = Input.Check(Keys.Space);

            var Direction = Convert.ToInt32(key_right) - Convert.ToInt32(key_left);
            Velocity.X = Direction * Speed;

            Position.X += Velocity.X;
            Velocity.Y += Gravity;
            for (int i = 0; i < Utilities.objList.Count; i++) {
                if (Utilities.objList[i].ToString() == "Objects.Wall") {
                    if (CollidingTop(Utilities.objList[i])) {
                        Velocity.Y = 0;
                        if (key_jump) {
                            Velocity.Y = -7;
                        }
                    }
                }
            }
            Position.Y += Velocity.Y;
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
