using System.ComponentModel.DataAnnotations;

namespace ProjectForIdentity.ViewModels
{
    public class CreateViewModel
    {
        
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]  
        public string Password { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Şifreleriniz eşleşmemektedir.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
