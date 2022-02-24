--Id					int
--Rid					nvarchar(255)
--InsertedTs			datetime
--CheckedTs			datetime
--StatusDetailsPage	nvarchar(150)
--Name				nvarchar(255)
--Vorname				nvarchar(150)
--Kammer				nvarchar(150)
--Adresse				nvarchar(150)

USE StBK_CheckRids;
CREATE TABLE Results
(
	[Id] int IDENTITY(1,1),
	[Rid] nvarchar(255),
	[InsertedTs] nvarchar(150),
	[CheckedTs] nvarchar(150),
	[StatusDetailsPage] nvarchar(150),
	[Name] nvarchar(255),
	[Vorname] nvarchar(150),
	[Kammer] nvarchar(150),
	[Adresse] nvarchar(150)
);