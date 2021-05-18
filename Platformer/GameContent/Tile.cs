using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.GameContent
{
    class Tile : ICollisionObject
    {
        public TileTypes type = TileTypes.NONE;

        public Tile()
        {

        }

        private Vector2 _position;
        public Vector2 position
        {
            get { return _position; }
            set { _position = value; }
        }

        private Vector2 _size;
        public Vector2 size
        {
            get { return _size; }
            set { _size = value;  }
        }

        public void Draw(SpriteBatch graphic, GameAssets assets)
        {
            if(type != TileTypes.NONE)
            {
                Rectangle rect = new Rectangle();
                rect.Width = (int)size.X;
                rect.Height = (int)size.Y;
                rect.X = (int)position.X;
                rect.Y = (int)position.Y;

                graphic.Draw(assets.tiles_textures[(int)type], rect, Color.White);
            }
        }

        public void Update(GameTime deltatime)
        {
            
        }
    }
}
