using AutoMapper;
using ScanME.DTO;
using ScanME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Mapping
{
    public class ModelToResourceProfile:Profile
    {
        public ModelToResourceProfile()
        {
            //users resource data
            CreateMap<Users, UserDTO>()
                .ForMember(u => u.Company,
                options => options.MapFrom(u => new CompanyDTO
                {
                    CompanyId = u.Company.CompanyId,
                    CompanyName = u.Company.Name,
                    Logo = u.Company.Logo,
                    Category = new CompanyCategoryDTO
                    {
                        CompanyCategoryId = u.Company.Category.CompanyCategoryId,
                        Name = u.Company.Category.Name,

                    },
                }))
                .ForMember(u => u.Role,
                    options => options.MapFrom(u => new RoleDTO { RoleId = u.Role.RoleId, Name = u.Role.Name }));
                
        }

    }
}
