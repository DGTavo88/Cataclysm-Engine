using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Cataclysm;
using Objects;

namespace CataclysmEngine
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Player player = new Player();
            player.Init(new Vector2(88, 88), Content.Load<Texture2D>("Sprites/Placeholders/spr_test32"));

            Enemy enemy = new Enemy();
            enemy.Init(new Vector2(88 * 4, 88), Content.Load<Texture2D>("Sprites/Placeholders/spr_enemy"));

            Wall wall = new Wall();
            ilol.Init(new Vector2(88, 88 * 4), Content.Load<Texture2D>("Sprites/Placeholders/spr_wall"));

            Utilities.objList.Add(player);
            Utilities.objList.Add(enemy);
            Utilities.objList.Add(ilol);
            Utilities.objList.Add(wall);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }


        protected override void UnloadContent()
        {
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //This loop goes through the object list and calls the Update function of all of them.
            for (int i = 0; i < Utilities.objList.Count; i++) {
                try {
                    Utilities.objList[i].Update(); //Call Update function.
                }
                catch (Exception e) {
                    Console.Error.WriteLine("[ERROR!]: Object \"" + Utilities.objList[i].ToString() + "\" has triggered an unknown exception (" + e.GetType().ToString() + ").");
                    Utilities.LogException(e);
                }
            }
            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //This loop goes through the object list and calls the Draw function on all of them.
            for (int i = 0; i < Utilities.objList.Count; i++) {
                Utilities.objList[i].Draw(spriteBatch); //Call draw function.
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
