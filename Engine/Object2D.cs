using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Cataclysm.Graphics;

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

        public Sprite sprite;                         //The sprite of the object.
        public int Depth = 0;                         //Depth of the object. The bigger the number, the closer to the screen.

        public bool Solid = true;
        public bool Climbable = true;
        public bool Damaging = false;

        public Rectangle Hitbox;                      //Rectangle used as a hitbox for the object. Gets updated on the Update function.

        //Content Manager that can be used to Load, Unload and manage the content of an object.
        public ContentManager contentManager;

        #region Standard Functions
        //[Init (Void)]
        //This function is used to initialize your object.
        //It requires two parameters: A Vector2 and a Texture2D.
        //All objects must be initialized, preferably after creation.
        //This function can be overrided.
        public virtual void Init(Vector2 pos, Texture2D texture) {
            Position = pos;                                                      //Assigns the Position of the object (Vector2).
            sprite = new Sprite(texture);                                        //Creates a Sprite object using the texture (Texture).
            Utilities.objList.Add(this);                                         //Adds the object to the object list (check the Update and Draw functions in Game1.cs).
            Utilities.objList.Sort(new Depth_Sorter());                          //Sorts the list by the objects' depth.
        }

        //[Init (Void) (Overload)]
        //This function is used to initialize your object.
        //This is an overload from the previous Init. Allows you to only initialize the position of an object.
        
        //This is useful if your object has a set texture.
        //Example:
        //  public Sprite sprite = contentManager.Load<Texture2D>("MyTexture");  
        
        //It requires one parameters: A Vector2.
        //All objects must be initialized, preferably after creation.
        //This function can be overrided.
        public virtual void Init(Vector2 pos) {
            Position = pos;
            Utilities.objList.Add(this);
            Utilities.objList.Sort(new Depth_Sorter());
        }

        //[Update (Void)]
        //This function, as the name implies, gets updated every frame.
        //You can use this function to write all of the behaviour of your object.
        //This function can be overrided.
        public virtual void Update() {
            Vector2 SpriteSize = sprite.GetSpriteSize();
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)SpriteSize.X * (int)sprite.Scale.X, (int)SpriteSize.Y * (int)sprite.Scale.Y); //Updates the position of the hitbox.
        }

        //[Draw (Void)]
        //This function, as the name implies, draws the sprite of the object according to the parameters previously assigned.
        //It also contains some error handling, which you can read on the .log file on the Local Appdata folder (default: %localappdata%\CataclysmEngine).
        //This function can be overrided.
        public virtual void Draw(SpriteBatch spriteBatch) {
            try {
                //spriteBatch.Draw(hitboxTexture, Hitbox, Color.White);
                sprite.Draw(Position, spriteBatch);
            }
            catch(NullReferenceException null_exception) {
                Utilities.LogException(null_exception, "NullReferenceException: Object \"" + this.ToString() + "\" has a null texture. (" + sprite.Texture.ToString() + ")");
            }
            catch(Exception e) {
                Console.Error.WriteLine("[ERROR!]: Object \"" + this.ToString() + "\" has triggered an unknown exception (" + e.GetType().Name + ")");
                Utilities.LogException(e);
            }
        }
        #endregion

        #region Collision Functions
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
        #endregion

    }
}
