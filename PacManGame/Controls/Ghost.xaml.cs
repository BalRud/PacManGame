using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace PacManGame.Controls
{
    /// <summary>
    /// Interaction logic for Ghost.xaml
    /// </summary>
    public partial class Ghost : UserControl
    {
        int currentX;
        int currentY;
        Point currentPoss;
        public Point startPosition = new Point(-1, -1);

        int levelWidth;
        int levelHeight;
        int tileSize;
        string[,] levelMap;

        bool goUp = false;
        bool goDown = false;
        bool goLeft = false;
        bool goRight = false;
        Random rand = new Random();

        Storyboard sbGhost = new Storyboard();

        #region ctor
        public Ghost(int levelWidth, int levelHeight, int tileSize, string[,] levelMap)
        {
            InitializeComponent();

            this.levelHeight = levelHeight;
            this.levelWidth = levelWidth;
            this.tileSize = tileSize;
            this.levelMap = levelMap;

        }
        #endregion

        #region methods
        public void SetPosition(Point point)
        {
            this.SetValue(Canvas.TopProperty, point.Y * tileSize + 1.0);
            this.SetValue(Canvas.LeftProperty, point.X * tileSize + 1.0);

            if (startPosition.X == -1)
            {
                startPosition = point;
            }
            currentPoss = point;
            currentX = (int)point.X;
            currentY = (int)point.Y;
        }

        public void NextMove()
        {
            ChooseDirrection(currentPoss);
            MoveGhost();
        }

        private void ChooseDirrection(Point currentPossition)
        {
            int d = rand.Next(0, 4);
            if (d == 0 && this.currentY > 0 && levelMap[this.currentX, this.currentY - 1] != "1")
            {
                goUp = true;
                goDown = false;
                goLeft = false;
                goRight = false;
            }
            else if (d == 1 && this.currentY < levelHeight - 1 && levelMap[this.currentX, this.currentY + 1] != "1")
            {
                goUp = false;
                goDown = true;
                goLeft = false;
                goRight = false;
            }
            else if (d == 2 && this.currentX > 0 && levelMap[this.currentX - 1, this.currentY] != "1")
            {
                goUp = false;
                goDown = false;
                goLeft = true;
                goRight = false;
            }
            else if (d == 3 && this.currentX < levelWidth - 1 && levelMap[this.currentX + 1, this.currentY] != "1")
            {
                goUp = false;
                goDown = false;
                goLeft = false;
                goRight = true;
            }
        }

        private void MoveGhost()
        {
            if (goUp && currentY > 0 && levelMap[currentX, currentY - 1] != "1")
            {
                MoveAnimation(new Point(currentX, currentY - 1), this, sbGhost);
            }
            if (goDown && currentY < levelHeight - 1 && levelMap[currentX, currentY + 1] != "1")
            {
                MoveAnimation(new Point(currentX, currentY + 1), this, sbGhost);
            }
            if (goLeft && currentX > 0 && levelMap[currentX - 1, currentY] != "1")
            {
                MoveAnimation(new Point(currentX - 1, currentY), this, sbGhost);
            }
            if (goRight && currentX < levelWidth - 1 && levelMap[currentX + 1, currentY] != "1")
            {
                MoveAnimation(new Point(currentX + 1, currentY), this, sbGhost);
            }

        }

        private void MoveAnimation(Point newPossition, FrameworkElement person, Storyboard sb)
        {
            var currentTop = (double)person.GetValue(Canvas.TopProperty);
            var currentLeft = (double)person.GetValue(Canvas.LeftProperty);
            var newTop = newPossition.Y * tileSize + 1.0;
            var newLeft = newPossition.X * tileSize + 1.0;

            var ms = 400;

            var topAnimation = new DoubleAnimation()
            {
                From = currentTop,
                To = newTop,
                Duration = TimeSpan.FromMilliseconds(ms)
            };
            var leftAnimation = new DoubleAnimation()
            {
                From = currentLeft,
                To = newLeft,
                Duration = TimeSpan.FromMilliseconds(ms)
            };

            Storyboard.SetTarget(leftAnimation, person);
            Storyboard.SetTargetProperty(leftAnimation, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(topAnimation, person);
            Storyboard.SetTargetProperty(topAnimation, new PropertyPath("(Canvas.Top)"));

            sb.Children.Add(leftAnimation);
            sb.Children.Add(topAnimation);
            sb.Begin();

            currentPoss = newPossition;
            currentX = (int)newPossition.X;
            currentY = (int)newPossition.Y;
        }

        public Rect GetRect(Canvas canvas)
        {
            GeneralTransform transform = this.TransformToVisual(canvas);
            return transform.TransformBounds(new Rect(0, 0, this.ActualWidth, this.ActualHeight));
        }

        #endregion
    }
}
