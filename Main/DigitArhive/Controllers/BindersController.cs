using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitArchive.Models;
using System.Net;
using DigitArhive.Models;
using DigitArhive.Helpers;
using Newtonsoft.Json;

namespace DigitArhive.Controllers
{
    public class BindersController : Controller
    {
        //GET: Binders
        public ActionResult Index()
        {
            var model = Binder.GetAllBinders();
            return View(model);
        }


        [HttpGet]
        public ActionResult Create(int id)
        {
            IEnumerable<Company> companies = Company.GetAllCompany();
            IEnumerable<BinderType> bindersType = BinderType.GetAllBinderTypes();

            Company company = Company.GetCompanyById(id);
            if (company == null)
            {
                return HttpNotFound();
            }

            var model = new BinderViewModel();

            model.CompanyName = company.CompanyName;
            model.CompanyId = id;
            model.BinderTypes = new List<BinderTypeViewModel>();

            foreach (var binderType in bindersType)
            {
                var binderTypeViewModel = new BinderTypeViewModel
                {
                    BinderTypeId = binderType.BinderTypeId,
                    BinderTypeName = binderType.BinderTypeName,
                    IsSelected = false
                };
                model.BinderTypes.Add(binderTypeViewModel);
            }


            return View(model);
        }


        [HttpPost]
        public ActionResult Create(BinderViewModel model)
        {
            if (ModelState.IsValid)
            {
                Binder binder = new Binder
                {
                    BarCode = model.BarCode,
                    CompanyId = model.CompanyId,
                    Description = model.Description,
                    Location = model.Location,
                    Year = model.Year
                };


                int binderId = Binder.CreateBinder(binder);

                if (binderId < 0)
                {
                    BinderTypeBinder.InsertBinderTypeBinder(binderId, model.BinderTypes);
                }

                return RedirectToAction("Details", "Company", new { id= model.CompanyId});
            }

            IEnumerable<Company> companies = Company.GetAllCompany();

            ViewBag.CompanyId = new SelectList(companies, "CompanyId", "CompanyName", model.CompanyId);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Binder binder = Binder.GetBinderById(id);
            var model = new BinderViewModel
            {
                BarCode = binder.BarCode,
                BinderId = binder.BinderId,
                CompanyId = binder.CompanyId,
                CompanyName = GlobalVariables.CompanyName,
                Description = binder.Description,
                Location = binder.Location,
                Year = binder.Year
            };

            var binderTypes = BinderType.GetAllBinderTypes();
            var binderTypeBinders = BinderTypeBinder.GetBinderTypeBindersById(id);

            foreach (var binderType in binderTypes)
            {
                BinderTypeViewModel btvm = new BinderTypeViewModel
                {
                    BinderTypeId = binderType.BinderTypeId,
                    BinderTypeName = binderType.BinderTypeName,
                };
                foreach (var binderTypeBinder in binderTypeBinders)
                {
                    
                    if (binderType.BinderTypeId == binderTypeBinder.BynderTypeId)
                    {
                        btvm.IsSelected = binderTypeBinder.IsType;
                    }                    
                }
                model.BinderTypes.Add(btvm);
            }
            ViewBag.CompanyName = GlobalVariables.CompanyName;
            if (binder == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(BinderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var binder = new Binder
                {                    
                    BarCode = model.BarCode,
                    Description = model.Description,
                    Location = model.Location,
                    Year = model.Year,
                    BinderId = model.BinderId                    
                };                

                Binder.EditBinder(binder);
                BinderTypeBinder.DeleteBinderTypeBindersById(binder.BinderId);
                BinderTypeBinder.InsertBinderTypeBinder(model.BinderId, model.BinderTypes);

                return RedirectToAction("Details", "Company", new { id = model.CompanyId });
            }

            IEnumerable<Company> companies = Company.GetAllCompany();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Binder b = Binder.GetBinderById(id);

            if (b == null)
            {
                return HttpNotFound();
            }

            return View(b);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Binder binder = Binder.GetBinderById(id);
            if (binder == null)
            {
                return HttpNotFound();
            }
            return View(binder);
        }


        [HttpPost]
        public ActionResult DeleteConfirmed(int? id)
        {
            var binder = Binder.GetBinderById(id);
            var companyId = binder.CompanyId;
            Binder.DeleteBinder(binder.BinderId);
            return RedirectToAction("Details", "Company", new { id = companyId });
            //return RedirectToAction("Index");
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

        public JsonResult SearchDocument(int binderId, string description)
        {
            var documents = Search.SearchDocument(binderId, description);
            return Json(documents, JsonRequestBehavior.AllowGet);
        }

    }
}


