using System.Linq;
using ShoeShop.Data;
using System.Collections.Generic; // Cần thêm using này cho List<>

namespace ShoeShop.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // --- QUAN TRỌNG: XÓA SẠCH ĐỂ NẠP LẠI ---
            // Đảm bảo có dữ liệu mới nhất (bao gồm cả Size)
            // Chỉ dùng khi phát triển (Development)
            if (context.ProductVariants.Any())
            {
                context.ProductVariants.RemoveRange(context.ProductVariants);
            }
            if (context.Products.Any())
            {
                context.Products.RemoveRange(context.Products);
            }
            context.SaveChanges();

            var products = new List<Product>
            {
                // --- 12 SẢN PHẨM NAM ---
                new Product {
                    Name = "Giày Chạy Bộ Năng Động", Category = "Nam", Price = 1200000, ImageUrl = "/images/a1.jpg", Description = "Giày chạy bộ nhẹ và êm ái, hỗ trợ tối đa cho việc luyện tập.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Lười Vải", Category = "Nam", Price = 780000, ImageUrl = "/images/a2.jpg", Description = "Thoải mái cho dạo phố, thiết kế trẻ trung, dễ phối đồ.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Tây Da Bò", Category = "Nam", Price = 2500000, ImageUrl = "/images/a3.jpg", Description = "Lịch lãm và sang trọng, 100% da bò thật, đế khâu chắc chắn.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Sneaker Cổ Cao Da", Category = "Nam", Price = 1800000, ImageUrl = "/images/a4.jpg", Description = "Phong cách đường phố, chất liệu da tổng hợp cao cấp, bền bỉ.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Sandal Da Quai Chéo", Category = "Nam", Price = 650000, ImageUrl = "/images/a5.jpg", Description = "Thoáng mát cho mùa hè, đế cao su chống trượt.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Boot Cổ Ngắn", Category = "Nam", Price = 2200000, ImageUrl = "/images/a6.jpg", Description = "Mạnh mẽ và nam tính, phù hợp cho các chuyến đi phượt.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Thể Thao Đa Năng", Category = "Nam", Price = 1400000, ImageUrl = "/images/a7.jpg", Description = "Hỗ trợ tập luyện đa dạng, từ gym đến chạy bộ.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Lười Da Lộn", Category = "Nam", Price = 1600000, ImageUrl = "/images/a8.jpg", Description = "Sang trọng, dễ phối đồ, chất liệu da lộn cao cấp.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Chạy Trail", Category = "Nam", Price = 1900000, ImageUrl = "/images/a9.jpg", Description = "Bám đường, chinh phục mọi địa hình, chống nước nhẹ.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Lười Vải Lưới", Category = "Nam", Price = 850000, ImageUrl = "/images/a10.jpg", Description = "Siêu thoáng khí, thích hợp cho mùa hè nóng nực.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Tennis Pro", Category = "Nam", Price = 2100000, ImageUrl = "/images/a11.jpg", Description = "Chuyên dụng cho sân tennis, đế bám, hỗ trợ di chuyển ngang.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Dép Quai Ngang Da", Category = "Nam", Price = 500000, ImageUrl = "/images/a12.jpg", Description = "Dép da cao cấp, mang lại sự thoải mái tối đa.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },

                // --- 12 SẢN PHẨM NỮ ---
                new Product {
                    Name = "Sneaker Cổ Điển", Category = "Nữ", Price = 950000, ImageUrl = "/images/n1.jpg", Description = "Đơn giản và tinh tế, phù hợp với mọi trang phục.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Cao Gót Mũi Nhọn", Category = "Nữ", Price = 1300000, ImageUrl = "/images/n2.jpg", Description = "Quyến rũ và thanh lịch, gót cao 7cm.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Búp Bê Da Mềm", Category = "Nữ", Price = 880000, ImageUrl = "/images/n3.jpg", Description = "Êm ái mỗi bước chân, chất liệu da mềm không gây đau.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Sandal Đế Xuồng", Category = "Nữ", Price = 1100000, ImageUrl = "/images/n4.jpg", Description = "Tôn dáng, dễ đi, đế xuồng cao 5cm.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Boot Cổ Cao", Category = "Nữ", Price = 2800000, ImageUrl = "/images/n5.jpg", Description = "Thời thượng và ấm áp, lót lông bên trong.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Lười Nữ", Category = "Nữ", Price = 920000, ImageUrl = "/images/n6.jpg", Description = "Năng động, tiện lợi, dễ dàng mang vào và tháo ra.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Thể Thao Trắng", Category = "Nữ", Price = 1150000, ImageUrl = "/images/n7.jpg", Description = "Must-have item, dễ dàng phối với váy hoặc quần jean.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Cao Gót Đế Vuông", Category = "Nữ", Price = 1400000, ImageUrl = "/images/n8.jpg", Description = "Vững chãi, hợp thời trang, gót cao 5cm.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Sandal Quai Mảnh", Category = "Nữ", Price = 750000, ImageUrl = "/images/n9.jpg", Description = "Nữ tính và nhẹ nhàng, thích hợp đi tiệc.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Chạy Bộ Nữ", Category = "Nữ", Price = 1250000, ImageUrl = "/images/n10.jpg", Description = "Hỗ trợ tối đa khi luyện tập, màu sắc thời trang.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Thể Dục Aerobic", Category = "Nữ", Price = 1350000, ImageUrl = "/images/n11.jpg", Description = "Nhẹ nhàng cho các bài tập trong nhà.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Dép Nữ", Category = "Nữ", Price = 600000, ImageUrl = "/images/n12.jpg", Description = "Tiện lợi, chống nước, thích hợp đi mưa.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                
                // --- 8 SẢN PHẨM TRẺ EM ---
                new Product {
                    Name = "Sneaker Trẻ Em Có Đèn", Category = "Trẻ Em", Price = 650000, ImageUrl = "/images/t1.jpg", Description = "Nhấp nháy vui nhộn, bé nào cũng thích.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Sandal Tập Đi", Category = "Trẻ Em", Price = 450000, ImageUrl = "/images/t2.jpg", Description = "Bảo vệ chân bé, đế mềm chống trượt.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Lười Vải Trẻ Em", Category = "Trẻ Em", Price = 500000, ImageUrl = "/images/t3.jpg", Description = "Dễ dàng mang vào, họa tiết hoạt hình.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Thể Thao Bé Trai", Category = "Trẻ Em", Price = 700000, ImageUrl = "/images/t4.jpg", Description = "Bền bỉ cho bé trai hiếu động.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Búp Bê Bé Gái", Category = "Trẻ Em", Price = 600000, ImageUrl = "/images/t5.jpg", Description = "Xinh xắn như công chúa, đính nơ dễ thương.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Boot Đi Mưa Trẻ Em", Category = "Trẻ Em", Price = 550000, ImageUrl = "/images/t6.jpg", Description = "Giữ chân bé khô ráo, chất liệu an toàn.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Sandal Bé Gái", Category = "Trẻ Em", Price = 480000, ImageUrl = "/images/t7.jpg", Description = "Họa tiết hoa xinh xắn, quai dán tiện lợi.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Vải Siêu Nhân", Category = "Trẻ Em", Price = 620000, ImageUrl = "/images/t8.jpg", Description = "Hình siêu nhân, đế cao su êm ái.", OriginalPrice = null, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },

                // --- 8 SẢN PHẨM GIẢM GIÁ ---
                new Product {
                    Name = "Giày Sneaker (Lỗi Mốt)", Category = "Giảm Giá", Price = 450000, ImageUrl = "/images/g1.jpg", Description = "Giảm giá 50%, hàng tồn kho, kiểu dáng 2023.", OriginalPrice = 900000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Sandal Hè (Cuối Mùa)", Category = "Giảm Giá", Price = 300000, ImageUrl = "/images/g2.jpg", Description = "Giảm giá sốc, xả hàng cuối mùa hè.", OriginalPrice = 600000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Tây (Xước Nhẹ)", Category = "Giảm Giá", Price = 900000, ImageUrl = "/images/g3.jpg", Description = "Hàng trưng bày, bị xước nhẹ không đáng kể, giảm 60%.", OriginalPrice = 2500000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Chạy (Phiên Bản Cũ)", Category = "Giảm Giá", Price = 700000, ImageUrl = "/images/g4.jpg", Description = "Phiên bản 2023, giảm 40%, chất lượng vẫn đảm bảo.", OriginalPrice = 1200000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Boot Da Nữ (Mới)", Category = "Giảm Giá", Price = 1500000, ImageUrl = "/images/g5.jpg", Description = "Mẫu mới, giảm giá 30% tuần lễ khai trương.", OriginalPrice = 2150000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Thể Thao Nam (Mới)", Category = "Giảm Giá", Price = 1100000, ImageUrl = "/images/g6.jpg", Description = "Hàng mới về, khuyến mãi 20% giới thiệu.", OriginalPrice = 1375000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Giày Lười (Hàng mẫu)", Category = "Giảm Giá", Price = 400000, ImageUrl = "/images/g7.jpg", Description = "Giày mẫu, giảm giá 70%, chỉ có size 37.", OriginalPrice = 800000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                },
                new Product {
                    Name = "Sandal (Còn 1 size)", Category = "Giảm Giá", Price = 250000, ImageUrl = "/images/g8.jpg", Description = "Chỉ còn size 36", OriginalPrice = 500000, Stock = 150,
                    Variants = new List<ProductVariant> {
                        new ProductVariant { Size = "M", Stock = 50 },
                        new ProductVariant { Size = "L", Stock = 50 },
                        new ProductVariant { Size = "XL", Stock = 50 }
                    }
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}