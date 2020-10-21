using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace Cataclysm.Graphics
{
    public class Tileset
    {
        public string TilesetName;
        public string TilesetPath = "Content/Sprites/Tilesets/";
        public string TilesetMetaPath = "Content/Metadata/";

        public Texture2D TilesetTexture;
        public List<Vector2> TilesetCoordinates = new List<Vector2>();

        public Tileset(string tilesetname, ContentManager Manager) {
            TilesetName = tilesetname;
            TilesetTexture = Manager.Load<Texture2D>(TilesetPath + TilesetName);
            var TilesetJsonString = File.ReadAllText(TilesetMetaPath + TilesetName + ".json");
            var preData = JsonConvert.DeserializeObject<List<List<int>>>(TilesetJsonString);
            for (var i = 0; i < preData.Count; i++) {
                Vector2 currentData = new Vector2(preData[i][0], preData[i][1]);
                Console.WriteLine(currentData);
                TilesetCoordinates.Add(currentData);
            }
            Console.WriteLine("Tileset Coordinates Size: " + TilesetCoordinates.Count);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(TilesetTexture, Vector2.Zero, Color.White);
        }

    }
}
