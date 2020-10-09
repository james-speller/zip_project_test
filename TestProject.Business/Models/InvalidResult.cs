namespace TestProject.Business.Models
{
    using System.Collections.Generic;

    public class InvalidResult<T>: Result<T>
    {
        private readonly List<string> errors;

        public InvalidResult(List<string> errors)
        {
            this.errors = errors;
        }

        public override ResultType ResultType => ResultType.Invalid;

        public override List<string> Errors => errors;

        public override T Data => default;
    }
}
