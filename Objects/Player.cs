using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Cataclysm;
using Cataclysm.Graphics;
using Cataclysm.Levels;

namespace Objects
{
    class Player : Object2D
    {
        int Speed = 2;
        int Climb_Speed = 2;
        float Jump_Height = -5f;
        float Gravity = 0.2f;
        static int Stamina = 60 * 5;
        Alarm Stamina_Counter = new Alarm(Stamina);

        Dictionary<string, Sprite> spriteList = new Dictionary<string, Sprite>();

        Camera camera = new Camera();

        public override void Init(Vector2 pos)
        {
            sprite = new Sprite("Sprites/Placeholders/spr_test32", contentManager);
            sprite.Scale = new Vector2(1f, 1f);
            Depth = 5;
            Console.WriteLine("Depth: " + Depth.ToString());
            //sprite.SetAnimationParameters(13, new Vector2(24, 39), 5, true);
            base.Init(pos);
            Console.WriteLine("Sprite: " + sprite.ToString());
            Console.WriteLine("Position: " + Position.ToString());
        }
        public override void Init(Vector2 pos, Texture2D texture)
        {
            base.Init(pos, texture);
        }

        public override void Update()
        {
            var key_up = Input.Check(Keys.Up);
            var key_down = Input.Check(Keys.Down);
            var key_left = Input.Check(Keys.Left);
            var key_right = Input.Check(Keys.Right);
            var key_jump = Input.Check(Keys.Space);
            var key_run = Input.Check(Keys.LeftShift);
            var key_climb = Input.Check(Keys.Z);
            var key_dash = Input.Check_Pressed(Keys.X);

            var Direction = Convert.ToInt32(key_right) - Convert.ToInt32(key_left);
            int DesiredSpeed = 0;

            Speed = (key_run) ? 4 : 2;

            //Console.WriteLine("Speed: " + Speed.ToString());

            Velocity.X = Direction * Speed;
            Velocity.Y += Gravity;
            for (int i = 0; i < Utilities.objList.Count; i++) {
                if (Utilities.objList[i].Solid && Utilities.objList[i].Climbable) {
                    if (CollidingLeft(Utilities.objList[i]) || CollidingRight(Utilities.objList[i]))
                    {
                        Velocity.X = 0;
                        if (key_climb && !Stamina_Counter.Finished()) {
                            Velocity.Y = 0;
                            Stamina_Counter.Update();
                            if (key_up)
                            {
                                Velocity.Y = -Climb_Speed;
                                for (int n = 0; n < Utilities.objList.Count; n++)
                                {
                                    if (CollidingBottom(Utilities.objList[n]))
                                    {
                                        Console.WriteLine("Colliding with bottom when climbing");
                                        Velocity.Y = 0;
                                        break;
                                    }
                                }
                            }
                            else if (key_down)
                            {
                                Velocity.Y = Climb_Speed;
                                for (int n = 0; n < Utilities.objList.Count; n++)
                                {
                                    if (CollidingTop(Utilities.objList[n]))
                                    {
                                        Console.WriteLine("Colliding with top when climbing");
                                        Velocity.Y = 0;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (CollidingTop(Utilities.objList[i])) {
                        Velocity.Y = 0;
                        Stamina_Counter.Restart(Stamina);
                        if (key_jump) {
                            Velocity.Y = Jump_Height;
                        }
                    }
                }
            }
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            camera.Update(Position);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color tcolor = Color.Black;
            spriteBatch.DrawString(Utilities.fontList["Consolas"], "Position.X: " + Position.X.ToString(), new Vector2(Position.X, Position.Y - 128 - 96), tcolor);
            spriteBatch.DrawString(Utilities.fontList["Consolas"], "Position.Y: " + Position.Y.ToString(), new Vector2(Position.X, Position.Y - 128 - 64), tcolor);
            spriteBatch.DrawString(Utilities.fontList["Consolas"], "Velocity.X: " + Velocity.X.ToString(), new Vector2(Position.X, Position.Y - 128 - 32), tcolor);
            spriteBatch.DrawString(Utilities.fontList["Consolas"], "Velocity.Y: " + Velocity.Y.ToString(), new Vector2(Position.X, Position.Y - 96), tcolor);
            spriteBatch.DrawString(Utilities.fontList["Consolas"], "Stamina Counter: " + Stamina_Counter.AlarmTime.ToString(), new Vector2(Position.X, Position.Y - 64), tcolor);
            spriteBatch.DrawString(Utilities.fontList["Consolas"], "Current Frame: " + sprite.CurrentFrame.ToString(), new Vector2(Position.X, Position.Y), tcolor);
            base.Draw(spriteBatch);
        }
    }
}
