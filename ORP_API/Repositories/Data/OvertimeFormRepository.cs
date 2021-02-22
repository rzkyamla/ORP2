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
        public IConfiguration Configuration { get; }
        public OvertimeFormRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            myContext.Set<OvertimeForm>();
            this.myContext = myContext;
            this.Configuration = configuration;
        }

        public int Apply(OvertimeFormViewModels overtimeFormViewModels)
        {
            TimeSpan difference = overtimeFormViewModels.EndTime - overtimeFormViewModels.StartTime;
            int totalHours = difference.Hours;

            DateTime date = DateTime.UtcNow;
            var overtimeForm = new OvertimeForm()
            {
                Name = overtimeFormViewModels.Name,
                SubmissionDate = date,
                CustomerId = overtimeFormViewModels.CustomerId
            };
            myContext.Add(overtimeForm);
            var resultOvertimeForm = myContext.SaveChanges();

            var detail = new Models.Details()
            {
                StartTime = overtimeFormViewModels.StartTime,
                EndTime = overtimeFormViewModels.EndTime,
                Activity = overtimeFormViewModels.Activity,
                AdditionalSalary = totalHours * 100000,
                OvertimeFormId = overtimeForm.Id
            };

            myContext.Add(detail);
            var resultDetails = myContext.SaveChanges();

            if (resultOvertimeForm > 0 && resultDetails > 0)
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
