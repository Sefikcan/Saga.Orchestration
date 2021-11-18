# Saga.Orchestration

![1_5x7OpMhymYKuXgoIit7eNQ](https://user-images.githubusercontent.com/11744411/142416232-bd9ea084-0342-4a82-b913-252c6bc4c544.png)

- Order Service create order and set order status pending. After, order service sends ORDER_CREATED event information to Orchestrator. After Orchestrator begin create order transaction.
- Orchestrator sends EXECUTE_PAYMENT event information to Payment Service. After Payment Service return information about refund money.
- Orchestrator sends UPDATE_STOCK event information to Stock service. After, Stock service returns the information that the stock information of the related products has been updated.
- Orchestrator sends ORDER_DELIVER event information to Shipment Service. After, Shipment Service return shipped information.
