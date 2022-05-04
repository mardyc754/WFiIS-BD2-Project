USE [Project]
GO

INSERT INTO [dbo].[ProductCategory]
           ([categoryID],[name])
     VALUES
           (1,'Zupy'),
		   (2,'Pierogi'),
		   (3,'Naleœniki'),
		   (4,'Dania miêsne'),
		   (5,'Napoje ciep³e'),
		   (6,'Napoje zimne'),
		   (7,'Pizza'),
		   (8,'Makaron');
GO

INSERT INTO [dbo].[Product]
           ([productID],[name],[priceSmall],[priceMedium],
		   [priceLarge],[vegetarian],[categoryID])
     VALUES
           (1,'Zupa pomidorowa',NULL, 10, NULL, 1, 1),
		   (2,'Zupa pieczarkowa',NULL, 12, NULL, 1, 1),
		   (3,'Zupa ogórkowa',NULL, 12, NULL, 1, 1),
		   (4,'Rosó³ z makaronem',NULL, 14, NULL, 0, 1),
		   (5,'¯urek',NULL, 15, NULL, 0, 1),
		   (6,'Barszcz czerwony',NULL, 15, NULL, 1, 1),
		   (7,'Krupnik',NULL, 14, NULL, 0, 1),
		   (8,'Pierogi z serem',NULL, 12, NULL, 1, 2),
		   (9,'Pierogi ruskie',NULL, 10, NULL, 1, 2),
		   (10,'Pierogi z miêsem',NULL, 12, NULL, 0, 2),
		   (11,'Pierogi ze szpinakiem',NULL, 13, NULL, 1, 2),
		   (12,'Naleœniki z serem',NULL, 15, NULL, 1, 3),
		   (13,'Naleœniki z d¿emem',NULL, 14, NULL, 1, 3),
		   (14,'Kotlet schabowy',NULL, 12, NULL, 0, 4),
		   (15,'Kotlet z drobiu', NULL, 12, NULL, 0, 4),
		   (16,'Gulasz wieprzowy', NULL, 14, NULL, 0, 4),
		   (17,'Gulasz drobiowy', NULL, 14, NULL, 0, 4),
		   (18,'Kawa', 5, 7.50, 10, NULL, 5),
		   (19,'Herbata', 5, 7.50, 10, NULL, 5),
		   (20,'Kakako', 6, 9, 12, NULL, 5),
		   (21,'Cola', 6, 9, 12, NULL, 6),
		   (22,'Oran¿ada', 6, 9, 12, NULL, 6),
		   (23,'Shake', 10, 15, 20, NULL, 6),
		   (24,'Kompot', 4, 6, 8, NULL, 6),
		   (25,'Margherita', 18, 25, 28, 1, 7),
		   (26,'Capricciosa', 20, 26, 32, 0, 7),
		   (27,'Pepperoni', 21, 27, 33, 0, 7),
		   (28,'Salami', 21, 27, 33, 0, 7),
		   (29,'Hawajska', 23, 30, 37, 0, 7),
		   (30,'Vegetariana', 25, 33, 41, 1, 7),
		   (31,'4 sery', 23, 33, 41, 1, 7),
		   (32,'Spaghetti bolognese', NULL, 22, NULL, 0, 8),
		   (33,'Spaghetti carbonara',NULL, 23, NULL, 0, 8);
GO

