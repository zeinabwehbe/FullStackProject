using AutoMapper;
using serverside.Models.Domain;
using serverside.Models.DTOs.Categories;
using serverside.Models.DTOs.Comments;
using serverside.Models.DTOs.Notifications;
using serverside.Models.DTOs.Post;
using serverside.Models.DTOs.PostTags;
using serverside.Models.DTOs.Tags;
using serverside.Models.DTOs.User;
using serverside.Models.DTOs.Votes;
namespace serverside.Mappings
{
    public class AutoMappersProfile: Profile
    {
        //inside constructors we create the mapping
        /*    public AutoMappersProfile()
            {
                CreateMap<UserDto, UserDomain>()
                    .ForMember(x => x.MypropertyDomain, opt => opt.MapFrom(x => x.MypropertyDto))
                    .ReverseMap();

            }

            public class UserDto {
                public string MypropertyDto { get; set; }

            }
            public class UserDomain {
                public string MypropertyDomain { get; set; }
            }*/
        public AutoMappersProfile()
        {
            CreateMap<Users, UserDto>().ReverseMap();
            CreateMap<AddUserRequestDto, Users>().ReverseMap();    //this will be used to convert DTO into the domain model
            CreateMap<UpdateUserRequestDto, Users>().ReverseMap();

            CreateMap<Posts, PostDto>().ReverseMap();
            CreateMap<AddPostRequestDto, Posts>().ReverseMap();    //this will be used to convert DTO into the domain model
            CreateMap<UpdatePostRequestDto, Posts>().ReverseMap();

            CreateMap<Votes, VoteDto>().ReverseMap();
            CreateMap<AddVoteRequestDto, Votes>().ReverseMap();    //this will be used to convert DTO into the domain model
            CreateMap<UpdateVoteRequestDto, Votes>().ReverseMap();

            CreateMap<Categories, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Categories>().ReverseMap();    //this will be used to convert DTO into the domain model
            CreateMap<UpdateCategoryRequestDto, Categories>().ReverseMap();

            CreateMap<Comments, CommentDto>().ReverseMap();
            CreateMap<AddCommentRequestDto, Comments>().ReverseMap();    //this will be used to convert DTO into the domain model
            CreateMap<UpdateCommentRequestDto, Comments>().ReverseMap();

            CreateMap<Notifications, NotificationDto>().ReverseMap();
            CreateMap<AddNotificationRequestDto, Notifications>().ReverseMap();    //this will be used to convert DTO into the domain model
            CreateMap<UpdateNotificationRequestDto, Notifications>().ReverseMap();

            CreateMap<Tags, TagDto>().ReverseMap();
            CreateMap<AddTagRequestDto, Tags>().ReverseMap();    //this will be used to convert DTO into the domain model
            CreateMap<UpdateTagRequestDto, Tags>().ReverseMap();

            CreateMap<PostTags, PostTagDto>().ReverseMap();
        }
    }
}
