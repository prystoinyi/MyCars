using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyCars.Models
{
    public class TypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

        public virtual ICollection<UserInfo> UsersInfo { get; set; }
        public TypeModel()
        {
            UsersInfo = new List<UserInfo>();
        }
    }
}