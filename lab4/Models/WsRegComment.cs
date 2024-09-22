namespace lab3b_vd.Models;

public class WsRegComment
{
    public int Id { get; set; }
    public string SessionId { get; set; }
    public string Comment { get; set; }
    public DateTime Stamp { get; set; } = DateTime.Now;

    public int WsRefId { get; set; }
    public WsRef WsRef { get; set; }
}
