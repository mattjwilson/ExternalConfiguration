# ExternalConfiguration
In progress, will be updated soon

Tools and classes for seperating and storing configuration external to projects in either a local file (or network drive), or Azure Blob Storage.

Currently supports xml configurations of the following structure:

<Configuration>
	<MyApplication>
		<Setting key=""></Setting>
		<Setting key="">
			<Value></Value>
			<Value></Value>
			<Value></Value>
		</Setting>
	</MyApplication>
	<MyApplicationTwo>
		<Setting key=""></Setting>
	</MyApplicationTwo>
</Configuration>

Settings and Configuration:
