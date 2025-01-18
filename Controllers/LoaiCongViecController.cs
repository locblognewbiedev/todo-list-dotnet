using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using todo.Data; // Import namespace chứa DbContext
using todo.Models;
using todo.Models.DTOs; // Import DTO

namespace todo.Controllers
{
    public class LoaiCongViecController : ApiController
    {
        // Khởi tạo DbContext
        private readonly DBContext _db;
        public LoaiCongViecController(DBContext context)
        {
            _db = context;
        }

        // GET API method để lấy dữ liệu từ cơ sở dữ liệu
        [HttpGet]
        [Route("api/LoaiCongViec")]
        public IHttpActionResult GetLoaiCongViecs()
        {
            List<LoaiCongViec> loaiCongViecs = _db.LoaiCongViecs.ToList();
            if (loaiCongViecs == null || loaiCongViecs.Count == 0)
            {
                return NotFound();
            }

            try
            {
                var loaiCongViecDtos = loaiCongViecs.Select(lcv => new LoaiCongViecDTO
                {
                    ID = lcv.ID,
                    TenLoai = lcv.TenLoai,
                    MucDoKhanCap = lcv.MucDoKhanCap
                }).ToList();

                return Ok(new { LoaiCongViecs = loaiCongViecDtos });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }

        // POST API method để tạo mới LoaiCongViec
        [HttpPost]
        [Route("api/LoaiCongViec")]
        public IHttpActionResult CreateLoaiCongViec(LoaiCongViecDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var loaiCongViec = new LoaiCongViec
                {
                    TenLoai = dto.TenLoai,
                    MucDoKhanCap = dto.MucDoKhanCap
                };

                _db.LoaiCongViecs.Add(loaiCongViec);
                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Tạo mới Loại Công Việc thành công.",
                    Data = new
                    {
                        loaiCongViec.ID,
                        loaiCongViec.TenLoai,
                        loaiCongViec.MucDoKhanCap
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }

        // PUT API method để cập nhật LoaiCongViec
        [HttpPut]
        [Route("api/LoaiCongViec/{id}")]
        public IHttpActionResult UpdateLoaiCongViec(int id, LoaiCongViecDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var loaiCongViec = _db.LoaiCongViecs.FirstOrDefault(lcv => lcv.ID == id);

                if (loaiCongViec == null)
                {
                    return NotFound();
                }

                // Cập nhật dữ liệu
                loaiCongViec.TenLoai = dto.TenLoai;
                loaiCongViec.MucDoKhanCap = dto.MucDoKhanCap;

                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Cập nhật Loại Công Việc thành công.",
                    Data = new
                    {
                        loaiCongViec.ID,
                        loaiCongViec.TenLoai,
                        loaiCongViec.MucDoKhanCap
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("api/LoaiCongViec/{id}")]
        public IHttpActionResult DeleteLoaiCongViec(int id)
        {
            try
            {
                var loaiCongViec = _db.LoaiCongViecs.FirstOrDefault(lcv => lcv.ID == id);

                if (loaiCongViec == null)
                {
                    return NotFound();
                }

                _db.LoaiCongViecs.Remove(loaiCongViec);
                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Công việc đã được xóa.",
                    Data = new
                    {
                        loaiCongViec.ID,
                        loaiCongViec.TenLoai,
                        loaiCongViec.MucDoKhanCap
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }
    }
}
