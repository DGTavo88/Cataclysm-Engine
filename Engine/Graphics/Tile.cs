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
    public class Tile : Object2D
    {
        public Texture2D Texture;                       //The texture of the tile.
        public Rectangle? srcRect = null;               //Rectangles that specify which part of the texture to draw.
        public Color Blend = Color.White;               //The color of the tile. Default: Color.White.
        public float Rotation = 0;                      //Rotation of the tile. Default: 0
        public Vector2 Origin = new Vector2(0, 0);      //The origin point of the tile. Default Coordinates: x: 0, y: 0
        public Vector2 Scale = new Vector2(1, 1);       //The scale of the tile. Default: x: 1, y: 1
        public SpriteEffects FX = SpriteEffects.None;   //I still have no idea what this is. Default: SpriteEffects.None
        public float SortingDepth = 0;                  //Sorting depth of the sprite, between 0 (front) and 1 (back). Default: 0.

        public int TileID;

        public Tile(Tileset tileset, Vector2 position, int tileid, Vector2 tiledimensions) {
            Position = position;
            Texture = tileset.TilesetTexture;
            TileID = tileid;
            srcRect = new Rectangle((int)(tileset.TilesetCoordinates[tileid].X * (int)Scale.X), (int)tileset.TilesetCoordinates[tileid].Y * (int)Scale.Y, (int)(tiledimensions.X), (int)(tiledimensions.Y));
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width * (int)Scale.X, (int)Texture.Height * (int)Scale.Y); //Updates the position of the hitbox.
        }

        public override void Update()
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, (int)Texture.Width * (int)Scale.X, (int)Texture.Height * (int)Scale.Y); //Updates the position of the hitbox.
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Console.WriteLine("Tile Draw Function");
            spriteBatch.Draw(Texture, Position, srcRect, Blend, Rotation, Origin, Scale, FX, SortingDepth);
            Color tcolor = Color.Black;
            //spriteBatch.DrawString(Utilities.fontList["Consolas"], TileID.ToString(), new Vector2(Position.X, Position.Y), tcolor);
        }

    }
}
