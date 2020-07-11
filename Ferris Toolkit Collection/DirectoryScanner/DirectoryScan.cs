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
		public void SortFiles(SortMode sm)
		{
			switch (sm)
			{
				case SortMode.NAME: Files.Sort(delegate (ScanFile s1, ScanFile s2) { return s1.Name.CompareTo(s2.Name); }); break;
				case SortMode.PATH: Files.Sort(delegate (ScanFile s1, ScanFile s2) { return s1.Path.CompareTo(s2.Path); }); break;
				case SortMode.SIZE: Files.Sort(delegate (ScanFile s1, ScanFile s2) { return s1.Size.CompareTo(s2.Size); }); break;
				case SortMode.NAME_DESC: Files.Sort(delegate (ScanFile s1, ScanFile s2) { return s2.Name.CompareTo(s1.Name); }); break;
				case SortMode.PATH_DESC: Files.Sort(delegate (ScanFile s1, ScanFile s2) { return s2.Path.CompareTo(s1.Path); }); break;
				case SortMode.SIZE_DESC: Files.Sort(delegate (ScanFile s1, ScanFile s2) { return s2.Size.CompareTo(s1.Size); }); break;
			}
		}
	}

	[Serializable]
	public class ScanFile
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public long Size { get; set; }
		public string SizeReadable { get; set; }
		public bool Favourite { get; set; }
	}
}
