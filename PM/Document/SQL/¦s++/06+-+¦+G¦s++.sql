
--��Դ����
BEGIN TRY
	BEGIN TRAN
		DELETE Res_Price
		DELETE Res_PriceType WHERE PriceTypeId NOT IN('192340F1-08E3-4B32-B65D-75E785062D05', 'ABC233A8-6C1E-4200-91EC-9223DBCDE390')
		DELETE Res_Resource
        DELETE Res_Attribute WHERE AttributeId NOT IN ('FF747819-77DE-4670-A4F5-8FED9A509A70', 'FF747819-77DE-4670-A4F5-8FED9A509A71')
		DELETE Res_ResourceType WHERE ResourceTypeId NOT IN ('6A1A7050-1F92-4291-B932-43E1DFCE6E90','6A1A7050-1F92-4291-B932-43E1DFCE6E91','6A1A7050-1F92-4291-B932-43E1DFCE6E92')
		DELETE Res_Unit    
		DELETE Res_TemplateItem  
		DELETE Res_Template

	COMMIT TRAN 
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE()
	ROLLBACK TRAN
END CATCH
GO
