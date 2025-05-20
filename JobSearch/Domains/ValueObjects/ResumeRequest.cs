namespace JobSearch.Domains.ValueObjects;

/// <summary>
/// Представляет значение для создания или обновления резюме пользователя.
/// </summary>
public class ResumeRequest
{
    /// <summary>
    /// Идентификатор пользователя, которому принадлежит резюме.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Заголовок резюме.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Информация об образовании пользователя.
    /// </summary>
    public required string Education { get; set; }

    /// <summary>
    /// Опыт работы пользователя.
    /// </summary>
    public required string Experience { get; set; }
}