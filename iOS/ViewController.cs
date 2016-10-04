using System;
using UIKit;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using inspirational;
using System.Threading.Tasks;

namespace inspirational.iOS
{
	public partial class ViewController : UIViewController
	{
		private DailyGemViewModel ViewModel;

		private LoadingOverlay loadingOverlay;

		public ViewController(IntPtr handle) : base(handle)
		{
			ViewModel = new DailyGemViewModel();
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ShowLoading();

			await ViewModel.GetData();

			HideLoading();

			SetCurrentGem();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}

		partial void ButtonNext_TouchUpInside(UIButton sender)
		{
			ViewModel.NextGem();
			SetCurrentGem();
		}

		partial void ButtonPrev_TouchUpInside(UIButton sender)
		{
			ViewModel.PrevGem();
			SetCurrentGem();
		}

		partial void ButtonLink_TouchUpInside(UIButton sender)
		{
			UIApplication.SharedApplication.OpenUrl(new Foundation.NSUrl(ViewModel.CurrentGem.ldsOrgUrl));
		}

		private void SetCurrentGem()
		{
			title.Text = ViewModel.CurrentGem.title;
			quote.Text = ViewModel.CurrentGem.quote;
			author.Text = ViewModel.CurrentGem.author;
			buttonLink.SetTitle(ViewModel.CurrentGem.ldsOrgUrl, UIControlState.Normal);
			image.Image = UIImage.FromFile(GetRandomImage());
		}

		private string GetRandomImage()
		{
			string result = ImagePicker.GetRandomImage(ViewModel.CurrentGem.author);

			return "Images/" + result + ".jpg";
		}

		private void ShowLoading()
		{
			var bounds = UIScreen.MainScreen.Bounds;

			// show the loading overlay on the UI thread using the correct orientation sizing
			loadingOverlay = new LoadingOverlay(bounds);
			View.Add(loadingOverlay);
		}

		private void HideLoading()
		{
			loadingOverlay.Hide();
		}
	}
}
