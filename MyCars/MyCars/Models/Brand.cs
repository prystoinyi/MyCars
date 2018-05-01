using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TypeModel> TypeModels { get; set; }
        public Brand()
        {
            TypeModels = new List<TypeModel>();
        }
    }
}