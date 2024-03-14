using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }

        public int IdCliente { get; set; }

        public int TotalProducto { get; set; }

        public decimal MontoTotal { get; set; }

        public string Contacto { get; set; }

        public String Distrito { get; set; }

        public String Telefono { get; set; }

        public String Direccion { get; set; }

        public String FechaTexto { get; set; }

        public String IdTransaccion { get; set; }

        public List<DetalleVenta> oDetalleVenta { get; set; }





    }
}
