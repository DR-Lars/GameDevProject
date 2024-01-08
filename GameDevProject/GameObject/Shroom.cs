﻿using GameDevProject.Animate;
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

namespace GameDevProject.GameObject
{
    internal class Shroom : IGameObject, IMovable
    {
        private Texture2D _texture;
        private Animation _animation;
        private MovementManager _movementManager;
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public IInputReader InputReader { get; set; }

        public Shroom(Texture2D texture, IInputReader inputReader)
        {
            _movementManager = new MovementManager();

            _texture = texture;
            InputReader = inputReader;

            _animation = new Animation(0);
            _animation.SelectAnimation(texture.Width, texture.Height, 4, 4);

            Position = new Vector2(0, 0);
            Speed = new Vector2(1, 1);
        }
        public void Update(GameTime gameTime)
        {
            Move();
            _animation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _animation.currentFrame.SourceRectangle, Color.White);
        }

        private void Move()
        {
            int aniDir = _movementManager.AnimationDirection(this);
            if (aniDir != _animation.direction)
            {
                _animation = new Animation(_movementManager.AnimationDirection(this));
            }
            _animation.SelectAnimation(_texture.Width, _texture.Height, 4, 4);
        }
    }
}