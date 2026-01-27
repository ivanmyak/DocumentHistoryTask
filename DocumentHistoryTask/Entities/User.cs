using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace DocumentHistoryTask.Entities
{
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Дата и время регистрации пользователя
        /// </summary>
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTimeOffset RegisteredAt { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// Документы, принадлежащие пользователю
        /// </summary>
        public ICollection<Document> Documents { get; set; } = new List<Document>();

    }
}

