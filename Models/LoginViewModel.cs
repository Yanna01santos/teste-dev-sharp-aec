using System.ComponentModel.DataAnnotations;

namespace AEC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o usuário.")]
        public string Usuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a senha.")]
        public string Senha { get; set; } = string.Empty;
    }
}