using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace inspirational
{
	public class DailyGemViewModel
	{
		private List<DailyGem> gems;

		private int CurrentGemIndex;

		public DailyGem CurrentGem { get; set; }

		public DailyGemViewModel()
		{
		}

		public async Task GetData()
		{
			var services = new WebServices();

			gems = await services.GetQuotes();

			SetCurrentGem(0);
		}

		public void PrevGem()
		{
			SetCurrentGem(CurrentGemIndex - 1);
		}

		public void NextGem()
		{
			SetCurrentGem(CurrentGemIndex + 1);
		}

		public void SetCurrentGem(int index)
		{
			if (index > gems.Count - 1)
			{
				index = 0;
			}

			if (index < 0)
			{
				index = gems.Count - 1;
			}

			CurrentGem = gems[index];
			CurrentGemIndex = index;
		}
	}
}
