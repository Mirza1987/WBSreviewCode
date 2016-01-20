using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DigitArchive.Models;

namespace DigitArhive.Controllers
{
    public class PagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pages
        //public ActionResult Index()
        //{
        //    var pages = db.Pages.Include(p => p.Document);
        //    return View(pages.ToList());
        //}

        //// GET: Pages/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Page page = db.Pages.Find(id);
        //    if (page == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(page);
        //}

        //// GET: Pages/Create
        //public ActionResult Create()
        //{
        //    ViewBag.DocumentId = new SelectList(db.Documents, "DocumentId", "DocumentDescription");
        //    return View();
        //}

        //// POST: Pages/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PageId,PageNumber,PagePath,DocumentId")] Page page)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Pages.Add(page);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.DocumentId = new SelectList(db.Documents, "DocumentId", "DocumentDescription", page.DocumentId);
        //    return View(page);
        //}

        // GET: Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = Page.GetPageById(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentId = new SelectList(db.Documents, "DocumentId", "DocumentDescription", page.DocumentId);
            return View(page);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageId,PageNumber,PagePath,DocumentId")] Page page)
        {
            if (ModelState.IsValid)
            {
                Page.EditPage(page);
                return RedirectToAction("Details", "Documents", new { id = page.DocumentId });
            }
            ViewBag.DocumentId = new SelectList(db.Documents, "DocumentId", "DocumentDescription", page.DocumentId);

            return View(page);
        }

        // GET: Pages/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Page page = Page.GetPageById(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = Page.GetPageById(id);

            Page.DeletePage(id);
            return RedirectToAction("Details","Documents", new { id = page.DocumentId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
