using Ecommerce.DAL.Entities;
using Ecoomerce.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecoomerce.BLL.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductSpecificationParameters param)
            :base(p=> (!param.brandId.HasValue|| p.ProductBrandId == param.brandId) && (!param.typeId.HasValue || p.ProductTypeId == param.typeId)&&
            (string.IsNullOrEmpty(param.Name) || p.Name.Contains(param.Name) ))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            ApplyingPagination(param.PageSize, (param.PageIndex - 1) * param.PageSize);
         
            if(param.order == "Desc")
            { AddOrderByDesc(o => o.Price); }
            else if(param.order == "Asc")
            { AddOrderBy(o => o.Price); }
            else { AddOrderBy(o => o.Name); }
            
         }

        public ProductWithBrandAndTypeSpecification(Expression<Func<Product,bool>> criteria):base(criteria)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
