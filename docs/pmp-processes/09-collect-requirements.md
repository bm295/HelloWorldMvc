# 09. Collect Requirements

## Metadata
- Restaurant: MilkCO (40 seats)
- Knowledge Area: Scope
- Process Group: Planning
- Document Owner: PM/BA
- Last Updated: 2026-03-10

## Objective
Collect, standardize, and prioritize business and technical requirements for the FoodApp integration to ensure consistent operations between in-store and online orders.

## Inputs
- Current product baseline (inventory, order, payment API).
- New requirement from Sponsor/Ops: support FoodApp channel.
- FoodApp API and partner onboarding documentation.
- Current restaurant operating process: order intake, preparation, delivery, payment, reconciliation.

## Tools & Techniques
- Three-party workshop: Business (Ops/Cashier), Technical team, FoodApp partner.
- User story mapping for the end-to-end FoodApp ordering flow.
- Interface analysis (payload mapping, enum mapping, error code mapping).
- MoSCoW prioritization for the first release.

## Outputs
- Functional requirements:
  - Receive FoodApp orders in real time or via polling fallback.
  - Map products/combos/add-ons from FoodApp to the internal menu.
  - Synchronize order status (accepted/preparing/completed/cancelled).
- Non-functional requirements:
  - Idempotency to avoid duplicate orders.
  - Retry/backoff on temporary connectivity loss.
  - Audit logging for order dispute traceability.
  - Scalability: the system must handle the initial FoodApp peak load of at least x requests per minute and be easy to scale as volume increases.
- Acceptance criteria for cross-channel UAT.
- Requirement traceability matrix linked to scope, testing, and deployment checklist.

## Affected Stakeholders
- Sponsor/Owner
- Operations Manager
- Cashier & Kitchen Staff
- PM/BA
- Tech Lead and Backend Engineers
- QA
- FoodApp Partner Technical Team

## Assumptions and Constraints
- Assumption: the internal menu catalog is sufficient to map to the FoodApp catalog.
- Assumption: FoodApp provides a test environment with enough error scenario coverage.
- Constraint: the release window depends on FoodApp's integration certification schedule.
- Constraint: QA capacity is limited, requiring prioritization of critical path scenarios.

## Follow-up Actions
- BA: finalize BRD/FRD for the FoodApp integration and sign off requirements.
- Tech Lead: create specification for the API adapter and mapping table.
- QA: prepare test cases for duplicate orders, partial failures, and delayed callbacks.
- Ops: confirm exception handling procedures at the counter when online order status fails.

## AI Agent Prompt Seed
```text
You are a PM Assistant for the MilkCO 40-seat F&B management software project.
Complete the Collect Requirements process according to PMBOK for the FoodApp integration request,
propose action items, owners, deadlines, and related risks/dependencies.
```
