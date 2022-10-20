const int major = 3;
const int minor = 0;

// 初始開發時間
var date = new DateTime(2022, 8, 18);

var build = DateTime.Today.Subtract(date).Days;
var revision = (int)DateTime.Now.Subtract(DateTime.Today).TotalSeconds / 2;

Console.WriteLine($"{major}.{minor}.{build}.{revision}");