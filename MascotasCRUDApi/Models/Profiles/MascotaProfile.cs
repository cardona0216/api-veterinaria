using AutoMapper;
using MascotasCRUDApi.Models.DTO;

namespace MascotasCRUDApi.Models.Profiles
{
    public class MascotaProfile : Profile
    {
        //creamos el constructor

        public MascotaProfile()
        {
            CreateMap<Mascota, MascotaDTO>();
            CreateMap<MascotaDTO, Mascota>();
        }

    }
}
