using System;
using BlogApp.Services.Repositories.Email;
using Hangfire;

namespace BlogApp.Services.Repositories.Schedule
{
	public class ScheduleService
	{
		public static void ScheduleSendRegisterEmail(string email,string name)
		{
			BackgroundJob.Schedule<IEmailService>(x=>x.SendRegisterEmail(name, email) , TimeSpan.FromMinutes(1));
			//İstek geldikten 1 dakika sonra SendRegisterEmail Metotunu çalıştırır.
		}

		public static void ScheduleSendRegisterEmailWithPassword(string email, string name, string password)
		{
            BackgroundJob.Schedule<IEmailService>(x => x.SendRegisterEmailWithPassword(name, email, password), TimeSpan.FromMinutes(1));
        }
    }
}
