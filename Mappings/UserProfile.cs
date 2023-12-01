using AutoMapper;
using BE_Shop.Data;
using BE_Shop.ViewModels;

namespace Chat.Web.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ChatLine, UserViewModel>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(x => x.UserName));

            CreateMap<UserViewModel, ChatLine>();
        }
    }
}
