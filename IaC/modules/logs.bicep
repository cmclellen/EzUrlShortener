@description('The Azure region into which the resources should be deployed.')
param location string = resourceGroup().location
param uniqueResourceGroupName string
param environment string

resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: 'logs-${uniqueResourceGroupName}-${environment}'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

resource insight 'Microsoft.Insights/components@2020-02-02' = {
  name: 'appi-${uniqueResourceGroupName}-${environment}'
  location: location
  properties: {
    Application_Type: 'web'
  }
  kind: 'web'
}

output logAnalyticsId string = logAnalytics.id
