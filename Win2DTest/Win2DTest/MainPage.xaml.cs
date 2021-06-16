using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;
using System.Threading;
using Song.Game;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Song.Game.GameObjects;
using System.Numerics;
using System.Diagnostics;
using Windows.UI.Core;

namespace Song
{

    public sealed partial class MainPage : Page
    {
        private GameData gameData = new GameData();
        private GameRender gameRender = new GameRender();
        private GameUpdate gameLoop = new GameUpdate();
        private  Size size;

        public MainPage()
        {
            this.InitializeComponent();
            this.SizeChanged += MainPage_SizeChanged;
        }


        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.size = e.NewSize;
            this.InitGame();
        }

        private void GenerateBalls(int counts)
        {
            if(counts > 100)
            {
                counts = 100;
            }

            for (int i = 0; i < counts; i++)
            {
                Ball b = new Ball();

                bool stop = false;
                foreach (Ball ball in this.gameData.balls)
                {
                    if (ball.CheckCollision(b))
                    {
                        stop = true;
                        break;
                    }
                }

                if (!stop)
                {
                    this.gameData.balls.Add(b);
                }
                else
                {
                    i--;
                }
            }
        }

        private void InitGame()
        {
            this.GenerateBalls(100);

            new Thread(() =>
            {
                gameLoop.Start(gameData, this.gameCanvas);
            }).Start();
        }

        void gameCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            gameRender.updateGameData(gameLoop.getData());
            gameRender.Draw(args.DrawingSession);

            sender.Invalidate();
        }

        private void gameCanvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {

        }
    }
}
