using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Data
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        // --- DÒNG MỚI ĐÃ THÊM ---
        [Required]
        public string Role { get; set; } = string.Empty; // Sẽ lưu "Admin" hoặc "User"
        // --- KẾT THÚC THÊM ---
    }
}