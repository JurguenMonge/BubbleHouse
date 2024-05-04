USE [BDBubbleHouse]
GO
/****** Object:  StoredProcedure [dbo].[Obtener_Ingredientes_Activos]    Script Date: 04/05/2024 05:03:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[Obtener_Ingrediente_ById]
	@id_ingrediente int
AS
BEGIN
    -- Seleccionar todos los productos cuyo estado sea diferente de 0
    SELECT I.ID_INGREDIENTE, I.DSC_NOMBRE_INGREDIENTE, I.DSC_DESCRIPCION, I.DSC_URL_IMAGEN, I.NUM_PRECIO, CI.ID_CATE_INGREDIENTE, CI.DSC_NOMBRE_CATEGORIA
    FROM TB_INGREDIENTE AS I
    INNER JOIN TB_CATE_INGREDIENTE AS CI ON CI.ID_CATE_INGREDIENTE = I.ID_CATE_INGREDIENTE
    WHERE I.ESTADO <> 0 and I.ID_INGREDIENTE = @id_ingrediente;
END
