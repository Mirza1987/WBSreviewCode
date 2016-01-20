using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitArchive.Models;

namespace DigitArhive.Helpers
{
    class Search
    {

        public static Company SearchCompany(string companyName)
        {
            using(var db = new ApplicationDbContext())
            {
                Company company = new Company();
                company = db.Companies.Where(x => x.CompanyName==companyName).SingleOrDefault();
                return company;
            }  
        }


        public static Binder SearchBinder(int companyId, string barCode)
        {
            using (var db = new ApplicationDbContext())
            {
                var binder = db.Binders.Where(x => x.BarCode == barCode && x.CompanyId == companyId).SingleOrDefault();
                if (binder != null)
                {
                    List<Document> documents = db.Documents.Where(x => x.BinderId == binder.BinderId).ToList();
                    binder.Documents = documents;
                }
                
                return binder;
            }
        }


        public static List<HelperObject> SearchDocument(int binderId, string documentDescription)
        {
            using (var db = new ApplicationDbContext())
            {
                var documents = db.Documents.Where(x => x.BinderId == binderId && x.DocumentDescription.Contains(documentDescription)).ToList();

                List<HelperObject> isolatedDocumentList = new List<HelperObject>();
                
                foreach (var d in documents)
                {
                    HelperObject isolatedDocument = new HelperObject();
                    isolatedDocument.DocumentId = d.DocumentId;
                    isolatedDocument.Description = d.DocumentDescription;
                    isolatedDocument.DocumentOrigin = d.CompanyFromDocument;
                    isolatedDocument.CreationDate = d.DocumentCreationDate.ToString();

                    isolatedDocumentList.Add(isolatedDocument);
                }

                return isolatedDocumentList;
            }
        }
    }

    class HelperObject
    {
        public int DocumentId { get; set; }
        public string Description { get; set; }
        public string DocumentOrigin { get; set; }
        public string CreationDate { get; set; }
    }
}
