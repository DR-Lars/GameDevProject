using GameDevProject.GameObject;
using GameDevProject.GameObject.Block;
using GameDevProject.Input;
using Microsoft.Xna.Framework;
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
    internal class Level2 : ILevel
    {
        private ContentManager _content;
        public bool active { get; set; } = false;
        public Level2(ContentManager content)
        {
            _content = content;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                //Background
                for (int x = 0; x < 256; x += 32)
                {
                    for (int y = 0; y < 144; y += 32)
                    {
                        BlockFactory.CreateBlock("NORMAL", x, y, _content.Load<Texture2D>("tileGrass")).Draw(spriteBatch);
                    }
                }

                //Obstacle
                BlockFactory.CreateBlock("BOULDER", 80, 50, _content.Load<Texture2D>("tileBoulder")).Draw(spriteBatch);

                //Characters
            }
        }
    }
}
