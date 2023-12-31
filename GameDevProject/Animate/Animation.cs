﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Animate
{
    internal class Animation
    {
        public AnimationFrame currentFrame { get; set; }
        private List<AnimationFrame> _frames;
        private int _counter;
        private double _secondCounter;
        private bool _plus;

        public int widthOfFrame, heightOfFrame;
        public int direction { get; private set; }
        
        public int fps { get; set; } = 5;

        public Animation(int direction)
        {
            _frames = new List<AnimationFrame>();
            this.direction = direction;
        }

        public void AddFrame(AnimationFrame frame)
        {
            _frames.Add(frame);
            currentFrame = _frames[0];
        }

        public void Update(GameTime gameTime)
        {
            currentFrame = _frames[_counter];

            _secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (_secondCounter >= 1d / fps)
            {
                switch (_counter)
                {
                    case 0:
                        _plus = true;
                        break;
                    case 3:
                        _plus = false;
                        break;
                    default:
                        break;
                }
                if (_plus)
                {
                    _counter++;
                }
                else
                {
                    _counter--;
                }
                _secondCounter = 0;
            }
        }

        public void SelectAnimation(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            widthOfFrame = width / numberOfWidthSprites;
            heightOfFrame = height / numberOfHeightSprites;

            if (direction >= 4)
            {
                direction -= 4;
            }

            for (int x = 0; x <= height - heightOfFrame; x += heightOfFrame)
            {
                _frames.Add(new AnimationFrame(new Rectangle(direction * widthOfFrame, x, widthOfFrame, heightOfFrame)));
            }
        }

    }
}
