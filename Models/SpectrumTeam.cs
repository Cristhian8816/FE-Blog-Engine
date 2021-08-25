using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpectrumTeamService.Models
{
    public class Languages
    {
        /// <summary>
        /// Gets or Sets from Id.
        /// </summary>
        
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets from Name.
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets from Code.
        /// </summary>
        
        public string Code { get; set; }

        /// <summary>
        /// Gets or Sets from CreatedAt.
        /// </summary>
       
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or Sets from UpdatedAt.
        /// </summary>
       
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets or Sets from CreatedBy.
        /// </summary>
       
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets from UpdatedBy.
        /// </summary>
        
        public string UpdatedBy { get; set; }
    }
}