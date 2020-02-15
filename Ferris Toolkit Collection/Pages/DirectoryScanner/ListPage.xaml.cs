using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Directory_Scanner_WPF_ModernUI.DirectoryScanner;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
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

		public Predicate<object> Filter { get; set; }

		public ListPage()	
		{
			InitializeComponent();		
			
		}
		#region IContent Events
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
			{
				LVFiles.ItemsSource = Scan.Files;
				LVFiles.Items.Filter = Filter;
			}

		}

		public void OnNavigatingFrom(NavigatingCancelEventArgs e)
		{
		}
		#endregion

		private void LVFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (LVFiles.SelectedItem is ScanFile item)
			{
				if (item.Favourite) item.Favourite = false;
				else item.Favourite = true;
			}
		}

		private async void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog
			{
				Filter = "Directory Scan|*.ds|Encrypted Directory Scan|*.dsc"
			};
			if (sfd.ShowDialog() == true)
			{
				await XMLSerializerHelper.SerializeAsync(Scan, sfd.FileName);
				ModernDialog.ShowMessage("Saved Scan successfully", "Saved", MessageBoxButton.OK);
			}
		}


		private void FilterChanged()
		{
			if(TBFavourites.IsChecked == true)
			{
				if (!string.IsNullOrWhiteSpace(TbFilter.Text))
				{
					Filter = new Predicate<object>(f =>
					{
					var file = (ScanFile)f;
						return file.Favourite && file.Name.Contains(TbFilter.Text);
					});
				}
				else
				{
					Filter = new Predicate<object>(f => ((ScanFile)f).Favourite);
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(TbFilter.Text))
				{
					Filter = new Predicate<object>(f => ((ScanFile)f).Name.Contains(TbFilter.Text));
				}
				else
				{
					Filter = null;
				}
			}
			try
			{
				LVFiles.Items.Filter = Filter;
			}
			catch (Exception)
			{
			}
		}

		private void TBFavourites_Checked_Changed(object sender, RoutedEventArgs e)
		{
			FilterChanged();
		}

		private void TbFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			FilterChanged();
		}
	}
}
