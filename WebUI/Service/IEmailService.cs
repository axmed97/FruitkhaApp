namespace WebUI.Service
{
    public interface IEmailService
    {
        void EmailSender(string email, string body, string subject);
    }
}
