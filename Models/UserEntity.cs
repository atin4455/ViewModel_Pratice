namespace ViewModel_Pratice.Models
{
    /// <summary>
    /// 模擬資料庫中的使用者實體。
    /// 實務上通常不會直接拿這個型別做表單綁定。
    /// </summary>
    public class UserEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // 敏感欄位：一般使用者不應透過一般編輯表單直接改到
        public bool IsAdmin { get; set; }
    }
}
