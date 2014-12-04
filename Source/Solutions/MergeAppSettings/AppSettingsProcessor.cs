namespace MergeAppSettings
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Xml.Linq;

    public class AppSettingsProcessor
    {
        private readonly string source;

        private readonly string target;

        public AppSettingsProcessor(string source, string target)
        {
            this.source = source;
            this.target = target;
        }

        public void Process()
        {
            var sourceDoc = XDocument.Load(this.source);
            var targetDoc = XDocument.Load(this.target);

            var sourceAppSettings = sourceDoc.Descendants("appSettings");
            var targetAppSettings = targetDoc.Descendants("appSettings");

            foreach (var element in sourceAppSettings.Descendants())
            {
                Console.WriteLine("key: {0}, value: {1}", element.Attribute("key").Value, element.Attribute("value").Value);

                if (targetAppSettings.Descendants().Any(x => x.Attribute("key").Value == element.Attribute("key").Value))
                {
                    Console.WriteLine("update value for existing key: {0}", element.Attribute("key").Value);
                    var updateElement = targetAppSettings.Descendants().First(x => x.Attribute("key").Value == element.Attribute("key").Value);
                    updateElement.SetAttributeValue("value", element.Attribute("value").Value);
                }
                else
                {
                    Console.WriteLine("add key for: {0}", element.Attribute("key").Value);
                    var newElement = new XElement("add");
                    newElement.SetAttributeValue("key", element.Attribute("key").Value);
                    newElement.SetAttributeValue("value", element.Attribute("value").Value);
                    targetAppSettings.First().Add(newElement);
                }
            }

            targetDoc.Save(this.target);
        }
    }
}