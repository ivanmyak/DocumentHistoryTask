using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentHistoryTask.Entities
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
        public Document Document { get; set; } = default!;

        [Required]
        [ForeignKey(name: nameof(Author))]
        public Guid AuthorId { get; set; }
        public User Author { get; set; } = default!;

        [Required, MaxLength(2000)]
        [Display(Name = "Текст комментария")]
        public string Text { get; set; } = default!;

        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
