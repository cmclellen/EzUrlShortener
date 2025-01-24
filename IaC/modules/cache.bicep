@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location
param uniqueResourceGroupName string
param environment string

resource redisCache 'Microsoft.Cache/redis@2023-08-01' = {
  name: 'redis-${uniqueResourceGroupName}-${environment}'
  location: location
  properties: {
    enableNonSslPort: false
    minimumTlsVersion: '1.2'
    sku: {
      capacity: 0
      family: 'C'
      name: 'Basic'
    }
    redisConfiguration: {
      'aad-enabled': 'true'
    }
  }
}
