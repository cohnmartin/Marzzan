using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public enum TipoIVA
{
    Inscripto = 1,
    Monotributista = 6,
    NoCategorizado = 7
}

public enum TipoGanacia
{
    Inscripto = 1,
    NoAlcanzado = 3,
    NoInscripto = 2
}

public enum TipoAltaCliente
{
    Potencial = 12,
    Revendedor = 1,
    Patocinador = 17
}

public enum EstadoUsuarios
{
    Pendiente = 31,
    Rechazado = 32,
    Aprobado = 33
}

public enum TipoClientes
{
    Director = 5,
    Coordinador = 2,
    Lider = 11,
    SubConGestion = 3,
    SubSinGestion = 2,
    Consultor = 1,
    Interno,
    PotencialBolso = 12,
    Marketing

}

public enum TiposDeParametros
{
    LimiteCtaBolsos = 60,
    LimiteContraReembolso = 61,
    Email = 24,
    Usuario = 25,
    Password = 26,
    SMTP = 27,
    LimiteCtaBolsoCanepa = 62,
    LimiteCompra = 63,
    LimiteIIBB = 175

}
public abstract class EstadosMails
{
    public const string SINLEER = "Sin Leer";
    public const string RESPONDIDO = "Respondido";
    public const string LEIDO = "Leido";
    public const string ENVIADO = "Enviado";
    public const string ELIMINADO = "Eliminado";
    public const string ELIMINADORECIVIDO = "Mail Recivido Eliminado";
    public const string ELIMINADOENVIADO = "Mail Enviado Eliminado";
}

public abstract class TipoClientesConstantes
{
    public const string DIRECTOR = "5";
    public const string COORDIANADOR = "2";
    public const string LIDER = "11";
    public const string SUBCONGESTION = "3";
    public const string SUBSINGESTION = "2";
    public const string SUBSINGESTIONDESC = "Sub Sin Gestion";
    public const string CONSULTOR = "1";
    public const string INTERNO = "NULL";
    public const string DESC_BOLSOS = "Bolsos";
    public const string DESC_STOCK = "Stock";
    public const string DESC_POTECIAL = "Potencial";
    public const string DESC_SINCARGO = "Sin Cargo";


}

public abstract class FormaDePagosConstantes
{
    public const string CUENTACORRIENTE = "2";
    public const string CONCONRTAREEMBOLSO = "3";
    public const string SINCONRTAREEMBOLSO = "4";
    public const string PAGOFACIL = "267";
    public const string CREDITO = "269";

}

public abstract class EstadoLogisticaGuias
{
    public const string PENDIENTE = "P - PENDIENTE";
    public const string VISITADO = "V - VISITADO";
    public const string ENTREGADA = "E - ENTREGADA";
    public const string DEVUELTA = "D - DEVUELTA";
    public const string SINIESTRADA_PARCIAL = "SP - SINIESTRADA PARCIAL";
    public const string SINIESTRADA_TOTAL = "ST - SINIESTRADA TOTAL";
    public const string EXTRAVIADAS = "Ex - EXTRAVIADAS";
    public const string ENTREGALIDER = "EL - ENTREGA LIDER";

}


