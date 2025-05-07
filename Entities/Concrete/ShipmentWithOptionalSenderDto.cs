using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ShipmentWithOptionalSenderDto
    {
        public Shipment Shipment { get; set; }
        public Sender? Sender { get; set; } 
    }
}
