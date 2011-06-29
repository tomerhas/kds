Dim appExcel 'As Excel.Application
Dim wbExcel 'As Excel.Workbook 'Classeur Excel
Dim wsExcel 'As Excel.Worksheet 'Feuille Excel
dim filename,args
Set args = WScript.Arguments
filename= args(0) 

Set appExcel = CreateObject("Excel.Application")
Set wbExcel = appExcel.Workbooks.Open(filename,3)
wbExcel.Save 

wbExcel.Close
appExcel.Quit
set wbExcel=nothing
set appExcel=nothing