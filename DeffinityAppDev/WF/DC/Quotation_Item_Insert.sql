GO
alter table DC.QuotationItems
add PolicyID int , PolicyNotes Varchar(max)
GO
alter proc  [dbo].[Quotation_Item_Insert]      
@ServiceID int,      
@IncidentID int,      
@QTY int,      
@ServiceTypeID int, 
@Type varchar(20)= 'FLS',     
@OutVal int output,
@ServiceDescription nvarchar(max)='',
@FixedRateTypeID int = 0,
@cost float = 0,
@VAT float = 0,
@Option int =0,
@Notes nvarchar(max) ='',
@Image uniqueidentifier ='00000000-0000-0000-0000-000000000000',
@PolicyID int =0,
@PolicyNotes nvarchar(max)=''
as      
declare @SellingPrice float      
declare @UnitConsumption float    
set @OutVal = 0    

if(@ServiceID = 0 and @ServiceDescription <> '')
Begin

if((select count(*) from DC.QuotationItems where lower(ServiceDescription) = lower(@ServiceDescription) and CallidID =@IncidentID and QuotationOptionID = @Option) =0)      
Begin  
set @SellingPrice = @cost
Insert into DC.QuotationItems(ServiceID,CallidID,QTY,SellingPrice,ServiceTypeID,Type,ServiceDescription,FixedRateTypeID,VAT,QuotationOptionID,Notes,Image,PolicyID,PolicyNotes)      
values (@ServiceID,@IncidentID,@QTY,@SellingPrice,@ServiceTypeID,@Type,@ServiceDescription,@FixedRateTypeID,@VAT,@Option,@Notes,@Image,@PolicyID,@PolicyNotes)   
set @OutVal = 1
End
else
Begin
set @OutVal = 2      
End

End

Else
  
if((select count(*) from DC.QuotationItems where serviceID = @ServiceID and CallidID =@IncidentID and QuotationOptionID = @Option) =0)      
Begin    
  
select @SellingPrice=sp from v_ShopItems_vendor where ID= @ServiceID and Type = @ServiceTypeID      
select @UnitConsumption=0 from v_ShopItems_vendor where ID= @ServiceID and Type = @ServiceTypeID     
Insert into DC.QuotationItems(ServiceID,CallidID,QTY,SellingPrice,ServiceTypeID,Type,ServiceDescription,FixedRateTypeID,VAT,QuotationOptionID,Notes,Image,PolicyID,PolicyNotes)      
values (@ServiceID,@IncidentID,@QTY,@SellingPrice,@ServiceTypeID,@Type,@ServiceDescription,@FixedRateTypeID,@VAT,@Option,@Notes,@Image,@PolicyID,@PolicyNotes)       
set @OutVal = 1      
End      
else 
Begin      
set @OutVal = 2      
End  




