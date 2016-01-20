using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitArchive.Models;
using System.Net;

namespace DigitArhive.Controllers
{
    public class DocumentTypesController : Controller
    {
        // GET: DocumentTypes
        public ActionResult Index()
        {
            var model = DocumentType.GetAllDocumentTypes();
            return View(model);
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DocumentType model)
        {
            if (!ModelState.IsValid)
                return View(model);

            DocumentType.CreateDocumentType(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DocumentType dt = DocumentType.GetDocumentTypeById(id);

            if (dt == null)
            {
                return HttpNotFound();
            }

            return View(dt);
        }



        [HttpPost]
        public ActionResult Edit([Bind(Include = "DocumantTypeId,DocumentTypeName")] DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                DocumentType.EditDocumentType(documentType);
                return RedirectToAction("Index");
            }
            return View(documentType);
        }


        // GET: Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType dt = DocumentType.GetDocumentTypeById(id);

            if (dt == null)
            {
                return HttpNotFound();
            }
            return View(dt);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentType.DeleteDocumentType(id);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            using (var db = new ApplicationDbContext())
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