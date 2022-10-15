# CategoriesApi

**Simple Category API creation example**

Used: Swagger (documentation), .NET 6, Dapper (micro ORM), HTTP-based caching, SQL Server (Database)

Access path for documentation: */swagger/index.html*

Example of documentation presentation.
[![Swagger documentation](https://prnt.sc/ZfquhsCR6FQr "Swagger documentation")](https://prnt.sc/ZfquhsCR6FQr "Swagger documentation")

Category visibility update api call example
[![Swagger example](https://prnt.sc/F3FWTtAjQY67 "Swagger example")](https://prnt.sc/F3FWTtAjQY67 "Swagger example")

**GET - /api/categories**
`Allows you to list all registered categories, being able to filter by Id or Slug.`

**POST - /api/categories** 
`Create a new category.`

**GET - /api/categories/detail**
`See the details of a category by filtering by Id or Slug.`

**GET - /api/categories/categoryChildren/{id}**
`Query the children of a specific category filtered by ID.`

**PATCH - /api/categories/updateVisibility/{id}**
`Updates the "visible" field for a specific category.`

**DELETE - /api/categories/{id}**
`Performs a physical deletion of the category.`

Cache time set to 30 seconds in all categories listing

Database template script:

```sql
CREATE DATABASE CategoriesDB
USE [CategoriesDB]

CREATE TABLE Categories
(
	Id VARCHAR(40),
	[Name] VARCHAR(100),
	Slug VARCHAR(100),
	ParentId VARCHAR(40),
	Visible BIT
)
```

Database configuration in appsettings.json:

```json
"ConnectionStrings": {
    "MSSQL": "Server=DESKTOP-FMPCMMB\\SQLEXPRESS;Database=CategoriesDB;Trusted_Connection=True;"
  }
```
