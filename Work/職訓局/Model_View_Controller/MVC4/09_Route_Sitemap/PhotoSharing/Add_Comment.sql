create proc Add_Comment
	@username nvarchar(100),
	@subject nvarchar(250),
	@body nvarchar(max),
	@photoid bigint
as
begin

	insert into Comments(username, [Subject], body, PhotoID)
	values(@username,@subject,@body,@photoid)
end