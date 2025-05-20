CREATE DATABASE AppGestionMuni;

USE AppGestionMuni;
-- DROP DATABASE AppGestionMuni;
CREATE TABLE ROL
(
    IdRol     INT         NOT NULL AUTO_INCREMENT,
    NombreRol VARCHAR(20) NOT NULL,
    PRIMARY KEY (IdRol)
);

CREATE TABLE USUARIO
(
    IdUsuario            INT          NOT NULL AUTO_INCREMENT,
    IdRol                INT          NOT NULL,
    NombreUsuario        VARCHAR(8)   NOT NULL UNIQUE,
    Contrasena           VARCHAR(255) NOT NULL,
    EstadoUsuario        BOOLEAN      NOT NULL DEFAULT TRUE,
    FechaCreacionUsuario DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdRol) REFERENCES ROL (IdRol),
    PRIMARY KEY (IdUsuario)
);

CREATE TABLE RESIDENTE
(
    IdResidente            INT         NOT NULL AUTO_INCREMENT,
    IdUsuario              INT         NOT NULL UNIQUE,
    NombreResidente        VARCHAR(20) NOT NULL,
    ApellidoResidente      VARCHAR(30) NOT NULL,
    DniResidente           VARCHAR(8)  NOT NULL,
    CorreoResidente        VARCHAR(25),
    DireccionResidente     VARCHAR(30) NOT NULL,
    EstadoResidente        BOOL        NOT NULL DEFAULT TRUE,
    FechaCreacionResidente DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TicketsTotalesGanados  INT         NOT NULL,
    FOREIGN KEY (IdUsuario) REFERENCES USUARIO (IdUsuario),
    PRIMARY KEY (IdResidente)
);


CREATE TABLE TICKET
(
    IdTicket            INT         NOT NULL,
    ColorTicket         VARCHAR(15) NOT NULL,
    EstadoTicket        BOOL        NOT NULL DEFAULT TRUE,
    FechaCreacionTicket DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdTicket)
);


CREATE TABLE CATEGORIADERESIDUO
(
    IdCategoriaResiduo            INT         NOT NULL AUTO_INCREMENT,
    IdTicket                      INT         NOT NULL,
    NombreCategoria               VARCHAR(15) NOT NULL,-- ORGANICO,
    EstadoCategoriaResiduo        BOOL        NOT NULL DEFAULT TRUE,
    FechaCreacionCategoriaResiduo DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdCategoriaResiduo),
    FOREIGN KEY (IdTicket) REFERENCES TICKET (IdTicket)
);



CREATE TABLE RESIDUO
(
    IdResiduo          INT         NOT NULL AUTO_INCREMENT,
    IdCategoriaResiduo INT         NOT NULL,
    NombreResiduo      VARCHAR(15) NOT NULL,-- PAPEL,PLASTICO,ETC
    EstadoResiduo      BOOL        NOT NULL DEFAULT TRUE,
    PRIMARY KEY (IdResiduo),
    FOREIGN KEY (IdCategoriaResiduo) REFERENCES CATEGORIADERESIDUO (IdCategoriaResiduo)
);


CREATE TABLE PREMIO
(
    IdPremio          INT         NOT NULL AUTO_INCREMENT,
    NombrePremio      VARCHAR(30) NOT NULL,
    DescripcionPremio VARCHAR(50) NOT NULL,
    PuntosRequeridos  INT         NOT NULL,
    EstadoPremio      BOOL        NOT NULL DEFAULT TRUE,
    PRIMARY KEY (IdPremio)
);

CREATE TABLE CANJE
(
    IdCanje       INT      NOT NULL AUTO_INCREMENT,
    IdResidente      INT      NOT NULL,
    IdPremio      INT      NOT NULL,
    FechaDeCanjeo DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EstadoCanje   BOOL     NOT NULL DEFAULT TRUE,
    PRIMARY KEY (IdCanje),
    FOREIGN KEY (IdResidente) REFERENCES RESIDENTE (IdResidente),
    FOREIGN KEY (IdPremio) REFERENCES PREMIO (IdPremio)
);


CREATE TABLE REGISTROSDERECICLAJE
(
    IdRegistrosReciclaje INT      NOT NULL AUTO_INCREMENT,
    IdResidente             INT      NOT NULL,
    IdResiduo            INT      NOT NULL,
    PesoKilogramo        DECIMAL(5,2)    NOT NULL,
    FechaRegistro        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    TicketsGanados       INT      NOT NULL,
    PRIMARY KEY (IdRegistrosReciclaje),
    FOREIGN KEY (IdResidente) REFERENCES RESIDENTE (IdResidente),
    FOREIGN KEY (IdResiduo) REFERENCES RESIDUO (IdResiduo)
);

CREATE TABLE CONDUCTOR
(
    IdConductor            INT         NOT NULL AUTO_INCREMENT,
    NombreConductor        VARCHAR(30) NOT NULL,
    ApellidoConductor      VARCHAR(30) NOT NULL,
    TelefonoConductor      CHAR(9)     NOT NULL,
    DniConductor           VARCHAR(8)  NOT NULL,
    EstadoConductor        BOOLEAN     NOT NULL DEFAULT TRUE,
    FechaCreacionConductor DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdConductor)
);

CREATE TABLE COMPACTADORA
(
    IdCompactadora            INT AUTO_INCREMENT,
    IdConductor               INT         NOT NULL,
    PlacaCompactadora         VARCHAR(20) NOT NULL UNIQUE,
    MarcaCompactadora         VARCHAR(50),
    ModeloCompactadora        VARCHAR(50),
    CapacidadCompactadora     DECIMAL(10, 2),
    EstadoCompactadora        BOOL        NOT NULL DEFAULT TRUE,
    FechaCreacionCompactadora DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdCompactadora),
    FOREIGN KEY (IdConductor) REFERENCES CONDUCTOR (IdConductor)
);


CREATE TABLE RUTA
(
    IdRuta            INT         NOT NULL AUTO_INCREMENT,
    OrigenRuta        VARCHAR(20) NOT NULL,
    DestinoRuta       VARCHAR(20) NOT NULL,
    DuracionRuta      TIME        NOT NULL,
    EstadoRuta        BOOLEAN     NOT NULL DEFAULT TRUE,
    FechaCreacionRuta DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdRuta)
);

CREATE TABLE HORARIO
(
    IdHorario            INT      NOT NULL AUTO_INCREMENT,
    IdCompactadora       INT      NOT NULL,
    IdRuta               INT      NOT NULL,
    FechaSalida          DATE     NOT NULL,
    HoraSalida           TIME     NOT NULL,
    EstadoHorario        BOOLEAN  NOT NULL DEFAULT TRUE,
    FechaCreacionHorario DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdHorario),
    FOREIGN KEY (IdCompactadora) REFERENCES COMPACTADORA (IdCompactadora),
    FOREIGN KEY (IdRuta) REFERENCES RUTA (IdRuta)
);



CREATE TABLE RECOLECCIONDERESIDUOS
(
    IdRecoleccion        INT      NOT NULL AUTO_INCREMENT,
    IdRegistrosReciclaje INT      NOT NULL,
    IdHorario            INT      NOT NULL,
    FechaRecoleccion     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (IdRecoleccion),
    FOREIGN KEY (IdRegistrosReciclaje) REFERENCES REGISTROSDERECICLAJE (IdRegistrosReciclaje),
    FOREIGN KEY (IdHorario) REFERENCES HORARIO (IdHorario)
);

INSERT INTO ROL (NombreRol)
VALUES ('administrador'),
       ('recolector'),
       ('vecinos'),
       ('conductor');



SELECT *FROM  USUARIO;





