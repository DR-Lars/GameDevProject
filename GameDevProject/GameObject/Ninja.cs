using GameDevProject.Animate;
using GameDevProject.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameObject
{
    internal class Ninja : IGameObject
    {
        private Texture2D _texture;
        private Animation _animation;
        private Vector2 _position, _speed;
        private IInputReader _inputReader;

        public Ninja(Texture2D texture, IInputReader inputReader)
        {
            _texture = texture;
            _inputReader = inputReader;

            _animation = new Animation();
            _animation.FramesFromTextureProperties(texture.Width, texture.Height, 3, 6);

            _position = new Vector2(0, 0);
            _speed = new Vector2(1, 1);
        }
        public void Update(GameTime gameTime)
        {
            Move();
            _animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _animation.currentFrame.SourceRectangle, Color.White);
        }

        private void Move()
        {
            Vector2 direction = _inputReader.ReadInput();
            direction *= _speed;
            _position += direction;
        }
    }
}
