using Microsoft.OpenApi.Models;

namespace TestProject.WebAPI
{
    internal class Info : OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}