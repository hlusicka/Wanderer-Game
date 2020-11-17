using System;
using System.Collections.Generic;
using System.Text;
using WandererReworked.enums;

namespace WandererReworked
{
    public class Character : GameObject
    {
        public bool alive = true;
        public int HP;
        public int DEF;
        public int ATK;
        public int LVL;
        public override bool Move(direction direction)
        {
            if (!alive) return true;
            var destinationX = X;
            var destinationY = Y;
            switch (direction)
            {
                case direction.up:
                    destinationY--;
                    break;
                case direction.down:
                    destinationY++;
                    break;
                case direction.left:
                    destinationX--;
                    break;
                case direction.right:
                    destinationX++;
                    break;
            }
            if (MainWindow.gameObjects.IsPassableAt(destinationX, destinationY))
            {
                X = destinationX;
                Y = destinationY;
                MainWindow.foxDraw.SetPosition(this, X * TileSize, Y * TileSize);
                return true;
            }
            else
            {
                var enemy = MainWindow.gameObjects.GetTargetAt(destinationX, destinationY);
                if (enemy != null)
                {
                    Fight(enemy);
                }
            }
            return false;
        }
        public void Die()
        {
            alive = false;
            Source = this is Hero ? new Avalonia.Media.Imaging.Bitmap("assets/rip-blood.png"): new Avalonia.Media.Imaging.Bitmap("assets/rip.png");
            isPassable = true;
            ZIndex = 1;
            if (this is Boss) MainWindow.NewLevel();
        }
        public void Fight(Character enemy)
        {
            if (!(this is Hero || enemy is Hero))return;
            while (true)
            {
                int attack = 2 * (rng.Next(6) + 1) + this.ATK;
                if (attack > enemy.DEF)
                {
                    enemy.HP -= attack - enemy.DEF;
                    if (enemy.HP <= 0)
                    {
                        enemy.Die();
                        MainWindow.gameObjects.PrintStatusBar();
                        return;
                    }
                }

                attack = 2 * (rng.Next(6) + 1) + enemy.ATK;
                if (attack > this.DEF)
                {
                    this.HP -= attack - this.DEF;
                    if (this.HP <= 0)
                    {
                        this.Die();
                        MainWindow.gameObjects.PrintStatusBar();
                        return;
                    }
                }
            }
        }
    }
}
