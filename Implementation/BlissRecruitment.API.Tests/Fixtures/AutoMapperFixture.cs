namespace BlissRecruitment.API.Tests.Fixtures
{
    using AutoMapper;

    public class AutoMapperFixture
    {
        public AutoMapperFixture()
        {
            this.Mapper = new MapperConfiguration(_ => _.AddProfiles(new[] { typeof(Infrastructure.AutoMapper.AutoMapperProfile) })).CreateMapper();
        }

        public IMapper Mapper { get; internal set; }
    }
}
