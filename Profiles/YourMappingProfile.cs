using AutoMapper;
using todo.Models;
using todo.Models.DTOs;
public class YourMappingProfile : Profile
{
    public YourMappingProfile()
    {
        // Cấu hình ánh xạ từ LoaiCongViec sang LoaiCongViecDTO
        CreateMap<LoaiCongViec, LoaiCongViecDTO>();

    }
}
