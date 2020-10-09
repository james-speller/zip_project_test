namespace TestProject.Business.Models
{
    using System.Collections.Generic;

    public class ConflictResult<T> : Result<T>
    {
        private readonly string error;

        public ConflictResult(string error)
        {
            this.error = error;
        }

        public override ResultType ResultType => ResultType.Conflict;

        public override List<string> Errors => new List<string> { error };

        public override T Data => default;
    }
}
