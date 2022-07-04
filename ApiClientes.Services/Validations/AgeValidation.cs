using System.ComponentModel.DataAnnotations;

namespace ApiClientes.Services.Validations
{
    public class AgeValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value != null) ;

            var dataNascimento = value.ToString();
            if (Convert.ToDateTime(dataNascimento).AddYears(18) < DateTime.Now) ;

            return false;
        }
    }
}