#
# Init.ps1  ���ز���װ .net core IIs ��������  by gongjie@qq.com
#

$name="API"
$path="./"
$port="8080"

#���ذ�װ�ļ�
Invoke-WebRequest -Uri "https://download.visualstudio.microsoft.com/download/pr/48adfc75-bce7-4621-ae7a-5f3c4cf4fc1f/9a8e07173697581a6ada4bf04c845a05/dotnet-hosting-2.2.0-win.exe" -OutFile "DotNetCore.WindowsHosting.exe"

#ִ�а�װ�ļ�
Start-Process "DotNetCore.WindowsHosting.exe" -Wait

#ֹͣIIS���� 
Invoke-Expression "net stop was /y"

#����IIS���� 
Invoke-Expression "net start w3svc"

#����Ƿ�װ�ɹ�
Get-WebGlobalModule -Name AspNetCoreModule -ErrorAction Ignore

#����һ��Ӧ�ó����
New-Item -path IIS:\AppPools\$name

#ָ��Ӧ�ó����Ϊ:���йܴ���
Set-ItemProperty -Path IIS:\AppPools\$name -Name managedRuntimeVersion -Value ''

#����һ����վ
New-Website -name TestSite -PhysicalPath $path -ApplicationPool $name -Port $port

#�������,���Է��������վ
$url="cmd.exe /C start http://localhost:{0}" -f($port)
Invoke-Expression $url 
