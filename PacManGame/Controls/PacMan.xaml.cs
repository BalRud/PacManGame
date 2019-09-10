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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacManGame.Controls
{
    /// <summary>
    /// Interaction logic for PacMan.xaml
    /// </summary>
    public partial class PacMan : UserControl
    {

        public int currentX;
        public int currentY;
        public Point currentPoss;
        public Point startPosition = new Point(-1, -1);

        int levelWidth;
        int levelHeight;
        int tileSize;
        string[,] levelMap;

        private Storyboard sbPacMan = new Storyboard();


        #region ctor
        public PacMan(int levelWidth, int levelHeight, int tileSize, string[,] levelMap)
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

        public void MovePlayer(bool goUp, bool goDown, bool goLeft, bool goRight)
        {
            if (goUp && currentY > 0 && levelMap[currentX, currentY - 1] != "1")
            {
                //SetPosition(currentX, currentY - 1);
                MoveAnimation(new Point(currentX, currentY - 1), this, sbPacMan);
            }
            if (goDown && currentY < levelHeight - 1 && levelMap[currentX, currentY + 1] != "1")
            {
                //SetPosition(currentX , currentY + 1);
                MoveAnimation(new Point(currentX, currentY + 1), this, sbPacMan);
            }
            if (goLeft && currentX > 0 && levelMap[currentX - 1, currentY] != "1")
            {
                //SetPosition(currentX - 1, currentY);
                MoveAnimation(new Point(currentX - 1, currentY), this, sbPacMan);
            }
            if (goRight && currentX < levelWidth - 1 && levelMap[currentX + 1, currentY] != "1")
            {
                //SetPosition(currentX + 1, currentY);
                MoveAnimation(new Point(currentX + 1, currentY), this, sbPacMan);
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
            return transform.TransformBounds(new Rect(-this.ActualWidth * 0.25, -this.ActualHeight * 0.25, this.ActualWidth * 0.75, this.ActualHeight * 0.75));
        }

        public void Died()
        {
            MoveAnimation(startPosition, this, sbPacMan);
        }

        #endregion
    }
}
