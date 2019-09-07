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

        /// <summary>
        /// The stating position of the mouse when the drag drop started
        /// </summary>
        private Point _mouseStartingPosition;

        /// <summary>
        /// The starting angle of the handle when the drag drop started
        /// </summary>
        private double _startingAngle;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new
                ApplicationViewModel();
        }

        #endregion
        
        #region Events

        /// <summary>
        /// Event to hundle the begining of the drag drop, to assign the values which should be assigned at the start of the drag drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            // Get the mouse position relative to the center of the handel
            _mouseStartingPosition = new Point(
                150 - e.HorizontalOffset,
                150 - e.VerticalOffset
                );

            // Get the angle of the handle
            _startingAngle = ((ApplicationViewModel)this.DataContext).Angle;
        }


        /// <summary>
        /// Event to handle the dragging, and updating the angle in respect to the angle of the mouse relative to the starting position of the dragging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            // Get the new position of the mouse 
            Point newPosition = new Point(
                _mouseStartingPosition.X - e.HorizontalChange,
                _mouseStartingPosition.Y - e.VerticalChange
                );

            // Computing the angles
            double angle1 = ArcTan(_mouseStartingPosition.X, _mouseStartingPosition.Y);

            double angle2 = ArcTan(newPosition.X, newPosition.Y);

            var angle = (angle2 - angle1);

            // Update the angle
            ((ApplicationViewModel)this.DataContext).Angle = _startingAngle + angle;
        }


        #endregion

        #region Helping Methods

        /// <summary>
        /// Computing the arc tan and converting it as required in respect to the quarter of the angle in the cartizian system
        /// </summary>
        /// <param name="x">The x coordination of the vector</param>
        /// <param name="y">The y coordination of the vector</param>
        /// <returns>The computed angle in degrees</returns>
        public double ArcTan(double x, double y)
        {
            // Get the angle and convert it to degrees
            double angle = Math.Atan(y / x) * 180 / Math.PI;

            // Farther processing according to where the angle reside
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
