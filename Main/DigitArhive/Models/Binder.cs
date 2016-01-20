using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using DigitArhive.Helpers;
using System.Data.Entity;

namespace DigitArchive.Models
{
    public class Binder
    {
        [Key]
        public int BinderId { get; set; }
        [Display(Name = "Firma")]
        [Required(ErrorMessage = "Obavezno polje")]
        public int CompanyId { get; set; }
        [MaxLength(1000)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Za godinu")]
        public string Year { get; set; }
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Lokacija")]
        public string Location { get; set; }
        public string BarCode { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [Display(Name = "Dokumenti")]
        public ICollection<Document> Documents { get; set; }

        public ICollection<BinderTypeBinder> BinderTypeBinders { get; set; }

        public Binder()
        {
            this.Documents = new HashSet<Document>();
            this.BinderTypeBinders = new HashSet<BinderTypeBinder>();
            this.TimeStamp = DateTime.Now;
        }

        

        internal static List<Binder> GetAllBinders()
        {
            using (var db = new ApplicationDbContext())
            {
                List<Binder> binders = db.Binders.ToList();
                foreach(var binder in binders)
                {
                    var compName = binder.Company.CompanyName;
                    binder.Company.CompanyName = compName;
                    binder.Documents = db.Documents.Where(d => d.BinderId == binder.BinderId).ToList();
                }
                return binders;
            }
        }

        //Get: Details/Edit/Delete
        internal static Binder GetBinderById(int? id)
        {
            using(var db=new ApplicationDbContext())
            {
                var binder = db.Binders.Find(id);
                binder.Documents = db.Documents.Where(x => x.BinderId == id).ToList();
                //var documentType = db.DocumentsTypes.Where(x => x.Documents == binder.Documents);
                var companyName = binder.Company.CompanyName;
                binder.Company.CompanyName = companyName;
                
                return binder;
            }
        }


        public static int CreateBinder(Binder binder)
        {
            if (binder != null)
            {
                using(var db=new ApplicationDbContext())
                {
                    //try
                    //{
                        db.Binders.Add(binder);
                        db.SaveChanges();
                        return binder.BinderId;
                    //}
                    //catch(Exception ex) when(ex is DbUpdateException ||
                    //                         ex is DbEntityValidationException ||
                    //                         ex is NotSupportedException ||
                    //                         ex is ObjectDisposedException ||
                    //                         ex is InvalidOperationException)
                    //{
                    //    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                    //}
                }
            }
            return 0;
        }

        internal static ICollection<Binder> GetAllBindersByCompanyId(int? companyId)
        {
            ICollection<Binder> binders = null;
            using (var db=new ApplicationDbContext())
            {
                binders = db.Binders.Where(c => c.CompanyId == companyId).ToList();

                foreach(var binder in binders)
                {
                    binder.Documents = db.Documents.Where(d => d.BinderId == binder.BinderId).ToList();
                }
            }

            return binders;
        }

        public static void EditBinder(Binder binder)
        {
            if (binder != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    //try
                    //{
                        db.Entry(binder).State = EntityState.Modified;
                        db.SaveChanges();
                    //}
                    //catch (Exception ex) when (ex is DbUpdateException ||
                    //                         ex is DbEntityValidationException ||
                    //                         ex is NotSupportedException ||
                    //                         ex is ObjectDisposedException ||
                    //                         ex is InvalidOperationException)
                    //{
                    //    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                    //}
                }
            }
        }


        public static void DeleteBinder(int id)
        {
            using(var db=new ApplicationDbContext())
            {
                db.Binders.Remove(db.Binders.Find(id));
                db.SaveChanges();
            }
        }

    }
}