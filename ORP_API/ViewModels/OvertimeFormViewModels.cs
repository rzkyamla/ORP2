using ORP_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.ViewModels
{
    public class OvertimeFormViewModels
    {
        //[Required(ErrorMessage = "Data tidak boleh kosong"), MaxLength(50, ErrorMessage = "Maksimal 50 Karakter")]
        public int Amount { get; set; }
        public int Id { get; set; }
        public string NIK { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public List<DetailsOvertimeRequest> listdetails { get; set; }
    }
    public class DetailsOvertimeRequest
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Act { get; set; }
    }

}
