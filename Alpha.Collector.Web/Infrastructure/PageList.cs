using System.Collections.Generic;

namespace Alpha.Collector.Web
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T>
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; }

        /// <summary>
        /// 数据项
        /// </summary>
        public List<T> Items { get; }

        /// <summary>
        /// 每页多少数据
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrev => PageIndex > 1;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNext => PageIndex < TotalPage;

        public PageList(int total, int pageSize, int pageIndex, List<T> items)
        {
            this.Total = total;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Items = items;
            TotalPage = ((this.Total % this.PageSize == 0) ? (this.Total / this.PageSize) : (this.Total / this.PageSize + 1));
        }
    }
}
