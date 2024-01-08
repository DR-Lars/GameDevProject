﻿using GameDevProject.GameObject.Block;
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
        private ContentManager _content;
        public Level1(ContentManager content)
        {
            _content = content;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Background
            for (int x = 0; x < 256; x += 32)
            {
                for(int y = 0; y < 144; y += 32)
                {
                    BlockFactory.CreateBlock("NORMAL", x, y, _content.Load<Texture2D>("tileGrass")).Draw(spriteBatch);
                }
            }

            //Obstacle
            BlockFactory.CreateBlock("BOULDER", 50, 30, _content.Load<Texture2D>("tileBoulder")).Draw(spriteBatch);
        }
    }
}
