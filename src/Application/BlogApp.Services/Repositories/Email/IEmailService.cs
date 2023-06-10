namespace BlogApp.Services.Repositories.Email;

public interface IEmailService
{
    Task SendRegisterEmail(string nickname, string email);
    Task SendRegisterEmailWithPassword(string nickname, string email, string password);
}