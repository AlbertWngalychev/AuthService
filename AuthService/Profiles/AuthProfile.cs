using AuthService.Dtos;
using AuthService.Models;
using AutoMapper;
using System.Text;

namespace AuthService.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<Auth, AuthReadDto>();
        }
    }
}
