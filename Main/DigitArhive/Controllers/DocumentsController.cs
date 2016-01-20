using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitArchive.Models;
using System.IO;
using DigitArhive.Helpers;


namespace DigitArhive.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
            var documents = Document.GetAllDocuments();
            return View(documents);
        }

        // GET: Documents/Create
        public ActionResult Create(int id)
        {
            Binder binder = Binder.GetBinderById(id);
            IEnumerable<Company> companies = Company.GetAllCompany();
            IEnumerable<DocumentType> docType = DocumentType.GetAllDocumentTypes();

            ViewBag.BinderId = id;
            ViewBag.CompanyId = binder.CompanyId;
            ViewBag.DocumentTypeId = new SelectList(docType, "DocumantTypeId", "DocumentTypeName");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentId,DocumentDescription,TimeStamp,DocumentCreationDate,CompanyFromDocument,DocumentTypeId,CompanyId,BinderId,FileUpload")] Document document)
        {
            var files = Request.Files;

            var documentDescription = document.DocumentDescription;
            var company = Company.GetCompanyById(document.CompanyId);
            var companyName = company.CompanyName;
            var documentType = DocumentType.GetDocumentTypeById(document.DocumentTypeId);
            var documentTyeName = documentType.DocumentTypeName;


            if (ModelState.IsValid)
            {
                document.TimeStamp = DateTime.Now;
                Document.CreateDocument(document);

                int documentId = document.DocumentId;

                FileMover.MoveFile(files, documentId, companyName, documentTyeName, documentDescription);

                return RedirectToAction("Details", "Binders", new { id = document.BinderId });
            }

            IEnumerable<Binder> binders = Binder.GetAllBinders();
            IEnumerable<Company> companies = Company.GetAllCompany();
            IEnumerable<DocumentType> docType = DocumentType.GetAllDocumentTypes();

            ViewBag.BinderId = new SelectList(binders, "BinderId", "Description", document.BinderId);
            ViewBag.CompanyId = new SelectList(companies, "CompanyId", "CompanyName", document.CompanyId);
            ViewBag.DocumentTypeId = new SelectList(docType, "DocumantTypeId", "DocumentTypeName", document.DocumentTypeId);

            return View(document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = Document.GetDocumentById(id);
            if (document == null)
            {
                return HttpNotFound();
            }

            IEnumerable<Binder> binders = Binder.GetAllBinders();
            IEnumerable<Company> companies = Company.GetAllCompany();
            IEnumerable<DocumentType> docType = DocumentType.GetAllDocumentTypes();

            ViewBag.BinderId = new SelectList(binders, "BinderId", "Description", document.BinderId);
            ViewBag.CompanyId = new SelectList(companies, "CompanyId", "CompanyName", document.CompanyId);
            ViewBag.DocumentTypeId = new SelectList(docType, "DocumantTypeId", "DocumentTypeName", document.DocumentTypeId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentId,DocumentDescription,TimeStamp,DocumentCreationDate,CompanyFromDocument,DocumentTypeId,CompanyId,BinderId")] Document document)
        {

            var files = Request.Files;

            if (ModelState.IsValid)
            {
                Document.EditDocument(document);
                return RedirectToAction("Details", "Binders", new { id = document.BinderId });
            }
            ViewBag.BinderId = new SelectList(db.Binders, "BinderId", "Description", document.BinderId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", document.CompanyId);
            ViewBag.DocumentTypeId = new SelectList(db.DocumentsTypes, "DocumantTypeId", "DocumentTypeName", document.DocumentTypeId);
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = Document.GetDocumentById(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var documnet = Document.GetDocumentById(id);
            Document.DeleteDocument(id);
            return RedirectToAction("Details", "Binders", new { id = documnet.BinderId });
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = Document.GetDocumentById(id);
            var documentDescription = document.DocumentDescription;
            document.Pages = Document.GetPages(id, documentDescription);

            if (document == null)
            {
                return HttpNotFound();
            }

            return View(document);
        }

        public ActionResult GetPdf(int id)
        {

            Page page = Page.GetPageById(id);
            var path = page.PagePath;

            var fileStream = new FileStream(path,FileMode.Open,FileAccess.Read);
            var fsResult = new FileStreamResult(fileStream, "application/pdf");
            return fsResult;
        }
    }
}
