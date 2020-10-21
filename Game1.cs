using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Cataclysm;
using Cataclysm.Levels;
using Cataclysm.Graphics;
using Objects;

namespace CataclysmEngine //Make sure the namespace is the same as the one in Program.cs
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level level;
        Player player;
        Alarm plsUpdate = new Alarm(60 * 1);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
            Utilities.gameServices = Services;
            Utilities.mainManager = Content;
            Utilities.mainGraphicsManager = graphics;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            level = new Level(Utilities.savepath + @"\Ogmo Projects\TilesetTest\level01.json");
            player = new Player();
            player.contentManager = Utilities.mainManager;
            player.Init(Vector2.Zero);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Utilities.fontList.Add("Consolas", Content.Load<SpriteFont>("Fonts/Consolas"));
        }


        protected override void UnloadContent()
        {
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var FPS = (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString();
            Window.Title = "Cataclysm Engine - FPS: " + FPS;

            if (Utilities.mainCamera != null) {
                if (Input.Check(Keys.F1)) {
                    Utilities.mainCamera.Zoom -= 1f;
                }
                else if (Input.Check(Keys.F2)) {
                    Utilities.mainCamera.Zoom += 1f;
                }
            }

            player.Update();


            level.Update();

            //This loop goes through the object list and calls the Update function of all of them.
            /*
            for (int i = 0; i < Utilities.objList.Count; i++) {
                try {
                    Utilities.objList[i].Update(); //Call Update function.
                }
                catch (Exception e) {
                    Utilities.LogException(e, "[ERROR!]: Object \"" + Utilities.objList[i].ToString() + "\" has triggered an unknown exception (" + e.GetType().ToString() + ").");
                }
            }
            */
            base.Update(gameTime);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (Utilities.mainCamera != null) {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Utilities.mainCamera.Transform);
            }
            else {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            }

            player.Draw(spriteBatch);

            level.Draw(spriteBatch);

            //This loop goes through the object list and calls the Draw function on all of them.
            /*
            for (int i = 0; i < Utilities.objList.Count; i++) {
                Utilities.objList[i].Draw(spriteBatch); //Call draw function.
            }
            */
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
