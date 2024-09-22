namespace lab3b_vd.Models;

public class WsRef
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public int Minus { get; set; } = 0;
    public int Plus { get; set; } = 0;

    public ICollection<WsRegComment> Comments { get; set; }
}
