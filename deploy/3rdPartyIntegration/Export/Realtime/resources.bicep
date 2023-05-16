@description('The location of all resources created.')
param location string = resourceGroup().location

@description('The prefix for names of all resources.')
param resourceNamePrefix string = 'integrationrealtime'

var storageName = length(resourceNamePrefix) > 21 ? take(resourceNamePrefix, 21) : resourceNamePrefix
var functionAppName = '${resourceNamePrefix}faw'
var storageAccountConnectionStr = 'DefaultEndpointsProtocol=https;AccountName=${stoAcc.name};AccountKey=${stoAcc.listKeys().keys[0].value};EndpointSuffix=${environment().suffixes.storage}'

@description('Resource representing a storage account for use by Azure function and also for outputting integration events from function.')
resource stoAcc 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: '${storageName}sto'
  location: location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_LRS'
  }
  properties: {
    accessTier: 'Hot'
    allowSharedKeyAccess: true
    encryption: {
      keySource: 'Microsoft.Storage'
      requireInfrastructureEncryption: false
      services: {
        blob: {
          enabled: true
          keyType: 'Account'
        }
        file: {
          enabled: true
          keyType: 'Account'
        }
        queue: {
          enabled: true
          keyType: 'Account'
        }
        table: {
          enabled: true
          keyType: 'Account'
        }
      }
    }
    minimumTlsVersion: 'TLS1_2'
    networkAcls: {
      defaultAction: 'Allow'
      bypass: 'AzureServices'
    }
    routingPreference: {
      routingChoice: 'MicrosoftRouting'
    }
    supportsHttpsTrafficOnly: true
  }
}

@description('Resource representing an relay with hybrid connection to connect to the event grid.')
resource relayNs 'Microsoft.Relay/namespaces@2021-11-01' = {
  name: '${resourceNamePrefix}relay'
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }

  resource relayNsKey 'authorizationRules' = {
    name: 'RootManageSharedAccessKey'
    properties: {
      rights: [
        'Listen'
        'Manage'
        'Send'
      ]
    }
  }

  resource relayNsNwRules 'networkRuleSets' = {
    name: 'default'
    properties: {
      publicNetworkAccess: 'Enabled'
      defaultAction: 'Allow'
    }
  }

  resource relayNsHybridConn 'hybridConnections' = {
    name: '${relayNs.name}hc'
    properties: {
      requiresClientAuthorization: true
    }

    resource relayNsHybridConnKey 'authorizationRules' = {
      name: '${relayNsHybridConn.name}ListenKey'
      properties: {
        rights: [
          'Listen'
        ]
      }
    }
  }
}

@description('Resource representing an event grid for outputting integration events from function.')
resource eventGrid 'Microsoft.EventGrid/topics@2022-06-15' = {
  name: '${resourceNamePrefix}eg'
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    dataResidencyBoundary: 'WithinRegion'
    disableLocalAuth: false
    inputSchema: 'CloudEventSchemaV1_0'
    publicNetworkAccess: 'Enabled'
  }

  resource eventGridSubs 'eventSubscriptions' = {
    name: '${eventGrid.name}hcsub'
    properties: {
      destination: {
        endpointType: 'HybridConnection'
        properties: {
          resourceId: relayNs::relayNsHybridConn.id
        }
      }
      filter: {
        enableAdvancedFilteringOnArrays: true
      }
      eventDeliverySchema: 'CloudEventSchemaV1_0'
      labels: []
      retryPolicy: {
        maxDeliveryAttempts: 10
        eventTimeToLiveInMinutes: 30
      }
    }
  }
}

@description('Resource representing a Log Analytics workspace that will be attached to the application insights.')
resource laWorkspace 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: '${resourceNamePrefix}laws'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
  }
}

@description('Resource representing an application insights instance.')
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: '${resourceNamePrefix}ai'
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    Flow_Type: 'Bluefield'
    IngestionMode: 'LogAnalytics'
    publicNetworkAccessForIngestion: 'Enabled'
    publicNetworkAccessForQuery: 'Enabled'
    RetentionInDays: 60
    WorkspaceResourceId: laWorkspace.id
  }
}

@description('Resource representing the underlying App Service Plan for the function app.')
resource funcAppServerFarm 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: '${resourceNamePrefix}sf'
  location: location
  sku: {
    name: 'Y1'
    capacity: 0
    family: 'Y'
    size: 'Y1'
    tier: 'Dynamic'
  }
  kind: 'functionapp'
  properties: {
    elasticScaleEnabled: false
    hyperV: false
    isSpot: false
    maximumElasticWorkerCount: 1
    perSiteScaling: false
    reserved: false
    targetWorkerCount: 0
    targetWorkerSizeId: 0
    zoneRedundant: false
  }
}

@description('Resource representing the function app.')
resource funcApp 'Microsoft.Web/sites@2022-03-01' = {
  name: functionAppName
  location: location
  kind: 'functionapp'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    clientAffinityEnabled: false
    clientCertEnabled: false
    containerSize: 1536
    dailyMemoryTimeQuota: 0
    enabled: true
    hostNamesDisabled: false
    hostNameSslStates: [
      {
        name: '${functionAppName}.azurewebsites.net'
        hostType: 'Standard'
        sslState: 'Disabled'
      }
      {
        name: '${functionAppName}.scm.azurewebsites.net'
        hostType: 'Repository'
        sslState: 'Disabled'
      }
    ]
    httpsOnly: true
    hyperV: false
    keyVaultReferenceIdentity: 'SystemAssigned'
    publicNetworkAccess: 'Enabled'
    redundancyMode: 'None'
    reserved: false
    scmSiteAlsoStopped: false
    serverFarmId: funcAppServerFarm.id
    siteConfig: {
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      http20Enabled: true
      functionAppScaleLimit: 200
      minimumElasticInstanceCount: 0
    }
    storageAccountRequired: false
  }

  resource funcAppConfigWeb 'config' = {
    name: 'web'
    properties: {
      acrUseManagedIdentityCreds: false
      alwaysOn: false
      appSettings: [
        {
          name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
          value: appInsights.properties.InstrumentationKey
        }
        {
          name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
          value: appInsights.properties.ConnectionString
        }
        {
          name: 'AzureWebJobsStorage'
          value: storageAccountConnectionStr
        }
        {
          name: 'BlobName'
          value: 'eventsprod/{0}/{1}.csv'
        }
        {
          name: 'BlobStorage__serviceUri'
          value: 'https://${stoAcc.name}.blob.${environment().suffixes.storage}'
        }
        {
          name: 'EventGridEndpoint'
          value: eventGrid.properties.endpoint
        }
        {
          name: 'EventGridKey'
          value: eventGrid.listKeys().key1
        }
        {
          name: 'FUNCTIONS_EXTENSION_VERSION'
          value: '~4'
        }
        {
          name: 'FUNCTIONS_WORKER_RUNTIME'
          value: 'dotnet'
        }
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
        {
          name: 'WEBSITE_CONTENTAZUREFILECONNECTIONSTRING'
          value: storageAccountConnectionStr
        }
        {
          name: 'WEBSITE_CONTENTSHARE'
          value: '${functionAppName}${take(uniqueString(functionAppName), 5)}'
        }
      ]
      azureStorageAccounts: {}
      autoHealEnabled: false
      cors: {
        allowedOrigins: [
          'https://portal.azure.com'
        ]
        supportCredentials: false
      }
      defaultDocuments: [
        'index.htm'
        'index.html'
      ]
      detailedErrorLoggingEnabled: false
      experiments: {
        rampUpRules: []
      }
      ftpsState: 'Disabled'
      functionAppScaleLimit: 200
      functionsRuntimeScaleMonitoringEnabled: false
      http20Enabled: true
      httpLoggingEnabled: true
      ipSecurityRestrictions: [
        {
          action: 'Allow'
          description: 'Allow all access'
          ipAddress: 'Any'
          name: 'Allow all'
          priority: 2147483647
        }
      ]
      loadBalancing: 'LeastRequests'
      localMySqlEnabled: false
      logsDirectorySizeLimit: 35
      managedPipelineMode: 'Integrated'
      minimumElasticInstanceCount: 0
      minTlsVersion: '1.2'
      netFrameworkVersion: 'v6.0'
      preWarmedInstanceCount: 0
      publicNetworkAccess: 'Enabled'
      publishingUsername: '$${functionAppName}'
      remoteDebuggingEnabled: false
      requestTracingEnabled: false
      scmIpSecurityRestrictions: [
        {
          action: 'Allow'
          description: 'Allow all access'
          ipAddress: 'Any'
          name: 'Allow all'
          priority: 2147483647
        }
      ]
      scmIpSecurityRestrictionsUseMain: false
      scmMinTlsVersion: '1.2'
      scmType: 'None'
      use32BitWorkerProcess: false
      virtualApplications: [
        {
          physicalPath: 'site\\wwwroot'
          preloadEnabled: false
          virtualPath: '/'
        }
      ]
      vnetPrivatePortsCount: 0
      vnetRouteAllEnabled: false
      webSocketsEnabled: false
    }
  }
}

@description('Represents the Storage Blob Data Owner role for the storage account.')
resource stoBlobDataOwnerRole 'Microsoft.Authorization/roleDefinitions@2022-04-01' existing = {
  scope: stoAcc
  name: 'b7e6dc6d-f1e8-4753-8033-0f276bb0955b'
}

@description('Represents the RBAC assignment for the function app to the Storage Blob Data Owner role.')
resource storageRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  scope: stoAcc
  name: guid(stoAcc.id, resourceGroup().id, stoBlobDataOwnerRole.id)
  properties: {
    principalId: funcApp.identity.principalId
    roleDefinitionId: stoBlobDataOwnerRole.id
    principalType: 'ServicePrincipal'
  }
}

output FunctionWebhookUrl string = 'https://${funcApp.properties.defaultHostName}/api/agentstatus'
output RelayNamespace string = relayNs.name
output HybridConnectionName string = relayNs::relayNsHybridConn.name
output SasKeyName string = relayNs::relayNsHybridConn::relayNsHybridConnKey.name

