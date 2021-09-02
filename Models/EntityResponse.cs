using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SpectrumTeamService.Models
{
    public class EntityResponse<T>
    {
        /// <summary>
        /// Gets or Sets from ResponseCode.
        /// </summary>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or Sets from ResponseMessage.
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Gets or Sets from IdTransactionCode.
        /// </summary>
        public string IdTransactionCode { get; set; }

        /// <summary>
        /// Gets or Sets from RowsAffected.
        /// </summary>
        public int RowsAffected { get; set; }

        /// <summary>
        /// Gets or Sets from ResultData.
        /// </summary>
        public Collection<T> ResultData { get; set; }

        /// <summary>
        /// Gets or Sets from Authorization.
        /// </summary>
        public string Authorization { get; set; }
    }
}