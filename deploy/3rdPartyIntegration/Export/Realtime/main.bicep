targetScope='subscription'

@description('The resource group name.')
param resourceGroupName string = 'IntegrationRealtimeTest'

@description('The location of the resource group. All resources will be created under this location.')
param resourceGroupLocation string = 'centralus'

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: resourceGroupLocation
}

module resources 'resources.bicep' = {
  name: 'allResources'
  scope: rg
  params: {
    location: resourceGroupLocation
    resourceNamePrefix: toLower(resourceGroupName)
  }
}




