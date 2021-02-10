use master
go

create database EvidencijaSati
go

use EvidencijaSati
go

alter database current collate Croatian_CS_AI_KS_WS;
go

alter table Djelatnik
add primary key (IDDjelatnik)
go

alter table Projekt
add foreign key (KlijentID) references Klijent(IDKlijent)
go

create proc GetDjelatnikByEmail
	@Email nvarchar(50)
as
begin
	select * from Djelatnik
	where Djelatnik.Email = @Email
end
go

create proc SelectDjelatnik
	@Id int
as
begin
	select * from Djelatnik
	where IDDjelatnik = @Id
end
go

create proc GetProjektiDjelatnika
	@Id int
as
begin
	select p.IDProjekt, p.Naziv, p.KlijentID, p.DatumOtvaranja, p.VoditeljProjektaID
	from ProjektDjelatnik as pd
	inner join Projekt as p on p.IDProjekt = pd.ProjektID
	where pd.DjelatnikID = @Id
end
go

insert into Djelatnik values
	(200, 'Matias', 'Salopek', 'msalopek@ingenii.hr', '1980-01-29', 'EH7VIH', 1, null, 1)
go

create sequence dbo.DjelatnikId_Sequence
    as int
    start with 201
    INCREMENT BY 1
    MINVALUE 200
    NO MAXVALUE
go

create sequence dbo.TimId_Sequence
    as int
    start with 21
    INCREMENT BY 1
    MINVALUE 20
    NO MAXVALUE
go

create sequence dbo.ProjektId_Sequence
    as int
    start with 261
    INCREMENT BY 1
    MINVALUE 260
    NO MAXVALUE
go

create sequence dbo.KlijentId_Sequence
    as int
    start with 101
    INCREMENT BY 1
    MINVALUE 100
    NO MAXVALUE
go

create sequence dbo.ProjektDjelatnikId_Sequence
    as int
    start with 699
    INCREMENT BY 1
    MINVALUE 698
    NO MAXVALUE
go

create table Satnica (
	IDSatnica int primary key identity,
	DjelatnikID int foreign key references Djelatnik(IDDjelatnik),
	Datum date,
	TotalRedovni int,
	TotalPrekovremeni int,
	Total int,
	[Status] int foreign key references SatnicaStatusType(IDSatnicaStatusType),
	Komentar nvarchar(max))
go

create proc AddSatnica
	@DjelatnikId int,
	@Datum date,
	@TotalRedovni int,
	@TotalPrekovremeni int,
	@Total int,
	@Status int,
	@Komentar nvarchar(max),
	@Id int out
as
begin
	insert into Satnica values 
		(@DjelatnikId, @Datum, @TotalRedovni, @TotalPrekovremeni, @Total, @Status, @Komentar)
	set @Id = SCOPE_IDENTITY()
end
go

create table SatnicaStatusType (
	IDSatnicaStatusType int primary key identity,
	Name nvarchar(20))
go

insert into SatnicaStatusType values
	('WAITING_APPROVAL'),
	('REVISION_NEEDED'),
	('APPROVED'),
	('WAITING_SUBMIT')
go

create table SatnicaProjekta (
	IDSatnicaProjekta int primary key identity,
	SatnicaID int foreign key references Satnica(IDSatnica),
	ProjektID int foreign key references Projekt(IDProjekt),
	[Start] datetime,
	[End] datetime,
	TotalMin float,
	Prekovremeni float)
go

create proc GetSatniceProjektaSatnice
	@IdSatnica int
as
begin
	select *
	from SatnicaProjekta as sp
	where sp.SatnicaID = @IdSatnica
end
go

create proc GetSatniceDjelatnika
	@Id int
as
begin
	select * from Satnica as s
	where s.DjelatnikID = @Id
end
go

create proc AddSatnicaProjekta
	@SatnicaID int,
	@ProjektID int,
	@Start datetime,
	@TotalMin float,
	@Id int out
as
begin
	insert into SatnicaProjekta values
		(@SatnicaID, @ProjektID, @Start, null, @TotalMin, 0)
	set @Id = SCOPE_IDENTITY()
end
go

create proc SelectProjekt
	@Id int
as
begin
	select * from Projekt
	where Projekt.IDProjekt = @Id
end
go

create proc ChangeSatnicaStatus
	@IdSatnica int,
	@Status int
as
begin
	update Satnica
	set Status = @Status
	where IDSatnica = @IdSatnica
end
go

create proc DeleteSatnicaProjekta
	@Id int
as
begin
	delete from SatnicaProjekta
	where IDSatnicaProjekta = @Id
end
go

create proc UpdateSatnicaProjekta
	@Id int, 
	@Start datetime,
	@End datetime,
	@TotalMin float
as
begin
	update SatnicaProjekta
	set
		[Start] = @Start,
		[End] = @End,
		TotalMin = @TotalMin
	where IDSatnicaProjekta = @Id
end
go

create proc SelectRadnaSatnicaProjekta
	@IdProjekt int
as
begin
	select * from SatnicaProjekta
	where [End] is null and [Start] is not null and ProjektID = @IdProjekt
end
go

create proc UpdateEndSatniceProjekta
	@Id int,
	@End datetime,
	@TotalMin float
as
begin
	update SatnicaProjekta
	set
		[End] = @End,
		TotalMin = @TotalMin
	where IDSatnicaProjekta = @Id
end
go

create proc UpdateZaporka
	@Id int,
	@Pass nvarchar(50)
as
begin
	update Djelatnik
	set Zaporka = @Pass
	where IDDjelatnik = @Id
end
go

create proc UpdateSatnica
	@DjelatnikId int,
	@Total int,
	@TotalRedovni int,
	@TotalPrekovremeni int,
	@Komentar nvarchar(50)
as
begin
	update Satnica
	set
		Total = @Total,
		TotalRedovni = @TotalRedovni,
		TotalPrekovremeni = @TotalPrekovremeni
	where DjelatnikID = @DjelatnikId
end
go

create proc GetSatniceProjektaZaVoditelja
	@IdVoditelj int,
	@ProjId int,
	@IdTip int,
	@IdStatus int
as
begin
	if @IdTip = 1
		select * from Satnica
		where IDSatnica in (
			select SatnicaID 
			from SatnicaProjekta)
				and [Status] = @IdStatus
	else
		select * from Satnica	
		where Satnica.IDSatnica in (
			select sp.SatnicaID 
			from SatnicaProjekta as sp
			where ProjektID in (
				select ProjektID
				from ProjektDjelatnik
				where DjelatnikID in(
					select IDDjelatnik from Djelatnik
					where TimID = (
						select TimID from Djelatnik
						where IDDjelatnik = @IdVoditelj))))
		and [Status] = @IdStatus
end
go

create proc GetDjelatnici
as
begin
	select * from Djelatnik
end
go

create proc GetTimovi
as
begin
	select * from Tim
end
go

alter table Djelatnik
add IsActive int
go

alter table Tim
add IsActive int
go

alter table Projekt
add IsActive int
go

alter table Projekt
add DatumZatvaranja date
go

alter table Klijent
add IsActive int
go

create proc UpdateDjelatnik
	@Id int,
	@Ime nvarchar(50),
	@Prezime nvarchar(50),
	@Email nvarchar(50),
	@Zaporka nvarchar(50),
	@DatumZaposlenja date,
	@TipDjelatnika int,
	@TimID int
as
begin
	update Djelatnik
	set
		Ime = @Ime,
		Prezime = @Prezime,
		Email = @Email,
		Zaporka = @Zaporka,
		DatumZaposlenja = @DatumZaposlenja,
		TipDjelatnikaID = @TipDjelatnika,
		TimID = @TimID
	where IDDjelatnik = @Id
end
go

update Djelatnik
set IsActive = 1
go

update Projekt
set IsActive = 1
go

update Tim
set IsActive = 1
go

update Klijent
set IsActive = 1
go

create proc AktivirajDjelatnika
	@Id int
as
begin
	update Djelatnik
	set IsActive = 1
	where IDDjelatnik = @Id
end
go

create proc DeaktivirajDjelatnika
	@Id int
as
begin
	update Djelatnik
	set IsActive = 0
	where IDDjelatnik = @Id
end
go

create proc DodajDjelatnika
	@Ime nvarchar(50),
	@Prezime nvarchar(50),
	@Email nvarchar(50),
	@DatumZaposlenja date,
	@Zaporka nvarchar(50),
	@TipDjelatnika int,
	@TimId int,
	@Id int out
as
begin
	insert into Djelatnik 
		select next value for dbo.DjelatnikId_Sequence,
			@Ime, @Prezime, @Email, @DatumZaposlenja, @Zaporka, @TipDjelatnika, @TimId, 1
	set @Id = SCOPE_IDENTITY()
end
go

create proc AddTim
	@Naziv nvarchar(50),
	@DatumKreiranja date,
	@Id int out
as
begin
	insert into Tim
		select next value for dbo.TimId_Sequence,
			@Naziv, @DatumKreiranja, 1
	set @Id =SCOPE_IDENTITY()
end
go

create proc AddProjekt
	@Naziv nvarchar(50),
	@KlijentId int,
	@DatumOtvaranja date,
	@VoditeljProjektaId int,
	@DatumZatvaranja date,
	@Id int out
as
begin
	insert into Projekt
		select next value for dbo.ProjektId_Sequence,
			@Naziv, @KlijentId, @DatumOtvaranja, @VoditeljProjektaId, 1, null
	set @Id =SCOPE_IDENTITY()
end
go

create proc AddKlijent
	@Naziv nvarchar(50),
	@Telefon nvarchar(50),
	@Email nvarchar(50),
	@Id int out
as
begin
	insert into Klijent
		select next value for dbo.ProjektId_Sequence,
			@Naziv, @Telefon, @Email, 1
	set @Id = SCOPE_IDENTITY()
end
go

create proc SelectTim
	@Id int
as
begin
	select * from Tim
	where IDTim = @Id
end
go

create proc GetProjektiDjelatnika
	@Id int
as
begin
	select p.*
	from ProjektDjelatnik as pd
	inner join Projekt as p on p.IDProjekt = pd.ProjektID
	where pd.DjelatnikID = @Id
end
go

create proc GetClanoviTima
	@IdTim int
as
begin
	select *
	from Djelatnik as d
	where d.TimID = @IdTim
end
go

create proc DeaktivirajTim
	@Id int
as
begin
	update Tim
	set IsActive = 0
	where IDTim = @Id
end
go

create proc AktivirajTim
	@Id int
as
begin
	update Tim
	set IsActive = 1
	where IDTim = @Id
end
go

create proc UpdateTim
	@Id int,
	@Naziv nvarchar(50),
	@DatumKreiranja date
as
begin
	update Tim
	set
		Naziv = @Naziv,
		DatumKreiranja = @DatumKreiranja
	where IDTim = @Id
end
go

create proc GetVoditeljiProjekata
as
begin
	
  select * from Djelatnik
  where IDDjelatnik in (
	select p.VoditeljProjektaID
	from Projekt as p)
end
go

create proc GetZaposleniNaProjektu
	@IdProjekt int
as
begin
	select *
	from Djelatnik as d
	where d.IDDjelatnik in (
		select pd.DjelatnikID
		from ProjektDjelatnik as pd
		where pd.ProjektID = @IdProjekt)
end
go

create proc GetKlijenti
as
begin
	select * from Klijent
end
go

create proc GetProjekti
as
begin
	select * from Projekt
end
go

create proc DeaktivirajProjekt
	@Id int
as
begin
	update Projekt
	set
		IsActive = 0
	where IDProjekt = @Id
end
go

create proc AktivirajProjekt
	@Id int
as
begin
	update Projekt
	set
		IsActive = 1
	where IDProjekt = @Id
end
go

create proc AddProjektDjelatnik
	@IdProjekt int,
	@IdDjelatnik int,
	@Id int out
as
begin
	insert into ProjektDjelatnik
		select next value for dbo.ProjektDjelatnikId_Sequence,
			@IdProjekt, @IdDjelatnik
	set @Id = SCOPE_IDENTITY()
end
go

create proc DeleteProjektDjelatnik
	@IdProjekt int,
	@IdDjelatnik int
as
begin
	delete from ProjektDjelatnik
	where ProjektID = @IdProjekt and DjelatnikID = @IdDjelatnik
end
go

create proc UpdateProjekt
	@Id int,
	@Naziv nvarchar(50),
	@KlijentId int,
	@DatumOtvaranja date,
	@VoditeljProjektaId int,
	@DatumZatvaranja date
as
begin
	update Projekt
	set	
		Naziv = @Naziv,
		KlijentID = @KlijentId,
		DatumOtvaranja = @DatumOtvaranja,
		VoditeljProjektaID = @VoditeljProjektaId,
		DatumZatvaranja = @DatumZatvaranja
	where IDProjekt = @Id
end
go

create proc SelectKlijent
	@Id int
as
begin
	select * from Klijent
	where IDKlijent = @Id
end
go

create proc GetProjektiKlijenta
	@IdKlijent int
as
begin
	select * from Projekt
	where KlijentID = @IdKlijent
end
go

create proc AktivirajKlijenta
	@Id int
as
begin
	update Klijent
	set IsActive = 1
	where IDKlijent = @Id
end
go

create proc DeaktivirajKlijenta
	@Id int
as
begin
	update Klijent
	set IsActive = 0
	where IDKlijent = @Id
end
go

create proc UpdateKlijent
	@Id int,
	@Naziv nvarchar(50),
	@Email nvarchar(50),
	@Telefon nvarchar(50)
as
begin
	update Klijent
	set
		Naziv = @Naziv,
		Email = @Email,
		Telefon = @Telefon
	where IDKlijent = @Id
end
go

create proc UpdateSatnicaProjektaZaPredaju
	@SatnicaId int,
	@ProjektId int,
	@Redovni float,
	@Prekovremeni float
as
	delete from SatnicaProjekta
	where SatnicaID = @SatnicaId and ProjektID = @ProjektId
	insert into SatnicaProjekta values
		(@SatnicaId, @ProjektId, null, null, @Redovni, @Prekovremeni)
go

create proc SelectSatnica
	@Id int
as
begin
	select * from Satnica
	where IDSatnica = @Id
end
go

create proc SelectSatniceZaDoradu
	@Id int
as
begin
	select * from Satnica
	where DjelatnikID = @Id and [Status] = 2
end
go