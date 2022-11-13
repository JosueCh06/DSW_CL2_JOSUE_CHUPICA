using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DSW_CL2_JOSUE_CHUPICA.Models;
using System.Web.Helpers;
using System.Data.Entity;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace DSW_CL2_JOSUE_CHUPICA.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        FUENTESODAEntities fs = new FUENTESODAEntities();
        public ActionResult Index()
        {
            var listaProducto = fs.PRODUCTO.ToList();
            var listaCategoria = fs.CATEGORIA.ToList();
            foreach (var item in listaProducto)
            {
                var categoria = listaCategoria.Find(x => x.IDE_CAT == item.IDE_PRO);

                if (categoria != null)
                {
                    item.CATEGORIA = categoria;
                }
            }
            return View(listaProducto);
        }

        public ActionResult Create()
        {
            List<CATEGORIA> categoria = new List<CATEGORIA>();
            categoria = fs.CATEGORIA.ToList();
            ViewBag.categorias = categoria;

            return View(new PRODUCTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PRODUCTO p)
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                ModelState.AddModelError("fotoproducto", "¡Seleciona una Imagen!");
                return View(p);
            }
            else
            {
                if (file.FileName.EndsWith(".jpg"))
                {
                    WebImage image = new WebImage(file.InputStream);
                    p.IMG_PRO = image.GetBytes();

                }
                else
                {
                    ModelState.AddModelError("fotoproducto", "Formato JPG");
                    return View(p);
                }
            }

            fs.PRODUCTO.Add(p);
            fs.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult getImage(int id)
        {
            PRODUCTO p = fs.PRODUCTO.Find(id);
            byte[] byteImage = p.IMG_PRO;

            MemoryStream memoria = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoria);

            memoria = new MemoryStream();
            image.Save(memoria, ImageFormat.Jpeg);
            memoria.Position = 0;

            return File(memoria, "image/jpg");
        }
    }
}