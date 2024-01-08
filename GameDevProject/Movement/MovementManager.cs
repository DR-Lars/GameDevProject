using GameDevProject.Animate;
using GameDevProject.GameObject;
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
        public double secondCounter;
        public Ninja target;
        public Vector2 direction { get; set; }
        public int fps { get; set; } = 5;

        public void Move(IMovable movable)
        {
            direction = movable.InputReader.ReadInput();


            Vector2 distance = direction * movable.Speed;
            Vector2 futurePosition = movable.Position + distance;


            if (futurePosition.X > -1 && futurePosition.X < (256 - 16) &&
                futurePosition.Y > -1 && futurePosition.Y < (144 - 16))
            {
                movable.Position = futurePosition;
            }
        }

        public void MoveToTarget(IMovable movable)
        {
            if (secondCounter >= 1d / fps)
            {
                direction = target.Position - movable.Position;
                Vector2 distance = direction * movable.Speed;
                movable.Position += distance;
                secondCounter = 0;
            }
        }

        public int AnimationDirection(IMovable movable, int movementType)
        {
            Vector2 beforePos = movable.Position;
            switch (movementType)
            {
                case 1:
                    MoveToTarget(movable);
                    break;
                default:
                    Move(movable);
                    break;
            }
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
