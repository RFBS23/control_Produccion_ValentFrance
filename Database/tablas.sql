create database ctrlprodvalentfrance
go

use ctrlprodvalentfrance;
go

create table nivelesaccesos(
	idnivelacceso int primary key identity,
	nombre varchar(50) not null unique,
	fecharegistro datetime default getdate()
)
insert into nivelesaccesos (nombre) values
	('administrador')
go

create table permisos
(
	idpermiso int primary key identity,
	idnivelacceso int references nivelesaccesos(idnivelacceso),
	nombremenu varchar(100),
	fecharegistro datetime default getdate()
)
insert into permisos (idrol,nombremenu) values
	(1, 'menuusuarios')
go

create table proveedores (
	idproveedor int primary key identity,
	nombre varchar(255) not null,
	documento varchar(50) not null unique,
	direccion varchar(100) null,
	correo varchar(50) not null,
	telefono varchar(9) not null unique,
	fecharegistro date default getdate()
)
go

create table usuarios
(
	idusuario int primary key identity,
	documento varchar(50) unique not null,
	nombres varchar(50) null,
	apellidos varchar(50) null,
	nombreusuario varchar(150) not null,
	correo varchar(150) not null,
	clave varchar(200) not null,
	idnivelacceso int references nivelesaccesos(idnivelacceso),
	estado bit not null default 1,
	fecharegistro datetime default getdate()
)
go
insert into usuarios (documento, nombres, apellidos, nombreusuario, correo, clave, idnivelacceso) values
('71447422', 'Rodrigo', 'barrios', 'fabrizio', 'fabrizio@gmail.com', '12345', 1)
go

select u.idusuario, u.documento, u.nombres, u.apellidos, u.nombreusuario, u.correo, u.clave, u.estado, nv.idnivelacceso, nv.nombre from usuarios u
inner join nivelesaccesos nv on nv.idnivelacceso = u.idnivelacceso
go

create table categorias(
	idcategoria int primary key identity,
	nombrecategoria varchar(100) not null,
	estado bit not null default 1,
	fecharegistro datetime default getdate()
)
go

/*por ejecutar*/
create table telas(
	idtela int primary key identity,
	np varchar(60) not null,
	nombretela varchar(150) not null,
	pp varchar(50) not null,
	tipocorte varchar(60) not null
)
go

create table productos(
	idproducto int primary key identity,
	np varchar(100) not null,
	nombreproducto varchar(200) not null,
	descripcion varchar(100) not null,
	idcategoria int references categorias(idcategoria) not null,
	colores varchar(40) not null,
	preciocompra decimal(10, 2) default 0 not null,
	precioventa decimal(10, 2) default 0 not null,
	estado bit not null default 1,
	fecharegistro datetime default getdate()
)
go

create table compras(
	idcompra int primary key identity,
	idusuario int references usuarios (idusuario),
	idproveedor int references proveedores (idproveedor),
	tipodocumento varchar(50),
	numerodocumento varchar(50),
	montototal decimal(10,2),
	fecharegistro datetime default getdate()
)
go

create table detallescompras(
	iddetallecompra int primary key identity,
	idcompra int references compras (idcompra),
	idproducto int references productos (idproducto),
	preciocompra decimal(10,2) default 0,
	precioventa decimal(10,2) default 0,
	cantidad int,
	montototal decimal(10,2),
	fecharegistro datetime default getdate()
)
go

create table ventas(
	idventa int primary key identity,
	idusuario int references usuarios (idusuario),
	tipodocumento varchar(50),
	numerodocumento varchar(50),
	documentocliente varchar(50),
	nombrecliente varchar(100),
	montopago decimal(10,2),
	montocambio decimal(10,2),
	montototal decimal(10,2),
	fecharegistro datetime default getdate()
)
go

create table detallesventas(
	iddetalleventa int primary key identity,
	idventa int references ventas(idventa),
	idproducto int references productos (idproducto),
	precioventa decimal(10,2),
	cantidad int,
	subtotal decimal(10,2),
	fecharegistro datetime default getdate()
)
go

create table negocios(
	idnegocio int primary key,
	ruc varchar(20) not null unique,
	nombre varchar(60) null,
	direccion varchar(100) not null,
	distrito varchar(100) not null,
	provincia varchar(100) not null,
	departamento varchar(100) not null,
	logo varbinary (max) null
)
go
insert into negocios(idnegocio, ruc, nombre, direccion, distrito, provincia, departamento) values
(1, '20609430517', 'valent france', 'Calle 02 Mz. a Lote 19 Urb. los Productores', 'santa anita', 'lima', 'lima');
go
select logo from negocios where idnegocio = 1
select idnegocio, ruc, nombre, direccion, distrito, provincia, departamento logo from negocios where idnegocio = 1
go

