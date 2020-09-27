using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.Network;
using Pulumi.Azure.Storage;

class MyStack : Stack
{
    public MyStack()
    {
        var suffix = "pulumi-devops";

        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup($"rg-{suffix}");
        CreateVirtualNetwork(suffix, resourceGroup);

        var publicip = new PublicIp($"pip-{suffix}", new PublicIpArgs { 
            ResourceGroupName = resourceGroup.Name,
            AllocationMethod = "Static",
        });



        // Create an Azure Storage Account
        var storageAccount = new Account("storage", new AccountArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AccountReplicationType = "LRS",
            AccountTier = "Standard"
        });

        // Export the connection string for the storage account
        this.ConnectionString = storageAccount.PrimaryConnectionString;
    }

    private static void CreateVirtualNetwork(string suffix, ResourceGroup resourceGroup)
    {
        var virtualNetwork = new VirtualNetwork($"vnet-{suffix}", new VirtualNetworkArgs
        {
            AddressSpaces = { "10.0.0.0/16" },
            ResourceGroupName = resourceGroup.Name
        });

        var subnet = new Subnet("apps", new SubnetArgs
        {
            ResourceGroupName = resourceGroup.Name,
            VirtualNetworkName = virtualNetwork.Name,
            AddressPrefixes = {
                "10.0.1.0/24",
            }
        });

        var nsg = new NetworkSecurityGroup($"nsg-{suffix}", new NetworkSecurityGroupArgs
        {
            ResourceGroupName = resourceGroup.Name,
        });

        var nsgSshAllowInbound = new NetworkSecurityRule("ssh-allow-inbound", new NetworkSecurityRuleArgs
        {
            Priority = 1001,
            Direction = "Inbound",
            Access = "Allow",
            Protocol = "Tcp",
            SourcePortRange = "*",
            DestinationPortRange = "22",
            SourceAddressPrefix = "*",
            DestinationAddressPrefix = "*",
            NetworkSecurityGroupName = nsg.Name,
            ResourceGroupName = resourceGroup.Name,
        });
    }

    [Output]
    public Output<string> ConnectionString { get; set; }
}
