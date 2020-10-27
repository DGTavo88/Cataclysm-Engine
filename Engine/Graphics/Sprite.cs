using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Cataclysm.Graphics
{
    public class Sprite
    {
        //[Sprite Class]
        //This class is used as a more advanced version of the Texture2D class.
        //Allows for the modification of sprites, all together in one package.

        //It also contains support for animated sprites.
        //For the animated sprites to work, you need to set the following variables:
        //  - AnimationFrames (int).
        //  - AnimationSpeed (float).
        //  - FrameSize (Vector2).

        //More information about these variables can be found on the Variables section.


        public Texture2D Texture;                       //The texture of the sprite.
        public Rectangle? drawRect = null;              //Rectangle that specifies which part of the sprite to draw. Only used with single frames. Default: Null.
        public Rectangle?[] srcRect = null;             //Rectangles that specify which part of the sprite to draw. Can be used for animations. Default: Null.
        public Color Blend = Color.White;               //The color of the sprite. Default: Color.White.
        public float Rotation = 0;                      //Rotation of the sprite. Default: 0
        public Vector2 Origin = new Vector2(0, 0);      //The origin point of the sprite. Default Coordinates: x: 0, y: 0
        public Vector2 Scale = new Vector2(1, 1);       //The scale of the sprite. Default: x: 1, y: 1
        public SpriteEffects FX = SpriteEffects.None;   //I still have no idea what this is. Default: SpriteEffects.None
        public float SortingDepth = 0;                  //Sorting depth of the sprite, between 0 (front) and 1 (back). Default: 0.

        public int AnimationFrames = 0;                 //Number of frames in the animation. Default: 0 (= No animation).
        public static float AnimationSpeed = 0;         //Speed of the animation. Default: 0 (= No animation).
        public int CurrentFrame = 0;                    //Current animation frame. Default: 0.
        public Vector2 FrameSize = new Vector2(0, 0);   //The size of each animation frame. Default: 0 (= No animation).
        public bool Loopable = false;                   //Set to true if the sprite must be looped. Default: false (= Don't loop).
        public Alarm TimeDelay = new Alarm(0);          //Alarm that is used together with the animation speed. Default: 0 (The Alarm won't start without valid animation parameters, for that check the SetAnimationParameters function)

        //[]
        public Sprite(Texture2D texture) {
            if (texture == null) { throw new ArgumentNullException("texture"); }  //If the texture given is null, throws an ArgumentNullException.
            Texture = texture;              //Sets the texture for the sprite. (Texture2D).
            Texture.Name = texture.Name;    //Sets the name for the sprite. (Texture2D).
        }

        public Sprite(string textpath, ContentManager sprmanager) {
            if (sprmanager == null) { throw new ArgumentNullException("sprmanager"); }  
            if (textpath == null) { throw new ArgumentNullException("textpath"); }
            else if (textpath == string.Empty) { throw new ArgumentException("Texture Path \"textpath\" is empty."); }
            Texture = sprmanager.Load<Texture2D>(textpath); //Load the texture.
            Texture.Name = textpath;
        }

        public Sprite(Texture2D texture, Rectangle?[] srcrect, Color blend, float rotation, Vector2 origin, Vector2 scale, SpriteEffects sprfx, float srtdepth) {
            Texture = texture;          //Set the texture for the sprite. (Texture2D).
            srcRect = srcrect;          //Sets a rectangle. Is Nullable. (Rectangle / null).
            Blend = blend;              //Sets the color of the sprite. (Color).
            Rotation = rotation;        //Sets the sprite's rotation. (float).
            Origin = origin;            //Sets the origin for the sprite. (Vector2).
            Scale = scale;              //Scales the sprite. (Vector2).
            FX = sprfx;                 //Sets the sprite effects. (SpriteEffects).
            SortingDepth = srtdepth;    //Sets the sorting depth. (float).
        }

        public Vector2 GetSpriteSize() {
            //Checks if the FrameSize is (0, 0), if it is, it returns the Texture's Width and Height as a Vector2.
            if (FrameSize == Vector2.Zero) { return new Vector2(Texture.Width, Texture.Height); }
            return FrameSize; //Returns the Vector2 FrameSize.
        }

        public void SetAnimationParameters(int animframes, float animspeed, bool animloop) {
            AnimationFrames = animframes;                                        //Sets the AnimationFrames variable to animframes.
            AnimationSpeed = animspeed;                                          //Sets the AnimationSpeed variable to animspeed.
            FrameSize = new Vector2(Texture.Width / animframes, Texture.Height); //Sets the size of the animation frame to the width of the texture divided by the number of animations, and the height of the texture.
            srcRect = new Rectangle?[AnimationFrames];                           //Creates a nullable rectangle array with a size equal to the number of animation frames.
            for (int i = 0; i < AnimationFrames; i++) {
                srcRect[i] = new Rectangle(i * (int)FrameSize.X, 0, (int)FrameSize.X, (int)FrameSize.Y); //Sets the current rectangle in the array to a new rectangle in the position and with the directions of the frame.
                Console.WriteLine("Current Frame (Rectangle): " + i.ToString()); //Logs the current frame number to the console. This is just for debugging purposes.
            }
            TimeDelay.Restart((int)AnimationSpeed); //Restarts the timer to the speed of the animation.
        }

        public bool AnimationFinished() {
            return CurrentFrame == AnimationFrames - 1; //Returns true or false depending on whether the current animation frame equals the number of animation frames - 1, meaning it reached the end.
        }

        public virtual void Draw(Vector2 Position, SpriteBatch spriteBatch) {
            try {
                //If the number of animation frames is equal to 0.
                //If the current frame is smaller than the number of animation frames - 1.
                //If the animation speed is not equal 0.
                //And if the size of the frame isn't (0, 0).
                if (AnimationFrames != 0 && CurrentFrame < AnimationFrames - 1 && AnimationSpeed != 0 && FrameSize != Vector2.Zero)
                {
                    //If the animation timer finished.
                    if (TimeDelay.Finished())
                    {
                        CurrentFrame += 1;                      //Moves to the next frame.
                        TimeDelay.Restart((int)AnimationSpeed); //Restarts the timer.
                    }
                    else
                    {
                        TimeDelay.Update();                     //Updates the timer.
                    }
                    spriteBatch.Draw(Texture, Position, srcRect[CurrentFrame], Blend, Rotation, Origin, Scale, FX, SortingDepth); //Draws the sprite frame. (Animation).
                }
                else if (AnimationFrames != 0 && Loopable && CurrentFrame >= AnimationFrames - 1 && AnimationSpeed != 0 && FrameSize != Vector2.Zero) {
                    CurrentFrame = 0;                           //Sets the current frame to 0.
                    TimeDelay.Restart((int)AnimationSpeed);     //Restarts the timer.
                }
                else if (AnimationFrames == 0) {
                    spriteBatch.Draw(Texture, Position, null, Blend, Rotation, Origin, Scale, FX, SortingDepth); //Draws the sprite.
                }
            }
            catch (NullReferenceException null_exception) {
                //Logs the exception.
                Utilities.LogException(null_exception, "NullReferenceException: Object \"" + this.ToString() + "\" has a null texture. (" + Texture.ToString() + ")");
            }
            catch (Exception e) {
                //Logs the exception.
                //TO DO: Update with newer version.
                Console.Error.WriteLine("[ERROR!]: Sprite \"" + this.ToString() + "\" has triggered an unknown exception (" + e.GetType().Name + ")");
                Utilities.LogException(e);
            }
        }

    }
}
