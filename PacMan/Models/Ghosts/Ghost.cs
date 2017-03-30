using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models.Ghosts
{
    class Ghost: Tile
    {
        public bool Vulnerable { get; set; }
        public Ghost()
        {

        }
    }
}
