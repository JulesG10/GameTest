using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Platformer
{
    interface ICollisionObject
    {
        public void Update(GameTime deltatime);
        public void Draw(SpriteBatch graphic,GameAssets assets);

        public  Vector2 size {  get; set; }
        public  Vector2 position { get; set; }
    }

}
