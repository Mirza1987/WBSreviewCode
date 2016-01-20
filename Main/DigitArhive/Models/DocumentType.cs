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
    public class DocumentType
    {
        [Key]
        public int DocumantTypeId { get; set; }
        [MaxLength(50, ErrorMessage = "Maksimalan broj karaktera za naziv tipa dokumenta je 50!")]
        [Display(Name = "Tip dokumenta")]
        public string DocumentTypeName { get; set; }
        public ICollection<Document> Documents { get; set; }

        public DocumentType()
        {
            this.Documents = new HashSet<Document>();
        }


        //Metode:

        internal static List<DocumentType> GetAllDocumentTypes(){

            using (var db = new ApplicationDbContext())
            {
                return db.DocumentsTypes.ToList();
            }
        }

        public static void CreateDocumentType(DocumentType docType)
        {
            if (docType != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    //try
                    //{
                        db.DocumentsTypes.Add(docType);
                        db.SaveChanges();
                    //}
                    //catch (Exception ex) when (ex is DbUpdateException ||
                    //                         ex is DbEntityValidationException ||
                    //                         ex is NotSupportedException ||
                    //                         ex is ObjectDisposedException ||
                    //                         ex is InvalidOperationException)
                    //{
                        //ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                    //}
                }
            }
        }


        //GET: Delete/Edit
        internal static DocumentType GetDocumentTypeById(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.DocumentsTypes.Find(id);
            }
        }


        //POST: Edit
        public static void EditDocumentType(DocumentType dt)
        {
            using (var db = new ApplicationDbContext())
            {
                //try
                //{ 
                    db.Entry(dt).State = EntityState.Modified;
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
        public static void DeleteDocumentType(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                //try
                //{
                    DocumentType dt = db.DocumentsTypes.Find(id);
                    db.DocumentsTypes.Remove(dt);
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