# Integrate Dynamics 365 Customer Service with third-party workforce management

This repository provides sample code and documentation related to integrating third-party workforce management with Dynamics 365 Customer Service.

## Integration

Dynamics 365 Customer Service allows third-party services to easily integrate and provide additional capabilities. Integration can be done in the following ways:

- **Export**: This allows third-party services to export data from Dynamics 365 Customer Service, to provide additional capabilities, or to integrate with other third-party service providers. An export can be done in the following ways:
  - [**Realtime**](/doc/3rdPartyIntegration/Export/Realtime/readme.md): This allows an event-driven, near real time data export to external systems.
  - [**Historical**](/doc/3rdPartyIntegration/Export/Historic/readme.md): This allows an export and processing of historical data to external systems.

- [**Schedule Import**](/doc/3rdPartyIntegration/Import/readme.md): This allows third-party services to import schedule data into Dynamics 365 Customer Service from external systems.

## Code samples

The following sample codes are available in the repository:

- [Realtime export integration](https://github.com/microsoft/dynamics365-customerservice-wem-samples/tree/main/src/3rdPartyIntegration/Export/Realtime)
- [Historical export integration](https://github.com/microsoft/dynamics365-customerservice-wem-samples/tree/main/src/3rdPartyIntegration/Export/Historical)

### Realtime export integration

This sample code shows how users can integrate with Dynamics 365 Customer Service to export event-driven data in near real time. For more information on real time export integration, see [Realtime export integration](/doc/3rdPartyIntegration/Export/Realtime/readme.md).

### Historical export integration

This sample code provides a [Jupyter Notebook](https://jupyter.org/) that can be used to process historical data that's been exported to an external data store, such as Azure Data Lake. For more information on historical export integration, see [Historical export integration](/doc/3rdPartyIntegration/Export/Historic/readme.md).

## Related sample repositories

- Dynamics 365 Apps samples: <https://github.com/microsoft/Dynamics365-Apps-Samples>
- Power Apps and Dataverse samples: <https://github.com/Microsoft/PowerApps-Samples>

## Related resources

- [Code of conduct](/CODE_OF_CONDUCT.md)
  - [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct)
- [Contributing](/CONTRIBUTING.md)
- [License](/LICENSE)
- [Reporting security issues](/SECURITY.md)
- [Getting support](/SUPPORT.md)
- [Dynamics 365 Customer Service documentation](https://learn.microsoft.com/dynamics365/customer-service).
