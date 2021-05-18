using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.GameContent;

namespace Platformer
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        private GameAssets assets;
        private Map map;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            
            /*Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            */
            assets = new GameAssets();
            map = new Map(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
            player = new Player(map);
        }

        private void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            map.Resize(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            assets.font = Content.Load<SpriteFont>("game_font");
            map.ReadMap(@"C:\Users\jules\OneDrive\Bureau\map.txt");

            for(int i=0;i <= 18; i++) 
            {
                string id = i.ToString();
                if(i<10)
                {
                    id = "0"+ i.ToString();
                }
                assets.tiles_textures.Add(this.Content.Load<Texture2D>("tile_" + id));
            }

            for (int i = 0; i <= 5; i++)
            {
                assets.player_textures.Add(this.Content.Load<Texture2D>("player_" + i.ToString()));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(29, 172, 224));
            
            spriteBatch.Begin();

           
            map.Draw(spriteBatch, assets);
            player.Draw(spriteBatch, assets);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }

}
