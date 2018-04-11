using MyCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCars.ViewModels
{
    public class CreateUserInfoViewModel
    {
        public UserInfo UserInfo { get; set; }
        public int TypeCarId { get; set; }
    }
}