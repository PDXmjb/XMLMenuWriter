# XMLMenuWriter

Writes given XML file as a menu structure with active menu item being listed.

When executing the file, one must supply:
1. the complete path of a valid XML (or text) file, and;
2. the currently active path, allowing menu items to be marked "ACTIVE".

Thus,
```xml
<menu>
	<id>Wyvern</id>
	<item>
		<displayName>Home</displayName>
		<path value="/mvc/wyvern/home"></path>
		<subMenu>
			<item>
				<displayName>News</displayName>
				<path value="/mvc/wyvern/home/news"></path>
				<subMenu>
					<item>
						<displayName>Favorites</displayName>
						<path value="/TWR/Directory.aspx" />
					</item>
					<item>
						<displayName>List of Aircraft</displayName>					
						<path system="WYVERN" theme="WYVERN" secure="true" context="/Wyvern" value="/TWR/AircraftSearch.aspx"/>
						<subMenu>
						  <item>
							<displayName>Add New</displayName>
							<path value="/aircraft/create.aspx"/>
						  </item>
						  <item>
							<displayName>Edit</displayName>
							<path value="/aircraft/edit.aspx"/>
						  </item>
						</subMenu>
					</item>
				</subMenu>
			</item>
			<item>
				<displayName>Directory</displayName>
				<path value="/Directory/Directory.aspx" />				
			</item>
		</subMenu>
	</item>
	<item>
		<displayName>PASS</displayName>				
		<path value="/PASS/GeneratePASS.aspx"/>			
		<subMenu>
			<item>
				<displayName>Create New</displayName>					
				<path value="/PASS/GeneratePASS.aspx"/>					
			</item>
			<item>
				<displayName>Sent Requests</displayName>					
				<path value="/PASS/YourPASSReports.aspx"/>					
			</item>
			<item>					
				<displayName>Received Requests</displayName>					
				<path value="/PASS/Pending/PendingRequests.aspx"/>					
			</item>
		</subMenu>
	</item>
	<item superOverride="true">
		<displayName>Company</displayName>
		<path value="/mvc/company/view" />			
		<subMenu>
			<item>
				<displayName>Users</displayName>					
				<path value="/mvc/account/list"/>					
			</item>
			<item>
				<displayName>Aircraft</displayName>					
				<path value="/aircraft/fleet.aspx"/>					
			</item>
			<item>
				<displayName>Insurance</displayName>					
				<path value="/insurance/policies.aspx"/>					
			</item>
			<item>
				<displayName>Certificate</displayName>					
				<path value="/Certificates/Certificates.aspx"/>					
			</item>
		</subMenu>
	</item>
</menu>
```

Becomes
![alt text](https://github.com/PDXmjb/XMLMenuWriter/blob/master/Execution%20example.PNG "execution results")
