using Ecommerce.DAL.Entities;
using Ecoomerce.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecoomerce.BLL.Specifications
{
    public class ProductSpecificationWithFilterFourCount : BaseSpecification<Product>
    {
        public ProductSpecificationWithFilterFourCount(ProductSpecificationParameters param)
            : base(p => (!param.brandId.HasValue || p.ProductBrandId == param.brandId) && (!param.typeId.HasValue || p.ProductTypeId == param.typeId)
            &&(string.IsNullOrEmpty(param.Name) || p.Name.Contains(param.Name)))
        { }
    }
}
