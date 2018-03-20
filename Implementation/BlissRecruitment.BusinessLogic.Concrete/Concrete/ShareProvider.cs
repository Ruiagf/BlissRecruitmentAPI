namespace BlissRecruitment.BusinessLogic.Concrete
{
    using BlissRecruitment.BusinessLogic.Abstract;
    using BlissRecruitment.Domain;
    using BlissRecruitment.Domain.Utils;
    using Ruiagf.Framework.BaseUtils.HelperTypes;
    using System;

    public class ShareProvider : IShareProvider
    {
        private readonly IEmailProvider provider;
        private readonly ShareConfiguration shareConfiguration;

        public ShareProvider(IEmailProvider provider, ShareConfiguration configuration)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.shareConfiguration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public OperationResult Share(ContentToShare content)
        {
            this.provider.SendMailAsync(shareConfiguration.From, shareConfiguration.FromDescription, content.DestinationEmail, string.Empty, shareConfiguration.Subject, shareConfiguration.Body.Replace("{URL}", content.ContentUrl));

            return new OperationResult(0);
        }
    }
}
