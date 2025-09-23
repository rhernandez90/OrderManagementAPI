# Order Management System con integración Trello

Este proyecto consiste en un sistema basado en **microservicios** compuesto por:
- **OrderManagementAPI** → expone endpoints para crear y actualizar órdenes.
- **IntegrationAPI** → escucha eventos en RabbitMQ y crea/actualiza tarjetas en Trello.
- **RabbitMQ** → utilizado como message broker.
- **SQL Server** → persistencia de datos para las órdenes.

---

## 📐 Arquitectura elegida y razones


- Cada API tiene su propio ciclo de vida, base de datos y responsabilidades claras.
- RabbitMQ permite comunicación asíncrona y confiable entre servicios.
- SQL Server asegura persistencia transaccional de las órdenes.
- Trello se usa como integración externa para gestión visual.


---

## 🧩 Principios SOLID aplicados

- **S (Single Responsibility)**:  
  - `OrderController` solo maneja endpoints.  
  - `OrderService` encapsula la lógica de negocio de órdenes.  
  - `RabbitMqService` se encarga únicamente de la comunicación con RabbitMQ.  

- **O (Open/Closed)**:  
  - Los servicios pueden extenderse (ej. nuevos publishers para más colas) sin modificar las clases existentes.  

- **L (Liskov Substitution)**:  
  - Interfaces como `IRabbitMqService` aseguran que cualquier implementación (real o fake para testing) sea intercambiable.  

- **I (Interface Segregation)**:  
  - Cada servicio define contratos mínimos (`IOrderService`, `IRabbitMqService`), evitando dependencias innecesarias.  

- **D (Dependency Inversion)**:  
  - Las dependencias se inyectan vía constructor usando **DI de .NET**, desacoplando de implementaciones concretas.

---

## 🏗️ Patrones de diseño usados

- **DDD**:
  - se utilizo una arquitectura basada en el dominio la cual nos permite segregar de manera clara y eficiente los proyectos.
  
- **Publisher/Subscriber (con RabbitMQ)**:  
  - OrderManagementAPI publica eventos en RabbitMQ.  
  - IntegrationAPI suscribe y procesa los mensajes.  
    
## Para Ejecutar el proyecto basta con ejecutar el comando docker-compose up --build
**El cual se encargara de levantar lo contenedores necesario para levantar el api de ordenes
El server SQL y el ser de RabbitMQ,**
**Ojo por la prontitud en la que se desarrollo el proyecto no pude dejar la ultima configuracion en docker para 
El servicio que escucha la informacion de la cola de RabbitMQ  pero basta con correrlo por separa el archivo .env
ya tiene todas las variables necesaria para conectarse localmente al servicio de rabbitmq que se levanta en docker**

### Nota:
- Agregar las variables necesarias para efectuar la coneccion con trello 
- Solamente quedo funcionando la creacion de la card en trello cada vez que se crea una nueva orden
- Al levantar los contenedores con docker se corren las migraciones la cual crea la base de datos y tablas 
    tambien 3 productos por defectos son insertados en la taba de product

