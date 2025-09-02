# BMG Propostas de Seguro Teste Técnico

## Tecnologias Usadas

- **.NET** 
- **SQL Server**
- **RabbitMQ**
- **Docker**
  
###  Instruções de build e execução

1) Acesse a pasta "docker" que está na raiz do projeto.

![Descrição da imagem](https://github.com/evertongmdr/BMG.PropostaSeguro/blob/master/documentos/prints/pasta-docker.png)

2) Abrir o terminal no respectivo local e exectuar o arquivo "arquivo-propostaseguro_producao" com o seguinte comando:<br>
**"docker-compose -f propostaseguro_producao.yml up --build"**

![Descrição da imagem](documentos/prints/arquivo-propostaseguro_producao.png)

![Descrição da imagem](documentos/prints/rodando-comando-docker.png)

3) Após executar o comando, a execução do projeto pode demorar um pouco, pois o Docker precisa baixar as imagens, construir o projeto e subir os containers.
Quando esse processo for concluído, o projeto estará disponível para testes.


### Endereços das APIs

Acesse as APIs localmente através do Swagger:

| API                        | Swagger URL                                      |
|----------------------------|-------------------------------------------------|
| Seguros BFF (API Gateway)  | [http://localhost:5501/swagger/index.html](http://localhost:5501/swagger/index.html) |
| Proposta API               | [http://localhost:5201/swagger/index.html](http://localhost:5201/swagger/index.html) |
| Contratação API            | [http://localhost:5301/swagger/index.html](http://localhost:5301/swagger/index.html) |
| Identidade API             | [http://localhost:5401/swagger/index.html](http://localhost:5401/swagger/index.html) |


### Diagrama simples da arquitetura

O Diagrma foi feito no https://excalidraw.com/ e encontra-se na pasta "documentos" no arquivo "BMG.PropostaSeguros.Diagrama.excalidraw", que fica na raiz do projeto.

![Descrição da imagem](https://github.com/evertongmdr/BMG.PropostaSeguro/blob/master/documentos/prints/pasta-documento.png)

![Descrição da imagem](https://github.com/evertongmdr/BMG.PropostaSeguro/blob/master/documentos/prints/nome-diagrama.png)


## Imagem do Diagrama

![Descrição da imagem](https://github.com/evertongmdr/BMG.PropostaSeguro/blob/master/documentos/prints/diagrama-arquitetura-simples.png)

### Overview do Diagrama

 1) **BFF API Gateway**: Ponto de entrada das requisições. Ele direciona as chamadas para os microsserviços apropriados.

2) **Microsserviços**:
   - **Proposta API**, **Contratacao API** e **Identidade API**. Cada um possui seu próprio banco de dados (BD).
   - A **Contratacao API** possui um **Background Service** para processar tarefas assíncronas.

3) **RabbitMQ**:
   - Funciona como um sistema de mensagens assíncronas.
   - A **Contratacao API** envia mensagens via **Producer** para uma **Queue**.
   - Um **Consumer** (rodando em um background service) consome essas mensagens e processa a lógica de salvar a contratação.
      - Background Service esta rodando dentro **Contratacao API** 

### Fluxo de dados
- As requisições entram pelo Gateway, que chama os microsserviços.
- A **Contratacao API** faz uma operação assíncrona.
- Para operações assíncronas, mensagens são enviadas para RabbitMQ, consumidas por outro serviço e processadas depois.

### Containerização
- Todo o diagrama está dentro de um contêiner **Docker**, cada serviço roda em um container isolado.
  - Serviços
    - Bancos/RabbitMQ/APIs/BFF API Gateway

### Contato e Suporte

Fico à disposição para esclarecer qualquer dúvida ou avaliar detalhes do projeto.  
Será um prazer conversar sobre qualquer ponto, arquitetura ou execução do sistema.


