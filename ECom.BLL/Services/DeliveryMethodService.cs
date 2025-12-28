using AutoMapper;
using ECom.BLL.DTOs;
using ECom.BLL.Interfaces;
using ECom.DAL.Data;
using ECom.DAL.Entities.Order;
using Microsoft.EntityFrameworkCore;

public class DeliveryMethodService : IDeliveryMethodService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public DeliveryMethodService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IReadOnlyList<DeliveryMethodDto>> GetAllAsync()
    {
        var methods = await _context.DeliveryMethods.ToListAsync();
        return _mapper.Map<IReadOnlyList<DeliveryMethodDto>>(methods);
    }

    public async Task<DeliveryMethodDto?> GetByIdAsync(int id)
    {
        var method = await _context.DeliveryMethods.FindAsync(id);
        if (method == null) return null;

        return _mapper.Map<DeliveryMethodDto>(method);
    }


    public async Task<DeliveryMethodDto> CreateAsync(CreateDeliveryMethodDto dto)
    {
        var method = _mapper.Map<DeliveryMethod>(dto);

        _context.DeliveryMethods.Add(method);
        await _context.SaveChangesAsync();

        return _mapper.Map<DeliveryMethodDto>(method);
    }


    public async Task<bool> UpdateAsync(int id, UpdateDeliveryMethodDto dto)
    {
        var method = await _context.DeliveryMethods.FindAsync(id);
        if (method == null) return false;

        _mapper.Map(dto, method);

        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var method = await _context.DeliveryMethods.FindAsync(id);
        if (method == null) return false;

        _context.DeliveryMethods.Remove(method);
        await _context.SaveChangesAsync();
        return true;
    }
}
