# Multiple Payment Methods — Readiness Trace

Source requirement: [`docs/business/multiple-payment-methods.feature`](../docs/business/multiple-payment-methods.feature)

| Requirement ID | Business requirement | Source file/section | Current code evidence | Status | Gap |
|---|---|---|---|---|---|
| PAY-MULTI-001 | A staff member can settle a VND 500,000 order with VND 200,000 cash and VND 300,000 card, record both payment details, show a successful receipt, and leave no balance. | `multiple-payment-methods.feature`, scenario | `PaymentUseCaseService.ProcessPaymentAsync` accepts one `ProcessPaymentRequest`, creates one `Payment`, and immediately sets the order to `Paid`. `Payment` stores `OrderId`, `Amount`, and `Method`. | BLOCKED — documentation first | The contract for submitting multiple payments is unspecified; partial-payment status, overpayment/underpayment rules, atomicity, allowed methods, and receipt output need confirmation. |

## Code-readiness gate

| Readiness criterion | Met/Not met | Evidence | Missing information or action |
|---|---|---|---|
| Specific business result | Met | Scenario specifies total and two payment amounts. | — |
| Traceable source and section | Met | Feature file and scenario above. | — |
| Actor/workflow identified | Met | Staff member creates, selects, and confirms payment. | — |
| Current gap identifiable | Met | Current service processes one payment and marks the order paid. | — |
| Acceptance test is precise | Not met | Happy path is precise, but request/response and sequencing are not. | Define endpoint payload and receipt response. |
| Inputs, outputs, errors, boundaries | Not met | No rules for zero, underpayment, overpayment, duplicate confirmation, or invalid methods. | Confirm validation and failure behavior. |
| No conflicting documentation | Met | No other multiple-payment requirement found. | — |
| Affected code area identifiable | Met | Payment service, controller, repository, and `Payment` entity. | — |
| Test infrastructure exists or can be created | Not met | No test project is present in the solution. | Next run must create and verify a .NET test project first. |
| Enough time for safe implementation | Not evaluated | This run is documentation-first. | Reassess after decisions are recorded. |

## Requirement prepared for next run

- **ID:** PAY-MULTI-001
- **Objective:** Reconcile one order across multiple captured payment methods.
- **Actor/workflow:** Staff member confirms a set of payments for an open unpaid order.
- **Preconditions:** Order exists, is open, and has total VND 500,000; selected payments total exactly VND 500,000.
- **Expected behavior:** Persist both payment rows atomically, mark the order fully paid, return/show a successful receipt, and report zero outstanding balance.
- **Error behavior:** Still requires confirmation for underpayment, overpayment, duplicate confirmation, unsupported method, and persistence failure.
- **First acceptance test:** Submit cash VND 200,000 plus card VND 300,000 and verify two payment records, paid status, receipt, and zero balance.
- **Expected code area:** `PaymentUseCaseService`, payment controller/request model, order/payment persistence.
- **Status:** BLOCKED — pending the contract and boundary decisions above; then `READY_FOR_TDD`.
