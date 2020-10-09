namespace TestProject.Business.Models
{
    public enum ResultType
    {
        /// <summary>
        /// This value is used to indicate success.
        /// </summary>
        Ok,

        /// <summary>
        /// This value is used to indicate that the request parameters were invalid for one or more reasons.
        /// </summary>
        Invalid,

        /// <summary>
        /// This value is used to indicate the requested resource was not found.
        /// </summary>
        NotFound,

        /// <summary>
        /// This value is used to indicate that a conflict was found on the server, in this scenario it indicates
        /// that there was a matching value found, and to prevent duplicates an object will not be created.
        /// </summary>
        Conflict,
    }
}
