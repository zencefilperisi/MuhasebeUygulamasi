create database MuhasebeDB
go

use MuhasebeDB
go

create table Kullanici (
Id int primary key identity,
KullaniciAdi nvarchar(50) not null unique,
Sifre nvarchar(100) not null,
AdSoyad nvarchar(100),
Email nvarchar(100),
Telefon nvarchar(20),
Rol nvarchar(20),
Aktif bit default 1,
KayitTarihi datetime default getdate()
);

create table StokKart (
Id int primary key identity,
StokKodu nvarchar(50),
StokAdi nvarchar(100),
Birim nvarchar(20),
Barkod nvarchar(50),
KDV decimal(5,2),
Aciklama nvarchar(255)
);

create table CariKart(
Id int identity(1,1) primary key,
CariKodu nvarchar(50) not null unique,
CariAdi nvarchar(150) not null,
Telefon nvarchar(20) null,
Adres nvarchar(250) null,
Email nvarchar(100) null
);

INSERT INTO CariKart (CariKodu, CariAdi, Telefon, Adres, Email)
VALUES 
('C001', 'ABC Ticaret', '05551234567', 'Ýstanbul', 'abc@ticaret.com'),
('C002', 'XYZ Ltd.', '05339876543', 'Ankara', 'xyz@ltd.com');

select * from CariKart

use MuhasebeDB

create table Users(
Id int primary key identity(1,1),
KullaniciAdi nvarchar(50) not null,
Sifre nvarchar(100) not null,
AdSoyad nvarchar(100),
Yetki nvarchar(50)
);

select * from Users
drop table Users

SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Users';

create table SatisFatura (
FaturaId int primary key identity(1,1),
FaturaNo nvarchar(50) not null, 
CariKodu nvarchar(50) not null,
UrunKodu nvarchar(50) not null,
UrunAdi nvarchar(100) not null,
Miktar decimal(18,2) not null,
BirimFiyat decimal(18,2) not null,
Tarih date not null
);

SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SatisFatura'

select * from SatisFatura
select * from CariKart

CREATE TABLE FisAll
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FisKodu NVARCHAR(50) NOT NULL,
    Cari NVARCHAR(100) NULL,
    OdemeTuru NVARCHAR(50) NULL,
    Tarih DATETIME NOT NULL DEFAULT GETDATE(),
    UrunKodu NVARCHAR(50) NULL,
    UrunAdi NVARCHAR(100) NULL,
    Miktar DECIMAL(18,2) NOT NULL DEFAULT 0,
    BirimFiyat DECIMAL(18,2) NOT NULL DEFAULT 0,
    Kdv DECIMAL(5,2) NOT NULL DEFAULT 0,
    Toplam AS (Miktar * BirimFiyat + (Miktar * BirimFiyat * Kdv / 100))
);
