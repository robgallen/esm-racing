using System.Threading.Tasks;
using JustGivingSDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ESM.Racing.Pages.Components
{
    public class JustGivingViewComponent : ViewComponent
    {
        private readonly JustGivingApiClient _client;
        private readonly string _shortPageName;

        public JustGivingViewComponent(IConfiguration config)
        {
            string appId = config.GetSection("JustGiving").GetValue<string>("AppId");
            _shortPageName = config.GetSection("JustGiving").GetValue<string>("Page");
            _client = new JustGivingApiClient(appId);

            _client.UseProduction();
            _client.LogEverything();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            JustGivingSDK.Contracts.Fundraising.GetFundraisingPageResponse campaignResponse = await _client.Fundraising.GetFundraisingPageDetails(_shortPageName);
            return View(campaignResponse);
        }
    }
}
