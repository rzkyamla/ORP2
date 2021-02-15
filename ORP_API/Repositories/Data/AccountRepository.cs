using ORP_API.Context;
using ORP_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, MyContext, string>
    {
        public AccountRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
