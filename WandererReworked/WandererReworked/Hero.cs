using System;
using System.Collections.Generic;
using System.Text;
using WandererReworked.enums;

namespace WandererReworked
{
    public class Hero : Character
    {
        public int maxHP;
        public Hero()
        {
            ZIndex = 2;
            X = 0;
            Y = 0;
            Source = new Avalonia.Media.Imaging.Bitmap("assets/hero-down.png");
            HP = 20 + 3 * (rng.Next(6) + 1);
            maxHP = HP;
            DEF = 2* (rng.Next(6) + 1);
            ATK = 5 + (rng.Next(6) + 1);
            LVL = 1;
        }
        public override bool Move(direction direction)
        {
            if (!alive) return true;
            var hero = MainWindow.gameObjects.Hero;
            switch (direction)
            {
                case direction.left:
                    hero.Source = new Avalonia.Media.Imaging.Bitmap("assets/hero-left.png");
                    break;
                case direction.right:
                    hero.Source = new Avalonia.Media.Imaging.Bitmap("assets/hero-right.png");
                    break;
                case direction.up:
                    hero.Source = new Avalonia.Media.Imaging.Bitmap("assets/hero-up.png");
                    break;
                case direction.down:
                    hero.Source = new Avalonia.Media.Imaging.Bitmap("assets/hero-down.png");
                    break;
            }
            base.Move(direction);
            MainWindow.gameObjects.moveCounter++;
            if (MainWindow.gameObjects.moveCounter % 2 ==1)
            {
                MainWindow.gameObjects.MoveSkeletons();
            }
            return true;
        }
        public string GetStatusText()
        {
            return $"Hero (Level {LVL}) HP: {HP}/{maxHP} | DEF: {DEF} | ATK: {ATK}";
        }
        public void LevelUp()
        {
            LVL++;
            var hpIncrease = (rng.Next(6) + 1);
            maxHP += hpIncrease;
            HP += hpIncrease;
            DEF += (rng.Next(6) + 1);
            ATK += (rng.Next(6) + 1);
            var perkGenerator = rng.Next(10);
            if (perkGenerator<5)
            {
                HP += maxHP / 10;
            }
            else if (perkGenerator<9)
            {
                HP += maxHP / 3;
            }
            else
            {
                HP = maxHP;
            }
            if (HP > maxHP) HP = maxHP;
        }
    }
}
