using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DSW_CL2_JOSUE_CHUPICA.Models;

namespace DSW_CL2_JOSUE_CHUPICA.Controllers
{
    public class CarritoController : Controller
    {
        // GET: Carrito
        FUENTESODAEntities fs = new FUENTESODAEntities();
        public int getIndex(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            for (int i = 0; i < compras.Count; i++)
            {
                if (compras[i].Producto.IDE_PRO == id)
                    return i;
            }
            return -1;
        }

        public ActionResult AgregarCarrito(int id)
        {
            if (Session["carrito"] == null)
            {
                List<CarritoItem> compras = new List<CarritoItem>();
                compras.Add(new CarritoItem(fs.PRODUCTO.Find(id), 1));
                Session["carrito"] = compras;
            }
            else
            {
                List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
                int Index = getIndex(id);
                if (Index == -1)
                    compras.Add(new CarritoItem(fs.PRODUCTO.Find(id), 1));
                else
                    compras[Index].Cantidad++;
                Session["carrito"] = compras;
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            compras.RemoveAt(getIndex(id));
            return View("AgregarCarrito");
        }

        public ActionResult FinalizarCompra()
        {
            List<CarritoItem> compras = (List<CarritoItem>)Session["carrito"];
            if (compras != null && compras.Count > 0)
            {
                BOLETA newVenta = new BOLETA();
                newVenta.FEC_BOL = DateTime.Now;
                newVenta.SUBTOTAL = compras.Sum(x => x.Producto.PRE_PRO * x.Cantidad);
                newVenta.IVA = newVenta.SUBTOTAL * 0.18;
                newVenta.TOTAL = newVenta.SUBTOTAL + newVenta.IVA;

                //cabecera
                newVenta.DETALLEBOLETA = (from Producto in compras
                                          select new DETALLEBOLETA
                                          {
                                              IDE_PRO = Producto.Producto.IDE_PRO,
                                              CAN_ART = Producto.Cantidad,
                                              TOTAL = Producto.Cantidad * Producto.Producto.PRE_PRO

                                          }).ToList();
                fs.BOLETA.Add(newVenta);
                fs.SaveChanges();
                Session["carrito"] = new List<CarritoItem>();
            }
            return View();
        }

        public ActionResult ListarVentas() 
        {
            return View(fs.BOLETA.ToList());
        }
    }
}