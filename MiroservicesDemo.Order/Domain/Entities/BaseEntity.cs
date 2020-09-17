using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiroservicesDemo.Order.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id {get; set;}

        protected BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }

        protected BaseEntity(Guid id)
        {
            this.Id = id;
        }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }        
    }
}
