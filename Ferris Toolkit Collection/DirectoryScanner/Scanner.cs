using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows;

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
					var scfile = Filter(file, extensions);
					if (scfile != null) yield return scfile;
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
				Size = fi.Length,
				SizeReadable = GetBytesReadable(fi.Length)
			};
			return sf;
		}

		private static string GetBytesReadable(long i)
		{
			// Get absolute value
			long absolute_i = (i < 0 ? -i : i);
			// Determine the suffix and readable value
			string suffix;
			double readable;
			if (absolute_i >= 0x1000000000000000) // Exabyte
			{
				suffix = "EB";
				readable = (i >> 50);
			}
			else if (absolute_i >= 0x4000000000000) // Petabyte
			{
				suffix = "PB";
				readable = (i >> 40);
			}
			else if (absolute_i >= 0x10000000000) // Terabyte
			{
				suffix = "TB";
				readable = (i >> 30);
			}
			else if (absolute_i >= 0x40000000) // Gigabyte
			{
				suffix = "GB";
				readable = (i >> 20);
			}
			else if (absolute_i >= 0x100000) // Megabyte
			{
				suffix = "MB";
				readable = (i >> 10);
			}
			else if (absolute_i >= 0x400) // Kilobyte
			{
				suffix = "KB";
				readable = i;
			}
			else
			{
				return i.ToString("0 B"); // Byte
			}
			// Divide by 1024 to get fractional value
			readable = (readable / 1024);
			// Return formatted number with suffix
			return readable.ToString("0.## ") + suffix;
		}
	}
}
