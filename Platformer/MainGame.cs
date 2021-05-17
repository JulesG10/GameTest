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
            Window.AllowUserResizing = true;
            Window.AllowAltF4 = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            player = new Player();
            assets = new GameAssets();
            map = new Map(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));
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

            for(int i=0;i <= 18; i++) 
            {
                string id = i.ToString();
                if(i<10)
                {
                    id = "0"+ i.ToString();
                }
                assets.textures.Add(this.Content.Load<Texture2D>("tile_" + id));
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
            GraphicsDevice.Clear(Color.Cyan);
            
            spriteBatch.Begin();

           
            map.Draw(spriteBatch, assets);
            player.Draw(spriteBatch, assets);
            spriteBatch.DrawString(assets.font, ((int)(1/gameTime.ElapsedGameTime.TotalSeconds)).ToString(), new Vector2(100, 10), Color.White);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }

}
