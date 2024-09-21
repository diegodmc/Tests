# Thunder API 
CRUD API de tarefas

# Tecnologia
WebApi .Net6
Dapper
MySQL 8.0
Docker 
docker-compose 

# Arquitetura Estrutural  
📂 src/  
   ├── 📂 API/                         # Projeto da API  
   ├── 📂 Domain/                       # Projeto do Domínio  
   ├── 📂 Business/                     # Projeto da Lógica de Negócio  
   ├── 📂 Infra/                        # Projeto da Infraestrutura  
   ├── 📂 UnitTests/                    # Projeto de Testes Unitários  
   ├── 📂 IntegrationTests/             # Pendente  
   └── 📂 EndToEndTests/                # Pendente  

# Arquitetura Estrutural Detalhada
📂 src/
   ├── 📂 API/
   │   ├── 📂 Endpoints/                  # Controladores e endpoints da API  
   │   ├── 📂 Contracts/                  # Request e Response do contrato de comunicação http  
   │   ├── 📂 Middlewares/                # Middlewares personalizados  
   │   ├── 📂 Configurations/             # Configurações da aplicação   
   │   ├── 📂 HttpResponseCommon/         # Respostas comuns e utilitários HTTP  
   │   └── 📂 Program.cs                  # Ponto de entrada principal da aplicação  
   │  
   ├── 📂 Domain/                         # Domínio  
   │   ├── 📂 Entities/                   # Entidades de domínio  
   │   └── 📂 Interfaces/                 # Interfaces do domínio  
   │  
   ├── 📂 Businnes/                       # Camada de aplicação   
   │   ├── 📂 Services/                   # Serviços de aplicação  
   │   └── 📂 DTOs/                       # Data Transfer Objects  
   │
   ├── 📂 Infra/                          # Implementações de infraestrutura  
   │   ├── 📂 Repositories/               # Implementação dos repositórios  
   │   ├── 📂 Mappings/                   # Mapeamento entre entidades e persistência  
   │   ├── 📂 Sql/                        # Comandos SQLs  
   │   |__ 📂 Docker/                     # Images   
   |   
   └── 📂 Tests/                          # Projeto de testes unitários, de integração e de aceitação  
       ├── 📂 UnitTests/                  # Testes unitários (domínio e aplicação)  
       ├── 📂 IntegrationTests/           # Testes de integração (infraestrutura)  
       └── 📂 EndToEndTests/              # Testes de ponta a ponta  

# Execução da API
windows altere para switch to linux container

CLI: docker-compose.yml up --build
