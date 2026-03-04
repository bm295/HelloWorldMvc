# 01. Develop Project Charter

## Metadata
- Project: MilkCO FnB Management API (40-seat restaurant)
- Knowledge Area: Integration
- Process Group: Initiating
- Document Owner: PM/BA
- Last Updated: 2026-03-04
- Status: Draft for sponsor approval

## Objective
Authorize and align the MilkCO FnB Management API initiative to digitize core restaurant operations (inventory, order capture, and payments), reduce manual errors, and provide a stable technical baseline for later POS extensions.

## Inputs
- Product and technical baseline:
  - [Repository Overview](../../README.md)
  - [Solution File](../../HelloWorldMvc.sln)
  - [API Startup and DI](../../WebApplication/Program.cs)
  - [Domain Models](../../WebApplication/Models)
  - [Order Workflow and Inventory Deduction](../../WebApplication/Services/OrderService.cs)
  - [Container Deployment](../../docker-compose.yml)
- Process and governance baseline:
  - [PMP Process Library](../README.md)
  - [Develop Project Management Plan](./02-develop-project-management-plan.md)
  - [Identify Stakeholders](./49-identify-stakeholders.md)

## Tools & Techniques
- Stakeholder workshops with sponsor, operations, and cashier representatives.
- Document analysis of existing API code, data model, and deployment configuration.
- Expert judgment from PM/BA and technical lead.
- High-level estimation for API hardening, UAT, and pilot release.
- Qualitative risk assessment using probability-impact scoring.

## Charter Statement
### Business Need
MilkCO needs a lightweight backend system for a 40-seat restaurant to control stock, capture orders reliably, and track payments in one place.

### High-Level Scope
In scope:
- REST API endpoints for health, inventory, orders, and payments.
- SQL Server persistence via Entity Framework Core migrations.
- Business rule: creating an order decrements inventory and blocks when stock is insufficient.
- Dockerized local environment for repeatable startup.

Out of scope for this phase:
- Frontend POS user interface.
- Authentication and role-based access control.
- Advanced analytics/reporting and accounting integrations.
- Multi-branch or multi-tenant capabilities.

### Measurable Objectives
- Complete charter approval and baseline by March 8, 2026.
- Reach UAT-ready API baseline with acceptance checks by March 22, 2026.
- Demonstrate end-to-end order, inventory, and payment flow in pilot by March 29, 2026.
- Keep build and deployment reproducible through `dotnet build` and `docker compose up --build`.

### Initial Constraints
- Named team capacity is not fully finalized.
- Current solution depends on SQL Server and .NET 10 runtime compatibility.
- Security controls are minimal and require a follow-on scope.

### Initial Assumptions
- Sponsor and operations stakeholders are available weekly for decisions.
- Existing API implementation is accepted as MVP baseline.
- Pilot environment can run Docker services for API and SQL Server.

## Outputs
- [Business Case](../01-project-charter/business-case.md)
- [Success Criteria](../01-project-charter/success-criteria.md)
- [Milestone List](../01-project-charter/milestone-list.md)
- [Risk Register](../01-project-charter/risk-register.md)
- [Stakeholder List](../01-project-charter/stakeholder-list.md)

## AI Agent Prompt Seed
```text
You are the PM Assistant for the MilkCO FnB Management API project.
Complete the Develop Project Charter process using the linked inputs,
then propose actions, owners, deadlines, and major risks for sponsor review.
```
