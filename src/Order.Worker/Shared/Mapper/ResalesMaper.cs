using Orders.Worker.Entities;
using Orders.Worker.Shared.Requests;
using Orders.Worker.Shared.Responses;

namespace Orders.Worker.Shared.Mapper
{
    public static class ResalesMaper
    {

        public static Resale ToEntity(ResalesRequest dto)
           => new Resale
           {
               Name = dto.Name,
               Active = true,
               Cnpj = dto.Cnpj,
               ContactName = dto.ContactName,
               Email = dto.Email,
               FantasyName = dto.FantasyName,
               Phone = dto.Phone,
             //  Address = AddressMaper.ToEntity(dto.Address),
               CreateAt = DateTime.UtcNow
           };

        //public static ResalesResponseDto ToResponseDto(Resale resale)
        //  => new ResalesResponseDto
        //  {
        //      Id = resale.Id,
        //      Name = resale.Name,
        //      Cnpj = resale.Cnpj,
        //      ContactName = resale.ContactName,
        //      Email = resale.Email,
        //      FantasyName = resale.FantasyName,
        //      Phone = resale.Phone,
        //      CreateAt = resale.CreateAt,
        //      Active = resale.Active,
        //      Addresses = AddressMaper.ToDto(resale.Address)
        //  };
    }
}
