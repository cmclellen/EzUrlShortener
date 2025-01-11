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

// resource redisCacheBuiltInAccessPolicyAssignment 'Microsoft.Cache/redis/accessPolicyAssignments@2023-08-01' = {
//   name: builtInAccessPolicyAssignmentName
//   parent: redisCache
//   properties: {
//     accessPolicyName: builtInAccessPolicyName
//     objectId: builtInAccessPolicyAssignmentObjectId
//     objectIdAlias: builtInAccessPolicyAssignmentObjectAlias
//   }
// }

// resource redisCacheCustomAccessPolicy 'Microsoft.Cache/redis/accessPolicies@2023-08-01' = {
//   name: customAccessPolicyName
//   parent: redisCache
//   properties: {
//     permissions: customAccessPolicyPermissions
//   }
//   dependsOn: [
//     redisCacheBuiltInAccessPolicyAssignment
//   ]
// }

// resource redisCacheCustomAccessPolicyAssignment 'Microsoft.Cache/redis/accessPolicyAssignments@2023-08-01' = {
//   name: customAccessPolicyAssignmentName
//   parent: redisCache
//   properties: {
//     accessPolicyName: customAccessPolicyName
//     objectId: customAccessPolicyAssignmentObjectId
//     objectIdAlias: customAccessPolicyAssignmentObjectAlias
//   }
//   dependsOn: [
//     redisCacheCustomAccessPolicy
//   ]
// }
