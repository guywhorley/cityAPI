using System.Diagnostics;

namespace CityInfo.API.Services
{
	public class CloudMailService : IMailService
    {
		// using appSettings.json file to get values
	    private readonly string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
	    private readonly string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

		public void Send(string subject, string message)
	    {
		    Debug.WriteLine($"Mail [From] \"{_mailFrom}\" [To] \"{_mailTo}\", with cloud mail service");
		    Debug.WriteLine($"[Subject] \"{subject}\"");
		    Debug.WriteLine($"[Message] \"{message}\"");
	    }

	}
}
