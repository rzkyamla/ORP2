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
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, MyContext, string>
    {
        private readonly MyContext myContext;
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
                RoleId = registerViewModels.RoleId,
                CustomerId = registerViewModels.CustomerId,
                Password = registerViewModels.Password
            };
            var account = new Account()
            {
                NIK = registerViewModels.NIK,
                Password = registerViewModels.Password
            };

            var resultEmployee = employeeRepository.Create(employee);
            myContext.Add(account);
            var resultAccount = myContext.SaveChanges();

            if (resultEmployee > 0 && resultAccount > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public LoginViewModels Login(string email, string password)
        {
            LoginViewModels result = null;

            string connectStr = Configuration.GetConnectionString("MyConnection");

            using (IDbConnection db = new SqlConnection(connectStr))
            {
                string readSp = "sp_retrieve_login";
                var parameter = new { Email = email, Password = password };
                result = db.Query<LoginViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }
        public int ResetPassword(Account account, string email)
        {
            string resetCode = Guid.NewGuid().ToString();
            var time24 = DateTime.Now.ToString("HH:mm:ss");
            var getuser = myContext.Employee.Where(a => a.Email == email).FirstOrDefault();
            if (getuser == null)
            {
                return 0;
            }
            else
            {
                var password = Hashing.HashPassword(resetCode);
                getuser.Password = password;
                var result = myContext.SaveChanges();

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("1997HelloWorld1997@gmail.com", "wwwsawwwsdwwwszwwwsx");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential nc = new NetworkCredential("1997HelloWorld1997@gmail.com", "wwwsawwwsdwwwszwwwsx");
                smtp.Credentials = nc;
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("1997HelloWorld1997@gmail.com", "Leave Request Reset Password");
                mailMessage.To.Add(new MailAddress(getuser.Email));
                mailMessage.Subject = "Reset Password " + time24;
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = "Hi " + getuser.Name + "\nThis is new password for your account.\n\n " + resetCode + "\nThank You";
                smtp.Send(mailMessage);
                return result;
                /*var data = myContext.Employee.Where(x => x.Email == email).FirstOrDefault();
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
                }*/
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