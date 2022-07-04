using ApiClientes.Services.Validations;
using System.ComponentModel.DataAnnotations;

namespace ApiClientes.Services.Requests
{
    public class ClientePutRequest
    {
        [Required(ErrorMessage = "Por favor, informe o Id do Cliente.")]
        public Guid IdCliente { get; set; }
        [Required(ErrorMessage = "Por favor, informe o nome do Cliente.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Por favor, informe o email do Cliente.")]
        [EmailAddress(ErrorMessage = "Por favor, digite um email válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor, informe o CPF do Cliente.")]
        [MinLength(11, ErrorMessage = "Preencha com pelo menos 11 caracteres.")]
        [MaxLength(14, ErrorMessage = "Preencha com, no máximo, 14 caracteres.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Por favor, informe a data de nascimento do Cliente.")]
        [AgeValidation(ErrorMessage = "O cliente precisa ter ao menos 18 anos.")]
        public DateTime DataNascimento { get; set; }
    }
}
