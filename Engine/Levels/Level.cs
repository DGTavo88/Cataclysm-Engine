using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Cataclysm.Graphics;

namespace Cataclysm.Levels
{
    public class Level
    {
        int Width;
        int Height;
        int OffsetX;
        int OffsetY;

        public dynamic[] Layers;
        public int[] TileData;
        public List<Tile> TilesList = new List<Tile>();
        public Tileset LevelTileset;

        public List<Object2D> ObjectsList = new List<Object2D>();

        string Path = "";
        Dictionary<string, dynamic> LevelData = new Dictionary<string, dynamic>();

        public bool LevelLoaded = false;

        public ContentManager contentManager = new ContentManager(Utilities.gameServices);


        public Level(string path) {
            Path = path;
            Utilities.currentLevel = this;
            LoadLevel();
        }

       void LoadLevel() {
            string JsonString = File.ReadAllText(Path);
            LevelData = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonString);

            Width = (int)LevelData["width"];
            Height = (int)LevelData["height"];
            OffsetX = (int)LevelData["offsetX"];
            OffsetY = (int)LevelData["offsetY"];

            Layers = LevelData["layers"].ToObject<object[]>();

            LoadLayers();
        }

        void LoadLayers() {
            for (int i = 0; i < 1/*Layers.Length*/; i++) {
                var currentLayer = Layers[i];
                switch(currentLayer["name"]) {
                    default:
                        Console.WriteLine("Level Tileset Case Statement (Line: 61)");
                        TileData = currentLayer["data"].ToObject<int[]>();
                        float gridCellWidth = currentLayer["gridCellWidth"];
                        float gridCellHeight = currentLayer["gridCellHeight"];
                        float gridCellsX = currentLayer["gridCellsX"];
                        float gridCellsY = currentLayer["gridCellsY"];
                        Console.WriteLine(gridCellWidth.GetType());
                        Vector2 gridDimensions = new Vector2(gridCellWidth, gridCellHeight);
                        Vector2 gridSize = new Vector2(gridCellsX, gridCellsY);
                        LoadTileset(currentLayer["tileset"].ToObject<string>(), gridDimensions, gridSize);
                        break;
                }
                Console.WriteLine(currentLayer["name"]);
            }
        }

        void LoadTileset(string tileset, Vector2 celldimensions, Vector2 cellnumber) {
            LevelTileset = new Tileset(tileset, 16, contentManager);
            int i = 0; 
            for (int tiley = 0; tiley < cellnumber.Y; tiley++) {
                for (int tilex = 0; tilex < cellnumber.X; tilex++) {
                    if (TileData[i] != -1) {
                        Console.WriteLine("Current Tile Data ID: " + TileData[i].ToString());
                        Vector2 currentPos = new Vector2(tilex * celldimensions.X, tiley * celldimensions.Y);
                        Tile currentTile = new Tile(LevelTileset, currentPos, TileData[i], celldimensions);
                        TilesList.Add(currentTile);
                    }
                    i++;
                }
            }
        }

        public void Update() {
            for (int i = 0; i < ObjectsList.Count; i++) {
                ObjectsList[i].Update();
            }

            if (Input.KeyboardCheckPressed(Keys.F5)){
                Console.WriteLine("Reload Level.");
                DestroyObjects();
                UnloadContent();
                LoadLevel();
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < TilesList.Count; i++) {
                TilesList[i].Draw(spriteBatch);
            }

            for (int i = 0; i < ObjectsList.Count; i++) {
                ObjectsList[i].Draw(spriteBatch);
            }
        }

        public void UnloadContent() {
            contentManager.Unload();
        }

        public void DestroyObjects() {
            for (var i = 0; i < TilesList.Count; i++) {
                TilesList[i] = null;
            }
            for (var i = 0; i < ObjectsList.Count; i++) {
                ObjectsList[i] = null;
            }
            TilesList.Clear();
            ObjectsList.Clear();
        }

    }
}
