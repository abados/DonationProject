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
    "id" INT NOT NULL primary key identity,
	"CampaignID" INT NOT NULL,
    "CampaignName" NVARCHAR(max) NOT NULL,
    "CampaignInfo" NVARCHAR(max) NOT NULL,
    "CampaignHashtag" NVARCHAR(max) NOT NULL,
    "CampaignWebUrl" NVARCHAR(max) NOT NULL,
    "DonationsAmount" DECIMAL(10, 2) NOT NULL,
    "NonProfitUserID" INT NOT NULL foreign key references NonProfits (id)
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
    "Campaign" INT NOT NULL foreign key references Campaigns (id),
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

insert into Activists values('2','QQQ','sad@sdf','sss','234',0,0,'sdfs_')

delete from Users where id=1

INSERT INTO Users ([UserType]) VALUES ('Activist') SELECT @@IDENTITY

insert into Activists values('1','QQQ','sad@sdf','sss','234',0,0,'sdfs_')

insert into NonProfits values('sf','sf@asd','453','ads','asd','ad')