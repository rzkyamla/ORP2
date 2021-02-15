using ORP_API.Context;
using ORP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class OvertimeFormEmployeeRepository : GeneralRepository<OvertimeFormEmployee, MyContext, int>
    {
        public OvertimeFormEmployeeRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
