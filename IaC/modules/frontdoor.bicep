param uniqueResourceGroupName string
param environment string
@allowed([
  'Standard_AzureFrontDoor'
  'Premium_AzureFrontDoor'
])
param frontDoorSkuName string = 'Standard_AzureFrontDoor'
param uiHostname string = 'stvnsxt6qwqbeksdev.z8.web.${az.environment().suffixes.storage}'
param apiHostname string = 'ca-vnsxt6qwqbeks-dev.wonderfulsmoke-5e15e657.australiaeast.azurecontainerapps.io'

resource frontDoorProfile 'Microsoft.Cdn/profiles@2021-06-01' = {
  name: 'afd-${uniqueResourceGroupName}-${environment}'
  location: 'global'
  sku: {
    name: frontDoorSkuName
  }
  properties: {
    originResponseTimeoutSeconds: 60
  }

  resource uiRuleSet 'ruleSets' = {
    name: 'uiruleset'
    resource rules 'rules' = {
      name: 'uirule'
      properties: {
        matchProcessingBehavior: 'Stop'
        actions: [
          {
            name: 'UrlRewrite'
            parameters: {
              destination: '/'
              sourcePattern: '/'
              typeName: 'DeliveryRuleUrlRewriteActionParameters'
              preserveUnmatchedPath: false
            }
          }
        ]
      }
    }
  }

  resource frontDoorEndpoint 'afdEndpoints' = {
    name: 'fde-${uniqueResourceGroupName}-${environment}'
    location: 'global'
    properties: {
      enabledState: 'Enabled'
    }

    resource uiFrontDoorRoute 'routes' = {
      name: 'uiroute'
      properties: {
        originGroup: {
          id: uiFrontDoorOriginGroup.id
        }
        supportedProtocols: [
          'Http'
          'Https'
        ]
        patternsToMatch: [
          '/*'
        ]
        forwardingProtocol: 'MatchRequest'
        linkToDefaultDomain: 'Enabled'
        httpsRedirect: 'Enabled'
        originPath: '/'
        ruleSets: [
          {
            id: frontDoorProfile::uiRuleSet.id
          }
        ]
      }
    }

    resource apiFrontDoorRoute 'routes' = {
      name: 'apiroute'
      properties: {
        originGroup: {
          id: apiFrontDoorOriginGroup.id
        }
        supportedProtocols: [
          'Http'
          'Https'
        ]
        patternsToMatch: [
          '/api/*'
          '/index.html'
        ]
        forwardingProtocol: 'MatchRequest'
        linkToDefaultDomain: 'Enabled'
        httpsRedirect: 'Enabled'
      }
    }
  }

  resource uiFrontDoorOriginGroup 'originGroups' = {
    name: 'uiorigingroup'
    properties: {
      loadBalancingSettings: {
        sampleSize: 4
        successfulSamplesRequired: 3
        additionalLatencyInMilliseconds: 50
      }
      healthProbeSettings: {
        probePath: '/'
        probeRequestType: 'HEAD'
        probeProtocol: 'Http'
        probeIntervalInSeconds: 100
      }
    }

    resource uiFrontDoorOrigin 'origins' = {
      name: 'uiorigin'
      properties: {
        hostName: uiHostname
        httpPort: 80
        httpsPort: 443
        originHostHeader: uiHostname
        priority: 1
        weight: 1000
        enabledState: 'Enabled'
      }
    }
  }

  resource apiFrontDoorOriginGroup 'originGroups' = {
    name: 'apiorigingroup'
    properties: {
      loadBalancingSettings: {
        sampleSize: 4
        successfulSamplesRequired: 3
        additionalLatencyInMilliseconds: 50
      }
      healthProbeSettings: {
        probePath: '/'
        probeRequestType: 'HEAD'
        probeProtocol: 'Http'
        probeIntervalInSeconds: 100
      }
    }

    resource apiFrontDoorOrigin 'origins' = {
      name: 'apiorigin'
      properties: {
        hostName: apiHostname
        httpPort: 80
        httpsPort: 443
        originHostHeader: apiHostname
        priority: 1
        weight: 1000
        enabledState: 'Enabled'
      }
    }
  }
}

output frontDoorEndpointHostName string = frontDoorProfile::frontDoorEndpoint.properties.hostName
