using System;
using System.Collections.Generic;
using System.Text;

namespace ND.ManagementSvcs.Dtos.GroupShopping.Responses
{
    public class GetFoundResponse : ResponseBase
    {
        public int GroupId { get; set; }
        public int HeadId { get; set; }
        public int Sku { get; set; }
        public decimal ActualPrice { get; set; }
        public byte HeadStatus { get; set; }
        public byte GroupStatus { get; set; }
        public DateTime? DtBegin { get; set; }
        public DateTime? DtEnd { get; set; }
        public DateTime DtInserted { get; set; }
        public string InsertedBy { get; set; }
        public DateTime DtLastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
