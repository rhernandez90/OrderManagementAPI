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
    
---


