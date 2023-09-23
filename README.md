# generalMyLauncher

## Compile comand example

input " **cscPath options targetSourceFilePath** " on Windows Power Shell.

e.g.  
 `C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:winexe /out:MyApp2.exe /win32icon:favicon.ico generalMyLauncher.cs`

### compile option

- **/t:winexe**  
  need for create Windows Form Application.
  <span style="color: green;">[must]</span>
- **/out:{fileName}**  
  create exe file named input name.
  <span style="color: green;">[optional]</span>
- **/win32icon:{filePath}**  
  exe file icon.
  <span style="color: green;">[optional]</span>

## file composition

- generalMyLauncher.cs
- favicon.ico
- settingList.xml

※ Please put "favicon.ico" as you like. If you don't need customize icon, you could compile without favicon.ico.
