using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace Platformer.GameContent
{
    public enum PlayerStates
    {
        RIGHT,
        D_RIGHT,
        LEFT,
        D_LEFT,
        BACK,
        D_BACK
    }

    class Player : ICollisionObject
    {
        public Map map;

        public Player(Map map)
        {
            this.map = map;
        }

        private Vector2 _position;
        public Vector2 position 
        {
            get { return _position;  }
            set { _position = value; }
        }

        public Vector2 size
        {
            get { return new Vector2(map.tileSize, map.tileSize); }
            set { }
        }

        public const int moveSpeed = 200;
        public const int fallSpeed = 100;
        public PlayerStates activeState = PlayerStates.LEFT;
        private long stateSwitch = 0;

        private Vector2 PositionTile(Vector2 position)
        {
            return new Vector2((float)Math.Floor((decimal)(position.X / map.tileSize)) * map.tileSize, (float)Math.Floor((decimal)(position.Y / map.tileSize)) * map.tileSize);
        }

        private void StateSwitch()
        {
            switch (activeState)
            {
                case PlayerStates.LEFT:
                    activeState = PlayerStates.D_LEFT;
                break;
                case PlayerStates.RIGHT:
                    activeState = PlayerStates.D_RIGHT;
                    break;
                case PlayerStates.BACK:
                    activeState = PlayerStates.D_BACK;
                    break;
                case PlayerStates.D_LEFT:
                    activeState = PlayerStates.LEFT;
                    break;
                case PlayerStates.D_RIGHT:
                    activeState = PlayerStates.RIGHT;
                    break;
                case PlayerStates.D_BACK:
                    activeState = PlayerStates.BACK;
                    break;
            }
        }

        private Tile getTileAround(int x=0,int y=0)
        {
            return map.GetTile(new Vector2(this.PositionTile(_position).X+ x, this.PositionTile(_position).Y + y));
        }

        private bool CollisionAround(int x=0, int y=0)
        {
            Tile tile= this.getTileAround(x, y);
            if (tile.position.X != -1 && tile.position.Y != -1)
            {
                return Collision.isCollision(tile, this);
            }
            return false;
        }

        private bool CollisionAround(TileTypes exclude,int x = 0, int y = 0)
        {
            Tile tile = this.getTileAround(x, y);
            
            if (tile.position.X != -1 && tile.position.Y != -1)
            {
                if(tile.type != exclude)
                {
                    return Collision.isCollision(tile, this);
                }
            }
            return false;
        }

        private void checkFall(GameTime deltatime)
        {
            if (!this.CollisionAround(TileTypes.NONE,0, map.tileSize))
            {
                activeState = PlayerStates.BACK;
                Vector2 moveState = position;
                moveState.Y += (fallSpeed * (float)deltatime.ElapsedGameTime.TotalSeconds);
                if (moveState.Y >= 0 && moveState.Y <= 450)
                {
                    _position.Y = moveState.Y;
                }
            }
        }

        private void PlayerMove(GameTime deltatime)
        {
            float move = (moveSpeed * (float)deltatime.ElapsedGameTime.TotalSeconds);
            Vector2 moveState = position;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                if (!this.CollisionAround(TileTypes.NONE, 0, -map.tileSize))
                {
                    moveState.Y -= move;
                }

                activeState = PlayerStates.BACK;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                move = ((moveSpeed / 10) * (float)deltatime.ElapsedGameTime.TotalSeconds);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (!this.CollisionAround(TileTypes.NONE, -map.tileSize, 0))
                {
                    moveState.X -= move;
                }
                activeState = PlayerStates.LEFT;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                if (!this.CollisionAround(TileTypes.NONE, map.tileSize, 0))
                {
                    moveState.X += move;
                }

                activeState = PlayerStates.RIGHT;
            }


            if (moveState.X >= 0 && moveState.X <= 800)
            {
                _position.X = moveState.X;
            }

            if (moveState.Y >= 0 && moveState.Y <= 450)
            {
                _position.Y = moveState.Y;
            }
        }

        public void Update(GameTime deltatime)
        {
            stateSwitch += deltatime.ElapsedGameTime.Ticks;
            if (stateSwitch > 20_00_000)
            {
                stateSwitch = 0;
                this.StateSwitch();
            }
            this.PlayerMove(deltatime);
            this.checkFall(deltatime);
        }

        public void Draw(SpriteBatch spriteBatch,GameAssets assets)
        {
            Rectangle rect = new Rectangle();
            rect.X = (int)(_position).X;
            rect.Y = (int)(_position).Y;
            rect.Width = (int)size.X;
            rect.Height = (int)size.Y;

            spriteBatch.Draw(assets.player_textures[(int)activeState], rect, Color.Red);
            spriteBatch.DrawString(assets.font,"("+ this.PositionTile(_position).X.ToString() + ";" + (this.PositionTile(_position).Y ).ToString()+")", new Vector2(10, 10), Color.White);
        }

    }
}
