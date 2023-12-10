using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevProject.Input
{
    internal interface IInputReader
    {
        Vector2 ReadInput();
    }
}
