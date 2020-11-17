using System;
using System.Collections.Generic;
using System.Text;

namespace WandererReworked
{
    class Floor : GameObject
    {
        public Floor(int x, int y)
        {
            ZIndex = 0;
            isPassable = true;
            X = x;
            Y = y;
            Source = new Avalonia.Media.Imaging.Bitmap("assets/floor.png");
        }
    }
}
