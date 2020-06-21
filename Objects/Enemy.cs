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
    class Enemy : Object2D
    {

        Vector2 pos1;
        Vector2 pos2;

        int mode = 0;
        int speed = 9;

        public override void Init(Vector2 pos, Texture2D texture)
        {
            base.Init(pos, texture);
            pos1 = new Vector2(pos.X - 128, pos.Y);
            pos2 = new Vector2(pos.X + 128, pos.Y);
        }

        public override void Update()
        {

            if (Input.Check(Keys.Enter)) {
                pos1 = new Vector2(Position.X - 128, Position.Y);
                pos2 = new Vector2(Position.X + 128, Position.Y);
            }

            if (mode == 0) {
                if (Position.X > pos1.X) {
                    Position.X -= speed;
                }
                else {
                    mode = 1;
                }
            }
            else if (mode == 1) {
                if (Position.X < pos2.X) {
                    Position.X += speed;
                }            
                else {
                    mode = 0;
                }
            }
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
