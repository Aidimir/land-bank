using System;
using System.ComponentModel.DataAnnotations;
using Dal.Constants;
using Dal.Models;

namespace Api.Controllers.Authorization.DTO.RequestModels
{
    public class RegisterRequest
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required]
        //[EnumDataType(typeof(List<Roles>), ErrorMessage = "Недопустимое значение для роли")]
        [Display(Name = "Роли")]
        [MinLength(1)]
        public List<string> Roles { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string Fullname { get; set; }
    }
}

