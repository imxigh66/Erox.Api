
# Erox Shoes — ASP.NET Core API + React.js

## Описание
Веб-приложение для интернет-магазина обуви, разработанное в рамках учебного проекта. 
Состоит из backend-части (**ASP.NET Core Web API**) и frontend-части (**React.js**), которые взаимодействуют через REST API.

---

## Основные возможности
- Регистрация и вход с использованием JWT
- Каталог товаров с фильтрацией и сортировкой
- Управление размерами и остатками
- Корзина и оформление заказов
- Админ-панель для управления пользователями, товарами и заказами
- Swagger-документация API

---

## Стек технологий
**Backend**:
- ASP.NET Core
- Entity Framework Core
- MediatR
- AutoMapper
- JWT (JSON Web Tokens)

**Frontend**:
- React.js
- Axios
- React Router

**База данных**:
- Microsoft SQL Server

**Инструменты**:
- Visual Studio 2022
- VS Code
- Postman
- Swagger

---

## Архитектура проекта
**Backend** разделён на слои:
- `Erox.Api` — API слой (контроллеры, точки входа)
- `Erox.Application` — бизнес-логика
- `Erox.DataAccess` — работа с базой данных
- `Erox.Domain` — сущности, интерфейсы, контракты

**Frontend** структурирован по модулям:
- `cart/` — корзина
- `auth/` — авторизация
- `products/` — каталог товаров
- `public/` — статические файлы (HTML, локализация)

---

![Демо](screenshots/demo.gif)

---



## Автор
Касапчук Влада
