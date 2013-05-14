<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Dashboard.Azure" generation="1" functional="0" release="0" Id="46bdab05-8b15-4b24-9d65-37c96bcf74b5" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="Dashboard.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WebUI:Unsecured" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/LB:WebUI:Unsecured" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WebUI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/MapWebUI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WebUIInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/MapWebUIInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WebUI:Unsecured">
          <toPorts>
            <inPortMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/WebUI/Unsecured" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWebUI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/WebUI/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWebUIInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/WebUIInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WebUI" generation="1" functional="0" release="0" software="C:\Users\BradC\Documents\GitHub\Dashboard\Dashboard.Azure\csx\Azure\roles\WebUI" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Unsecured" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WebUI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WebUI&quot;&gt;&lt;e name=&quot;Unsecured&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/WebUIInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/WebUIUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/WebUIFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WebUIUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WebUIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WebUIInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="29682991-0b57-499a-a011-35c96367f3e0" ref="Microsoft.RedDog.Contract\ServiceContract\Dashboard.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="2dc00dfc-b57c-49b5-9204-49941a39eef4" ref="Microsoft.RedDog.Contract\Interface\WebUI:Unsecured@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Dashboard.Azure/Dashboard.AzureGroup/WebUI:Unsecured" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>