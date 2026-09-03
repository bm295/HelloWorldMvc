Feature: Pay for an order using multiple payment methods

  Scenario: Customer pays partially in cash and the remainder by card
    Given a staff member has created an order with a total amount of VND 500,000
    And the order is unpaid
    When the staff member selects "Cash" as a payment method for VND 200,000
    And the staff member selects "Card" as a payment method for VND 300,000
    And the staff member confirms the payment
    Then the system records the order as fully paid for VND 500,000
    And the system saves the payment details as VND 200,000 in cash
    And the system saves the payment details as VND 300,000 by card
    And the system prints or displays a successful payment receipt
    And the order has no outstanding balance
