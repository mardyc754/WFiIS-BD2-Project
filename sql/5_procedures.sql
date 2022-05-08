USE Project
GO

/* Funkcja zwracaj¹ca wszystkie produkty wraz z kategoriami */
CREATE FUNCTION dbo.getAllProductsWithCategories(@xml xml)
RETURNS TABLE AS 
RETURN(
	SELECT 
	prodCol.value('(./@id)[1]', 'int') as productID,
	prodCol.value('(./Name)[1]', 'nvarchar(max)') as name,
	prodCol.value('(./@vegetarian)[1]', 'bit') as vegetarian,
	prodCol.value('(./Prices/PriceSmall)[1]', 'money') as priceSmall,
	prodCol.value('(./Prices/PriceMedium)[1]', 'money') as priceMedium,
	prodCol.value('(./Prices/PriceLarge)[1]', 'money') as priceLarge,
	categoryCol.value('./@id', 'int') as categoryID,
	categoryCol.value('./@name', 'nvarchar(100)') as categoryName
	FROM  @xml.nodes('/Menu/Category') catTable(categoryCol)
	CROSS APPLY
	categoryCol.nodes('Product') AS ProdTable(prodCol)
);
GO

/* Wszystkie wêz³y z kategoriami */
CREATE PROCEDURE dbo.categoriesTable AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT col.value('./@id', 'int') as categoryID, col.query('.') AS categories 
	FROM  @xml.nodes('/Menu/Category') T(col);
END;
GO

/* Wszystkie wêz³y z produktami */
CREATE PROCEDURE dbo.productsTable AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT col.value('./@id', 'int') as productID, col.query('.') AS Products  
	FROM  @xml.nodes('/Menu/Category/Product') T(col)
END;
GO

/* Zwraca XML produktu po ID */
CREATE PROCEDURE dbo.getProductXMLByID(@id int, @prodXML xml out) AS
BEGIN
	DECLARE @xml xml;
	DECLARE @tempResult TABLE (productID int, productXML xml);
	SET @xml = (SELECT * FROM dbo.ProjectXML);

	INSERT INTO @tempResult 
	SELECT col.value('./@id', 'int') as prodID, col.query('.') AS prod  
	FROM  @xml.nodes('/Menu/Category/Product') T(col);

	SELECT @prodXML = productXML from @tempResult WHERE productID = @id;
END
GO

/* Zwraca XML kategorii po ID */
CREATE PROCEDURE dbo.getCategoryXMLByID(@id int, @categoryXML xml out) AS
BEGIN
	DECLARE @xml xml;
	DECLARE @tempResult TABLE (categoryID int, categoryXML xml);
	SET @xml = (SELECT * FROM dbo.ProjectXML);

	INSERT INTO @tempResult 
	SELECT col.value('./@id', 'int') as catID, col.query('.') AS category  
	FROM  @xml.nodes('/Menu/Category') T(col);

	SELECT @categoryXML = categoryXML from @tempResult WHERE categoryID = @id;
END
GO

/* Zwraca produkt po ID */
CREATE PROCEDURE dbo.getProductByID(@id int) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE productID = @id;
END

/* Zwraca kategoriê po ID */
CREATE PROCEDURE dbo.getCategoryByID(@id int) AS
BEGIN
	DECLARE @categoryXML xml;
	EXEC dbo.getCategoryXMLByID @id, @categoryXML out;

	SELECT 
	col.value('./@id', 'int') as categoryID, 
	col.value('./@name', 'nvarchar(100)') as Name 
	FROM  @categoryXML.nodes('./Category') T(col);
END
GO

/* Zwraca dane produktów z wybranej kategorii po jej ID */
CREATE PROCEDURE dbo.getProductsFromCategoryByCategoryID(@id int) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE categoryID = @id;
END
GO

/* Zwraca dane produktów z wybranej kategorii po nazwie pasuj¹cej do wzorca */
CREATE PROCEDURE dbo.getProductsFromCategoryByCategoryName(@name nvarchar(100)) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE categoryName like @name;
END
GO

/* Zwraca produkt po jego nazwie */
CREATE PROCEDURE dbo.getProductByName(@name nvarchar(max)) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE name like @name;
END;
GO

/* Zwraca wszystkie wegetariañskie produkty w menu */
CREATE PROCEDURE dbo.getVegetarianProducts AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE vegetarian = 1;
END;
GO

/* Zwraca produkty wegetariañskie w podanej kategorii */
CREATE PROCEDURE dbo.getVegetarianProductsInCategory(@categoryID int) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE vegetarian = 1 AND categoryID = @categoryID;
END;
GO

/* Zwraca produkty w zadanym przedziale cenowym */
CREATE PROCEDURE dbo.getProductByPrice(@priceMin money, @priceMax money) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	IF (@priceMIN IS NOT NULL AND @priceMax IS NOT NULL)
		SELECT * from dbo.getAllProductsWithCategories(@xml)
		WHERE 
		(@priceMin <= priceLarge AND @priceMax >= priceSmall)
		OR
		(@priceMin <= priceMedium AND @priceMax >= priceMedium);
	ELSE
		IF (@priceMin IS NULL AND @priceMax IS NOT NULL) 
			SELECT * from dbo.getAllProductsWithCategories(@xml)
			WHERE  @priceMax >= priceSmall OR @priceMax >= priceMedium;
		ELSE
			SELECT * from dbo.getAllProductsWithCategories(@xml)
			WHERE @priceMin <= priceLarge OR @priceMin <= priceMedium;
END
GO

/* Zwraca produkty w zadanym przedziale cenowym w wybranej kategorii */
CREATE PROCEDURE dbo.getProductByPriceInCategory(@categoryID int, @priceMin money, @priceMax money) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	IF (@priceMIN IS NOT NULL AND @priceMax IS NOT NULL)
		SELECT * from dbo.getAllProductsWithCategories(@xml)
		WHERE 
		categoryID = @categoryID AND (
		(@priceMin <= priceLarge AND @priceMax >= priceSmall)
		OR
		(@priceMin <= priceMedium AND @priceMax >= priceMedium));
	ELSE
		IF (@priceMin IS NULL AND @priceMax IS NOT NULL) 
			SELECT * from dbo.getAllProductsWithCategories(@xml)
			WHERE  categoryID = @categoryID AND 
			(@priceMax >= priceSmall OR @priceMax >= priceMedium);
		ELSE
			SELECT * from dbo.getAllProductsWithCategories(@xml)
			WHERE  categoryID = @categoryID AND
			(@priceMin <= priceLarge OR @priceMin <= priceMedium);
END
GO

/* Dodaje pust¹ kategoriê do menu */
CREATE PROCEDURE dbo.addCategory(@categoryName nvarchar(100)) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	DECLARE @newID int;
	SET @newID = (SELECT max(categoryID)+1 from dbo.getAllProductsWithCategories(@xml));

	DECLARE @newCategory XML = '<Category />'
	SET @newCategory.modify(
    'insert 
    (
		attribute id {sql:variable("@newID")},
		attribute name {sql:variable("@categoryName")}
	)
    into (/Category)[1]');

	UPDATE projXML 
	SET menuXML.modify('insert sql:variable("@newCategory") as last into (/Menu)[1]')
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Dodaje produkt do xml */
CREATE PROCEDURE dbo.addProduct(@categoryID int, @prodName nvarchar(max), @vegetarian bit, @priceMedium money) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	DECLARE @newID int;
	SET @newID = (SELECT max(productID)+1 from dbo.getAllProductsWithCategories(@xml));

	DECLARE @newProduct XML = '<Product />'
	SET @newProduct.modify(N'insert (attribute id {sql:variable("@newID")})
    into (/Product)[1]');

	IF @vegetarian IS NOT NULL
		SET @newProduct.modify(N'insert (attribute vegetarian {sql:variable("@vegetarian")})
		into (/Product)[1]');
	
	SET @newProduct.modify('insert <Name>{sql:variable("@prodName")}</Name> into (/Product)[1]');
	SET @newProduct.modify('insert <Prices /> into (/Product)[1]');
	SET @newProduct.modify(N'insert <PriceMedium>{sql:variable("@priceMedium")}</PriceMedium> 
	into (/Product/Prices)[1]');

	UPDATE projXML 
	SET menuXML.modify(N'insert sql:variable("@newProduct") as last into 
	(/Menu/Category[@id=sql:variable("@categoryID")])[1]')
	FROM ( SELECT TOP 1 menuXML FROM dbo.ProjectXML) projXML;
END
GO

/* 
	Dodaje ma³y rozmiar produktu
	Przed dodaniem sprawdza czy cena za produkt jest mniejsza
	od ceny za œredni produkt
	oraz czy ma³y rozmiar danego produktu ju¿ istnieje w bazie 
*/
CREATE PROCEDURE dbo.addPriceSmall(@productID int, @priceSmall money) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	
	DECLARE @existingPriceSmall money;
	SET @existingPriceSmall = (SELECT priceSmall
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@existingPriceSmall IS NOT NULL)
		RAISERROR ('Ma³y rozmiar produktu ju¿ istnieje', 1, 1 );

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);
	
	IF (@priceSmall IS NOT NULL AND @existingPriceSmall IS NULL)
		IF (@priceSmall < @priceMedium)
			UPDATE projXML 
			SET menuXML.modify(N'insert <PriceSmall>{sql:variable("@priceSmall")}</PriceSmall> 
			as first into 
			(/Menu/Category/Product[@id=sql:variable("@productID")]/Prices)[1]')
			FROM ( SELECT TOP 1 menuXML FROM dbo.ProjectXML) projXML;
		ELSE
			RAISERROR ('Cena za ma³y rozmiar musi byæ mniejsza od ceny za œredni rozmiar', 1, 1 );
END
GO

/* 
	Dodaje du¿y rozmiar produktu
	Przed dodaniem sprawdza czy cena za produkt jest wiêksza
	od ceny za œredni produkt
	oraz czy du¿y rozmiar danego produktu ju¿ istnieje w bazie 
*/
CREATE PROCEDURE dbo.addPriceLarge(@productID int, @priceLarge money) AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);
	
	DECLARE @existingPriceLarge money;
	SET @existingPriceLarge = (SELECT priceLarge
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@existingPriceLarge IS NOT NULL)
		RAISERROR ('Du¿y rozmiar produktu ju¿ istnieje', 1, 1 );

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);
		
	IF (@priceLarge IS NOT NULL AND @existingPriceLarge IS NULL)
		IF (@priceLarge > @priceMedium)
			UPDATE projXML 
			SET menuXML.modify(N'insert <PriceLarge>{sql:variable("@priceLarge")}</PriceLarge> 
			as last into 
			(/Menu/Category/Product[@id=sql:variable("@productID")]/Prices)[1]')
			FROM ( SELECT TOP 1 menuXML FROM dbo.ProjectXML) projXML;
		ELSE
			RAISERROR ('Cena za du¿y rozmiar musi byæ wiêksza od ceny za œredni rozmiar', 1, 1 );
END
GO

/* Modyfikuje nazwê kategorii */
CREATE PROCEDURE dbo.modifyCategoryName(@newName nvarchar(100), @categoryID int) 
AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);

	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(/Menu/Category[@id=sql:variable("@categoryID")]/@name)[1] 
	with (sql:variable("@newName"))') FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje nazwê produktu */
CREATE PROCEDURE dbo.modifyProductName(@newName nvarchar(max), @productID int) 
AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);

	DECLARE @oldName nvarchar(max);
	SET @oldName = (SELECT name from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);
	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Name/text())[1] with sql:variable("@newName")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje cenê za ma³y rozmiar */
CREATE PROCEDURE dbo.modifyProductPriceSmall(@newPriceSmall money, @productID int) 
AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@newPriceSmall >= @priceMedium)
		RAISERROR ('Cena za ma³y rozmiar musi byæ mniejsza od ceny za œredni rozmiar', 1, 1 );

	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Prices/PriceSmall/text())[1] with sql:variable("@newPriceSmall")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje cenê za œredni rozmiar */
CREATE PROCEDURE dbo.modifyProductPriceMedium(@newPriceMedium money, @productID int) 
AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);

	DECLARE @priceSmall money, @priceLarge money;
	SELECT @priceSmall = (priceSmall), @priceLarge = (priceLarge) from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID;

	IF (@priceLarge IS NOT NULL AND @priceSmall IS NOT NULL AND 
		(@newPriceMedium >= @priceLarge OR @newPriceMedium <= @priceSmall))
		RAISERROR (N'Cena za œredni rozmiar musi byæ wiêksza od ceny za ma³y rozmiar oraz mniejsza od ceny za du¿y rozmiar', 1, 1 );
	
	IF (@priceSmall IS NOT NULL AND @priceLarge IS NULL AND (@newPriceMedium <= @priceSmall))
		RAISERROR (N'Cena za œredni rozmiar musi byæ wiêksza od ceny za ma³y rozmiar', 1, 1 );

	IF (@priceLarge IS NOT NULL AND @priceSmall IS NULL AND (@newPriceMedium >= @priceLarge))
		RAISERROR (N'Cena za œredni rozmiar musi byæ mniejsza od ceny za du¿y rozmiar', 1, 1 );
	
	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Prices/PriceMedium/text())[1] with sql:variable("@newPriceMedium")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje cenê za du¿y rozmiar */
CREATE PROCEDURE dbo.modifyProductPriceLarge(@newPriceLarge money, @productID int) 
AS
BEGIN
	DECLARE @xml xml;
	SET @xml = (SELECT * FROM dbo.ProjectXML);

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@newPriceLarge <= @priceMedium)
		RAISERROR ('Cena za du¿y rozmiar musi byæ wiêksza od ceny za œredni rozmiar', 1, 1 );

	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Prices/PriceLarge/text())[1] with sql:variable("@newPriceLarge")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa cenê za najwiêkszy rozmiar */
CREATE PROCEDURE dbo.deleteProductPriceLarge(@productID int) AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/Category/Product[@id=sql:variable("@productID")]/Prices/PriceLarge)[1]') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa cenê za najmniejszy rozmiar */
CREATE PROCEDURE dbo.deleteProductPriceSmall(@productID int) AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/Category/Product[@id=sql:variable("@productID")]/Prices/PriceSmall)[1]') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa produkt o podanym ID */
CREATE PROCEDURE dbo.deleteProduct(@productID int) AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/Category/Product[@id=sql:variable("@productID")])[1]') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa kategoriê o podanym ID */
CREATE PROCEDURE dbo.deleteCategory(@productID int) AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/Category[@id=sql:variable("@productID")])[1]') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa ca³e menu */
CREATE PROCEDURE dbo.deleteMenu AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/*)') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO
