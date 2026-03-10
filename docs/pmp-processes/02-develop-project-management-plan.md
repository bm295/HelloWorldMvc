# 02. Develop Project Management Plan

## Metadata
- Restaurant: MilkCO (40 seats)
- Knowledge Area: Integration
- Process Group: Planning
- Document Owner: PM/BA
- Last Updated: 2026-03-10

## Mục tiêu
Cập nhật kế hoạch quản lý dự án tổng thể để hấp thụ yêu cầu tích hợp FoodApp mà vẫn giữ mục tiêu vận hành ổn định cho hệ thống quản lý FnB của MilkCO.

## Inputs
- [Project Charter](./01-develop-project-charter.md) với change note FoodApp integration.
- [Collect Requirements](./09-collect-requirements.md).
- [Define Scope](./10-define-scope.md).
- [Identify Risks](./37-identify-risks.md).
- [Identify Stakeholders](./49-identify-stakeholders.md).
- Tài liệu kỹ thuật FoodApp API (sandbox/prod), chính sách bảo mật, SLA phản hồi.

## Tools & Techniques
- Planning workshop liên chức năng (PM, BA, Tech Lead, Ops, QA).
- Rolling wave planning cho các hạng mục phụ thuộc bên thứ ba.
- Baseline impact analysis cho Scope/Schedule/Cost/Risk/Communication.
- Definition of Done mở rộng cho tích hợp đối tác (contract test + UAT + runbook).

## Outputs
- **Project Management Plan vNext** có bổ sung workstream “FoodApp Integration”.
- Cập nhật baselines:
  - Scope baseline: thêm integration adapter, mapping, đồng bộ trạng thái.
  - Schedule baseline: thêm mốc sandbox onboarding, partner UAT, production certification.
  - Risk baseline: thêm vendor/API availability, retry/idempotency, reconciliation sai lệch đơn hàng.
- Quy ước vận hành:
  - Incident communication matrix cho lỗi kết nối FoodApp.
  - Integration readiness checklist trước go-live.

## Assumptions and Constraints
- Assumptions:
  - FoodApp cung cấp môi trường sandbox tương đồng production.
  - Ops team có thể bố trí ca thử nghiệm UAT liên kênh (offline + FoodApp).
- Constraints:
  - Timeline phụ thuộc vào tốc độ cấp API key/approval từ FoodApp.
  - Đội kỹ thuật hiện tại phải cân bằng giữa bảo trì core API và phát triển integration.

## Follow-up Actions
- PM: tạo change package và trình CCB phê duyệt re-baseline (D+2).
- Tech Lead: chốt integration architecture decision record (ADR) cho FoodApp adapter (D+3).
- QA Lead: bổ sung test strategy cho contract test, idempotency test, failure-retry test (D+4).
- Ops Manager: xác nhận quy trình xử lý đơn lỗi/reconcile cuối ngày (D+5).

## AI Agent Prompt Seed
```text
Bạn là PM Assistant cho dự án phần mềm quản lý FnB MilkCO 40 chỗ.
Hãy hoàn thiện process Develop Project Management Plan theo PMBOK, tích hợp yêu cầu FoodApp,
đề xuất action items, owner, deadline, risk, dependency và thay đổi baseline cần phê duyệt.
```
