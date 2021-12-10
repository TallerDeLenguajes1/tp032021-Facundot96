using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public int UserType { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(200, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [EmailAddress(ErrorMessage = "El campo no coincide con un email válido")]
        public string Email { get; set; }
        public string Password { get; set; }
        public List<UserViewModel> Usuarios { get; set; }
        public UserViewModel()
        {
        }
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

    public class ApprovedUserViewModel
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
        public ApprovedUserViewModel()
        {
        }
    }

    public class EditUserViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(200, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Namee { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [StringLength(10, ErrorMessage = "El campo debe tener como máximo {0}")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [EmailAddress(ErrorMessage = "El campo no coincide con un email válido")]
        public string Email { get; set; }
        public EditUserViewModel()
        {
        }
    }
}
