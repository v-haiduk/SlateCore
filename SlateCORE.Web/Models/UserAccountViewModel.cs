using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SlateCORE.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SlateCORE.Web.Models
{
    public class UserAccountViewModel : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}