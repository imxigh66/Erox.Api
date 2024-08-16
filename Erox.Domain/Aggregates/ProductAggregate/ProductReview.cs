using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.ProductAggregate
{
    public class ProductReview
    {
       
        public Guid ReviewId { get; private set; }
        public Guid Productid { get; private set; }
        public string Text { get; private set; }
        public Guid UserProfieId { get; private set; }
        public string Rating { get; private set; }
        public bool IsApproved { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }
    }
}
