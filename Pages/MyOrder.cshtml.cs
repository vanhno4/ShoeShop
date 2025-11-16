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
        public List<CartItemViewModel> OrderItems { get; set; } = new List<CartItemViewModel>();

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
            OrderItems = JsonSerializer.Deserialize<List<CartItemViewModel>>(lastOrderJson) ?? new List<CartItemViewModel>();

            // Xoa "LastOrder" di de lan sau khong xem lai duoc nua (tuy chon)
            // HttpContext.Session.Remove("LastOrder");

            return Page();
        }
    }
}