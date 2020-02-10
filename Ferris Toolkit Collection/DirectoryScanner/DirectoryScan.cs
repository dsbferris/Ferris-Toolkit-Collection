using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Scanner_WPF_ModernUI.DirectoryScanner
{
	[Serializable]
	public class DirectoryScan
	{
		public List<string> Folders { get; set; }
		public List<string> Extensions { get; set; }
		public List<ScanFile> Files { get; set; }

		/// <summary>
		/// Sort modes for Sorting Scanfiles. Default ascending, use [MODE]_DESC for descending.
		/// </summary>
		public enum SortMode
		{
			NAME, PATH, SIZE, NAME_DESC, PATH_DESC, SIZE_DESC
		};

		/// <summary>
		/// Sorts the Files list of a Directory Scan.
		/// </summary>
		/// <param name="sm">SortMode for sorting.</param>
		public static void SortFiles(SortMode sm)
		{
			//TODO Implement SORT
			throw new NotImplementedException();
		}
	}

	[Serializable]
	public class ScanFile
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public long Size { get; set; }
		public bool Favourite { get; set; }
	}
}
