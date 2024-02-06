CREATE DATABASE BD_UPCH;
USE BD_UPCH;

CREATE TABLE TipoDocumento(
	IdTipoDocumento int primary key IDENTITY(1,1) NOT NULL,
	Descripcion varchar(100) NOT NULL,
	Estado bit default 1,
	FechaRegistro datetime default getdate()
)
GO

insert into TipoDocumento(Descripcion) values ('DNI')
GO
insert into TipoDocumento(Descripcion) values ('PASAPORTE')
GO

CREATE PROCEDURE GetTipoDocumentos
AS
BEGIN 
	SELECT IdTipoDocumento, Descripcion FROM TipoDocumento
	WHERE ESTADO = 1
END

GO

exec GetTipoDocumentos
GO

CREATE PROCEDURE GetTipoDocumento(
@IdTipoDocumento INT)
AS
BEGIN 
	SELECT IdTipoDocumento, Descripcion FROM TipoDocumento
	WHERE ESTADO = 1 AND IdTipoDocumento = @IdTipoDocumento
END

GO

CREATE PROCEDURE RegistrarTipoDocumento(
    @Descripcion varchar(100),
	@IdProceso INT OUTPUT 
)
AS
BEGIN
    
    IF EXISTS (SELECT 1 FROM TipoDocumento WHERE Descripcion = ltrim(rtrim(@Descripcion)))
    BEGIN       
		SET @IdProceso = 0;
        RETURN;
    END
   
    INSERT INTO TipoDocumento (Descripcion)
    VALUES (@Descripcion);
   
    SET @IdProceso = SCOPE_IDENTITY();
   
END

GO

CREATE PROCEDURE ActualizarTipoDocumento
    @IdTipoDocumento INT,
    @Descripcion varchar(100),    
	@IdProceso INT OUTPUT 
AS
BEGIN
   
    IF NOT EXISTS (SELECT 1 FROM TipoDocumento WHERE IdTipoDocumento = @IdTipoDocumento)
    BEGIN 
		SET @IdProceso = 0;
        RETURN @IdProceso; 
    END

    
    UPDATE TipoDocumento
    SET Descripcion = @Descripcion
    WHERE IdTipoDocumento = @IdTipoDocumento;

	SET @IdProceso = 1;
    RETURN @IdProceso; 
END

GO

CREATE PROCEDURE EliminarTipoDocumento
    @IdTipoDocumento INT,   
	@IdProceso INT OUTPUT 
AS
BEGIN
    
    IF NOT EXISTS (SELECT 1 FROM TipoDocumento WHERE IdTipoDocumento = @IdTipoDocumento)
    BEGIN 
		SET @IdProceso = 0;
        RETURN @IdProceso; 
    END

    
    delete from TipoDocumento where IdTipoDocumento = @IdTipoDocumento;

	SET @IdProceso = 1;
    RETURN @IdProceso; 
END

GO

CREATE TABLE Persona(
	IdPersona int primary key IDENTITY(1,1) NOT NULL,
	IdTipoDocumento int NOT NULL,
	NumeroDocumento varchar(20) NOT NULL,
	Nombres varchar(100) NOT NULL,
	ApellidoPaterno varchar(100) NOT NULL,
	ApellidoMaterno varchar(100) NOT NULL,
	Telefono varchar(11) NOT NULL,
	Correo varchar(100) NOT NULL,
	Direccion varchar(100) NOT NULL,	
	Estado bit default 1,
	FechaRegistro datetime default getdate(),
	FOREIGN KEY (IdTipoDocumento) REFERENCES TipoDocumento(IdTipoDocumento)
) 
GO


CREATE INDEX INDX_Persona_NumeroDocumento ON Persona(NumeroDocumento)
GO

CREATE PROCEDURE GetPersonas
AS
BEGIN 
	select p.IdPersona,p.IdTipoDocumento,t.Descripcion,p.NumeroDocumento,p.Nombres,
	p.ApellidoPaterno,p.ApellidoMaterno,p.Telefono,p.Correo,p.Direccion from Persona p 
	inner join TipoDocumento t
	on p.IdTipoDocumento = t.IdTipoDocumento
	where p.Estado = 1
END

GO

CREATE PROCEDURE GetPersona(
@IdPersona INT)
AS
BEGIN 
	select p.IdPersona,p.IdTipoDocumento,t.Descripcion,p.NumeroDocumento,p.Nombres,
	p.ApellidoPaterno,p.ApellidoMaterno,p.Telefono,p.Correo,p.Direccion from Persona p 
	inner join TipoDocumento t
	on p.IdTipoDocumento = t.IdTipoDocumento
	where p.Estado = 1 and IdPersona = @IdPersona
END

GO

CREATE PROCEDURE RegistrarPersona(
    @IdTipoDocumento INT,
	@NumeroDocumento varchar(20),
	@Nombres varchar(100),
	@ApellidoPaterno varchar(100),
	@ApellidoMaterno varchar(100),
	@Telefono varchar(11),
	@Correo varchar(100),
	@Direccion varchar(100),
	@IdProceso INT OUTPUT 
)
AS
BEGIN
    
    IF EXISTS (SELECT 1 FROM Persona WHERE NumeroDocumento = ltrim(rtrim(@NumeroDocumento)))
    BEGIN       
		SET @IdProceso = 0;
        RETURN;
    END
   
    INSERT INTO Persona(IdTipoDocumento,NumeroDocumento,Nombres,ApellidoPaterno,ApellidoMaterno,Telefono,Correo,Direccion)
    VALUES (@IdTipoDocumento,@NumeroDocumento,@Nombres,@ApellidoPaterno,@ApellidoMaterno,@Telefono,@Correo,@Direccion);
   
    SET @IdProceso = SCOPE_IDENTITY();
   
END

GO

CREATE PROCEDURE ActualizarPersona
    @IdPersona int,
	@IdTipoDocumento INT,
	@NumeroDocumento varchar(20),
	@Nombres varchar(100),
	@ApellidoPaterno varchar(100),
	@ApellidoMaterno varchar(100),
	@Telefono varchar(11),
	@Correo varchar(100),
	@Direccion varchar(100),
	@IdProceso INT OUTPUT 
AS
BEGIN
   
    IF NOT EXISTS (SELECT 1 FROM Persona WHERE IdPersona = @IdPersona)
    BEGIN 
		SET @IdProceso = 0;
        RETURN @IdProceso; 
    END
	ELSE IF NOT EXISTS (SELECT 1 FROM TipoDocumento WHERE IdTipoDocumento = @IdTipoDocumento)
	BEGIN
		SET @IdProceso = -1;
        RETURN @IdProceso; 
	END
	ELSE IF EXISTS (SELECT 1 FROM Persona WHERE IdPersona != @IdPersona and NumeroDocumento = @NumeroDocumento)
	BEGIN
		SET @IdProceso = -2;
        RETURN @IdProceso; 
	END	 
	    
    UPDATE Persona
    SET IdTipoDocumento = @IdTipoDocumento,
	NumeroDocumento = @NumeroDocumento,
	Nombres = @Nombres,
	ApellidoPaterno = @ApellidoPaterno,
	ApellidoMaterno = @ApellidoMaterno,
	Telefono = @Telefono,
	Correo = @Correo,
	Direccion = @Direccion
    WHERE IdPersona = @IdPersona;

	SET @IdProceso = 1;
    RETURN @IdProceso; 
END

GO

CREATE PROCEDURE EliminarPersona
    @IdPersona INT,   
	@IdProceso INT OUTPUT 
AS
BEGIN
    
    IF NOT EXISTS (SELECT 1 FROM Persona WHERE IdPersona = @IdPersona)
    BEGIN 
		SET @IdProceso = 0;
        RETURN @IdProceso; 
    END

    
    delete from Persona where IdPersona = @IdPersona;

	SET @IdProceso = 1;
    RETURN @IdProceso; 
END

GO