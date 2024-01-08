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
        public Rectangle BoundingBox { get; set; }
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public bool active { get; set; } = true;

        public Block(int x, int y, Texture2D texture)
        {
            BoundingBox = new Rectangle(x, y, 32, 32);
            Passable = true;
            Color = Color.White;
            Texture = texture;
        }
        public void Update(GameTime gameTime)
        {

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
