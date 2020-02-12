﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Directory_Scanner_WPF_ModernUI.DirectoryScanner;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.Win32;

namespace Directory_Scanner_WPF_ModernUI.Pages.DirectoryScanner
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class ListPage : UserControl, IContent
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
			SaveFileDialog sfd = new SaveFileDialog
			{
				Filter = "Directory Scan|*.ds|Encrypted Directory Scan|*.dsc"
			};
			if (sfd.ShowDialog() == true)
			{
				XMLSerializerHelper.Serialize(Scan, sfd.FileName);
			}
		}

		public void OnFragmentNavigation(FragmentNavigationEventArgs e)
		{
		}

		public void OnNavigatedFrom(NavigationEventArgs e)
		{
		}

		public void OnNavigatedTo(NavigationEventArgs e)
		{
			if (Application.Current.Properties.Contains("Scan"))
			{
				Scan = (DirectoryScan)Application.Current.Properties["Scan"];
			}
			if (Scan != null && Scan.Files != null)
				LVFiles.ItemsSource = Scan.Files;
		}

		public void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
		}
	}
}
