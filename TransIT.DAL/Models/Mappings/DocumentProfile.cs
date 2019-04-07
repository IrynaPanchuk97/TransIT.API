using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDTO, Document>()
                .ForMember(d => d.ModId, opt => opt.Ignore())
                .ForMember(d => d.CreateId, opt => opt.Ignore())
                .ForMember(d => d.Mod, opt => opt.Ignore())
                .ForMember(d => d.Create, opt => opt.Ignore())
                .ForMember(d => d.Bill, opt => opt.Ignore())
                .ForMember(d => d.IssueLogId, opt => opt.Ignore());
            CreateMap<Document, DocumentDTO>();
        }
    }
}