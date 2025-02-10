# Developer Evaluation Project

# Projeto .NET Core 8 com Banco de Dados AWS e Testes
Este projeto demonstra uma aplicação .NET Core 8 que utiliza um banco de dados hospedado na AWS e possui uma estrutura de testes abrangente, incluindo: testes unitários, de integração e funcionais.

## Tecnologias
* **.NET Core 8:** Framework para desenvolvimento de aplicações web*
* **RDS Postegres:** O banco de dados SQL utilizado neste projeto está hospedado na AWS.*
* **MongoDB:** O banco de dados NoSQL utilizado neste projeto está hospedado no Atlas Cloud.*
* **Testes Unitários:** Testes que verificam o comportamento de unidades individuais de código (classes, métodos, etc.).*
* **Testes de Integração:** Testes que verificam a interação entre diferentes partes do sistema (camadas, módulos, etc.).*
* **Testes Funcionais:** Testes que verificam se o sistema como um todo atende aos requisitos funcionais.*

## Configuração
1. **Pré-requisitos:**
* .NET Core 8 SDK instalado.

2. **Configuração do Banco de Dados:**
* As configurações de acesso ao banco de dados estão localizadas no arquivo `appsettings.json`.
* Certifique-se de que as credenciais e a URL estejam corretas.

3. **Execução do Projeto:**
* Clone o repositório.
* Abra o projeto no Visual Studio ou em outra IDE compatível.
* Restaure os pacotes NuGet: `dotnet restore`
* Execute o projeto: `dotnet run`

## Estrutura do Projeto
O projeto está organizado em camadas para facilitar a manutenção e a escalabilidade:
* **Camada de Apresentação (API):** Contém os controladores e os endpoints da API.* 
**Camada de Negócios:** Contém a lógica de negócios da aplicação.* 
**Camada de Acesso a Dados:** Contém a lógica de acesso ao banco de dados.

## Testes
O projeto possui três camadas de testes:
* **Testes Unitários:** 
Os testes unitários estão localizados na pasta `ambev-developer-evaluation\tests\Ambev.DeveloperEvaluation.Unit`.* 
* **Testes de Integração:** Os testes de integração estão localizados na pasta `ambev-developer-evaluation\tests\Ambev.DeveloperEvaluation.Integration`.* 
* **Testes Funcionais:** Os testes funcionais estão localizados na pasta `ambev-developer-evaluation\tests\Ambev.DeveloperEvaluation.Functional`.

## Execução dos Testes
Os testes podem ser executados no Visual Studio ou através da linha de comando:
* **Visual Studio:** Abra o Test Explorer e execute os testes desejados.*
* **Linha de Comando:** `dotnet test`