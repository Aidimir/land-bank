using System;
using Dal.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.LandAssets.DTO.RequestModels
{
    public class LandAssetRequestModel
    {
        [Required]
        [MinLength(2)]
        public string Owner { get; set; }

        [Required]
        [MinLength(2)]
        public string ObjectName { get; set; }

        [Required]
        [MinLength(2)]
        public string Fullname { get; set; }

        [EnumDataType(typeof(LandType), ErrorMessage = "Недопустимое значение для типа земельного актива")]
        public required LandType Type { get; set; }

        [EnumDataType(typeof(DealStageEnum), ErrorMessage = "Недопустимое значение для статуса")]
        public required DealStageEnum DealStage { get; set; }

        public LandAsset CreateLandAsset()
        {
            return new LandAsset { DealStage = DealStage, Fullname = Fullname, Owner = Owner, Type = Type, ObjectName = ObjectName };
        }
    }
}

