namespace TestProject.Business.Models
{
    using System.Collections.Generic;

    public class SuccessResult<T> : Result<T>
    {
        private readonly T data;

        public SuccessResult(T data)
        {
            this.data = data;
        }

        public override ResultType ResultType => ResultType.Ok;

        public override List<string> Errors => new List<string>();

        public override T Data => data;
    }
}
