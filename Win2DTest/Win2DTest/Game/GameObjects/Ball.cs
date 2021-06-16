using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.Foundation;
using Windows.UI;
using Windows.System;
using Microsoft.Graphics.Canvas.Text;

namespace Song.Game.GameObjects
{
    class Ball : GameObject
    {
        public double maxDuration = 100;
        private double duration = 0;
        public bool exists { get; private set; }

        private bool isHover = false;
        private Vector2 positionHover;
        private int clickCount = 0;

        private bool rectShapeEnable = false;

        private Color c = Color.FromArgb(255, 0, 0, 0);

        public Ball(Vector2 startPos)
        {
            this.position = startPos;
            this.size = new Size(100, 100);
            this.exists = true;
        }

        public Ball()
        {
            Random rnd = new Random();
            Vector2 startPos = new Vector2(rnd.Next(0, 1920), rnd.Next(0, 1080));

            this.maxDuration = rnd.Next(100, 500);

            this.position = startPos;
            this.size = new Size(100, 100);
            this.exists = true;
        }

        public override void SetCanvasControl(CanvasControl control)
        {
            control.PointerMoved += Control_PointerMoved;
            control.PointerPressed += Control_PointerPressed;
            control.ContextRequested += Control_ContextRequested;
        }

        private void Control_ContextRequested(UIElement sender, Windows.UI.Xaml.Input.ContextRequestedEventArgs args)
        {
            rectShapeEnable = !rectShapeEnable;
        }

        private bool isPointCollision(Point p)
        {
            if (p.X >= this.position.X && p.Y >= this.position.Y)
            {
                if (p.X <= this.position.X + this.size.Width && p.Y <= this.position.Y + this.size.Height)
                {
                    return true;
                }
            }

            return false;
        }

        private void Control_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point p = e.GetCurrentPoint((UIElement)sender).Position;
            if (this.isPointCollision(p))
            {
                this.clickCount++;
            }
        }

        private void Control_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point p = e.GetCurrentPoint((UIElement)sender).Position;
            if (this.isPointCollision(p))
            {
                this.isHover = true;
                this.positionHover = new Vector2((float)p.X, (float)p.Y);
                this.c = Color.FromArgb(255, 255, 0, 0);
            }
            else
            {
                this.isHover = false;
                this.c = Color.FromArgb(255, 0, 0, 0);
            }
        }

        public override void Update(double deltatime)
        {
            if (this.duration > this.maxDuration)
            {
                this.exists = false;
            }
            else
            {
                this.duration += deltatime;
                this.exists = true;
            }
        }

        public override void Draw(CanvasDrawingSession session)
        {
            if (this.exists)
            {
                int per = (int)((100 * this.duration) / this.maxDuration);
                CanvasTextFormat textFormat = new CanvasTextFormat();
                textFormat.FontSize = 15;
                Vector2 textPos = new Vector2((float)(this.position.X + (this.size.Width / 2) - textFormat.FontSize), (float)(this.position.Y + (this.size.Height / 2) - textFormat.FontSize));
                session.DrawText(per.ToString() + "%", textPos, this.c, textFormat);

                Rect rect = new Rect();
                rect.X = this.position.X;
                rect.Y = this.position.Y;
                rect.Width = this.size.Width;
                rect.Height = this.size.Height;

                if(rectShapeEnable)
                {
                    session.DrawRectangle(rect, this.c);
                }
                else
                {
                    session.DrawCircle((float)(rect.X + (rect.Width / 2)), (float)(rect.Y + (rect.Width / 2)), (float)rect.Width / 2, this.c);
                }
               

                Rect rayon = new Rect();
                int r_w = (int)((size.Width * 2) - (((size.Width * 2) * this.duration) / this.maxDuration));
                int r_h = (int)((size.Height * 2) - (((size.Height * 2) * this.duration) / this.maxDuration));

                rayon.X = this.position.X - ((r_w - this.size.Width) / 2);
                rayon.Y = this.position.Y - ((r_h - this.size.Height) / 2);
                rayon.Width = r_w;
                rayon.Height = r_h;

                byte alpha = (byte)(255 - ((255 * this.duration) / this.maxDuration));
                if (rectShapeEnable)
                {
                    session.DrawRectangle(rayon, Color.FromArgb(alpha, 0, 255, 0));
                }
                else
                {
                    session.DrawCircle((float)(rayon.X + (rayon.Width / 2)), (float)(rayon.Y + (rayon.Width / 2)), (float)rayon.Width / 2, Color.FromArgb(alpha, 0, 255, 0));
                }

                if (this.isHover)
                {
                    CanvasTextFormat f = new CanvasTextFormat();
                    f.FontSize = 10;
                    int cpd = (int)(this.clickCount > 0 ? this.duration/ this.clickCount : 0);
                    int cpdper = (int)(cpd * 100/this.maxDuration);

                    string info = "now (%): "+per+"/100 \nnow: " + this.duration + "\nend: " + this.maxDuration + "\nclick: " + this.clickCount+"\ncpd:"+ cpd + "\ncpd(%): "+ cpdper + "\n("+this.positionHover.X+";"+this.positionHover.Y+")";
                    session.DrawText(info, this.positionHover, this.c, f);
                }
            }
        }
    }
}
