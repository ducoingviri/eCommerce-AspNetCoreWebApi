
<h1 align="center">
  eCommerce APIs with .Net 7.0
</h1>

<h4 align="center">Basic endpoints to implement an eCommerce platform</h4>

## About

This is a project built to expose endpoints that can be useful for an eCommerce platform.
It integrates authentication and authorization using JWT.

## How To Use

Use your favorite IDE to run this project.

> **Note**
> Consider this repo is at project level, not at solution level.

## Steps

1. Run the application
2. Register a user using the sign-up endpoint
3. Login with the created user using the sign-in endpoint
4. Save the generated token
5. Invoke the product store endpoint (which is configured to work only with authorization token) to save a product
6. Invoke the product index endpoint (which is configured to work for everyone, means without authorization token) to list all products

### HTTP requests

#### Request to register the apple.2585 user

```
POST http://localhost:5068/api/Authenticate/register
Content-Type: application/json

{
"username": "apple.2585",
"email": "apple.2585@hotmail.com",
"password": "Apple@2585"
}
```

#### Request to register the kiwi.2506 user

```
POST http://localhost:5068/api/Authenticate/register
Content-Type: application/json

{
"username": "kiwi.2506",
"email": "kiwi.2506@hotmail.com",
"password": "Kiwi@2506"
}
```

#### Request to sign-in with the apple.2585 user

```
POST http://localhost:5068/api/Authenticate/login
Content-Type: application/json

{
  "username": "apple.2585",
  "password": "Apple@2585"
}
```

Response:

```
{
"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXBwbGUuMjU4NSIsImp0aSI6IjdmMjIwYTFkLTk1MGEtNDY1OC04OTBkLWYzYjU1MTg2ZDdiMCIsImV4cCI6MTY3NDUxODM1NSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDY4IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.-I2kK2amHjeqgfMilXt4oigoJaVnB4ZXz_apdDhTgFc",
"expiration": "2023-01-23T23:59:15Z"
}
```

#### Request to store some products

```
POST http://localhost:5068/api/Product
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXBwbGUuMjU4NSIsImp0aSI6IjdmMjIwYTFkLTk1MGEtNDY1OC04OTBkLWYzYjU1MTg2ZDdiMCIsImV4cCI6MTY3NDUxODM1NSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDY4IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.-I2kK2amHjeqgfMilXt4oigoJaVnB4ZXz_apdDhTgFc

{
"code": "BL2852",
"description": "Black Box (5 in x 2 in x 6 in) - Wood 50th Ed.",
"price": 140,
"photo": "pic.com",
"stock": 36
}
```

```
POST http://localhost:5068/api/Product
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXBwbGUuMjU4NSIsImp0aSI6IjdmMjIwYTFkLTk1MGEtNDY1OC04OTBkLWYzYjU1MTg2ZDdiMCIsImV4cCI6MTY3NDUxODM1NSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDY4IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.-I2kK2amHjeqgfMilXt4oigoJaVnB4ZXz_apdDhTgFc

{
"code": "BL2999",
"description": "White Box (5 in x 2 in x 6 in) - Wood 50th Ed.",
"price": 115,
"photo": "pic.com",
"stock": 11
}
```

#### Request to fetch all products

```
GET http://localhost:5068/api/Product
```

Response:

```
[
  {
    "id": 1,
    "code": "XS23",
    "description": "Camera 12MP + Bluetooth & WiFi Comp. Black Ed.",
    "price": 100.00,
    "photo": "string.pic",
    "stock": 12,
    "inventories": [],
    "sales": []
  },
  {
    "id": 2,
    "code": "WF28",
    "description": "TV Full HD 4K 50 inch.",
    "price": 1200.00,
    "photo": "string.pic",
    "stock": 6,
    "inventories": [],
    "sales": []
  },
  {
    "id": 3,
    "code": "PO86",
    "description": "Laptop ASUS ROG 16'' - 16GB RAM - 1TB HDD",
    "price": 24000.00,
    "photo": "string.pic",
    "stock": 3,
    "inventories": [],
    "sales": []
  },
  {
    "id": 4,
    "code": "BL2852",
    "description": "Black Box (5 in x 2 in x 6 in) - Wood 50th Ed.",
    "price": 140.00,
    "photo": "pic.com",
    "stock": 36,
    "inventories": [],
    "sales": []
  },
  {
    "id": 5,
    "code": "BL2999",
    "description": "White Box (5 in x 2 in x 6 in) - Wood 50th Ed.",
    "price": 115.00,
    "photo": "pic.com",
    "stock": 11,
    "inventories": [],
    "sales": []
  }
]
```

