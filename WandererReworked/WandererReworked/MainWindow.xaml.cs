using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GreenFox;
using System;
using WandererReworked.enums;

namespace WandererReworked
{
    public class MainWindow : Window
    {
        public static FoxDraw foxDraw;
        public static Canvas canvas;
        public static GameObjectList gameObjects;
        public static TextBlock statusBar;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            canvas = this.Get<Canvas>("canvas");
            foxDraw = new FoxDraw(canvas);
            statusBar = this.Get<TextBlock>("textblock");

            gameObjects = new GameObjectList();
            gameObjects.LoadMap();
            gameObjects.LoadHero();
            gameObjects.LoadBoss();
            gameObjects.LoadSkeletons(3);
            gameObjects.Draw();
            gameObjects.PrintStatusBar();
            
            
            this.KeyUp += MainWindow_KeyDown;
        }
        private void MainWindow_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            var hero = gameObjects.Hero;
            switch (e.Key)
            {
                case Avalonia.Input.Key.Up:
                    hero.Move(direction.up);                    
                    break;
                case Avalonia.Input.Key.Down:
                    hero.Move(direction.down);
                    break;
                case Avalonia.Input.Key.Left:
                    hero.Move(direction.left);
                    break;
                case Avalonia.Input.Key.Right:
                    hero.Move(direction.right);
                    break;
            }
        }

        public static void NewLevel()
        {
            gameObjects.level++;
            var saveHero = gameObjects.Hero;
            saveHero.LevelUp();
            gameObjects.Clear();
            gameObjects.LoadMap();
            gameObjects.Add(saveHero);
            gameObjects.Hero.X = 0;
            gameObjects.Hero.Y = 0;
            gameObjects.LoadBoss();
            gameObjects.LoadSkeletons(3);
            gameObjects.Draw();
            gameObjects.PrintStatusBar();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
