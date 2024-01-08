using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.GameObject.Block
{
    internal class BlockFactory
    {
        public static Block CreateBlock(
        string type, int x, int y, Texture2D texture)
        {
            Block newBlock = null;
            type = type.ToUpper();
            if (type == "NORMAL")
            {
                newBlock = new Block(x, y, texture);
            }
            if (type == "TRAP")
            {
                //newBlock = new TrapBlock(x, y, graphics);
            }
            if (type == "SLIME")
            {
                //newBlock = new SlimeBlock(x, y, graphics);
            }
            return newBlock;
        }
    }

}
