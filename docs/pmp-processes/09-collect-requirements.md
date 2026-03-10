# 09. Collect Requirements

## Metadata
- Restaurant: MilkCO (40 seats)
- Knowledge Area: Scope
- Process Group: Planning
- Document Owner: PM/BA
- Last Updated: 2026-03-10

## Mục tiêu
Thu thập, chuẩn hóa, và ưu tiên yêu cầu nghiệp vụ/kỹ thuật cho tích hợp FoodApp nhằm đảm bảo luồng vận hành đồng nhất giữa đơn tại quầy và đơn online.

## Inputs
- Product baseline hiện tại (inventory, order, payment API).
- Yêu cầu mới từ Sponsor/Ops: hỗ trợ kênh FoodApp.
- Tài liệu API/partner onboarding của FoodApp.
- Quy trình vận hành nhà hàng hiện tại: nhận đơn, chế biến, giao món, thanh toán, đối soát.

## Tools & Techniques
- Workshop 3 bên: Business (Ops/Cashier), Technical team, FoodApp partner.
- User story mapping cho end-to-end flow đặt món từ FoodApp.
- Interface analysis (payload mapping, enum mapping, error code mapping).
- MoSCoW prioritization cho release đầu tiên.

## Outputs
- Functional requirements:
  - Nhận đơn FoodApp theo thời gian thực hoặc polling fallback.
  - Ánh xạ sản phẩm/combo/add-on từ FoodApp vào menu nội bộ.
  - Đồng bộ trạng thái đơn (accepted/preparing/completed/cancelled).
- Non-functional requirements:
  - Idempotency để tránh tạo trùng đơn.
  - Retry/backoff khi mất kết nối tạm thời.
  - Audit log cho truy vết tranh chấp đơn hàng.
- Acceptance criteria cho UAT liên kênh.
- Requirement traceability matrix liên kết đến scope, test, và deployment checklist.

## Affected Stakeholders
- Sponsor/Owner
- Operations Manager
- Cashier & Kitchen Staff
- PM/BA
- Tech Lead + Backend Engineers
- QA
- FoodApp Partner Technical Team

## Assumptions and Constraints
- Assumption: danh mục món nội bộ đủ chuẩn để map với danh mục FoodApp.
- Assumption: FoodApp cho phép môi trường test đủ kịch bản lỗi.
- Constraint: release window phụ thuộc vào lịch chứng nhận tích hợp của FoodApp.
- Constraint: nguồn lực QA giới hạn, cần ưu tiên kịch bản critical path.

## Follow-up Actions
- BA: chốt BRD/FRD phần FoodApp integration và sign-off nghiệp vụ.
- Tech Lead: tạo specification cho API adapter + mapping table.
- QA: chuẩn bị test case cho duplicate order, partial failure, delayed callback.
- Ops: xác nhận quy trình xử lý ngoại lệ tại quầy khi đơn online lỗi trạng thái.

## AI Agent Prompt Seed
```text
Bạn là PM Assistant cho dự án phần mềm quản lý FnB MilkCO 40 chỗ.
Hãy hoàn thiện process Collect Requirements theo PMBOK cho yêu cầu tích hợp FoodApp,
đề xuất action items, owner, deadline, và risk/dependency liên quan.
```
