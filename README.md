# ExternalConfiguration
In progress, will be updated soon

Tools and classes for seperating and storing, and caching configuration external to projects in either a local file (or network drive), or Azure Blob Storage.  This can simplify deployments for scale environments that use a single configuration over multiple machines, or environmental transfers (shifting from dev, to Preview, to Prod).  Currently I've only built out the xml client / structure, and only supports strings at this point in time.  I'm looking at filling out the generic / typed read, but it's still under development.

The library expects a standard format for configuration files.  Check out the ExampleConfiguration.xml in the repo for the format.

Using the WindowsDirectoryLoader:
You will want to use the WindowsDirectoryLoader for any configuration that is stored either local on the machine, or on a shared network drive that the machine / process has access too.

Create a new instance of the Configuration settings, providing the details for your environment:
var settings = new ConfigurationSettings()
{
	CacheTimeout = TimeSpan.FromMilliseconds(10000d),
	ConfigurationSection = "MyApplication",
	FileLocation = <PATH TO YOUR CONFIGURATION FILE>,
};

File location needs to include the full path and file name (expected path would look like: "C:\superprogram\externalconfiguration.xml").

Create a new instance of the client with the Windows Directory Loader:
var loader = new WindowsDirectoryLoader(settings);
var client = new XmlConfigurationClient(loader, settings);

Use the client:
var test = client.ContainsKey("TestOne"); // Will tell you if the key is present in the current configuration file.
var getTest = client.GetString("matt"); // Returns the string representation of the value in the requested element.

Using the AzureBlobLoader:
If you want to centralize the configuration in and Azure blob storage account, use the aptly named AzureBlobLoader to access, and load the configuration.  The azure library extends the existing settings to include some azure blob specific settings, mostly container and filename (since the location contains the connection string).

var settings = new AzureConfigurationSettings()
{
	CacheTimeout = TimeSpan.FromMilliseconds(10000d),
	ConfigurationSection = "mattTest",
	ContainerName = "<NAME OF THE CONTAINER IN THE STORAGE ACCOUNT>",
	FileLocation = @"<CONNECTION STRING TO THE AZURE BLOB ACCOUNT>",
	FileName = "TestConfig.xml"
};

var loader = new AzureBlobLoader(settings);
var client = new XmlConfigurationClient(loader, settings);

Check out the unit and functional tests for a more detailed view of setup and execution.
