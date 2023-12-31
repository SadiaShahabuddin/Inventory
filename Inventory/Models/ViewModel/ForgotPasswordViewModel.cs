﻿using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
