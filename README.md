# GameShop - ASP.NET Core Web API

## Description

GameShop is an ASP.NET Core Web API project designed for an online store selling computer games. The API is built using best practices, including layered architecture and Data Transfer Objects (DTOs) for data encapsulation. The API uses in-memory data storage and is documented using Open API (Swagger). Token-based authentication is implemented to secure the API.

## Features

- **Game Management:** Add, edit, delete, and view game details.
- **Category Management:** Organize games into categories for easier browsing.
- **Customer Management:** Manage customer information and their orders.
- **In-Memory Data Storage:** Temporary data storage for testing and development purposes.
- **Open API Documentation:** API documentation and testing using Swagger.
- **Token-based Authentication:** Secure API endpoints with JWT tokens.

## Technologies Used

- **Framework:** ASP.NET Core
- **Programming Language:** C#
- **Data Storage:** In-Memory
- **External Libraries:**
  - Swashbuckle for Open API (Swagger) documentation
  - System.IdentityModel.Tokens.Jwt for token-based authentication

## Installation and Usage

### Prerequisites

- .NET Core 3.1 or later

### Installation Steps

1. **Clone the repository:**
   ```sh
   git clone https://github.com/your-username/GameShop.git
### Usage
- Swagger UI: Once the application is running, navigate to http://localhost:5000/swagger in your browser to view and interact with the API documentation.
- API Endpoints: Use the Swagger UI to test the various endpoints for managing games, categories, orders, and customers.
- Authentication: Obtain a JWT token by logging in and use it to authenticate API requests.
### Project Structure
- Controllers: Handle HTTP requests and responses.
- Services: Business logic and operations.
- Repositories: Data access and manipulation.
- Models: Data structures and entities.
- DTOs (Data Transfer Objects): Data encapsulation for input and output.
- Authentication: Token generation and validation.
