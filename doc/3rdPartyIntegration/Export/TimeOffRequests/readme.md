# Dynamics 365 Api based Integration Console Application

## In this article

- Api based export integration with Dynamics 365 Customer Service
    - [Introduction](#introduction)
    - [Sample code](#sample-code)
    - [Prerequisites](#prerequisites)
    - [Azure Entra Client App Setup](#azure-entra-client-app-setup)
    - [Sample Data Setup](#sample-data-setup)
    - [Sample Code Overview](#sample-code-overview)
      - [Key Components](#1-key-components)
      - [Workflow](#2-workflow)
      - [Comparison of REST and Dataverse Approaches](#3-comparison-of-rest-and-dataverse-approaches)

## Introduction

This project is a .NET 9 console application designed to interact with Microsoft Dynamics 365 using both REST APIs and the Dataverse client SDK.
It retrieves and displays data related to "Time Off Types" and "Time Off Requests."

[Return to top](#in-this-article)

## Sample code

The sample code for this topic is located in the [GitHub code repository](https://github.com/microsoft/dynamics365-customerservice-wem-samples/tree/main/src/3rdPartyIntegration/Export/TimeOffRequests) in the **src/3rdPartyIntegration/Export/TimeOffRequests** folder.

[Return to top](#in-this-article)

## Prerequisites

In order to run this demo, you need the following prerequisites:

- An active Dynamics 365 Customer Service org with Omnichannel installed. You must have adminstrator permissions for this org.
- An active Azure subscription with permissions to create resources in it.

 [Return to top](#in-this-article)

---
# Azure Entra Client App Setup

This guide outlines the steps to register an application in Azure Entra ID, generate and install certificates, configure Dynamics 365 application user, and update your integration code.


## 1. Register an Application in Azure Entra ID

- Go to [Azure Portal](https://portal.azure.com), navigate to **Azure Active Directory > App registrations**, and create a new app registration.
- Record the **Application (Client) ID** and **Tenant ID**.
- Under **Certificates & Secrets**, upload your certificate (typically a `.cer` file for public key) and note the **Thumbprint**.

---

## 2. Generate and Install the Certificate

- For sample testing purposes, you can generate a self-signed certificate using **Azure Key Vault** or tools like **PowerShell** or **OpenSSL**.
- For production, use a **trusted Certificate Authority (CA)** and **secret management stores** like **Azure Key Vault**. Avoid cross-environment sharing and follow other security guidelines for renewal etc.
- Install the certificate (usually `.pfx` format) on the machine or service that will run the integration code.

---

## 3. Create an Application User in Dynamics 365

- In **Dynamics 365**, go to **Settings > Security > Users**, switch the view to **Application Users**, and create a new user.
- Map it to the **Application ID** from your Azure app registration.
- Assign appropriate **security roles** to this user.

---

## 4. Update app config

Update your sample integration code config with the following values, in the `appSettings.json` file:

- **TenantId**: Azure Active Directory tenant id for client app registration.
- **ClientId**: Azure Active Directory application (client) ID.
- **CertThumbprint**: The certificate thumbprint for the generated cert.
- **DynamicsUrl**: The Dynamics 365 organization url without a trailing slash ("/").

[Return to top](#in-this-article)

---
# Sample Data Setup

1. **Enable required WEM features**  
   - If not already done, enable required channels in **CSAC → Channels  →  Manage Channels**   
   - In **CSAC → Workforce Management**, ensure **Schedule Management** is turned on.


2. **Verify manager assignment**  
   - Ensure the user has a manager assigned in  
   **CSAC → User Management → [Select User] → Organization Information → Manager**.  
   > ⚠️ Without a manager, the user cannot raise a Time Off Request (TOR).

3. **Define Time-Off types**  
   - Add time-off types under  
   **CSAC → Workforce Management → Time Management → Time-off Request Types**.

4. **Create Time-Off requests**  
   - Log in with few users and raise time-off requests under  
   **CSW → Request Management**.

5. **Act on Time-Off requests using manager login**  
   - Approve or deny requests under  
   **CSW → Request Management** using a **Manager** login.

[Return to top](#in-this-article)

## Sample Code Overview
The application uses two approaches to interact with Dynamics 365:
- **REST API**: Implemented in `RestClient.cs`.
- **Dataverse SDK**: Implemented in `DataverseClient.cs`.

The entry point is `Program.cs`, which orchestrates the flow by acquiring an access token and invoking methods from both clients.

---

## **1. Key Components**

### **Program.cs**
- **Authentication**:
  - Uses Azure AD to authenticate via a certificate-based approach.
  - Acquires an access token using the `ConfidentialClientApplicationBuilder` from the Microsoft Identity Client library.
- **Data Retrieval**:
  - Calls methods from `RestClient` and `DataverseClient` to fetch and display data.

### **RestClient.cs**
- Provides static methods to interact with Dynamics 365 using HTTP requests:
  - **`GetTimeOffTypes`**:
    - Sends a GET request to fetch all "Time Off Types".
    - Parses and prints the JSON response.
  - **`GetTimeOffRequests`**:
    - Sends a GET request to fetch "Approved Time Off Requests" (filtered by `msdyn_requeststatus eq 4`).
    - Parses and prints the JSON response.

### **DataverseClient.cs**
- Uses the Dataverse SDK to interact with Dynamics 365:
  - **`GetTimeOffTypes`**:
    - Queries the `msdyn_timeofftype` table to retrieve all columns.
    - Prints the retrieved entities.
  - **`GetTimeOffRequests`**:
    - Queries the `msdyn_wemrequest` table to fetch approved requests (`msdyn_requeststatus = 4`).
    - Implements paging to handle large datasets.

---

## **2. Workflow**
1. **Authentication**:
   - The application retrieves a certificate from the local store using its thumbprint.
   - It uses the certificate to authenticate with Azure AD and acquire an access token.

2. **Data Retrieval**:
   - The access token is passed to both `RestClient` and `DataverseClient` methods.
   - Each client fetches and displays data using its respective approach.

3. **Error Handling**:
   - Errors during authentication or data retrieval are caught and logged to the console.

---

## **3. Comparison of REST and Dataverse client approaches**
- **REST API**:
  - Direct HTTP requests.
  - Requires manual construction of URLs and parsing of JSON responses.
- **Dataverse SDK**:
  - Abstracts the underlying API calls.
  - Provides a strongly-typed interface for querying and manipulating data.

[Return to top](#in-this-article)