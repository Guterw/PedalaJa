# PedalaJa

PedalaJa é um aplicativo de localização de bicicletas desenvolvido em .NET que utiliza a API do CityBik.es para mostrar estações e bicicletas disponíveis na cidade de Sorocaba. O projeto utiliza Blazor para a construção da interface e integração com o banco de dados para armazenar e exibir informações relevantes sobre as estações.

## Funcionalidades

- Exibir todas as estações de bicicletas de Sorocaba(Em breve outras localidades) em um mapa interativo.
- Informar detalhes das estações ao passar o mouse sobre elas, como:
  - Nome da estação.
  - Quantidade de bicicletas livres.
  - Quantidade de slots vazios.
  - Companhia responsável.
- Atualização dinâmica dos dados com botão de recarga.

## Tecnologias Utilizadas

- **Frontend**: Blazor, Leaflet.js para mapas.
- **Backend**: ASP.NET Core, C# para CRUD e manipulação de dados.
- **Banco de Dados**: SQL Server (ou o banco de dados utilizado na sua configuração local).
- **Integração**: API do CityBik.es para obter dados em tempo real.

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


Boa continuação bravo desenvolvedor :)
