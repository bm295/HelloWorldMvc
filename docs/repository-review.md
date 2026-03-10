# MilkCO Repository Review Report

## 1. Repository overview

- The repository contains a single ASP.NET Core MVC/API project under `WebApplication` with supporting docs.
- Technical baseline targets **.NET 10** and **C# 14** via project properties.
- Current code organization is feature folders (`Controllers`, `Models`, `Services`, `Repositories`, `Data`) rather than explicit Hexagonal layers.

## 2. Architecture evaluation

- The architecture is primarily a layered MVC + EF Core style.
- Dependency Injection is present in `Program.cs` for DbContext, repository, and service registrations.
- Asynchronous programming is used in repositories, services, and controllers.
- However, architecture boundaries are porous:
  - Controllers directly use EF Core DbContext in several places.
  - Services use DbContext and repository simultaneously.
  - Domain entities are persistence-annotated with EF/data annotations.

## 3. Hexagonal architecture compliance

Expected Hexagonal shape (`Domain`/`Application`/`Adapters`/`Infrastructure`) is **not implemented** as separate bounded layers.

Compliance findings:
- ❌ No dedicated `Domain` project/folder with pure entities and value objects.
- ❌ No explicit `Application` use-case layer (commands/queries/use-case handlers).
- ⚠️ One partial port-like interface exists (`IOrderRepository`) but it resides in the same application project and is consumed inconsistently.
- ❌ Controllers act as delivery adapters but also contain workflow/business checks and data-access responsibilities.
- ❌ Infrastructure concerns (EF Core, migrations, DbContext) are not isolated behind adapter implementations.

## 4. Domain model evaluation

Implemented entities cover some core concepts:
- `Order`, `OrderItem`, `Payment`, `InventoryItem`.

Gaps relative to restaurant domain:
- No **Table** / **Seat** aggregate or model for 40-seat capacity planning.
- No order lifecycle state model (e.g., Draft, SentToKitchen, Paid, Closed).
- No kitchen ticket concept or event.
- No reporting model/read side.
- Domain logic is not encapsulated in aggregate methods or domain services; behavior is mostly in `OrderService` and controllers.

## 5. FnB functionality coverage

Required flow vs implementation:
1. Create order for a table → **Partially met** (order creation exists, but no table linkage).
2. Add/remove items → **Not met** (no update/remove item endpoints/use cases).
3. Send order to kitchen → **Not met** (no status transition or kitchen queue integration).
4. Process payment → **Partially met** (payment record endpoint exists; no business rules with order state).
5. Deduct inventory → **Met** during order creation in `OrderService` transaction.
6. Close order → **Not met** (no close operation/status).

Also missing:
- Basic reporting endpoints/use cases.
- Capacity-aware table/seat allocation and occupancy controls for 40 seats.

## 6. Dependency direction analysis

Observed dependency flow violations:
- Controllers depend directly on persistence (`ApplicationDbContext`) in `PaymentsController`, `InventoryController`, and `OrderPageController`.
- Service (`OrderService`) depends on both DbContext and repository, coupling use-case logic to EF infrastructure details.
- Models include validation/data annotations directly in persistence/domain classes.

Result: dependencies do **not** consistently flow inward toward a pure domain core.

## 7. Code quality review

Strengths:
- Uses DI container configuration.
- Uses async/await for I/O-bound operations.
- Clear, readable code with straightforward endpoints.
- Transaction is used in order creation flow.

Weaknesses:
- Limited testability because business logic and persistence are tightly coupled.
- Inconsistent abstraction boundaries (some repository abstraction, some direct DbContext access).
- Missing use-case level contracts and DTO segregation.
- No automated tests found for domain/application behavior.

## 8. Identified architectural violations

1. **Direct infrastructure use in adapters/controllers**
   - `PaymentsController` and `InventoryController` use `ApplicationDbContext` directly.
   - `OrderPageController` performs data querying and presentation composition with direct DbContext calls.

2. **Business rules outside a pure domain/application core**
   - Stock validation and mutation in `OrderService` directly use EF entities and DbContext transaction.

3. **No hexagonal package/module boundaries**
   - Missing dedicated `Domain`, `Application`, `Adapters`, `Infrastructure` projects/folders.

4. **Missing mandatory restaurant capabilities**
   - No table/seat management model or use cases.
   - No order send-to-kitchen / close lifecycle operations.
   - No reporting use cases.

## 9. Recommended refactoring

1. **Restructure into hexagonal modules/projects**
   - `src/Domain`: pure entities, value objects, domain services, domain events.
   - `src/Application`: use cases, command/query contracts, inbound/outbound ports.
   - `src/Adapters`: HTTP controllers, persistence adapter implementations, presenters.
   - `src/Infrastructure`: EF Core DbContext, migrations, provider-specific setup.

2. **Introduce explicit ports**
   - Outbound ports: `IOrderRepository`, `IInventoryRepository`, `IPaymentGateway`, `ITableRepository`, `IReportingReadModel`.
   - Inbound ports/use cases: `CreateOrder`, `AddOrderItem`, `RemoveOrderItem`, `SendOrderToKitchen`, `ProcessPayment`, `CloseOrder`.

3. **Model core restaurant concepts**
   - Add `Table` aggregate with seat count and occupancy state.
   - Add `OrderStatus` state machine and rules for transitions.
   - Ensure payment completion precondition for closing order.

4. **Move EF annotations away from domain**
   - Keep domain model framework-agnostic.
   - Use EF fluent configuration in infrastructure adapters.

5. **Add reporting capabilities**
   - Implement basic reports (daily sales, top-selling items, inventory alerts).

6. **Add automated tests**
   - Unit tests for domain/application use cases.
   - Adapter integration tests for API + persistence behavior.

## 10. Overall verdict

**FAIL** — The repository does not follow Hexagonal Architecture and does not yet satisfy critical restaurant management requirements (table/seat management, order lifecycle operations, reporting), despite partial support for order creation, payment record storage, and inventory deduction.
