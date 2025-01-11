targetScope = 'resourceGroup'

@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location

@description('The environment.')
@allowed([
  'dev'
])
param environment string
param scPricipalId string
param sqlAdminSid string
param sqlAdminLogin string

var uniqueResourceGroupName = uniqueString(resourceGroup().id)

module storage_account 'modules/web.bicep' = {
  name: 'webStorageAccount'
  params: {
    location: location
    scPricipalId: scPricipalId
    uniqueResourceGroupName: uniqueResourceGroupName
    environment: environment
  }
}

module database 'modules/database.bicep' = {
  name: 'database'
  params: {
    location: location
    uniqueResourceGroupName: uniqueResourceGroupName
    environment: environment
    sqlAdminSid: sqlAdminSid
    sqlAdminLogin: sqlAdminLogin
  }
}

module cache 'modules/cache.bicep' = {
  name: 'cache'
  params: {
    location: location
    uniqueResourceGroupName: uniqueResourceGroupName
    environment: environment
  }
}
