using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BLogService.Models
{
    public class Blogs
    {
        /// <summary>
        /// Gets or Sets from Id.
        /// </summary>
        
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets from Name.
        /// </summary>
        
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets from Description.
        /// </summary>
        
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets from Owner.
        /// </summary>

        public string Owner { get; set; }

        /// <summary>
        /// Gets or Sets from CreatedAt.
        /// </summary>

        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets from UpdatedAt.
        /// </summary>
       
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// Gets or Sets from CreatedBy.
        /// </summary>
       
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets from UpdatedBy.
        /// </summary>
        
        public string ModifiedBy { get; set; }      
    }
}