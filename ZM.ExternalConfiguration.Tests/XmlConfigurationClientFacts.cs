namespace ZM.ExternalConfiguration.Tests
{
    using Moq;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Xunit;
    using System.Linq;

    public class XmlconfigurationClientFacts
    {
        private MemoryStream GenerateXmlStream()
        {
            var doc = new XDocument(new XElement("Configurations",
                new XElement("MattTest",
                    new XElement("Setting", new XAttribute("key", "Matt")) { Value = "Wilson" },
                    new XElement("Setting", new XAttribute("key", "Collection"),
                        new XElement("Child", new XAttribute("key", "MattChild")){ Value = "Dominic"},
                        new XElement("Child", new XAttribute("key", "MattChild")){ Value = "Calvin"}))));

            var stream = new MemoryStream(Encoding.Default.GetBytes(doc.ToString()));
            return stream;
        }

        private MemoryStream GenerateNextXmlStream()
        {
            var doc = new XDocument(new XElement("Configurations",
                new XElement("MattTest",
                    new XElement("Setting", new XAttribute("key", "Matt")) { Value = "Changed" })));

            var stream = new MemoryStream(Encoding.Default.GetBytes(doc.ToString()));
            return stream;
        }

        [Fact]
        public void ContainsKeyShouldReturnTrueWhenKeyPresent()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream);

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(10000));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);

            var result = target.ContainsKey("Matt");
            Assert.True(result);
        }

        [Fact]
        public void ContainsKeyShouldReturnFalseWhenKeyNotPresent()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream);

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(10000));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);

            var result = target.ContainsKey("John"); // That damn John guy...
            Assert.False(result);
        }

        [Fact]
        public void GetStringShouldReturnProperValue()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream);

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(10000));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);

            var result = target.GetString("Matt");
            Assert.Equal("Wilson", result);
        }

        [Fact]
        public void GetStringShouldThrowKeyNotFoundIfKeyNotFound()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream);

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(10000));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);
            var ran = false;

            try
            {
                var result = target.GetString("John"); // That damn John guy...again...
            }
            catch (Exception ex)
            {
                ran = true;
                Assert.IsType<KeyNotFoundException>(ex);
            }

            Assert.True(ran);
        }

        [Fact]
        public void CacheRefreshShouldPickUpChanges()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream());

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(70));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);

            var result = target.GetString("Matt");

            Assert.Equal("Wilson", result);
            loader.Setup(r => r.GetFile()).Returns(this.GenerateNextXmlStream());

            Task.WaitAll(Task.Delay(100));

            result = target.GetString("Matt");
            Assert.Equal("Changed", result);
        }

        [Fact(DisplayName = "GetStrings should return empty collection if no key located")]
        public void GetStringsShouldReturnEmptyIfNoKeyFound()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream());

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(10000));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);

            var result = target.GetStrings("NotFoundKey");

            Assert.Empty(result);
        }

        [Fact(DisplayName = "GetStrings should return only single value for element with value set.")]
        public void GetStringsShouldReturnSingleValueForSingleElements()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream());

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(10000));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);

            var result = target.GetStrings("Matt");

            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
            Assert.Equal("Wilson", result.First());
        }

        [Fact(DisplayName = "GetStrings should return all values for collections")]
        public void GetStringsShouldReturnAllValuesForCollections()
        {
            var loader = new Mock<IFileLoader>();
            loader.Setup(r => r.GetFile()).Returns(this.GenerateXmlStream());

            var config = new Mock<IConfigurationSettings>();
            config.Setup(c => c.CacheTimeout).Returns(TimeSpan.FromMilliseconds(10000));
            config.Setup(c => c.ConfigurationSection).Returns("MattTest");

            var target = new XmlConfigurationClient(loader.Object, config.Object);

            var result = target.GetStrings("Collection");

            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Dominic", result.First());
            Assert.Equal("Calvin", result.Last());
        }
    }
}
