INSERT INTO Users ([UserType]) VALUES ('Activist') SELECT @@IDENTITY
INSERT INTO Users ([UserType]) VALUES ('Owner') SELECT @@IDENTITY
INSERT INTO Users ([UserType]) VALUES ('Business') SELECT @@IDENTITY
INSERT INTO Users ([UserType]) VALUES ('NonProfit') SELECT @@IDENTITY
INSERT INTO Users ([UserType]) VALUES ('NonProfit') SELECT @@IDENTITY

insert into Activists values(1,'QQQ','sad@sdf','sss','234','',0,0,0)

insert into NonProfits values(4,'sf','roniazulay95@gmail.com','453','ads','asd','ad')

insert into NonProfits values(5,'sf','roniazulay95@gmail2.com','453','ads','asd','ad')

 declare @NonID int
select @NonID = (select id from NonProfits where Email = 'roniazulay95@gmail.com')
insert into Campaigns values(1,'Campaigns1','adsf','asd','sf',0)

--declare @NonID int
select @NonID = (select id from NonProfits where Email = 'roniazulay95@gmail2.com')
insert into Campaigns values(1,'Campaigns2','adsf','asd','sf',0)

select * from Owner
select * from Users
select * from Businesses
select * from Activists
select * from NonProfits
select * from Campaigns
select * from Products