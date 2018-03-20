namespace BlissRecruitment.BusinessLogic.Abstract
{
    using System.Threading.Tasks;

    public interface IEmailProvider
    {
        Task<bool> SendMailAsync(string from, string fromDescription, string to, string toDescription, string subject, string body);
    }
}
