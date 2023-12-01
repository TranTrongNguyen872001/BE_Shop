using AutoMapper;
using BE_Shop.Data;
using BE_Shop.ViewModels;

namespace BE_Shop.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(dst => dst.FromUserName, opt => opt.MapFrom(x => x.FromUser.UserName))
                .ForMember(dst => dst.Room, opt => opt.MapFrom(x => x.ToRoom.Name));

            CreateMap<MessageViewModel, Message>();
        }
    }
}
