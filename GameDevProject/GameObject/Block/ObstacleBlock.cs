using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameObject.Block
{
    internal class ObstacleBlock : Block
    {
        public Hitbox hitbox;
        public ObstacleBlock(int x, int y, Texture2D texture) : base(x, y, texture)
        {
            BoundingBox = new Rectangle(x, y, 32, 32);
            Passable = true;
            hitbox = new Hitbox(BoundingBox);
        }

    }
}
