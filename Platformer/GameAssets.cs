using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Platformer
{
    class GameAssets
    {
        public GameAssets()
        {

        }

        public List<Texture2D> tiles_textures = new List<Texture2D>();
        public SpriteFont font;
        public List<Texture2D> player_textures = new List<Texture2D>();
    }
}
