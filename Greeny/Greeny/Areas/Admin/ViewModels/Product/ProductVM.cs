using Greeny.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Greeny.Areas.Admin.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public decimal Price { get; set; }
        public bool IsStock { get; set; }
        public int SaleCount { get; set; }
        public int RateCount { get; set; }
        public int ProductCount { get; set; }
        public string CategoryName { get; set; }      
        public string BrandName { get; set; }     
        public int SkuCode { get; set; }
        public string Images { get; set; }
        
    }
}
