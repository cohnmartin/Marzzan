using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for Niveles
/// </summary>
namespace CommonMarzzan
{
    public partial class Nivele
    {
        private List<DetalleNivele> _ColDetalleNivelesAgrupado = new List<DetalleNivele>();

        public List<DetalleNivele> ColDetalleNivelesAgrupado
        {
            get
            {

                var grupos = from D in this._ColDetalleNiveles
                             group D by D.Grupo into c
                             where c.Count() > 1
                             select new { Grupo = c.Key, componentes = c };

                if (grupos.Count() == 0)
                    return this.ColDetalleNiveles.ToList<DetalleNivele>();
                else
                {
                    grupos = from D in this._ColDetalleNiveles
                             group D by D.Grupo into c
                             select new { Grupo = c.Key, componentes = c };

                    foreach (var componente in grupos)
                    {
                        _ColDetalleNivelesAgrupado.Add(componente.componentes.First<DetalleNivele>());
                        if (componente.componentes.Count() > 1)
                        {
                            _ColDetalleNivelesAgrupado[_ColDetalleNivelesAgrupado.Count - 1].MultiplesElementos = true;
                        }
                    }

                    return _ColDetalleNivelesAgrupado;
                }

            }
        }
    }
}
