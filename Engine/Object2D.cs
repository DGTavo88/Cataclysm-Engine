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
        //[Object2D Class]
        //This class contains the standard functionalities for game objects.
        
        //You can use this class to create your own custom objects. All you need to do is inherit it.
        //EXAMPLE:
        //  class MyObject : Object2D

        //[Variables]
        public Vector2 Position;                      //Position of the current object on the screen.
        public Vector2 Velocity;                      //Velocity of the current object on the screen. Can be used to move the object.

        public Texture2D Sprite;                      //The sprite of the object.
        public Color Blend = Color.White;             //The color of the sprite. Default: Color.White.
        public float Rotation = 0;                    //Rotation of the sprite. Default: 0
        public Vector2 Origin = new Vector2(0, 0);    //The origin point of the sprite. Default Coordinates: x: 0, y: 0
        public Vector2 Scale = new Vector2(1, 1);     //The scale of the sprite. Default: x: 1, y: 1
        public SpriteEffects FX = SpriteEffects.None; //I still have no idea what this is. Default: SpriteEffects.None
        public float Depth = 0;                       //Sorting depth of the sprite, between 0 (front) and 1 (back). Default: 0.

        public Rectangle Hitbox;                      //Rectangle used as a hitbox for the object. Gets updated on the Update function.

        //[Init (Void)]
        //This function is used to initialize your object.
        //It requires two parameters: A Vector2 and a Texture2D.
        //All objects must be initialized, preferably after creation.
        //This function can be overrided.
        public virtual void Init(Vector2 pos, Texture2D texture) {
            Position = pos;     //Assigns the Position of the object (Vector2).
            Sprite = texture;   //Assigns the Sprite of the object (Texture2D).
        }

        //[Update (Void)]
        //This function, as the name implies, gets updated every frame.
        //You can use this function to write all of the behaviour of your object.
        //This function can be overrided.
        public virtual void Update() {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width * (int)Scale.X, Sprite.Height * (int)Scale.Y); //Updates the position of the hitbox.
        }

        //[Draw (Void)]
        //This function, as the name implies, draws the sprite of the object according to the parameters previously assigned.
        //It also contains some error handling, which you can read on the .log file on the Local Appdata folder (default: %localappdata%\CataclysmEngine).
        //This function can be overrided.
        public virtual void Draw(SpriteBatch spriteBatch) {
            try {
                spriteBatch.Draw(Sprite, Position, null, Blend, Rotation, Origin, Scale, FX, Depth); //Draws the sprite.
            }
            catch(NullReferenceException null_exception) {
                Console.Error.WriteLine("[NullReferenceException]: Object \"" + this.ToString() + "\" has a null texture.");
                Utilities.LogException(null_exception);
            }
            catch(Exception e) {
                Console.Error.WriteLine("[ERROR!]: Object \"" + this.ToString() + "\" has triggered an unknown exception (" + e.GetType().Name + ")");
                Utilities.LogException(e);
            }
        }

        //[Collision Functions (Void)]
        //All of these functions are used for collision handling.
        //They all require a Object2D object to be used.
        //They can all be overrided.

        //[Colliding (Void)]
        //This function checks if the object is colliding with the desired object.
        //This function activates from any side.
        protected virtual bool Colliding(Object2D obj) {
            return Hitbox.Intersects(obj.Hitbox);
        }

        //[CollidingLeft (Void)]
        //This function checks if the object is colliding with the left side of the desired object.
        protected virtual bool CollidingLeft(Object2D obj) {
            return Hitbox.Right + Velocity.X > obj.Hitbox.Left &&
                   Hitbox.Left < obj.Hitbox.Left &&
                   Hitbox.Bottom > obj.Hitbox.Top &&
                   Hitbox.Top < obj.Hitbox.Bottom;
        }

        //[CollidingRight (Void)]
        //This function checks if the object is colliding with the right side of the desired object.
        protected virtual bool CollidingRight(Object2D obj)
        {
            return Hitbox.Left + Velocity.X < obj.Hitbox.Right &&
                   Hitbox.Right > obj.Hitbox.Right &&
                   Hitbox.Bottom > obj.Hitbox.Top &&
                   Hitbox.Top < obj.Hitbox.Bottom;
        }

        //[CollidingTop (Void)]
        //This function checks if the object is colliding with the top side of the desired object.
        protected virtual bool CollidingTop(Object2D obj)
        {
            return Hitbox.Bottom + Velocity.Y > obj.Hitbox.Top &&
                   Hitbox.Top < obj.Hitbox.Top &&
                   Hitbox.Right > obj.Hitbox.Left &&
                   Hitbox.Left < obj.Hitbox.Right;
        }

        //[CollidingBottom (Void)]
        //This function checks if the object is colliding with the bottom side of the desired object.
        protected virtual bool CollidingBottom(Object2D obj)
        {
            return Hitbox.Top + Velocity.Y < obj.Hitbox.Bottom &&
                   Hitbox.Bottom > obj.Hitbox.Bottom &&
                   Hitbox.Right > obj.Hitbox.Left &&
                   Hitbox.Left < obj.Hitbox.Right;
        }

    }
}
