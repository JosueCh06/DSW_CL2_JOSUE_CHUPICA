using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Data.Entity;
using DSW_CL2_JOSUE_CHUPICA.Models;

namespace DSW_CL2_JOSUE_CHUPICA.Controllers
{
    public class VendedorController : Controller
    {
        // GET: Vendedor
        FUENTESODAEntities fs = new FUENTESODAEntities();
        public ActionResult Index()
        {
            var listaVendedor = fs.VENDEDOR.ToList();
            var listDistrito = fs.DISTRITO.ToList();
            foreach (var item in listaVendedor)
            {
                var distrito = listDistrito.Find(x => x.IDE_DIS == item.IDE_DIS);

                if (distrito != null)
                {
                    item.DISTRITO = distrito;
                }
            }
            return View(listaVendedor);
        }

        public ActionResult Create()
        {
            List<DISTRITO> distritos = new List<DISTRITO>();
            distritos = fs.DISTRITO.ToList();
            ViewBag.distrito = distritos;

            return View(new VENDEDOR());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VENDEDOR v)
        {
            if (ModelState.IsValid)
            {
                fs.VENDEDOR.Add(v);
                fs.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(v);
        }

        public ActionResult Edit(int? id)
        {
            List<DISTRITO> distrito = new List<DISTRITO>();
            distrito = fs.DISTRITO.ToList();
            ViewBag.distrito = distrito;

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            VENDEDOR vendedor = fs.VENDEDOR.Find(id);

            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VENDEDOR reg)
        {
            if (ModelState.IsValid)
            {
                fs.Entry(reg).State = EntityState.Modified;
                fs.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reg);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VENDEDOR ven = fs.VENDEDOR.Find(id);

            if (ven == null)
            {
                return HttpNotFound();
            }
            return View(ven);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VENDEDOR vendedor = fs.VENDEDOR.Find(id);

            if (vendedor == null)
            {
                return HttpNotFound();
            }
            return View(vendedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            VENDEDOR vendedor = fs.VENDEDOR.Find(id);
            fs.VENDEDOR.Remove(vendedor);
            fs.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}