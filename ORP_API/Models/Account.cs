using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Models
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key, Required(ErrorMessage = "Tidak boleh kosong"), MinLength(6, ErrorMessage = "Minimal 6 Character"), MaxLength(10, ErrorMessage = "Maksimal 10 Karakter"), RegularExpression(@"^\d+$", ErrorMessage = "Harus berupa angka")]
        public string NIK { get; set; }
        [Required(ErrorMessage = "Tidak boleh kosong"), DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
