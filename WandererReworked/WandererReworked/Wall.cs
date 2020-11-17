using System;
using System.Collections.Generic;
using System.Text;

namespace WandererReworked
{
    class Wall : GameObject
    {
        public Wall(int x, int y)
        {
            ZIndex = 0;
            isPassable = false;
            X = x;
            Y = y;
            Source = new Avalonia.Media.Imaging.Bitmap("assets/wall.png");
        }
    }
}
