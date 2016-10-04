using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace inspirational
{
	public class WebServices
	{
		public async Task<List<DailyGem>> GetQuotes()
		{
			var result = new List<DailyGem>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://glfeed.azurewebsites.net/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.GetAsync("api/ldsgems?lang=eng");

				if (response.IsSuccessStatusCode)
				{
					string json = await response.Content.ReadAsStringAsync();

					result = JsonConvert.DeserializeObject<List<DailyGem>>(json);
				}
			}

			return result;
		}

	}
}
