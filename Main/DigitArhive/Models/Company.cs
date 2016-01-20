using DigitArhive.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace DigitArchive.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Naziv firme")]
        public string CompanyName { get; set; }
        [Display(Name = "ID broj firme")]
        [MaxLength(13, ErrorMessage = "Maksimalan broj karaktera kod ID broja firme je 13")]
        [MinLength(13, ErrorMessage = "Minimalan broj karaktera kod ID broja firme je 13")]
        public string CompanyIdNumber { get; set; }
        [Display(Name = "PDV broj firme")]
        [MaxLength(12, ErrorMessage = "Maksimalan broj karaktera kod PDV broja firme je 12")]
        [MinLength(12, ErrorMessage = "Minimalan broj karaktera kod PDV broja firme je 12")]
        public string CompanyTaxNumber { get; set; }
        public bool CompanyStatus { get; set; }

        public ICollection<Binder> Binders { get; set; }

        [ForeignKey("CompanyId")]
        public ICollection<Document> Documents { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }



        //Metode

        public static void CreateCompany(Company company)
        {
            if (company != null)
            {
                using(var db=new ApplicationDbContext())
                {
                    //try
                    //{
                        db.Companies.Add(company);
                        db.SaveChanges();
                    //}
                    //catch(Exception ex) when (ex is DbUpdateException || 
                    //                          ex is DbEntityValidationException || 
                    //                          ex is NotSupportedException || 
                    //                          ex is ObjectDisposedException || 
                    //                          ex is InvalidOperationException)
                    //{
                    //    ErrorHelpers.LogError(ex, ErrorLevel.Error, "Error on write in database");
                    //}                    
                }
            }
        }

        internal static List<Company> GetAllCompany()
        {
            using(var db=new ApplicationDbContext())
            {
                return db.Companies.ToList();
            }
        }




        //GET: Delete/Edit/Details
        internal static Company GetCompanyById(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                var company = db.Companies.Find(id);
                company.Binders = db.Binders.Where(x => x.CompanyId == id).ToList();
                return company;
            }
        }


        //POST: Edit
        public static void EditCompany(Company c)
        {
            using (var db = new ApplicationDbContext())
            {
                //try
                //{
                    db.Entry(c).State = EntityState.Modified;
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
        public static void DeleteCompany(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                //try
                //{
                    Company c = db.Companies.Find(id);
                    db.Companies.Remove(c);
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






        public Company()
        {
            this.Binders = new HashSet<Binder>();
            this.Documents = new HashSet<Document>();
            this.Users = new HashSet<ApplicationUser>();
        }        
    }
}