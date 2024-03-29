using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public class UserService : IUserService
{

    private readonly ApplicationDbContext _applicationDbContext;

    public UserService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<List<User>> GetUsers()
    {
        return _applicationDbContext
            .Users
            .Where(user => user.Status == UserStatus.Inactive)
            .ToListAsync();
    }

    public Task<User> GetUser()
    {
        var user1 = new User();
        var userOrderCounts = from user in _applicationDbContext.Users
            join order in _applicationDbContext.Orders on user.Id equals order.UserId into userOrders
            select new
            {
                UserId = user.Id,
                OrderCount = userOrders.Count()
            };

        
        var userWithMostOrders = userOrderCounts
            .OrderByDescending(u => u.OrderCount)
            .FirstOrDefault();
        
        user1 = _applicationDbContext
            .Users
            .FirstOrDefault(u => u.Id == userWithMostOrders.UserId);

        return Task.FromResult(user1);
    }
}