# mffms-api 
:soccer: API của hệ thống quản lý sân bóng đá mini (Mini Football Field Management System)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

## Giới thiệu
* **MFFMS** (Mini Football Field Management System) là hệ thống quản lý sân bóng đá mini trên nền tảng web. MFFMS hỗ trợ tin học hóa các chức năng nghiệp vụ cơ bản mà một sân bóng đá mini cần phải có.
* Repository này là phần back-end của hệ thống.

## Ngôn ngữ lập trình và công nghệ
* Ngôn ngữ lập trình: C#
* Framework: ASP.NET Core
* Hệ quản trị CSDL: MySQL

## Môi trường phát triển
* ASP.NET Core 2.2
* C# 6.0
* Microsoft Visual Studio Enterprise 2017

## Cài đặt
* Cài đặt [ASP.NET Core](https://dotnet.microsoft.com/download/dotnet-core/2.2), phiên bản 2.2.
* Cài đặt hệ quản trị CSDL MySQL trên máy tính với [XAMPP](https://www.apachefriends.org/download.html) hoặc [MySQL Workbench](https://www.mysql.com/products/workbench/).
* Clone repository `mffms-api` về máy thông qua dòng lệnh sau:
```bash
> git clone https://github.com/KutieKat/mffms-api
```
* Mở project bằng Visual Studio, chọn `Tools > NuGet Package Manager > Package Manager Console`, lần lượt thực thi các dòng lệnh sau:
```bash
> dotnet ef migrations add InitialCreate
> dotnet ef database update
```
* Chạy MySQL với XAMPP hoặc MySQL Workbench.
* Sau khi quá trình hoàn tất, nhấn tổ hợp phím `Ctrl + F5` để chạy server tại địa chỉ `http://localhost:5000`.

## Thư viện
* [AutoMapper](https://www.nuget.org/packages/AutoMapper.Extensions.Microsoft.DependencyInjection/)
* [MySql.Data.EntityFrameworkCore](https://www.nuget.org/packages/MySql.Data.EntityFrameworkCore)
* [Swashbuckle.AspNetCore](https://www.nuget.org/packages/swashbuckle.aspnetcore/)

## Giấy phép
* [MIT](https://github.com/KutieKat/mffms-api/blob/master/LICENSE)
