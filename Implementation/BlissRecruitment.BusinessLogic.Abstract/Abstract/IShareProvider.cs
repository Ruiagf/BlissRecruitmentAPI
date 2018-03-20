namespace BlissRecruitment.BusinessLogic.Abstract
{
    using BlissRecruitment.Domain;
    using Ruiagf.Framework.BaseUtils.HelperTypes;

    public interface IShareProvider
    {
        OperationResult Share(ContentToShare content);
    }
}
