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
        private Animation _animation;

        public Ninja(Texture2D texture)
        {
            _texture = texture;
            _animation = new Animation();
            _animation.FramesFromTextureProperties(texture.Width, texture.Height, 3, 6);
        }
        public void Update(GameTime gameTime)
        {
            _animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(0, 0), _animation.currentFrame.SourceRectangle, Color.White);
        }
    }
}
