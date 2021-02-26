using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.ViewModels
{
    public class RelationalManagerViewModels
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string NIK { get; set; }
        public int Status { get; set; }
        public int OvertimeFormId { get; set; }
    }
}
