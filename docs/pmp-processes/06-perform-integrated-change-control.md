# 06. Perform Integrated Change Control

## Metadata
- Restaurant: MilkCO (40 seats)
- Knowledge Area: Integration
- Process Group: Monitoring and Controlling
- Document Owner: PM/BA
- Last Updated: 2026-03-10

## Mục tiêu
Đảm bảo yêu cầu mới tích hợp FoodApp được đánh giá, phê duyệt, và triển khai có kiểm soát trên toàn bộ baselines (scope/schedule/cost/risk/architecture).

## Inputs
- Change Request: “Add FoodApp Integration for online delivery order channel”.
- [Project Charter](./01-develop-project-charter.md).
- [Project Management Plan](./02-develop-project-management-plan.md).
- [Define Scope](./10-define-scope.md), [Collect Requirements](./09-collect-requirements.md).
- [Identify Risks](./37-identify-risks.md).
- Technical notes về API contract, webhook, security từ FoodApp.

## Tools & Techniques
- Integrated impact assessment (business + technical + operations).
- Change Control Board (CCB) review với Sponsor, PM, Tech Lead, Ops Lead.
- Options analysis:
  - Option A: tích hợp realtime toàn bộ flow.
  - Option B: triển khai phased (order ingestion trước, status sync sau).
- Decision log và traceability matrix cho requirement-to-deliverable.

## Outputs
- Change decision: **Approved with phased delivery** (khuyến nghị để giảm rủi ro go-live).
- Cập nhật change log, issue log, risk register.
- Trigger cập nhật các tài liệu kế hoạch và thực thi liên quan.

## Change Impact Summary
- Scope: mở rộng sang external order channel và synchronization lifecycle.
- Schedule: thêm mốc onboarding/certification từ FoodApp; critical path phụ thuộc đối tác.
- Cost: tăng effort cho integration development, test automation, observability.
- Architecture: thêm integration boundary (adapter), cơ chế idempotency, retry/backoff, audit trail.
- Operations: thêm runbook xử lý đơn treo/duplicate/rejected và reconciliation cuối ca.

## Dependencies and Risks to Monitor
- Dependency: FoodApp cấp thông tin API, credentials, callback whitelist đúng hạn.
- Risk: contract thay đổi cận ngày go-live → cần freeze window + regression contract test.
- Risk: webhook thất bại gây lệch trạng thái đơn → cần dead-letter/replay process.
- Risk: thiếu ownership liên phòng ban khi xử lý incident → cần RACI rõ ràng.

## Follow-up Actions
- PM: tổ chức CCB check-point hàng tuần đến khi go-live.
- Tech Lead: thiết lập integration spike + proof-of-connectivity sớm.
- QA: thiết lập bộ test hồi quy cho flow đơn từ FoodApp.
- Ops: hoàn thiện SOP xử lý sự cố kênh FoodApp và đào tạo cashier/kitchen.

## AI Agent Prompt Seed
```text
Bạn là PM Assistant cho dự án phần mềm quản lý FnB MilkCO 40 chỗ.
Hãy hoàn thiện process Perform Integrated Change Control theo PMBOK cho yêu cầu tích hợp FoodApp,
bao gồm tác động baseline, quyết định CCB, risk/dependency và action theo owner.
```
