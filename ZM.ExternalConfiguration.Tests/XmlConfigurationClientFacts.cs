namespace ZM.ExternalConfiguration.Tests
{
    using Moq;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Xunit;

    public class XmlconfigurationClientFacts
    {
        private MemoryStream GenerateXmlStream()
        {
            var doc = new XDocument(new XElement("Configurations",
                new XElement("MattTest",
                    new XElement("Setting", new XAttribute("key", "Matt")) { Value = "Wilson" })));

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
    }
}
