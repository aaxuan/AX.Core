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

        public int Count { get; set; }

        public int TotalCount { get; set; }

        public List<T> Data { get; set; }
    }
}