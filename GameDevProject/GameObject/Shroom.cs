using GameDevProject.Animate;
using GameDevProject.Input;
using GameDevProject.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDevProject.GameObject
{
    internal class Shroom : IGameObject, IMovable
    {
        private Texture2D _texture;
        private Animation _animation;
        private MovementManager _movementManager;
        private Ninja _target;
        public Hitbox hitbox { get; set; }
        public bool active { get; set; } = true;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public IInputReader InputReader { get; set; }

        public Shroom(Texture2D texture, Ninja target)
        {
            _movementManager = new MovementManager();

            _texture = texture;
            _target = target;

            _movementManager.target = _target;

            _animation = new Animation(0);
            _animation.SelectAnimation(texture.Width, texture.Height, 4, 4);

            Position = new Vector2(200, 20);
            Speed = new Vector2((float)0.1, (float)0.1);

            CalculateHitbox();
        }
        public void Update(GameTime gameTime)
        {
            if (active)
            {
                Move(gameTime.ElapsedGameTime.TotalSeconds);
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

        private void Move(double totalseconds)
        {
            _movementManager.secondCounter += totalseconds;
            int aniDir = _movementManager.AnimationDirection(this, 1);
            if (aniDir != _animation.direction)
            {
                _animation = new Animation(_movementManager.AnimationDirection(this, 1));
            }
            _animation.SelectAnimation(_texture.Width, _texture.Height, 4, 4);
        }

        private void CalculateHitbox()
        {
            hitbox = new Hitbox(new Rectangle((int)Position.X, (int)Position.Y, 14, 14));
        }
    }
}
