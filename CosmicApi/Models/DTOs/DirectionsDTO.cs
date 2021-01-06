using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static CosmicApi.Models.Directions;

namespace CosmicApi.Models.DTOs
{
    public class DirectionsDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public double Elevation { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int CosmicSpotId { get; set; }
        public CosmicSpotDTO CosmicSpot { get; set; }
    }
}
