using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitArchive.Models;
using System.Net;

namespace DigitArhive.Controllers
{
    public class BinderTypesController : Controller
    {
        // GET: BinderTypes
        public ActionResult Index()
        {
            var model = BinderType.GetAllBinderTypes();
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BinderType model)
        {
            if (!ModelState.IsValid)
                return View(model);

            BinderType.CreateBinderType(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BinderType bt = BinderType.GetBinderTypeById(id);

            if (bt == null)
            {
                return HttpNotFound();
            }

            return View(bt);
        }



        [HttpPost]
        public ActionResult Edit([Bind(Include = "BynderTypeId,BynderTypeName")] BinderType binderType)
        {
            if (ModelState.IsValid)
            {
                BinderType.EditBinderType(binderType);
                return RedirectToAction("Index");
            }
            return View(binderType);
        }


        // GET: BinderTypes1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinderType bt = BinderType.GetBinderTypeById(id);

            if (bt == null)
            {
                return HttpNotFound();
            }

            return View(bt);
        }

        // POST: BinderTypes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinderType.DeleteBinderType(id);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            using(var db = new ApplicationDbContext())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }

    }
}