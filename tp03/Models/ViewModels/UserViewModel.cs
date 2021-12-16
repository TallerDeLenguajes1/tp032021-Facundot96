using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public int Type { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(200, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [EmailAddress(ErrorMessage = "El campo no coincide con un email válido")]
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserViewModel> Users { get; set; }
        public UserViewModel() { }
    }

    public class ApproveUserViewModel
    {
        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(100, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(10, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [EmailAddress(ErrorMessage = "El campo no coincide con un email válido")]
        public string Email { get; set; }
        public ApproveUserViewModel() { }
    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(200, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(10, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [EmailAddress(ErrorMessage = "El campo no coincide con un email válido")]
        public string Email { get; set; }
        public EditUserViewModel() { }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(200, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(10, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Password { get; set; }

        public LoginViewModel() { }
    }
}
