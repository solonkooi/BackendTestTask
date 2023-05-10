using AutoMapper;
using BackendTestTask.Business.Models;
using BackendTestTask.Core;

namespace BackendTestTask.Api.Mappers;

public class AutoMapperProfile :  Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Node, NodeModel>()
            .ForMember(dest => dest.Children, 
                act => act.MapFrom(src => src.ChildNodes))
            .ForMember(dest => dest.ParentId, 
                act => act.MapFrom(src => src.parent_id))
            .ForMember(dest => dest.TreeName, 
                act => act.MapFrom(src => src.tree_name));
        
    }
}