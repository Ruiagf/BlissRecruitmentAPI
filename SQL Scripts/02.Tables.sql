-- **************************************************************************************
-- Project: Bliss Recruitment API
-- Author: Rui Freitas
-- Date: 2018-03-17
-- **************************************************************************************

go
create table [dbo].[Question]
(
	[Id]                 [int] not null identity(1,1) constraint [PK_dbo_Question] primary key clustered ([Id] asc),
	[ImageUrl] 			 [nvarchar](max) not null,
	[ThumbUrl] 		     [nvarchar](max) not null,
	[QuestionText]       [nvarchar](max) not null,
	[PublishedDatetime]  [datetime] not null constraint [DF_dbo_Question_PublishedDatetime] default(getdate()),
)

go
create table [dbo].[Choice]
(
	[Id] 				[int] 			 not null	identity(1,1),
	[QuestionId]	    [int]			 not null    constraint [FK_dbo_Choice_dbo_Question]	foreign key references [dbo].[Question] ([Id]),
	[ChoiceText] 		[nvarchar](max) not null,
	[Votes]	            [int]			 not null,
	[CreateDatetime] 	[datetime] 	 not null constraint [DF_dbo_Choice_CreateDatetime] 	default(getdate()),
	constraint [PK_dbo_Choice] primary key clustered ([QuestionId], [Id] asc)
)

go

create table [dbo].[HealthState]
(
	[Id]			[smallint] 		not null constraint [PK_dbo_HealthState] primary key clustered ([Id] asc),
	[Description]   [nvarchar](max) not null
)

go
create table [dbo].[Health]
(
	[Id]	[int]	   not null constraint [PK_dbo_Health] primary key clustered ([Id] asc),
	[State] [smallint] not null constraint [FK_dbo_Health_dbo_HealthState] foreign key references [dbo].[HealthState] 
)

