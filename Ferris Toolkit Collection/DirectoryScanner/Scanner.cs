using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Directory_Scanner_WPF_ModernUI.DirectoryScanner
{
	public static class Scanner
	{
		/// <summary>
		/// Scans all folders and subfolders for files with given extensions
		/// </summary>
		/// <param name="folders">Folders to scan for</param>
		/// <param name="extensions">Extension to scan for. Null for no filter</param>
		/// <returns></returns>
		public static List<ScanFile> Scan(List<string> folders, List<string> extensions)
		{
			List<ScanFile> files = new List<ScanFile>();
			if (extensions.Contains("*")) extensions = null;

			//Should start a scan for each given folder
			foreach (var folder in folders)
			{
				files.AddRange(ScanFolder(folder, extensions));
			}
			return files;
		}

		/// <summary>
		/// Scans a folder and all subfolders for files with matching extension
		/// </summary>
		/// <param name="folder">Folder to Scan</param>
		/// <param name="extensions">Extensions to match</param>
		/// <returns></returns>
		private static IEnumerable<ScanFile> ScanFolder(string folder, List<string> extensions)
		{
			Queue<string> folders = new Queue<string>();
			folders.Enqueue(folder);
			while(folders.Count > 0)
			{
				//Add all subfolders to queue
				string currentFolder = folders.Dequeue();
				foreach(var dir in Directory.EnumerateDirectories(currentFolder))
				{
					folders.Enqueue(dir);
				}
				//return all filtered files in current folder
				foreach(var file in Directory.EnumerateFiles(currentFolder))
				{
					yield return Filter(file, extensions);
				}
			}
		}

		/// <summary>
		/// Check if given file meets filtering options.
		/// </summary>
		/// <param name="file">File to check for</param>
		/// <param name="extensions">Extensions to filter for. NULL if no filter to be applied.</param>
		/// <returns>Returns a ScanFile or null if it didnt fit the extensions</returns>
		private static ScanFile Filter(string file, List<string> extensions)
		{
			if (extensions == null) return CreateScanFile(file);
			foreach(var ext in extensions)
			{
				if (file.EndsWith(ext)) return CreateScanFile(file);
			}
			return null;
		}

		/// <summary>
		/// Create ScanFile object.
		/// </summary>
		/// <param name="file">Filepath to file</param>
		/// <returns></returns>
		private static ScanFile CreateScanFile(string file)
		{
			FileInfo fi = new FileInfo(file);
			ScanFile sf = new ScanFile
			{
				Favourite = false,
				Name = fi.Name,
				Path = fi.FullName,
				Size = fi.Length
			};
			return sf;
		}
	}
}
