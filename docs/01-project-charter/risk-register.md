# Risk Register - MilkCO FnB Management API

## Rating Scale
- Probability: Low (L), Medium (M), High (H)
- Impact: Low (L), Medium (M), High (H)

| Risk ID | Risk Description | Probability | Impact | Owner | Mitigation / Response | Trigger |
|---|---|---|---|---|---|---|
| R-01 | Runtime compatibility issues with `.NET 10` across target environments | M | H | Technical Lead | Validate SDK/runtime versions early; lock environment with documented setup and container checks | Build or startup failures on target host |
| R-02 | SQL Server dependency unavailable or unhealthy during deployment | M | H | DevOps | Use health checks, startup dependency gating, and backup/restart procedures | API cannot connect to DB or startup stalls |
| R-03 | Inventory oversell under concurrent order submissions | M | H | Backend Engineer | Add concurrency test scenarios; evaluate transaction isolation and row-level protection strategy | Negative stock or inconsistent item quantity |
| R-04 | Missing authentication/authorization exposes APIs in non-local environments | H | H | Security/Technical Lead | Keep pilot in controlled network; plan auth scope for next phase before public exposure | Unauthorized access attempts or security review findings |
| R-05 | Payment records can be created without strict linkage/validation to order lifecycle | M | M | Backend Engineer | Add business validation and reconciliation checks for payment to order states | Payment/order mismatches found in UAT |
| R-06 | Limited automated tests may allow regressions into pilot | H | M | QA Lead | Prioritize smoke + regression test pack for core flows before go-live | Reopened defects after fixes |
| R-07 | Stakeholder availability delays approvals and UAT feedback | M | M | PM/BA | Schedule fixed weekly checkpoints; escalate blockers within 24 hours | Missed review meetings or delayed sign-offs |
| R-08 | Hardcoded development credentials reused outside controlled environments | M | H | DevOps + Security | Enforce environment-specific secrets; avoid default credentials in shared environments | Security scan or audit flags weak credentials |

## Current Risk Posture
- Open risks: 8
- Highest priority for pilot: R-01, R-02, R-03, R-04, R-08
