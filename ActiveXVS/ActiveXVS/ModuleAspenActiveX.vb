Option Explicit On
Imports System.Runtime.InteropServices
Module AspenActiveX
    Public myDSTWU As New DSTWU
    Public myRadFrac As New RadFrac
    Public CellList() As Integer
    Public Application_path As String

    Public Structure Node
        Dim Row As Integer
        Dim deep As Integer
        Dim deepin() As String
        Dim Name As String
        Dim value As String
        Dim address As String
    End Structure

    Public Structure Triple_Result

        Dim value As String
        Dim Name As String
        Dim UnitString As String
    End Structure
    Public Structure Multi_Result

        Dim number As Integer       '记录塔板位置
        Dim HYD_TL As Double
        Dim HYD_TVTO As Double        'Temperature vapor to
        Dim HYD_LMF As Double         'Mass flow liquid from:
        Dim HYD_VMF As Double         'Mass flow vapor to
        Dim HYD_LVF As Double         'Volume flow liquid from
        Dim HYD_VVF As Double         'Volume flow vapor to
        Dim HYD_MWL As Double         'Molecular wt liquid from:
        Dim HYD_MWV As Double         'Molecular wt vapor to
        Dim HYD_RHOL As Double        'Density liquid from
        Dim HYD_RHOV As Double        'Density vapor to
        Dim HYD_MUL As Double         'Viscosity liquid from
        Dim HYD_MUV As Double         'Viscosity vapor to
        Dim HYD_STEN As Double        'Surface tension liquid from
        Dim HYD_FMIDX As Double       'Foaming Index
        Dim HYD_PARM As Double        'Flow parameter
        Dim HYD_QR As Double          'Reduced vapor
        Dim HYD_FFR As Double         'Reduced F factor 
    End Structure

    Sub OpenSimulation()
        'old path = "C:\Users\57584\Documents\GitHub\Ditill\Distill\AspenActiveX\ActiveXVS\ActiveXVS\bin\Debug\AspenBKP\"
        Dim AspenPlusDistilR As HappLS
        Dim AspenPlusRadFrac1 As HappLS
        Dim path As String = ""
        Dim path_Array() As String = Split(Application_path, "\")
        On Error GoTo ErrorHandler

        For i = 0 To path_Array.Count - 2   '去除文件名
            path = path & path_Array(i) & "\"
        Next

        ' open existing simulation
        AspenPlusDistilR = GetObject(path & "DistilR.apwz")
        AspenPlusRadFrac1 = GetObject(path & "RadFrac1.apwz")
        myDSTWU.ihApsim = AspenPlusDistilR.Application
        myRadFrac.ihAPsim = AspenPlusRadFrac1.Application
        ' display the GUI
        myDSTWU.ihApsim.Visible = True
        myRadFrac.ihAPsim.Visible = True
        ' run the simulation
        myDSTWU.ihApsim.Run()
        myRadFrac.ihAPsim.Run()

        Exit Sub
ErrorHandler:
        MsgBox("OpenSimulation raised error " & Err.Description)
        End
    End Sub

    Sub CloseSimulation(ByVal ihAPsim As IHapp)

        On Error GoTo ErrorHandler
        'Quit without saving
        ihAPsim.Quit()
        Exit Sub
ErrorHandler:
        MsgBox("CloseSimulation raised error " & Err.Description)
        End

    End Sub

End Module