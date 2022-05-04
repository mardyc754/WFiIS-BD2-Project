USE Project
GO

-- Zastanowiæ siê czy nie konsekwentniej by³oby daæ nazwê produktu
-- jako atrybut do Product zamiast osobnego tagu

DECLARE @xml xml;

SET @xml = (
	SELECT
	C.categoryID as [@id],
	C.name as [@name],
	(
		SELECT
		P.productID as [@id],  
		P.vegetarian as [@vegetarian],
		P.name  AS [Name],
		P.priceSmall as [Prices/PriceSmall],
		P.priceMedium as [Prices/PriceMedium],
		P.priceLarge as [Prices/PriceLarge]
		FROM dbo.Product P 
		WHERE P.categoryID = C.categoryID
		FOR XML PATH('Product'), type
	)
	FROM dbo.ProductCategory C
	FOR XML PATH ('Category'), ROOT('Menu')
);

INSERT INTO dbo.ProjectXML (menuXML) VALUES (@xml);

