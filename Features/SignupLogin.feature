@api
Feature: Signup and Login API

  Background:
    Given the API base URL is "https://api.demoblaze.com"

  @signup
  Scenario: Crear un nuevo usuario
When I send POST request to "/signup" with body
  | username     | password       |
  | nuevoUsuario | Password123!   |
    Then the response status code should be 200

  @signup_existing
  Scenario: Crear un usuario ya existente
    When I send POST request to "/signup" with body
  | username     | password       |
  | nuevoUsuario | Password123!   |
    Then the response status code should be 400
    And the response contains "This user already exists."

  @login_success
  Scenario: Usuario y password correctos
    When I send POST request to "/login" with body
  | username     | password       |
  | nuevoUsuario | Password123!   |
    Then the response status code should be 200

  @login_fail
  Scenario: Usuario y password incorrectos
    When I send POST request to "/login" with body
  | username     | password       |
  | nuevoUsuario | Password123!   |
    Then the response status code should be 401
    And the response contains "User does not exist."