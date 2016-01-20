using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigitArchive.Models
{
    public class BinderTypeBinder
    {
        [Key]
        public int BinderTypeBinderId { get; set; }
        public int BynderTypeId { get; set; }
        public int BinderId { get; set; }        

        public virtual Binder Binder { get; set; }
        public virtual BinderType BinderType { get; set; }

        public bool IsType { get; set; }

        public static void InsertBinderTypeBinder()
        {
            
        }

        internal static void InsertBinderTypeBinder(int binderId, List<BinderTypeViewModel> binderTypes)
        {
            using (var db = new ApplicationDbContext())
            {
                foreach(var binderType in binderTypes)
                {
                    BinderTypeBinder binderTypeBinder = new BinderTypeBinder
                    {
                        BinderId = binderId,
                        BynderTypeId = binderType.BinderTypeId,
                        IsType = binderType.IsSelected
                    };
                    db.BinderTypeBinders.Add(binderTypeBinder);                    
                }
                db.SaveChanges();
            }
        }

        internal static List<BinderTypeBinder> GetBinderTypeBindersById(int? id)
        {
            using(var db=new ApplicationDbContext())
            {
                return db.BinderTypeBinders.Where(bt => bt.BinderId == id).ToList();
            }
        }

        internal static void DeleteBinderTypeBindersById(int binderId)
        {
            using(var db=new ApplicationDbContext())
            {
                IEnumerable<BinderTypeBinder> binderTypeBinders = db.BinderTypeBinders.Where(b=>b.BinderId==binderId).ToList();

                db.BinderTypeBinders.RemoveRange(binderTypeBinders);
                db.SaveChanges();                
            }
        }
    }
}