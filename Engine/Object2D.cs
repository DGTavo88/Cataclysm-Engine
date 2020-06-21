using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cataclysm
{
    public class Object2D
    {
        public Vector2 Position;
        public Texture2D Sprite;
        public Rectangle Hitbox;

        public virtual void Init(Vector2 pos, Texture2D texture) {
            Position = pos;
            Sprite = texture;
        }

        public virtual void Update() {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }

        protected bool CollidingLeft(Object2D obj) {
            return Hitbox.Right > obj.Hitbox.Left &&
                   Hitbox.Left < obj.Hitbox.Left &&
                   Hitbox.Bottom > obj.Hitbox.Top &&
                   Hitbox.Top < obj.Hitbox.Bottom;
        }

        protected bool CollidingRight(Object2D obj)
        {
            return Hitbox.Left < obj.Hitbox.Right &&
                   Hitbox.Right > obj.Hitbox.Right &&
                   Hitbox.Bottom > obj.Hitbox.Top &&
                   Hitbox.Top < obj.Hitbox.Bottom;
        }

        protected bool CollidingTop(Object2D obj)
        {
            return Hitbox.Bottom > obj.Hitbox.Top &&
                   Hitbox.Top < obj.Hitbox.Top &&
                   Hitbox.Right > obj.Hitbox.Left &&
                   Hitbox.Left < obj.Hitbox.Right;
        }

        protected bool CollidingBottom(Object2D obj)
        {
            return Hitbox.Top < obj.Hitbox.Bottom &&
                   Hitbox.Bottom > obj.Hitbox.Bottom &&
                   Hitbox.Right > obj.Hitbox.Left &&
                   Hitbox.Left < obj.Hitbox.Right;
        }

    }
}
