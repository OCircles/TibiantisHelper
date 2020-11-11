@echo off

for /f %%1 in ('dir /b *.mon') do (
	rename %%1 %%~n1.txt
)

pause > nul