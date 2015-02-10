using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerWPF.Entidades.VentasEntidades
{
    public class PagosDetalleDto : BindableBase
    {
        int idPagoDetalle;
        public int IdPagoDetalle 
        {
            get { return idPagoDetalle; }
            set { SetProperty(ref this.idPagoDetalle, value); }
        }

        int idPago;
        public int IdPago {
            get { return idPago; }
            set { SetProperty(ref this.idPago, value); }
        }

        int idFormaPago;
        public int IdFormaPago {
            get { return idFormaPago; }
            set { SetProperty(ref this.idFormaPago, value); }
        }

        string institucion;
        public string Institucion {
            get { return institucion; }
            set { SetProperty(ref this.institucion, value); }
        }

        string referencia;
        public string Referencia {
            get { return referencia; }
            set { SetProperty(ref this.referencia, value); }
        }

        double cantidad;
        public double Cantidad {
            get { return cantidad; }
            set { SetProperty(ref this.cantidad, value); }
        }

        Nullable<System.DateTime> fechaAlta;
        public Nullable<System.DateTime> FechaAlta {
            get { return fechaAlta; }
            set { SetProperty(ref this.fechaAlta, value); }
        }

        Nullable<System.DateTime> fechaModificacion;
        public Nullable<System.DateTime> FechaModificacion {
            get { return fechaModificacion; }
            set { SetProperty(ref this.fechaModificacion, value); }
        }

        public Nullable<int> estatus;
        public Nullable<int> Estatus {
            get { return estatus; }
            set { SetProperty(ref this.estatus, value); }
        }

        public Nullable<int> idUsuarioCreacion;
        public Nullable<int> IdUsuarioCreacion {
            get { return idUsuarioCreacion; }
            set { SetProperty(ref this.idUsuarioCreacion, value); }
        }

        public Nullable<int> idUsuarioModificacion;
        public Nullable<int> IdUsuarioModificacion {
            get { return idUsuarioModificacion; }
            set { SetProperty(ref this.idUsuarioModificacion, value); }
        }
    }
}
