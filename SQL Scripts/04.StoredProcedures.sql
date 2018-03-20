-- **************************************************************************************
-- Project: Bliss Recruitment API
-- Author: Rui Freitas
-- Date: 2018-03-17
-- **************************************************************************************
go

if exists(select 1 from sys.objects so inner join sys.schemas ss on so.schema_id = ss.schema_id where so.type = 'p' and so.name = 'sp_GetHealthStatus' and ss.name = 'dbo')
	drop procedure [dbo].[sp_GetHealthStatus]
go
create procedure [dbo].[sp_GetHealthStatus]
as
begin
    set nocount on
    set xact_abort on

	begin transaction
	set transaction isolation level read committed

	select h.[State], hs.Description
	from dbo.Health as h
	     inner join
		 dbo.HealthState as hs
		 on h.State = hs.Id
	where h.Id = 1

	commit
	return 0
end
go

if exists(select 1 from sys.objects so inner join sys.schemas ss on so.schema_id = ss.schema_id where so.type = 'p' and so.name = 'sp_GetQuestionById' and ss.name = 'dbo')
	drop procedure [dbo].[sp_GetQuestionById]
go

create procedure [dbo].[sp_GetQuestionById]
    @Id int
as
    set nocount on
	set xact_abort on

	begin transaction
	set transaction isolation level read committed

	select q.[Id], q.[ImageUrl], q.[ThumbUrl], q.[QuestionText], q.[PublishedDatetime]
	from [dbo].[Question] as q
	where q.[Id] = @Id

    commit
	return 0
go


if exists(select 1 from sys.objects so inner join sys.schemas ss on so.schema_id = ss.schema_id where so.type = 'p' and so.name = 'sp_InsertQuestion' and ss.name = 'dbo')
	drop procedure [dbo].[sp_InsertQuestion]
go
create procedure [dbo].[sp_InsertQuestion]
	@ImageUrl nvarchar(max),
	@ThumbUrl nvarchar(max),
	@QuestionText nvarchar(max),
	@Id int out
as
    set nocount on
	set xact_abort on

	insert into [dbo].[Question] ([ImageUrl], [ThumbUrl], [QuestionText])
	values (@ImageUrl, @ThumbUrl, @QuestionText)

	if @@rowcount <> 1
    begin
        return -1
    end

	set @Id = scope_identity()

	return 0
go


if exists(select 1 from sys.objects so inner join sys.schemas ss on so.schema_id = ss.schema_id where so.type = 'p' and so.name = 'sp_InsertChoice' and ss.name = 'dbo')
	drop procedure [dbo].[sp_InsertChoice]
go
create procedure [dbo].[sp_InsertChoice]
    @QuestionId int,
	@ChoiceText nvarchar(max),
	@Votes int = 0
as
    set nocount on
	set xact_abort on

	insert into [dbo].[Choice] ([QuestionId], [ChoiceText], [Votes])
	values (@QuestionId, @ChoiceText, @Votes)

	if @@rowcount <> 1
    begin
        return -1
    end

	return 0
go

if exists(select 1 from sys.objects so inner join sys.schemas ss on so.schema_id = ss.schema_id where so.type = 'p' and so.name = 'sp_GetAllQuestions' and ss.name = 'dbo')
	drop procedure [dbo].[sp_GetAllQuestions]
go
create procedure [dbo].[sp_GetAllQuestions]
    @MaximumRows int,
	@StartRowIndex int,
	@Text nvarchar(30) = '',
	@Count int output
as
    set nocount on
	set xact_abort on

	begin transaction
	set transaction isolation level read committed

    select [Id], [ImageUrl], [ThumbUrl], [QuestionText], [PublishedDatetime]
	from (
	        select row_number() over (order by q.[Id] asc) as rCount, q.[Id], q.[ImageUrl], q.[ThumbUrl], q.[QuestionText], q.[PublishedDatetime]
	        from [dbo].[Question] as q
	        where q.QuestionText like ('%' + @Text + '%') or exists (
																select c.QuestionId
																from [dbo].[Choice] as c
																where c.QuestionId = q.Id and c.ChoiceText like ('%' + @Text + '%')
															)
			) as t
	where t.rCount > @StartRowIndex and t.rCount <= @StartRowIndex + @maximumRows

	select @Count = count(1)
	from [dbo].[Question] as q
	where q.QuestionText like ('%' + @Text + '%') or exists (
														select c.QuestionId
														from [dbo].[Choice] as c
														where c.QuestionId = q.Id and c.ChoiceText like ('%' + @Text + '%')
													)

    commit
	return 0
go

if exists(select 1 from sys.objects so inner join sys.schemas ss on so.schema_id = ss.schema_id where so.type = 'p' and so.name = 'sp_GetChoicesByQuestionId' and ss.name = 'dbo')
	drop procedure [dbo].[sp_GetChoicesByQuestionId]
go
create procedure [dbo].[sp_GetChoicesByQuestionId]
    @QuestionId int
as
    set nocount on
	set xact_abort on

    select c.Id, c.ChoiceText, c.Votes
    from [dbo].[Choice] as c
    where c.QuestionId = @QuestionId

    return 0
go