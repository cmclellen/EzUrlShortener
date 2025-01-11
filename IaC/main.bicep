@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location

@description('The environment.')
@allowed([
  'dev'  
])
param environment string

param scPricipalId string

var uniqueResourceGroupName = uniqueString(resourceGroup().id)

param deploymentScriptTimestamp string = utcNow()
param indexDocument string = 'index.html'
param errorDocument404Path string = 'error.html'

resource webStorageAccount 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: 'st${uniqueResourceGroupName}${environment}'
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2018-11-30' = {
  name: 'DeploymentScript'
  location: location
}

var storageAccountContributorRoleDefinitionId = subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '17d1049b-9a84-46fb-8f53-869881c3d3ab') 
var storageAccountBlobDataOwnerRoleDefinitionId = subscriptionResourceId('Microsoft.Authorization/roleDefinitions', 'b7e6dc6d-f1e8-4753-8033-0f276bb0955b') 

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: webStorageAccount
  name: guid(resourceGroup().id, storageAccountContributorRoleDefinitionId)
  properties: {
    roleDefinitionId: storageAccountContributorRoleDefinitionId
    principalId: managedIdentity.properties.principalId
  }
}

resource scRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: webStorageAccount
  name: guid(resourceGroup().id, storageAccountBlobDataOwnerRoleDefinitionId)
  properties: {
    roleDefinitionId: storageAccountBlobDataOwnerRoleDefinitionId
    principalId: scPricipalId
  }
}

resource deploymentScript 'Microsoft.Resources/deploymentScripts@2023-08-01' = {
  name: 'deploymentScript'
  location: location
  kind: 'AzurePowerShell'
  identity: {
    type: 'UserAssigned'
    userAssignedIdentities: {
      '${managedIdentity.id}': {}
    }
  }  
  dependsOn: [
    roleAssignment
  ]
  properties: {
    azPowerShellVersion: '3.0'
    scriptContent: loadTextContent('./scripts/enable-storage-static-website.ps1')
    forceUpdateTag: deploymentScriptTimestamp
    retentionInterval: 'PT4H'
    arguments: '-ResourceGroupName ${resourceGroup().name} -StorageAccountName ${webStorageAccount.name} -IndexDocument ${indexDocument} -ErrorDocument404Path ${errorDocument404Path}'
  }
}

output staticWebsiteHostName string = replace(replace(webStorageAccount.properties.primaryEndpoints.web, 'https://', ''), '/', '')
