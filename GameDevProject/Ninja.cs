using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject
{
    internal class Ninja : IGameObject
    {
        private Texture2D _texture;
        private Rectangle _rectFrame;
        private int _frameX;

        public Ninja(Texture2D texture)
        {
            _texture = texture;
            _rectFrame = new Rectangle(_frameX+1, 0, 14, 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(0, 0), _rectFrame, Color.White);
        }
        public void Update()
        {
            _frameX += 16;
            if (_frameX > 48)
            {
                _frameX = 0;
            }
            _rectFrame.X = _frameX;
        }
    }
}
