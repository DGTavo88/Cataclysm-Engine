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
    class Ilol : Object2D
    {

        Vector2 pos1;
        Vector2 pos2;

        int mode = 0;
        int speed = 2;

        public override void Init(Vector2 pos, Texture2D texture)
        {
            base.Init(pos, texture);
            pos1 = new Vector2(pos.X, pos.Y);
            pos2 = new Vector2(pos.X + 128 * 2, pos.Y + 128 * 2);
        }

        public override void Update()
        {
            if (mode == 0) {
                if (Position.X < pos2.X) { Position.X += speed; }
                if (Position.Y < pos2.Y) { Position.Y += speed; }
                if (Position.X == pos2.X && Position.Y == pos2.Y) { mode = 1; }
            }
            else if (mode == 1) {
                if (Position.X > pos1.X) { Position.X -= speed; }
                if (Position.Y > pos1.Y) { Position.Y -= speed; }
                if (Position.X == pos1.X && Position.Y == pos1.Y) { mode = 0; }
            }
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
