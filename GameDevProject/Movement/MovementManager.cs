using GameDevProject.Animate;
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
        private int savedDirection;
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

        public int AnimationDirection(IMovable movable)
        {
            Vector2 beforePos = movable.Position;
            Move(movable);
            if (beforePos.Y > movable.Position.Y)
            {
                savedDirection = 1;
                return savedDirection;
            }
            else if (beforePos.Y < movable.Position.Y)
            {
                savedDirection = 0;
                return savedDirection;
            }
            else if (beforePos.X > movable.Position.X)
            {
                savedDirection = 2;
                return savedDirection;
            }
            else if (beforePos.X < movable.Position.X)
            {
                savedDirection = 3;
                return savedDirection;
            }
            else return 4 + savedDirection;
        }
    }
}
