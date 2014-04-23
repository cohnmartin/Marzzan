using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DetalleNivele
/// </summary>
namespace CommonMarzzan
{
    public partial class DetalleNivele
    {
        private bool _MultiplesElementos = false;

        public bool MultiplesElementos
        {
            get { return _MultiplesElementos; }
            set { _MultiplesElementos = value; }
        }

    }
}
