using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using CommonMarzzan;

/// <summary>
/// Summary description for Direcciones
/// </summary>
namespace CommonMarzzan
{
    public partial class Direccione
    {
        public string DireccionCompleta
        {
            get
            {
                return this.Calle + " - " +
                this.Provincia + " - " + this.Departamento + " - " + this.Localidad;
            }
        }
    }
}
