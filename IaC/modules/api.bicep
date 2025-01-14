@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location
param uniqueResourceGroupName string
param environment string
param containerImage string = 'mcr.microsoft.com/azuredocs/containerapps-helloworld:latest'

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2022-10-01' existing = {
  name: 'logs-${uniqueResourceGroupName}-${environment}'
}

resource containerAppEnv 'Microsoft.App/managedEnvironments@2022-06-01-preview' = {
  name: 'cae-${uniqueResourceGroupName}-${environment}'
  location: location
  sku: {
    name: 'Consumption'
  }
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
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
        targetPort: 8081
        allowInsecure: false
        traffic: [
          {
            latestRevision: true
            weight: 100
          }
        ]
      }
    }

    template: {
      revisionSuffix: 'latest'
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
              value: 'Server=tcp:sql-vnsxt6qwqbeks-dev.database.windows.net,1433;Initial Catalog=sqldb-vnsxt6qwqbeks-dev;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication="Active Directory Default";'
            }
            {
              name: 'ConnectionStrings__redis'
              value: 'redis-vnsxt6qwqbeks-dev.redis.cache.windows.net'
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

output containerAppFQDN string = containerApp.properties.configuration.ingress.fqdn
