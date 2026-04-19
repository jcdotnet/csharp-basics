# 🛒 Cloud-Native eCommerce App
*A distributed microservices platform built with ASP.NET Core, showcasing diverse architectural patterns and Azure DevOps integration.*

This project demonstrates a real-world microservices implementation, where each service is built using the specific architecture and database engine that best fits its functional requirements.

## 🏗️ Microservices Breakdown
*   **Users Microservice**: **Clean Architecture** | **Postgres** + **Dapper**.
    *   *Azure DevOps (CI/CD & Infrastructure):* [Repo Link](https://github.com/jcdotnet/azure-devops-users-microservice)
*   **Products Microservice**: **N-Tier Architecture** | **MySQL** + **EF Core**.
    *   *Azure DevOps (CI/CD & Infrastructure):* [Repo Link](https://github.com/jcdotnet/azure-devops-products-microservice)
*   **Orders Microservice**: **N-Tier Architecture** | **MongoDB** + **EF Core**.
    *   *Azure DevOps (CI/CD & Infrastructure):* [Repo Link](https://github.com/jcdotnet/azure-devops-orders-microservice)

## 🔄 Project Evolution (Git Branches)
I used different branches to show how the app grows from basic containers to a full cloud setup. Check the branches to see:
1.  **Communication**: Switching from Synchronous (HTTP) to Asynchronous messaging.
2.  **Deployment**: Evolution from local **Docker** to **Kubernetes** and finally **Azure AKS**.

## 🚀 Key Technical Highlights
*   **Database Diversity**: I used different engines (Postgres, MySQL, and MongoDB) depending on the service needs.
*   **Communication**:  Implementation of API Gateways and resilient communication between services.
*   **DevOps**: Fully automated CI/CD pipelines hosted in dedicated Azure DevOps repositories.