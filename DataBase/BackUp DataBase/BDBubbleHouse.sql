USE [BDBubbleHouse]
GO
/****** Object:  Table [dbo].[TB_BITACORA]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_BITACORA](
	[ID_BITACORA] [int] IDENTITY(1,1) NOT NULL,
	[DSC_CLASE] [nvarchar](100) NULL,
	[DSC_METODO] [nvarchar](100) NULL,
	[NUM_TIPO] [smallint] NULL,
	[DSC_DESCRIPCION] [nvarchar](max) NULL,
	[DSC_REQUEST] [nvarchar](max) NULL,
	[DSC_RESPONSE] [nvarchar](max) NULL,
	[FEC_REGISTRO] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_BITACORA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_CATE_INGREDIENTE]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_CATE_INGREDIENTE](
	[ID_CATE_INGREDIENTE] [int] IDENTITY(1,1) NOT NULL,
	[DSC_NOMBRE_CATEGORIA] [nvarchar](max) NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_CATE_INGREDIENTE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_CATE_PRODUCTO]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_CATE_PRODUCTO](
	[ID_CATE_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[DSC_NOMBRE_CATEGORIA] [nvarchar](100) NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_CATE_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_ERROR_EN_BASE_DATOS]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_ERROR_EN_BASE_DATOS](
	[ID_ERROR_EN_BASE_DATOS] [int] IDENTITY(1,1) NOT NULL,
	[NUM_SEVERIVDAD] [int] NULL,
	[STORE_PROCEDURE] [nvarchar](50) NULL,
	[NUM_ERROR] [int] NULL,
	[DSC_DESCRIPCION] [nvarchar](max) NULL,
	[NUM_LINEA] [int] NULL,
	[FEC_ERROR] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_ERROR_EN_BASE_DATOS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_FACTURA]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_FACTURA](
	[ID_FACTURA] [int] IDENTITY(1,1) NOT NULL,
	[ID_PRODUCTO] [int] NULL,
	[ID_SESION] [nvarchar](100) NULL,
	[NUM_SUBTOTAL] [decimal](10, 2) NULL,
	[NUM_DESCUENTO] [decimal](10, 2) NULL,
	[NUM_TOTAL] [decimal](10, 2) NULL,
	[FECHA] [datetime] NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_FACTURA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_INGREDIENTE]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_INGREDIENTE](
	[ID_INGREDIENTE] [int] IDENTITY(1,1) NOT NULL,
	[ID_CATE_INGREDIENTE] [int] NULL,
	[DSC_NOMBRE_INGREDIENTE] [nvarchar](100) NULL,
	[DSC_DESCRIPCION] [nvarchar](max) NULL,
	[DSC_URL_IMAGEN] [nvarchar](max) NULL,
	[NUM_PRECIO] [decimal](10, 2) NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_INGREDIENTE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_PRODUCTO]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_PRODUCTO](
	[ID_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[ID_SUBCATE_PRODUCTO] [int] NULL,
	[DSC_NOMBRE_PRODUCTO] [nvarchar](100) NULL,
	[DSC_DESCRIPCION] [nvarchar](max) NULL,
	[DSC_URL_IMAGEN] [nvarchar](max) NULL,
	[NUM_PRECIO] [decimal](10, 2) NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_R_ROL_USUARIO]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_R_ROL_USUARIO](
	[ID_R_ROL_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[ID_ROL] [int] NULL,
	[ID_USUARIO] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_R_ROL_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_RECETA]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_RECETA](
	[ID_RECETA] [int] IDENTITY(1,1) NOT NULL,
	[ID_PRODUCTO] [int] NULL,
	[ID_ING_LACTEO] [int] NULL,
	[ID_ING_SABOR] [int] NULL,
	[ID_ING_AZUCAR] [int] NULL,
	[ID_ING_TOPPING] [int] NULL,
	[ID_ING_BORDEADO] [int] NULL,
	[ID_ING_BUBBLES] [int] NULL,
	[DSC_NOMBRE] [nvarchar](max) NULL,
	[DSC_TAMANO] [nvarchar](50) NULL,
	[FECHA] [datetime] NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_RECETA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_ROL]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_ROL](
	[ID_ROL] [int] IDENTITY(1,1) NOT NULL,
	[DSC_TIPO_ROL] [nvarchar](50) NULL,
	[DSC_PERMISOS] [nvarchar](max) NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_ROL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_SESION]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_SESION](
	[ID_SESION] [nvarchar](100) NOT NULL,
	[ID_USUARIO] [int] NULL,
	[DSC_SESION] [nvarchar](max) NULL,
	[DSC_ORIGEN] [nvarchar](max) NULL,
	[DSC_CIERRE] [nvarchar](max) NULL,
	[FEC_INICIO] [datetime] NULL,
	[FEC_CIERRE] [datetime] NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_SESION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_SUBCATEGORIA_PRODUCTO]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_SUBCATEGORIA_PRODUCTO](
	[ID_SUBCATE_PRODUCTO] [int] IDENTITY(1,1) NOT NULL,
	[ID_CATE_PRODUCTO_ID] [int] NULL,
	[DSC_NOMBRE_SUBCATEGORIA] [nvarchar](100) NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_SUBCATE_PRODUCTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_USUARIO]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_USUARIO](
	[ID_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[DSC_NOMBRE] [nvarchar](50) NULL,
	[DSC_PRIMER_APELLIDO] [nvarchar](50) NULL,
	[DSC_SEGUNDO_APELLIDO] [nvarchar](50) NULL,
	[DSC_CORREO] [nvarchar](50) NULL,
	[DSC_PASSWORD] [nvarchar](max) NULL,
	[FEC_REGISTRO] [datetime] NULL,
	[DSC_TELEFONO] [nvarchar](50) NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TB_FACTURA]  WITH CHECK ADD  CONSTRAINT [FK_TB_FACTURA_TB_PRODUCTO] FOREIGN KEY([ID_PRODUCTO])
REFERENCES [dbo].[TB_PRODUCTO] ([ID_PRODUCTO])
GO
ALTER TABLE [dbo].[TB_FACTURA] CHECK CONSTRAINT [FK_TB_FACTURA_TB_PRODUCTO]
GO
ALTER TABLE [dbo].[TB_FACTURA]  WITH CHECK ADD  CONSTRAINT [FK_TB_FACTURA_TB_SESION] FOREIGN KEY([ID_SESION])
REFERENCES [dbo].[TB_SESION] ([ID_SESION])
GO
ALTER TABLE [dbo].[TB_FACTURA] CHECK CONSTRAINT [FK_TB_FACTURA_TB_SESION]
GO
ALTER TABLE [dbo].[TB_INGREDIENTE]  WITH CHECK ADD FOREIGN KEY([ID_CATE_INGREDIENTE])
REFERENCES [dbo].[TB_CATE_INGREDIENTE] ([ID_CATE_INGREDIENTE])
GO
ALTER TABLE [dbo].[TB_PRODUCTO]  WITH CHECK ADD FOREIGN KEY([ID_SUBCATE_PRODUCTO])
REFERENCES [dbo].[TB_SUBCATEGORIA_PRODUCTO] ([ID_SUBCATE_PRODUCTO])
GO
ALTER TABLE [dbo].[TB_R_ROL_USUARIO]  WITH CHECK ADD FOREIGN KEY([ID_ROL])
REFERENCES [dbo].[TB_ROL] ([ID_ROL])
GO
ALTER TABLE [dbo].[TB_R_ROL_USUARIO]  WITH CHECK ADD FOREIGN KEY([ID_USUARIO])
REFERENCES [dbo].[TB_USUARIO] ([ID_USUARIO])
GO
ALTER TABLE [dbo].[TB_RECETA]  WITH CHECK ADD FOREIGN KEY([ID_ING_LACTEO])
REFERENCES [dbo].[TB_INGREDIENTE] ([ID_INGREDIENTE])
GO
ALTER TABLE [dbo].[TB_RECETA]  WITH CHECK ADD FOREIGN KEY([ID_ING_SABOR])
REFERENCES [dbo].[TB_INGREDIENTE] ([ID_INGREDIENTE])
GO
ALTER TABLE [dbo].[TB_RECETA]  WITH CHECK ADD FOREIGN KEY([ID_ING_AZUCAR])
REFERENCES [dbo].[TB_INGREDIENTE] ([ID_INGREDIENTE])
GO
ALTER TABLE [dbo].[TB_RECETA]  WITH CHECK ADD FOREIGN KEY([ID_ING_TOPPING])
REFERENCES [dbo].[TB_INGREDIENTE] ([ID_INGREDIENTE])
GO
ALTER TABLE [dbo].[TB_RECETA]  WITH CHECK ADD FOREIGN KEY([ID_ING_BORDEADO])
REFERENCES [dbo].[TB_INGREDIENTE] ([ID_INGREDIENTE])
GO
ALTER TABLE [dbo].[TB_RECETA]  WITH CHECK ADD FOREIGN KEY([ID_ING_BUBBLES])
REFERENCES [dbo].[TB_INGREDIENTE] ([ID_INGREDIENTE])
GO
ALTER TABLE [dbo].[TB_RECETA]  WITH CHECK ADD FOREIGN KEY([ID_PRODUCTO])
REFERENCES [dbo].[TB_PRODUCTO] ([ID_PRODUCTO])
GO
ALTER TABLE [dbo].[TB_SESION]  WITH CHECK ADD FOREIGN KEY([ID_USUARIO])
REFERENCES [dbo].[TB_USUARIO] ([ID_USUARIO])
GO
ALTER TABLE [dbo].[TB_SUBCATEGORIA_PRODUCTO]  WITH CHECK ADD FOREIGN KEY([ID_CATE_PRODUCTO_ID])
REFERENCES [dbo].[TB_CATE_PRODUCTO] ([ID_CATE_PRODUCTO])
GO
/****** Object:  StoredProcedure [dbo].[Desactivar_Ingrediente]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Desactivar_Ingrediente]
    @Id_Ingrediente int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar el ingrediente
        UPDATE TB_INGREDIENTE
        SET ESTADO = 2
        WHERE ID_INGREDIENTE = @Id_Ingrediente;

        SET @IDRETURN = 1; -- Éxito

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Desactivar_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Desactivar_Producto]
    @Id_Producto int
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            PRINT 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Cambiar el estado del producto a 0 (desactivado)
        UPDATE TB_PRODUCTO
        SET ESTADO = 2
        WHERE ID_PRODUCTO = @Id_Producto;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        -- Si ocurre algún error durante la transacción, se deshace la transacción
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Categoria_Ingrediente]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Eliminar_Categoria_Ingrediente]
    @Id_Categoria_Ingrediente int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la categoría de ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar la categoría de ingrediente
        UPDATE TB_CATE_INGREDIENTE
        SET ESTADO = 0
        WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente;

        SET @IDRETURN = 1; -- Éxito

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Categoria_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_Categoria_Producto]
    @Id_Categoria_Producto int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la categoría de producto existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La categoría de producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar la categoría de producto
        UPDATE TB_CATE_PRODUCTO
        SET ESTADO = 0
        WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto;

        SET @IDRETURN = 1; -- Éxito

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Factura]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_Factura]
    @Id_Factura int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la factura existe
        IF NOT EXISTS (SELECT * FROM TB_FACTURA WHERE ID_FACTURA = @Id_Factura)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La factura especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar la factura
        UPDATE TB_FACTURA
        SET ESTADO = 0
        WHERE ID_FACTURA = @Id_Factura;

        SET @IDRETURN = 1;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Ingrediente]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Eliminar_Ingrediente]
    @Id_Ingrediente int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar el ingrediente
        UPDATE TB_INGREDIENTE
        SET ESTADO = 0
        WHERE ID_INGREDIENTE = @Id_Ingrediente;

        SET @IDRETURN = 1; -- Éxito

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Eliminar_Producto]
    @Id_Producto int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Cambiar el estado del producto a 0 (desactivado)
        UPDATE TB_PRODUCTO
        SET ESTADO = 0
        WHERE ID_PRODUCTO = @Id_Producto;

        SET @IDRETURN = 1; -- Éxito

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_SubCategoria_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Eliminar_SubCategoria_Producto]
    @Id_SubCategoria_Producto int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la subcategoría de producto existe
        IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La subcategoría de producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar la subcategoría de producto
        UPDATE TB_SUBCATEGORIA_PRODUCTO
        SET ESTADO = 0
        WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto;

        SET @IDRETURN = 1; -- Éxito

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Usuario]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Eliminar_Usuario]
    @Id_Usuario int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT * FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El usuario especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Eliminar el usuario
        DELETE FROM TB_USUARIO
        WHERE ID_USUARIO = @Id_Usuario;

        SET @IDRETURN = 1; -- Éxito

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Bitacora]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Bitacora]
(
	@CLASE nvarchar(max),
	@METODO nvarchar(max),
	@TIPO smallint,
	@DESCRIPCION nvarchar(max),
	@REQUEST nvarchar(max),
	@RESPONSE nvarchar(max)
)
AS
BEGIN
    INSERT INTO TB_BITACORA (DSC_CLASE, DSC_METODO,	NUM_TIPO, DSC_DESCRIPCION, DSC_REQUEST,	DSC_RESPONSE, FEC_REGISTRO) 
	VALUES (@CLASE,	@METODO, @TIPO,	@DESCRIPCION, @REQUEST,	@RESPONSE, GETUTCDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Categoria_Ingrediente]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Categoria_Ingrediente]
    @Dsc_Nombre_Categoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Insertar la nueva categoría de ingrediente
        INSERT INTO TB_CATE_INGREDIENTE (DSC_NOMBRE_CATEGORIA, ESTADO)
        VALUES (@Dsc_Nombre_Categoria, 1);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Categoria_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Categoria_Producto]
    @Dsc_Nombre_Categoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Insertar la nueva categoría de producto
        INSERT INTO TB_CATE_PRODUCTO(DSC_NOMBRE_CATEGORIA, ESTADO)
        VALUES (@Dsc_Nombre_Categoria, 1);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Factura]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Factura]
    @Id_Producto int,
	@Id_Sesion int,
    @Num_Subtotal decimal(10,2),
	@Num_Descuento decimal(10,2),
	@Num_Total decimal(10,2),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT * FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Subtotal IS NULL OR LEN(@Num_Subtotal) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El subtotal es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Descuento IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El descuento es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Total IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El total es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        INSERT INTO TB_FACTURA(ID_PRODUCTO, ID_SESION, NUM_SUBTOTAL, NUM_DESCUENTO, NUM_TOTAL, FECHA, ESTADO)
        VALUES (@Id_Producto, @Id_Sesion, @Num_Subtotal, @Num_Descuento, @Num_Total, GETUTCDATE(), 1);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Ingrediente]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Ingrediente]
    @Id_Cate_Ingrediente int,
    @Dsc_Nombre_Ingrediente nvarchar(100),
    @Dsc_Descripcion nvarchar(max),
    @Dsc_Url_Imagen nvarchar(max),
    @Num_Precio decimal(10,2),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF NOT EXISTS (SELECT * FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Ingrediente) > 0 OR LEN(@Dsc_Nombre_Ingrediente) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del ingrediente contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'La descripción del ingrediente contiene caracteres especiales o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Aquí podrías agregar validaciones adicionales si es necesario

        INSERT INTO TB_INGREDIENTE (ID_CATE_INGREDIENTE, DSC_NOMBRE_INGREDIENTE, DSC_DESCRIPCION, DSC_URL_IMAGEN, NUM_PRECIO, ESTADO)
        VALUES (@Id_Cate_Ingrediente, @Dsc_Nombre_Ingrediente, @Dsc_Descripcion, @Dsc_Url_Imagen, @Num_Precio, 1);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Producto]
    @Id_SubCategoria_Producto int, 
    @Dsc_Nombre_Producto nvarchar(100),
    @Dsc_Descripcion nvarchar(max),
    @Dsc_Url_Imagen nvarchar(max),
    @Num_Precio float,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La subcategoría especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'La descripción del producto contiene caracteres especiales o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF ISNUMERIC(@Num_Precio) <> 1
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 5;
            SET @ERRORDESCRIPCION = 'El precio del producto debe ser un valor numérico.';
            RETURN; -- Salir del procedimiento almacenado
        END

        INSERT INTO TB_PRODUCTO (ID_SUBCATE_PRODUCTO, DSC_NOMBRE_PRODUCTO, DSC_DESCRIPCION, DSC_URL_IMAGEN, NUM_PRECIO, ESTADO)
        VALUES (@Id_SubCategoria_Producto, @Dsc_Nombre_Producto, @Dsc_Descripcion, @Dsc_Url_Imagen, @Num_Precio, 1);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Sesion]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Sesion]
	@Id_Sesion NVARCHAR(100),
    @Id_Usuario INT,
    @Dsc_Sesion NVARCHAR(MAX),
    @Dsc_Origen NVARCHAR(MAX),
    @Dsc_Cierre NVARCHAR(MAX),
    @Fec_Inicio DATETIME,
    @Fec_Cierre DATETIME,
    @Estado TINYINT,
    @IDRETURN INT OUTPUT,
    @ERRORID INT OUTPUT,
    @ERRORDESCRIPCION NVARCHAR(MAX) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT * FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El usuario especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Insertar la sesión
        INSERT INTO TB_SESION (ID_SESION, ID_USUARIO, DSC_SESION, DSC_ORIGEN, DSC_CIERRE, FEC_INICIO, FEC_CIERRE, ESTADO)
        VALUES (@Id_Sesion, @Id_Usuario, @Dsc_Sesion, @Dsc_Origen, @Dsc_Cierre, @Fec_Inicio, @Fec_Cierre, @Estado);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_SubCategoria_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_SubCategoria_Producto]
	@Id_Categoria_Producto int,
    @Dsc_Nombre_SubCategoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

		IF NOT EXISTS (SELECT * FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría del producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END


        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_SubCategoria) > 0 OR LEN(@Dsc_Nombre_SubCategoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la subcategoría contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Insertar la nueva subcategoría de producto
        INSERT INTO TB_CATE_PRODUCTO(ID_CATE_PRODUCTO, DSC_NOMBRE_CATEGORIA, ESTADO)
        VALUES (@Id_Categoria_Producto, @Dsc_Nombre_SubCategoria, 1);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Usuario]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Usuario]
    @Dsc_Nombre nvarchar(50),
    @Dsc_Primer_Apellido nvarchar(50),
    @Dsc_Segundo_Apellido nvarchar(50),
    @Dsc_Correo nvarchar(50),
    @Dsc_Password nvarchar(max),
    @Fec_Registro datetime,
    @Dsc_Telefono nvarchar(50),
    @Estado tinyint,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Primer_Apellido) > 0 OR LEN(@Dsc_Primer_Apellido) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El primer apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Segundo_Apellido) > 0 OR LEN(@Dsc_Segundo_Apellido) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'El segundo apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Correo) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'El correo está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Password) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 5;
            SET @ERRORDESCRIPCION = 'La contraseña está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        INSERT INTO TB_USUARIO (DSC_NOMBRE, DSC_PRIMER_APELLIDO, DSC_SEGUNDO_APELLIDO, DSC_CORREO, DSC_PASSWORD, FEC_REGISTRO, DSC_TELEFONO, ESTADO)
        VALUES (@Dsc_Nombre, @Dsc_Primer_Apellido, @Dsc_Segundo_Apellido, @Dsc_Correo, @Dsc_Password, @Fec_Registro, @Dsc_Telefono, @Estado);

        SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modifcar_SubCategoria_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modifcar_SubCategoria_Producto]
	@Id_SubCategoria_Producto int,
	@Id_Categoria_Producto int,
    @Dsc_Nombre_SubCategoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

		IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La subcategoría del producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT * FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría del producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END


        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_SubCategoria) > 0 OR LEN(@Dsc_Nombre_SubCategoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la subcategoría contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar la subcategoría de producto
        UPDATE TB_SUBCATEGORIA_PRODUCTO
        SET ID_CATE_PRODUCTO_ID = @Id_Categoria_Producto,
			DSC_NOMBRE_SUBCATEGORIA = @Dsc_Nombre_SubCategoria
        WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto;

        SET @IDRETURN = @Id_SubCategoria_Producto;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Categoria_Ingrediente]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Modificar_Categoria_Ingrediente]
    @Id_Categoria_Ingrediente int,
    @Dsc_Nombre_Categoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la categoría de ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Validaciones de datos
        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar la categoría de ingrediente
        UPDATE TB_CATE_INGREDIENTE
        SET DSC_NOMBRE_CATEGORIA = @Dsc_Nombre_Categoria
        WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente;

        SET @IDRETURN = @Id_Categoria_Ingrediente;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Categoria_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Categoria_Producto]
    @Id_Categoria_Producto int,
    @Dsc_Nombre_Categoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si la categoría de producto existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'La categoría de producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Validaciones de datos
        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar la categoría de producto
        UPDATE TB_CATE_PRODUCTO
        SET DSC_NOMBRE_CATEGORIA = @Dsc_Nombre_Categoria
        WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto;

        SET @IDRETURN = @Id_Categoria_Producto;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Factura]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Factura]
	@Id_Factura int,
    @Id_Producto int,
	@Id_Sesion int,
    @Num_Subtotal decimal(10,2),
	@Num_Descuento decimal(10,2),
	@Num_Total decimal(10,2),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

		IF NOT EXISTS (SELECT * FROM TB_FACTURA WHERE ID_FACTURA = @Id_Factura)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La factura especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

		IF NOT EXISTS (SELECT * FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Subtotal IS NULL OR LEN(@Num_Subtotal) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El subtotal es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Descuento IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El descuento es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF @Num_Total IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El total es nulo o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        UPDATE TB_FACTURA 
		SET ID_PRODUCTO = @Id_Producto,
			ID_SESION = @Id_Sesion, 
			NUM_SUBTOTAL = @Num_Subtotal, 
			NUM_DESCUENTO = @Num_Descuento, 
			NUM_TOTAL = @Num_Total, 
			FECHA = GETUTCDATE()
		WHERE ID_FACTURA = @Id_Factura;

        SET @IDRETURN = @Id_Factura;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Ingrediente]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Modificar_Ingrediente]
    @Id_Ingrediente int,
    @Id_Cate_Ingrediente int,
    @Dsc_Nombre_Ingrediente nvarchar(100),
    @Dsc_Descripcion nvarchar(max),
    @Dsc_Url_Imagen nvarchar(max),
    @Num_Precio decimal(10,2),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT * FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Verificar si la categoría de ingrediente especificada existe
        IF NOT EXISTS (SELECT * FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Validaciones de datos
        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Ingrediente) > 0 OR LEN(@Dsc_Nombre_Ingrediente) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 8;
            SET @ERRORDESCRIPCION = 'El nombre del ingrediente contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 9;
            SET @ERRORDESCRIPCION = 'La descripción del ingrediente contiene caracteres especiales o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 10;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar el ingrediente
        UPDATE TB_INGREDIENTE
        SET ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente,
            DSC_NOMBRE_INGREDIENTE = @Dsc_Nombre_Ingrediente,
            DSC_DESCRIPCION = @Dsc_Descripcion,
            DSC_URL_IMAGEN = @Dsc_Url_Imagen,
            NUM_PRECIO = @Num_Precio
        WHERE ID_INGREDIENTE = @Id_Ingrediente;

        SET @IDRETURN = @Id_Ingrediente;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Producto]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Modificar_Producto]
    @Id_Producto int,
    @Id_SubCategoria_Producto int, 
    @Dsc_Nombre_Producto nvarchar(100),
    @Dsc_Descripcion nvarchar(max),
    @Dsc_Url_Imagen nvarchar(max),
    @Num_Precio float,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Verificar si la subcategoría especificada existe
        IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'La subcategoría especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Validaciones de datos
        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 8;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 9;
            SET @ERRORDESCRIPCION = 'La descripción del producto contiene caracteres especiales o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 10;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF ISNUMERIC(@Num_Precio) <> 1
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 11;
            SET @ERRORDESCRIPCION = 'El precio del producto debe ser un valor numérico.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar el producto
        UPDATE TB_PRODUCTO
        SET ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto,
            DSC_NOMBRE_PRODUCTO = @Dsc_Nombre_Producto,
            DSC_DESCRIPCION = @Dsc_Descripcion,
            DSC_URL_IMAGEN = @Dsc_Url_Imagen,
            NUM_PRECIO = @Num_Precio
        WHERE ID_PRODUCTO = @Id_Producto;

        SET @IDRETURN = @Id_Producto;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Usuario]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Modificar_Usuario]
    @Id_Usuario int,
    @Dsc_Nombre nvarchar(50),
    @Dsc_Primer_Apellido nvarchar(50),
    @Dsc_Segundo_Apellido nvarchar(50),
    @Dsc_Correo nvarchar(50),
    @Dsc_Password nvarchar(max),
    @Fec_Registro datetime,
    @Dsc_Telefono nvarchar(50),
    @Estado tinyint,
	@IDRETURN int output,
	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION 

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT * FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
			set @idReturn = -1;
			set @errorId = 1;
			set @errorDescripcion = 'El usuario especificado no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 2;
			set @errorDescripcion = 'El nombre contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Primer_Apellido) > 0 OR LEN(@Dsc_Primer_Apellido) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 2;
			set @errorDescripcion = 'El primer apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Segundo_Apellido) > 0 OR LEN(@Dsc_Segundo_Apellido) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 3;
			set @errorDescripcion = 'El segundo apellido contiene caracteres especiales o está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Correo) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 4;
			set @errorDescripcion = 'El correo está vacío.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF LEN(@Dsc_Password) = 0
        BEGIN
			set @idReturn = -1;
			set @errorId = 5;
			set @errorDescripcion = 'La contraseña está vacía.';
            RETURN; -- Salir del procedimiento almacenado
        END

        -- Actualizar el usuario
        UPDATE TB_USUARIO
        SET DSC_NOMBRE = @Dsc_Nombre,
            DSC_PRIMER_APELLIDO = @Dsc_Primer_Apellido,
            DSC_SEGUNDO_APELLIDO = @Dsc_Segundo_Apellido,
            DSC_CORREO = @Dsc_Correo,
            DSC_PASSWORD = @Dsc_Password,
            FEC_REGISTRO = @Fec_Registro,
            DSC_TELEFONO = @Dsc_Telefono,
            ESTADO = @Estado
        WHERE ID_USUARIO = @Id_Usuario;

        COMMIT TRANSACTION 
    END TRY
    BEGIN CATCH
        set @idReturn = -1;
		set @errorId = ERROR_NUMBER();
		set @errorDescripcion = ERROR_MESSAGE();
        ROLLBACK TRANSACTION 
        -- Aquí puedes manejar el error como prefieras, por ejemplo, lanzar una excepción o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Cate_Ingredientes_Activos]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_Cate_Ingredientes_Activos]
AS
BEGIN
    -- Seleccionar todos los productos cuyo estado sea diferente de 0
    SELECT CI.ID_CATE_INGREDIENTE, CI.DSC_NOMBRE_CATEGORIA
    FROM TB_CATE_INGREDIENTE AS CI
    WHERE CI.ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Cate_Productos_Activos]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Cate_Productos_Activos]
AS
BEGIN
    -- Seleccionar todos las categorías de productos cuyo estado sea diferente de 0
    SELECT CP.ID_CATE_PRODUCTO, CP.DSC_NOMBRE_CATEGORIA
    FROM TB_CATE_PRODUCTO AS CP
    WHERE CP.ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Facturas_Completadas]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Facturas_Completadas]
AS
BEGIN
    -- Seleccionar todas las facturas no pagadas
    SELECT F.ID_FACTURA, F.ID_PRODUCTO, F.ID_SESION, F.NUM_SUBTOTAL, F.NUM_DESCUENTO, F.NUM_TOTAL, F.FECHA, F.ESTADO
    FROM TB_FACTURA AS F
    WHERE F.ESTADO = 3;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Facturas_No_Pagadas]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Facturas_No_Pagadas]
AS
BEGIN
    -- Seleccionar todas las facturas no pagadas
    SELECT F.ID_FACTURA, F.ID_PRODUCTO, F.ID_SESION, F.NUM_SUBTOTAL, F.NUM_DESCUENTO, F.NUM_TOTAL, F.FECHA, F.ESTADO
    FROM TB_FACTURA AS F
    WHERE F.ESTADO = 1;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Facturas_No_Preparadas]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Facturas_No_Preparadas]
AS
BEGIN
    -- Seleccionar todas las facturas no pagadas
    SELECT F.ID_FACTURA, F.ID_PRODUCTO, F.ID_SESION, F.NUM_SUBTOTAL, F.NUM_DESCUENTO, F.NUM_TOTAL, F.FECHA, F.ESTADO
    FROM TB_FACTURA AS F
    WHERE F.ESTADO = 2;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Ingredientes_Activos]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_Ingredientes_Activos]
AS
BEGIN
    -- Seleccionar todos los productos cuyo estado sea diferente de 0
    SELECT I.ID_INGREDIENTE, I.DSC_NOMBRE_INGREDIENTE, I.DSC_DESCRIPCION, I.DSC_URL_IMAGEN, I.NUM_PRECIO, CI.ID_CATE_INGREDIENTE, CI.DSC_NOMBRE_CATEGORIA
    FROM TB_INGREDIENTE AS I
    INNER JOIN TB_CATE_INGREDIENTE AS CI ON CI.ID_CATE_INGREDIENTE = I.ID_CATE_INGREDIENTE
    WHERE I.ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Productos_Activos]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_Productos_Activos]
AS
BEGIN
    -- Seleccionar todos los productos cuyo estado sea diferente de 0
    SELECT P.DSC_NOMBRE_PRODUCTO, P.DSC_DESCRIPCION, P.DSC_URL_IMAGEN, P.NUM_PRECIO, P.ESTADO, SUBC.ID_SUBCATE_PRODUCTO, SUBC.DSC_NOMBRE_SUBCATEGORIA, CATE.ID_CATE_PRODUCTO, CATE.DSC_NOMBRE_CATEGORIA
    FROM TB_PRODUCTO AS P
    INNER JOIN TB_SUBCATEGORIA_PRODUCTO AS SUBC ON P.ID_SUBCATE_PRODUCTO = SUBC.ID_SUBCATE_PRODUCTO
    INNER JOIN TB_CATE_PRODUCTO AS CATE ON SUBC.ID_CATE_PRODUCTO_ID = CATE.ID_CATE_PRODUCTO
    WHERE P.ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Roles]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Roles]
AS
BEGIN
    -- Seleccionar todos los roles cuyo estado sea diferente de 0
    SELECT R.ID_ROL, R.DSC_TIPO_ROL, R.DSC_PERMISOS, R.ESTADO
    FROM TB_ROL AS R
    WHERE ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Roles_Por_Id_Usuario]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Roles_Por_Id_Usuario]
@Id_Usuario INT
AS
BEGIN
    -- Seleccionar todos los roles de un usuario cuyo estado sea diferente de 0
    SELECT R.ID_ROL, R.DSC_TIPO_ROL, R.DSC_PERMISOS, R.ESTADO
    FROM TB_ROL AS R
	INNER JOIN TB_R_ROL_USUARIO AS RE ON RE.ID_ROL = R.ID_ROL
	INNER JOIN TB_USUARIO AS U ON U.ID_USUARIO = RE.ID_USUARIO
    WHERE R.ESTADO <> 0 AND U.ID_USUARIO=@Id_Usuario;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Sesion_Activa_By_IdUsuario]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_Sesion_Activa_By_IdUsuario]
	-- Add the parameters for the stored procedure here
	@Id_Usuario INT
AS
BEGIN
	SELECT S.ID_SESION, S.ID_USUARIO, S.DSC_SESION, S.FEC_INICIO, S.ESTADO, U.ID_USUARIO, U.DSC_CORREO, U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_TELEFONO, U.ESTADO,
	R.DSC_PERMISOS, R.DSC_TIPO_ROL FROM TB_SESION AS S
	INNER JOIN TB_USUARIO AS U ON U.ID_USUARIO = S.ID_USUARIO
	INNER JOIN TB_R_ROL_USUARIO AS RE ON RE.ID_USUARIO = U.ID_USUARIO
	INNER JOIN TB_ROL AS R ON R.ID_ROL = RE.ID_ROL
	WHERE S.ID_USUARIO = @Id_Usuario AND S.ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_SubCate_Productos_Activos]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_SubCate_Productos_Activos]
AS
BEGIN
    -- Seleccionar todos las subcategorías de productos cuyo estado sea diferente de 0
    SELECT SCP.ID_SUBCATE_PRODUCTO, SCP.ID_CATE_PRODUCTO_ID, SCP.DSC_NOMBRE_SUBCATEGORIA
    FROM TB_SUBCATEGORIA_PRODUCTO AS SCP
    WHERE SCP.ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_SubCate_Productos_Por_Cate]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_SubCate_Productos_Por_Cate]
@Id_Categoria_Producto int
AS
BEGIN
    -- Seleccionar todos las subcategorías de una categoría de productos cuyo estado sea diferente de 0
    SELECT SCP.ID_SUBCATE_PRODUCTO, SCP.ID_CATE_PRODUCTO_ID, SCP.DSC_NOMBRE_SUBCATEGORIA
    FROM TB_SUBCATEGORIA_PRODUCTO AS SCP
	INNER JOIN TB_CATE_PRODUCTO AS CP ON SCP.ID_CATE_PRODUCTO_ID = CP.ID_CATE_PRODUCTO
    WHERE SCP.ESTADO <> 0 AND CP.ID_CATE_PRODUCTO = @Id_Categoria_Producto;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Usuario]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_Usuario]
@Id_Usuario int
AS
BEGIN
    -- Seleccionar todos los usuarios cuyo estado sea diferente de 0
    SELECT U.ID_USUARIO, U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_CORREO, U.DSC_PASSWORD, U.FEC_REGISTRO, U.DSC_TELEFONO, U.ESTADO
    FROM TB_USUARIO AS U
    WHERE ESTADO <> 0 AND U.ID_USUARIO = @Id_Usuario;
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Usuarios_Activos]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_Usuarios_Activos]
AS
BEGIN
    -- Seleccionar todos los productos cuyo estado sea diferente de 0
    SELECT P.DSC_NOMBRE_PRODUCTO, P.DSC_DESCRIPCION, P.DSC_URL_IMAGEN, P.NUM_PRECIO, P.ESTADO, SUBC.ID_SUBCATE_PRODUCTO, SUBC.DSC_NOMBRE_SUBCATEGORIA, CATE.ID_CATE_PRODUCTO, CATE.DSC_NOMBRE_CATEGORIA
    FROM TB_PRODUCTO AS P
    INNER JOIN TB_SUBCATEGORIA_PRODUCTO AS SUBC ON P.ID_SUBCATE_PRODUCTO = SUBC.ID_SUBCATE_PRODUCTO
    INNER JOIN TB_CATE_PRODUCTO AS CATE ON SUBC.ID_CATE_PRODUCTO_ID = CATE.ID_CATE_PRODUCTO
    WHERE P.ESTADO <> 0;
END
GO
/****** Object:  StoredProcedure [dbo].[Obterner_Sesion]    Script Date: 13/4/2024 11:41:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Obterner_Sesion]
	-- Add the parameters for the stored procedure here
	@Id_Sesion NVARCHAR(100)
AS
BEGIN
	SELECT S.ID_SESION, S.ID_USUARIO, S.DSC_SESION, S.FEC_INICIO, S.ESTADO, U.ID_USUARIO, U.DSC_CORREO, U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_TELEFONO, U.ESTADO,
	R.DSC_PERMISOS, R.DSC_TIPO_ROL FROM TB_SESION AS S
	INNER JOIN TB_USUARIO AS U ON U.ID_USUARIO = S.ID_USUARIO
	INNER JOIN TB_R_ROL_USUARIO AS RE ON RE.ID_USUARIO = U.ID_USUARIO
	INNER JOIN TB_ROL AS R ON R.ID_ROL = RE.ID_ROL
	WHERE S.ID_SESION = @Id_Sesion;
END
GO
