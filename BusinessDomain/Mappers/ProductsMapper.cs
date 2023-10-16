using BusinessDomain.DTOs;
using CommonDataModels.DataModels;

namespace BusinessDomain.Mappers
{
    internal static class ProductsMapper
    {
        internal static ProductRespDTO FromProductToProductDTO(Product product, string userName, MoneyAccount moneyAccount, TypeProduct typeProduct, TypeStatus typeStatus)
        {
            return new ProductRespDTO()
            {
                ProductNumber = product.ProductNumber,
                CreationDate = product.CreationDate,
                ClientUserName = userName,
                MoneyAccount = new MoneyAccountDTO()
                {
                    Id = moneyAccount.Id,
                    IdProduct = moneyAccount.IdProduct,
                    CurrentBalance = moneyAccount.CurrentBalance,
                    LastUpdateBalance = moneyAccount.LastUpdateBalance,
                },
                TypeProduct = new TypeProductDTO()
                {
                    Code = typeProduct.Code,
                    Name = typeProduct.Name,
                    Description = typeProduct.Description,
                    IsEnabled = typeProduct.IsEnabled
                },
                TypeStatus = new TypeStatusDTO()
                {
                    Code = typeStatus.Code,
                    Name = typeStatus.Name,
                    Description = typeStatus.Description,
                    IsEnabled = typeStatus.IsEnabled
                }
            };
        }
    }
}
