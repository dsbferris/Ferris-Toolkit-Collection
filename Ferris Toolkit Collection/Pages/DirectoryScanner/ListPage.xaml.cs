using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Directory_Scanner_WPF_ModernUI.DirectoryScanner;
using Microsoft.Win32;

namespace Directory_Scanner_WPF_ModernUI.Pages.DirectoryScanner
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class ListPage : UserControl
	{
		public DirectoryScan Scan { get; set; }

		public ListPage()
		{
			InitializeComponent();
		}

		private void LVFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (LVFiles.SelectedItem is ScanFile item)
			{
				if (item.Favourite) item.Favourite = false;
				else item.Favourite = true;
			}
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			if (sfd.ShowDialog() == true)
			{
				XMLSerializerHelper.Serialize(Scan, sfd.FileName);
			}
		}
	}
}
