using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.Network;
using Pulumi.Azure.Storage;

class MyStack : Stack
{
    public MyStack()
    {
        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("pulumi-devops");

        // Create Virtual Network
        var virtualNetwork = new VirtualNetwork("pulumi", new VirtualNetworkArgs
        {
            AddressSpaces = { "10.0.0.0/16" },
            ResourceGroupName = resourceGroup.Name
        });

        var subnet = new Subnet("pulumi", new SubnetArgs
        {
            ResourceGroupName= resourceGroup.Name,
            VirtualNetworkName = virtualNetwork.Name,
            AddressPrefix = "10.0.1.0/24"
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

    [Output]
    public Output<string> ConnectionString { get; set; }
}
