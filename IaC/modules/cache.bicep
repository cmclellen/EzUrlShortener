@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location
param uniqueResourceGroupName string
param environment string

// param builtInAccessPolicyAssignmentName string = 'builtInAccessPolicyAssignment-${uniqueString(resourceGroup().id)}'
// param builtInAccessPolicyAssignmentObjectId string = newGuid()
// param builtInAccessPolicyAssignmentObjectAlias string = 'builtInAccessPolicyApplication-${uniqueString(resourceGroup().id)}'
// param customAccessPolicyName string = 'customAccessPolicy-${uniqueString(resourceGroup().id)}'
// param customAccessPolicyAssignmentName string = 'customAccessPolicyAssignment-${uniqueString(resourceGroup().id)}'
// param customAccessPolicyAssignmentObjectId string = newGuid()
// param customAccessPolicyAssignmentObjectAlias string = 'customAccessPolicyApplication-${uniqueString(resourceGroup().id)}'

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

  // resource redisCacheBuiltInAccessPolicyAssignment 'accessPolicyAssignments' = {
  //   name: builtInAccessPolicyAssignmentName
  //   properties: {
  //     accessPolicyName: 'Data Reader'
  //     objectId: builtInAccessPolicyAssignmentObjectId
  //     objectIdAlias: builtInAccessPolicyAssignmentObjectAlias
  //   }
  // }
}
