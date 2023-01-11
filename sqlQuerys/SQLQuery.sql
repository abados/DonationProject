CREATE TABLE "Users"(
    "id" INT NOT NULL primary key identity,
    "UserType" NVARCHAR(max) NOT NULL
);



CREATE TABLE "ConfigData"(
    "KEY_TOKEN" NVARCHAR(max) NOT NULL,
    "VALUE" NVARCHAR(max) NOT NULL
);

select * from Config
 insert into Config values('ACCESS_TOKEN_SECRET','bu9nlH5Rf9MdspFM2PrX1JUeTzf2H3DCXDRizFFOBrQAn')
 select VALUE from Config where [KEY]='CONSUMER_KEY' or [KEY]='CONSUMER_SECRET' or
 [KEY]='ACCESS_TOKEN' or [KEY]='ACCESS_TOKEN_SECRET'
 SELECT  VALUE FROM Config WHERE [KEY]='CONSUMER_KEY'
SELECT  VALUE FROM Config WHERE [KEY]='CONSUMER_SECRET'
SELECT  VALUE FROM Config WHERE [KEY]='ACCESS_TOKEN'
SELECT  VALUE FROM Config WHERE [KEY]='ACCESS_TOKEN_SECRET'

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

ALTER TABLE ActiveCampaigns
change COLUMN ActivistBuyerID TO ActivistUserID;

SELECT p.ProductName, p.Price, p.ActivistBuyerID, a.FullName, a.Address
FROM Products p
INNER JOIN Activists a ON p.ActivistBuyerID = a.id
WHERE p.BusinessUser = 1 AND p.IsBought = 1



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
select * from Products
select * from ActiveCampaigns
select * from Campaigns
select * from Activists

update Products 
set IsDelivered=0,IsBought=0
where id=25

update Activists set Earnings = Earnings + (5 * 2) where id = 2
update Activists set Earnings = Earnings + (5 * 2) where ActivistUsersID = 5

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

update Activists set ChosenProducts = ChosenProducts +1 where FullName='nasir'


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
