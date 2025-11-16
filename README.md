# Dự án ShoeShop (Website bán giày)

Đây là dự án website bán hàng cơ bản được xây dựng bằng .NET.

## Mô tả
Một website bán giày đơn giản, cho phép xem sản phẩm và quản lý giỏ hàng.

## Yêu cầu để chạy dự án
* [.NET SDK](https://dotnet.microsoft.com/en-us/download) (Khuyến nghị .NET 6.0 hoặc 7.0, 8.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (Hoặc một trình soạn thảo code như VS Code)

## Cách chạy dự án

Bạn có 2 cách để chạy:

### Cách 1: Sử dụng Visual Studio (Khuyến nghị)
1.  Clone (tải) dự án này về máy.
2.  Mở thư mục dự án, tìm và click đúp vào file `.sln` (ShoeShop.sln).
3.  Trong Visual Studio, nhấn phím **F5** (hoặc nút Start màu xanh lá) để bắt đầu.

### Cách 2: Sử dụng dòng lệnh (Terminal)
1.  Clone (tải) dự án này về máy.
2.  Mở Terminal (Command Prompt hoặc Git Bash) tại thư mục gốc của dự án.
3.  Gõ lệnh sau và nhấn Enter:
    ```bash
    dotnet run
    ```
4.  Mở trình duyệt và truy cập vào địa chỉ `http://localhost:5xxx` (số cổng chính xác sẽ hiển thị trong terminal).

## 🗃️ Cơ sở dữ liệu
Dự án sử dụng **SQLite** (file `ShoeShop.db`). Dữ liệu đã được tích hợp sẵn, không cần cài đặt máy chủ CSDL.
