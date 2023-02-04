﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BooksMvc.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Имя пользователя не указано")]
        [Display(Name = "Имя пользователя")]
        [MaxLength(255)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Пароль является обязательным")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [MaxLength(255)]
        public string Password { get; set; } = null!;

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string? ReturnUrl { get; set; }
    }

}

