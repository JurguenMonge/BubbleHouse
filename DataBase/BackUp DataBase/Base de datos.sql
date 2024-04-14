USE BDBubbleHouse;

CREATE TABLE TB_ROL(
	ID_ROL INT PRIMARY KEY IDENTITY(1,1),
	DSC_TIPO_ROL NVARCHAR(50),
	DSC_PERMISOS NVARCHAR(MAX),
	ESTADO TINYINT
)

CREATE TABLE TB_USUARIO(
	ID_USUARIO INT PRIMARY KEY IDENTITY(1,1),
	DSC_NOMBRE NVARCHAR(50),
	DSC_PRIMER_APELLIDO NVARCHAR(50),
	DSC_SEGUNDO_APELLIDO NVARCHAR(50),
	DSC_CORREO NVARCHAR(50),
	DSC_PASSWORD NVARCHAR(MAX),
	FEC_REGISTRO DATETIME,
	DSC_TELEFONO NVARCHAR(50),
	ESTADO TINYINT
)

CREATE TABLE TB_R_ROL_USUARIO(
	ID_R_ROL_USUARIO INT PRIMARY KEY IDENTITY(1,1),
	ID_ROL INT, 
	FOREIGN KEY (ID_ROL) REFERENCES TB_ROL(ID_ROL),
	ID_USUARIO INT,
	FOREIGN KEY (ID_USUARIO) REFERENCES TB_USUARIO(ID_USUARIO),
)


CREATE TABLE TB_SESION(
	ID_SESION NVARCHAR(100) PRIMARY KEY,
	ID_USUARIO INT,
	FOREIGN KEY (ID_USUARIO) REFERENCES TB_USUARIO(ID_USUARIO),
	DSC_SESION NVARCHAR(MAX),
	DSC_ORIGEN NVARCHAR(MAX),
	DSC_CIERRE NVARCHAR(MAX),
	FEC_INICIO DATETIME,
	FEC_CIERRE DATETIME, 
	ESTADO TINYINT
)

CREATE TABLE TB_CATE_PRODUCTO(
	ID_CATE_PRODUCTO INT PRIMARY KEY IDENTITY(1,1),
	DSC_NOMBRE_CATEGORIA NVARCHAR(100),
	ESTADO TINYINT
)

CREATE TABLE TB_SUBCATEGORIA_PRODUCTO(
	ID_SUBCATE_PRODUCTO INT PRIMARY KEY IDENTITY(1,1),
	ID_CATE_PRODUCTO_ID INT,
	FOREIGN KEY (ID_CATE_PRODUCTO_ID) REFERENCES TB_CATE_PRODUCTO(ID_CATE_PRODUCTO), 
	DSC_NOMBRE_SUBCATEGORIA NVARCHAR(100),
	ESTADO TINYINT
)

CREATE TABLE TB_PRODUCTO(
	ID_PRODUCTO INT PRIMARY KEY IDENTITY(1,1),
	ID_SUBCATE_PRODUCTO INT,
	FOREIGN KEY (ID_SUBCATE_PRODUCTO) REFERENCES TB_SUBCATEGORIA_PRODUCTO(ID_SUBCATE_PRODUCTO),
	DSC_NOMBRE_PRODUCTO NVARCHAR(100),
	DSC_DESCRIPCION NVARCHAR(MAX),
	DSC_URL_IMAGEN NVARCHAR(MAX),
	NUM_PRECIO DECIMAL(10,2),
	ESTADO TINYINT
)

CREATE TABLE TB_FACTURA(
  ID_FACTURA INT PRIMARY KEY IDENTITY(1,1),
  ID_PRODUCTO INT,
  ID_SESION NVARCHAR(100),
  NUM_SUBTOTAL DECIMAL(10,2),
  NUM_DESCUENTO DECIMAL(10,2),
  NUM_TOTAL DECIMAL(10,2),
  FECHA DATETIME,
  ESTADO TINYINT
)

CREATE TABLE TB_CATE_INGREDIENTE (
  ID_CATE_INGREDIENTE INT PRIMARY KEY IDENTITY(1,1),
  DSC_NOMBRE_CATEGORIA NVARCHAR(MAX),
  ESTADO TINYINT
)

CREATE TABLE TB_INGREDIENTE (
  ID_INGREDIENTE INT PRIMARY KEY IDENTITY(1,1),
  ID_CATE_INGREDIENTE INT,
  FOREIGN KEY (ID_CATE_INGREDIENTE) REFERENCES TB_CATE_INGREDIENTE(ID_CATE_INGREDIENTE),
  DSC_NOMBRE_INGREDIENTE NVARCHAR(100),
  DSC_DESCRIPCION NVARCHAR(MAX),
  DSC_URL_IMAGEN NVARCHAR(MAX),
  NUM_PRECIO DECIMAL(10,2),
  ESTADO TINYINT
)

CREATE TABLE TB_RECETA(
  ID_RECETA INT PRIMARY KEY IDENTITY(1,1),
  ID_PRODUCTO INT,
  FOREIGN KEY (ID_PRODUCTO) REFERENCES TB_PRODUCTO(ID_PRODUCTO),
  ID_ING_LACTEO INT,
  FOREIGN KEY (ID_ING_LACTEO) REFERENCES TB_INGREDIENTE(ID_INGREDIENTE),
  ID_ING_SABOR INT,
  FOREIGN KEY (ID_ING_SABOR) REFERENCES TB_INGREDIENTE(ID_INGREDIENTE),
  ID_ING_AZUCAR INT,
  FOREIGN KEY (ID_ING_AZUCAR) REFERENCES TB_INGREDIENTE(ID_INGREDIENTE),
  ID_ING_TOPPING INT,
  FOREIGN KEY (ID_ING_TOPPING) REFERENCES TB_INGREDIENTE(ID_INGREDIENTE),
  ID_ING_BORDEADO INT,
  FOREIGN KEY (ID_ING_BORDEADO) REFERENCES TB_INGREDIENTE(ID_INGREDIENTE),
  ID_ING_BUBBLES INT,
  FOREIGN KEY (ID_ING_BUBBLES) REFERENCES TB_INGREDIENTE(ID_INGREDIENTE),
  DSC_NOMBRE NVARCHAR(MAX),
  DSC_TAMANO NVARCHAR(50),
  FECHA DATETIME,
  ESTADO TINYINT
)

CREATE TABLE TB_ERROR_EN_BASE_DATOS(
	ID_ERROR_EN_BASE_DATOS INT PRIMARY KEY IDENTITY(1,1),
	NUM_SEVERIVDAD INT,
	STORE_PROCEDURE NVARCHAR(50),
	NUM_ERROR INT,
	DSC_DESCRIPCION NVARCHAR(MAX),
	NUM_LINEA INT,
	FEC_ERROR DATETIME
)

CREATE TABLE TB_BITACORA(
	ID_BITACORA INT PRIMARY KEY IDENTITY(1,1),
	DSC_CLASE NVARCHAR(100),
	DSC_METODO NVARCHAR(100),
	NUM_TIPO SMALLINT,
	DSC_DESCRIPCION NVARCHAR(MAX),
	DSC_REQUEST NVARCHAR(MAX),
	DSC_RESPONSE NVARCHAR(MAX),
	FEC_REGISTRO DATETIME
)

CREATE TABLE TB_RECUPERACION_PASSWORD(
	ID_RECUPERACION_PASSWORD INT PRIMARY KEY IDENTITY(1,1),
	DSC_TOKEN NVARCHAR(100),
	ID_USUARIO INT,
	FOREIGN KEY (ID_USUARIO) REFERENCES TB_USUARIO(ID_USUARIO),
	FEC_SOLICITUD DATETIME,
	FEC_USO DATETIME,
	ESTADO TINYINT
)

ALTER TABLE TB_FACTURA
ADD CONSTRAINT FK_TB_FACTURA_TB_PRODUCTO 
FOREIGN KEY (ID_PRODUCTO) 
REFERENCES TB_PRODUCTO(ID_PRODUCTO);

ALTER TABLE TB_FACTURA
ADD CONSTRAINT FK_TB_FACTURA_TB_SESION
FOREIGN KEY (ID_SESION) 
REFERENCES TB_SESION(ID_SESION);