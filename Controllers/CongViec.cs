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
    public class CongViecController : ApiController
    {
        // Khởi tạo DbContext
        private readonly DBContext _db;
        public CongViecController(DBContext context)
        {
            _db = context;
        }

        // GET API method để lấy danh sách công việc
        [HttpGet]
        [Route("api/CongViec")]
        public IHttpActionResult GetCongViecs()
        {
            try
            {
                var congViecs = _db.CongViecs.ToList();

                if (congViecs == null || congViecs.Count == 0)
                {
                    return NotFound();
                }

                var congViecDtos = congViecs.Select(cv => new CongViecDTO
                {
                    ID = cv.ID,
                    TieuDe = cv.TieuDe,
                    MoTa = cv.MoTa,
                    NgayBatDau = cv.NgayBatDau,
                    NgayKetThuc = cv.NgayKetThuc,
                    TrangThai = cv.TrangThai,
                    LoaiCongViecID = cv.LoaiCongViecID
                }).ToList();

                return Ok(new { CongViecs = congViecDtos });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }

        // POST API method để tạo mới công việc
        [HttpPost]
        [Route("api/CongViec")]
        public IHttpActionResult CreateCongViec(CongViecDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var congViec = new CongViec
                {
                    TieuDe = dto.TieuDe,
                    MoTa = dto.MoTa,
                    NgayBatDau = dto.NgayBatDau,
                    NgayKetThuc = dto.NgayKetThuc,
                    TrangThai = dto.TrangThai,
                    LoaiCongViecID = dto.LoaiCongViecID
                };

                _db.CongViecs.Add(congViec);
                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Tạo mới Công Việc thành công.",
                    Data = new
                    {
                        congViec.ID,
                        congViec.TieuDe,
                        congViec.MoTa,
                        congViec.NgayBatDau,
                        congViec.NgayKetThuc,
                        congViec.TrangThai,
                        congViec.LoaiCongViecID
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }

        // PUT API method để cập nhật công việc
        [HttpPut]
        [Route("api/CongViec/{id}")]
        public IHttpActionResult UpdateCongViec(int id, CongViecDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            try
            {
                var congViec = _db.CongViecs.FirstOrDefault(cv => cv.ID == id);

                if (congViec == null)
                {
                    return NotFound();
                }

                congViec.TieuDe = dto.TieuDe;
                congViec.MoTa = dto.MoTa;
                congViec.NgayBatDau = dto.NgayBatDau;
                congViec.NgayKetThuc = dto.NgayKetThuc;
                congViec.TrangThai = dto.TrangThai;
                congViec.LoaiCongViecID = dto.LoaiCongViecID;

                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Cập nhật Công Việc thành công.",
                    Data = new
                    {
                        congViec.ID,
                        congViec.TieuDe,
                        congViec.MoTa,
                        congViec.NgayBatDau,
                        congViec.NgayKetThuc,
                        congViec.TrangThai,
                        congViec.LoaiCongViecID
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }
        [HttpGet]
        [Route("api/CongViec/{id}")]
        public IHttpActionResult GetCongViecById(int id)
        {
            try
            {
                var congViec = _db.CongViecs.FirstOrDefault(cv => cv.ID == id);

                if (congViec == null)
                {
                    return NotFound();
                }

                var congViecDto = new CongViecDTO
                {
                    ID = congViec.ID,
                    TieuDe = congViec.TieuDe,
                    MoTa = congViec.MoTa,
                    NgayBatDau = congViec.NgayBatDau,
                    NgayKetThuc = congViec.NgayKetThuc,
                    TrangThai = congViec.TrangThai,
                    LoaiCongViecID = congViec.LoaiCongViecID
                };

                return Ok(new
                {
                    Message = "Lấy chi tiết Công Việc thành công.",
                    Data = congViecDto
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return InternalServerError(ex);
            }
        }

        // DELETE API method để xóa công việc
        [HttpDelete]
        [Route("api/CongViec/{id}")]
        public IHttpActionResult DeleteCongViec(int id)
        {
            try
            {
                var congViec = _db.CongViecs.FirstOrDefault(cv => cv.ID == id);

                if (congViec == null)
                {
                    return NotFound();
                }

                _db.CongViecs.Remove(congViec);
                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Công Việc đã được xóa.",
                    Data = new
                    {
                        congViec.ID,
                        congViec.TieuDe,
                        congViec.MoTa,
                        congViec.NgayBatDau,
                        congViec.NgayKetThuc,
                        congViec.TrangThai,
                        congViec.LoaiCongViecID
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
