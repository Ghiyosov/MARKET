    create database marketdb;
       
    -- Создание таблицы products
    CREATE TABLE products (
                              productid SERIAL PRIMARY KEY, -- Уникальный идентификатор продукта
                              name VARCHAR(100) NOT NULL,    -- Название продукта
                              price DECIMAL(10, 2) NOT NULL  -- Цена продукта
    );

-- Создание таблицы customers
    CREATE TABLE customers (
                               customerid SERIAL PRIMARY KEY, -- Уникальный идентификатор клиента
                               name VARCHAR(100) NOT NULL,     -- Имя клиента
                               email VARCHAR(100) UNIQUE,      -- Email клиента
                               phone VARCHAR(20),              -- Телефон клиента
                               budget DECIMAL(10, 2) NOT NULL DEFAULT 0 -- Бюджет клиента
    );

-- Создание таблицы orders
    CREATE TABLE orders (
                            orderid SERIAL PRIMARY KEY,      -- Уникальный идентификатор заказа
                            customerid INT NOT NULL,         -- Связь с customers
                            productid INT NOT NULL,          -- Связь с products
                            quantity INT NOT NULL,            -- Количество продуктов
                            totalprice DECIMAL(10, 2) NOT NULL DEFAULT 0, -- Общая стоимость заказа
                            orderdate TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Дата заказа
                            FOREIGN KEY (customer_id) REFERENCES customers (customer_id) ON DELETE CASCADE,
                            FOREIGN KEY (product_id) REFERENCES products (product_id) ON DELETE CASCADE
    );

        