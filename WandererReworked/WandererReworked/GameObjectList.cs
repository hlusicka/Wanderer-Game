using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GreenFox;
using WandererReworked.enums;

namespace WandererReworked
{
    public class GameObjectList
    {
        public int moveCounter = 0;
        public int level = 1;
        public List<GameObject> GameObjects = new List<GameObject>();
        public Hero Hero
        {
            get
            {
                foreach (var gameobject in GameObjects)
                {
                    if (gameobject is Hero) return gameobject as Hero;
                }
                return null;
            }
        }
        public void Add(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }
        public bool IsPassableAt(int x, int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9)
            {
                return false;
            }
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.X == x && gameObject.Y == y)
                {
                    if (!gameObject.IsPassable())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void Draw()
        {
            foreach (var gameObject in GameObjects)
            {
                MainWindow.foxDraw.AddImage(gameObject, gameObject.X * 72, gameObject.Y * 72);
            }
        }
        public void LoadMap()
        {
            string path = $"assets/level{level % 5}.txt";
            using (var reader = new StreamReader(path))
            {
                for (int i = 0; i < 100; i++)
                {
                    char nextChar = (char)reader.Read();
                    if (nextChar == '1')
                    {
                        this.Add(new Floor(i / 10, i % 10));
                    }
                    else
                    {
                        this.Add(new Wall(i / 10, i % 10));
                    }
                }
            }
        }
        public void LoadHero()
        {
            Add(new Hero());
        }
        public void LoadBoss()
        {
            Add(new Boss());
        }
        public void LoadSkeletons(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Add(new Skeleton(level));
            }
        }
        public void Clear()
        {
            MainWindow.canvas.Children.RemoveAll(GameObjects);
            GameObjects.Clear();
        }
        public void MoveSkeletons()
        {
            var rng = new Random();
            foreach (var gameObject in GameObjects)
            {


                if (gameObject is Skeleton)
                {
                    var x = gameObject.X;
                    var y = gameObject.Y;
                    if (IsPassableAt(x - 1, y) || IsPassableAt(x + 1, y) || IsPassableAt(x, y - 1) || IsPassableAt(x, y + 1))
                    {
                        bool moved = false;
                        do
                        {
                            var direction = rng.Next(4);
                            moved = gameObject.Move((direction)direction);
                        } while (moved == false);
                        
                    }
                }
            }
        }
        public Character GetTargetAt(int x, int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9)
            {
                return null;
            }
            foreach (var gameObject in GameObjects)
            {
                if (gameObject.X == x && gameObject.Y == y && gameObject is Character)
                {
                    return gameObject as Character;
                }
            }
            return null;
        }
        public void PrintStatusBar()
        {
            MainWindow.statusBar.Text = MainWindow.gameObjects.Hero.GetStatusText();
        }

    }
}
