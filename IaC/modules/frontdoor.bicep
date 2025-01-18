param uniqueResourceGroupName string
param environment string
@allowed([
  'Standard_AzureFrontDoor'
  'Premium_AzureFrontDoor'
])
param frontDoorSkuName string = 'Standard_AzureFrontDoor'

resource frontDoorProfile 'Microsoft.Cdn/profiles@2021-06-01' = {
  name: 'afd-${uniqueResourceGroupName}-${environment}'
  location: 'global'
  sku: {
    name: frontDoorSkuName
  }

  resource rule 'ruleSets' = {
    name: 'ui-rulesets'
    resource Identifier 'rules' = {
      name: 'ui-rules'
      properties: {
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

    resource frontDoorRoute 'routes' = {
      name: 'ui-route'
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
        forwardingProtocol: 'HttpsOnly'
        linkToDefaultDomain: 'Enabled'
        httpsRedirect: 'Enabled'
      }
    }
  }

  resource uiFrontDoorOriginGroup 'originGroups' = {
    name: 'ui'
    properties: {
      loadBalancingSettings: {
        sampleSize: 4
        successfulSamplesRequired: 3
      }
      healthProbeSettings: {
        probePath: '/'
        probeRequestType: 'HEAD'
        probeProtocol: 'Http'
        probeIntervalInSeconds: 100
      }
    }

    resource uiFrontDoorOrigin 'origins' = {
      name: 'ui-origin'
      properties: {
        hostName: 'stvnsxt6qwqbeksdev.z8.web.core.${az.environment().suffixes.storage}'
        httpPort: 80
        httpsPort: 443
        originHostHeader: 'stvnsxt6qwqbeksdev.z8.web.${az.environment().suffixes.storage}'
        priority: 1
        weight: 1000
      }
    }
  }
}

output frontDoorEndpointHostName string = frontDoorProfile::frontDoorEndpoint.properties.hostName
