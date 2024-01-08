using GameDevProject.Animate;
using GameDevProject.Input;
using GameDevProject.Movement;
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
    internal class Ninja : IGameObject, IMovable
    {
        private Texture2D _texture;
        private Animation _animation;
        public  MovementManager movementManager;
        public Hitbox hitbox { get; set; }
        public bool active { get; set; } = true;
        public Vector2 Acceleration { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public IInputReader InputReader { get; set; }

        public Ninja(Texture2D texture, IInputReader inputReader)
        {
            movementManager = new MovementManager();

            _texture = texture;
            InputReader = inputReader;

            _animation = new Animation(0);
            _animation.SelectAnimation(texture.Width, texture.Height, 4, 4);

            Position = new Vector2(0, 0);
            Speed = new Vector2(1, 1);
            Acceleration = new Vector2(0.1f, 0.1f);

            CalculateHitbox();
        }
        public void Update(GameTime gameTime)
        {
            if (active)
            {
                Move();
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

        private void Move()
        {
            int aniDir = movementManager.AnimationDirection(this, 0);
            if (aniDir != _animation.direction)
            {
                _animation = new Animation(movementManager.AnimationDirection(this, 0));
            }
            _animation.SelectAnimation(_texture.Width, _texture.Height, 4, 4);
        }

        private void CalculateHitbox()
        {
            hitbox = new Hitbox(new Rectangle((int)Position.X, (int)Position.Y, 12, 12));
        }
    }
}
