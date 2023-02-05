using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopBridgeAPI.Models;
using ShopBridgeAPI.Models.Dtos;
using ShopBridgeAPI.Models.Enumerations;
using ShopBridgeAPI.Services.DataServices.Interfaces;
using System.Data;
using System.Text;

namespace ShopBridgeAPI.Services.DataServices
{
    public class GetProductService : IGetProductService
    {
        private readonly IDbConnection connection;

        public GetProductService(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("ShopBridgeDBConnection"));
        }

        public async Task<FetchProductResponseDto?> GetByIdAsync(int productId)
        {
            var request = new FetchProductRequestDto { ProductId = productId };
            return (await GetAsync(request)).SingleOrDefault();
        }

        public async Task<IEnumerable<FetchProductResponseDto>> GetAsync(FetchProductRequestDto requestDto)
        {
            var parameters = new DynamicParameters();
            return await connection.QueryAsync<FetchProductResponseDto>(FetchQueryBuiler(requestDto, parameters), parameters);
        }

        private string FetchQueryBuiler(FetchProductRequestDto requestDto, DynamicParameters parameters)
        {
            var stringBuilder = new StringBuilder()
                .Append("SELECT p.Id AS ProductId, p.name AS ProductName, p.Description as Description, p.price as Price")
                .Append(", c.Id AS CategoryId, c.Name AS CategoryName")
                .Append(", p.price - (p.price * c.DiscountPercentage/100) AS DiscountPrice")
                .Append(" FROM Product p")
                .Append(" JOIN Category c ON c.Id = p.categoryId")
                .Append(" WHERE 1=1 ");

            if (requestDto.ProductId != 0)
            {
                stringBuilder.Append(" AND p.Id = @ProductId");
                parameters.Add("ProductId", requestDto.ProductId);
            }

            if (requestDto.Categories?.Any() == true)
            {
                stringBuilder.Append(" AND c.Name IN @Categories");
                parameters.Add("Categories", requestDto.Categories);
            }

            if (!string.IsNullOrWhiteSpace(requestDto.SearchTerm))
            {
                stringBuilder.Append(" AND (c.Name LIKE Concat('%',@SearchTerm,'%')")
                .Append(" OR p.Name LIKE Concat('%',@SearchTerm,'%')")
                .Append(" OR p.description LIKE Concat('%',@SearchTerm,'%'))");
                parameters.Add("SearchTerm", requestDto.SearchTerm);
            }


            AddOrderByQuery(stringBuilder, requestDto);
            AddLimitQuery(stringBuilder, parameters, requestDto);
            return stringBuilder.ToString();
        }

        private void AddLimitQuery(StringBuilder stringBuilder, DynamicParameters parameters, FetchProductRequestDto requestDto)
        {
            if (requestDto.PageSize == 0)
            {
                return;
            }

            parameters.Add("OffSet", (requestDto.PageNumber - 1) * requestDto.PageSize);
            stringBuilder.Append(" OFFSET @OffSet ROWS");

            parameters.Add("Limit", requestDto.PageSize);
            stringBuilder.Append(" FETCH NEXT @Limit ROWS ONLY");
        }

        private void AddOrderByQuery(StringBuilder stringBuilder, FetchProductRequestDto requestDto)
        {
            var columnName = "p.Id";
            switch (requestDto.SortProperty)
            {
                case OrderByColumn.ProductName:
                    columnName = "p.Name";
                    break;
                case OrderByColumn.CategoryName:
                    columnName = "c.Name";
                    break;
                case OrderByColumn.Price:
                    columnName = "p.Price";
                    break;

            }

            var sortDirection = requestDto.SortDirection == "DESC" ? "DESC" : "ASC";
            stringBuilder.Append($" ORDER BY {columnName} {sortDirection}");
        }
    }
}
