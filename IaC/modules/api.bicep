@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location
param uniqueResourceGroupName string
param environment string
param containerImage string = 'ghcr.io/cmclellen/ezurlshortener:latest'

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2022-10-01' existing = {
  name: 'logs-${uniqueResourceGroupName}-${environment}'
}

resource appinsights 'Microsoft.Insights/components@2020-02-02' existing = {
  name: 'appi-${uniqueResourceGroupName}-${environment}'
}

resource redisCache 'Microsoft.Cache/redis@2024-11-01' existing = {
  name: 'redis-${uniqueResourceGroupName}-${environment}'
}

resource containerAppEnv 'Microsoft.App/managedEnvironments@2024-10-02-preview' = {
  name: 'cae-${uniqueResourceGroupName}-${environment}'
  location: location
  properties: {
    appInsightsConfiguration: {
      connectionString: appinsights.properties.ConnectionString
    }
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
    openTelemetryConfiguration: {
      tracesConfiguration: {
        destinations: ['appInsights']
      }
      logsConfiguration: {
        destinations: ['appInsights']
      }
    }
  }
}

var cpuCore = '0.25'
var memorySize = '0.5'
var containerAppName = 'ca-${uniqueResourceGroupName}-${environment}'

resource containerApp 'Microsoft.App/containerApps@2024-10-02-preview' = {
  name: 'ca-${uniqueResourceGroupName}-${environment}'
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: containerAppEnv.id
    configuration: {
      activeRevisionsMode: 'single'
      ingress: {
        external: true
        // targetPort: 8081
        allowInsecure: true
        traffic: [
          {
            latestRevision: true
            weight: 100
          }
        ]
      }
    }
    template: {
      // revisionSuffix: 'latest'
      containers: [
        {
          name: containerAppName
          image: containerImage
          resources: {
            cpu: json(cpuCore)
            memory: '${memorySize}Gi'
          }
          env: [
            {
              name: 'ConnectionStrings__url-shortener-db'
              value: 'Server=tcp:sql-vnsxt6qwqbeks-dev${az.environment().suffixes.sqlServerHostname},1433;Initial Catalog=sqldb-vnsxt6qwqbeks-dev;TrustServerCertificate=True;Connection Timeout=30;Authentication="Active Directory Default";'
            }
            {
              name: 'ConnectionStrings__azcache'
              value: 'redis-vnsxt6qwqbeks-dev.redis.cache.windows.net:6380'
            }
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: 'Development'
            }
            {
              name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
              value: appinsights.properties.ConnectionString
            }
          ]
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 1
      }
    }
  }
}

resource redisCacheBuiltInAccessPolicyAssignment 'Microsoft.Cache/redis/accessPolicyAssignments@2024-11-01' = {
  name: 'builtInAccessPolicyAssignment-${uniqueString(resourceGroup().id)}'
  parent: redisCache
  properties: {
    accessPolicyName: 'Data Reader'
    objectId: containerApp.identity.principalId
    objectIdAlias: containerAppEnv.name
  }
}

output containerAppFQDN string = containerApp.properties.configuration.ingress.fqdn
