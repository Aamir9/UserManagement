using System.ComponentModel;

namespace AutoSmartTechAPI
{
    public class SortExpression
    {
        public SortExpression(string sortBy, ListSortDirection sortDirection)
        {
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        public string SortBy { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}
