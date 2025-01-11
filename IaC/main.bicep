@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location

@description('The environment.')
@allowed([
  'dev'  
])
param environment string

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

resource deploymentScript 'Microsoft.Resources/deploymentScripts@2020-10-01' = {
  name: 'deploymentScript'
  location: location
  kind: 'AzurePowerShell'
  // identity: {
  //   type: 'UserAssigned'
  //   userAssignedIdentities: {
  //     '${managedIdentity.id}': {}
  //   }
  // }
  
  properties: {
    azPowerShellVersion: '3.0'
    scriptContent: '''
param(
    [string] $ResourceGroupName,
    [string] $StorageAccountName,
    [string] $IndexDocument,
    [string] $ErrorDocument404Path)

$ErrorActionPreference = 'Stop'
$storageAccount = Get-AzStorageAccount -ResourceGroupName $ResourceGroupName -AccountName $StorageAccountName

$ctx = $storageAccount.Context
Enable-AzStorageStaticWebsite -Context $ctx -IndexDocument $IndexDocument -ErrorDocument404Path $ErrorDocument404Path
'''
    forceUpdateTag: deploymentScriptTimestamp
    retentionInterval: 'PT4H'
    arguments: '-ResourceGroupName ${resourceGroup().name} -StorageAccountName ${webStorageAccount.name} -IndexDocument ${indexDocument} -ErrorDocument404Path ${errorDocument404Path}'
  }
}
