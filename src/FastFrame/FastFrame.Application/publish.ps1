#
# publish.ps1  �Զ����뷢��,���µ�Ŀ¼
# 

#Դ��Ŀ¼
$sourcePath="D:\CoreProject\FastFrame\src\FastFrame\FastFrame.Application"

#����Ŀ¼
$targetPath=".\bin\publish"

Write-Host 0,������ʼ ��ĿĿ¼: $sourcePath  ����Ŀ¼:$targetPath

#��λ����ĿĿ¼
Write-Host 1,��λ����ĿĿ¼: $sourcePath
cd $sourcePath

#���app_offline.htm�ļ�������Ŀ¼[IIS���Զ�ֹͣվ��]
Write-Host 2,���app_offline.htm�ļ�������Ŀ¼[IIS���Զ�ֹͣվ��]
New-Item $targetPath\app_offline.htm -type file -force 

#ʹ��dotnet������,���벢������ָ���ļ���
Write-Host 3,ʹ��dotnet������,���벢������ָ���ļ���:$targetPath
dotnet publish -c release -o $targetPath 

#�ӷ���Ŀ¼ɾ��app_offline.htm�ļ� [IIS���Զ�����վ��]
Write-Host 4,�ӷ���Ŀ¼ɾ��app_offline.htm�ļ� [IIS���Զ�����վ��]
Remove-Item -Path $targetPath\app_offline.htm -Force

Write-Host 5,�������

