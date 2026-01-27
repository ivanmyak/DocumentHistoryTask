using DocumentHistoryTask.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentHistoryTask.Entities
{
    /// <summary>
    /// Основная сущность документа, принадлежащего пользователю.
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Уникальный идентификатор документа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Владелец документа.
        /// </summary>
        [ForeignKey(name: nameof(Owner))]
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = default!;

        /// <summary>
        /// Ссылка на оригинал или версию (а-ля направленный список)
        /// </summary>
        [ForeignKey(name: nameof(Parent))]
        public Guid? ParentId { get; set; }
        public Document? Parent { get; set; }


        [Required, MaxLength(200)]
        [Display(Name = "Название документа")]
        public string Name { get; set; } = default!;

        [MaxLength(2000)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        /// <summary>
        /// Текущий статус документа.
        /// </summary>
        [Display(Name = "Статус документа")]
        public DocumentStatus Status { get; set; } = DocumentStatus.Active;

        /// <summary>
        /// Признак временного документа (копия для шаринга).
        /// </summary>
        public bool IsTemporary { get; set; } = false;

        /// <summary>
        /// Токен для доступа к временной копии.
        /// </summary>
        [MaxLength(100)]
        public string? ShareToken { get; set; }

        /// <summary>
        /// Срок действия временной копии.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTimeOffset? ExpiresAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTimeOffset? UpdatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTimeOffset HandleByAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        [DataType(DataType.DateTime)]
        public DateTimeOffset? DeletedAt { get; set; }

        [Required, MaxLength(500)]
        public string StoragePath { get; set; } = default!;

        public long FileSize { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }


}
