using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class RentViewModel : IValidatableObject
    {
        public bool IsChecked { get; set; }
        public int InventoryId { get; set; }
        public string EquipmentName { get; set; }
        public string TypeName { get; set; }

        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Field accept numberic and higher than 0")]
        public string Days { get; set; }     
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsChecked && string.IsNullOrEmpty(Days))
            {
                yield return new ValidationResult("Days cannot be null for " + EquipmentName + " when days selected");             
            }
            if (!IsChecked && !string.IsNullOrEmpty(Days))
            {
                yield return new ValidationResult("Selection for " + EquipmentName+ " cannot be null when days selected");
            }            
        }
    }
}
