using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.GameContent;
using System.Diagnostics;

namespace Platformer
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Player player;
        private GameAssets assets;
        private Map map;

        private bool editMode = false;
        private bool roundEditMode = false;
        private int currentTilesIndex = 0;

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
            map.ReadMap(@"C:\Users\jules\source\repos\Platformer\Platformer\map.txt");

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

        private InputDelay enterDelay = new InputDelay(80,true);
        private InputDelay spaceDelay = new InputDelay(80, true);

        private InputDelay rightClickDelay = new InputDelay(55,true);
        private InputDelay leftClickDelay = new InputDelay(55, true);

        protected override void Update(GameTime gameTime)
        {
            float deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            bool enter = enterDelay.isActive(deltatime, () =>
            {
                return Keyboard.GetState().IsKeyDown(Keys.Enter);
            });
            bool space = enterDelay.isActive(deltatime, () =>
            {
                return Keyboard.GetState().IsKeyDown(Keys.Space);
            });

            bool rightClick = rightClickDelay.isActive(deltatime, ()=>
            {
                return (Mouse.GetState().RightButton == ButtonState.Pressed);
            });

            bool leftClick = leftClickDelay.isActive(deltatime, () =>
            {
                return (Mouse.GetState().LeftButton == ButtonState.Pressed);
            });

            if(space && this.editMode)
            {
                this.roundEditMode = !this.roundEditMode;
            }

            if (enter)
            {
                this.editMode = !this.editMode;
            }

            if(rightClick)
            {
                if(this.editMode)
                {
                    this.currentTilesIndex++;
                    if(this.currentTilesIndex >= this.assets.tiles_textures.Count)
                    {
                        this.currentTilesIndex = 0;
                    }
                }
            }

            if(leftClick)
            {
                if(this.editMode)
                {
                    Tile t = new Tile();
                    t.type = (TileTypes)this.currentTilesIndex;

                    this.map.SetTile(new Vector2(Mouse.GetState().X,Mouse.GetState().Y),t);
                }
            }

            if(!this.editMode)
            {
                player.Update(gameTime);
            }
            else
            {

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(29, 172, 224));
            spriteBatch.Begin();

           
            map.Draw(spriteBatch, assets);
            if(this.editMode)
            {
                Rectangle rect = new Rectangle();
                rect.X = Mouse.GetState().X - (this.map.tileSize/2);
                rect.Y = Mouse.GetState().Y - (this.map.tileSize / 2);

                if(this.roundEditMode)
                {
                    rect.X = ((int)(Mouse.GetState().X / this.map.tileSize) * this.map.tileSize);
                    rect.Y = ((int)(Mouse.GetState().Y / this.map.tileSize) * this.map.tileSize);
                }

                rect.Width = this.map.tileSize;
                rect.Height = this.map.tileSize;

                spriteBatch.Draw(this.assets.tiles_textures[this.currentTilesIndex], rect,Color.White);
            }
            else
            {
                player.Draw(spriteBatch, assets);
            }
            

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }

}
