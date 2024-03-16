# Courier Hub - A Platform for Efficient Courier Services

## Introduction

The Courier Hub project is aimed at creating a versatile platform that enables users to request courier services from various sources and companies. This README outlines the key features and requirements of the project.

## Table of Contents

1. [Project Description](#project-description)
2. [Key Features](#key-features)
3. [User Roles](#user-roles)
4. [Technical Requirements](#technical-requirements)
5. [API Documentation](#api-documentation)

## Project Description

The Courier Hub is designed to be a central hub for requesting and managing courier services. Here's an overview of the main functionalities:

- Users can interact with the system through a website.
- The landing page provides essential information about the platform, including its name, a brief description, and an option to create delivery requests anonymously.
- Each delivery request includes package dimensions, weight, delivery date, source and destination addresses, priority (low or high), and weekend delivery options.
- The application waits for the best offers from integrated courier suppliers, displaying them in a list.
- Users can log in or use anonymous mode to proceed.
- Anonymous users provide their contact information, including first name, last name, company name, email, and address.
- A summary page displays all the cost breakdown details, and users can submit the request.
- Clients receive a notification when their request is submitted.
- Upon acceptance, the client receives an email with a contract agreement and receipt.
- After delivery, clients can rate the service.
- Logged-in users have their address information automatically populated.
- Users can view their order history and inquiry details.
- Couriers can view and update the status of deliveries.
- Office workers can manage inquiries and accept or reject offers.

## Key Features

### Business Part 1: Client
- Registration and login options, including third-party authentication.
- List of the last 30 days' inquiries, including package details, source/destination, date, and status.
- Creation of new inquiries with package details, pickup/delivery dates, company status, priority, and weekend delivery.
- Email notifications upon inquiry submission.
- Viewing and accepting valid offers.
- Requirement to provide personal or company data.

### Business Part 2: Courier Company Office Worker
- Viewing and managing inquiries for the company.
- Filtering and sorting inquiries.
- Handling offer requests, including accepting and rejecting.
- Providing agreements and reasons for rejections.

### Business Part 3: Courier
- Viewing all delivery requests.
- Filtering and sorting delivery requests.
- Updating delivery statuses, including pickup, delivery, and inability to deliver.

### Business Part 4: API Functions
- Creating inquiries with authentication and required fields.
- Creating offers with authentication and required field checks.
- Checking the status of offers with authentication.
- Listing inquiries and offers, available only to Courier Company Office Workers.

## User Roles

1. **Client:** Registered users with the ability to request courier services and manage inquiries.
2. **Courier Company Office Worker:** Employees of courier companies who manage inquiries and offer requests.
3. **Courier:** Couriers who handle delivery requests and update delivery statuses.

## Technical Requirements

- Implementation of a web-based UI with registration and authentication, supporting AzureAD and other OAuth providers.
- Development of a custom Courier API for managing inquiries and offers.
- Integration with at least one external API provided by another group.
- Creation of an API for authorizing and handling inquiries, offers, and deliveries.
- Implementation of Swagger documentation for the API.

## API Documentation

The API documentation for the Courier Hub project can be found in the Swagger documentation provided.

For more detailed technical information, please refer to the Swagger documentation and the project's source code.

This README provides an overview of the Courier Hub project, its features, and key requirements. Please refer to the project's source code and documentation for implementation details and updates.

**Note:** Additional API details provided by the lecturer will be integrated into the project as necessary.
