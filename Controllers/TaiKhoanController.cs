using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MFFMS.API.Data;
using MFFMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MFFMS.API.Dtos.TaiKhoanDto;
using Microsoft.AspNetCore.Authorization;
using MFFMS.API.Helpers;
using AutoMapper;
using MFFMS.API.Helpers.Params;
using MFFMS.API.Dtos.ResponseDto;

namespace MFFMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class TaiKhoanController : ControllerBase
    {
        private readonly ITaiKhoanRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly string _entityName;

        public TaiKhoanController(ITaiKhoanRepository repo, IConfiguration config, IMapper mapper)
        {
            _config = config;
            _repo = repo;
            _mapper = mapper;
            _entityName = "tài khoản";
        }

        //[Authorize(Roles = "NQL")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaiKhoanParams userParams)
        {
            try
            {
                var result = await _repo.GetAll(userParams);
                var resultToReturn = _mapper.Map<IEnumerable<TaiKhoanForListDto>>(result);

                Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy danh sách tất cả các " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = resultToReturn,
                        TotalItems = _repo.GetTotalItems(),
                        TotalPages = _repo.GetTotalPages(),
                        PageNumber = userParams.PageNumber,
                        PageSize = userParams.PageSize,
                        StatusStatistics = _repo.GetStatusStatistics(userParams)
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy danh sách tất cả các " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        //[Authorize(Roles = "NQL")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _repo.GetById(id);
                var resultToReturn = _mapper.Map<TaiKhoanForViewDto>(result);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy thông tin chi tiết của " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = resultToReturn
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy thông tin chi tiết của " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        //[Authorize(Roles = "NQL")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(string id, TaiKhoanForUpdateDto taiKhoan)
        {
            try
            {
                var result = await _repo.UpdateById(id, taiKhoan);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Cập nhật " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Cập nhật " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangePassword(string id, TaiKhoanForChangePasswordDto taiKhoan)
        {
            try
            {
                var result = await _repo.ChangePassword(id, taiKhoan);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Thay đổi mật khẩu cho tài khoản thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Thay đổi mật khẩu cho tài khoản thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ResetPassword(string id)
        {
            try
            {
                var result = await _repo.ResetPassword(id);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Khôi phục mật khẩu mặc định cho tài khoản thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Khôi phục mật khẩu mặc định cho tài khoản thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PermanentlyDeleteById(string id)
        {
            try
            {
                var result = await _repo.PermanentlyDeleteById(id);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Xóa " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Xóa " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> TemporarilyDeleteById(string id)
        {
            try
            {
                var result = await _repo.TemporarilyDeleteById(id);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Xóa tạm thời " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Xóa tạm thời " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> RestoreById(string id)
        {
            try
            {
                var result = await _repo.RestoreById(id);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Khôi phục " + _entityName + " thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Khôi phục " + _entityName + " thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(TaiKhoanForCreateDto taiKhoan)
        {
            try
            {
                taiKhoan.TenDangNhap = taiKhoan.TenDangNhap.ToLower();
                var exists = await _repo.TaiKhoanTonTai(taiKhoan.TenDangNhap);

                if (exists == true)
                {
                    return BadRequest();
                }

                var taiKhoanMoi = new TaiKhoan
                {
                    TenDangNhap = taiKhoan.TenDangNhap,
                    HoVaTen = taiKhoan.HoVaTen,
                    GioiTinh = taiKhoan.GioiTinh,
                    ThoiGianTao = DateTime.Now,
                    DiaChi = taiKhoan.DiaChi,
                    NgaySinh = taiKhoan.NgaySinh,
                    Email = taiKhoan.Email,
                    SoDienThoai = taiKhoan.SoDienThoai,
                    PhanQuyen = taiKhoan.PhanQuyen
                };

                var taiKhoanDuocTao = await _repo.TaoTaiKhoan(taiKhoanMoi, taiKhoan.MatKhau);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Tạo tài khoản mới thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = taiKhoanMoi
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Tạo tài khoản mới thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ValidateHash([FromBody] ValidateHashDto objectToValidate)
        {
            try
            {
                var result = await _repo.ValidateHash(objectToValidate.TenDangNhap, objectToValidate.Hash);

                if (result == null)
                {
                    return StatusCode(500, new FailedResponseDto
                    {
                        Message = "Tài khoản không tồn tại!"
                    });
                }
                else
                {
                    return StatusCode(200, new SuccessResponseDto
                    {
                        Message = "Tài khoản tồn tại!",
                        Result = new SuccessResponseResultWithSingleDataDto
                        {
                            Data = result
                        }
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Tài khoản không tồn tại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] TaiKhoanForLoginDto taiKhoan)
        {
            try
            {
                var taiKhoanDuocDangNhap = await _repo.DangNhap(taiKhoan.TenDangNhap.ToLower(), taiKhoan.MatKhau);

                if (taiKhoanDuocDangNhap == null)
                {
                    return StatusCode(500, new FailedResponseDto
                    {
                        Message = "Đăng nhập vào hệ thống thất bại!"
                    });
                }

                //var claims = new[]
                //{
                //    new Claim(ClaimTypes.NameIdentifier, taiKhoanDuocDangNhap.MaTaiKhoan.ToString()),
                //    new Claim(ClaimTypes.Name, taiKhoanDuocDangNhap.TenDangNhap),
                //    new Claim(ClaimTypes.Role, taiKhoanDuocDangNhap.PhanQuyen)
                //};

                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                //var tokenDescriptor = new SecurityTokenDescriptor
                //{
                //    Subject = new ClaimsIdentity(claims),
                //    Expires = DateTime.Now.AddDays(1),
                //    SigningCredentials = creds
                //};

                //var tokenHandler = new JwtSecurityTokenHandler();
                //var token = tokenHandler.CreateToken(tokenDescriptor);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Đăng nhập vào hệ thống thành công!",
                    Result = new SuccessResponseResultWithSingleDataDto
                    {
                        Data = taiKhoanDuocDangNhap
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Đăng nhập vào hệ thống thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }
    }
}