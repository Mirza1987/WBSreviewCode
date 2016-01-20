using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.IO;
using System.IO.Compression;
using DigitArhive.Helpers;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace DigitArchive.Models
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        [Display(Name ="Broj stranice")]
        public int PageNumber { get; set; }
        public string PagePath { get; set; }
        public int DocumentId { get; set; }

        [ForeignKey("DocumentId")]
        public virtual Document Document { get; set; }



        //Metode

        public static void Create(string finalFilePath, int DocumentId, int pageNumber)
        {
            using (var db = new ApplicationDbContext())
            {
                Page page = new Page();
                page.DocumentId = DocumentId;
                page.PageNumber = pageNumber;
                page.PagePath = finalFilePath;

                //try
                //{
                    db.Pages.Add(page);
                    db.SaveChanges();
                //}
                //catch (Exception ex) when (ex is DbUpdateException ||
                //                              ex is DbEntityValidationException ||
                //                              ex is NotSupportedException ||
                //                              ex is ObjectDisposedException ||
                //                              ex is InvalidOperationException)
                //{
                //    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                //}
            }
        }


        //Post: Edit
        public static void EditPage(Page page)
        {
            if (page != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    //try
                    //{
                        db.Entry(page).State = EntityState.Modified;
                        db.SaveChanges();
                //}
                //    catch (Exception ex) when (ex is DbUpdateException ||
                //                             ex is DbEntityValidationException ||
                //                             ex is NotSupportedException ||
                //                             ex is ObjectDisposedException ||
                //                             ex is InvalidOperationException)
                //{
                //    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                //}
            }
            }
        }

        public static Page GetPageById(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Pages.Find(id);
            }
        }

        public static void DeletePage(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                Page page = db.Pages.Find(id);
                FileMover.RemovePage(page.PagePath);
                db.Pages.Remove(page);
                db.SaveChanges();
            }
        }
    }
}