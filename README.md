# NetCore_CassandraDrive
App de acesso ao banco de dados NoSQL Cassandra com .Net Core 3.1


Requisitos:
Instalar o CassandraCSharpDriver no seu Visual Studio ou Vs Code:

PM> Install-Package CassandraCSharpDriver

Instalar o Docker Desktop:
https://hub.docker.com/editions/community/docker-ce-desktop-windows

1 - Baixe a imagem do cassandra utilizando o docker:  https://hub.docker.com/_/cassandra

2 - No prompt de comando, utilize o comando  docker-compose build  para compilar o projeto

3 - Utilize o comando docker-compose up -d para subir os containers da Aplicação e do Cassandra

