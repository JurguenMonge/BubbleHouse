-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Insertar_Producto
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
            SET @ERRORDESCRIPCION = 'La subcategor�a especificada no existe.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Nombre_Producto) > 0 OR LEN(@Dsc_Nombre_Producto) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 2;
            SET @ERRORDESCRIPCION = 'El nombre del producto contiene caracteres especiales o est� vac�o.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9 ]%', @Dsc_Descripcion) > 0 OR LEN(@Dsc_Descripcion) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 3;
            SET @ERRORDESCRIPCION = 'La descripci�n del producto contiene caracteres especiales o est� vac�a.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF PATINDEX('%[^a-zA-Z0-9:/. ]%', @Dsc_Url_Imagen) > 0 OR LEN(@Dsc_Url_Imagen) = 0
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 4;
            SET @ERRORDESCRIPCION = 'La URL de la imagen contiene caracteres no v�lidos o est� vac�a.';
            RETURN; -- Salir del procedimiento almacenado
        END

        IF ISNUMERIC(@Num_Precio) <> 1
        BEGIN
            SET @IDRETURN = -1;
            SET @ERRORID = 5;
            SET @ERRORDESCRIPCION = 'El precio del producto debe ser un valor num�rico.';
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
        -- Aqu� puedes manejar el error como prefieras, por ejemplo, lanzar una excepci�n o registrar el error en una tabla de registro de errores.
    END CATCH
END
GO

