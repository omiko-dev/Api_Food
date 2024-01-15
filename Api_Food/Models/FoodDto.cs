using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Food.Models
{
    public class FoodDto
    {
        public int Id { get; set; }

        public string Meal { get; set; }

        public string DrinkAlternate { get; set; }

        public string Category { get; set; }

        public string Area { get; set; }

        public string Instructions { get; set; }

        public string MealThumb { get; set; }

        public string Tags { get; set; }

        public string Youtube { get; set; }

        [NotMapped]
        public List<string>? Ingredients
        {
            get => JsonConvert.DeserializeObject<List<string>>(IngredientsJson!);
            set => IngredientsJson = JsonConvert.SerializeObject(value);
        }

        [Column("Ingredient")]
        public string? IngredientsJson { get; set; }



        [NotMapped]
        public List<string>? Allergens
        {
            get => JsonConvert.DeserializeObject<List<string>>(MeasuresJson!);
            set => MeasuresJson = JsonConvert.SerializeObject(value);
        }

        [Column("Measures")]
        public string? MeasuresJson { get; set; }

        public string Source { get; set; }

        public string ImageSource { get; set; }

        public string CreativeCommonsConfirmed { get; set; }

        public string DateModified { get; set; }
    }
}
