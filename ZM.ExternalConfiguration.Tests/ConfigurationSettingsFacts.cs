namespace ZM.ExternalConfiguration.Tests
{
    using System;
    using Xunit;

    public class ConfigurationSettingsFacts
    {
        [Fact]
        public void CacheTimeoutShouldSet()
        {
            var target = new ConfigurationSettings();
            var timeout = TimeSpan.FromMilliseconds(1000);
            target.CacheTimeout = timeout;

            Assert.Equal(timeout, target.CacheTimeout);
        }

        [Fact]
        public void SourceTypeShouldSet()
        {
            var target = new ConfigurationSettings() { SourceType = ConfigurationSourceType.SharedDirectory };

            Assert.Equal(ConfigurationSourceType.SharedDirectory, target.SourceType);
        }

        [Fact]
        public void FileLocationShouldSet()
        {
            const string location = "my test location";
            var target = new ConfigurationSettings() { FileLocation = location };

            Assert.Equal(location, target.FileLocation);
        }
    }
}
