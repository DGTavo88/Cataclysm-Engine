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

            Utilities.objList.Add(player);
            Utilities.objList.Add(enemy);
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

            for (int i = 0; i < Utilities.objList.Count; i++) {
                try {
                    Utilities.objList[i].Update();
                }
                catch (Exception e) {
                    Console.Error.WriteLine("[" + e.ToString() + "]: Object " + Utilities.objList[i].ToString() + " doesn't have an Update function.");
                    continue;
                }
            }

            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int i = 0; i < Utilities.objList.Count; i++) {
                Utilities.objList[i].Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
