namespace TestProject.Business.Models
{
    using System.Collections.Generic;

    public class NotFoundResult<T> : Result<T>
    {
        private readonly string error;

        public NotFoundResult(string error)
        {
            this.error = error;
        }

        public override ResultType ResultType => ResultType.NotFound;

        public override List<string> Errors => new List<string> { error };

        public override T Data => default;
    }
}
