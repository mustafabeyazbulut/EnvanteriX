# Inventory and Service Tracking System

Welcome to the **Inventory and Service Tracking System**, a comprehensive solution designed to efficiently manage and track assets, their movements, maintenance, and related services within an organization. This project provides robust features to maintain detailed records and streamline the management of hardware, software, and other inventory items.

## Technologies & Architecture

- Developed with **.NET Core 9**
- Implements **Onion Architecture** for a clean separation of concerns
- Uses **CQRS (Command Query Responsibility Segregation)** for command and query separation
- Utilizes **Mediator Design Pattern** via MediatR for decoupled request handling
- Applies **Repository Pattern** to abstract data access logic
- Built with **.NET Core 9 MVC** for the web application layer

## Key Features

- **Asset Management:** Track all assets with detailed information, including brand, model, and location.
- **Asset Movement:** Monitor the transfer and location changes of assets over time.
- **Maintenance Tracking:** Keep logs of all maintenance activities to ensure assets are properly serviced and operational.
- **User & Role Management:** Manage system users with defined roles for controlled access and permissions.
- **Software License Management:** Track software licenses associated with assets to ensure compliance.
- **Vendor Management:** Keep vendor information organized for procurement and service purposes.

## Database Tables Overview

| Table Name           | Description                                         |
|----------------------|-----------------------------------------------------|
| **Asset**            | Records of all physical or digital assets.          |
| **AssetMovement**    | Logs of asset transfers and location changes.        |
| **Brand**            | Information about asset brands.                      |
| **Location**         | Details of asset storage or usage locations.        |
| **MaintenanceRecord** | History and details of asset maintenance activities. |
| **Model**            | Specific model information for assets.              |
| **Role**             | User roles defining access levels and permissions.  |
| **SoftwareLicense**  | Information on software licenses assigned to assets.|
| **User**             | System users who interact with the platform.        |
| **Vendor**           | Vendor details for asset procurement and services.  |

## Getting Started

*Instructions for installation, configuration, and running the application will go here.*

## Contributing

*Guidelines for contributing to the project.*

## License

*Specify the project license here.*
