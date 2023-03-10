# Notification Api
Este projeto foi realizado para botar em prática o meu conhecimento sobre novos assuntos que eu aprendi, como por exemplo: o RabbitMQ.\
Neste projeto foi criado uma api com quatro endpoints:\
-Cadastro de notificações;\
-Consulta de uma notificação por ID;\
-Consulta de uma notificação com paginação;\
-Exclusão de uma notificação por ID.\
Sendo o cadastro e a exclusão feitas em segundo plano, escutando uma fila do RabbitMQ

# Execução
Para a execução deste projeto é necessário baixar o RabbitMQ via Chocolatey com o seguinte comando no PowerShell:\
`choco install rabbitmq`\
Para acessar o dashboard do RabbitMQ acesse `http://localhost:15672/` e faça login com o usuário `guest` e senha `guest`\
Após isso, basta somente iniciar o projeto.
