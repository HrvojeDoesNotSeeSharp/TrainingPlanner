
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/30/2014 12:53:14
-- Generated from EDMX file: D:\TrainingPlanner\TrainingPlanner\TreningModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Trening];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClanTrening]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Trening] DROP CONSTRAINT [FK_ClanTrening];
GO
IF OBJECT_ID(N'[dbo].[FK_TreningZagrijavanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zagrijavanje] DROP CONSTRAINT [FK_TreningZagrijavanje];
GO
IF OBJECT_ID(N'[dbo].[FK_TreningIstezanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Istezanje] DROP CONSTRAINT [FK_TreningIstezanje];
GO
IF OBJECT_ID(N'[dbo].[FK_ClanTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Test] DROP CONSTRAINT [FK_ClanTest];
GO
IF OBJECT_ID(N'[dbo].[FK_TestSlika]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Slika] DROP CONSTRAINT [FK_TestSlika];
GO
IF OBJECT_ID(N'[dbo].[FK_ZagrijavanjeSlikeZagrijavanjePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ZagrijavanjeSlike] DROP CONSTRAINT [FK_ZagrijavanjeSlikeZagrijavanjePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_VjezbeSlikeVjezbePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VjezbeSlike] DROP CONSTRAINT [FK_VjezbeSlikeVjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_IstezanjeSlikeIstezanjePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IstezanjeSlike] DROP CONSTRAINT [FK_IstezanjeSlikeIstezanjePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_TreningSekcijaVjezbi]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SekcijaVjezbi] DROP CONSTRAINT [FK_TreningSekcijaVjezbi];
GO
IF OBJECT_ID(N'[dbo].[FK_SekcijaVjezbiVjezba]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vjezba] DROP CONSTRAINT [FK_SekcijaVjezbiVjezba];
GO
IF OBJECT_ID(N'[dbo].[FK_ZagrijavanjeZagrijavanjePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zagrijavanje] DROP CONSTRAINT [FK_ZagrijavanjeZagrijavanjePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_VjezbaVjezbePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vjezba] DROP CONSTRAINT [FK_VjezbaVjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_IstezanjeIstezanjePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Istezanje] DROP CONSTRAINT [FK_IstezanjeIstezanjePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_ClanSlikeClan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClanSlike] DROP CONSTRAINT [FK_ClanSlikeClan];
GO
IF OBJECT_ID(N'[dbo].[FK_AntropometrijaClan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Antropometrija] DROP CONSTRAINT [FK_AntropometrijaClan];
GO
IF OBJECT_ID(N'[dbo].[FK_AmnezaSlikeAmneza]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AmnezaSlike] DROP CONSTRAINT [FK_AmnezaSlikeAmneza];
GO
IF OBJECT_ID(N'[dbo].[FK_AmnezaClan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Amneza] DROP CONSTRAINT [FK_AmnezaClan];
GO
IF OBJECT_ID(N'[dbo].[FK_TestMotorickiRezultatiTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MotorickiRezultatiTest] DROP CONSTRAINT [FK_TestMotorickiRezultatiTest];
GO
IF OBJECT_ID(N'[dbo].[FK_TestFunkcionalniRezultatiTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FunkcionalniRezultatiTest] DROP CONSTRAINT [FK_TestFunkcionalniRezultatiTest];
GO
IF OBJECT_ID(N'[dbo].[FK_AerobneVjezbePopisAerobneVjezbeSlike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AerobneVjezbeSlike] DROP CONSTRAINT [FK_AerobneVjezbePopisAerobneVjezbeSlike];
GO
IF OBJECT_ID(N'[dbo].[FK_AnaerobneVjezbePopisAnaerobneVjezbeSlike]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnaerobneVjezbeSlike] DROP CONSTRAINT [FK_AnaerobneVjezbePopisAnaerobneVjezbeSlike];
GO
IF OBJECT_ID(N'[dbo].[FK_SekcijaVjezbiAerobneVjezbe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AerobneVjezbe] DROP CONSTRAINT [FK_SekcijaVjezbiAerobneVjezbe];
GO
IF OBJECT_ID(N'[dbo].[FK_AerobneVjezbeAerobneVjezbePopis]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AerobneVjezbe] DROP CONSTRAINT [FK_AerobneVjezbeAerobneVjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[FK_AnaerobneVjezbePopisAnaerobneVjezbe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnaerobneVjezbe] DROP CONSTRAINT [FK_AnaerobneVjezbePopisAnaerobneVjezbe];
GO
IF OBJECT_ID(N'[dbo].[FK_AnaerobneVjezbeSekcijaVjezbi]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnaerobneVjezbe] DROP CONSTRAINT [FK_AnaerobneVjezbeSekcijaVjezbi];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clan];
GO
IF OBJECT_ID(N'[dbo].[Test]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Test];
GO
IF OBJECT_ID(N'[dbo].[Trening]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trening];
GO
IF OBJECT_ID(N'[dbo].[IstezanjePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IstezanjePopis];
GO
IF OBJECT_ID(N'[dbo].[VjezbePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[ZagrijavanjePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZagrijavanjePopis];
GO
IF OBJECT_ID(N'[dbo].[Zagrijavanje]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zagrijavanje];
GO
IF OBJECT_ID(N'[dbo].[Vjezba]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vjezba];
GO
IF OBJECT_ID(N'[dbo].[Istezanje]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Istezanje];
GO
IF OBJECT_ID(N'[dbo].[Slika]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Slika];
GO
IF OBJECT_ID(N'[dbo].[ZagrijavanjeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZagrijavanjeSlike];
GO
IF OBJECT_ID(N'[dbo].[VjezbeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VjezbeSlike];
GO
IF OBJECT_ID(N'[dbo].[IstezanjeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IstezanjeSlike];
GO
IF OBJECT_ID(N'[dbo].[SekcijaVjezbi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SekcijaVjezbi];
GO
IF OBJECT_ID(N'[dbo].[ClanSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClanSlike];
GO
IF OBJECT_ID(N'[dbo].[Antropometrija]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Antropometrija];
GO
IF OBJECT_ID(N'[dbo].[Amneza]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Amneza];
GO
IF OBJECT_ID(N'[dbo].[AmnezaSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AmnezaSlike];
GO
IF OBJECT_ID(N'[dbo].[FunkcionalniRezultatiTest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FunkcionalniRezultatiTest];
GO
IF OBJECT_ID(N'[dbo].[MotorickiRezultatiTest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MotorickiRezultatiTest];
GO
IF OBJECT_ID(N'[dbo].[AerobneVjezbePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AerobneVjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[AnaerobneVjezbePopis]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnaerobneVjezbePopis];
GO
IF OBJECT_ID(N'[dbo].[AerobneVjezbeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AerobneVjezbeSlike];
GO
IF OBJECT_ID(N'[dbo].[AnaerobneVjezbeSlike]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnaerobneVjezbeSlike];
GO
IF OBJECT_ID(N'[dbo].[AerobneVjezbe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AerobneVjezbe];
GO
IF OBJECT_ID(N'[dbo].[AnaerobneVjezbe]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnaerobneVjezbe];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clan'
CREATE TABLE [dbo].[Clan] (
    [ClanId] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL,
    [GodinaRodenja] datetime  NOT NULL,
    [Visina] nvarchar(max)  NOT NULL,
    [Tezina] nvarchar(max)  NOT NULL,
    [GodineStarosti] smallint  NOT NULL,
    [Sport] nvarchar(max)  NULL,
    [Napomena] nvarchar(max)  NULL
);
GO

-- Creating table 'Test'
CREATE TABLE [dbo].[Test] (
    [TestId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ClanId] int  NOT NULL,
    [FunkcionalneSposobnosti] nvarchar(max)  NULL,
    [MotorickeSposobnosti] nvarchar(max)  NULL
);
GO

-- Creating table 'Trening'
CREATE TABLE [dbo].[Trening] (
    [TreningId] int IDENTITY(1,1) NOT NULL,
    [TipTreninga] nvarchar(max)  NULL,
    [DatumTreninga] datetime  NULL,
    [ImeTreninga] nvarchar(max)  NULL,
    [ClanId] int  NOT NULL,
    [Napomena] nvarchar(max)  NULL,
    [NapomenaZagrijavanje] nvarchar(max)  NULL,
    [NapomenaVjezba] nvarchar(max)  NULL,
    [NapomenaIstezanje] nvarchar(max)  NULL
);
GO

-- Creating table 'IstezanjePopis'
CREATE TABLE [dbo].[IstezanjePopis] (
    [IstezanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'VjezbePopis'
CREATE TABLE [dbo].[VjezbePopis] (
    [VjezbeId] int IDENTITY(1,1) NOT NULL,
    [ImeVjezbe] nvarchar(max)  NOT NULL,
    [Slika] varbinary(max)  NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'ZagrijavanjePopis'
CREATE TABLE [dbo].[ZagrijavanjePopis] (
    [ZagrijavanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL
);
GO

-- Creating table 'Zagrijavanje'
CREATE TABLE [dbo].[Zagrijavanje] (
    [ZagrijavanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL,
    [TreningId] int  NOT NULL,
    [Tempo] nvarchar(max)  NULL,
    [Puls] nvarchar(max)  NULL,
    [ZagrijavanjeNapomena] nvarchar(max)  NULL,
    [ZagrijavanjePopisZagrijavanjeId] int  NOT NULL,
    [Trajanje] nvarchar(max)  NULL
);
GO

-- Creating table 'Vjezba'
CREATE TABLE [dbo].[Vjezba] (
    [VjezbaId] int IDENTITY(1,1) NOT NULL,
    [ImeVjezbe] nvarchar(max)  NOT NULL,
    [Slika] varbinary(max)  NULL,
    [Info] nvarchar(max)  NULL,
    [BrojPonavljanja] nvarchar(max)  NULL,
    [BrojSerija] nvarchar(max)  NULL,
    [Kilogrami] nvarchar(max)  NULL,
    [Odmor] nvarchar(max)  NULL,
    [SekcijaId] int  NOT NULL,
    [VjezbePopisVjezbeId] int  NOT NULL
);
GO

-- Creating table 'Istezanje'
CREATE TABLE [dbo].[Istezanje] (
    [IstezanjeId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [Info] nvarchar(max)  NULL,
    [TreningId] int  NOT NULL,
    [VrijemeIzdrzaja] nvarchar(max)  NULL,
    [VrstaIstezanja] nvarchar(max)  NULL,
    [IstezanjePopisIstezanjeId] int  NOT NULL
);
GO

-- Creating table 'Slika'
CREATE TABLE [dbo].[Slika] (
    [SlikaId] int IDENTITY(1,1) NOT NULL,
    [TestTestId] int  NOT NULL,
    [SlikaIme] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ZagrijavanjeSlike'
CREATE TABLE [dbo].[ZagrijavanjeSlike] (
    [ZagrijavanjeSlikeId] int IDENTITY(1,1) NOT NULL,
    [ZagrijavanjePopisZagrijavanjeId] int  NOT NULL,
    [ZagrijavanjeSlikaIme] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VjezbeSlike'
CREATE TABLE [dbo].[VjezbeSlike] (
    [VjezbeSlikeId] int IDENTITY(1,1) NOT NULL,
    [VjezbeSlikaIme] nvarchar(max)  NOT NULL,
    [VjezbePopisVjezbeId] int  NOT NULL
);
GO

-- Creating table 'IstezanjeSlike'
CREATE TABLE [dbo].[IstezanjeSlike] (
    [IstezanjeSlikeId] int IDENTITY(1,1) NOT NULL,
    [IstezanjeSlikaIme] nvarchar(max)  NOT NULL,
    [IstezanjePopisIstezanjeId] int  NOT NULL
);
GO

-- Creating table 'SekcijaVjezbi'
CREATE TABLE [dbo].[SekcijaVjezbi] (
    [SekcijaId] int IDENTITY(1,1) NOT NULL,
    [TreningId] int  NOT NULL,
    [BrojKrugova] nvarchar(max)  NULL,
    [Odmor] nvarchar(max)  NULL,
    [PopisVjezbi] nvarchar(max)  NULL
);
GO

-- Creating table 'ClanSlike'
CREATE TABLE [dbo].[ClanSlike] (
    [ClanSlikeId] int IDENTITY(1,1) NOT NULL,
    [ClanSlikaIme] nvarchar(max)  NOT NULL,
    [ClanClanId] int  NOT NULL
);
GO

-- Creating table 'Antropometrija'
CREATE TABLE [dbo].[Antropometrija] (
    [AntropometrijaId] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NULL,
    [Visina] nvarchar(max)  NULL,
    [Tezina] nvarchar(max)  NULL,
    [PotkoznoMasnoTkivo] nvarchar(max)  NULL,
    [BezmasnaMasa] nvarchar(max)  NULL,
    [ClanClanId] int  NOT NULL
);
GO

-- Creating table 'Amneza'
CREATE TABLE [dbo].[Amneza] (
    [AmnezaId] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NULL,
    [Opis] nvarchar(max)  NULL,
    [ClanClanId] int  NOT NULL
);
GO

-- Creating table 'AmnezaSlike'
CREATE TABLE [dbo].[AmnezaSlike] (
    [SlikaId] int IDENTITY(1,1) NOT NULL,
    [SlikaIme] nvarchar(max)  NULL,
    [AmnezaAmnezaId] int  NOT NULL
);
GO

-- Creating table 'FunkcionalniRezultatiTest'
CREATE TABLE [dbo].[FunkcionalniRezultatiTest] (
    [FunkcionalniRezultatiTestId] int IDENTITY(1,1) NOT NULL,
    [Datum] datetime  NULL,
    [Rezultat] nvarchar(max)  NULL,
    [TestId] int  NOT NULL
);
GO

-- Creating table 'MotorickiRezultatiTest'
CREATE TABLE [dbo].[MotorickiRezultatiTest] (
    [MotorickiRezultatiTestId] int IDENTITY(1,1) NOT NULL,
    [TestId] int  NOT NULL,
    [Datum] datetime  NULL,
    [Rezultat] nvarchar(max)  NULL
);
GO

-- Creating table 'AerobneVjezbePopis'
CREATE TABLE [dbo].[AerobneVjezbePopis] (
    [AerobnaVjezbaId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NULL,
    [Info] nvarchar(max)  NULL,
    [Slika] varbinary(max)  NULL
);
GO

-- Creating table 'AnaerobneVjezbePopis'
CREATE TABLE [dbo].[AnaerobneVjezbePopis] (
    [AnaerobnaVjezbaId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NULL,
    [Info] nvarchar(max)  NULL,
    [Slika] varbinary(max)  NULL
);
GO

-- Creating table 'AerobneVjezbeSlike'
CREATE TABLE [dbo].[AerobneVjezbeSlike] (
    [AerobnaVjezbaSlikaId] int IDENTITY(1,1) NOT NULL,
    [AerobneVjezbePopisAerobnaVjezbaId] int  NOT NULL,
    [AerobnaVjezbaSlikaIme] nvarchar(max)  NULL
);
GO

-- Creating table 'AnaerobneVjezbeSlike'
CREATE TABLE [dbo].[AnaerobneVjezbeSlike] (
    [AnaerobnaVjezbaSlikaId] int IDENTITY(1,1) NOT NULL,
    [AnaerobneVjezbePopisAnaerobnaVjezbaId] int  NOT NULL,
    [AnaerobnaVjezbaSlikaIme] nvarchar(max)  NULL
);
GO

-- Creating table 'AerobneVjezbe'
CREATE TABLE [dbo].[AerobneVjezbe] (
    [AerobnaVjezbaId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NULL,
    [Puls] nvarchar(max)  NULL,
    [Tempo] nvarchar(max)  NULL,
    [Trajanje] nvarchar(max)  NULL,
    [Napomena] nvarchar(max)  NULL,
    [SekcijaVjezbiSekcijaId] int  NOT NULL,
    [AerobneVjezbePopisAerobnaVjezbaId] int  NOT NULL,
    [Ime] nvarchar(max)  NULL,
    [Info] nvarchar(max)  NULL,
    [Slika] varbinary(max)  NULL
);
GO

-- Creating table 'AnaerobneVjezbe'
CREATE TABLE [dbo].[AnaerobneVjezbe] (
    [AnaerobnaVjezbaId] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NULL,
    [Puls] nvarchar(max)  NULL,
    [Tempo] nvarchar(max)  NULL,
    [BrojSprintova] nvarchar(max)  NULL,
    [Odmor] nvarchar(max)  NULL,
    [Napomena] nvarchar(max)  NULL,
    [AnaerobneVjezbePopisAnaerobnaVjezbaId] int  NOT NULL,
    [SekcijaVjezbiSekcijaId] int  NOT NULL,
    [Ime] nvarchar(max)  NULL,
    [Info] nvarchar(max)  NULL,
    [Slika] varbinary(max)  NULL,
    [TrajanjeSprinta] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClanId] in table 'Clan'
ALTER TABLE [dbo].[Clan]
ADD CONSTRAINT [PK_Clan]
    PRIMARY KEY CLUSTERED ([ClanId] ASC);
GO

-- Creating primary key on [TestId] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [PK_Test]
    PRIMARY KEY CLUSTERED ([TestId] ASC);
GO

-- Creating primary key on [TreningId] in table 'Trening'
ALTER TABLE [dbo].[Trening]
ADD CONSTRAINT [PK_Trening]
    PRIMARY KEY CLUSTERED ([TreningId] ASC);
GO

-- Creating primary key on [IstezanjeId] in table 'IstezanjePopis'
ALTER TABLE [dbo].[IstezanjePopis]
ADD CONSTRAINT [PK_IstezanjePopis]
    PRIMARY KEY CLUSTERED ([IstezanjeId] ASC);
GO

-- Creating primary key on [VjezbeId] in table 'VjezbePopis'
ALTER TABLE [dbo].[VjezbePopis]
ADD CONSTRAINT [PK_VjezbePopis]
    PRIMARY KEY CLUSTERED ([VjezbeId] ASC);
GO

-- Creating primary key on [ZagrijavanjeId] in table 'ZagrijavanjePopis'
ALTER TABLE [dbo].[ZagrijavanjePopis]
ADD CONSTRAINT [PK_ZagrijavanjePopis]
    PRIMARY KEY CLUSTERED ([ZagrijavanjeId] ASC);
GO

-- Creating primary key on [ZagrijavanjeId] in table 'Zagrijavanje'
ALTER TABLE [dbo].[Zagrijavanje]
ADD CONSTRAINT [PK_Zagrijavanje]
    PRIMARY KEY CLUSTERED ([ZagrijavanjeId] ASC);
GO

-- Creating primary key on [VjezbaId] in table 'Vjezba'
ALTER TABLE [dbo].[Vjezba]
ADD CONSTRAINT [PK_Vjezba]
    PRIMARY KEY CLUSTERED ([VjezbaId] ASC);
GO

-- Creating primary key on [IstezanjeId] in table 'Istezanje'
ALTER TABLE [dbo].[Istezanje]
ADD CONSTRAINT [PK_Istezanje]
    PRIMARY KEY CLUSTERED ([IstezanjeId] ASC);
GO

-- Creating primary key on [SlikaId] in table 'Slika'
ALTER TABLE [dbo].[Slika]
ADD CONSTRAINT [PK_Slika]
    PRIMARY KEY CLUSTERED ([SlikaId] ASC);
GO

-- Creating primary key on [ZagrijavanjeSlikeId] in table 'ZagrijavanjeSlike'
ALTER TABLE [dbo].[ZagrijavanjeSlike]
ADD CONSTRAINT [PK_ZagrijavanjeSlike]
    PRIMARY KEY CLUSTERED ([ZagrijavanjeSlikeId] ASC);
GO

-- Creating primary key on [VjezbeSlikeId] in table 'VjezbeSlike'
ALTER TABLE [dbo].[VjezbeSlike]
ADD CONSTRAINT [PK_VjezbeSlike]
    PRIMARY KEY CLUSTERED ([VjezbeSlikeId] ASC);
GO

-- Creating primary key on [IstezanjeSlikeId] in table 'IstezanjeSlike'
ALTER TABLE [dbo].[IstezanjeSlike]
ADD CONSTRAINT [PK_IstezanjeSlike]
    PRIMARY KEY CLUSTERED ([IstezanjeSlikeId] ASC);
GO

-- Creating primary key on [SekcijaId] in table 'SekcijaVjezbi'
ALTER TABLE [dbo].[SekcijaVjezbi]
ADD CONSTRAINT [PK_SekcijaVjezbi]
    PRIMARY KEY CLUSTERED ([SekcijaId] ASC);
GO

-- Creating primary key on [ClanSlikeId] in table 'ClanSlike'
ALTER TABLE [dbo].[ClanSlike]
ADD CONSTRAINT [PK_ClanSlike]
    PRIMARY KEY CLUSTERED ([ClanSlikeId] ASC);
GO

-- Creating primary key on [AntropometrijaId] in table 'Antropometrija'
ALTER TABLE [dbo].[Antropometrija]
ADD CONSTRAINT [PK_Antropometrija]
    PRIMARY KEY CLUSTERED ([AntropometrijaId] ASC);
GO

-- Creating primary key on [AmnezaId] in table 'Amneza'
ALTER TABLE [dbo].[Amneza]
ADD CONSTRAINT [PK_Amneza]
    PRIMARY KEY CLUSTERED ([AmnezaId] ASC);
GO

-- Creating primary key on [SlikaId] in table 'AmnezaSlike'
ALTER TABLE [dbo].[AmnezaSlike]
ADD CONSTRAINT [PK_AmnezaSlike]
    PRIMARY KEY CLUSTERED ([SlikaId] ASC);
GO

-- Creating primary key on [FunkcionalniRezultatiTestId] in table 'FunkcionalniRezultatiTest'
ALTER TABLE [dbo].[FunkcionalniRezultatiTest]
ADD CONSTRAINT [PK_FunkcionalniRezultatiTest]
    PRIMARY KEY CLUSTERED ([FunkcionalniRezultatiTestId] ASC);
GO

-- Creating primary key on [MotorickiRezultatiTestId] in table 'MotorickiRezultatiTest'
ALTER TABLE [dbo].[MotorickiRezultatiTest]
ADD CONSTRAINT [PK_MotorickiRezultatiTest]
    PRIMARY KEY CLUSTERED ([MotorickiRezultatiTestId] ASC);
GO

-- Creating primary key on [AerobnaVjezbaId] in table 'AerobneVjezbePopis'
ALTER TABLE [dbo].[AerobneVjezbePopis]
ADD CONSTRAINT [PK_AerobneVjezbePopis]
    PRIMARY KEY CLUSTERED ([AerobnaVjezbaId] ASC);
GO

-- Creating primary key on [AnaerobnaVjezbaId] in table 'AnaerobneVjezbePopis'
ALTER TABLE [dbo].[AnaerobneVjezbePopis]
ADD CONSTRAINT [PK_AnaerobneVjezbePopis]
    PRIMARY KEY CLUSTERED ([AnaerobnaVjezbaId] ASC);
GO

-- Creating primary key on [AerobnaVjezbaSlikaId] in table 'AerobneVjezbeSlike'
ALTER TABLE [dbo].[AerobneVjezbeSlike]
ADD CONSTRAINT [PK_AerobneVjezbeSlike]
    PRIMARY KEY CLUSTERED ([AerobnaVjezbaSlikaId] ASC);
GO

-- Creating primary key on [AnaerobnaVjezbaSlikaId] in table 'AnaerobneVjezbeSlike'
ALTER TABLE [dbo].[AnaerobneVjezbeSlike]
ADD CONSTRAINT [PK_AnaerobneVjezbeSlike]
    PRIMARY KEY CLUSTERED ([AnaerobnaVjezbaSlikaId] ASC);
GO

-- Creating primary key on [AerobnaVjezbaId] in table 'AerobneVjezbe'
ALTER TABLE [dbo].[AerobneVjezbe]
ADD CONSTRAINT [PK_AerobneVjezbe]
    PRIMARY KEY CLUSTERED ([AerobnaVjezbaId] ASC);
GO

-- Creating primary key on [AnaerobnaVjezbaId] in table 'AnaerobneVjezbe'
ALTER TABLE [dbo].[AnaerobneVjezbe]
ADD CONSTRAINT [PK_AnaerobneVjezbe]
    PRIMARY KEY CLUSTERED ([AnaerobnaVjezbaId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClanId] in table 'Trening'
ALTER TABLE [dbo].[Trening]
ADD CONSTRAINT [FK_ClanTrening]
    FOREIGN KEY ([ClanId])
    REFERENCES [dbo].[Clan]
        ([ClanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanTrening'
CREATE INDEX [IX_FK_ClanTrening]
ON [dbo].[Trening]
    ([ClanId]);
GO

-- Creating foreign key on [TreningId] in table 'Zagrijavanje'
ALTER TABLE [dbo].[Zagrijavanje]
ADD CONSTRAINT [FK_TreningZagrijavanje]
    FOREIGN KEY ([TreningId])
    REFERENCES [dbo].[Trening]
        ([TreningId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TreningZagrijavanje'
CREATE INDEX [IX_FK_TreningZagrijavanje]
ON [dbo].[Zagrijavanje]
    ([TreningId]);
GO

-- Creating foreign key on [TreningId] in table 'Istezanje'
ALTER TABLE [dbo].[Istezanje]
ADD CONSTRAINT [FK_TreningIstezanje]
    FOREIGN KEY ([TreningId])
    REFERENCES [dbo].[Trening]
        ([TreningId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TreningIstezanje'
CREATE INDEX [IX_FK_TreningIstezanje]
ON [dbo].[Istezanje]
    ([TreningId]);
GO

-- Creating foreign key on [ClanId] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [FK_ClanTest]
    FOREIGN KEY ([ClanId])
    REFERENCES [dbo].[Clan]
        ([ClanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanTest'
CREATE INDEX [IX_FK_ClanTest]
ON [dbo].[Test]
    ([ClanId]);
GO

-- Creating foreign key on [TestTestId] in table 'Slika'
ALTER TABLE [dbo].[Slika]
ADD CONSTRAINT [FK_TestSlika]
    FOREIGN KEY ([TestTestId])
    REFERENCES [dbo].[Test]
        ([TestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestSlika'
CREATE INDEX [IX_FK_TestSlika]
ON [dbo].[Slika]
    ([TestTestId]);
GO

-- Creating foreign key on [ZagrijavanjePopisZagrijavanjeId] in table 'ZagrijavanjeSlike'
ALTER TABLE [dbo].[ZagrijavanjeSlike]
ADD CONSTRAINT [FK_ZagrijavanjeSlikeZagrijavanjePopis]
    FOREIGN KEY ([ZagrijavanjePopisZagrijavanjeId])
    REFERENCES [dbo].[ZagrijavanjePopis]
        ([ZagrijavanjeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZagrijavanjeSlikeZagrijavanjePopis'
CREATE INDEX [IX_FK_ZagrijavanjeSlikeZagrijavanjePopis]
ON [dbo].[ZagrijavanjeSlike]
    ([ZagrijavanjePopisZagrijavanjeId]);
GO

-- Creating foreign key on [VjezbePopisVjezbeId] in table 'VjezbeSlike'
ALTER TABLE [dbo].[VjezbeSlike]
ADD CONSTRAINT [FK_VjezbeSlikeVjezbePopis]
    FOREIGN KEY ([VjezbePopisVjezbeId])
    REFERENCES [dbo].[VjezbePopis]
        ([VjezbeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VjezbeSlikeVjezbePopis'
CREATE INDEX [IX_FK_VjezbeSlikeVjezbePopis]
ON [dbo].[VjezbeSlike]
    ([VjezbePopisVjezbeId]);
GO

-- Creating foreign key on [IstezanjePopisIstezanjeId] in table 'IstezanjeSlike'
ALTER TABLE [dbo].[IstezanjeSlike]
ADD CONSTRAINT [FK_IstezanjeSlikeIstezanjePopis]
    FOREIGN KEY ([IstezanjePopisIstezanjeId])
    REFERENCES [dbo].[IstezanjePopis]
        ([IstezanjeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IstezanjeSlikeIstezanjePopis'
CREATE INDEX [IX_FK_IstezanjeSlikeIstezanjePopis]
ON [dbo].[IstezanjeSlike]
    ([IstezanjePopisIstezanjeId]);
GO

-- Creating foreign key on [TreningId] in table 'SekcijaVjezbi'
ALTER TABLE [dbo].[SekcijaVjezbi]
ADD CONSTRAINT [FK_TreningSekcijaVjezbi]
    FOREIGN KEY ([TreningId])
    REFERENCES [dbo].[Trening]
        ([TreningId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TreningSekcijaVjezbi'
CREATE INDEX [IX_FK_TreningSekcijaVjezbi]
ON [dbo].[SekcijaVjezbi]
    ([TreningId]);
GO

-- Creating foreign key on [SekcijaId] in table 'Vjezba'
ALTER TABLE [dbo].[Vjezba]
ADD CONSTRAINT [FK_SekcijaVjezbiVjezba]
    FOREIGN KEY ([SekcijaId])
    REFERENCES [dbo].[SekcijaVjezbi]
        ([SekcijaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SekcijaVjezbiVjezba'
CREATE INDEX [IX_FK_SekcijaVjezbiVjezba]
ON [dbo].[Vjezba]
    ([SekcijaId]);
GO

-- Creating foreign key on [ZagrijavanjePopisZagrijavanjeId] in table 'Zagrijavanje'
ALTER TABLE [dbo].[Zagrijavanje]
ADD CONSTRAINT [FK_ZagrijavanjeZagrijavanjePopis]
    FOREIGN KEY ([ZagrijavanjePopisZagrijavanjeId])
    REFERENCES [dbo].[ZagrijavanjePopis]
        ([ZagrijavanjeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZagrijavanjeZagrijavanjePopis'
CREATE INDEX [IX_FK_ZagrijavanjeZagrijavanjePopis]
ON [dbo].[Zagrijavanje]
    ([ZagrijavanjePopisZagrijavanjeId]);
GO

-- Creating foreign key on [VjezbePopisVjezbeId] in table 'Vjezba'
ALTER TABLE [dbo].[Vjezba]
ADD CONSTRAINT [FK_VjezbaVjezbePopis]
    FOREIGN KEY ([VjezbePopisVjezbeId])
    REFERENCES [dbo].[VjezbePopis]
        ([VjezbeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VjezbaVjezbePopis'
CREATE INDEX [IX_FK_VjezbaVjezbePopis]
ON [dbo].[Vjezba]
    ([VjezbePopisVjezbeId]);
GO

-- Creating foreign key on [IstezanjePopisIstezanjeId] in table 'Istezanje'
ALTER TABLE [dbo].[Istezanje]
ADD CONSTRAINT [FK_IstezanjeIstezanjePopis]
    FOREIGN KEY ([IstezanjePopisIstezanjeId])
    REFERENCES [dbo].[IstezanjePopis]
        ([IstezanjeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IstezanjeIstezanjePopis'
CREATE INDEX [IX_FK_IstezanjeIstezanjePopis]
ON [dbo].[Istezanje]
    ([IstezanjePopisIstezanjeId]);
GO

-- Creating foreign key on [ClanClanId] in table 'ClanSlike'
ALTER TABLE [dbo].[ClanSlike]
ADD CONSTRAINT [FK_ClanSlikeClan]
    FOREIGN KEY ([ClanClanId])
    REFERENCES [dbo].[Clan]
        ([ClanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClanSlikeClan'
CREATE INDEX [IX_FK_ClanSlikeClan]
ON [dbo].[ClanSlike]
    ([ClanClanId]);
GO

-- Creating foreign key on [ClanClanId] in table 'Antropometrija'
ALTER TABLE [dbo].[Antropometrija]
ADD CONSTRAINT [FK_AntropometrijaClan]
    FOREIGN KEY ([ClanClanId])
    REFERENCES [dbo].[Clan]
        ([ClanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AntropometrijaClan'
CREATE INDEX [IX_FK_AntropometrijaClan]
ON [dbo].[Antropometrija]
    ([ClanClanId]);
GO

-- Creating foreign key on [AmnezaAmnezaId] in table 'AmnezaSlike'
ALTER TABLE [dbo].[AmnezaSlike]
ADD CONSTRAINT [FK_AmnezaSlikeAmneza]
    FOREIGN KEY ([AmnezaAmnezaId])
    REFERENCES [dbo].[Amneza]
        ([AmnezaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AmnezaSlikeAmneza'
CREATE INDEX [IX_FK_AmnezaSlikeAmneza]
ON [dbo].[AmnezaSlike]
    ([AmnezaAmnezaId]);
GO

-- Creating foreign key on [ClanClanId] in table 'Amneza'
ALTER TABLE [dbo].[Amneza]
ADD CONSTRAINT [FK_AmnezaClan]
    FOREIGN KEY ([ClanClanId])
    REFERENCES [dbo].[Clan]
        ([ClanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AmnezaClan'
CREATE INDEX [IX_FK_AmnezaClan]
ON [dbo].[Amneza]
    ([ClanClanId]);
GO

-- Creating foreign key on [TestId] in table 'MotorickiRezultatiTest'
ALTER TABLE [dbo].[MotorickiRezultatiTest]
ADD CONSTRAINT [FK_TestMotorickiRezultatiTest]
    FOREIGN KEY ([TestId])
    REFERENCES [dbo].[Test]
        ([TestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestMotorickiRezultatiTest'
CREATE INDEX [IX_FK_TestMotorickiRezultatiTest]
ON [dbo].[MotorickiRezultatiTest]
    ([TestId]);
GO

-- Creating foreign key on [TestId] in table 'FunkcionalniRezultatiTest'
ALTER TABLE [dbo].[FunkcionalniRezultatiTest]
ADD CONSTRAINT [FK_TestFunkcionalniRezultatiTest]
    FOREIGN KEY ([TestId])
    REFERENCES [dbo].[Test]
        ([TestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestFunkcionalniRezultatiTest'
CREATE INDEX [IX_FK_TestFunkcionalniRezultatiTest]
ON [dbo].[FunkcionalniRezultatiTest]
    ([TestId]);
GO

-- Creating foreign key on [AerobneVjezbePopisAerobnaVjezbaId] in table 'AerobneVjezbeSlike'
ALTER TABLE [dbo].[AerobneVjezbeSlike]
ADD CONSTRAINT [FK_AerobneVjezbePopisAerobneVjezbeSlike]
    FOREIGN KEY ([AerobneVjezbePopisAerobnaVjezbaId])
    REFERENCES [dbo].[AerobneVjezbePopis]
        ([AerobnaVjezbaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AerobneVjezbePopisAerobneVjezbeSlike'
CREATE INDEX [IX_FK_AerobneVjezbePopisAerobneVjezbeSlike]
ON [dbo].[AerobneVjezbeSlike]
    ([AerobneVjezbePopisAerobnaVjezbaId]);
GO

-- Creating foreign key on [AnaerobneVjezbePopisAnaerobnaVjezbaId] in table 'AnaerobneVjezbeSlike'
ALTER TABLE [dbo].[AnaerobneVjezbeSlike]
ADD CONSTRAINT [FK_AnaerobneVjezbePopisAnaerobneVjezbeSlike]
    FOREIGN KEY ([AnaerobneVjezbePopisAnaerobnaVjezbaId])
    REFERENCES [dbo].[AnaerobneVjezbePopis]
        ([AnaerobnaVjezbaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnaerobneVjezbePopisAnaerobneVjezbeSlike'
CREATE INDEX [IX_FK_AnaerobneVjezbePopisAnaerobneVjezbeSlike]
ON [dbo].[AnaerobneVjezbeSlike]
    ([AnaerobneVjezbePopisAnaerobnaVjezbaId]);
GO

-- Creating foreign key on [SekcijaVjezbiSekcijaId] in table 'AerobneVjezbe'
ALTER TABLE [dbo].[AerobneVjezbe]
ADD CONSTRAINT [FK_SekcijaVjezbiAerobneVjezbe]
    FOREIGN KEY ([SekcijaVjezbiSekcijaId])
    REFERENCES [dbo].[SekcijaVjezbi]
        ([SekcijaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SekcijaVjezbiAerobneVjezbe'
CREATE INDEX [IX_FK_SekcijaVjezbiAerobneVjezbe]
ON [dbo].[AerobneVjezbe]
    ([SekcijaVjezbiSekcijaId]);
GO

-- Creating foreign key on [AerobneVjezbePopisAerobnaVjezbaId] in table 'AerobneVjezbe'
ALTER TABLE [dbo].[AerobneVjezbe]
ADD CONSTRAINT [FK_AerobneVjezbeAerobneVjezbePopis]
    FOREIGN KEY ([AerobneVjezbePopisAerobnaVjezbaId])
    REFERENCES [dbo].[AerobneVjezbePopis]
        ([AerobnaVjezbaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AerobneVjezbeAerobneVjezbePopis'
CREATE INDEX [IX_FK_AerobneVjezbeAerobneVjezbePopis]
ON [dbo].[AerobneVjezbe]
    ([AerobneVjezbePopisAerobnaVjezbaId]);
GO

-- Creating foreign key on [AnaerobneVjezbePopisAnaerobnaVjezbaId] in table 'AnaerobneVjezbe'
ALTER TABLE [dbo].[AnaerobneVjezbe]
ADD CONSTRAINT [FK_AnaerobneVjezbePopisAnaerobneVjezbe]
    FOREIGN KEY ([AnaerobneVjezbePopisAnaerobnaVjezbaId])
    REFERENCES [dbo].[AnaerobneVjezbePopis]
        ([AnaerobnaVjezbaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnaerobneVjezbePopisAnaerobneVjezbe'
CREATE INDEX [IX_FK_AnaerobneVjezbePopisAnaerobneVjezbe]
ON [dbo].[AnaerobneVjezbe]
    ([AnaerobneVjezbePopisAnaerobnaVjezbaId]);
GO

-- Creating foreign key on [SekcijaVjezbiSekcijaId] in table 'AnaerobneVjezbe'
ALTER TABLE [dbo].[AnaerobneVjezbe]
ADD CONSTRAINT [FK_AnaerobneVjezbeSekcijaVjezbi]
    FOREIGN KEY ([SekcijaVjezbiSekcijaId])
    REFERENCES [dbo].[SekcijaVjezbi]
        ([SekcijaId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnaerobneVjezbeSekcijaVjezbi'
CREATE INDEX [IX_FK_AnaerobneVjezbeSekcijaVjezbi]
ON [dbo].[AnaerobneVjezbe]
    ([SekcijaVjezbiSekcijaId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------