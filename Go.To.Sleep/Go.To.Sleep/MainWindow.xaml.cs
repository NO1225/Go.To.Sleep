using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Go.To.Sleep
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private Point _mouseOffsetToCenter;

        private double _startingAngle;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new
                ApplicationViewModel();
        }

        #endregion
        
        #region Events

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            var h = e.HorizontalOffset;
            var v = e.VerticalOffset;

            _mouseOffsetToCenter = new Point(
                150 - h,
                150 - v
                );
            _startingAngle = ((ApplicationViewModel)this.DataContext).Angle;
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Point newPosition = new Point(
                _mouseOffsetToCenter.X - e.HorizontalChange,
                _mouseOffsetToCenter.Y - e.VerticalChange
                );

            double angle1 = ArcTan(_mouseOffsetToCenter.X, _mouseOffsetToCenter.Y);

            double angle2 = ArcTan(newPosition.X, newPosition.Y);

            var angle = (angle2 - angle1);

            ((ApplicationViewModel)this.DataContext).Angle = _startingAngle + angle;
        }


        #endregion

        #region Helping Methods

        public double ArcTan(double x, double y)
        {
            double angle = Math.Atan(y / x) * 180 / Math.PI;
            if (x >= 0 && y >= 0)
                return angle;
            else if (x >= 0 && y < 0)
                return 360 + angle;
            else if (x < 0 && y > 0)
                return 180 + angle;
            else
                return 180 + angle;
        }

        #endregion
    }
}
