# Telligent.Core

## 版本管理

1. 版本號可透過執行 Telligent.Core.VersionGenerator 產生器產生（不需修改程式）

   * major: 主要版本號。目前為 3
   * minor: 次要版本號。目前為 0
   * build: 經過日期（基礎日期為 2022/08/18）
   * revision: 當天總秒數

2. nuget 上版語法

   ```powershell
   // 記得先 cd 至專案根目錄，並且先透過 Visual Studio 重建並產生套件
   // Telligent.Core.Application 更換為要上版的套件
   // 3.0.0.26301 修改為上版的版號
   nuget push .\Telligent.Core.Application.3.0.0.26301.nupkg f870e5b6-a357-4558-8f12-2adf56c29197 -Source http://teola.3rdchannel.com.tw/Nuget/nuget/
   ```

   

