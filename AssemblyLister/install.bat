copy AssemLst.exe %windir%\Microsoft.net\Framework\v2*\
copy AssemLst64.exe %windir%\Microsoft.net\Framework64\v2*\AssemLst.exe
%windir%\Microsoft.net\Framework64\v2*\AssemLst.exe -ngen %windir%\Microsoft.net\Framework64\v2*\AssemLst.exe
%windir%\Microsoft.net\Framework\v2*\AssemLst.exe -ngen %windir%\Microsoft.net\Framework\v2*\AssemLst.exe
