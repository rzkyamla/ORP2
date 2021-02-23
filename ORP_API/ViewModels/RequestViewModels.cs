using ORP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.ViewModels
{
    public class RequestViewModels
    {
        public StatusRequest Status { get; set; }
        public int CustomerId { get; set; }
        public string NIK { get; set; }
        public string Email { get; set; }
        public int OvertimeFormId { get; set; }
        public int RoleId { get; set; }
    }
}
