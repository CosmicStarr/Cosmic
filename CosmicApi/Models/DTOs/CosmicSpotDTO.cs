﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.Models.DTOs
{
    public class CosmicSpotDTO
    {
   
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public DateTime Created { get; set; }
        public byte[] Images { get; set; }
        public DateTime Established { get; set; }
    }
}
