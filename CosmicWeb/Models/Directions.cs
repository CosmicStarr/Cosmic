using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicWeb.Models
{
    public class Directions
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public double Elevation { get; set; }
        public enum DifficultyType { Easy, Medium, Hard, Extreme }

        public DifficultyType Difficulty { get; set; }
        [Required]
        public int CosmicSpotId { get; set; }
        public CosmicSpot CosmicSpot { get; set; }
    }
}
