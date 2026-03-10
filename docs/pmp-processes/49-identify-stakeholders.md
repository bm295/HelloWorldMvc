# 49. Identify Stakeholders

## Metadata
- Restaurant: MilkCO (40 seats)
- Knowledge Area: Stakeholder
- Process Group: Initiating
- Document Owner: PM/BA
- Last Updated: 2026-03-10

## Mục tiêu
Xác định đầy đủ các bên liên quan chịu tác động hoặc có ảnh hưởng đến yêu cầu tích hợp FoodApp, từ đó thiết kế kế hoạch phối hợp và truyền thông phù hợp.

## Inputs
- [Project Charter](./01-develop-project-charter.md).
- Yêu cầu thay đổi tích hợp FoodApp.
- Cơ cấu vận hành hiện tại của MilkCO.
- Thông tin đầu mối kỹ thuật/vận hành phía FoodApp.

## Tools & Techniques
- Stakeholder mapping (Power/Interest grid).
- Phỏng vấn nhanh theo vai trò (Sponsor, Ops, Tech, Partner).
- RACI drafting cho các quyết định và hoạt động tích hợp.

## Outputs
- Stakeholder register cập nhật cho workstream FoodApp.

### Stakeholder List (FoodApp Integration)
- **Sponsor/Owner (High power, High interest):** phê duyệt ngân sách và mức ưu tiên.
- **PM/BA (High power, High interest):** điều phối phạm vi, timeline, change control.
- **Tech Lead/Backend Team (Medium-high power, High interest):** thiết kế và triển khai tích hợp.
- **QA Lead/QA Team (Medium power, High interest):** đảm bảo chất lượng luồng đa kênh.
- **Ops Manager (High power, High interest):** chuẩn hóa quy trình xử lý đơn và đối soát.
- **Cashier/Kitchen Staff (Low-medium power, High interest):** người dùng vận hành trực tiếp.
- **FoodApp Partner Manager (Medium power, High interest):** điều phối onboarding và go-live.
- **FoodApp Technical Support (Medium power, High interest):** cung cấp API support, xử lý sự cố kỹ thuật.

## Stakeholder Engagement Notes
- Cần cơ chế escalation song phương MilkCO ↔ FoodApp cho incident giờ cao điểm.
- Cần lịch sync cố định hằng tuần trong giai đoạn onboarding.
- Cần sign-off rõ cho acceptance criteria từ cả Ops nội bộ và FoodApp.

## Follow-up Actions
- PM: phát hành stakeholder matrix + communication cadence trong 48 giờ.
- BA: xác nhận owner duyệt mapping nghiệp vụ menu/order status.
- Ops: chọn key user đại diện ca để tham gia UAT và training.

## AI Agent Prompt Seed
```text
Bạn là PM Assistant cho dự án phần mềm quản lý FnB MilkCO 40 chỗ.
Hãy hoàn thiện process Identify Stakeholders theo PMBOK cho yêu cầu tích hợp FoodApp,
bao gồm stakeholder register, mức ảnh hưởng, kế hoạch engagement và action items.
```
