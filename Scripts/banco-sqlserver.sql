@'
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    UsuarioLogin NVARCHAR(50) NOT NULL,
    Senha NVARCHAR(100) NOT NULL
);

CREATE TABLE Enderecos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Cep NVARCHAR(20) NOT NULL,
    Logradouro NVARCHAR(150) NOT NULL,
    Complemento NVARCHAR(100) NULL,
    Bairro NVARCHAR(100) NOT NULL,
    Cidade NVARCHAR(100) NOT NULL,
    Uf NVARCHAR(2) NOT NULL,
    Numero NVARCHAR(20) NOT NULL,
    UsuarioId INT NOT NULL,

    CONSTRAINT FK_Enderecos_Usuarios
        FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

INSERT INTO Usuarios (Nome, UsuarioLogin, Senha)
VALUES ('Administrador', 'admin', '123456');
'@ | Set-Content -Path Scripts\banco-sqlserver.sql -Encoding UTF8