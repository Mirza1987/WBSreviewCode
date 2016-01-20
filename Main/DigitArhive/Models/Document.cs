using DigitArhive.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DigitArchive.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        [MaxLength(1000)]
        [Display(Name = "Opis dokumenta")]
        public string DocumentDescription { get; set; }
        [Display(Name = "Datum")]
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Datum kreiranja dokumenta")]
        public DateTime DocumentCreationDate { get; set; }
        [Display(Name = "Porijeklo dokumenta")]
        public string CompanyFromDocument { get; set; }
        [Display(Name = "Tip dokumenta")]
        [Required(ErrorMessage ="Obavezno polje")]
        public int DocumentTypeId { get; set; }
        [Display(Name = "Firma")]
        [Required(ErrorMessage = "Obavezno polje")]
        public int CompanyId { get; set; }
        [Display(Name = "Registrator")]
        [Required(ErrorMessage = "Obavezno polje")]
        public int BinderId { get; set; }
        
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [ForeignKey("DocumentTypeId")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey("BinderId")]
        [Display(Name = "Registrator")]
        public virtual Binder Binder { get; set; }

        public ICollection<Page> Pages { get; set; }

        public Document()
        {
            this.TimeStamp = DateTime.Now;
            this.Pages = new HashSet<Page>();
        }




        //Metode:

        internal static List<Document> GetAllDocuments()
        {
            using (var db = new ApplicationDbContext())
            {
                List<Document> documents = db.Documents.ToList();
                foreach(var document in documents)
                {
                    var binderDesc = document.Binder.Description;
                    var compName = document.Company.CompanyName;
                    var docTypeName = document.DocumentType.DocumentTypeName;

                    document.Binder.Description = binderDesc;
                    document.Company.CompanyName = compName;
                    document.DocumentType.DocumentTypeName = docTypeName;
                }

                return documents;
            }
        }


        public static void CreateDocument(Document d)
        {

            using(var db=new ApplicationDbContext())
            {
                //try
                //{
                    db.Documents.Add(d);
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

        //Get Delete/Edit
        internal static Document GetDocumentById(int? id)
        {
            using (var db= new ApplicationDbContext())
            {
                Document document = db.Documents.Find(id);

                //ovo treba zbog Get:Delete View-a
                var desc = document.Binder.Description;
                var compName = document.Company.CompanyName;
                var docTypeName = document.DocumentType.DocumentTypeName;

                document.DocumentType.DocumentTypeName = docTypeName;
                document.Company.CompanyName = compName;
                document.Binder.Description = desc;

                //stranice u dokumentu
                //document.Pages = db.Pages.Where(x => x.DocumentId == id && x.Document.DocumentDescription == documentDescription).ToList();

                return document;
               
            }
        }


        public static void EditDocument(Document d)
        {
            using(var db=new ApplicationDbContext())
            {
                //try
                //{
                    db.Entry(d).State = EntityState.Modified;
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





        public static void  DeleteDocument(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                //try
                //{
                    Document d = db.Documents.Find(id);
                    db.Documents.Remove(d);
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


        public static List<Page> GetPages(int? id, string documentDescription)
        {
            List<Page> pagesInDocument;
            using (var db = new ApplicationDbContext())
            {
                pagesInDocument = db.Pages.Where(x => x.DocumentId == id && x.Document.DocumentDescription == documentDescription).ToList();

            }
            return pagesInDocument;
        }

       

    }
}