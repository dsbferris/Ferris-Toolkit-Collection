using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Directory_Scanner_WPF_ModernUI.DirectoryScanner
{
	public static class XMLSerializerHelper
	{

		public static Task SerializeAsync(DirectoryScan ds, string filePath)
		{
			XmlSerializer serializer = new XmlSerializer(ds.GetType());
			using (var writer = XmlWriter.Create(filePath))
			{
				serializer.Serialize(writer, ds);
			}
			return Task.CompletedTask;
		}

		public static Task<DirectoryScan> DeserializeAsync(string filePath)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(DirectoryScan));
			using (var reader = XmlReader.Create(filePath))
			{
				var ds = (DirectoryScan) serializer.Deserialize(reader);
				return Task.FromResult(ds);
			}
		}

		/// <summary>
		/// Serializes a Directory Scan as xml file.
		/// </summary>
		/// <param name="ds">Directory Scan to serialize</param>
		/// <param name="filePath">Path to save the file</param>
		public static void Serialize(DirectoryScan ds, string filePath)
		{
			XmlSerializer serializer = new XmlSerializer(ds.GetType());
			using (var writer = XmlWriter.Create(filePath))
			{
				serializer.Serialize(writer, ds);
			}
		}

		/// <summary>
		/// Deserialized a DirectoyScan from give filepath.
		/// </summary>
		/// <param name="filePath">File path of Directory Scan file</param>
		/// <returns>Saved Directory Scan</returns>
		public static DirectoryScan Deserialize(string filePath)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(DirectoryScan));
			using (var reader = XmlReader.Create(filePath))
			{
				var ds = serializer.Deserialize(reader);
				return (DirectoryScan)ds;
			}
		}
	}
}
