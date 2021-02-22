using Microsoft.Extensions.Configuration;
using ORP_API.Context;
using ORP_API.Models;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class OvertimeFormRepository : GeneralRepository<OvertimeForm, MyContext, int>
    {
        private readonly MyContext myContext;
        public OvertimeFormRepository(MyContext myContext) : base(myContext)
        {
            myContext.Set<OvertimeForm>();
            this.myContext = myContext;
        }

        public int Apply(OvertimeFormViewModels overtimeFormViewModels)
        {
            TimeSpan difference = overtimeFormViewModels.EndTime - overtimeFormViewModels.StartTime;
            int totalHours = difference.Hours;

            DateTime date = DateTime.Now;
            var overtimeForm = new OvertimeForm()
            {
                Name = overtimeFormViewModels.Name,
                SubmissionDate = date,
                CustomerId = overtimeFormViewModels.CustomerId
            };
            myContext.Add(overtimeForm);
            var resultOvertimeForm = myContext.SaveChanges();
            var overtimeformemployee = new OvertimeFormEmployee()
            {
                NIK = overtimeFormViewModels.NIK,
                Status = StatusRequest.Waiting,
                CustomerId = overtimeFormViewModels.CustomerId,
                OvertimeFormId = overtimeForm.Id
            };
            myContext.Add(overtimeformemployee);
            var resulthistory = myContext.SaveChanges();

           /* for (int i = 0; i < 3; i++)
            {*/
            var detail = new DetailOvertimeRequest()
            {
                StartTime = overtimeFormViewModels.StartTime,
                EndTime = overtimeFormViewModels.EndTime,
                Act = overtimeFormViewModels.Act,
                AdditionalSalary = totalHours * 100000,
                OvertimeFormId = overtimeForm.Id
            };
                myContext.Add(detail);
            //}
            var resultDetails = myContext.SaveChanges();

            if (resultOvertimeForm > 0 && resultDetails > 0 && resulthistory > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        }
}
