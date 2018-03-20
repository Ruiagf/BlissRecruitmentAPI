namespace Ruiagf.Framework.BaseUtils.HelperTypes
{
    using System.Collections.Generic;

    public class OperationResult
    {
        public OperationResult(params string[] errors)
        {
            this.Succeeded = false;
            this.Errors = errors;
        }

        public OperationResult(IEnumerable<string> errors)
        {
            this.Succeeded = false;
            this.Errors = errors;
        }

        public OperationResult(int status)
        {
            this.Succeeded = (status == 0);
            this.Status = status;
        }

        public IEnumerable<string> Errors { get; }

        public bool Succeeded { get; }

        public int Status { get; set; }
       
        public override string ToString() => $@"{nameof(OperationResult)} {{ {nameof(this.Succeeded)}:{this.Succeeded} {nameof(this.Status)}:{this.Status} {nameof(this.Errors)}:{(this.Errors == null ? "[]" : string.Join(" | ", this.Errors))} }}";
    }
}
