using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Models
{
    [Table("tb_m_detail_overtime_request")]
    public class DetailOvertimeRequest
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
        public string Act { get; set; }
        public int AdditionalSalary { get; set; }
        public int OvertimeFormId { get; set; }
        public virtual OvertimeForm OvertimeForm { get; set; }
        public virtual List<OvertimeFormEmployee> OvertimeFormEmployees { get; set; } = new List<OvertimeFormEmployee>();

    }

}
