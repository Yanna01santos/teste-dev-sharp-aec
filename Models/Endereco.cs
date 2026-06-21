using System.ComponentModel.DataAnnotations;

namespace AEC.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(20, ErrorMessage = "O CEP deve ter no máximo 20 caracteres.")]
        public string Cep { get; set; } = string.Empty;

        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(150, ErrorMessage = "O logradouro deve ter no máximo 150 caracteres.")]
        public string Logradouro { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo 100 caracteres.")]
        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres.")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A UF é obrigatória.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "A UF deve ter 2 caracteres.")]
        public string Uf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(20, ErrorMessage = "O número deve ter no máximo 20 caracteres.")]
        public string Numero { get; set; } = string.Empty;

        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }
    }
}