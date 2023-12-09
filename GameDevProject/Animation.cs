﻿using Microsoft.Xna.Framework;
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
        public Animation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            currentFrame = frames[0];
        }

        public void Update()
        {
            currentFrame = frames[counter];
            counter++;
            if(counter >= frames.Count)
            {
                counter = 0;
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
