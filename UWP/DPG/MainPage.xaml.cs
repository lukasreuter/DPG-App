using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DPG
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
    {
        public MainPage()
        {
			InitializeComponent();
        }

		private void GeneratePassword_Button_Tapped(object sender, TappedRoutedEventArgs e)
		{
			//TODO: check if both boxes have any content
			var d = new dpg();
			d.GeneratePassword(sentenceBox.Password, wordBox.Text);
		}
	}
}
