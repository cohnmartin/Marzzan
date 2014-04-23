using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections;
using System.Data.Linq;
using System.Collections.Generic;


/// <summary>
/// Summary description for Productos
/// </summary>

namespace CommonMarzzan
{
    public partial class Producto
    {
        public string DescripcionCompleta
        {
            get
            {
                if (this.Tipo == 'D' || this.Tipo == 'R' || this.Tipo == 'G' || this.Tipo == 'P' || this.Tipo == 'I')
                    return "";
                else
                    return this.Descripcion + " x ";
                    //return Helper.ObtenerDescripcionCompletaProducto(this) + " x ";
            }

        }

        public long CantidadElementoRequeridos
        {
            get 
            {
                long cantidad = 0;
                if (this.ColComposiciones != null && this.ColComposiciones.Count > 0)
                {
                    var grupos = (from C in this.ColComposiciones
                                  where C.Grupo != null && C.TipoComposicion == "C"
                                  group C by C.Grupo into g
                                  select new
                                  {
                                      key = g.Key,
                                      Elementos = g
                                  });

                    foreach (var item in grupos)
                    {
                        cantidad += long.Parse(item.Elementos.First().Cantidad);
                    }
                
                }

                return cantidad;
            }
        }

       
        
    }
}
