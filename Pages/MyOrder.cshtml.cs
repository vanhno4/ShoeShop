using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace ShoeShop.Pages
{
    [Authorize]
    public class MyOrderModel : PageModel
    {
        // Dung chung ViewModel voi trang Checkout
        public List<CartViewModel> OrderItems { get; set; } = new List<CartViewModel>();

        public IActionResult OnGet()
        {
            // Doc Session "LastOrder"
            var lastOrderJson = HttpContext.Session.GetString("LastOrder");

            if (string.IsNullOrEmpty(lastOrderJson))
            {
                // Neu khong co don hang nao, quay ve Trang Chu
                return RedirectToPage("/Index");
            }

            // Chuyen JSON thanh danh sach san pham
            OrderItems = JsonSerializer.Deserialize<List<CartViewModel>>(lastOrderJson) ?? new List<CartViewModel>();

            // Xoa "LastOrder" di de lan sau khong xem lai duoc nua (tuy chon)
            // HttpContext.Session.Remove("LastOrder");

            return Page();
        }
    }
}