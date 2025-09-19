Feature: Compra en Demoblaze
  Como usuario del sitio Demoblaze
  Quiero realizar una compra completa
  Para verificar el correcto funcionamiento del flujo de e-commerce

  @agregar
  Scenario: Agregar dos productos al carrito
    Given el usuario navega a la página principal
    When agrega el primer producto al carrito
    And agrega el segundo producto al carrito
    Then el carrito muestra 2 productos

  @visualizar
  Scenario: Visualizar el carrito
    Given el usuario tiene productos en el carrito
    When el usuario accede al carrito
    Then se listan los productos agregados

  @formulario
  Scenario: Completar el formulario de compra
    Given el usuario está en el carrito con productos
    When completa el formulario de compra con datos válidos
    Then el sistema acepta la información ingresada

  @finalizar
  Scenario: Finalizar la compra
    Given el usuario completó el formulario de compra
    When confirma la compra
    Then se muestra un mensaje de confirmación de compra exitosa