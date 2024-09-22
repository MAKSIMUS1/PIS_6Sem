using System.ComponentModel.DataAnnotations;

namespace lab3b_vd.DTO.WsRefComments;

public class WsRefCommentsAdd
{
    [Required(ErrorMessage = "WsRefId is requied")]
    public int WsRefId { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    [MinLength(2, ErrorMessage = "min length of comment is 2")]
    public string Comment { get; set; }
}
