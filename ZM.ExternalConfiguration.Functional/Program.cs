namespace ZM.ExternalConfiguration.Functional
{
    using ZM.ExternalConfiguration.Azure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            TestSharedDirectory();
            //TestAzure(); // removing so that I don't put my azure connection string up.

            Console.ReadLine();
        }

        private static void TestSharedDirectory()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "TestConfiguration.xml";
            var settings = new ConfigurationSettings()
            {
                CacheTimeout = TimeSpan.FromMilliseconds(10000d),
                ConfigurationSection = "MyApplication",
                FileLocation = path,
            };

            var client = new XmlConfigurationClient(new WindowsDirectoryLoader(settings), settings);

            var test = client.ContainsKey("TestOne");
            Console.WriteLine("Contains Key = {0}", test);
            var getTest = client.GetString("TestOne");
            Console.WriteLine("Value = {0}", getTest);

            var testCollection = client.GetStrings("TestTwo");
            foreach (var item in testCollection)
                Console.WriteLine(item);
        }

        private static void TestAzure()
        {
            var settings = new AzureConfigurationSettings()
            {
                CacheTimeout = TimeSpan.FromMilliseconds(10000d),
                ConfigurationSection = "mattTest",
                ContainerName = "configuration",
                FileLocation = "",
                FileName = "TestConfig.xml"
            };

            var client = new XmlConfigurationClient(new AzureBlobLoader(settings), settings);

            var test = client.ContainsKey("matt");
            Console.WriteLine("Contains Key = {0}", test);
            var getTest = client.GetString("matt");
            Console.WriteLine("Value = {0}", getTest);
        }
    }
}
