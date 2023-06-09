# LeadYourWay API Endpoints

## Description
LeadYourWay is a startup project for school, which is a platform similar to Airbnb but for bikes. It is built using the Entity Framework and MySQL database. The backend API provides endpoints for managing users, bikes, and cards.

Base URL: `https://leadyourway.azurewebsites.net/`

## Users

- **GET:** `/api/User`
  - Retrieves a list of all users.
- **GET:** `/api/User/{id}`
  - Retrieves a specific user by ID.
- **POST:** `/api/User/Login`
  - Logs in a user by validating the login credentials.
- **POST:** `/api/User`
  - Creates a new user.
- **PUT:** `/api/User/{id}`
  - Updates an existing user.
- **DELETE:** `/api/User/{id}`
  - Deletes a user by ID.

## Bicycles

- **GET:** `/api/Bicycle`
  - Retrieves a list of all bicycles.
- **GET:** `/api/Bicycle/{id}`
  - Retrieves a specific bicycle by ID.
- **GET:** `/api/Bicycle/filterByUserId/{id}`
  - Retrieves bicycles filtered by user ID.
- **POST:** `/api/Bicycle`
  - Creates a new bicycle.
- **PUT:** `/api/Bicycle/{id}`
  - Updates an existing bicycle.
- **DELETE:** `/api/Bicycle/{id}`
  - Deletes a bicycle by ID.

## Cards

- **GET:** `/api/Card`
  - Retrieves a list of all cards.
- **GET:** `/api/Card/{id}`
  - Retrieves a specific card by ID.
- **GET:** `/api/Card/filterByUserId/{id}`
  - Retrieves cards filtered by user ID.
- **POST:** `/api/Card`
  - Creates a new card.
- **PUT:** `/api/Card/{id}`
  - Updates an existing card.
- **DELETE:** `/api/Card/{id}`
  - Deletes a card by ID.
