using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using WandererReworked.enums;

namespace WandererReworked
{
    public class GameObject : Image
    {
        public Random rng = new Random();
        public int X { get; set; }
        public int Y { get; set; }
        public int TileSize
        {
            get
            {
                return 72;
            }
        }

        internal bool isPassable;
        public bool IsPassable()
        {
            return isPassable;
        }
        public virtual bool Move(direction direction)
        {
            return false;
        }

    }
}
