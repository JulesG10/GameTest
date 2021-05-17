using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Platformer.GameContent
{
    class Player : ICollisionObject
    {
        public Player()
        {

        }

        private Vector2 _position;
        public Vector2 position 
        {
            get { return _position;  }
            set { _position = value; }
        }

        public Vector2 size
        {
            get { return new Vector2(16, 16); }
            set { }
        }

        public const int moveSpeed = 200; 

        public void Update(GameTime deltatime)
        {
            float move = moveSpeed * (float)deltatime.ElapsedGameTime.TotalSeconds;

            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _position.X -= move;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _position.X += move;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _position.Y -= move;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _position.Y += move;
            }
           
        }

        public void Draw(SpriteBatch spriteBatch,GameAssets assets)
        {
            Rectangle rect = new Rectangle();
            rect.X = (int)_position.X;
            rect.Y = (int)_position.Y;
            rect.Width = (int)size.X;
            rect.Height = (int)size.Y;

            spriteBatch.Draw(assets.textures[0], rect, Color.Red);
            spriteBatch.DrawString(assets.font, ((int)_position.X).ToString() + " " + ((int)_position.Y).ToString(), new Vector2(10, 10), Color.White);
        }

    }
}
