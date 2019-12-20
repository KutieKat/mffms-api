using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFFMS.API.Data.ThongKeRepository;
using MFFMS.API.Dtos.ResponseDto;
using MFFMS.API.Helpers;
using MFFMS.API.Helpers.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MFFMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ThongKeController :ControllerBase
    {
        private readonly IThongKeRepository _repo;

        public ThongKeController(IThongKeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> TongSoLuotDatSan([FromQuery] ThongKeParams userParams)
        {
            try
            {
                var result = await _repo.TongSoLuotDatSan(userParams);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TongTienDichVu([FromQuery] ThongKeParams userParams)
        {
            try
            {
                var result = await _repo.TongTienDichVu(userParams);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TongTienDatSan([FromQuery] ThongKeParams userParams)
        {
            try
            {
                var result = await _repo.TongTienDatSan(userParams);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> TongTienNhapHang([FromQuery] ThongKeParams userParams)
        {
            try
            {
                var result = await _repo.TongTienNhapHang(userParams);

                return StatusCode(200, new SuccessResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thành công!",
                    Result = new SuccessResponseResultWithMultipleDataDto
                    {
                        Data = result
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new FailedResponseDto
                {
                    Message = "Lấy dữ liệu thống kê thất bại!",
                    Result = new FailedResponseResultDto
                    {
                        Errors = e
                    }
                });
            }
        }

    }
}
