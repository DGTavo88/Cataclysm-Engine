using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public Rectangle? drawRect = null;              //Rectangle that specifies which part of the 
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
            if (texture == null) { throw new ArgumentNullException("texture"); }
            Texture = texture; //Sets the texture for the sprite. (Texture2D).
            Texture.Name = texture.Name;
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
            if (FrameSize == Vector2.Zero) { return new Vector2(Texture.Width, Texture.Height); }
            return FrameSize;
        }

        public void SetAnimationParameters(int animframes, float animspeed, bool animloop) {
            AnimationFrames = animframes;
            AnimationSpeed = animspeed;
            FrameSize = new Vector2(Texture.Width / animframes, Texture.Height);
            srcRect = new Rectangle?[AnimationFrames];
            for (int i = 0; i < AnimationFrames; i++) {
                srcRect[i] = new Rectangle(i * (int)FrameSize.X, 0, (int)FrameSize.X, (int)FrameSize.Y);
                Console.WriteLine("Current Frame (Rectangle): " + i.ToString());
            }
            TimeDelay.Restart((int)AnimationSpeed);
        }

        public bool AnimationFinished() {
            return CurrentFrame == AnimationFrames;
        }

        public virtual void Draw(Vector2 Position, SpriteBatch spriteBatch) {
            try {
                if (AnimationFrames != 0 && CurrentFrame < AnimationFrames - 1 && AnimationSpeed != 0 && FrameSize != Vector2.Zero)
                {
                    if (TimeDelay.Finished())
                    {
                        CurrentFrame += 1;
                        TimeDelay.Restart((int)AnimationSpeed);
                    }
                    else
                    {
                        TimeDelay.Update();
                    }
                    spriteBatch.Draw(Texture, Position, srcRect[CurrentFrame], Blend, Rotation, Origin, Scale, FX, SortingDepth); //Draws the sprite frame. (Animation).
                }
                else if (AnimationFrames != 0 && Loopable && CurrentFrame >= AnimationFrames - 1 && AnimationSpeed != 0 && FrameSize != Vector2.Zero) {
                    CurrentFrame = 0;
                    TimeDelay.Restart((int)AnimationSpeed);
                }
                else if (AnimationFrames == 0) {
                    spriteBatch.Draw(Texture, Position, null, Blend, Rotation, Origin, Scale, FX, SortingDepth); //Draws the sprite.
                }
            }
            catch (NullReferenceException null_exception) {
                Utilities.LogException(null_exception, "NullReferenceException: Object \"" + this.ToString() + "\" has a null texture. (" + Texture.ToString() + ")");
            }
            catch (Exception e) {
                Console.Error.WriteLine("[ERROR!]: Sprite \"" + this.ToString() + "\" has triggered an unknown exception (" + e.GetType().Name + ")");
                Utilities.LogException(e);
            }
        }

    }
}
