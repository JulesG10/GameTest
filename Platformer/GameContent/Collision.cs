using System;
using System.Collections.Generic;
using System.Text;

namespace Platformer.GameContent
{
    class Collision
    {
        public static bool isCollision(ICollisionObject rect1, ICollisionObject rect2)
        {
            if (rect1.position.X < rect2.position.X + rect2.size.X &&
                rect1.position.X + rect1.size.X > rect2.position.X &&
                rect1.position.Y < rect2.position.Y + rect2.size.Y &&
                rect1.size.Y + rect1.position.Y > rect2.position.Y)
            {
                return true;
            }

            return false;
        }
    }
}
