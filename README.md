# PruebaJWt

#Te dejo Aqui el script de la base de datos
reate table Usuario(
IdUsuario int PRIMARY Key IDENTITY,
Nombre varchar(50),
Correo VARCHAR(50),
Clave VARCHAR(100)
);

create table Producto(
IdProducto int PRIMARY Key IDENTITY,
Nombre VARCHAR(50),
Marca VARCHAR(50),
Precio decimal(10,2)
);

INSERT into Producto( Nombre, Marca, Precio) VALUES('Laptop gamer 1001','HP',1500)
,('Monitor Curvo HD','Apple',3590.45)
,('Iphone Pro Max','Aplle',500);

select \* from Producto;

select NEWID()

SELECT @@SERVERNAME;
GO
