﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManNamespace.Models
{
    public class Bullet : Tile
    {
        public Bullet()
        {
            this.isMoving = false;
            this.Speed = 0.2;
            this.CurrentImageUrl = "bullet-R.png";
        }
    }
}
