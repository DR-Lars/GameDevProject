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
    internal class Level1 : ILevel
    {
        private ContentManager _content;
        public List<Block> obstacles {  get; set; }
        public bool active { get; set; }
        public Level1(ContentManager content)
        {
            _content = content;
            obstacles = new List<Block>();
            obstacles.Add(BlockFactory.CreateBlock("BOULDER", 50, 25, _content.Load<Texture2D>("tileBoulder")));
            obstacles.Add(BlockFactory.CreateBlock("BOULDER", 90, 70, _content.Load<Texture2D>("tileBoulder")));
            obstacles.Add(BlockFactory.CreateBlock("BOULDER", 115, 16, _content.Load<Texture2D>("tileBoulder")));
            obstacles.Add(BlockFactory.CreateBlock("BOULDER", 190, 65, _content.Load<Texture2D>("tileBoulder")));
            obstacles.Add(BlockFactory.CreateBlock("BOULDER", 140, 95, _content.Load<Texture2D>("tileBoulder")));
        }

        public void Update(GameTime gameTime)
        {
            foreach (var block in obstacles)
            {
                block.Update(gameTime);
            }
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
                foreach(Block block in obstacles)
                {
                    block.Draw(spriteBatch);
                }

                //Characters
            }
        }
    }
}
