# Success Criteria - MilkCO FnB Management API

## Acceptance Window
- Baseline date: March 4, 2026
- Target pilot readiness: March 29, 2026

## Measurable Criteria
| ID | Criterion | Target | Validation Method | Owner |
|---|---|---|---|---|
| SC-01 | Build reliability | `dotnet build HelloWorldMvc.sln` succeeds with 0 errors | Build log review | Technical Lead |
| SC-02 | Environment startup | `docker compose up --build` starts API and SQL containers successfully | Container health and API check | DevOps/Backend |
| SC-03 | Inventory read/write flow | `GET /api/inventory`, `POST /api/inventory`, `GET /api/inventory/{id}` work as expected | API functional test | QA |
| SC-04 | Order flow with stock protection | `POST /api/orders` creates order only when stock is sufficient; rejects insufficient stock | API functional test with positive/negative cases | QA + Backend |
| SC-05 | Payment read/write flow | `GET /api/payments`, `POST /api/payments`, `GET /api/payments/{id}` work as expected | API functional test | QA |
| SC-06 | Operational health visibility | `GET /api/health` returns status payload and HTTP 200 | Smoke test | Operations |
| SC-07 | Charter governance artifacts | Business case, success criteria, milestones, risks, stakeholders are documented and linked from charter | Document review | PM/BA |
| SC-08 | Stakeholder sign-off | Sponsor and operations representative approve pilot scope and readiness | Sign-off record | PM/BA |

## Quality Thresholds
- No blocker defects in order and inventory workflows at pilot go/no-go.
- Data validation rules in request models and domain entities remain active and tested.

## Exit Criteria
Pilot launch is approved only when SC-01 through SC-08 are met or formally waived by sponsor.
