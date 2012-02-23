using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Imbube.NationalRail.Domain.Interface
{
    /// <summary>
    /// Domain Object Model
    /// </summary>
    public interface IStation
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        string Address1 { get; set; }
        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>The post code.</value>
        string PostCode { get; set; }
        /// <summary>
        /// Gets or sets the coordinate X.
        /// </summary>
        /// <value>The coordinate X.</value>
        long CoordinateX { get; set; }
        /// <summary>
        /// Gets or sets the coordinate Y.
        /// </summary>
        /// <value>The coordinate Y.</value>
        long CoordinateY { get; set; }
        /// <summary>
        /// Gets or sets the open time.
        /// </summary>
        /// <value>The open time.</value>
        DateTime OpenTime { get; set; }
        /// <summary>
        /// Gets or sets the close time.
        /// </summary>
        /// <value>The close time.</value>
        DateTime CloseTime { get; set; }
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        string Code { get; set; }
    }
}
