using System.ComponentModel.DataAnnotations;

namespace ViewModel_Pratice.Models
{
    /// <summary>
    /// 給「編輯個人資料」畫面使用的 ViewModel，只包含允許編輯欄位。
    /// </summary>
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
