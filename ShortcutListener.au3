HotKeySet("^+f", "Main") ; ^ = Ctrl, look in Send() to see the correct syntax.

While 1
    Sleep(10)
WEnd

Func Main()
    ShellExecute(@ScriptDir & "\FixScreen.au3")
EndFunc 