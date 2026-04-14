# ViewModel_Pratice

## ViewModel 與 Over-Posting 測試說明

此專案包含一個 `BindingDemo` 頁面，用來示範：

- `UpdateBad(UserEntity model)`：直接綁定 Entity（危險）
- `UpdateGood(EditProfileViewModel vm)`：綁定 ViewModel（安全）

### 1) 啟動專案

在專案根目錄執行：

```bash
dotnet run
```

開啟瀏覽器進入：

- `https://localhost:44375/Home/BindingDemo`
  - 若你的埠號不同，請以終端機顯示為準

### 2) 一般送出（正常流程）

在 `BindingDemo` 畫面中：

1. 在左側「危險：直接綁定 Entity」表單修改 `Name` 或 `Email`，按送出
2. 在右側「安全：只綁定 ViewModel」表單修改 `Name` 或 `Email`，按送出

兩者都會更新一般欄位，這是正常現象。

### 3) 快速重現 over-posting（直接用畫面欄位）

在 `BindingDemo` 畫面中：

1. 左側「危險：直接綁定 Entity」勾選 `IsAdmin` 後送出
2. 重新整理頁面，觀察「目前資料庫資料」中的 `IsAdmin`

預期結果：

- `UpdateBad` 會讓 `IsAdmin` 被改成 `True`
- 這就是 over-posting / mass assignment 風險

### 4) 比較 `UpdateGood`（ViewModel）

在右側「安全：只綁定 ViewModel」同樣勾選 `IsAdmin` 後送出，會看到：

- `Name`、`Email` 可更新
- `IsAdmin` 不會被改動

原因是 `EditProfileViewModel` 沒有 `IsAdmin` 欄位，模型繫結不會把該值綁進來，並且 Controller 只手動映射允許欄位。

### 5) 進階：用 Network 手動改請求

若你想模擬「真正攻擊者手動改 payload」，可用 DevTools：

1. 先送出一次左側 `UpdateBad` 表單
2. 在 Network 找到 `UpdateBad` 請求，右鍵複製為 `fetch`
3. 到 Console 貼上該 `fetch(...)`
4. 在 `body` 字串最後加上 `&IsAdmin=true`
5. 執行後回到頁面重新整理，觀察 `IsAdmin`

`body` 範例（重點是最後一段）：

```text
Id=1&Name=王小明&Email=ming%40example.com&__RequestVerificationToken=...&IsAdmin=true
```

### 6) 常見問題

- **403 / 驗證失敗**：防偽權杖過期，請重新整理頁面再重新複製 fetch
- **看不到變化**：請確認送的是 `UpdateBad`，並重新整理 `BindingDemo` 頁面