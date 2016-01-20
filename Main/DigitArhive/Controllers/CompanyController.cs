using DigitArchive.Models;
using DigitArhive.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DigitArhive.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            var model = Company.GetAllCompany();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Company.CreateCompany(model);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company c = Company.GetCompanyById(id);

            if (c == null)
            {
                return HttpNotFound();
            }

            return View(c);
        }



        [HttpPost]
        public ActionResult Edit([Bind(Include = "CompanyId,CompanyName,CompanyIdNumber,CompanyTaxNumber")] Company company)
        {
            if (ModelState.IsValid)
            {
                Company.EditCompany(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }


        // GET: Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = Company.GetCompanyById(id);

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company.DeleteCompany(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company company = Company.GetCompanyById(id);

            GlobalVariables.CompanyName = company.CompanyName;

            company.Binders = Binder.GetAllBindersByCompanyId(id);

            if (company == null)
            {
                return HttpNotFound();
            }

            return View(company);
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



        public ActionResult SearchCompany(string searchTerm)
        {
            Company result = Search.SearchCompany(searchTerm);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchBinder(int companyId, string barCode)
        {
            Binder result = Search.SearchBinder(companyId, barCode);

            Binder binder = new Binder();
            if (result != null)
            {
                binder.BinderId = result.BinderId;
                binder.BarCode = result.BarCode;
                binder.Description = result.Description;
                binder.Year = result.Documents.Count.ToString();
            }
            

            return Json(binder, JsonRequestBehavior.AllowGet);
        }
    }
}