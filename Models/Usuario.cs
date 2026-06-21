using System.ComponentModel.DataAnnotations;

namespace AEC.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O usuário é obrigatório.")]
        public string UsuarioLogin { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; } = string.Empty;

        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}