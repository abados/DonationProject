CREATE TABLE "Users"(
    "id" INT NOT NULL primary key identity,
    "UserType" NVARCHAR(max) NOT NULL
);


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
    "ProductID" INT NOT NULL,
    "ProductName" NVARCHAR(max) NOT NULL,
    "Price" DECIMAL(10, 2) NOT NULL,
    "BusinessUser" INT NOT NULL foreign key references Businesses (id),
    "Campaign" INT NOT NULL foreign key references Campaigns (CampaignId),
    "IsBought" BIT NOT NULL,
	"IsDelivered" BIT NOT NULL,
    "ActivistBuyerID" INT foreign key references Activists (id), 
);


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

INSERT INTO Users ([UserType]) VALUES ('Admin') SELECT @@IDENTITY

insert into Owner values(2,'NewUser.fullName',' NewUser.email ','NewUser.cellPhone')

select * from Owner
select * from Users
select * from Businesses
select * from Activists
select * from NonProfits
select * from Campaigns


select * from Non

insert into Activists values('2','QQQ','sad@sdf','sss','234',0,0,'sdfs_')

delete from Users where id=1

INSERT INTO Users ([UserType]) VALUES ('Activist') SELECT @@IDENTITY

insert into Activists values('1','QQQ','sad@sdf','sss','234',0,0,'sdfs_')

insert into NonProfits values('sf','sf@asd','453','ads','asd','ad')

declare @answer varchar(100)
 if exists (select * from Activists where Email='economy.telhai@gmail.com') begin select @answer = 'true' end else begin select @answer = 'false' end select @answer

 declare @NonID int
 select @NonID = (select NonProfitUserID from NonProfits where Email='roniazulay95@gmail.com')
 insert into Campaigns values(@NonID,'sf','sf@asd','453','ads',0)

 declare @NonID int
select @NonID = (select NonProfitUserID from NonProfits where Email = 'roniazulay95@gmail.com')
insert into Campaigns values(@NonID,'brain','leg','#foot','good',0)

update Campaigns set CampaignName ='qqqq', CampaignInfo='eeee', CampaignHashtag='rrrr',
CampaignWebUrl='url'where CampaignName='qqqq'

update Campaigns set CampaignName ='name', CampaignInfo='fsdf', CampaignHashtag='hashtag',
CampaignWebUrl='url'where CampaignName='name'

