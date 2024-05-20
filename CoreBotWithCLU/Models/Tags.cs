using System;
using System.Collections.Generic;

namespace CoreBotCLU.Models
{
    public partial class Tags
    {
        public Tags()
        {
            Taggableitems = new HashSet<Taggableitems>();
        }

        public Guid TagId { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<Taggableitems> Taggableitems { get; set; }
    }
}
