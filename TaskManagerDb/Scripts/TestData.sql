DECLARE @statusId INT,
	@taskId INT,
	@userId INT

IF NOT EXISTS (SELECT * FROM [User] WHERE UserName = 'bhogg')
	INSERT INTO [dbo].[User] ([FirstName], [LastName], [UserName])
		VALUES (N'Boss', N'Hogg', N'bhogg')

IF NOT EXISTS (SELECT * FROM [User] WHERE UserName = 'jbob')
	INSERT INTO [dbo].[User] ([FirstName], [LastName], [UserName])
		VALUES (N'Jim', N'Bob', N'jbob') 

IF NOT EXISTS (SELECT * FROM [User] WHERE UserName = 'jdoe')
	INSERT INTO [dbo].[User] ([FirstName], [LastName], [UserName])
		VALUES (N'John', N'Doe', N'jdoe') 

IF NOT EXISTS (SELECT * FROM dbo.Task WHERE Subject = 'Test Task')
BEGIN
	SELECT TOP 1 @statusId = StatusId FROM Status ORDER BY StatusId;
	SELECT TOP 1 @userId = UserId FROM [User] ORDER BY UserId;

	INSERT INTO dbo.Task(Subject, StartDate, StatusId, CreatedDate, CreatedUserId)
		VALUES('Test Task', getDate(), @statusId, getDate(), @userId);

	set @taskId = SCOPE_IDENTITY();

	INSERT [dbo].[TaskUser] ([TaskId], [UserId])
		VALUES (@taskId, @userId)
END
