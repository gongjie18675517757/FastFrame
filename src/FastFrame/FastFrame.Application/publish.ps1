#
# publish.ps1  自动编译发布,更新到目录
# 

#源码目录
$sourcePath="D:\CoreProject\FastFrame\src\FastFrame\FastFrame.Application"

#发布目录
$targetPath=".\bin\publish"

Write-Host 0,发布开始 项目目录: $sourcePath  发布目录:$targetPath

#定位到项目目录
Write-Host 1,定位到项目目录: $sourcePath
cd $sourcePath

#添加app_offline.htm文件到发布目录[IIS会自动停止站点]
Write-Host 2,添加app_offline.htm文件到发布目录[IIS会自动停止站点]
New-Item $targetPath\app_offline.htm -type file -force 

#使用dotnet命令行,编译并发布到指定文件夹
Write-Host 3,使用dotnet命令行,编译并发布到指定文件夹:$targetPath
dotnet publish -c release -o $targetPath 

#从发布目录删除app_offline.htm文件 [IIS会自动启动站点]
Write-Host 4,从发布目录删除app_offline.htm文件 [IIS会自动启动站点]
Remove-Item -Path $targetPath\app_offline.htm -Force

Write-Host 5,发布完成

