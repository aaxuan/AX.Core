using System.Collections.Generic;

namespace AX.Core.CommonModel
{
    public class PageDateModel<T>
    {
        public PageDateModel()
        { }

        public PageDateModel(int totalCount, List<T> data)
        {
            Data = data;
            TotalCount = totalCount;
            Count = data.Count;
        }

        /// <summary>
        /// 当前Page数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}