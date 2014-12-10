using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using BananaFramework.Managers;

namespace BananaFramework.Chunks.Support
{
	/// <summary>
	/// The Animation class represents a single animation, encompassing associated image keys, 
	/// frame information, etc.
	/// </summary>
	public class Animation
	{
		[XmlElement("name")]
		public string name;
		[XmlElement("textureKey")]
		public string sheetKey;

		[XmlElement("loop")]
		public bool isLooping;

		[XmlElement("frameWidth")]
		public int frameWidth;
		[XmlElement("frameHeight")]
		public int frameHeight;

		[XmlElement("frameCount")]
		public int frameCount;
		[XmlArray("frameSpeeds")]
		public List<int> frameSpeeds;

		[XmlArray("imagePoints")]
		public List<int> imagePoints;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Animation() { }

		/// <summary>
		/// Loads animations from the specified XML file and returns them in a list.
		/// </summary>
		/// <param name="XmlPath">The path to the XML file from which to load animations.</param>
		/// <returns></returns>
		public static List<Animation> LoadAnimationsFromXml(string XmlPath)
		{
			// XML deserializer
			XmlSerializer deserializer = new XmlSerializer(typeof(List<Animation>));

			// Load the specified XML file
			XmlReader xmlReader = XmlReader.Create(XmlPath);

			// Deserialize the XML file into a temporary
			List<Animation> animationList = (List<Animation>)deserializer.Deserialize(xmlReader);

			// Close the TextReader
			xmlReader.Close();

			return animationList;
		}
	}
}
