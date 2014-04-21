using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonMarzzan;

/// <summary>
/// Summary description for TempClass
/// </summary>
public class TempClass
{
    public class tempBusqueda
    {
        public string NombreFormula { get; set; }
        public string CodigoFormula { get; set; }
    }

    public class tempEnviosMisiones
    {
        public string NroPedido { get; set; }
        public decimal MontoTotal{ get; set; }
        public string Cliente { get; set; }
        public DateTime FechaPedido{ get; set; }
    }

    public class TempInfoPedidos
    {
        public string Grupo { get; set; }
        public List<CabeceraPedido> Pedidos { get; set; }
    }

    public class TempAuditoriaImpresion
    {
        public string Grupo { get; set; }
        public string Transporte { get; set; }
        public List<long> Pedidos { get; set; }
        public int Cantidad { get; set; }

    }
}
