using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Platformer.GameContent
{
    class Map
    {
        private Tile[] tiles;
        private Vector2 winSize;
        private Vector2 winTiles;
        public int tileSize = 20;

        public Map(Vector2 windowBounds)
        {
            this.winSize = windowBounds;
            this.SizeInit();
        }

        private void SizeInit()
        {
            winTiles.X = (winSize.X/tileSize);
            winTiles.Y = (winSize.Y / tileSize);
            int tilesSize = (int)(winTiles.X * winTiles.Y);

            tiles = new Tile[tilesSize];
            

            for (int row = 0; row < winTiles.Y; row++)
            {
                for (int col = 0; col < winTiles.X; col++)
                {
                    int index = (int)winTiles.X * row + col;
                    tiles[index] = new Tile();
                    tiles[index].position = new Vector2(col * tileSize, row * tileSize);
                    tiles[index].type = TileTypes.NONE;
                    tiles[index].size = new Vector2(tileSize, tileSize);
                }
            }

        }

        public Tile GetTile(Vector2 position)
        {
            int row = (int)position.X / tileSize;
            int col = (int)position.Y / tileSize;

            int index = (int)winTiles.X * col + row;
            if (index > -1 && index < tiles.Length)
            {
                return tiles[index];
            }

            Tile tile = new Tile();
            tile.position = new Vector2(-1, -1);
            return tile;
        }

        public void SetTile(Vector2 position,Tile tile)
        {
            int row = (int)position.X / tileSize;
            int col = (int)position.Y / tileSize;

            int index = (int)winTiles.X * col + row;
            if (index > -1 && index < tiles.Length)
            {
                tiles[index].type = tile.type;
            }
        }

        public void ReadMap(string path)
        {
            if(File.Exists(path))
            {
                string content = File.ReadAllText(path);
                string[] strTiles = content.Split(',');
                for(int i=0;i< strTiles.Length;i++)
                {
                    if (i > -1 && i < tiles.Length)
                    {
                        try
                        {
                            tiles[i].type = (TileTypes)int.Parse(strTiles[i]);
                        }
                        catch { }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for(int i=0;i<tiles.Length;i++)
            {
                tiles[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameAssets assets)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].Draw(spriteBatch, assets);
            }
        }

        public void Resize(Vector2 windowBounds)
        {
            winSize = windowBounds;
            this.SizeInit();
        }

    }
}
