using System.ComponentModel.DataAnnotations;

namespace BlazorPeliculas.Validaciones
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, 
                    ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success!;
            }

            var valueString = value.ToString()!;
            var primeraLetra = valueString[0].ToString();

            if (primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayúscula", 
                        [validationContext.MemberName!]);
            }

            return ValidationResult.Success!;
        }
    }
}
