using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Data.Linq;
using System.IO;
using System.Text;
using CommonMarzzan;

namespace CommonMarzzan
{
    public class HelperReglasMails
    {
        public static void GestionarReglaMoverMailSegunOrigen(long Origen, List<MailsDestino> mailDestinos, Marzzan_InfolegacyDataContext dc)
        {

            /// 1. Primero busco las reglas del tipo "Segun Origen" y que Accion1 sea "Mover" para los destinos y donde 
            /// el origen sea igual a la CondicionUsuario
            /// 
            /// 2. Con las reglas obtenidas busco los registros destino del mail y cargo la ubicación
            /// segun la definicion de la regla.
            List<long> idsDestinos = mailDestinos.Select(w => w.Usuario.Value).ToList();
            var reglasDefinidas = (from r in dc.MailsReglas
                                   where r.Accion1 == "Mover" && r.Nombre == "Segun Origen"
                                   && (r.CondicionUsuario != null && r.CondicionUsuario.Value == Origen)
                                   && (idsDestinos.Contains(r.Usuario.Value))
                                   select new
                                   {
                                       carpetaDestino = r.DestinoCarpeta.Value,
                                       r.Usuario
                                   }).ToList();

            foreach (var item in reglasDefinidas)
            {
                var mDestinos = (from d in mailDestinos
                                 where d.Usuario == item.Usuario
                                 select d).ToList();

                foreach (var m in mDestinos)
                {
                    m.Ubicacion = item.carpetaDestino;
                }
            }

            dc.SubmitChanges();

        }

    }
}
