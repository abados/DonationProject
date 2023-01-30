CREATE TABLE "Users"(
    "id" INT NOT NULL primary key identity,
    "UserType" NVARCHAR(max) NOT NULL
);



CREATE TABLE "ConfigData"(
    "KEY_TOKEN" NVARCHAR(max) NOT NULL,
    "VALUE" NVARCHAR(max) NOT NULL
);

UPDATE Config 
SET Bearer ='Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImtGVlhfRjFobTI1dklkRTFadWQ2cSJ9.eyJpc3MiOiJodHRwczovL2Rldi1xamY3aGdxZXUxNmZ5bWVtLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJEeFZDY0RMR3hGQXc2TmtEWUlKM1ZZeFFiT1RXU3cwaEBjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9kZXYtcWpmN2hncWV1MTZmeW1lbS51cy5hdXRoMC5jb20vYXBpL3YyLyIsImlhdCI6MTY3NTA3NzM2MSwiZXhwIjoxNjc3NjY5MzYxLCJhenAiOiJEeFZDY0RMR3hGQXc2TmtEWUlKM1ZZeFFiT1RXU3cwaCIsInNjb3BlIjoicmVhZDpjbGllbnRfZ3JhbnRzIGNyZWF0ZTpjbGllbnRfZ3JhbnRzIGRlbGV0ZTpjbGllbnRfZ3JhbnRzIHVwZGF0ZTpjbGllbnRfZ3JhbnRzIHJlYWQ6dXNlcnMgdXBkYXRlOnVzZXJzIGRlbGV0ZTp1c2VycyBjcmVhdGU6dXNlcnMgcmVhZDp1c2Vyc19hcHBfbWV0YWRhdGEgdXBkYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSBkZWxldGU6dXNlcnNfYXBwX21ldGFkYXRhIGNyZWF0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgcmVhZDp1c2VyX2N1c3RvbV9ibG9ja3MgY3JlYXRlOnVzZXJfY3VzdG9tX2Jsb2NrcyBkZWxldGU6dXNlcl9jdXN0b21fYmxvY2tzIGNyZWF0ZTp1c2VyX3RpY2tldHMgcmVhZDpjbGllbnRzIHVwZGF0ZTpjbGllbnRzIGRlbGV0ZTpjbGllbnRzIGNyZWF0ZTpjbGllbnRzIHJlYWQ6Y2xpZW50X2tleXMgdXBkYXRlOmNsaWVudF9rZXlzIGRlbGV0ZTpjbGllbnRfa2V5cyBjcmVhdGU6Y2xpZW50X2tleXMgcmVhZDpjb25uZWN0aW9ucyB1cGRhdGU6Y29ubmVjdGlvbnMgZGVsZXRlOmNvbm5lY3Rpb25zIGNyZWF0ZTpjb25uZWN0aW9ucyByZWFkOnJlc291cmNlX3NlcnZlcnMgdXBkYXRlOnJlc291cmNlX3NlcnZlcnMgZGVsZXRlOnJlc291cmNlX3NlcnZlcnMgY3JlYXRlOnJlc291cmNlX3NlcnZlcnMgcmVhZDpkZXZpY2VfY3JlZGVudGlhbHMgdXBkYXRlOmRldmljZV9jcmVkZW50aWFscyBkZWxldGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGNyZWF0ZTpkZXZpY2VfY3JlZGVudGlhbHMgcmVhZDpydWxlcyB1cGRhdGU6cnVsZXMgZGVsZXRlOnJ1bGVzIGNyZWF0ZTpydWxlcyByZWFkOnJ1bGVzX2NvbmZpZ3MgdXBkYXRlOnJ1bGVzX2NvbmZpZ3MgZGVsZXRlOnJ1bGVzX2NvbmZpZ3MgcmVhZDpob29rcyB1cGRhdGU6aG9va3MgZGVsZXRlOmhvb2tzIGNyZWF0ZTpob29rcyByZWFkOmFjdGlvbnMgdXBkYXRlOmFjdGlvbnMgZGVsZXRlOmFjdGlvbnMgY3JlYXRlOmFjdGlvbnMgcmVhZDplbWFpbF9wcm92aWRlciB1cGRhdGU6ZW1haWxfcHJvdmlkZXIgZGVsZXRlOmVtYWlsX3Byb3ZpZGVyIGNyZWF0ZTplbWFpbF9wcm92aWRlciBibGFja2xpc3Q6dG9rZW5zIHJlYWQ6c3RhdHMgcmVhZDppbnNpZ2h0cyByZWFkOnRlbmFudF9zZXR0aW5ncyB1cGRhdGU6dGVuYW50X3NldHRpbmdzIHJlYWQ6bG9ncyByZWFkOmxvZ3NfdXNlcnMgcmVhZDpzaGllbGRzIGNyZWF0ZTpzaGllbGRzIHVwZGF0ZTpzaGllbGRzIGRlbGV0ZTpzaGllbGRzIHJlYWQ6YW5vbWFseV9ibG9ja3MgZGVsZXRlOmFub21hbHlfYmxvY2tzIHVwZGF0ZTp0cmlnZ2VycyByZWFkOnRyaWdnZXJzIHJlYWQ6Z3JhbnRzIGRlbGV0ZTpncmFudHMgcmVhZDpndWFyZGlhbl9mYWN0b3JzIHVwZGF0ZTpndWFyZGlhbl9mYWN0b3JzIHJlYWQ6Z3VhcmRpYW5fZW5yb2xsbWVudHMgZGVsZXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRzIGNyZWF0ZTpndWFyZGlhbl9lbnJvbGxtZW50X3RpY2tldHMgcmVhZDp1c2VyX2lkcF90b2tlbnMgY3JlYXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgZGVsZXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgcmVhZDpjdXN0b21fZG9tYWlucyBkZWxldGU6Y3VzdG9tX2RvbWFpbnMgY3JlYXRlOmN1c3RvbV9kb21haW5zIHVwZGF0ZTpjdXN0b21fZG9tYWlucyByZWFkOmVtYWlsX3RlbXBsYXRlcyBjcmVhdGU6ZW1haWxfdGVtcGxhdGVzIHVwZGF0ZTplbWFpbF90ZW1wbGF0ZXMgcmVhZDptZmFfcG9saWNpZXMgdXBkYXRlOm1mYV9wb2xpY2llcyByZWFkOnJvbGVzIGNyZWF0ZTpyb2xlcyBkZWxldGU6cm9sZXMgdXBkYXRlOnJvbGVzIHJlYWQ6cHJvbXB0cyB1cGRhdGU6cHJvbXB0cyByZWFkOmJyYW5kaW5nIHVwZGF0ZTpicmFuZGluZyBkZWxldGU6YnJhbmRpbmcgcmVhZDpsb2dfc3RyZWFtcyBjcmVhdGU6bG9nX3N0cmVhbXMgZGVsZXRlOmxvZ19zdHJlYW1zIHVwZGF0ZTpsb2dfc3RyZWFtcyBjcmVhdGU6c2lnbmluZ19rZXlzIHJlYWQ6c2lnbmluZ19rZXlzIHVwZGF0ZTpzaWduaW5nX2tleXMgcmVhZDpsaW1pdHMgdXBkYXRlOmxpbWl0cyBjcmVhdGU6cm9sZV9tZW1iZXJzIHJlYWQ6cm9sZV9tZW1iZXJzIGRlbGV0ZTpyb2xlX21lbWJlcnMgcmVhZDplbnRpdGxlbWVudHMgcmVhZDphdHRhY2tfcHJvdGVjdGlvbiB1cGRhdGU6YXR0YWNrX3Byb3RlY3Rpb24gcmVhZDpvcmdhbml6YXRpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25zIGRlbGV0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25fbWVtYmVycyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJzIGRlbGV0ZTpvcmdhbml6YXRpb25fbWVtYmVycyBjcmVhdGU6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHJlYWQ6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgZGVsZXRlOm9yZ2FuaXphdGlvbl9jb25uZWN0aW9ucyBjcmVhdGU6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgZGVsZXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgY3JlYXRlOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyByZWFkOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyBkZWxldGU6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIHJlYWQ6b3JnYW5pemF0aW9uc19zdW1tYXJ5IGNyZWF0ZTphY3Rpb25zX2xvZ19zZXNzaW9ucyBjcmVhdGU6YXV0aGVudGljYXRpb25fbWV0aG9kcyByZWFkOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgdXBkYXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgZGVsZXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.OmE-dSzuxpbUxZNmlEmBcn7d5a9_KjBnpr64qZZya7l_nlABdoIH_27KJVwwbHqxLzt7nQhmst_l5KkxX37wlczN5bE3YoPOrSjM6fr86VE8dYthNHTE6UThLVeLZBQYIe-BiLoDhXO0QnJfkUrwipVPENMJUFvCOpLA7xzqSu2Lt4my2wfzt7zbh2Yd2f1jVZG6kmAOMfR4EFjxBEPtLy4aEV3gNvpDvj80ItqIRlrZVCWxV689VCCknYvt_zG4g79fnYQ5AnMpi59pE6VYp6Ayuq6SjZxrtCnYObH4F8arOCrO4le-HRC1NhbsAFdD2nGsvFQ_n7yoWHKVi5uTgw' 

select * from Config
 insert into Config values('TweetSearchQuery','https://api.twitter.com/2/tweets/search/recent?')
 insert into Config values('logFilePath','C:\\Users\\User\\source\\Projects\\Semester2\\MidProject\\C#\\CampaignProject\\logs\\Log.txt')
 select VALUE from Config where [KEY]='CONSUMER_KEY' or [KEY]='CONSUMER_SECRET' or
 [KEY]='ACCESS_TOKEN' or [KEY]='ACCESS_TOKEN_SECRET'
 SELECT  VALUE FROM Config WHERE [KEY]='CONSUMER_KEY'
SELECT  VALUE FROM Config WHERE [KEY]='CONSUMER_SECRET'
SELECT  VALUE FROM Config WHERE [KEY]='ACCESS_TOKEN'
SELECT  VALUE FROM Config WHERE [KEY]='ACCESS_TOKEN_SECRET'
SELECT  VALUE from Config where [KEY]='TweetSearchQuery'
select * from Config where [KEY]!='Bearer'



CREATE TABLE "Businesses"(
    "id" INT NOT NULL primary key identity,
    "FullName" NVARCHAR(max) NOT NULL,
    "Email" NVARCHAR(max) NOT NULL,
	"Phone" NVARCHAR(max) NOT NULL,
    "CompanyName" NVARCHAR(max) NOT NULL,
	"BusinessUserID" INT NOT NULL foreign key references Users (id)
);

CREATE TABLE "NonProfits"(
    "id" INT NOT NULL primary key identity,
	"NonProfitUserID" INT NOT NULL foreign key references Users (id),
    "FullName" NVARCHAR(max) NOT NULL,
    "Email" NVARCHAR(max) NOT NULL,
	"Phone" NVARCHAR(max) NOT NULL,
    "OrganizationWebUrl" NVARCHAR(max) NOT NULL,
    "OrganizationName" NVARCHAR(max) NOT NULL,
	"OrganizationDesc" NVARCHAR(max) NOT NULL
);


CREATE TABLE "Campaigns"(
    "CampaignId" INT NOT NULL primary key identity,
	"NonProfitUserID" INT NOT NULL foreign key references NonProfits (id),
    "CampaignName" NVARCHAR(max) NOT NULL,
    "CampaignInfo" NVARCHAR(max) NOT NULL,
    "CampaignHashtag" NVARCHAR(max) NOT NULL,
    "CampaignWebUrl" NVARCHAR(max) NOT NULL,
    "DonationsAmount" DECIMAL(10, 2) NOT NULL

);

SELECT SUM(Earnings) as TotalEarnings FROM Activists;

CREATE TABLE "Activists"(
    "id" INT NOT NULL  primary key identity,
	"ActivistUsersID" INT NOT NULL foreign key references Users (id),
    "FullName" NVARCHAR(max) NOT NULL,
    "Email" NVARCHAR(max) NOT NULL,
    "Address" NVARCHAR(max) NOT NULL,
    "Phone" NVARCHAR(max) NOT NULL,
	"TwitterAcount" NVARCHAR(max) NOT NULL,
    "Earnings" DECIMAL(10, 2) NOT NULL,
	"ChosenProducts" INT NOT NULL,
	"ChosenCampaings" INT NOT NULL);


CREATE TABLE "Products"(
    "id" INT NOT NULL primary key identity,
    "ProductName" NVARCHAR(max) NOT NULL,
    "Price" DECIMAL(10, 2) NOT NULL,
    "BusinessUser" INT NOT NULL foreign key references Businesses (id),
    "Campaign" INT NOT NULL foreign key references Campaigns (CampaignId),
    "IsBought" BIT NOT NULL,
	"IsDelivered" BIT NOT NULL,
    "ActivistBuyerID" INT, --foreign key references Activists (id), 
);

CREATE TABLE "ActiveCampaigns"(
    "id" INT NOT NULL primary key identity,
	"CampaignId" INT NOT NULL,
	"CampaignName" NVARCHAR(max) NOT NULL,
	"CampaignHashtag" NVARCHAR(max) NOT NULL,
	"ActivistUserID" INT,
    "ActivistFullName" NVARCHAR(max) NOT NULL,
);

CREATE TABLE "Tweets"(
    "id" INT NOT NULL primary key identity,
	"TweetId" NVARCHAR(max) NOT NULL,
	"ActivistUserID" INT,
	"CampaignId" INT NOT NULL,
	"CampaignName" NVARCHAR(max) NOT NULL,
	"TweetHashtag" NVARCHAR(max) NOT NULL,
    "ActivistFullName" NVARCHAR(max) NOT NULL,
	"TweetText" NVARCHAR(max) NOT NULL,
	"Date" DATE NOT NULL,
	"Time" TIME NOT NULL,
);

create table Logs (LoggerID int primary key identity, 
EventMessage nvarchar(max), ErrorMessage nvarchar(max), Exception nvarchar(max), LogDate DateTime)
select * from Logs

insert into Logs values('aaa','bbb','cc',GETDATE())


SELECT TOP 1 id,  
    CONVERT(NVARCHAR(10), Date, 126) AS FormattedDate,
    CONVERT(NVARCHAR(8), Time, 108) + 'Z' AS FormattedTime
FROM Tweets
ORDER BY id DESC

SELECT TOP 1 concat(Date,'TT',
    CONVERT(NVARCHAR(8), Time, 108) + 'Z') AS FormattedTime
FROM Tweets
ORDER BY id DESC



insert into Tweets values('1614531797416648705','5','40','Animal & Plant Health Inspection Service','#PlantHealth','hay',
'save the ower plant',
GETDATE(), CONVERT(TIME, DATEADD(second, DATEPART(HOUR, GETDATE())*3600+DATEPART(MINUTE, GETDATE())*60+DATEPART(SECOND, GETDATE()), 0)));
select * from Tweets
drop table Tweets


SELECT p.ProductName, p.Price, p.ActivistBuyerID, a.FullName, a.Address
FROM Products p
INNER JOIN Activists a ON p.ActivistBuyerID = a.id
WHERE p.BusinessUser = 1 AND p.IsBought = 1

select * from Config

select * from Products where BusinessUser=1

insert into Products values('bike',10,1,1,0,0,0)
  insert into Products values('bike2',5,1,5,0,0,0)


CREATE TABLE "Owner"(
    "id" INT NOT NULL primary key identity,
	"OwnerUserID" INT NOT NULL foreign key references Users (id),
    "FullName" NVARCHAR(max) NOT NULL,
    "Email" NVARCHAR(max) NOT NULL,
	"Phone" NVARCHAR(max) NOT NULL
);

  insert into Owner values(1,'hay','hay@ga.co.il',1)

Drop table [dbo].[Businesses]
Drop table [dbo].[NonProfits]
drop table [dbo].[Users]
drop table [dbo].[Owner]
Drop table [dbo].[Products]
Drop table [dbo].[Activists]
Drop table [dbo].[Campaigns]
Drop table ActiveCampaigns

INSERT INTO Users ([UserType]) VALUES ('Admin') SELECT @@IDENTITY

insert into Owner values(2,'NewUser.fullName',' NewUser.email ','NewUser.cellPhone')

select * from Owner
select * from Users
select * from Businesses
select * from NonProfits

select * from ActiveCampaigns
select * from Products
select * from Campaigns
select * from Activists


SELECT AC.*
FROM ActiveCampaigns AC INNER JOIN Activists A ON AC.ActivistBuyerID = A.ActivistUsersID where	A.Email='economy.telhai@gmail.com'

SELECT AC.* FROM ActiveCampaigns AC INNER JOIN Activists A ON
AC.ActivistBuyerID = A.ActivistUsersID where A.Email='economy.telhai@gmail.com'

update Products set IsDelivered=0
UPDATE NonProfits SET FullName ='Hay',[OrganizationWebUrl]='https://www.ausa.org/',[OrganizationName]='ASSOCIATION OF THE UNITED STATES ARMY
' where id=2
UPDATE NonProfits SET OrganizationDesc='They Gave us so much, so lets give them back' where id=2
delete from ActiveCampaigns where CampaignId=11
delete from Products where BusinessUser=1

update Campaigns SET DonationsAmount = DonationsAmount - (SELECT Price from Products where ProductName ='bike') where CampaignId=43 
delete from Products where ProductName ='bike' and BusinessUser=1

update Campaigns SET DonationsAmount =0 DonationsAmount - (SELECT Price from Products where 

select * from ActiveCampaigns where A

update Products 
set IsDelivered=0,IsBought=0
where id=25

update Activists set Earnings = 500
update Activists set Earnings = Earnings + (5 * 2) where id = 2
update Activists set Earnings = Earnings + (5 * 2) where ActivistUsersID = 5

delete from Products where ProductName ='Bicycle' and BusinessUser=1
 update Campaigns SET DonationsAmount = DonationsAmount - (SELECT Price from Products where ProductName ='Bicycle') 


insert into ActiveCampaigns 
values((select CampaignId from Campaigns where CampaignName='aa'),
'aa',(select CampaignHashtag from Campaigns where CampaignName='aa'),(select ActivistUsersID from Activists where Email='economy.telhai@gmail.com'),
(select FullName from Activists where Email='economy.telhai@gmail.com'))


SELECT AC.*, A.TwitterAcount 
FROM ActiveCampaigns AC INNER JOIN Activists A ON AC.ActivistBuyerID = A.ActivistUsersID

SELECT AC.*, A.TwitterAcount FROM ActiveCampaigns AC 
INNER JOIN Activists A ON AC.ActivistBuyerID = A.ActivistUsersID



delete from Campaigns where CampaignName ='aa'

insert into ActiveCampaigns 
select CampaignId, 'aa', CampaignHashtag, ActivistUsersID, FullName
from Campaigns, Activists
where Campaigns.CampaignName='aa' and Activists.Email='economy.telhai@gmail.com'
and not exists (select * from ActiveCampaigns where CampaignId = (select CampaignId from Campaigns where CampaignName='aa')
and Email='economy.telhai@gmail.com')


select * from Products where BusinessUser=" + userID + " and IsBought = 1

UPDATE Campaigns
SET DonationsAmount = 10
WHERE CampaignId = 4;

UPDATE Campaigns
SET DonationsAmount = DonationsAmount + 10
WHERE CampaignId = 3;


select * from Non

insert into Activists values(1,'QQQ','sad@sdf','sss','234',0,0,'sdfs_')

delete from Users where id=1

INSERT INTO Users ([UserType]) VALUES ('Activist') SELECT @@IDENTITY
INSERT INTO Users ([UserType]) VALUES ('Owner') SELECT @@IDENTITY
INSERT INTO Users ([UserType]) VALUES ('Business') SELECT @@IDENTITY
INSERT INTO Users ([UserType]) VALUES ('NonProfit') SELECT @@IDENTITY

insert into Activists values('1','QQQ','sad@sdf','sss','234',0,0,'sdfs_')

insert into NonProfits values(6,'sf','sf@asd66','453','ads','asd','ad')

declare @answer varchar(100)
 if exists (select * from Activists where Email='economy.telhai@gmail.com') begin select @answer = 'true' end else begin select @answer = 'false' end select @answer

 declare @NonID int
 select @NonID = (select NonProfitUserID from NonProfits where Email='roniazulay95@gmail.com')
 insert into Campaigns values(4,'cmapaign1','sf@asd','453','ads',0)

 declare @NonID int
select @NonID = (select id from NonProfits where Email = 'sf@asd66')

insert into Campaigns values(@NonID,'roni','leg','#foot','good',0)

update Campaigns set CampaignName ='qqqq', CampaignInfo='eeee', CampaignHashtag='rrrr',
CampaignWebUrl='url'where CampaignName='qqqq'

update Campaigns set CampaignName ='name', CampaignInfo='fsdf', CampaignHashtag='hashtag',
CampaignWebUrl='url'where CampaignName='name'

declare @answer varchar(100)
 if exists (select * from NonProfits where Email='roniazulay95@gmail.com') 
 begin 
 select @answer = 'true' 
 end 
 else 
 begin 
 select @answer = 'false' 
 end select @answer

 select * from NonProfits
select * from Campaigns

 declare @NonID int
select @NonID = (select id from NonProfits where Email = 'roniazulay95@gmail.com')
select @NonID
insert into Campaigns values(4,'juju','adsf','asd','sf',0)


UPDATE Campaigns
SET CampaignName = 'aa'
WHERE  CampaignId=1;



select id from Businesses where Email='noya.tuizer@gmail.com'
select id from Businesses where  Email='noya.tuizer@gmail.com'

select id from Campaigns where  CampaignName='aa'

insert into Products values('bike','10','1','1','False','False','0')

declare @answer varchar(100)
 if exists (select * from Activists where Email='economy.telhai@gmail.com') begin select @answer = 'true' end
 else begin select @answer = 'false' end select @answer


 select * from Activists where ChosenProducts != 0

 select * from Activists
 select * from NonProfits
select * from Campaigns
select * from Products

update Activists set Earnings = 400 where id=2


select * from Activists where id=(select ActivistBuyerID from Products where IsBought=1)

update Campaigns
set DonationsAmount = DonationsAmount - 1
where CampaignId =(select Campaign from Products where ProductName='red')

select * from Products where Campaign=(select CampaignId from Campaigns where CampaignName='sdf')

 select * from Campaigns where NonProfitUserID=(select id from NonProfits where OrganizationName ='kuku')


 declare @answer varchar(100)
 if exists (select * from Activists where Email='economy.telhai@gmail.com') 
 begin select @answer = 'true' end else begin select @answer = 'false' end select @answer
 select Earnings from Activists where Email='economy.telhai@gmail.com'

select * from Products

UPDATE Activists
SET Earnings = Earnings - 100
where Email='economy.telhai@gmail.com'


UPDATE Products
SET IsBought = 0 , ActivistBuyerID=(select id from Activists where Email='economy.telhai@gmail.com')
where ProductName='red'

UPDATE Products
SET IsBought = 0 , ActivistBuyerID=0



 UPDATE Activists
SET Earnings = 320
where Email='economy.telhai@gmail.com'

 UPDATE Activists
SET Earnings = 220
where Email='nasir@walla.com'

UPDATE Products
SET ActivistBuyerID=0,IsBought=0
where ProductName='red'

UPDATE Activists SET Earnings = Earnings - 100 where Email = 'economy.telhai@gmail.com'UPDATE Products SET IsBought = 1, ActivistBuyerID = 
(select id from Activists where Email = 'economy.telhai@gmail.com') where ProductName = 'red'

select * from Products where BusinessUser=1 and IsBought = 0

select * from Products where Campaign=
(select CampaignId from Campaigns where CampaignName='sdf') and IsBought = 0

select * from Products where ActivistBuyerID=
(select id from Activists where Email = 'economy.telhai@gmail.com') and IsBought = 1 


UPDATE Activists SET Earnings = Earnings - 100 where Email = 'economy.telhai@gmail.com'

UPDATE Products SET IsBought = 1, ActivistBuyerID =
(select id from Activists where Email = 'economy.telhai@gmail.com') where ProductName = 'red'

 update Campaigns set DonationsAmount = DonationsAmount - 100 where CampaignId =
 (select Campaign from Products where ProductName='red')

 UPDATE Activists SET Earnings = Earnings - 10 where Email = 'economy.telhai@gmail.com'UPDATE Products SET IsBought = 1, ActivistBuyerID = (select id from Activists where Email = 'economy.telhai@gmail.com') where ProductName = 'TOMATO'
 update Campaigns set DonationsAmount = DonationsAmount - 10 where CampaignId =(select Campaign from Products where ProductName='TOMATO') update Activists set ChosenProducts = ChosenProducts +1 where FullName='economy.telhai@gmail.com'
