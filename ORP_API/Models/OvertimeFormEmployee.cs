using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Models
{
    [Table("tb_m_overtime_form_employee")]
    public class OvertimeFormEmployee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public StatusRequest Status { get; set; }
        public int CustomerId { get; set; }
        public string NIK { get; set; }
        [ForeignKey("NIK")]
        public virtual Employee Employee { get; set; }
        public int OvertimeFormId { get; set; }
        [ForeignKey("OvertimeFormId")]
        public virtual OvertimeForm OvertimeForm { get; set; }
    }
    public enum StatusRequest
    {
        [Display(Name = "Waiting For Approval")]
        Waiting = 0,
        [Display(Name = "Request Approved By Supervisor")]
        ApproveBySupervisor = 1,
        [Display(Name = "Request Approved By Relational Manager")]
        ApproveByRelatonalManager = 2,
        [Display(Name = "Request Rejected")]
        Reject = 3
    }
}
