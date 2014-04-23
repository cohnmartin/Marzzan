using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonMarzzan
{
    public class HelperTxtBejerman
    {
        public const string NOTAPEDIDO = "NP";
        public const string REMITO = "RT";
        public const string TIPODOCUMENTO = "5";

        public static string GetStringWidth0(string Valor, int Largo)
        {

           return Valor.PadLeft(Largo, '0');

        }
        public static string GetStringNumeric(string Valor, int Largo)
        {

            return string.Format("{0:0.00}", int.Parse(Valor)).PadLeft(Largo, ' ').Replace(",",".");

        }
        public static string GetString(string Valor, int Largo,bool EsNumerico)
        {
            if (EsNumerico && Valor == "0")
            {
                return string.Format("{0:0.00}", int.Parse(Valor)).PadLeft(Largo, ' ').Replace(",", ".");
            }
            else if (EsNumerico && Valor != "0")
            {
                return Valor.PadLeft(Largo, ' ').Replace(",", "."); 
            }
            else
            {
                return Valor.PadRight(Largo, ' ');
            }

        }

    }
}
