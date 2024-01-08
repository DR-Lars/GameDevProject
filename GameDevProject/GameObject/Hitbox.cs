using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameObject
{
    internal class Hitbox
    {
        public Rectangle rectangle;

        private Rectangle originalRectangle;

        public Hitbox(Rectangle rectangle)
        {
            this.rectangle = rectangle;
            Update(Vector2.Zero);
        }

        public void Update(Vector2 position)
        {
            rectangle = new Rectangle((position + (originalRectangle.Location.ToVector2() - position)).ToPoint(), originalRectangle.Size.ToVector2().ToPoint());
        }


        public bool IsTouching(Hitbox hitbox)
        {
            return rectangle.Intersects(hitbox.rectangle);
        }
    }
}
