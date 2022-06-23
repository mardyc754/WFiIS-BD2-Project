USE Project
GO

/* Funkcja zwracająca wszystkie produkty wraz z kategoriami */
CREATE FUNCTION dbo.getAllProductsWithCategories(@xml xml(MenuSchema))
RETURNS TABLE AS 
RETURN(
	SELECT 
	prodCol.value('(./@id)[1]', 'int') as productID,
	prodCol.value('(./Name)[1]', 'nvarchar(max)') as name,
	prodCol.value('(./@vegetarian)[1]', 'bit') as vegetarian,
	prodCol.value('(./Prices/Price[@type="small"])[1]', 'money') as priceSmall,
	prodCol.value('(./Prices/Price[@type="medium"])[1]', 'money') as priceMedium,
	prodCol.value('(./Prices/Price[@type="large"])[1]', 'money') as priceLarge,
	categoryCol.value('./@id', 'int') as categoryID,
	categoryCol.value('./@name', 'nvarchar(100)') as categoryName
	FROM  @xml.nodes('/Menu/Category') catTable(categoryCol)
	CROSS APPLY
	categoryCol.nodes('Product') AS ProdTable(prodCol)
);
GO

/* Procedura zwracająca wszystkie produkty */
CREATE PROCEDURE dbo.getAllProducts AS 
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1 menuXML FROM dbo.ProjectXML);
	SELECT * FROM dbo.getAllProductsWithCategories(@xml);
END;
GO

/* Procedura zwracająca wszystkie kategorie */
CREATE PROCEDURE dbo.getAllCategories AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1 menuXML FROM dbo.ProjectXML);
	SELECT
	categoryCol.value('./@id', 'int') as categoryID,
	categoryCol.value('./@name', 'nvarchar(100)') as categoryName
	FROM  @xml.nodes('/Menu/Category') catTable(categoryCol)
END;
GO

/* Zwraca dane wszystkich produktow z wybranej kategorii po jej ID */
CREATE PROCEDURE dbo.getProductsFromCategory(@categoryID int) AS 
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1 menuXML FROM dbo.ProjectXML);
	SELECT * FROM dbo.getAllProductsWithCategories(@xml)
	WHERE categoryID = @categoryID;
END;
GO

/* Wszystkie węzly z kategoriami */
CREATE PROCEDURE dbo.categoriesTable AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1 menuXML FROM dbo.ProjectXML);
	SELECT col.value('./@id', 'int') as categoryID, col.query('.') AS categories 
	FROM  @xml.nodes('/Menu/Category') T(col);
END;
GO

/* Wszystkie wezly z produktami */
CREATE PROCEDURE dbo.productsTable AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1 menuXML FROM dbo.ProjectXML);
	SELECT col.value('./@id', 'int') as productID, col.query('.') AS Products  
	FROM  @xml.nodes('/Menu/Category/Product') T(col)
END;
GO

/* Zwraca XML produktu po ID */
CREATE PROCEDURE dbo.getProductXMLByID(@id int, @prodXML xml out) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	DECLARE @tempResult TABLE (productID int, productXML xml);
	SET @xml = (SELECT TOP 1 menuXML FROM dbo.ProjectXML);

	INSERT INTO @tempResult 
	SELECT col.value('./@id', 'int') as prodID, col.query('.') AS prod  
	FROM  @xml.nodes('/Menu/Category/Product') T(col);

	SELECT @prodXML = productXML from @tempResult WHERE productID = @id;
END
GO

/* Zwraca XML kategorii po ID */
CREATE PROCEDURE dbo.getCategoryXMLByID(@id int, @categoryXML xml out) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
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
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE productID = @id;
END
GO

/* Zwraca kategorie po ID */
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

/* Zwraca produkt po jego nazwie */
CREATE PROCEDURE dbo.getProductByName(@name nvarchar(max)) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE LOWER(name) like @name;
END;
GO

/* Zwraca wszystkie wegetarianskie produkty w menu */
CREATE PROCEDURE dbo.getVegetarianProducts AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE vegetarian = 1;
END;
GO

/* Zwraca produkty wegetarianskie w podanej kategorii */
CREATE PROCEDURE dbo.getVegetarianProductsInCategory(@categoryID int) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1 menuXML FROM dbo.ProjectXML);
	SELECT * from dbo.getAllProductsWithCategories(@xml)
	WHERE vegetarian = 1 AND categoryID = @categoryID;
END;
GO

/* Zwraca produkty w zadanym przedziale cenowym */
CREATE PROCEDURE dbo.getProductByPrice(@priceMin money, @priceMax money) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
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
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
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

/* Dodaje pusta kategorie do menu */
CREATE PROCEDURE dbo.addCategory(@categoryName nvarchar(100)) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
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
CREATE PROCEDURE dbo.addProduct(@categoryID int, @prodName nvarchar(max), @priceMedium money, @vegetarian bit) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
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
	SET @newProduct.modify(N'insert <Price type="medium">{sql:variable("@priceMedium")}</Price> 
	into (/Product/Prices)[1]');

	UPDATE projXML 
	SET menuXML.modify(N'insert sql:variable("@newProduct") as last into 
	(/Menu/Category[@id=sql:variable("@categoryID")])[1]')
	FROM ( SELECT TOP 1 menuXML FROM dbo.ProjectXML) projXML;
END
GO

/* 
	Dodaje maly rozmiar produktu
	Przed dodaniem sprawdza czy cena za produkt jest mniejsza
	od ceny za sredni produkt
	oraz czy maly rozmiar danego produktu juz istnieje w bazie 
*/
CREATE PROCEDURE dbo.addPriceSmall(@productID int, @priceSmall money) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
	
	DECLARE @existingPriceSmall money;
	SET @existingPriceSmall = (SELECT priceSmall
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@existingPriceSmall IS NOT NULL)
		RAISERROR ('Mały rozmiar produktu już istnieje', 1, 1 );

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);
	
	IF (@priceSmall IS NOT NULL AND @existingPriceSmall IS NULL)
		IF (@priceSmall < @priceMedium)
			UPDATE projXML 
			SET menuXML.modify(N'insert <Price type="small">{sql:variable("@priceSmall")}</Price> 
			as first into 
			(/Menu/Category/Product[@id=sql:variable("@productID")]/Prices)[1]')
			FROM ( SELECT TOP 1 menuXML FROM dbo.ProjectXML) projXML;
		ELSE
			RAISERROR ('Cena za mały rozmiar musi być mniejsza od ceny za średni rozmiar', 1, 1 );
END
GO

/* 
	Dodaje duzy rozmiar produktu
	Przed dodaniem sprawdza czy cena za produkt jest wieksza
	od ceny za sredni produkt
	oraz czy duzy rozmiar danego produktu juz istnieje w bazie 
*/
CREATE PROCEDURE dbo.addPriceLarge(@productID int, @priceLarge money) AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);
	
	DECLARE @existingPriceLarge money;
	SET @existingPriceLarge = (SELECT priceLarge
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@existingPriceLarge IS NOT NULL)
		RAISERROR ('Duży rozmiar produktu już istnieje', 1, 1 );

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium
		from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);
		
	IF (@priceLarge IS NOT NULL AND @existingPriceLarge IS NULL)
		IF (@priceLarge > @priceMedium)
			UPDATE projXML 
			SET menuXML.modify(N'insert <Price type="large">{sql:variable("@priceLarge")}</Price> 
			as last into 
			(/Menu/Category/Product[@id=sql:variable("@productID")]/Prices)[1]')
			FROM ( SELECT TOP 1 menuXML FROM dbo.ProjectXML) projXML;
		ELSE
			RAISERROR ('Cena za duży rozmiar musi być większa od ceny za średni rozmiar', 1, 1 );
END
GO

/* Modyfikuje nazwe kategorii */
CREATE PROCEDURE dbo.modifyCategoryName(@newName nvarchar(100), @categoryID int) 
AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);

	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(/Menu/Category[@id=sql:variable("@categoryID")]/@name)[1] 
	with (sql:variable("@newName"))') FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje nazwe produktu */
CREATE PROCEDURE dbo.modifyProductName(@newName nvarchar(max), @productID int) 
AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);

	DECLARE @oldName nvarchar(max);
	SET @oldName = (SELECT name from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);
	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Name)[1] with sql:variable("@newName")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje cene za maly rozmiar */
CREATE PROCEDURE dbo.modifyProductPriceSmall(@newPriceSmall money, @productID int) 
AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@newPriceSmall >= @priceMedium)
		RAISERROR ('Cena za mały rozmiar musi być mniejsza od ceny za średni rozmiar', 1, 1 );

	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Prices/Price[@type="small"])[1] with sql:variable("@newPriceSmall")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje cene za sredni rozmiar */
CREATE PROCEDURE dbo.modifyProductPriceMedium(@newPriceMedium money, @productID int) 
AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);

	DECLARE @priceSmall money, @priceLarge money;
	SELECT @priceSmall = (priceSmall), @priceLarge = (priceLarge) from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID;

	IF (@priceLarge IS NOT NULL AND @priceSmall IS NOT NULL AND 
		(@newPriceMedium >= @priceLarge OR @newPriceMedium <= @priceSmall))
		RAISERROR (N'Cena za średni rozmiar musi być większa od ceny za mały rozmiar oraz mniejsza od ceny za duży rozmiar', 1, 1 );
	
	IF (@priceSmall IS NOT NULL AND @priceLarge IS NULL AND (@newPriceMedium <= @priceSmall))
		RAISERROR (N'Cena za średni rozmiar musi być większa od ceny za mały rozmiar', 1, 1 );

	IF (@priceLarge IS NOT NULL AND @priceSmall IS NULL AND (@newPriceMedium >= @priceLarge))
		RAISERROR (N'Cena za średni rozmiar musi być mniejsza od ceny za duży rozmiar', 1, 1 );
	
	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Prices/Price[@type="medium"])[1] with sql:variable("@newPriceMedium")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* Modyfikuje cene za duzy rozmiar */
CREATE PROCEDURE dbo.modifyProductPriceLarge(@newPriceLarge money, @productID int) 
AS
BEGIN
	DECLARE @xml xml(MenuSchema);
	SET @xml = (SELECT TOP 1  menuXML FROM dbo.ProjectXML);

	DECLARE @priceMedium money;
	SET @priceMedium = (SELECT priceMedium from dbo.getAllProductsWithCategories(@xml) WHERE productID = @productID);

	IF (@newPriceLarge <= @priceMedium)
		RAISERROR ('Cena za duży rozmiar musi być większa od ceny za średni rozmiar', 1, 1 );

	UPDATE projXML 
	SET menuXML.modify(N'replace value of 
	(./Menu/Category/Product[@id=sql:variable("@productID")]/Prices/Price[@type="large"])[1] with sql:variable("@newPriceLarge")') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa cene za najwiekszy rozmiar */
CREATE PROCEDURE dbo.deleteProductPriceLarge(@productID int) AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/Category/Product[@id=sql:variable("@productID")]/Prices/Price[@type="large"])[1]') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa cene za najmniejszy rozmiar */
CREATE PROCEDURE dbo.deleteProductPriceSmall(@productID int) AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/Category/Product[@id=sql:variable("@productID")]/Prices/Price[@type="small"])[1]') 
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

/* usuwa kategorie o podanym ID */
CREATE PROCEDURE dbo.deleteCategory(@categoryID int) AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/Category[@id=sql:variable("@categoryID")])[1]') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO

/* usuwa cale menu */
CREATE PROCEDURE dbo.deleteMenu AS
BEGIN
	UPDATE projXML 
	SET menuXML.modify(N'delete (/Menu/*)') 
	FROM ( select TOP 1 menuXML from dbo.ProjectXML) projXML;
END
GO
