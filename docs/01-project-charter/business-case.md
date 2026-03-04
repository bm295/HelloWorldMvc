# Business Case - MilkCO FnB Management API

## Overview
MilkCO operates a 40-seat restaurant and needs reliable control of inventory, order capture, and payment tracking. The current repository already provides a functional API baseline and database model, but formal project authorization and delivery criteria are required to move from code baseline to controlled rollout.

## Problem Statement
- Manual or disconnected tracking can cause stock discrepancies and slower service.
- Order and payment records are harder to reconcile without a centralized backend.
- Existing implementation needs governance, success measures, and rollout planning before operational adoption.

## Proposed Solution
Use the existing .NET 10 Web API and SQL Server stack in this repository as the MVP backend:
- Inventory management API (`/api/inventory`)
- Order API with inventory deduction and stock validation (`/api/orders`)
- Payment API (`/api/payments`)
- Health endpoint (`/api/health`)
- Docker-based startup for repeatable local/pilot deployment

## Strategic Fit
- Aligns with MilkCO operational goals: faster service, fewer stock mistakes, and clearer transaction records.
- Creates a backend foundation that can support future POS UI channels.

## Options Considered
1. Do nothing: keep manual flow and fragmented records.
2. Spreadsheet-first process: lower short-term effort, limited data integrity and automation.
3. API-first backend (recommended): better control, extensibility, and process consistency.

## Expected Benefits
- Reduced inventory mismatch from automatic stock deduction during order creation.
- Faster order handling due to centralized API workflows.
- Better traceability of orders and payments.
- Repeatable onboarding and environment setup through Docker.

## Cost and Effort Assumptions
This repository already contains the MVP implementation. Remaining delivery effort is primarily stabilization and rollout:
- API hardening and validation: 1 week
- UAT and fixes: 1 week
- Pilot deployment and adoption support: 1 week

## Recommendation
Proceed with Option 3 (API-first backend) and approve the charter to execute stabilization, UAT, and pilot rollout under controlled scope.

## Approval Required
- Sponsor approval: Restaurant Owner
- Delivery accountability: PM/BA with Technical Lead
