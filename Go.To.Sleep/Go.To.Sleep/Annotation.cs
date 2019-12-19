using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Go.To.Sleep
{
    /// <summary>
    /// Annotation model
    /// </summary>
    public class Annotation
    {
        /// <summary>
        /// The angle at which this annotation should be ratated
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// The text that should be shown in this annotation
        /// </summary>
        public string Text { get; set; }

        public double InnerRadius { get; set; }

        public double OuterRaduis { get; set; }

        public double Width { get; set; } = 93.17486;

        public double Height { get; set; } = 35.11;

        public double CenterX { get; set; } = 46.58743;

        public double CenterY { get; set; } = 180;

        public ObservableCollection<Point> Points { get; set; } = new ObservableCollection<Point>()
        {
            new Point(0, 6.13),
            new Point(7.76,35.11),
            new Point(85.414,35.11),
            new Point(93.175,6.13),
            new Point(0,6.13),
        };

        public ObservableCollection<Size> Sizes { get; set; } = new ObservableCollection<Size>()
        {
            new Size(140,140),
            new Size(180,180)
        };


    }
}
