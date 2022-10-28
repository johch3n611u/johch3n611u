USE master 
GO
IF  EXISTS (SELECT name FROM sys.databases WHERE name='MySystem')
DROP DATABASE MySystem
GO

Create database [MySystem]
go

use [MySystem]
go

Create table Members(
	Account varchar(10) primary key,
	Pswd varchar(4000) not null,
	[Name] nvarchar(20) not null,
	Birthday datetime not null,
	Email varchar(30) not null,
	Gender bit not null,
	EduLevel char(1) not null,
	Notes nvarchar(max)
)

Create table Edu(
	EduLevel_Code char(1) primary key,
	EduLevel nvarchar(5) not null
)
go

ALTER TABLE Members
  ADD FOREIGN KEY([EduLevel]) REFERENCES [Edu] ([EduLevel_Code])
GO

Insert into Edu values('1','國小'),('2','國中'),('3','高中'),('4','大學'),('5','研究所以上')
go

Insert into Members values
('wuchi',HASHBYTES('SHA2_256','abcd1234'),'張無忌','1999-12-3','wuchi@wda.gov.tw',1,'4',null ),
('huchun',HASHBYTES('SHA2_256','1234abcd'),'令狐沖','1984-11-6','huchun@wda.gov.tw',1,'5',null ),
('fone',HASHBYTES('SHA2_256','abcdaaaa'),'蕭峰','1993-2-23','fone@wda.gov.tw',1,'3',null ),
('bubai',HASHBYTES('SHA2_256','1234aaaa'),'東方不敗','1988-7-25','bubai@wda.gov.tw',0,'3',null ),
('hufay',HASHBYTES('SHA2_256','fhff4aaaa'),'胡斐','1970-6-22','hufay@wda.gov.tw',1,'4',null ),
('guochin',HASHBYTES('SHA2_256','ddfh6789'),'郭靖','1971-4-7','guochin@wda.gov.tw',1,'4',null ),
('bow',HASHBYTES('SHA2_256','dsfa53df'),'韋小寶','1976-8-31','bow@wda.gov.tw',1,'4',null ),
('yang',HASHBYTES('SHA2_256','sfjaaao9t'),'楊過','1985-6-11','yang@wda.gov.tw',1,'5',null ),
('dragan',HASHBYTES('SHA2_256','df$678hff'),'小龍女','1986-10-10','dragan@wda.gov.tw',0,'2',null ),
('HuangRong',HASHBYTES('SHA2_256','4rtkdasdf'),'黃蓉','1998-4-8','HuangRong@wda.gov.tw',0,'3',null ),
('WangYuYen',HASHBYTES('SHA2_256','fgfbhn6asf'),'王語焉','1963-6-17','WangYuYen@wda.gov.tw',0,'4',null ),
('bochun',HASHBYTES('SHA2_256','dsf56tyhdd'),'岳不群','1989-5-21','bochun@wda.gov.tw',0,'4',null ),
('botong',HASHBYTES('SHA2_256','abcd233455'),'周伯通','1990-9-15','botong@wda.gov.tw',1,'5',null )
go





