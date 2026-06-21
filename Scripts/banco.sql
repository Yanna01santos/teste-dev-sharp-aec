CREATE TABLE Usuarios (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nome TEXT NOT NULL,
    UsuarioLogin TEXT NOT NULL,
    Senha TEXT NOT NULL
);

CREATE TABLE Enderecos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Cep TEXT NOT NULL,
    Logradouro TEXT NOT NULL,
    Complemento TEXT NULL,
    Bairro TEXT NOT NULL,
    Cidade TEXT NOT NULL,
    Uf TEXT NOT NULL,
    Numero TEXT NOT NULL,
    UsuarioId INTEGER NOT NULL,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
);

INSERT INTO Usuarios (Nome, UsuarioLogin, Senha)
VALUES ('Administrador', 'admin', '123456');