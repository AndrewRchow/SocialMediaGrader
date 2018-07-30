using Memeni.Models.Domain;
using Memeni.Models.Requests;
using System.Threading.Tasks;

namespace Memeni.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendContactEmail(EmailMessageAddRequest model);
        EmailCode SelectById(int id);
        Task SendEmailConfirmation(string email, string guid);
        void DeleteById(int id);
        Task ForgotPwEmail(string email, string code);
        int Insert(EmailMessageAddRequest model);
        Task ScheduleErrorEmail();
        Task ScheduleReportEmail(ReportAddRequest model);
    }
}