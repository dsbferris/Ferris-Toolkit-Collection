using FirstFloor.ModernUI.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Directory_Scanner_WPF_ModernUI.DirectoryScanner;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;

namespace Directory_Scanner_WPF_ModernUI.Pages.DirectoryScanner
{
	/// <summary>
	/// Interaction logic for BasicPage1.xaml
	/// </summary>
	public partial class SearchPage : UserControl, IContent
	{
		private DirectoryScan scan;
		public SearchPage()
		{
			InitializeComponent();
		}

		#region IContent NavigationEvents
		public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
		{
		}

		public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
		{
		}

		public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
		{
		}

		public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
		{
			if(e.Source.OriginalString == "/Pages/DirectoryScanner/ListPage.xaml")
			{
				if (scan == null)
					e.Cancel = true;
			}
		}
		#endregion

		private void BtnOpen_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog()
			{
				Multiselect = false,
				Filter = "Directory Scan|*.ds|Encrypted Directory Scan|*.dsc"
			};
			if(ofd.ShowDialog() == true)
			{
				var scan = XMLSerializerHelper.Deserialize(ofd.FileName);
				//TODO Change to ListTab
				var list = new ListPage();
				list.Scan = scan;
				list.BeginInit();
			}
			
		}

		/// <summary>
		/// Handles drop of folders into ListView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LVFolders_Drop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string[] dirs = (string[])e.Data.GetData(DataFormats.FileDrop);
				foreach (var dir in dirs)
				{
					if (Directory.Exists(dir)) LVFolders.Items.Add(dir);
				}
			}
		}

		/// <summary>
		/// Handles delete keypress in ListView for folders
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LVFolders_KeyDown(object sender, KeyEventArgs e)
		{
			if (LVFolders.SelectedItems.Count > 1)
			{
				if (MessageBoxResult.Yes == MessageBox.Show("Are you sure to remove all folders from selection?", "Think about it", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes))
				{
					foreach (var item in LVFolders.SelectedItems) LVFolders.Items.Remove(item);
				}
			}
			else if (LVFolders.SelectedItems.Count == 1) LVFolders.Items.Remove(LVFolders.SelectedItem);
		}

		/// <summary>
		/// Handles adding of extensions into TreeView from Textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TbExtensionAdd_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				List<string> extensions = GetExtensions();
				string extension = TbExtensionAdd.Text;

				if (!String.IsNullOrEmpty(extension) && extension.Length > 0 && extension != " ")
				{
					if (!extensions.Contains(extension))
						TVExtensions.Items.Add(new CheckBox()
						{
							Content = extension,
							IsChecked = true,
						});
				}

			}
		}

		/// <summary>
		/// Opens dialog for adding folders
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LVFolders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog("Select folders to add")
			{
				IsFolderPicker = true,
				Multiselect = true,
				//DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				var dirs = dialog.FileNames;
				foreach (var dir in dirs)
				{
					if (Directory.Exists(dir)) LVFolders.Items.Add(dir);
				}
			}
		}


		/// <summary>
		/// Gather all checked extensions from TreeView
		/// </summary>
		/// <returns></returns>
		private List<string> GetExtensions()
		{
			List<string> extensions = new List<string>();
			foreach (var item in TVExtensions.Items)
			{
				if (item.GetType() == typeof(CheckBox))
				{
					CheckBox cb = (CheckBox)item;
					if (cb.IsChecked == true) extensions.Add(cb.Content.ToString());
				}
				else if (item.GetType() == typeof(TreeViewItem))
				{
					foreach (var subitem in ((TreeViewItem)item).Items)
					{
						CheckBox cb = (CheckBox)subitem;
						if (cb.IsChecked == true) extensions.Add(cb.Content.ToString());
					}
				}
			}
			return extensions;
		}

		/// <summary>
		/// Get all selected Folders from ListView
		/// </summary>
		/// <returns></returns>
		private List<string> GetFolders()
		{
			List<string> folders = new List<string>();
			foreach (var item in LVFolders.Items)
			{
				if (Directory.Exists(item.ToString())) folders.Add(item.ToString());
			}
			return folders;
		}


		/// <summary>
		/// Handles start of scanning
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnStart_Click(object sender, RoutedEventArgs e)
		{
			List<string> folders = GetFolders();
			if (folders.Count < 1) return;
			List<string> extensions = GetExtensions();
			if (extensions.Count < 1) return;

			ListPage lp = new ListPage();
			lp.Scan = new DirectoryScan();
			lp.Scan.Folders = folders;
			lp.Scan.Extensions = extensions;
			//App.Current.MainWindow = lp;
			/*
			System.Windows.IInputElement target = FirstFloor.ModernUI.Windows.Navigation.NavigationHelper.FindFrame("_top", System.Windows.Application.Current.MainWindow); 
			System.Windows.Input.NavigationCommands.GoToPage.Execute("/Pages/DirectoryScanner/ListPage.xaml", target);
			*/

			//TODO Navigate to ListPage
		}

		private void TbExtensionAdd_GotFocus(object sender, RoutedEventArgs e)
		{
			var tb = sender as TextBox;
			if (tb.Text == "Add your own here") tb.Clear();
		}
	}
}
