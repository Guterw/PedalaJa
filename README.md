# PedalaJa

PedalaJa é um aplicativo de localização de bicicletas desenvolvido em .NET que utiliza a API do CityBik.es para mostrar estações e bicicletas disponíveis na cidade de Sorocaba. O projeto foi desenvolvido com o Blazor para a construção da interface e a integração com o banco de dados para armazenar e exibir informações relevantes sobre as estações.

## Funcionalidades

- Exibir todas as estações de bicicletas de Sorocaba (Em breve outras localidades) em um mapa interativo.
- Informar detalhes das estações ao passar o mouse sobre elas, como:
  - Nome da estação.
  - Quantidade de bicicletas livres.
  - Quantidade de slots vazios.
  - Companhia responsável.
- Atualização dinâmica dos dados com um botão de recarga.
- Página de suporte com informações detalhadas sobre o projeto e seu desenvolvimento.

## Tecnologias Utilizadas

- **Frontend**: Blazor, Leaflet.js para mapas.
- **Backend**: ASP.NET Core, C# para CRUD e manipulação de dados.
- **Banco de Dados**: SQL Server (ou o banco de dados utilizado na sua configuração local).
- **Integração**: API do CityBik.es para obter dados em tempo real.

## Novidades da Versão 2.0

A versão 2.0 do PedalaJa inclui melhorias significativas e novas funcionalidades:

1. **Avaliações de Segurança**: Agora o projeto possui uma análise detalhada sobre as práticas de segurança implementadas, garantindo maior proteção dos dados e integridade do sistema.
2. **Mudanças no Estilo dos Botões**: Melhorias nos estilos dos botões para tornar a interface mais atraente e intuitiva.
3. **Nova Página de Suporte**: A página de suporte foi reformulada, oferecendo um espaço mais organizado para tirar dúvidas sobre o projeto e como contribuir.
4. **Atualização na Lógica de Funcionamento**: Mudanças na lógica de atualização das estações, agora mais eficiente e com recarga dinâmica dos dados em tempo real.

## Como foi Construído

O projeto foi desenvolvido com foco em usabilidade e performance. A integração com a API do CityBik.es foi feita de forma a otimizar a busca de dados em tempo real. Para o frontend, utilizamos Blazor para a construção da interface, e Leaflet.js foi escolhido para a renderização dos mapas, proporcionando uma visualização interativa e dinâmica das estações de bicicletas.

Além disso, a implementação do banco de dados SQL Server permitiu o armazenamento e manipulação eficiente das informações, como a quantidade de bicicletas e slots disponíveis nas estações.

### Avaliações de Segurança

A segurança do sistema foi uma prioridade desde o início. Algumas das principais avaliações de segurança realizadas incluem:

- **Proteção contra injeções SQL**: Utilizando práticas de consulta parametrizada.
- **Segurança no armazenamento de dados**: O banco de dados foi configurado com criptografia e práticas recomendadas para proteger dados sensíveis.
- **Autenticação e Autorização**: Implementação de autenticação segura para proteger páginas sensíveis, embora no momento não haja login, esta funcionalidade está planejada para versões futuras.


## Instruções de Instalação

1. Clone o repositório:

git clone https://github.com/guterw/pedalaja.git

2. Navegue até o diretório do projeto:
   
cd PedalaJa

3. Configure o banco de dados SQL conforme necessário, ajustando as configurações de conexão no appsettings.json.
Para restaurar os pacotes e dependências:

dotnet restore

-Qualquer problema rode:

dotnet ef database drop
dotnet ef database update

4. Compile e rode o projeto:

dotnet run

5.Caso encontre erros ao clonar ou fazer commit no repositório, ajuste o buffer de envio do Git com o comando:

git config --global http.postBuffer 157286400
Abra o navegador e acesse http://localhost:5000(ou local que o terminal/IDE instanciar) para visualizar o aplicativo em execução.

## Problemas Conhecidos:
-Variaveis de Ambiente (MacOS/Linux e Windows)

Como desenvolvi e configurei a base do projeto no ambiente do MacOS a string de conexão do banco de dados que está atrelada é referente a esse ambiente, mas deixei comentado a do ambiente do windows, então se a sua for windows, apenas descomente e comente a do Mac, e se for Linux seu ambiente a do Mac já serve pra ti.

## Contribuições

Se você deseja contribuir para o projeto, fique à vontade para abrir um *pull request* ou relatar problemas.

## Créditos

Este projeto foi desenvolvido por **Luccas da Silva**, como parte da **Usina de Aprendizagem de Projetos**, no **Quinto Semestre de Análise e Desenvolvimento de Sistemas na Faculdade de Engenharia de Sorocaba - Facens**.

Link do repositório: [https://github.com/Guterw/PedalaJa](https://github.com/Guterw/PedalaJa)

Boa continuação bravo desenvolvedor :)
