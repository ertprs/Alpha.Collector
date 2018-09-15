using System.Collections.Generic;

namespace Alpha.Collector.Web
{
    public class Query<TModel, TParam> where TParam : new()
    {
        #region 构造函数、内置参数

        public Query()
        {
            this.__pageIndex = 1;
            this.__pageSize = 15;
            this.__totalRecord = 0;
            this.__totalPage = 0;
            this.Params = new TParam();
        }

        public bool __defaultView { get; set; }
        public int __totalRecord { get; set; }
        public int __pageIndex { get; set; }
        public int __pageSize { get; set; }
        public int __totalPage { get; set; }
        public string __partialView { get; set; }
        public string __formId { get; set; }
        public string __containerId { get; set; }

        #endregion

        public TParam Params { get; set; }

        public IEnumerable<TModel> ItemList;

    }

    #region 分页扩展方法

    public static class QueryExtensions
    {
        public static void UpdateQuery<TModel, TParam>(this  PageList<TModel> pageList, Query<TModel, TParam> query) where TParam : new()
        {
            query.ItemList = pageList.Items;
            query.__totalRecord = pageList.Total;
            query.__totalPage = pageList.TotalPage;
        }

        public static int RowNumber<TModel, TParam>(this Query<TModel, TParam> query, int index) where TParam : new()
        {
            return index + (query.__pageIndex - 1) * query.__pageSize;
        }
    }

    #endregion

}