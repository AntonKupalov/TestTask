using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public OrderService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public Task<List<Order>> GetOrders()
    {
        return _applicationDbContext
            .Orders
            .Where(order => order.Quantity > 10).ToListAsync();
    }

    public Task<Order> GetOrder()
    {
        return _applicationDbContext
            .Orders
            .OrderByDescending(o => o.Quantity * o.Price)
            .FirstOrDefaultAsync();
    }
}