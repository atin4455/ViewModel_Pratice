namespace ViewModel_Pratice.Models
{
    /// <summary>
    /// 專門給「儀表板」畫面用的資料形狀：只包含 View 需要顯示的欄位，
    /// 不必與資料庫 Entity 或 API DTO 一模一樣。
    /// </summary>
    public class DashboardViewModel
    {
        public string PageTitle { get; set; } = string.Empty;

        public string Greeting { get; set; } = string.Empty;

        /// <summary>畫面上要列出的待辦標題（已是顯示用字串）。</summary>
        public IReadOnlyList<string> TodoTitles { get; set; } = Array.Empty<string>();

        public int TodoCount => TodoTitles.Count;
    }
}
