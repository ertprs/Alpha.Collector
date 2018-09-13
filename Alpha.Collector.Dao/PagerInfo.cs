namespace Alpha.Collector.Dao
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PagerInfo
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        private string _pkField = "id";
        /// <summary>
        /// 主键名称
        /// </summary>
        public string PKField
        {
            get
            {
                return this._pkField;
            }
            set
            {
                this._pkField = value;
            }
        }

        private string _returnField = "*";
        /// <summary>
        /// 返回列 逗号分隔
        /// </summary>
        public string ReturnField
        {
            get
            {
                return this._returnField;
            }
            set
            {
                this._returnField = value;
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string sord { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (records > 0)
                {
                    return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
