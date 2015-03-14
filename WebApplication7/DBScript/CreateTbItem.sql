CREATE TABLE [dbo].[tbItem](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Weight] [decimal](18, 0) NULL,
	[Dimensions] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[Show] [bit] NOT NULL CONSTRAINT [DF_tbItem_Show]  DEFAULT ((1)),
 CONSTRAINT [PK_tbItem] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

SET IDENTITY_INSERT tbItem ON

Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (1,'Olive oil',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (3,'Olive Tree',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (4,'Aqiqa',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (5,'AL-AQSA FOOTBALL CUP',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (6,'Absent Justice',80,'7 DVDs','1 package of 7 DVDs','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (8,'Charity Dinner',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (9,'Olive Oil-Sakhrah',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (10,'Olive Oil-Zaytoun',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (11,'Pickled Olives',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (12,'Pickled Chillies',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (13,'Flashing Foam Stick',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (14,'Fibre Optic Torch',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (15,'Mini Spinner',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (16,'Flashing Mouth',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (17,'Glow Bracelets',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (18,'Light Sabre Sword',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (19,'Flashing Windmill',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (21,'Thermo Mugs',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (22,'Curvy ballpens',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (23,'',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (24,'Projects',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (25,'Water Project',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (26,'Emergency Gaza Flood Appeal',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (27,'Qurbani Appeal',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (28,'Gaza Emergency Appeal 09',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (29,'Gaza Football Cup',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (30,'Sakhrah Olive oil',null,null,' 1 bottle','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (32,'Al-Aqsa Mosque Project',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (33,'GAZA Convay Appeal 09',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (34,'Sacrifice/Thabeeha',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (35,'Fidya',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (36,'Ramadan 2009',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (37,'Sister''s Iftaar 09',null,null,'Ticket','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (38,'Kaffarah',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (39,'Qurbani Appeal 09',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (41,'FR Events',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (42,'Gaza 1 Year Remembering',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (43,'Gaza Flood 2010',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (44,'Al Aqsa Football Tournament 2010',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (45,'Al-aqsa Charity Cup 2010',null,null,'al-aqsa charity cup 2010','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (46,'Medical Appeal Convy May 2010',null,null,'Medical Appeal Convy May 2010','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (47,'Emergency Gaza Medical Appeal June 2010',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (48,'Moon Valley Dates',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (49,'Ramadan Aid',null,null,'Ramadan Aid 2010','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (50,'Clean Water Project',null,null,'Clean Water Supply Aid 2010','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (52,'Educational Aid',null,null,'Educational Aid 2010','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (54,'Medical Aid',null,null,'Medical Aid 2010','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (55,'Qurbani General Package',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (56,'Kettering Town FC Shirt',null,null,'Kettering Town FC Project','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (57,'Community Development',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (58,'Medical Appeal April 2011',null,null,'Medical Appeal April 2011','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (59,'Connect: Food Aid',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (60,'Connect',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (61,'Connect 2',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (62,'Connect 3',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (63,'Qurbani appeal 2011',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (64,'Fidya',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (65,'Kaffarah',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (66,'Qurbani 2011',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (67,'Pal Products',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (68,'Children of Palestine',null,null,'Package','true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (69,'Mending Hope',null,null,'Package','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (70,'Liberating Life',null,null,'Package','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (71,'Ramadan',null,null,'Package','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (72,'Eid Al Fitr',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (73,'Education Aid',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (74,'Eid Al-Adha',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (75,'Gaza Emergency Campaign',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (76,'Gaza Rebuilding Campaign',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (77,'Raise for Life Birmingham',null,null,'Birmingham Initiative 2012','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (79,'Chance To Live',null,null,'Medical Aid 2013','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (81,'Nakba 6th Charity Dinner 2013',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (82,'Food Parcel',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (83,'Ramadan- Refugees',null,null,'Ramadan 2013','false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (108,'Medical Aid',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (110,'Food Aid',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (111,'Water Aid',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (113,'Agriculture Aid',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (114,'Where Most Needed',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (115,'Live Appeal 1 (Ramadan)',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (116,'Live Appeal 2 (Ramadan)',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (117,'Live Appeal 3 (Ramadan)',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (118,'Back 2 School 2013',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (119,'Eid Al Adha 2013',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (120,'Qurbani live appeal 2013',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (121,'Interpal E-store',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (122,'GPU Food Parcel',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (123,'GPU Olive Tree',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (124,'Winter Aid 2014',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (125,'Emergency Live Appeal (Winter Aid 2014)',null,null,null,'false');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (126,'Refugee Crisis',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (127,'Nakba Tickets',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (128,'Refugee Relief',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (129,'Nakbah LA 2014',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (130,'Fidya & Kaffarah',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (131,'Feeding Futures',null,null,'Ramadan 2014','true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (132,'Welcoming Ramadan LA',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (133,'Ramadan LA 2',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (135,'Alhiwar Liveappeal',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (136,'Radio Live Appeal',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (137,'Child Sponsorship',null,null,null,'true');
Insert into tbItem ("ItemID","Name","Weight","Dimensions","Description","Show") values (138,'Ramadan LA 3',null,null,null,'true');

SET IDENTITY_INSERT tbItem OFF