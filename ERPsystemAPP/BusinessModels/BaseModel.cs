using System.ComponentModel.DataAnnotations;

namespace ERPsystemAPP.BusinessModels
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; }
    }

}
