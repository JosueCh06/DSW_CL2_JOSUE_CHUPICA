using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSW_CL2_JOSUE_CHUPICA.Models
{
    public class CarritoItem
    {
        private PRODUCTO _producto;

        public PRODUCTO Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        public CarritoItem() { }

        public CarritoItem(PRODUCTO producto, int cantidad)
        {
            this._producto = producto;
            this._cantidad = cantidad;
        }
    }
}