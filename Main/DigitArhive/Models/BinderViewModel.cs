using DigitArchive.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitArchive.Models
{
    public class BinderViewModel
    {
        [Key]
        public int BinderId { get; set; }        
        [Required(ErrorMessage = "Obavezno polje")]
        public int CompanyId { get; set; }
        [Display(Name = "Firma")]
        public string CompanyName { get; set; }
        [MaxLength(1000)]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Za godinu")]
        public string Year { get; set; }
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Lokacija")]
        public string Location { get; set; }
        public string BarCode { get; set; }


        [Display(Name = "Tipovi registratora")]
        public List<BinderTypeViewModel> BinderTypes { get; set; }

        public BinderViewModel()
        {
            BinderTypes = new List<Models.BinderTypeViewModel>();
        }

    }

    public class BinderTypeViewModel
    {
        public int BinderTypeId { get; set; }

        public string BinderTypeName { get; set; }

        public bool IsSelected { get; set; }
    }
}