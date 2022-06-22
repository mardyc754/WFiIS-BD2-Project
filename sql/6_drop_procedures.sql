USE [Project]
GO

DROP PROCEDURE dbo.deleteMenu;
GO

DROP PROCEDURE dbo.deleteCategory;
GO

DROP PROCEDURE dbo.deleteProduct;
GO

DROP PROCEDURE dbo.deleteProductPriceSmall;
GO

DROP PROCEDURE dbo.deleteProductPriceLarge;
GO

DROP PROCEDURE dbo.modifyProductPriceLarge;
GO

DROP PROCEDURE dbo.modifyProductPriceMedium;
GO

DROP PROCEDURE dbo.modifyProductPriceSmall;
GO

DROP PROCEDURE dbo.modifyProductName;
GO

DROP PROCEDURE dbo.modifyCategoryName;
GO

DROP PROCEDURE dbo.addPriceLarge;
GO

DROP PROCEDURE dbo.addPriceSmall;
GO

DROP PROCEDURE dbo.addProduct;
GO

DROP PROCEDURE dbo.addCategory;
GO

DROP PROCEDURE dbo.getProductByPriceInCategory
GO

DROP PROCEDURE dbo.getProductByPrice;
GO

DROP PROCEDURE dbo.getVegetarianProducts;
GO

DROP PROCEDURE dbo.getVegetarianProductsInCategory
GO

DROP PROCEDURE dbo.getProductByName;
GO

DROP PROCEDURE dbo.getProductsFromCategoryByCategoryName
GO

DROP PROCEDURE dbo.getProductsFromCategoryByCategoryID
GO

DROP PROCEDURE dbo.getCategoryByID;
GO

DROP PROCEDURE dbo.getProductByID;
GO

DROP PROCEDURE dbo.getCategoryXMLByID;
GO

DROP PROCEDURE dbo.getProductXMLByID;
GO

DROP PROCEDURE dbo.productsTable;
GO

DROP PROCEDURE dbo.categoriesTable;
GO

DROP PROCEDURE dbo.getProductsFromCategory;
GO

DROP PROCEDURE dbo.getAllCategories;
GO

DROP PROCEDURE dbo.getAllProducts;
GO

DROP FUNCTION dbo.allCategories;
GO

DROP FUNCTION dbo.getAllProductsWithCategories;
GO
