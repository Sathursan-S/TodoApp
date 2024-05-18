using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TodoApi.Models;

[Table("Tasks")]
public class TodoTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title length can't be more than 100 characters.")]
    public string Title { get; set; }

    [StringLength(600, ErrorMessage = "Description length can't be more than 600 characters.")]
    public string Description { get; set; }

    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Priority Priority { get; set; } = Priority.Low;
}

public enum Priority
{
    Low,
    Medium,
    High
}