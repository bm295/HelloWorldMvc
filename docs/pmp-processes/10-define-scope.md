# 10. Define Scope

## Metadata
- Restaurant: MilkCO (40 seats)
- Knowledge Area: Scope
- Process Group: Planning
- Document Owner: PM/BA
- Last Updated: 2026-03-10

## Mục tiêu
Xác định rõ phạm vi triển khai FoodApp integration cho giai đoạn hiện tại, tránh scope creep và đảm bảo alignment giữa business value với khả năng kỹ thuật.

## Inputs
- [Collect Requirements](./09-collect-requirements.md).
- [Project Charter](./01-develop-project-charter.md).
- Change decision từ [Perform Integrated Change Control](./06-perform-integrated-change-control.md).

## Tools & Techniques
- Product scope description workshop.
- Decomposition theo capability (ingestion, mapping, status sync, monitoring).
- Boundary analysis giữa hệ thống nội bộ và FoodApp.

## Outputs
- Scope statement cho FoodApp integration (phase 1).

### In Scope (Phase 1)
- Tích hợp nhận đơn FoodApp vào hệ thống order nội bộ.
- Mapping SKU/menu giữa FoodApp và hệ thống hiện tại.
- Đồng bộ trạng thái đơn hàng cốt lõi: accepted, preparing, completed, cancelled.
- Cơ chế idempotency + retry + logging để vận hành ổn định.
- Dashboard/monitoring tối thiểu cho theo dõi lỗi tích hợp.

### Out of Scope (Phase 1)
- Dynamic pricing và campaign đồng bộ ngược từ hệ thống nội bộ lên FoodApp.
- Đối soát tài chính tự động đầy đủ nhiều nền tảng.
- Multi-platform aggregator abstraction (chỉ ưu tiên FoodApp trong phase hiện tại).

## Scope Impacts
- Product scope: mở rộng từ kênh nội bộ sang kênh external partner.
- Project scope: tăng deliverables về contract testing, runbook, và đào tạo vận hành.
- Architecture scope: cần integration component chuyên biệt, không nhúng trực tiếp vào domain core.

## Constraints and Assumptions
- Constraint: phạm vi phase 1 ưu tiên time-to-market, chưa bao phủ toàn bộ tính năng partner ecosystem.
- Constraint: phụ thuộc chính sách rate limit và API quota của FoodApp.
- Assumption: volume đơn FoodApp giai đoạn đầu nằm trong capacity hiện tại sau tối ưu nhẹ.

## Follow-up Actions
- PM/BA: baseline scope statement và WBS bổ sung.
- Tech Lead: review impact kiến trúc và đưa tiêu chí mở rộng cho phase 2.
- QA/Ops: đồng thuận định nghĩa “operationally ready” trước go-live.

## AI Agent Prompt Seed
```text
Bạn là PM Assistant cho dự án phần mềm quản lý FnB MilkCO 40 chỗ.
Hãy hoàn thiện process Define Scope theo PMBOK cho yêu cầu tích hợp FoodApp,
đề xuất phạm vi in-scope/out-of-scope, assumption/constraint và action items.
```
