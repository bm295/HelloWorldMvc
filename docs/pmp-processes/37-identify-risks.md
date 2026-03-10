# 37. Identify Risks

## Metadata
- Restaurant: MilkCO (40 seats)
- Knowledge Area: Risk
- Process Group: Planning
- Document Owner: PM/BA
- Last Updated: 2026-03-10

## Mục tiêu
Nhận diện sớm các rủi ro khi bổ sung tích hợp FoodApp để chuẩn bị kế hoạch phản ứng và giảm ảnh hưởng đến vận hành nhà hàng.

## Inputs
- [Collect Requirements](./09-collect-requirements.md).
- [Define Scope](./10-define-scope.md).
- [Perform Integrated Change Control](./06-perform-integrated-change-control.md).
- Tài liệu API/SLA và quy trình chứng nhận tích hợp của FoodApp.

## Tools & Techniques
- Risk workshop liên phòng ban (PM, Tech, QA, Ops).
- Risk breakdown structure (technical / external dependency / operational / compliance).
- Probability-impact matrix và risk owner assignment.

## Outputs
- Risk register cập nhật cho FoodApp integration.

### Key Risks (Initial)
1. **Vendor Dependency Delay**: chậm cấp credentials/sandbox access làm trễ schedule.
2. **API Contract Volatility**: thay đổi schema/enum từ FoodApp gây lỗi mapping.
3. **Duplicate/Out-of-order Events**: webhook trùng hoặc đến sai thứ tự gây lệch trạng thái đơn.
4. **Operational Overload**: staff chưa quen xử lý đơn lỗi đa kênh trong giờ cao điểm.
5. **Reconciliation Gap**: sai lệch giữa đơn ghi nhận nội bộ và đơn trên FoodApp.
6. **Security/Compliance Misconfiguration**: callback endpoint hoặc secret quản lý chưa chuẩn.

### Dependencies
- FoodApp technical contact và SLA support.
- Môi trường test có dữ liệu mô phỏng đủ kịch bản.
- Lịch UAT của Ops team.

### Initial Response Direction
- Thiết lập contract test pipeline để phát hiện breaking changes sớm.
- Thiết kế idempotency + dead-letter queue/replay procedure.
- Tổ chức training vận hành và diễn tập incident runbook trước go-live.
- Định nghĩa reconciliation report hằng ngày cho tuần đầu sau triển khai.

## Follow-up Actions
- PM: cập nhật risk review cadence hàng tuần.
- Tech Lead: lập danh sách technical risk với trigger và threshold rõ ràng.
- Ops Lead: định danh risk owner tại ca vận hành.

## AI Agent Prompt Seed
```text
Bạn là PM Assistant cho dự án phần mềm quản lý FnB MilkCO 40 chỗ.
Hãy hoàn thiện process Identify Risks theo PMBOK cho yêu cầu tích hợp FoodApp,
bao gồm risk register ban đầu, dependency, impact và hướng risk response.
```
