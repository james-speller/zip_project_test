namespace TestProject.WebAPI.MappingProfiles
{
    using AutoMapper;
    using TestProject.Data.Models;
    using TestProject.WebAPI.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // For simplicity I went both ways with this by using reverse map.
            // I wouldn't do that for something more complex without specifying things like passwords to ignore.

            this.CreateMap<AccountViewModel, Account>()
                .ReverseMap();

            this.CreateMap<UserViewModel, User>()
                .ReverseMap();
        }
    }
}
