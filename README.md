# Million Solution – ASP.NET Core 8 + MongoDB

## 📖 Overview

This solution contains two projects:

- **Million.Api** – Main RESTful API built with **.NET 8**, **MongoDB**, and **Clean Architecture principles**.
- **Million.Api.Tests** – Unit, API, and integration tests written with **NUnit** and **Moq**.

The API manages **Owners**, **Properties**, **Property Images**, and **Property Traces**, providing CRUD operations and relationship-based queries.

---

## 🏛 Architecture

The project follows a **Clean Architecture / Layered approach**

**Million.Api** contains:
├── Controllers → HTTP endpoints (REST API)
├── Services → Business logic layer
├── Repositories → Data access layer (MongoDB)
├── Domain → Core entities (Property, Owner, etc.)
├── DTOs → Data transfer objects
├── Infrastructure → MongoDB context, migrations, indexes
└── Middleware → Global error handling

**Million.Api.Tests** contains:

Million.Api.Tests
├── Unit → Unit tests for services
├── Api → Controller/API-level tests
└── Integration → Full in-memory API + DB tests

### The operational logic flow is like this:

Controller → DTO's → Service → Repository → MongoDB


---

## 🛠 Technologies Used

- **.NET 8** – API framework
- **MongoDB.Driver** – MongoDB connection & queries
- **Docker & Docker Compose** – Local MongoDB and API hosting
- **Swagger / Swashbuckle** – API documentation and testing
- **NUnit** – Testing framework
- **Moq** – Mocking dependencies for unit tests

---

## 📦 Dependencies

Before running the project, install:

- **.NET 8 SDK** → [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Docker Desktop** → [Download here](https://www.docker.com/products/docker-desktop/)
- **MongoDB Community Server** → [Download here](https://www.mongodb.com/try/download/community)
- **MongoDB Compass** *(optional)* → For DB visualization [Download here](https://www.mongodb.com/try/download/compass)

---

## 🚀 Running the API

### 1️⃣ Clone the repository
- Run `git clone https://github.com/eduardohurtado/MillionSolution-Backend-NETCore8`
- Run `cd MillionSolution-Backend-NETCore8`

### 2️⃣ Clean, build with .NETCore and prepare MongoDB

- Run `dotnet clean`
- Run `dotnet build`
- Start mongodb community server, in **MacOSX** Run `brew services start mongodb-community`

### 3️⃣ Launch the API with .NETCore or Docker as you prefer

- For **.NETCore** enter path `cd Million.Api/`, then Run `dotnet run` the app listens in [http://localhost:5153/swagger/index.html](http://localhost:5153/swagger/index.html)
- For **Docker** Run `docker-compose up --build` the app listens in [http://localhost:5001/swagger/index.html](http://localhost:5001/swagger/index.html)

**Note:** Mongo seed data is created by MongoMigrations.cs at startup. You can visualize data in MongoDB Compass by connecting to: mongodb://localhost:27017

---

## 📊 Run tests

In the root solution folder you can open a command window and run the test cases based on needs, all test cases are encoded in a shell script so you can execute them with the following commands:

**Run unit tests only**
./run-tests.sh unit

**Run api tests only**
./run-tests.sh api

**Run integration tests only**
./run-tests.sh integration

**Run all tests**
./run-tests.sh all

---

## © Copyright
2025 All rights reserved. Created by Eduardo Hurtado