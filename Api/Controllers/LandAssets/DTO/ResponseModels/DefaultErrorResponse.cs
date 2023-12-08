using System.ComponentModel.DataAnnotations;


namespace Api.Controllers.LandAssets.DTO.ResponseModels
{
    public class DefaultErrorResponseModel
    {
        [Display(Name = "detail")]
        public string Detail { get; set; }
    }
}

