# Project Brief

## Project Name
demo01 — Order Management REST API

## Core Requirements
- Build a RESTful API for creating and managing orders
- Use ASP.NET Core 9 Minimal API pattern
- PostgreSQL database via Entity Framework Core + Npgsql
- Follow REST API best practices (HATEOAS, proper status codes, versioned endpoints)

## Goals
- Implement CRUD operations for Orders starting with POST `/api/v1/orders`
- Clean separation of concerns: Endpoints → Services → Data layer
- Comprehensive test coverage with xUnit integration tests
- Security review and coding guidelines via GitHub Copilot agents/instructions

## Scope
- **In scope:** Order creation (201), error handling, validation, authentication, API versioning
- **Out of scope (for now):** UI, deployment pipelines, multi-tenancy
