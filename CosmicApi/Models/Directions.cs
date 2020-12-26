using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicApi.Models
{
    public class Directions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultyType { Easy, Medium, Hard, Extreme }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int CosmicSpotId { get; set; }
        [ForeignKey("CosmicSpotId")]
        public CosmicSpot CosmicSpot { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
