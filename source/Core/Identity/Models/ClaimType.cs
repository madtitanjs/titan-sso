using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Identity.Models
{
    public class ClaimType
    {
        public ClaimType()
        {
            Type = "string";
            IsRequired = false;
        }
        public Guid Id { get; set; }

        /// <summary>
        /// Type of data being carried by the ClaimType
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Name representing the ClaimType
        /// </summary>
        public string Name { get; set; }

        public bool IsRequired { get; set; }

        private string[] ValidDataTypes = { "string", "datetime", "int" };

        protected bool IsValid
        {
            get
            {
                int idx = ValidDataTypes.IndexOf(Type);
                return idx != -1;
            }
        }
    }
}
