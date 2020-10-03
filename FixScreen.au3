#include <FileConstants.au3>

Func ChangeHertz()
    $sFilePath =  @ScriptDir & "\second-screen-refresh-rate.txt"
    $hFilehandle = FileOpen($sFilePath, $FO_READ)
    $previousRefreshRate = FileRead($sFilePath)
    FileClose($hFilehandle)
    $hFilehandle = FileOpen($sFilePath, $FO_OVERWRITE)
    If ($previousRefreshRate == "60") Then
        FileWrite($hFilehandle, "50")
        $hertz="50";
    Else
        FileWrite($hFilehandle, "60")
        $hertz="60";
    EndIf
    FileClose($hFilehandle)
    Return $hertz
EndFunc

Func Main()
    if (ChangeHertz() == "60") Then
        ShellExecute(@ScriptDir & "\SetScreenTo60.bat")
    Else
        ShellExecute(@ScriptDir & "\SetScreenTo50.bat")
    EndIf
EndFunc

Main()