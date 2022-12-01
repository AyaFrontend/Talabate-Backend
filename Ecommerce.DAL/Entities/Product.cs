using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.DAL.Entities
{
    public class Product :BaseEntity
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public string PictureUrl { set; get; }

       
        public /*virtual*/ ProductType ProductType { set; get; }
        public int ProductTypeId { set; get; }

        public /*virtual*/ ProductBrand ProductBrand { set; get; }
        public int ProductBrandId { set; get; } 

    }
}
