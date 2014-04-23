using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMarzzan
{
    partial class CabeceraPedido
    {
        private DetallePedido[] _detallesInfo;
        private decimal _costoFlete;
        private string _transportista;
        private string _totalProductos;

        public DetallePedido[]  DetallesInfo
        {
            get { return _detallesInfo; }
            set { _detallesInfo = value; }
        }

        public decimal CostoFlete
        {
            get { return _costoFlete; }
            set { _costoFlete = value; }
        }

        public string Transportista
        {
            get { return _transportista; }
            set { _transportista = value; }
        }

        public string TotalProductos
        {
            get { return _totalProductos; }
            set { _totalProductos = value; }
        }

        public string MesAñoPedido
        {
            get { 
            
                return string.Format("{0:M/yyyy}" ,this.FechaPedido); 
            
            }
        }

    }


}
