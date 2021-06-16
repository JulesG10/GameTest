using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas;
using Windows.UI;
using Song.Game.GameObjects;
using System.Numerics;

namespace Song.Game
{
    class GameRender
    {
        private GameData data = new GameData();

        public GameRender()
        {

        }

        public void updateGameData(GameData d)
        {
            this.data = d;
        }


        public void Draw(CanvasDrawingSession session)
        {
            session.DrawText(this.data.balls.Count.ToString(), new Vector2(10, 10), Color.FromArgb(255, 0, 0, 0));

            for(int i=0;i< this.data.balls.Count;i++)
            {
                if(!this.data.balls[i].exists)
                {
                    this.data.balls.RemoveAt(i);
                    i--;
                }
                else
                {
                    this.data.balls[i].Draw(session);
                }
            }
        }
    }
}
