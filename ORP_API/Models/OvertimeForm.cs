using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Models
{
    [Table("tb_m_overtime_form")]
    public class OvertimeForm
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), MaxLength(50, ErrorMessage = "Maksimal 50 Karakter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime SubmissionDate { get; set; }
        public int CustomerId { get; set; }
        public virtual List<Details> Details { get; set; }
        public virtual List<OvertimeFormEmployee> OvertimeFormEmployees { get; set; } = new List<OvertimeFormEmployee>();
    }
    
    public class Details
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Data tidak boleh kosong"), DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), MaxLength(100, ErrorMessage = "Maksimal 100 Karakter")]
        public string Activity { get; set; }
        public int AdditionalSalary { get; set; }
        public int OvertimeFormId { get; set; }
    }
}
