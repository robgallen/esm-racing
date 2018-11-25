using System.Threading.Tasks;
using JustGivingSDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ESM.Racing.Pages.Components
{
    public class JustGivingViewComponent : ViewComponent
    {
        private readonly JustGivingApiClient _client;

        public JustGivingViewComponent(IConfiguration config)
        {
            string appId = config.GetSection("JustGiving").GetValue<string>("AppId");
            _client = new JustGivingApiClient(appId);

            _client.UseProduction();
            _client.LogEverything();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            JustGivingSDK.Contracts.Campaign.GetCampaignDetailsResponse campaignResponse = await _client.Campaigns.GetCampaignDetails("mariecurie", "mypeakchallenge");
            return View(campaignResponse);
        }
    }
}