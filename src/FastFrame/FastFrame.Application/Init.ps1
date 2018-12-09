#
# Init.ps1  下载并安装 .net core IIs 寄主环境  by gongjie@qq.com
#

$name="API"
$path="./"
$port="8080"

#下载安装文件
Invoke-WebRequest -Uri "https://download.visualstudio.microsoft.com/download/pr/48adfc75-bce7-4621-ae7a-5f3c4cf4fc1f/9a8e07173697581a6ada4bf04c845a05/dotnet-hosting-2.2.0-win.exe" -OutFile "DotNetCore.WindowsHosting.exe"

#执行安装文件
Start-Process "DotNetCore.WindowsHosting.exe" -Wait

#停止IIS进程 
Invoke-Expression "net stop was /y"

#启动IIS进程 
Invoke-Expression "net start w3svc"

#检查是否安装成功
Get-WebGlobalModule -Name AspNetCoreModule -ErrorAction Ignore

#创建一个应用程序池
New-Item -path IIS:\AppPools\$name

#指定应用程序池为:无托管代码
Set-ItemProperty -Path IIS:\AppPools\$name -Name managedRuntimeVersion -Value ''

#创建一个网站
New-Website -name TestSite -PhysicalPath $path -ApplicationPool $name -Port $port

#打开浏览器,尝试访问这个网站
$url="cmd.exe /C start http://localhost:{0}" -f($port)
Invoke-Expression $url 
