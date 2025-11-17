using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Data
{
    // Kế thừa từ DbContext của EF Core
    public class ApplicationDbContext : DbContext
    {
        // Hàm khởi tạo, nhận các tùy chọn cấu hình
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Khai báo với EF Core rằng chúng ta có một bảng tên là "Products"
        // dựa trên mô hình "Product"
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
    }
}