﻿namespace OrderManagementAPI.Aplication.DTOs
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }    // Total de registros encontrados
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
