using Entities.Models;
using LiteNMS.DTOS;

namespace LIteNMS.Application.Contracts;

public interface IDiscoveryProfileService
{
    Task<IEnumerable<DiscoveryProfileResponseDto>> GetAllAsync();
    Task<DiscoveryProfileResponseDto> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(DiscoveryProfileRequestDto profileDto);
    Task UpdateAsync(Guid id, DiscoveryProfileRequestDto profileDto);
    Task DeleteAsync(Guid id);
}