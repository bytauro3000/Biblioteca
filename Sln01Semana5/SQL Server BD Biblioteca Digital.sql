-- Crear base de datos
CREATE DATABASE BibliotecaDigital;
GO

USE BibliotecaDigital;
GO

-- Crear tabla Categoría
CREATE TABLE Categoria (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);
GO

-- Crear tabla Libro
CREATE TABLE Libro (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(200) NOT NULL,
    Autor VARCHAR(150) NOT NULL,
    Anio_publicacion INT NOT NULL,
    Resumen TEXT,
    NumPaginas INT NOT NULL,
    Editorial VARCHAR(100),
    Categoria_ID INT,
    CONSTRAINT FK_Categoria FOREIGN KEY (Categoria_ID) REFERENCES Categoria(ID)
);
GO

-- Insertar valores en Categoría
INSERT INTO Categoria (Nombre) VALUES
('Ciencia'),
('Literatura'),
('Tecnología'),
('Historia'),
('Arte');
GO

-- Insertar valores en Libro
INSERT INTO Libro (Titulo, Autor, Anio_publicacion, Resumen, NumPaginas, Editorial, Categoria_ID) VALUES
('El origen de las especies', 'Charles Darwin', 1859, 'Libro seminal sobre la teoría de la evolución.', 502, 'John Murray', 1),
('Cien años de soledad', 'Gabriel García Márquez', 1967, 'Obra maestra de la literatura latinoamericana.', 417, 'Sudamericana', 2),
('Clean Code', 'Robert C. Martin', 2008, 'Guía para escribir código limpio y mantenible.', 431, 'Prentice Hall', 3),
('Sapiens: De animales a dioses', 'Yuval Noah Harari', 2011, 'Breve historia de la humanidad.', 498, 'Debate', 4),
('La historia del arte', 'E. H. Gombrich', 1950, 'Introducción a la historia del arte universal.', 688, 'Phaidon Press', 5);
GO

-- Insertar más libros en la tabla Libro
INSERT INTO Libro (Titulo, Autor, Anio_publicacion, Resumen, NumPaginas, Editorial, Categoria_ID) VALUES
-- Categoría: Ciencia
('Breve historia del tiempo', 'Stephen Hawking', 1988, 'Una introducción a la cosmología moderna.', 256, 'Bantam Books', 1),
('El gen egoísta', 'Richard Dawkins', 1976, 'Teoría sobre la evolución basada en los genes.', 352, 'Oxford University Press', 1),
('La estructura de las revoluciones científicas', 'Thomas Kuhn', 1962, 'Análisis de los paradigmas en la ciencia.', 264, 'University of Chicago Press', 1),
-- Categoría: Literatura
('Don Quijote de la Mancha', 'Miguel de Cervantes', 1605, 'La obra cumbre de la literatura española.', 863, 'Francisco de Robles', 2),
('Orgullo y prejuicio', 'Jane Austen', 1813, 'Una historia sobre amor y prejuicios.', 432, 'T. Egerton', 2),
('1984', 'George Orwell', 1949, 'Novela distópica sobre un estado totalitario.', 328, 'Secker & Warburg', 2),
-- Categoría: Tecnología
('The Pragmatic Programmer', 'Andrew Hunt y David Thomas', 1999, 'Guía para programadores prácticos.', 352, 'Addison-Wesley', 3),
('Design Patterns', 'Erich Gamma et al.', 1994, 'Un catálogo de patrones de diseño.', 395, 'Addison-Wesley', 3),
('Artificial Intelligence: A Modern Approach', 'Stuart Russell y Peter Norvig', 2021, 'Texto líder en inteligencia artificial.', 1136, 'Pearson', 3),
-- Categoría: Historia
('Guns, Germs, and Steel', 'Jared Diamond', 1997, 'Impacto de la geografía en el desarrollo humano.', 480, 'W. W. Norton & Company', 4),
('El espejo enterrado', 'Carlos Fuentes', 1992, 'Historia cultural de Hispanoamérica.', 416, 'Fondo de Cultura Económica', 4),
('Historia de las civilizaciones', 'Arnold Toynbee', 1934, 'Análisis de las civilizaciones humanas.', 850, 'Oxford University Press', 4),
-- Categoría: Arte
('La interpretación de las artes visuales', 'Erwin Panofsky', 1939, 'Ensayo sobre la interpretación de las obras de arte.', 340, 'Harvard University Press', 5),
('Los principios del diseño', 'William Lidwell', 2003, 'Guía básica para diseñadores.', 272, 'Rockport Publishers', 5),
('El Renacimiento', 'Andrew Graham-Dixon', 1999, 'Estudio detallado sobre el Renacimiento.', 400, 'Thames & Hudson', 5);
GO

-- Procedimiento almacenado para retornar los datos de la tabla Libro con el nombre de la Categoria
CREATE PROCEDURE SP_ObtenerLibrosPorCategoria
    @categoriaID INT
AS
BEGIN
    SELECT 
        L.ID,
        L.Titulo,
        L.Autor,
        L.Anio_publicacion,
        L.Resumen,
        L.NumPaginas,
        L.Editorial,
        C.Nombre AS NombreCategoria
    FROM 
        Libro L
    LEFT JOIN 
        Categoria C ON L.Categoria_ID = C.ID
    WHERE
        L.Categoria_ID = @categoriaID;
END
GO

-- Ejecutar el procedimiento almacenado
-- EXEC SP_ObtenerLibrosPorCategoria 3;

-- SELECT * FROM Libro WHERE ID = 2

CREATE PROCEDURE SP_ActualizarLibro
    @pIdLibro INT,
    @pTitulo NVARCHAR(200),
    @pAutor NVARCHAR(150),
    @pAnio INT,
    @pResumen NVARCHAR(MAX),
    @pNPaginas INT,
    @pEditorial NVARCHAR(100),
    @pIdCategoria INT
AS
BEGIN
    -- Intentamos realizar la actualización
    BEGIN TRY
        UPDATE Libro 
        SET 
            Titulo = @pTitulo,
            Autor = @pAutor,
            Anio_publicacion = @pAnio,
            Resumen = @pResumen,
            NumPaginas = @pNPaginas,
            Editorial = @pEditorial,
            Categoria_ID = @pIdCategoria
        WHERE ID = @pIdLibro;

        -- Si se actualizó al menos una fila, consideramos la operación exitosa
        IF @@ROWCOUNT > 0
        BEGIN
            SELECT 1 AS Success;  -- Indica que la actualización fue exitosa
        END
        ELSE
        BEGIN
            SELECT 0 AS Success;  -- No se encontró el libro o no se actualizó nada
        END
    END TRY
    BEGIN CATCH
        SELECT 0 AS Success;  -- En caso de error, retornamos un 0
    END CATCH
END

-- EXEC SP_ActualizarLibro