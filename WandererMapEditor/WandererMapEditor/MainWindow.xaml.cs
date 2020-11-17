using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GreenFox;
using System;
using System.IO;

namespace WandererMapEditor
{
    public class MainWindow : Window
    {
        public static FoxDraw foxDraw;
        public Canvas canvas;
        public static Tile[,] map;
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            canvas = this.Get<Canvas>("canvas");
            foxDraw = new FoxDraw(canvas);

            map = InitializeMap();
            DrawMap();
            this.PointerPressed += Canvas_PointerPressed;
            this.KeyDown += MainWindow_KeyDown;
        }

        private void Canvas_PointerPressed(object sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            var clickPoint = (e.GetPosition(canvas));
            int x = (int)(clickPoint.X / 72);
            var y = (int)(clickPoint.Y / 72);
            map[x, y].SwapTerrain();
        }
        private void MainWindow_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            if (e.Key == Avalonia.Input.Key.E)
            {
                ExportMap();
                this.Close();
            }
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public static Tile[,] InitializeMap()
        {
            var returnMap = new Tile[10, 10];
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    returnMap[row, col] = new Tile(true);
                }
            }
            return returnMap;
        }
        public static void DrawMap()
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    foxDraw.AddImage(map[row, col], row * 72, col * 72);
                }
            }
        }
        public static void ExportMap()
        {
            using (var writer = new StreamWriter("level.txt"))
            {
                foreach (var tile in map)
                {
                    writer.Write(tile.ToString());
                }
            }
        }
    }
    public class Tile : Image
    {
        bool isFloor = true;
        public Tile(bool IsFloor)
        { 
            if (IsFloor)
            {
                Source = new Avalonia.Media.Imaging.Bitmap("assets/floor.png");
                isFloor = true;
            }
            else
            {
                Source = new Avalonia.Media.Imaging.Bitmap("assets/wall.png");
                isFloor = false;
            }
        }
        public bool IsFloor()
        {
            return isFloor;
        }
        public void SwapTerrain()
        {
            if (isFloor)
            {
                Source = new Avalonia.Media.Imaging.Bitmap("assets/wall.png");
                isFloor = false;
            }
            else
            {
                Source = new Avalonia.Media.Imaging.Bitmap("assets/floor.png");
                isFloor = true;
            }
        }
        public override string ToString()
        {
            return IsFloor() ? "1" : "0";
        }
    }
}
