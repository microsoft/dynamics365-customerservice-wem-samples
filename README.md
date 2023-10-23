# Dynamics 365 Workforce Management for Customer Service samples

This repository provides sample code and documentation related to Dynamics 365 Workforce Management for Customer Service. For documentation on Dynamics 365 Customer Service, please visit <https://learn.microsoft.com/dynamics365/customer-service>.

## Integration

Dynamics 365 Workforce Management for Customer Service allows 3rd party services to easily integrate and provide additional capabilities. Integration can be done in the following ways:

- **Export** - This allows 3rd party services to export data out of Dynamics 365 Customer Service, to provide additional capabilities or to integrate with other 3rd party service providers. Export can be done in the following ways:
  - [**Realtime**](/doc/3rdPartyIntegration/Export/Realtime/readme.md) - This allows event driven, near real time, data export to external systems.
  - [**Historical**](/doc/3rdPartyIntegration/Export/Historic/readme.md) - This allows export and processing of historical data, to external systems.

- [**Import**](/doc/3rdPartyIntegration/Import/readme.md) - This allows 3rd party services to import data into Dynamics 365 Workforce Management for Customer Service, from external systems.

## Code Samples

The following sample codes are available in the repository:

- [Realtime export integration](https://github.com/microsoft/dynamics365-customerservice-wem-samples/tree/main/src/3rdPartyIntegration/Export/Realtime)
- [Historical export integration](https://github.com/microsoft/dynamics365-customerservice-wem-samples/tree/main/src/3rdPartyIntegration/Export/Historical)

### Realtime export integration

This sample code demonstrates how users can integrate with Dynamics 365 Customer Service, to export event driven data, in near real time. For more information on real time export integration, please refer to the [Realtime export integration](/doc/3rdPartyIntegration/Export/Realtime/readme.md) document.

### Historical export integration

This sample code provides a Jupyter Notebook, that can be used to process historical data that has been exported to an external data store, such as Azure Data Lake. For more information on historical export integration, please refer to the [Historical export integration](/doc/3rdPartyIntegration/Export/Historic/readme.md) document.

## Related sample repositories

- Dynamics 365 Apps samples - <https://github.com/microsoft/Dynamics365-Apps-Samples>
- Power Apps and Dataverse samples - <https://github.com/Microsoft/PowerApps-Samples>

## Related resources

- [Code of conduct](/CODE_OF_CONDUCT.md)
  - [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct)
- [Contributing](/CONTRIBUTING.md)
- [License](/LICENSE)
- [Reporting security issues](/SECURITY.md)
- [Getting support](/SUPPORT.md)
