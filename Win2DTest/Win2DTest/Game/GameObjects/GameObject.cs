using System;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Numerics;
using Windows.Foundation;

namespace Song.Game.GameObjects
{
    abstract class GameObject
    {
        protected  Vector2 position;
        protected  Size size;
        public GameData data;

        public bool CheckCollision(GameObject obj)
        {
            if (this.position.X < obj.position.X + obj.size.Width &&
                this.position.X + size.Width > obj.position.X &&
                this.position.Y < obj.position.Y + obj.size.Height &&
                this.position.Y + size.Height > obj.position.Y)
            {
                return true;
            }
            return false;
        }

        public virtual void SetCanvasControl(CanvasControl control) { }
        public virtual void Update(double deltatime) { }
        public virtual void Draw(CanvasDrawingSession session) { }
    }
}
