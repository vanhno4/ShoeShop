using System.Linq;
using ShoeShop.Data;

namespace ShoeShop.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // --- THAY ĐỔI QUAN TRỌNG ---
            // Chỉ thêm sản phẩm NẾU CSDL trống (không có sản phẩm nào)
            if (context.Products.Any())
            {
                return; // CSDL đã có dữ liệu, không làm gì cả
            }
            // --- KẾT THÚC THAY ĐỔI ---

            // Nếu CSDL trống, thêm 40 sản phẩm
            var products = new List<Product>
            {
                // (Tôi rút gọn danh sách cho dễ nhìn, 
                // bạn chỉ cần biết là nó chứa 40 sản phẩm)
                
                // --- 12 sản phẩm Nam (Stock = 50) ---
                new Product { Name = "Giày Chạy Bộ Năng Động", Category = "Nam", Price = 1200000, ImageUrl = "/images/a1.jpg", Description = "Giày chạy bộ nhẹ và êm ái.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Lười Vải", Category = "Nam", Price = 780000, ImageUrl = "/images/a2.jpg", Description = "Thoải mái cho dạo phố.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Tây Da Bò", Category = "Nam", Price = 2500000, ImageUrl = "/images/a3.jpg", Description = "Lịch lãm và sang trọng.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Sneaker Cổ Cao Da", Category = "Nam", Price = 1800000, ImageUrl = "/images/a4.jpg", Description = "Phong cách đường phố.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Sandal Da Quai Chéo", Category = "Nam", Price = 650000, ImageUrl = "/images/a5.jpg", Description = "Thoáng mát cho mùa hè.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Boot Cổ Ngắn", Category = "Nam", Price = 2200000, ImageUrl = "/images/a6.jpg", Description = "Mạnh mẽ và nam tính.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Thể Thao Đa Năng", Category = "Nam", Price = 1400000, ImageUrl = "/images/a7.jpg", Description = "Hỗ trợ tập luyện đa dạng.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Lười Da Lộn", Category = "Nam", Price = 1600000, ImageUrl = "/images/a8.jpg", Description = "Sang trọng, dễ phối đồ.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Chạy Trail", Category = "Nam", Price = 1900000, ImageUrl = "/images/a9.jpg", Description = "Bám đường, chinh phục địa hình.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Lười Vải Lưới", Category = "Nam", Price = 850000, ImageUrl = "/images/a10.jpg", Description = "Siêu thoáng khí.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Tennis Pro", Category = "Nam", Price = 2100000, ImageUrl = "/images/a11.jpg", Description = "Chuyên dụng cho sân tennis.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Dép Quai Ngang Da", Category = "Nam", Price = 500000, ImageUrl = "/images/a12.jpg", Description = "Dép da cao cấp.", OriginalPrice = null, Stock = 50 },

                // --- 12 sản phẩm Nữ (Stock = 50) ---
                new Product { Name = "Sneaker Cổ Điển", Category = "Nữ", Price = 950000, ImageUrl = "/images/n1.jpg", Description = "Đơn giản và tinh tế.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Cao Gót Mũi Nhọn", Category = "Nữ", Price = 1300000, ImageUrl = "/images/n2.jpg", Description = "Quyến rũ và thanh lịch.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Búp Bê Da Mềm", Category = "Nữ", Price = 880000, ImageUrl = "/images/n3.jpg", Description = "Êm ái mỗi bước chân.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Sandal Đế Xuồng", Category = "Nữ", Price = 1100000, ImageUrl = "/images/n4.jpg", Description = "Tôn dáng, dễ đi.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Boot Cổ Cao", Category = "Nữ", Price = 2800000, ImageUrl = "/images/n5.jpg", Description = "Thời thượng và ấm áp.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Lười Nữ", Category = "Nữ", Price = 920000, ImageUrl = "/images/n6.jpg", Description = "Năng động, tiện lợi.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Thể Thao Trắng", Category = "Nữ", Price = 1150000, ImageUrl = "/images/n7.jpg", Description = "Must-have item.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Cao Gót Đế Vuông", Category = "Nữ", Price = 1400000, ImageUrl = "/images/n8.jpg", Description = "Vững chãi, hợp thời trang.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Sandal Quai Mảnh", Category = "Nữ", Price = 750000, ImageUrl = "/images/n9.jpg", Description = "Nữ tính và nhẹ nhàng.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Chạy Bộ Nữ", Category = "Nữ", Price = 1250000, ImageUrl = "/images/n10.jpg", Description = "Hỗ trợ tối đa khi luyện tập.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Thể Dục Aerobic", Category = "Nữ", Price = 1350000, ImageUrl = "/images/n11.jpg", Description = "Nhẹ nhàng cho các bài tập.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Dép Nữ", Category = "Nữ", Price = 600000, ImageUrl = "/images/n12.jpg", Description = "Tiện lợi, chống nước.", OriginalPrice = null, Stock = 50 },
                
                // --- 8 sản phẩm Trẻ Em (Stock = 50) ---
                new Product { Name = "Sneaker Trẻ Em Có Đèn", Category = "Trẻ Em", Price = 650000, ImageUrl = "/images/t1.jpg", Description = "Nhấp nháy vui nhộn.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Sandal Tập Đi", Category = "Trẻ Em", Price = 450000, ImageUrl = "/images/t2.jpg", Description = "Bảo vệ chân bé.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Lười Vải Trẻ Em", Category = "Trẻ Em", Price = 500000, ImageUrl = "/images/t3.jpg", Description = "Dễ dàng mang vào.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Thể Thao Bé Trai", Category = "Trẻ Em", Price = 700000, ImageUrl = "/images/t4.jpg", Description = "Bền bỉ cho bé trai.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Búp Bê Bé Gái", Category = "Trẻ Em", Price = 600000, ImageUrl = "/images/t5.jpg", Description = "Xinh xắn như công chúa.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Boot Đi Mưa Trẻ Em", Category = "Trẻ Em", Price = 550000, ImageUrl = "/images/t6.jpg", Description = "Giữ chân bé khô ráo.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Sandal Bé Gái", Category = "Trẻ Em", Price = 480000, ImageUrl = "/images/t7.jpg", Description = "Họa tiết hoa xinh xắn.", OriginalPrice = null, Stock = 50 },
                new Product { Name = "Giày Vải Siêu Nhân", Category = "Trẻ Em", Price = 620000, ImageUrl = "/images/t8.jpg", Description = "Hình siêu nhân.", OriginalPrice = null, Stock = 50 },

                // --- 8 sản phẩm Giảm Giá (Stock = 50) ---
                new Product { Name = "Giày Sneaker ", Category = "Giảm Giá", Price = 450000, ImageUrl = "/images/g1.jpg", Description = "Giảm giá 50%", OriginalPrice = 900000, Stock = 50 },
                new Product { Name = "Sandal Hè ", Category = "Giảm Giá", Price = 300000, ImageUrl = "/images/g2.jpg", Description = "Giảm giá sốc", OriginalPrice = 600000, Stock = 50 },
                new Product { Name = "Giày Tây ", Category = "Giảm Giá", Price = 900000, ImageUrl = "/images/g3.jpg", Description = "Hàng trưng bày", OriginalPrice = 2500000, Stock = 50 },
                new Product { Name = "Giày Chạy ", Category = "Giảm Giá", Price = 700000, ImageUrl = "/images/g4.jpg", Description = "Phiên bản 2023", OriginalPrice = 1200000, Stock = 50 },
                new Product { Name = "Boot Da Nữ ", Category = "Giảm Giá", Price = 1500000, ImageUrl = "/images/g5.jpg", Description = "Giảm giá 30%", OriginalPrice = 2150000, Stock = 50 },
                new Product { Name = "Giày Thể Thao Nam ", Category = "Giảm Giá", Price = 1100000, ImageUrl = "/images/g6.jpg", Description = "Khuyến mãi 20%", OriginalPrice = 1375000, Stock = 50 },
                new Product { Name = "Giày Lười ", Category = "Giảm Giá", Price = 400000, ImageUrl = "/images/g7.jpg", Description = "Giày mẫu", OriginalPrice = 800000, Stock = 50 },
                new Product { Name = "Sandal ", Category = "Giảm Giá", Price = 250000, ImageUrl = "/images/g8.jpg", Description = "Chỉ còn size 36", OriginalPrice = 500000, Stock = 50 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}