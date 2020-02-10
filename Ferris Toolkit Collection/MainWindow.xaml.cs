using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Presentation;

namespace Directory_Scanner_WPF_ModernUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ModernWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			//Makes default Application Color to Green. I like it.
			AppearanceManager.Current.AccentColor = Colors.Green;
			//TODO Handle Environment.GetCommandLineArgs() to check if a file was dropped on the exe. Handle then properly.
			//Example: scan.ds dropped onto this exe -> open scan in directory scanner listview
		}
	}
}
