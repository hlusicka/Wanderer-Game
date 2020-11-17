using System;
using System.Collections.Generic;
using System.Text;

namespace WandererReworked
{
    class Boss : Character
    {
        public Boss()
        {
            ZIndex = 2;
            LVL = MainWindow.gameObjects.level;
            Source = new Avalonia.Media.Imaging.Bitmap("assets/boss.png");
            X = 9;
            Y = 9;
            HP = 2* LVL * (rng.Next(6) + 1);
            DEF = (LVL / 2) * (rng.Next(6) + 1) + (rng.Next(6) + 1) / 2;
            ATK = LVL* (rng.Next(6) + 2);
        }
    }
}
