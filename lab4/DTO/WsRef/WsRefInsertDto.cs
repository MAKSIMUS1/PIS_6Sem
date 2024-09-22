using System.ComponentModel.DataAnnotations;

namespace lab3b_vd.DTO.WsRef;

public class WsRefInsertDto
{
    [Required(ErrorMessage = "Url is required")]
    [MinLength(5, ErrorMessage = "Url min length is 5")]
    [RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$", ErrorMessage = "Url is not valid")]
    public string Url { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(2, ErrorMessage = "Description min length is 2")]
    public string Description { get; set; }
}
