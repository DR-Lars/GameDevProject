using GameDevProject.GameObject.Block;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Level
{
    internal class Level1
    {
        Texture2D texture;
        public Level1(ContentManager content)
        {
            texture = content.Load<Texture2D>("tileGrass");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < 256; x += 32)
            {
                for(int y = 0; y < 144; y += 32)
                {
                    BlockFactory.CreateBlock("NORMAL", x, y, texture).Draw(spriteBatch);
                }
            }
        }
    }
}
