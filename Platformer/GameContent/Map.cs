using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer.GameContent
{
    class Map
    {
        private Tile[] tiles;
        private Vector2 winSize;

        public Map(Vector2 windowBounds)
        {
            this.winSize = windowBounds;
            this.SizeInit();
        }

        private void SizeInit()
        {
            int size = 16;
           
            int w = (int)(winSize.X/size);
            int h = (int)(winSize.Y / size);
            int tilesSize = w * h;

            tiles = new Tile[tilesSize];
            

            for (int row = 0; row < h; row++)
            {
                for (int col = 0; col < w; col++)
                {
                    int index = w * row + col;
                    tiles[index] = new Tile();
                    tiles[index].position = new Vector2(col * size, row * size);
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
