using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Reflection;

namespace inspirational.Droid
{
	[Activity(Label = "Inspirational Messages", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private DailyGemViewModel ViewModel;

		private ProgressDialog progressDialog;

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button prev = FindViewById<Button>(Resource.Id.buttonPrev);
			Button next = FindViewById<Button>(Resource.Id.buttonNext);

			prev.Click += buttonPrev_Click;
			next.Click += buttonNext_Click;

			ShowLoading();

			ViewModel = new DailyGemViewModel();

			await ViewModel.GetData();

			SetCurrentGem();

			HideLoading();
		}

		private void buttonPrev_Click(object s, EventArgs e)
		{
			ViewModel.PrevGem();
			SetCurrentGem();
		}

		private void buttonNext_Click(object s, EventArgs e)
		{
			ViewModel.NextGem();
			SetCurrentGem();
		}

		private void ShowLoading()
		{
			progressDialog = ProgressDialog.Show(this, "", "Loading...", false, false);
			progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

		}

		private void HideLoading()
		{
			progressDialog.Hide();
		}

		private void SetCurrentGem()
		{
			var title = FindViewById<TextView>(Resource.Id.textTitle);
			var quote = FindViewById<TextView>(Resource.Id.textQuote);
			var author = FindViewById<TextView>(Resource.Id.textAuthor);
			var image = FindViewById<ImageView>(Resource.Id.imageView1);
			var link = FindViewById<TextView>(Resource.Id.Link);

			title.Text = ViewModel.CurrentGem.title;
			quote.Text = ViewModel.CurrentGem.quote;
			author.Text = ViewModel.CurrentGem.author;
			link.Text = ViewModel.CurrentGem.ldsOrgUrl;

			image.SetImageResource(GetRandomImage());
		}

		private int GetRandomImage()
		{
			string imageName = ImagePicker.GetRandomImage(ViewModel.CurrentGem.author);

			int result = (int)typeof(Resource.Drawable).GetField(imageName.ToLower()).GetValue(null);

			return result;
		}
	}
}

