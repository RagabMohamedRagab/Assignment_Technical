

using System.ComponentModel.DataAnnotations;

namespace Task.Application.ViewModel
{
    public class GetAllProductViewModel
    {
       
        public List<Data> datas { get; set; }
        public int TotalRecord {  get; set; }
       
    }

    public class Data
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Unit { get; set; }
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}
