using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Movement
{
    internal class MovementManager
    {
        public Vector2 direction { get; set; }
        public void Move(IMovable movable)
        {
            direction = movable.InputReader.ReadInput();


            Vector2 distance = direction * movable.Speed;
            Vector2 futurePosition = movable.Position + distance;


            if (futurePosition.X > -1 && futurePosition.X < (256 - 16) &&
                futurePosition.Y > -1 && futurePosition.Y < (144 - 22))
            {
                movable.Position = futurePosition;
            }
        }
    }
}
