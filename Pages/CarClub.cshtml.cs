using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ESM.Racing.Pages
{
    public class CarClubModel : PageModel
    {
        [BindProperty]
        public bool PostSuccess { get; set; }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        private readonly SendGridClient _client;
        
        public CarClubModel(IConfiguration config)
        {
            string apiKey = config.GetSection("SendGrid").GetValue<string>("ApiKey");
            _client = new SendGridClient(apiKey);
        }

        public void OnGet()
        {
            PostSuccess = false;
        }

        public void OnPost()
        {
            // SendEmail().Wait();

            PostSuccess = true;
        }

        private async Task SendEmail()
        {
            EmailAddress from = new EmailAddress(EmailAddress, Name);
            string subject = "ESM Car Club Enquiry";
            EmailAddress to = new EmailAddress("test@example.com", "ESM website");
            string plainTextContent = $"This is an automated email from the ESM website. {Name} <{EmailAddress}> would like to get involved.";
            string htmlContent = $"This is an automated email from the ESM website. <strong>{Name}</strong> &lt;{EmailAddress}&gt; would like to get involved.";

            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Response response = await _client.SendEmailAsync(msg);
        }
    }
}
