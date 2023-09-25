# MassTransit-RabbitMQ

#### Iniciar en docker contenedor de Rabbit MQ (latest RabbitMQ 3.12)
###### Colocar lo siguiente en PowerShell:
`
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
`
###### User:
guest
###### Pass:
guest


[Descarga - Página oficial] https://www.rabbitmq.com/download.html