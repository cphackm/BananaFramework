using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using BananaFramework.GameManagers;

namespace BananaFramework.GameChunks
{
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

		public Animation() { }

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
