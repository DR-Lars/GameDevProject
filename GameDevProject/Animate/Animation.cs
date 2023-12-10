using Microsoft.Xna.Framework;
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

        public int fps { get; set; } = 5;

        public Animation()
        {
            _frames = new List<AnimationFrame>();
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
                    case 2:
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

        public void FramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;

            for (int x = 0; x <= width - widthOfFrame; x += widthOfFrame)
            {
                _frames.Add(new AnimationFrame(new Rectangle(x, 0, widthOfFrame, heightOfFrame)));
            }
        }

    }
}
