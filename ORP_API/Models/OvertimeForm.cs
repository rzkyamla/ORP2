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
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime SubmissionDate { get; set; }
        public virtual List<DetailOvertimeRequest> DetailOvertimeReq { get; set; }
        public virtual List<OvertimeFormEmployee> OvertimeFormEmployees { get; set; } = new List<OvertimeFormEmployee>();
    }
}
