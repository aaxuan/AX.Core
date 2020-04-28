using System.Collections.Generic;

namespace AX.Core.CommonModel
{
    public class SearchFilter
    {
        public SearchFilter()
        {
            WhereFilter = new List<SearchWhereFilter>();
        }

        public int Count { get; set; }

        public int Page { get; set; }

        public string OrderBy { get; set; }

        public List<SearchWhereFilter> WhereFilter { get; set; }

        public bool HasPage
        {
            get
            {
                if (Count <= 0)
                { return false; }
                if (Page < 0)
                { return false; }
                return true;
            }
        }

        public int GetBeginIndex()
        {
            if (Page <= 0) { Page = 1; }
            if (Count <= 0) { Count = 20; }
            return (Page - 1) * Count;
        }
    }

    public class SearchWhereFilter
    {
        public string FilterName { get; set; }

        public string FilterType { get; set; }

        public string FilterValue { get; set; }

        public bool IsVaild
        {
            get
            {
                if (string.IsNullOrWhiteSpace(FilterName))
                { return false; }
                if (string.IsNullOrWhiteSpace(FilterType))
                { return false; }
                if (GetVaildFilderType() == string.Empty)
                { return false; }
                if (GetVaildFilderType() != "in" && string.IsNullOrWhiteSpace(FilterValue))
                { return false; }

                return true;
            }
        }

        public string GetVaildFilderType()
        {
            switch (FilterType)
            {
                case "=": return "=";
                case "<": return "<";
                case "<=": return "<=";
                case ">": return ">";
                case ">=": return ">=";
                case "in": return "in";
                case "like": return "like";
                default: return string.Empty;
            }
        }
    }
}