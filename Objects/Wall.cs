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
    class Wall : Object2D
    {

        bool isClimb = true;
        Random random = new Random();
        public override void Init(Vector2 pos, Texture2D texture)
        {
            base.Init(pos, texture);
            sprite.Scale = new Vector2(12, 8);
        }

        public override void Update()
        {
            //Scale = new Vector2(random.Next(1, 12), random.Next(1, 8));
            var r = random.Next(0, 255);
            var g = random.Next(0, 255);
            var b = random.Next(0, 255);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
