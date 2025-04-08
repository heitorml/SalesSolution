using Resales.Api.Entities;
using Resales.Api.Shared.Requests;
using Resales.Api.Shared.Responses;

namespace Resales.Api.Shared.Mapper
{
    public static class ResalesMaper
    {

        public static Resale ToEntity(ResalesCreateRequest dto)
           => new Resale
           {
               Name = dto.Name,
               Active = true,
               Cnpj = dto.Cnpj,
               ContactName = dto.ContactName,
               Email = dto.Email,
               FantasyName = dto.FantasyName,
               Phone = dto.Phone,
               Address = AddressMaper.ToEntity(dto.Address),
               CreateAt = DateTime.UtcNow
           };

        public static ResalesResponse ToResponseDto(Resale resale)
          => new ResalesResponse
          {
              Id = resale.Id,
              Name = resale.Name,
              Cnpj = resale.Cnpj,
              ContactName = resale.ContactName,
              Email = resale.Email,
              FantasyName = resale.FantasyName,
              Phone = resale.Phone,
              CreateAt = resale.CreateAt,
              Active = resale.Active,
              Addresses = AddressMaper.ToDto(resale.Address)
          };
    }
}
