using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Song.Game.GameObjects;

namespace Song.Game
{
    class GameUpdate
    {
        private GameData data = new GameData();
        private long last;


        public GameUpdate()
        {
            this.last = DateTime.Now.Ticks;
        }

        public void Start(GameData d,CanvasControl control)
        {
            this.data = d;
            bool run = true;
            float framesRate = 1.0f / 60.0f;

            for (int i = 0; i< this.data.balls.Count; i++)
            {
                this.data.balls[i].SetCanvasControl(control);
            }

            while (run)
            {
                float deltatime = (DateTime.Now.Ticks - this.last);
                if (deltatime >= 1000.0f)
                {
                    this.last = DateTime.Now.Ticks;
                }
                deltatime = framesRate/deltatime;

                for (int i = 0; i < this.data.balls.Count; i++)
                {
                    try
                    {
                        this.data.balls[i].data = this.data;
                        this.data.balls[i].Update(deltatime);
                    }
                    catch { }
                }
            }
        }

        public GameData getData()
        {
            return this.data;
        }
    }
}
