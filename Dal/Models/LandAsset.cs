using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dal.Models
{
    [Table("LandAssets")]
	public class LandAsset
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public required string ObjectName { get; set; }

        public required string Owner { get; set; }

        public required string Fullname { get; set; }

        [EnumDataType(typeof(LandType), ErrorMessage = "Недопустимое значение для типа земельного актива")]
        public required LandType Type { get; set; }

        [EnumDataType(typeof(DealStageEnum), ErrorMessage = "Недопустимое значение для статуса")]
        public required DealStageEnum DealStage { get; set; }

        //[JsonIgnore]
        //[ForeignKey("ForeignId")]
        //public int ForeignId { get; set; }

        //public virtual Contragent Contragent { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
    }
}

