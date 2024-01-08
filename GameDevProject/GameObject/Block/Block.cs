using GameDevProject.Animate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameObject.Block
{
    internal class Block : IGameObject
    {
        public Hitbox hitbox { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public bool active { get; set; } = true;

        public Block(int x, int y, Texture2D texture)
        {
            BoundingBox = new Rectangle(x, y, 32, 32);
            hitbox = new Hitbox(BoundingBox);
            Color = Color.White;
            Texture = texture;
        }
        public void Update(GameTime gameTime)
        {
            hitbox.Update(new Vector2(BoundingBox.X, BoundingBox.Y));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color);
            }
        }
    }
}
