﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Models
{
    public class LoginClass
    {
        [Required(ErrorMessage = "Please Enter Your Username!")]
        [Display(Name = "Enter Username: ")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Please Enter Your Password!")]
        [Display(Name = "Enter Password: ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}