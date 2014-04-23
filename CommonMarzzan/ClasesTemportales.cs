using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMarzzan
{
    public class TempFormasPago
    {
        // Datos Comunes
        public long? IdDetalle{ get; set; }
        public string DescGeneral { get; set; }
        public decimal Monto { get; set; }
        public string DescMonto { get; set; }

        public string DescFormaPago { get; set; }
        public long IdFormaPago { get; set; }
        
        public string DescBanco { get; set; }
        public long? IdBanco { get; set; }

        // Datos Cheque
        public string CH_NroCheque { get; set; }
        public DateTime? CH_FechaCobro { get; set; }
        public string CH_Titular { get; set; }


        // Datos Transferencia
        public DateTime? T_FechaTranferencia { get; set; }
        public string T_NroControl { get; set; }
        public string T_NroCtaDestino { get; set; }
        public string T_Referencia { get; set; }

        // Datos Deposito
        public DateTime? D_FechaDeposito { get; set; }
        public string D_NroCtaDestino { get; set; }
        public string D_NroTransaccion { get; set; }

        // Otro Medio Pago
        public DateTime? O_FechaPago { get; set; }
        public string O_NroOperacion { get; set; }
        public long? O_IdOperador { get; set; }

    }
}
