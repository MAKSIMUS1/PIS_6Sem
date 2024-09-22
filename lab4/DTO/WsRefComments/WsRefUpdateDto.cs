using System.ComponentModel.DataAnnotations;

namespace lab3b_vd.DTO.WsRefComments;

public class WsRefUpdateDto
{
    public int Id { get; set; }

    [MinLength(2, ErrorMessage = "Comment min length is 5")]
    public string Comment { get; set; }
}
