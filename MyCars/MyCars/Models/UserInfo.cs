using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string middleName { get; set; }
        public int PhoneNumber { get; set; }
        public string CarNumber { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<TypeModel> TypeModels { get; set; }
        public UserInfo()
        {
            TypeModels = new List<TypeModel>();
        }

        //public ICollection<UserCar> UserCars { get; set; }
        //public UserInfo()
        //{
        //    UserCars = new List<UserCar>();
        //}
    }
}