@echo off

RMDIR C:\Projects\PoToPe\trunk\webservices\src\Utopia.UserManager /s /q

nant\bin\nant.exe %*

MOVE C:\Projects\PoToPe\trunk\webservices\src\ProjectTemplate\out\Utopia.UserManager C:\Projects\PoToPe\trunk\webservices\src\