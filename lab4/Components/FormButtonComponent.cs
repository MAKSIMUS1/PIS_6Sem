namespace lab3b_vd.Components;

public class FormButtonComponent
{
    public string Text { get; set; }
    public string ClassName { get; set; }
    public string FormClassName { get; set; }
    public string Method { get; set; }
    public string Action { get; set; }
    public string[] HiddenKeys { get; set; } = [];
    public string[] HiddenValues { get; set; } = [];
}
