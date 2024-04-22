USE [BDBubbleHouse]
GO
/****** Object:  Table [dbo].[TB_BITACORA]    Script Date: 21/04/2024 10:31:55 p. m. ******/
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
/****** Object:  Table [dbo].[TB_CATE_INGREDIENTE]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_CATE_PRODUCTO]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_ERROR_EN_BASE_DATOS]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_FACTURA]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_INGREDIENTE]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_PRODUCTO]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_R_ROL_USUARIO]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_RECETA]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_RECUPERACION_PASSWORD]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_RECUPERACION_PASSWORD](
	[ID_RECUPERACION_PASSWORD] [int] IDENTITY(1,1) NOT NULL,
	[DSC_TOKEN] [nvarchar](100) NULL,
	[ID_USUARIO] [int] NULL,
	[FEC_SOLICITUD] [datetime] NULL,
	[FEC_USO] [datetime] NULL,
	[ESTADO] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_RECUPERACION_PASSWORD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_ROL]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_SESION]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_SUBCATEGORIA_PRODUCTO]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  Table [dbo].[TB_USUARIO]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
ALTER TABLE [dbo].[TB_RECUPERACION_PASSWORD]  WITH CHECK ADD  CONSTRAINT [FK_TB_RECUPERACION_TB_USUARIO] FOREIGN KEY([ID_USUARIO])
REFERENCES [dbo].[TB_USUARIO] ([ID_USUARIO])
GO
ALTER TABLE [dbo].[TB_RECUPERACION_PASSWORD] CHECK CONSTRAINT [FK_TB_RECUPERACION_TB_USUARIO]
GO
ALTER TABLE [dbo].[TB_SESION]  WITH CHECK ADD FOREIGN KEY([ID_USUARIO])
REFERENCES [dbo].[TB_USUARIO] ([ID_USUARIO])
GO
ALTER TABLE [dbo].[TB_SUBCATEGORIA_PRODUCTO]  WITH CHECK ADD FOREIGN KEY([ID_CATE_PRODUCTO_ID])
REFERENCES [dbo].[TB_CATE_PRODUCTO] ([ID_CATE_PRODUCTO])
GO
/****** Object:  StoredProcedure [dbo].[Desactivar_Ingrediente]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar el ingrediente
			UPDATE TB_INGREDIENTE
			SET ESTADO = 2
			WHERE ID_INGREDIENTE = @Id_Ingrediente;

			SET @IDRETURN = 1; -- Éxito

        END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Desactivar_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
    @Id_Producto int,
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE
		BEGIN
			-- Cambiar el estado del producto a 0 (desactivado)
			UPDATE TB_PRODUCTO
			SET ESTADO = 2
			WHERE ID_PRODUCTO = @Id_Producto;
			SET @IDRETURN = 1; -- Éxito
        END 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Categoria_Ingrediente]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        -- Verificar si la categoría de ingrediente existe
        IF NOT EXISTS (SELECT ID_CATE_INGREDIENTE FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar la categoría de ingrediente
			UPDATE TB_CATE_INGREDIENTE
			SET ESTADO = 0
			WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente;

			SET @IDRETURN = 1; -- Éxito
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Categoria_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
        -- Verificar si la categoría de producto existe
        IF NOT EXISTS (SELECT ID_CATE_PRODUCTO FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La categoría de producto especificada no existe.';
            
        END
		ELSE
		BEGIN
			-- Eliminar la categoría de producto
			UPDATE TB_CATE_PRODUCTO
			SET ESTADO = 0
			WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto;

			SET @IDRETURN = 1; -- Éxito
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		--Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Factura]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
        -- Verificar si la factura existe
        IF NOT EXISTS (SELECT ID_FACTURA FROM TB_FACTURA WHERE ID_FACTURA = @Id_Factura)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La factura especificada no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar la factura
			UPDATE TB_FACTURA
			SET ESTADO = 0
			WHERE ID_FACTURA = @Id_Factura;

			SET @IDRETURN = 1;
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		--Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Ingrediente]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar el ingrediente
			UPDATE TB_INGREDIENTE
			SET ESTADO = 0
			WHERE ID_INGREDIENTE = @Id_Ingrediente;

			SET @IDRETURN = 1; -- Éxito
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE
			BEGIN
			-- Cambiar el estado del producto a 0 (desactivado)
			UPDATE TB_PRODUCTO
			SET ESTADO = 0
			WHERE ID_PRODUCTO = @Id_Producto;

			SET @IDRETURN = 1; -- Éxito
			END
        
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

        INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();  
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Receta]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jurguen Monge
-- Create date: 13-04-2024
-- Description:	SP de eliminar receta
-- =============================================
CREATE PROCEDURE [dbo].[Eliminar_Receta]
	@ID_RECETA INT,
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
	BEGIN TRY
			-- Verificar si la receta existe
			IF NOT EXISTS (SELECT ID_RECETA FROM TB_RECETA WHERE ID_RECETA = @ID_RECETA)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 12;
				SET @ERRORDESCRIPCION = 'La receta especificada no existe.';
			END
			ELSE
			BEGIN
				 -- Eliminar la receta
				UPDATE TB_RECETA
				SET ESTADO = 0
				WHERE ID_RECETA = @ID_RECETA;

				SET @IDRETURN = 1;
			END
	END TRY
	BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Sesion]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>D
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Eliminar_Sesion]
    @Id_Sesion nvarchar(max),
    @DSC_cierre nvarchar(max),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;
        -- Verificar si la sesión existe
        IF NOT EXISTS (SELECT ID_SESION FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
        END
		ELSE
		BEGIN
			-- Eliminar la sesión
			UPDATE TB_SESION
			SET ESTADO = 0,
			    DSC_CIERRE = @DSC_cierre,
			    FEC_CIERRE = GETDATE()
			WHERE ID_SESION = @Id_Sesion;

			SET @IDRETURN = 1; -- Éxito
		END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        -- Bitácora de error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[Eliminar_SubCategoria_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
        -- Verificar si la subcategoría de producto existe
        IF NOT EXISTS (SELECT ID_SUBCATE_PRODUCTO FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 12;
            SET @ERRORDESCRIPCION = 'La subcategoría de producto especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END
		ELSE
		BEGIN
			-- Eliminar la subcategoría de producto
			UPDATE TB_SUBCATEGORIA_PRODUCTO
			SET ESTADO = 0
			WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto;

			SET @IDRETURN = 1; -- Éxito
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Usuario]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT ID_USUARIO FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El usuario especificado no existe.';
        END
		ELSE
		BEGIN
			UPDATE TB_USUARIO SET
			ESTADO = 0
			WHERE ID_USUARIO = @Id_Usuario;

			SET @IDRETURN = 1; -- Éxito

        END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Bitacora]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Insertar_Categoria_Ingrediente]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
        

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Insertar la nueva categoría de ingrediente
			INSERT INTO TB_CATE_INGREDIENTE (DSC_NOMBRE_CATEGORIA, ESTADO)
			VALUES (@Dsc_Nombre_Categoria, 1);

			SET @IDRETURN = SCOPE_IDENTITY();
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
		(
			NUM_SEVERIVDAD,
			STORE_PROCEDURE,
			NUM_ERROR,
			DSC_DESCRIPCION,
			NUM_LINEA,
			FEC_ERROR
		) 
		SELECT ERROR_SEVERITY(),
				ERROR_PROCEDURE(),
				ERROR_NUMBER(),
				ERROR_MESSAGE(),
				ERROR_LINE(),
				GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Categoria_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
        -- Insertar la nueva categoría de producto
        INSERT INTO TB_CATE_PRODUCTO(DSC_NOMBRE_CATEGORIA, ESTADO)
        VALUES (@Dsc_Nombre_Categoria, 1);

        SET @IDRETURN = SCOPE_IDENTITY();
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Factura]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE IF NOT EXISTS (SELECT * FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
        END
        ELSE IF @Num_Subtotal IS NULL OR LEN(@Num_Subtotal) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El subtotal es nulo o está vacío.';
        END
        ELSE IF @Num_Descuento IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El descuento es nulo o está vacío.';
        END
        ELSE IF @Num_Total IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El total es nulo o está vacío.';
        END
        ELSE
        BEGIN
            INSERT INTO TB_FACTURA(ID_PRODUCTO, ID_SESION, NUM_SUBTOTAL, NUM_DESCUENTO, NUM_TOTAL, FECHA, ESTADO)
            VALUES (@Id_Producto, @Id_Sesion, @Num_Subtotal, @Num_Descuento, @Num_Total, GETUTCDATE(), 1);

            SET @IDRETURN = SCOPE_IDENTITY();
        END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
		--Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Ingrediente]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        IF NOT EXISTS (SELECT ID_CATE_INGREDIENTE FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Ingrediente) > 0 OR LEN(@Dsc_Nombre_Ingrediente) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del ingrediente contiene caracteres especiales o está vacío.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'La descripción del ingrediente contiene caracteres especiales o está vacía.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
        END
		ELSE
		BEGIN
			-- Aquí podrías agregar validaciones adicionales si es necesario

			INSERT INTO TB_INGREDIENTE (ID_CATE_INGREDIENTE, DSC_NOMBRE_INGREDIENTE, DSC_DESCRIPCION, DSC_URL_IMAGEN, NUM_PRECIO, ESTADO)
			VALUES (@Id_Cate_Ingrediente, @Dsc_Nombre_Ingrediente, @Dsc_Descripcion, @Dsc_Url_Imagen, @Num_Precio, 1);

			SET @IDRETURN = SCOPE_IDENTITY();
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        IF NOT EXISTS (SELECT * FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La subcategoría especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'La descripción del producto contiene caracteres especiales o está vacía.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
        END
		ELSE IF ISNUMERIC(@Num_Precio) <> 1
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 5;
            SET @ERRORDESCRIPCION = 'El precio del producto debe ser un valor numérico.';
        END
		ELSE
		BEGIN
			INSERT INTO TB_PRODUCTO (ID_SUBCATE_PRODUCTO, DSC_NOMBRE_PRODUCTO, DSC_DESCRIPCION, DSC_URL_IMAGEN, NUM_PRECIO, ESTADO)
			VALUES (@Id_SubCategoria_Producto, @Dsc_Nombre_Producto, @Dsc_Descripcion, @Dsc_Url_Imagen, @Num_Precio, 1);

			SET @IDRETURN = SCOPE_IDENTITY();

        END 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[Insertar_Receta]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fernanda
-- Create date: 13-04-2024
-- Description:	SP Insertar Receta
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Receta]

	@ID_RECETA INT,
	@ID_PRODUCTO INT,
	@ID_ING_LACTEO INT,
	@ID_ING_SABOR INT,
	@ID_ING_AZUCAR INT,
	@ID_ING_TOPPING INT,
	@ID_ING_BORDEADO INT,
	@ID_ING_BUBBLES INT,
	@DSC_NOMBRE VARCHAR(MAX),
	@DSC_TAMANO VARCHAR(50),
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

    IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @ID_PRODUCTO)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
	ELSE
		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_LACTEO)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Lacteo especificado no existe.';
            
        END
    ELSE
	    IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_SABOR)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Sabor especificado no existe.';
            
        END
   ELSE
		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_AZUCAR)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El azucar especificado no existe.';
            
        END
   ELSE
	    IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_TOPPING)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Topping especificado no existe.';
            
        END
   ELSE
		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BORDEADO)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Bordeado especificado no existe.';
            
        END
   ELSE
		IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BUBBLES)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El Bubbles especificado no existe.';
            
        END
   ELSE
	    IF PATINDEX('%[^a-zA-Z0-9 ]%', @DSC_NOMBRE) > 0 OR LEN(@DSC_NOMBRE) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
           
        END
   ELSE
		IF PATINDEX('%[^a-zA-Z0-9 ]%', @DSC_TAMANO) > 0 OR LEN(@DSC_TAMANO) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El tamaño del producto contiene caracteres especiales o está vacío.';
            
        END
   ELSE
      BEGIN
	     BEGIN TRANSACTION

         INSERT INTO TB_RECETA(
		      ID_RECETA, 
			  ID_PRODUCTO, 
			  ID_ING_TOPPING, 
			  ID_ING_SABOR, 
			  ID_ING_LACTEO, 
			  ID_ING_BUBBLES, 
			  ID_ING_BORDEADO, 
			  ID_ING_AZUCAR, 
			  DSC_NOMBRE, 
			  DSC_TAMANO, 
			  FECHA, 
			  ESTADO
			  )VALUES(
			  @ID_RECETA, 
			  @ID_PRODUCTO, 
			  @ID_ING_TOPPING, 
			  @ID_ING_SABOR, 
			  @ID_ING_LACTEO, 
			  @ID_ING_BUBBLES,
			  @ID_ING_BORDEADO, 
			  @ID_ING_AZUCAR,
			  @DSC_NOMBRE, 
			  @DSC_TAMANO, 
			  GETUTCDATE(), 
			  1
			  );
              SET @IDRETURN = SCOPE_IDENTITY();

        COMMIT TRANSACTION
	  END
END TRY

	BEGIN CATCH
	   ROLLBACK TRANSACTION 
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()

        
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Recuperacion_Password]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insertar_Recuperacion_Password]
    @Dsc_Token nvarchar(100),
    @Dsc_Correo nvarchar(50),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
		IF PATINDEX('%[^a-zA-Z0-9@.]%', @Dsc_Correo) > 0 OR LEN(@Dsc_Correo) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'El correo contiene caracteres especiales o está vacía.';
        END
		ELSE
		BEGIN
			IF LEN(@Dsc_Token) = 0
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 2;
				SET @ERRORDESCRIPCION = 'El token está vacío';
			END
			ELSE
			BEGIN
				DECLARE @ID_USUARIO int = 0;
				IF NOT EXISTS (SELECT U.ID_USUARIO FROM TB_USUARIO AS U WHERE U.DSC_CORREO = @Dsc_Correo)
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 1;
					SET @ERRORDESCRIPCION = 'El Usuario no existe';
				END
				ELSE
				BEGIN
					SELECT @ID_USUARIO = U.ID_USUARIO 
					FROM TB_USUARIO AS U 
					WHERE U.DSC_CORREO = @Dsc_Correo;
				END

				INSERT INTO TB_RECUPERACION_PASSWORD (DSC_TOKEN, ID_USUARIO, FEC_SOLICITUD, FEC_USO, ESTADO)
				VALUES (@Dsc_Token,@ID_USUARIO,GETDATE(),NULL,1);

				SET @IDRETURN = SCOPE_IDENTITY();
			END
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Sesion]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
    @IDRETURN INT OUTPUT,
    @ERRORID INT OUTPUT,
    @ERRORDESCRIPCION NVARCHAR(MAX) OUTPUT
AS
BEGIN
    BEGIN TRY

        -- Verificar si el usuario existe
        IF NOT EXISTS (SELECT ID_USUARIO FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El usuario especificado no existe.';
        END
		ELSE
		BEGIN
			-- Insertar la sesión
			INSERT INTO TB_SESION (ID_SESION, ID_USUARIO, DSC_SESION, DSC_ORIGEN, DSC_CIERRE, FEC_INICIO, FEC_CIERRE, ESTADO)
			VALUES (@Id_Sesion, @Id_Usuario, @Dsc_Sesion, @Dsc_Origen, null, GETDATE(), null, 1);

			SET @IDRETURN = SCOPE_IDENTITY();

        END 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_SubCategoria_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

		IF NOT EXISTS (SELECT ID_CATE_PRODUCTO FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría del producto especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_SubCategoria) > 0 OR LEN(@Dsc_Nombre_SubCategoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la subcategoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Insertar la nueva subcategoría de producto
			INSERT INTO TB_CATE_PRODUCTO(ID_CATE_PRODUCTO, DSC_NOMBRE_CATEGORIA, ESTADO)
			VALUES (@Id_Categoria_Producto, @Dsc_Nombre_SubCategoria, 1);

			SET @IDRETURN = SCOPE_IDENTITY();
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertar_Usuario]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insertar_Usuario]
    @Dsc_Nombre nvarchar(50),
    @Dsc_Primer_Apellido nvarchar(50),
    @Dsc_Segundo_Apellido nvarchar(50),
    @Dsc_Correo nvarchar(50),
    @Dsc_Password nvarchar(max),
    @Dsc_Telefono nvarchar(50),
	@ID_ROL int,
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
			IF EXISTS (SELECT DSC_CORREO FROM TB_USUARIO WHERE DSC_CORREO = @Dsc_Correo)
				-- Si, el correo si está registrado. Devolver error.
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 1; --Correo ya registrado
					SET @ERRORDESCRIPCION = 'Correo ya registrado';
				END
			ELSE
				IF NOT EXISTS (SELECT ID_ROL FROM TB_ROL WHERE ID_ROL = @ID_ROL)
					BEGIN
						SET @IDRETURN = -1;
						SET @ERRORID = 2;
						SET @ERRORDESCRIPCION = 'El Rol seleccionado no existe';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
				BEGIN
				SET @IDRETURN = -1;
					SET @ERRORID = 3;
					SET @ERRORDESCRIPCION = 'El nombre contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Primer_Apellido) > 0 OR LEN(@Dsc_Primer_Apellido) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 4;
					SET @ERRORDESCRIPCION = 'El primer apellido contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Segundo_Apellido) > 0 OR LEN(@Dsc_Segundo_Apellido) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 5;
					SET @ERRORDESCRIPCION = 'El segundo apellido contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF LEN(@Dsc_Password) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 6;
					SET @ERRORDESCRIPCION = 'La contraseña está vacía.';
				END
			ELSE
			IF LEN(@Dsc_Telefono) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 7;
					SET @ERRORDESCRIPCION = 'El telefono está vacío.';
				END
			ELSE
				BEGIN
					BEGIN TRANSACTION
						INSERT INTO TB_USUARIO(
							DSC_NOMBRE,
							DSC_PRIMER_APELLIDO,
							DSC_SEGUNDO_APELLIDO,
							DSC_CORREO,
							DSC_PASSWORD,
							FEC_REGISTRO,
							DSC_TELEFONO,
							ESTADO
						)VALUES(
							@Dsc_Nombre,
							@Dsc_Primer_Apellido,
							@Dsc_Segundo_Apellido,
							@Dsc_Correo,
							@Dsc_Password,
							GETUTCDATE(),
							@Dsc_Telefono,
							1
						)
						set @idReturn = scope_identity();

						INSERT INTO TB_R_ROL_USUARIO(
							ID_ROL,
							ID_USUARIO
						)VALUES(
							@ID_ROL,
							@IDRETURN
						)
				COMMIT TRANSACTION
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Categoria_Ingrediente]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        -- Verificar si la categoría de ingrediente existe
        IF NOT EXISTS (SELECT ID_CATE_INGREDIENTE FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Actualizar la categoría de ingrediente
			UPDATE TB_CATE_INGREDIENTE
			SET DSC_NOMBRE_CATEGORIA = @Dsc_Nombre_Categoria
			WHERE ID_CATE_INGREDIENTE = @Id_Categoria_Ingrediente;

			SET @IDRETURN = @Id_Categoria_Ingrediente;

        END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[Modificar_Categoria_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        -- Verificar si la categoría de producto existe
        IF NOT EXISTS (SELECT ID_CATE_PRODUCTO FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'La categoría de producto especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Categoria) > 0 OR LEN(@Dsc_Nombre_Categoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El nombre de la categoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Actualizar la categoría de producto
			UPDATE TB_CATE_PRODUCTO
			SET DSC_NOMBRE_CATEGORIA = @Dsc_Nombre_Categoria
			WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto;

			SET @IDRETURN = @Id_Categoria_Producto;
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Factura]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

		IF NOT EXISTS (SELECT * FROM TB_FACTURA WHERE ID_FACTURA = @Id_Factura)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La factura especificada no existe.';
        END
		ELSE IF NOT EXISTS (SELECT * FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE IF NOT EXISTS (SELECT * FROM TB_SESION WHERE ID_SESION = @Id_Sesion)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La sesión especificada no existe.';
        END
		ELSE IF @Num_Subtotal IS NULL OR LEN(@Num_Subtotal) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El subtotal es nulo o está vacío.';
        END
		ELSE IF @Num_Descuento IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El descuento es nulo o está vacío.';
        END
		ELSE IF @Num_Total IS NULL OR LEN(@Num_Descuento) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El total es nulo o está vacío.';
        END
		ELSE
		BEGIN
			UPDATE TB_FACTURA 
			SET ID_PRODUCTO = @Id_Producto,
				ID_SESION = @Id_Sesion, 
				NUM_SUBTOTAL = @Num_Subtotal, 
				NUM_DESCUENTO = @Num_Descuento, 
				NUM_TOTAL = @Num_Total, 
				FECHA = GETUTCDATE()
			WHERE ID_FACTURA = @Id_Factura;

			SET @IDRETURN = @Id_Factura;
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Ingrediente]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        -- Verificar si el ingrediente existe
        IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @Id_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'El ingrediente especificado no existe.';
        END

        ELSE IF NOT EXISTS (SELECT ID_CATE_INGREDIENTE FROM TB_CATE_INGREDIENTE WHERE ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'La categoría de ingrediente especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Ingrediente) > 0 OR LEN(@Dsc_Nombre_Ingrediente) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 8;
            SET @ERRORDESCRIPCION = 'El nombre del ingrediente contiene caracteres especiales o está vacío.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 9;
            SET @ERRORDESCRIPCION = 'La descripción del ingrediente contiene caracteres especiales o está vacía.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 10;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
        END
		ELSE
		BEGIN
			-- Actualizar el ingrediente
			UPDATE TB_INGREDIENTE
			SET ID_CATE_INGREDIENTE = @Id_Cate_Ingrediente,
				DSC_NOMBRE_INGREDIENTE = @Dsc_Nombre_Ingrediente,
				DSC_DESCRIPCION = @Dsc_Descripcion,
				DSC_URL_IMAGEN = @Dsc_Url_Imagen,
				NUM_PRECIO = @Num_Precio
			WHERE ID_INGREDIENTE = @Id_Ingrediente;

			SET @IDRETURN = @Id_Ingrediente;

        END 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[Modificar_Password]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Modificar_Password]
    @Dsc_Token nvarchar(100),
    @New_Password nvarchar(max),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
        IF LEN(@Dsc_Token) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El token está vacío';
        END
        ELSE
        BEGIN
            IF EXISTS (SELECT R.ID_USUARIO FROM TB_RECUPERACION_PASSWORD AS R WHERE R.DSC_TOKEN = @Dsc_Token AND R.ESTADO = 1)
            BEGIN
                DECLARE @FEC_SOLICITUD DATETIME;
                DECLARE @ID_USUARIO int = 0;

                SELECT @FEC_SOLICITUD = R.FEC_SOLICITUD 
                FROM TB_RECUPERACION_PASSWORD AS R 
                WHERE R.DSC_TOKEN = @Dsc_Token AND R.ESTADO = 1;

                IF DATEDIFF(HOUR, @FEC_SOLICITUD, GETDATE()) >= 1
                BEGIN
                    SET @IDRETURN = -1;
                    SET @ERRORID = 1;
                    SET @ERRORDESCRIPCION = 'El Token ya expiró';

                    UPDATE TB_RECUPERACION_PASSWORD 
                    SET FEC_USO = GETDATE(), ESTADO = 0
                    WHERE DSC_TOKEN = @Dsc_Token AND ESTADO = 1;
                END
                ELSE
                BEGIN
                    SELECT @ID_USUARIO = R.ID_USUARIO 
                    FROM TB_RECUPERACION_PASSWORD AS R 
                    WHERE R.DSC_TOKEN = @Dsc_Token AND R.ESTADO = 1;

                    UPDATE TB_USUARIO 
                    SET DSC_PASSWORD = @New_Password
                    WHERE ID_USUARIO = @ID_USUARIO;

                    UPDATE TB_RECUPERACION_PASSWORD 
                    SET FEC_USO = GETDATE(), ESTADO = 0
                    WHERE DSC_TOKEN = @Dsc_Token AND ESTADO = 1;
                END
            END
            ELSE
            BEGIN
				SET @IDRETURN = -1;
                SET @ERRORID = 1;
                SET @ERRORDESCRIPCION = 'El Usuario no existe';
            END  
        END
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        INSERT INTO TB_ERROR_EN_BASE_DATOS 
            (
                NUM_SEVERIVDAD,
                STORE_PROCEDURE,
                NUM_ERROR,
                DSC_DESCRIPCION,
                NUM_LINEA,
                FEC_ERROR
            ) 
        SELECT 
            ERROR_SEVERITY(),
            ERROR_PROCEDURE(),
            ERROR_NUMBER(),
            ERROR_MESSAGE(),
            ERROR_LINE(),
            GETUTCDATE();
        
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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

        -- Verificar si el producto existe
        IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @Id_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 6;
            SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
        END
		ELSE IF NOT EXISTS (SELECT ID_SUBCATE_PRODUCTO FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'La subcategoría especificada no existe.';
        END

        ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 8;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o está vacío.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 9;
            SET @ERRORDESCRIPCION = 'La descripción del producto contiene caracteres especiales o está vacía.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 10;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no válidos o está vacía.';
        END
		ELSE IF ISNUMERIC(@Num_Precio) <> 1
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 11;
            SET @ERRORDESCRIPCION = 'El precio del producto debe ser un valor numérico.';
        END
		ELSE
		BEGIN
			-- Actualizar el producto
			UPDATE TB_PRODUCTO
			SET ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto,
				DSC_NOMBRE_PRODUCTO = @Dsc_Nombre_Producto,
				DSC_DESCRIPCION = @Dsc_Descripcion,
				DSC_URL_IMAGEN = @Dsc_Url_Imagen,
				NUM_PRECIO = @Num_Precio
			WHERE ID_PRODUCTO = @Id_Producto;

			SET @IDRETURN = @Id_Producto;

        END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[Modificar_Receta]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_Receta]
	@ID_RECETA INT,
	@ID_PRODUCTO INT,
	@ID_ING_LACTEO INT,
	@ID_ING_SABOR INT,
	@ID_ING_AZUCAR INT,
	@ID_ING_TOPPING INT,
	@ID_ING_BORDEADO INT,
	@ID_ING_BUBBLES INT,
	@DSC_NOMBRE NVARCHAR(MAX),
	@DSC_TAMANO NVARCHAR(50),
	@IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
	 BEGIN TRY
			IF NOT EXISTS (SELECT ID_RECETA FROM TB_RECETA WHERE ID_RECETA = @ID_RECETA)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 1;
				SET @ERRORDESCRIPCION = 'La receta especificada no existe.';
			END
			ELSE IF NOT EXISTS (SELECT ID_PRODUCTO FROM TB_PRODUCTO WHERE ID_PRODUCTO = @ID_PRODUCTO)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 2;
				SET @ERRORDESCRIPCION = 'El producto especificado no existe.';
				
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_LACTEO)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 3;
				SET @ERRORDESCRIPCION = 'El ingrediente lacteo especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_SABOR)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 4;
				SET @ERRORDESCRIPCION = 'El ingrediente sabor especificado no existe.';
				
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_AZUCAR)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 5;
				SET @ERRORDESCRIPCION = 'El ingrediente azúcar especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_TOPPING)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 6;
				SET @ERRORDESCRIPCION = 'El ingrediente topping especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BORDEADO)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 7;
				SET @ERRORDESCRIPCION = 'El ingrediente de bordeado especificado no existe.';
				 
			END
			ELSE IF NOT EXISTS (SELECT ID_INGREDIENTE FROM TB_INGREDIENTE WHERE ID_INGREDIENTE = @ID_ING_BUBBLES)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 8;
				SET @ERRORDESCRIPCION = 'El ingrediente de bubble especificado no existe.';
				 
			END
			ELSE IF PATINDEX('%[^a-zA-Z0-9 ":]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
			BEGIN
				SET @idReturn = -1;
				SET @errorId = 9;
				SET @errorDescripcion = 'El nombre contiene caracteres especiales o está vacío.';
				
			END
			ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @DSC_TAMANO) > 0 OR LEN(@DSC_TAMANO) = 0
			BEGIN
				set @idReturn = -1;
				set @errorId = 2;
				set @errorDescripcion = 'El tamaño contiene caracteres especiales o está vacío.';
				
			END
			ELSE
			BEGIN
				UPDATE TB_RECETA SET
					ID_PRODUCTO = @ID_PRODUCTO,
					ID_ING_LACTEO = @ID_ING_LACTEO,
					ID_ING_SABOR = @ID_ING_SABOR,
					ID_ING_AZUCAR = @ID_ING_AZUCAR,
					ID_ING_TOPPING = @ID_ING_TOPPING,
					ID_ING_BORDEADO = @ID_ING_BORDEADO,
					ID_ING_BUBBLES = @ID_ING_BUBBLES,
					DSC_NOMBRE = @DSC_NOMBRE,
					DSC_TAMANO = @DSC_TAMANO,
					FECHA = GETUTCDATE(),
					ESTADO = 1
				WHERE ID_RECETA = @ID_RECETA

        END 
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
        
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_SubCategoria_Producto]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Modificar_SubCategoria_Producto]
	@Id_SubCategoria_Producto int,
	@Id_Categoria_Producto int,
    @Dsc_Nombre_SubCategoria nvarchar(100),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY

		IF NOT EXISTS (SELECT ID_SUBCATE_PRODUCTO FROM TB_SUBCATEGORIA_PRODUCTO WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La subcategoría del producto especificada no existe.';
        END
		ELSE IF NOT EXISTS (SELECT ID_CATE_PRODUCTO FROM TB_CATE_PRODUCTO WHERE ID_CATE_PRODUCTO = @Id_Categoria_Producto)
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'La categoría del producto especificada no existe.';
        END
		ELSE IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_SubCategoria) > 0 OR LEN(@Dsc_Nombre_SubCategoria) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 1;
            SET @ERRORDESCRIPCION = 'El nombre de la subcategoría contiene caracteres especiales o está vacío.';
        END
		ELSE
		BEGIN
			-- Actualizar la subcategoría de producto
			UPDATE TB_SUBCATEGORIA_PRODUCTO
			SET ID_CATE_PRODUCTO_ID = @Id_Categoria_Producto,
				DSC_NOMBRE_SUBCATEGORIA = @Dsc_Nombre_SubCategoria
			WHERE ID_SUBCATE_PRODUCTO = @Id_SubCategoria_Producto;

			SET @IDRETURN = @Id_SubCategoria_Producto;

        END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE();
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Modificar_Usuario]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
    @Dsc_Telefono nvarchar(50),
	@IDRETURN int output,
	@ERRORID int output,
	@ERRORDESCRIPCION nvarchar(max) output
AS
BEGIN
    BEGIN TRY
        IF EXISTS (SELECT DSC_CORREO, ID_USUARIO FROM TB_USUARIO WHERE DSC_CORREO = @Dsc_Correo AND ID_USUARIO <> @Id_Usuario)
			BEGIN
				SET @IDRETURN = -1;
				SET @ERRORID = 1;
				SET @ERRORDESCRIPCION = 'Correo ya registrado en otro usuario';
			END
			ELSE
				IF NOT EXISTS (SELECT ID_USUARIO FROM TB_USUARIO WHERE ID_USUARIO = @Id_Usuario)
					BEGIN
						SET @IDRETURN = -1;
						SET @ERRORID = 2;
						SET @ERRORDESCRIPCION = 'El Usuario seleccionado no existe';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Nombre) > 0 OR LEN(@Dsc_Nombre) = 0
				BEGIN
				SET @IDRETURN = -1;
					SET @ERRORID = 3;
					SET @ERRORDESCRIPCION = 'El nombre contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Primer_Apellido) > 0 OR LEN(@Dsc_Primer_Apellido) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 4;
					SET @ERRORDESCRIPCION = 'El primer apellido contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF PATINDEX('%[^a-zA-Z ]%', @Dsc_Segundo_Apellido) > 0 OR LEN(@Dsc_Segundo_Apellido) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 5;
					SET @ERRORDESCRIPCION = 'El segundo apellido contiene caracteres especiales o está vacío.';
				END
			ELSE
				IF LEN(@Dsc_Password) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 6;
					SET @ERRORDESCRIPCION = 'La contraseña está vacía.';
				END
			ELSE
			IF LEN(@Dsc_Telefono) = 0
				BEGIN
					SET @IDRETURN = -1;
					SET @ERRORID = 7;
					SET @ERRORDESCRIPCION = 'El telefono está vacío.';
				END
			ELSE
				BEGIN
					BEGIN TRANSACTION
						UPDATE TB_USUARIO SET
						DSC_NOMBRE = @Dsc_Nombre,
						DSC_PRIMER_APELLIDO = @Dsc_Primer_Apellido,
						DSC_SEGUNDO_APELLIDO = @Dsc_Segundo_Apellido,
						DSC_CORREO = @Dsc_Correo,
						DSC_PASSWORD = @Dsc_Password,
						FEC_REGISTRO = GETUTCDATE(),
						DSC_TELEFONO = @Dsc_Telefono,
						ESTADO = 1
						WHERE ID_USUARIO = @Id_Usuario

						set @idReturn = scope_identity();
				COMMIT TRANSACTION
			END
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();

		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Cate_Ingredientes_Activos]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Cate_Productos_Activos]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Facturas_Completadas]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Facturas_No_Pagadas]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Facturas_No_Preparadas]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Ingredientes_Activos]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Productos_Activos]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Receta]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fernanda
-- Create date: 14-04-2024
-- Description:	Obtener Receta
-- =============================================
CREATE PROCEDURE [dbo].[Obtener_Receta]
	


AS
BEGIN 
	SELECT * FROM TB_RECETA Re
	INNER JOIN TB_PRODUCTO AS Pro ON Re.ID_PRODUCTO = Pro.ID_PRODUCTO
	INNER JOIN TB_INGREDIENTE AS ING_Lac ON Re.ID_ING_LACTEO = ING_LAC.ID_INGREDIENTE
	INNER JOIN TB_INGREDIENTE AS ING_Sab ON Re.ID_ING_SABOR = ING_Sab.ID_INGREDIENTE
	INNER JOIN TB_INGREDIENTE AS ING_Azu ON Re.ID_ING_AZUCAR = ING_Azu.ID_INGREDIENTE
	INNER JOIN TB_INGREDIENTE AS ING_Topp ON Re.ID_ING_TOPPING = ING_Topp.ID_INGREDIENTE
	INNER JOIN TB_INGREDIENTE AS ING_Borde ON Re.ID_ING_BORDEADO = ING_Borde.ID_INGREDIENTE
	INNER JOIN TB_INGREDIENTE AS ING_Bubb ON Re.ID_ING_BUBBLES = ING_Bubb.ID_INGREDIENTE
	WHERE Re.ESTADO = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Roles]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Obtener_Roles]
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
	BEGIN TRY
		SELECT R.ID_ROL, R.DSC_TIPO_ROL, R.DSC_PERMISOS, R.ESTADO
		FROM TB_ROL AS R
		WHERE ESTADO <> 0;
	END TRY
    BEGIN CATCH
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Roles_Por_Id_Usuario]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Sesion]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[Obtener_Sesion]
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
/****** Object:  StoredProcedure [dbo].[Obtener_Sesion_Activa_By_IdUsuario]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_SubCate_Productos_Activos]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_SubCate_Productos_Por_Cate]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Usuario]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
/****** Object:  StoredProcedure [dbo].[Obtener_Usuarios_Activos]    Script Date: 21/04/2024 10:31:56 p. m. ******/
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
	@ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
   BEGIN TRY
		SELECT U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_CORREO, U.DSC_PASSWORD,
		U.DSC_TELEFONO, RO.DSC_TIPO_ROL, RO.DSC_PERMISOS FROM TB_USUARIO AS U
		INNER JOIN TB_R_ROL_USUARIO R ON U.ID_USUARIO = R.ID_USUARIO
		INNER JOIN TB_ROL RO ON RO.ID_ROL = R.ID_ROL
		WHERE U.ESTADO <> 0 AND RO.ESTADO <> 0;
   END TRY
   BEGIN CATCH
		SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 

			select ERROR_SEVERITY(),
					ERROR_PROCEDURE(),
					ERROR_NUMBER(),
					ERROR_MESSAGE(),
					ERROR_LINE(),
					GETUTCDATE()
   END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Solicitar_Login]    Script Date: 21/04/2024 10:31:56 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Solicitar_Login]
    @Dsc_Correo nvarchar(50),
    @IDRETURN int OUTPUT,
    @ERRORID int OUTPUT,
    @ERRORDESCRIPCION nvarchar(max) OUTPUT
AS
BEGIN
    BEGIN TRY
	IF PATINDEX('%[^a-zA-Z0-9@. ]%', @Dsc_Correo) > 0 OR LEN(@Dsc_Correo) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 7;
            SET @ERRORDESCRIPCION = 'El Correo contiene caracteres especiales o está vacío.';
        END
	ElSE
		BEGIN
			SELECT U.ID_USUARIO, U.DSC_NOMBRE, U.DSC_PRIMER_APELLIDO, U.DSC_SEGUNDO_APELLIDO, U.DSC_CORREO, U.DSC_PASSWORD, U.FEC_REGISTRO, U.DSC_TELEFONO, U.ESTADO
			FROM TB_USUARIO AS U
			WHERE ESTADO <> 0 AND U.DSC_CORREO = @Dsc_Correo;
		END
    END TRY
    BEGIN CATCH
        SET @IDRETURN = -1;
        SET @ERRORID = ERROR_NUMBER();
        SET @ERRORDESCRIPCION = ERROR_MESSAGE();
        --Bitacorear error en BD.
		INSERT INTO TB_ERROR_EN_BASE_DATOS 
			(
				NUM_SEVERIVDAD,
				STORE_PROCEDURE,
				NUM_ERROR,
				DSC_DESCRIPCION,
				NUM_LINEA,
				FEC_ERROR
			) 
			SELECT ERROR_SEVERITY(),
				   ERROR_PROCEDURE(),
				   ERROR_NUMBER(),
				   ERROR_MESSAGE(),
				   ERROR_LINE(),
				   GETUTCDATE(); 
    END CATCH
END
GO
