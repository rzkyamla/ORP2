using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.ViewModels
{
    public class RegisterViewModels
    {
        [Key, Required(ErrorMessage = "Tidak boleh kosong"), MinLength(6, ErrorMessage = "Minimal 6 Character"), MaxLength(10, ErrorMessage = "Maksimal 10 Karakter"), RegularExpression(@"^\d+$", ErrorMessage = "Harus berupa angka")]
        public string NIK { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), MaxLength(50, ErrorMessage = "Maksimal 50 karakter"), RegularExpression(@"^\D+$", ErrorMessage = "Tidak boleh berupa angka")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public string BirthDate { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong")]
        public string Religion { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), EmailAddress(ErrorMessage = "Masukan format email yang valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), RegularExpression(@"^08[0-9]{10,12}$", ErrorMessage = "Harus berupa angka diawali 08"), MinLength(10, ErrorMessage = "Minimal 10 karakter"), MaxLength(12, ErrorMessage = "Maksimal 12 karakter")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Password)]
        public int RoleId { get; set; }
        public int CustomerId { get; set; }
        public string Password { get; set; }
    }
}
