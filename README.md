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

### Contato e Suporte

Fico à disposição para esclarecer qualquer dúvida ou avaliar detalhes do projeto.  
Será um prazer conversar sobre qualquer ponto, arquitetura ou execução do sistema.


