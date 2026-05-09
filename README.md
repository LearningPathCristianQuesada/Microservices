# Microservices Architecture with API Gateway

## Overview

This project implements a simple microservices architecture using ASP.NET Core and Ocelot API Gateway.

The solution contains:

- ProductService
- OrderService
- API Gateway

The goal of the project is to demonstrate:

- Microservices separation
- Communication between services using HTTP
- API Gateway routing with Ocelot

---

# Architecture

```text
Client
   ↓
API Gateway (Ocelot)
   ↓             ↓
ProductService   OrderService
                      ↓
             HTTP call to ProductService
```

---

# Project Structure

```text
MicroservicesSolution/
│
├── ProductService/
├── OrderService/
└── ApiGateway/
```

---

# ProductService

The ProductService manages the product catalog.

## Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | /products | Get all products |
| GET | /products/{id} | Get product by ID |
| POST | /products | Create a product |

---

# OrderService

The OrderService handles order creation.

When an order is created:

1. OrderService sends an HTTP request to ProductService
2. Product existence is validated
3. Total price is calculated

## Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | /orders | Get all orders |
| POST | /orders | Create an order |

---

# API Gateway

Acts as a single entry point for the client.

The gateway uses Ocelot to route requests to the correct microservice.

## Routes

| External Route | Destination Service |
|---|---|
| /api/products | ProductService |
| /api/orders | OrderService |

---
# Validations Implemented

## ProductService

- Product name cannot be empty
- Product price must be greater than 0

## OrderService

- Quantity must be greater than 0
- Product must exist before creating an order

---

# Running the Application

Three applications must run simultaneously:

| Project | Port |
|---|---|
| ProductService | 5001 |
| OrderService | 5002 |
| ApiGateway | 5000 |

---

# Testing

## 1. Get Products

### Request

```http
GET http://localhost:5000/api/products
```

### Screenshot

> Add screenshot here

---

## 2. Create Product

### Request

```http
POST http://localhost:5000/api/products
```

### Body

```json
{
  "name": "Monitor",
  "price": 900
}
```

### Screenshot

> Add screenshot here

---

# 3. Create Valid Order

### Request

```http
POST http://localhost:5000/api/orders
```

### Body

```json
{
  "productId": 1,
  "quantity": 2
}
```

### Expected Result

- Order created successfully
- TotalPrice calculated automatically

### Screenshot

> Add screenshot here

---

# 4. Create Invalid Order

### Request

```http
POST http://localhost:5000/api/orders
```

### Body

```json
{
  "productId": 999,
  "quantity": 2
}
```

### Expected Result

- Error response because product does not exist

### Screenshot

> Add screenshot here

---