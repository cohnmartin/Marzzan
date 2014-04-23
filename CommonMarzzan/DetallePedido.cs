using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CommonMarzzan
{
    partial class DetallePedido
    {
        private string _tipo;
        private object _descProductosUtilizados;
        private long _idPadre;
        private long _IdRegaloPresentacion;
        private long _IdRegaloSeleccionado;
        private string _DescPadre;
        private string _DescripcionCompleta;
        private string _ProductoDesc;
        private string _PresentacionDesc;
        private string _RegaloDesc;

        public bool UnoPorPedido
        {
            get;
            set;
        }

        public long IdRelacionDetallePromo
        {
            get;
            set;
        }

        public string Tipo
        {
            get
            {

                if (_tipo == null && this.objProducto != null)
                {
                    if (this.objProducto.Tipo != 'P' && this.objProducto.Tipo != 'D')
                    {
                        if (this.objProducto.objPadre.EsUltimoNivel.Value)
                        {
                            _DescripcionCompleta = this.ProductoDesc + " x " + this.PresentacionDesc;
                            _idPadre = this.objProducto.objPadre.IdProducto;
                            _DescPadre = Helper.ObtenerDescripcionCompletaProducto(this.objProducto.objPadre);
                        }
                        else
                        {
                            _DescripcionCompleta = this.objProducto.DescripcionCompleta + this.PresentacionDesc;
                            _idPadre = this.objProducto.objPadre.objPadre.IdProducto;
                            _DescPadre = Helper.ObtenerDescripcionCompletaProducto(this.objProducto.objPadre.objPadre);
                        }
                    }
                    else
                    {
                        _DescripcionCompleta = this.PresentacionDesc;
                        _idPadre = this.objProducto.objPadre.IdProducto;
                        _DescPadre = this.objProducto.objPadre.Descripcion;
                    }

                    return this.objProducto.Tipo.ToString();
                }
                else
                {
                    return _tipo;
                }

            }
            set { _tipo = value; }
        }

        public object DescProductosUtilizados
        {
            get { return _descProductosUtilizados; }
            set { _descProductosUtilizados = value; }
        }

        public long IdPadre
        {
            get { return _idPadre; }
            set { _idPadre = value; }
        }

        public string DescPadre
        {
            get { return _DescPadre; }
            set { _DescPadre = value; }
        }

        public string DescripcionCompleta
        {
            get
            {
                if (_DescripcionCompleta != "")
                {
                    return _DescripcionCompleta;
                }
                else
                {
                    return _DescripcionCompleta;
                }
            }
            set { _DescripcionCompleta = value; }
        }

        public string ProductoDesc
        {
            get
            {
                if (_ProductoDesc != null)
                {
                    return _ProductoDesc;
                }
                else
                {
                    if (this.objProducto != null)
                        return this.objProducto.Descripcion;
                    else
                        return "";
                }
            }
            set { _ProductoDesc = value; }
        }

        public string PresentacionDesc
        {
            get
            {

                if (_PresentacionDesc != null)
                {
                    return _PresentacionDesc;
                }
                else
                {
                    if (this.objPresentacion != null)
                        return this.objPresentacion.Descripcion;
                    else
                        return "";

                }

            }
            set { _PresentacionDesc = value; }
        }

        public string RegaloDesc
        {
            get { return _RegaloDesc; }
            set { _RegaloDesc = value; }
        }

        public long IdRegaloPresentacion
        {
            get { return _IdRegaloPresentacion; }
            set { _IdRegaloPresentacion = value; }
        }

        public long IdRegaloSeleccionado
        {
            get { return _IdRegaloSeleccionado; }
            set { _IdRegaloSeleccionado = value; }
        }

        public int OrdenInterno
        {
            get
            {

                string Linea = "";
                string Unidad = "";
                if (this.objProducto.Tipo == 'A')
                {
                    if (this.objProducto.objPadre != null && this.objProducto.objPadre.objPadre != null)
                    {
                        Linea = this.objProducto.objPadre.Descripcion.Trim();
                        Unidad = this.objProducto.objPadre.objPadre.Descripcion.Trim();
                        return GetLayOut(Unidad, Linea, this.objPresentacion.Descripcion);
                    }
                    else
                        return 40;
                }
                else
                    return 40;
            }

        }

        public int GetLayOut(string unidada, string Linea, string presentacion)
        {
            SortedList orden = new SortedList();

            orden.Add("Tal Cual Femenino", 1);
            orden.Add("Tal Cual Masculino", 2);

            orden.Add("Sandra Marzzan Exclusiva Femenino", 3);
            orden.Add("Sandra Marzzan Exclusiva Masculino", 4);

            orden.Add("Nuevo Latido Femenino", 5);
            orden.Add("Nuevo Latido Masculino", 6);

            orden.Add("Sandra Marzzan Cosmética Alta Cosmética", 7);
            orden.Add("Sandra Marzzan Cosmética Básica", 8);

            orden.Add("La Casa del Aroma Fresh", 9);
            orden.Add("La Casa del Aroma Aromaterapia", 10);
            orden.Add("La Casa del Aroma Aromas de la Casa", 11);
            orden.Add("La Casa del Aroma Selectivo", 12);
            orden.Add("La Casa del Aroma Aromas Regionales", 13);

            orden.Add("Deditos", 14);

            orden.Add("Inquietos Nenas Inquietas", 19);
            orden.Add("Inquietos Nenes Inquietos", 20);
            orden.Add("Inquietos Cositas Inquietas", 21);

            orden.Add("Spa", 22);

            orden.Add("Bolsos", 27);
            orden.Add("Plumines", 28);

            orden.Add("Herramientas de Venta Folletería y Papelería", 29);
            orden.Add("Herramientas de Venta Muestras", 30);
            orden.Add("Herramientas de Venta Accesorios y Merchandisin", 31);
            orden.Add("Incorporaciones Folletería y Papelería", 32);
            orden.Add("Incorporaciones Incorporaciones", 33);
            orden.Add("Incorporaciones Muestras", 34);

            int ordenEncontrado = 0;
            object pos = null;

            if (presentacion.IndexOf("5 ml") == 0)
            {
                pos = orden["Plumines"];
            }
            else if (presentacion.IndexOf("bolso") >= 0)
            {
                pos = orden["Bolsos"];
            }
            else
            {
                pos = orden[unidada + " " + Linea];
                if (pos == null)
                    pos = orden[unidada];

            }

            if (pos != null)
                ordenEncontrado = int.Parse(pos.ToString());
            else
                ordenEncontrado = 40;

            return ordenEncontrado;

        }

        public long IdDetalleLineaGuardado
        { get; set; }
    }
}
