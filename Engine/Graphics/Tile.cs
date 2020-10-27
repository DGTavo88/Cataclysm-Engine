using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cataclysm.Graphics
{
    public class Tile
    {
        public Texture2D Texture;                       //The texture of the tile.
        public Vector2 Position = new Vector2(0, 0);    //Position of the tile. Default: (0, 0)
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, srcRect, Blend, Rotation, Origin, Scale, FX, SortingDepth);
            Color tcolor = Color.Black;
        }

    }
}
