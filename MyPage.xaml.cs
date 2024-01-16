
using System.Xml;

namespace MauiAppCrash
{
	public partial class MyPage : ContentPage
	{
		public MyPage()
		{
			InitializeComponent();
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			var securityHelper = new SecurityHelper();
			var xDoc = new XmlDocument();
			xDoc.LoadXml("<root><data>this is a dummy string</data><count>-1000</count></root>");

			securityHelper.RASSignXml(ref xDoc, out string debug);

			myLabel.Text = debug;
		}
	}

}
