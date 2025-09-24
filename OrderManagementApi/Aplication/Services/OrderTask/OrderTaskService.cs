using OrderManagementAPI.Infrastructure.Percistence;

namespace OrderManagementAPI.Aplication.Services.OrderTask
{
    public class OrderTaskService : IOrderTaskService
    {
        private readonly AppDbContext _context;

        public OrderTaskService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Guid> CreateOrderTaskAsync(Guid OrderId, string TaskId)
        {
            var orderTask = new Domain.Entities.OrderTasks(OrderId, TaskId);
            _context.OrderTasks.Add(orderTask);
            await _context.SaveChangesAsync();
            return orderTask.Id;
        }

    }
}
