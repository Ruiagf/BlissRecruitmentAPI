-- **************************************************************************************
-- Project: Bliss Recruitment API
-- Author: Rui Freitas
-- Date: 2018-03-17
-- **************************************************************************************

go

insert into [dbo].[HealthState] ([Id], [Description]) values (0, 'Service ok')
insert into [dbo].[HealthState] ([Id], [Description]) values (1, 'Service unavailable')
go

insert into [dbo].[Health] ([Id], [State])
values (1, 0)

go
