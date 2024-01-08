using GameDevProject.Animate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameObject
{
    internal class Coin : IGameObject
    {
        private Texture2D _texture;
        private Animation _animation;
        public Hitbox hitbox { get; set; }
        public bool active { get; set; }
        public Vector2 Position { get; set; }
        public Coin(Texture2D texture)
        {
            _texture = texture;
            _animation = new Animation(0);


            for (int x = 0; x <= texture.Width - texture.Width/4; x += texture.Width / 4)
            {
                _animation.AddFrame(new AnimationFrame(new Rectangle(x, 0, texture.Width / 4, texture.Height)));
            }

            Position = new Vector2(200, 20);

            hitbox = new Hitbox(new Rectangle((int)Position.X, (int)Position.Y, texture.Width/4, texture.Height));
        }
        public void Update(GameTime gameTime)
        {
            if (active)
            {
                _animation.Update(gameTime);
                hitbox.Update(Position);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(_texture, Position, _animation.currentFrame.SourceRectangle, Color.White);
            }
        }
    }
}
