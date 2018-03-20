namespace BlissRecruitment.BusinessLogic.Concrete
{
    using Abstract;
    using AutoMapper;
    using BlissRecruitment.DataAccess.Abstract;
    using log4net;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System;

    public class HealthStatusProvider : IHealthStatusProvider
    {
        private readonly ILog logger;
        private readonly IMapper mapper;
        private readonly IHealthStatusRepository repository;

        public HealthStatusProvider(ILog logger, IMapper mapper, IHealthStatusRepository repository)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public OperationResult GetHealthStatus()
        {
            try
            {
                var res = this.mapper.Map<Domain.HealthStatus>(this.repository.GetHealthStatus());
                return new OperationResult(res.State);
            }
            catch (Exception e)
            {
                this.logger.Error(e.Message, e);
            }

            return new OperationResult(-1);
        }
    }
}
