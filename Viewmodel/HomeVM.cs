using Estore.Areas.Admin.Models;

namespace Estore.Viewmodel
{
    public class HomeVM
    {
        public IEnumerable<Product>? Product { get; set; }
        public IEnumerable<Label>? Label { get; set; }
    }
}
