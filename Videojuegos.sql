-- Crear la base de datos videojuegos
CREATE DATABASE videojuegos;

-- Usar la base de datos videojuegos
USE videojuegos;

-- Crear la tabla usuarios
CREATE TABLE usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,    -- Identificador único de cada usuario
    nombre VARCHAR(100),                  -- Nombre del usuario
    contrasena VARCHAR(255),              -- Contraseña del usuario
    correo VARCHAR(100) UNIQUE,           -- Correo del usuario, debe ser único
   fechacreacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- Fecha de creación del usuario (con valor por defecto de la fecha y hora actual)
);

-- Crear la tabla videojuegos
CREATE TABLE videojuegos (
    id INT AUTO_INCREMENT PRIMARY KEY,    -- Identificador único del videojuego
    nombre VARCHAR(255),                  -- Nombre del videojuego
    cantidad INT,                         -- Cantidad de existencias
    costo DOUBLE(10, 2),                  -- Costo del videojuego (usando formato de doble con 2 decimales)
    imagenUrl VARCHAR(255)                -- URL de la imagen del videojuego
);

-- INSERT INTO `videojuegos`.`usuarios` (`id`, `nombre`, `contrasena`, `correo`, `fechacreacion`) 
-- VALUES ('1', 'jchavarin', '12345', '112175@alumnouninter.mx', '2024-11-13 12:24:00');
