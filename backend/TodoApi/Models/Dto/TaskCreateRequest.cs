namespace TodoApi.Models.Dto;

public class TaskCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
}