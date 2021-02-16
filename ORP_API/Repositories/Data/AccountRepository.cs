using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ORP_API.Context;
using ORP_API.Handler;
using ORP_API.Models;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, MyContext, string>
    {
        private readonly MyContext myContext;
        private DbSet<Account> accounts;
        private readonly EmployeeRepository employeeRepository;
        private readonly SendEmail sendEmail = new SendEmail();
        public IConfiguration Configuration { get; }
        public AccountRepository(MyContext myContext, EmployeeRepository employeeRepository, IConfiguration configuration) : base(myContext)
        {
            myContext.Set<Account>();
            this.myContext = myContext;
            this.employeeRepository = employeeRepository;
            this.Configuration = configuration;
        }

        public int Register(RegisterViewModels registerViewModels)
        {
            var employee = new Employee()
            {
                NIK = registerViewModels.NIK,
                Name = registerViewModels.Name,
                BirthDate = registerViewModels.BirthDate,
                Gender = registerViewModels.Gender,
                Religion = registerViewModels.Religion,
                Email = registerViewModels.Email,
                PhoneNumber = registerViewModels.PhoneNumber,
                RoleId = 4,
                CustomerId = registerViewModels.CustomerId
            };
            var account = new Account()
            {
                NIK = registerViewModels.NIK,
                Password = Hashing.HashPassword("B0o7c@mp")
            };

            var resultEmployee = employeeRepository.Create(employee);
            myContext.Add(account);
            var resultAccount = myContext.SaveChanges();

            if (resultEmployee > 0 && resultAccount > 0)
            {
                sendEmail.SendPassword(employee.Email);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public LoginViewModels Login(LoginViewModels loginViewModels)
        {
            LoginViewModels result = null;

            string connectStr = Configuration.GetConnectionString("MyConnection");
            var employeeCondition = myContext.Employee.Where(a => a.Email == loginViewModels.Email).FirstOrDefault();

            if (employeeCondition != null)
            {
                //var accountCondition = myContext.Account.Any(a => a.Password == loginViewModels.Password);

                if (Hashing.ValidatePassword(loginViewModels.Password, employeeCondition.Name))
                {
                    using (IDbConnection db = new SqlConnection(connectStr))
                    {
                        string readSp = "sp_retrieve_login";
                        var parameter = new { Email = loginViewModels.Email, Password = loginViewModels.Password };
                        result = db.Query<LoginViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    }
                }
            }
            return result;
        }
        public int ResetPassword(Account account, string email)
        {
            var data = myContext.Employee.Where(x => x.Email == email).FirstOrDefault();
            if (data == null)
            {
                return 0;
            }
            else
            {
                var accounts = new Account()
                {
                    NIK = account.NIK,
                    Password = Hashing.HashPassword(account.Password)
                };
                myContext.Entry(accounts).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                sendEmail.SendNotification(email);
                return result;
            }
        }
        public int ChangePassword(string NIK, string password)
        {
            Account acc = myContext.Account.Where(a => a.NIK == NIK).FirstOrDefault();
            //Account acc = accounts.Find(NIK);
            acc.Password = password;
            myContext.Entry(acc).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
