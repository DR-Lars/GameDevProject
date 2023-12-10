using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Animation _animation;
        private Vector2 _position, _speed;

        public Ninja(Texture2D texture)
        {
            _texture = texture;
            _animation = new Animation();
            _animation.FramesFromTextureProperties(texture.Width, texture.Height, 3, 6);

            _position = new Vector2(0, 0);
            _speed = new Vector2(1, 1);
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            var direction = Vector2.Zero;
            if (state.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
            }
            direction *= _speed;
            _position += direction;

            _animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _animation.currentFrame.SourceRectangle, Color.White);
            //Move();
        }

        private void Move()
        {
            _position += _speed;
            if (_position.X > 256 - 16 || _position.X < 0)
            {
                _speed.X *= -1;
            }
            if (_position.Y > 144 - 22 || _position.Y < 0)
            {
                _speed.Y *= -1;
            }
        }
    }
}
