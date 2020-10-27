using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace Cataclysm.Graphics
{
    public class Tileset
    {
        public string TilesetName;
        public string TilesetPath = "Content/Sprites/Tilesets/";
        public int TileSize = 16;

        public Texture2D TilesetTexture;
        public List<Vector2> TilesetCoordinates = new List<Vector2>();

        public Tileset(string Tileset_Name, int Tile_Size, ContentManager Manager) {
            TilesetName = Tileset_Name;
            TilesetTexture = Manager.Load<Texture2D>(TilesetPath + TilesetName);
            for (int i = 0; i <= TilesetTexture.Height; i += TileSize) {
                for (int n = 0; n <= TilesetTexture.Width; n += TileSize) {
                    TilesetCoordinates.Add(new Vector2(i, n));
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Tileset Coordinates Size: " + TilesetCoordinates.Count);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(TilesetTexture, Vector2.Zero, Color.White);
        }

    }
}
