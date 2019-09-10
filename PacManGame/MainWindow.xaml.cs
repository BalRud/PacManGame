using PacManGame.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PacManGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentScore;
        int livesCount;
        const int levelWidth = 40;
        const int levelHeight = 30;
        const int tileSize = 25;

        bool goUp = false;
        bool goDown = false;
        bool goLeft = false;
        bool goRight = false;

        public string[,] levelMap = new string[levelWidth, levelHeight];
        Tile[,] levelTiles = new Tile[levelWidth, levelHeight];

        PacMan player = null;
        List<Ghost> ghosts = new List<Ghost>();
        DispatcherTimer timer = new DispatcherTimer();
        Point exitPoint;

        public MainWindow()
        {
            InitializeComponent();

            player = new PacMan(levelWidth, levelHeight, tileSize, levelMap);
            cnvLevel.Children.Add(player);


            ghosts.Add(new Ghost(levelWidth, levelHeight, tileSize, levelMap));
            ghosts[0].MrYelow.Visibility = Visibility.Visible;
            ghosts.Add(new Ghost(levelWidth, levelHeight, tileSize, levelMap));
            ghosts[1].MrBlue.Visibility = Visibility.Visible;
            ghosts.Add(new Ghost(levelWidth, levelHeight, tileSize, levelMap));
            ghosts[2].MrPink.Visibility = Visibility.Visible;
            foreach (var ghost in ghosts)
            {
                cnvLevel.Children.Add(ghost);
            }

            CreateLevel();

            timer.Tick += Tick;
            timer.Interval = TimeSpan.FromMilliseconds(400);
            timer.Start();
        }

        private void CreateLevel()
        {
            for (var c = 0; c < levelWidth; c++)
            {
                grdLevel.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (var l = 0; l < levelHeight; l++)
            {
                grdLevel.RowDefinitions.Add(new RowDefinition());
            }

            var fileName = string.Format(@"Levels\01.txt");

            using (var sr = new System.IO.StreamReader(fileName))
            {
                var y = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    for (var x = 0; x < line.Length; x++)
                    {
                        levelMap[x, y] = line[x].ToString();

                        var tile = new Tile();
                        if (line[x] == '1')
                        {
                            tile.CreateWall();
                        }
                        if (line[x] == 'E')
                        {
                            tile.CreateExit();
                            exitPoint = new Point(x, y);
                        }
                        if (line[x] == 'Y')
                        {
                            ghosts[0].SetPosition(new Point(x, y));
                        }
                        if (line[x] == 'B')
                        {
                            ghosts[1].SetPosition(new Point(x, y));
                        }
                        if (line[x] == 'P')
                        {
                            ghosts[2].SetPosition(new Point(x, y));
                        }
                        if (line[x] == 'S')
                        {
                            player.SetPosition(new Point(x, y));
                        }
                        tile.SetValue(Grid.ColumnProperty, x);
                        tile.SetValue(Grid.RowProperty, y);
                        grdLevel.Children.Add(tile);
                        levelTiles[x, y] = tile;

                    }
                    y++;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up && player.currentY > 0 && levelMap[player.currentX, player.currentY - 1] != "1")
            {
                goUp = true;
                goDown = false;
                goLeft = false;
                goRight = false;
            }
            else if (e.Key == Key.Down && player.currentY < levelHeight - 1 && levelMap[player.currentX, player.currentY + 1] != "1")
            {
                goUp = false;
                goDown = true;
                goLeft = false;
                goRight = false;
            }
            else if (e.Key == Key.Left && player.currentX > 0 && levelMap[player.currentX - 1, player.currentY] != "1")
            {
                goUp = false;
                goDown = false;
                goLeft = true;
                goRight = false;
            }
            else if (e.Key == Key.Right && player.currentX < levelWidth - 1 && levelMap[player.currentX + 1, player.currentY] != "1")
            {
                goUp = false;
                goDown = false;
                goLeft = false;
                goRight = true;
            }
        }

        private void Tick(object sender, EventArgs e)
        {
            if (player.currentPoss == exitPoint)
            {
                LevelCleared();
            }
            player.MovePlayer(goUp, goDown, goLeft, goRight);

            foreach (var ghost in ghosts)
            {
                ghost.NextMove();
            }
            if (levelTiles[player.currentX, player.currentY].Dot.Visibility == Visibility.Visible)
            {
                levelTiles[player.currentX, player.currentY].CollectDot();
                currentScore += 10;
                this.lb_Score.Content = "Score: " + currentScore;
                if (currentScore % 500 == 0)
                {
                    AddLife();
                }
            }
            if (Collision())
            {
                PlayerDied();
            }
        }

        private void AddLife()
        {
            livesCount++;
            this.lb_Life.Content = "Lives: " + livesCount;
        }

        private void RemoveLife()
        {
            if (livesCount > 0)
            {
                livesCount--;
                this.lb_Life.Content = "Lives: " + livesCount;
            }
            else
            {
                lb_Game_Over.Visibility = Visibility.Visible;
            }
        }

        private bool Collision()
        {
            var rectPlayer = player.GetRect(cnvLevel);

            foreach (var ghost in ghosts)
            {
                var rectGhost = ghost.GetRect(cnvLevel);

                if (rectPlayer.IntersectsWith(rectGhost))
                {
                    return true;
                }
            }

            return false;
        }

        private void PlayerDied()
        {
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;

            player.Died();
            foreach (var ghost in ghosts)
            {
                ghost.SetPosition(ghost.startPosition);
            }
            RemoveLife();
        }

        private void LevelCleared()
        {
            lb_Cleared.Visibility = Visibility.Visible;
            timer.Stop();
        }
    }
}
