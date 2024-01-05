using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Input
{
    internal class StandardNPC : IInputReader
    {
        private bool idle = true;
        private double totalElapsedSeconds;
        public Vector2 ReadInput()
        {
            if (idle)
            {
                Random random = new Random();
                int randomDirection = random.Next(8);
                int randomTime = random.Next(1000) * 10;
                totalElapsedSeconds++;

                if (totalElapsedSeconds >= randomTime)
                {
                    totalElapsedSeconds -= randomTime;
                    switch (randomDirection)
                    {
                        case 1:
                            return new Vector2(-1, 0);
                        case 2:
                            return new Vector2(1, 0);
                        case 3:
                            return new Vector2(0, -1);
                        case 4:
                            return new Vector2(0, 1);
                        default:
                            return Vector2.Zero;
                    }
                }
                else
                {
                    return Vector2.Zero;
                }
            }
            else
            {
                return Vector2.Zero;
            }
        }
    }
}
