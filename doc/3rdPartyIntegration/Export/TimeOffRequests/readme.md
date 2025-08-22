# Exporting Time off data from Dynamics 365 Workforce Management

## In this article

- [Exporting Time off data from Dynamics 365 Workforce Management](#exporting-time-off-data-from-dynamics-365-workforce-management)
  - [In this article](#in-this-article)
  - [Introduction](#introduction)
  - [Sample code](#sample-code)
  - [Prerequisites](#prerequisites)
  - [Update app config](#update-app-config)
  - [Sample Data Setup](#sample-data-setup)
    - [Enable required WEM features](#enable-required-wem-features)
    - [Verify manager assignment](#verify-manager-assignment)
    - [Define Time-Off types](#define-time-off-types)
    - [Create Time-Off requests](#create-time-off-requests)
    - [Act on Time-Off requests using manager login](#act-on-time-off-requests-using-manager-login)
  - [Sample Code Overview](#sample-code-overview)

## Introduction

This project is a .NET 9 console application designed to interact with Microsoft Dynamics 365 using both REST APIs and the Dataverse client SDK.
It retrieves and displays data related to "Time Off Types" and "Time Off Requests."

[Return to top](#in-this-article)

## Sample code

The sample code for this topic is located in the [GitHub code repository](https://github.com/microsoft/dynamics365-customerservice-wem-samples/tree/main/src/3rdPartyIntegration/Export/TimeOffRequests) in the **src/3rdPartyIntegration/Export/TimeOffRequests** folder.

[Return to top](#in-this-article)

## Prerequisites

In order to run this demo, you need the following prerequisites:

- An active Dynamics 365 Customer Service org with Omnichannel installed. You must have administrator permissions for this org.

 [Return to top](#in-this-article)


## Update app config

Update your sample integration code config with the following values, in the `appSettings.json` file:

- **TenantId**: Azure Active Directory tenant id for client app registration.
- **ClientId**: Azure Active Directory application (client) ID.
- **CertThumbprint**: The certificate thumbprint for the generated cert.
- **DynamicsUrl**: The Dynamics 365 organization url without a trailing slash ("/").

[Return to top](#in-this-article)

## Sample Data Setup

### Enable required WEM features
If not already done, enable required channels in `CSAC > Channels > Manage Channels`.   

### Verify manager assignment
Ensure the user has a manager assigned in  `CSAC > User Management > [Select User] > Organization Information > Manager`.
  > [!IMPORTANT]
  > An user cannot raise a Time Off Request (TOR), without having a manager.

### Define Time-Off types
Add time-off types under `CSAC > Workforce Management > Time Management > Time-off Request Types`.

### Create Time-Off requests
Log in with few users and raise time-off requests under `Copilot Service Workspace > Workforce Management > Request Management`.

### Act on Time-Off requests using manager login
Approve or deny requests under  `Copilot Service Workspace > Workforce Management > Request Management` using a **Manager** login.

[Return to top](#in-this-article)

## Sample Code Overview
The application shows how to use the [Dataverse client SDK](https://learn.microsoft.com/dotnet/api/microsoft.powerplatform.dataverse.client.serviceclient?view=dataverse-sdk-latest) to query for the Time Off Types and the Time Off Requests. The provided sample is a console application written in C#.

The sample code contains two methods under `DataverseClient.cs`.

- **GetTimeOffTypes**: Queries the `msdyn_timeofftype` table to get the list of defined Time Off types. Finally it prints it on the console.
- **GetTimeOffRequests**: Queries the `msdyn_wemrequest` table to get the list of all time off requests. Additionally you can pass in a flag to get only approved requests. Finally it prints teh requests on the console.

> [!NOTE]
> You can also use the OData Web API to retrieve the data from Dataverse. This is not shown in the sample application. More information on the Web API can be found [here](https://learn.microsoft.com/power-apps/developer/data-platform/webapi/overview).

[Return to top](#in-this-article)