using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.GameContent
{
    class Tile : ICollisionObject
    {
        public Tile()
        {

        }

        private Vector2 _position;
        public Vector2 position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 size
        {
            get { return new Vector2(16,16); }
            set {  }
        }

        public void Draw(SpriteBatch graphic, GameAssets assets)
        {
            Rectangle rect = new Rectangle();
            rect.Width = (int)size.X;
            rect.Height = (int)size.Y;
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;

            graphic.Draw(assets.textures[1], rect,Color.White);
        }

        public void Update(GameTime deltatime)
        {
            
        }
    }
}
