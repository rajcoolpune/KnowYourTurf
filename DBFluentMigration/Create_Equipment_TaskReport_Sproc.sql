
/****** Object:  StoredProcedure [dbo].[EquipmentTaskReport]    Script Date: 2/8/2013 11:02:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[EquipmentTaskReport] 
	@EquipmentId int,
	@StartDate	DateTime,
	@EndDate	DateTime,
	@Complete	bit,
	@EmployeeId	int,
	@TaskTypeId	int,
	@ClientId	int
AS
BEGIN
	SET NOCOUNT ON;
select 
e.Name as EquipmentName,
tt.Name as TaskType,
et.ScheduledDate,
et.ActualTimeSpent--,
--p.name as PartName
 from EquipmentTask as et left join Equipment as e on e.EntityId = et.Equipment_id
 left join TaskType as tt on et.TaskType_id = tt.EntityId
where et.ClientId = @ClientId
	  AND @EquipmentId = 0 or et.Equipment_id = @EquipmentId
	  AND @StartDate = CAST('1800-01-01' as Date) AND et.ScheduledDate = et.ScheduledDate
	  OR 
	  et.ScheduledDate >= @StartDate
	  AND
	  @EndDate = CAST('1800-01-01' as Date) AND et.ScheduledDate = et.ScheduledDate
	  OR 
	  et.ScheduledDate <= @EndDate  
	  AND
	  @TaskTypeId = 0 or @TaskTypeId = et.TaskType_Id
	  AND
	  et.Complete = @Complete
END
