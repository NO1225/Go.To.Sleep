using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
