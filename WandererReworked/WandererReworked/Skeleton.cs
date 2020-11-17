using System;
using System.Collections.Generic;
using System.Text;

namespace WandererReworked
{
    class Skeleton : Character
    {
        public Skeleton(int mapLvl)
        {
            ZIndex = 2;
            Source = new Avalonia.Media.Imaging.Bitmap("assets/skeleton.png");
            int x;
            int y;
            do
            {
                x = rng.Next(10);
                y = rng.Next(10);
            } while (!MainWindow.gameObjects.IsPassableAt(x,y));
            X = x;
            Y = y;
            var levelGenerator = rng.Next(10);
            if (levelGenerator<5)
            {
                LVL = mapLvl;
            }
            else if (levelGenerator< 9)
            {
                LVL = mapLvl + 1;
            }
            else
            {
                LVL = mapLvl + 2;
            }
            HP = 2* LVL * (rng.Next(6) + 1);
            DEF = (LVL / 2) * (rng.Next(6) + 1);
            ATK = LVL * (rng.Next(6) + 1);
        }
    }
}
