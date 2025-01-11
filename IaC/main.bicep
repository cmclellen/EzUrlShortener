@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location

@description('The environment.')
@allowed([
  'dev'  
])
param environment string

var uniqueResourceGroupName = uniqueString(resourceGroup().id)

resource webStorageAccount 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: 'st${uniqueResourceGroupName}${environment}'
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}
