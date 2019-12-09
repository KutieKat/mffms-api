using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFFMS.API.Helpers.Params
{
    public abstract class BaseParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
        public string Keyword { get; set; }
        public DateTime ThoiGianTaoBatDau { get; set; }
        public DateTime ThoiGianTaoKetThuc { get; set; }
        public DateTime ThoiGianCapNhatBatDau { get; set; }
        public DateTime ThoiGianCapNhatKetThuc { get; set; }
        public int TrangThai { get; set; } = 0;
        public int DaXoa { get; set; } = 0;
    }
}
