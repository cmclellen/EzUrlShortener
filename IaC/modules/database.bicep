@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location
param uniqueResourceGroupName string
param environment string
param sqlAdminSid string
param sqlAdminLogin string

var fwRules = [
  {
    name: 'AzureInternal'
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
]

resource sqlServer 'Microsoft.Sql/servers@2024-05-01-preview' = {
  name: 'sql-${uniqueResourceGroupName}-${environment}'
  location: location
  properties: {
    publicNetworkAccess: 'Enabled'
    administrators: {
      administratorType: 'ActiveDirectory'
      azureADOnlyAuthentication: true
      login: sqlAdminLogin
      principalType: 'User'
      sid: sqlAdminSid
      tenantId: tenant().tenantId
    }
  }

  resource sqlDB 'databases' = {
    name: 'sqldb-${uniqueResourceGroupName}-${environment}'
    location: location
    sku: {
      name: 'Basic'
      tier: 'Basic'
    }
  }

  resource fwrules 'firewallRules' = [
    for fwRule in fwRules: {
      name: fwRule.name
      properties: {
        startIpAddress: fwRule.startIpAddress
        endIpAddress: fwRule.endIpAddress
      }
    }
  ]
}
