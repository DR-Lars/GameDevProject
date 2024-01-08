using GameDevProject.GameObject;
using GameDevProject.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Movement
{
    internal interface IMovable
    {

        public Hitbox hitbox { get; set; }
        public bool active { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public IInputReader InputReader { get; set; }
    }
}
