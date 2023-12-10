using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    internal class Animation
    {
        public AnimationFrame currentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;
        private double secondCounter;
        private bool right;

        public int fps { get; set; } = 6;

        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            currentFrame = frames[0];
        }

        public void Update(GameTime gameTime)
        {
            currentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if(secondCounter >= 1d / fps)
            {
                switch (counter)
                {
                    case 0:
                        right = true;
                        break;
                    case 2:
                        right = false;
                        break;
                    default:
                        break;
                }
                if (right)
                {
                    counter++;
                }
                else
                {
                    counter--;
                }
                secondCounter = 0;
            }
        }

        public void FramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;

            for (int x = 0; x <= width - widthOfFrame; x += widthOfFrame)
            {
                frames.Add(new AnimationFrame(new Rectangle(x, 0, widthOfFrame, heightOfFrame)));
            }
        }

    }
}
