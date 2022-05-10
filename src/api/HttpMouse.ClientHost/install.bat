CHCP 65001
@echo off
color 0e
@echo ==================================
@echo 提醒：请右键本文件，用管理员方式打开。
@echo ==================================
@echo Start Install HttpMouse.ClientHost

sc create HttpMouse.ClientHost binPath=%~dp0\HttpMouse.ClientHost.exe start= auto 
sc description HttpMouse.ClientHost "内网穿透服务"
Net Start HttpMouse.ClientHost
pause