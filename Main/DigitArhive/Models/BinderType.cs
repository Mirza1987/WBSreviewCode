using DigitArhive.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DigitArchive.Models
{
    public class BinderType
    {
        [Key]
        public int BinderTypeId { get; set; }
        [MaxLength(50, ErrorMessage = "Maksimalan broj karaktera za naziv tipa registratora je 50!")]
        [Display(Name = "Tip registratora")]
        public string BinderTypeName { get; set; }

        public ICollection<BinderTypeBinder> BinderTypeBinders { get; set; }

        public BinderType()
        {
            this.BinderTypeBinders = new HashSet<BinderTypeBinder>();
        }



        //Metode:

        internal static List<BinderType> GetAllBinderTypes()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.BindersTypes.ToList();
            }
        }

        public static void CreateBinderType(BinderType binderType)
        {
            if (binderType != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    //try
                    //{
                        db.BindersTypes.Add(binderType);
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


        //GET: Delete/Edit
        internal static BinderType GetBinderTypeById(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.BindersTypes.Find(id);
            }
        }


        //POST: Edit
        public static void EditBinderType(BinderType bt)
        {
            using (var db = new ApplicationDbContext())
            {
                //try
                //{
                    db.Entry(bt).State = EntityState.Modified;
                    db.SaveChanges();
                //}
                //catch (Exception ex) when (ex is DbUpdateException ||
                //                            ex is DbEntityValidationException ||
                //                            ex is NotSupportedException ||
                //                            ex is ObjectDisposedException ||
                //                            ex is InvalidOperationException)
                //{
                //    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                //}
            }
        }

        //POST: Delete
        public static void DeleteBinderType(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                //try
                //{
                    BinderType bt = db.BindersTypes.Find(id);
                    db.BindersTypes.Remove(bt);
                    db.SaveChanges();
                //}
                //catch (Exception ex) when (ex is DbUpdateException ||
                //                           ex is DbEntityValidationException ||
                //                           ex is NotSupportedException ||
                //                           ex is ObjectDisposedException ||
                //                           ex is InvalidOperationException)
                //{
                //    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                //}
            }
        }

    }
}