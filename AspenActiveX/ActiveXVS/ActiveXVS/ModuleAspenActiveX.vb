Option Explicit On
Module AspenActiveX

    Public AspenPlusDistilR As HappLS
    Public AspenPlusRadFrac1 As HappLS
    Public RadFrac_Standard As HappLS
    Public Lightkey_Row As Integer              '轻组分数据库位置
    Public Heavykey_Row As Integer              '重组分数据库位置
    Public ModifyR_DSTWU As Double
    Public ModifyStage_DSTWU As Double
    Public ModifyR_RadFrac As Double
    Public Modify_DistillRate_RadFrac As Double
    Public RadFrac_ModifyR As Double
    Public RadFrac_Stage As Double
    Public RadFrac_Nstage_feed As Integer       'RadFrac 模型中进料板位置

    Public Heavykey_feed_MassFrac As Double     '重组分进料质量分数
    Public Lightkey_feed_MassFrac As Double     '轻组分进料质量分数  
    Public Heavykey_Up_MassFrac As Double       '重组分塔顶回收质量分数
    Public Lightkey_Up_MassFrac As Double       '轻组分塔顶回收质量分数
    Public Heavykey_Bottom_MassFrac As Double   '重组分塔釜回收质量分数
    Public Temperature_feed As Double           '进料温度
    Public Pressure_feed As Double              '进料压力
    Public velocity_feed As Double              '进料流速   kg/hr
    Public Pressure_Bottom As Double            '塔底压力
    Public Pressure_Top As Double               '塔顶压力
    Public Pressure_Column_Drop As Double       '全塔压降
    Public Reflux_Ratio As Double               '回流比

    ' Public ihAPsim As IHapp
    Public ihAPsim_RadFrac As IHapp
    Public ihApsim_DSTWU As IHapp
    Public DSTWU_R As New ArrayList
    Public DSTWU_Stage As New ArrayList
    Public LowStage As Double
    Public HighStage As Double
    Public engine_DSTWU As Happ.IHAPEngine
    Public engine_RadFrac As Happ.IHAPEngine
    Public eachStage_LightKey_MassFrac As New ArrayList
    Public eachStage_MassFrac_StageNumber As New ArrayList
    Public eachStage_HeavyKey_MassFrac As New ArrayList

    Public Structure Node
        Dim Row As Integer
        Dim deep As Integer
        Dim deepin() As String
        Dim Name As String
        Dim value As String
        Dim address As String
    End Structure

    Public Structure Binary_Result

        Dim value As String
        Dim Name As String
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

    Function OpenDistilR() As IHapp

        Dim ihAPsim_DSTWU As IHapp
        Dim ihAPsim_RadFrac As IHapp
        On Error GoTo ErrorHandler
        Dim VERSION As String = "V8.4"
        Dim VERSIONNUMBER As String = "30.0"

        ' define the path to the AspenPlus examples folder
        Dim defaultpath As String
        If (8 = IntPtr.Size Or Not String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))) Then
            defaultpath = Environment.GetEnvironmentVariable("ProgramFiles(x86)") + "\AspenTech\Aspen Plus " + VERSION + "\GUI"
        Else
            defaultpath = Environment.GetEnvironmentVariable("ProgramFiles") + "\AspenTech\Aspen Plus " + VERSION + "\GUI"
        End If

        Dim path As String
        path = "C:\Users\57584\Documents\GitHub\Ditill\Distill\AspenActiveX\ActiveXVS\ActiveXVS\bin\Debug\AspenBKP\"

        ' open existing simulation
        AspenPlusDistilR = GetObject(path & "DistilR.bkp")
        ihAPsim_DSTWU = AspenPlusDistilR.Application
        engine_DSTWU = AspenPlusDistilR.Engine

        ' display the GUI
        ihAPsim_DSTWU.Visible = True

        ' run the simulation
        ihAPsim_DSTWU.Run()

        ' return the Happ object for the AspenPlus simulator
        OpenDistilR = ihAPsim_DSTWU
        Exit Function
ErrorHandler:
        MsgBox("OpenSimulation raised error " & Err.Description)
        End
    End Function

    Function OpenRadFrac1() As IHapp

        Dim ihAPsim_RadFrac As IHapp
        On Error GoTo ErrorHandler
        Dim VERSION As String = "V8.4"
        Dim VERSIONNUMBER As String = "30.0"

        ' define the path to the AspenPlus examples folder
        Dim defaultpath As String
        If (8 = IntPtr.Size Or Not String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))) Then
            defaultpath = Environment.GetEnvironmentVariable("ProgramFiles(x86)") + "\AspenTech\Aspen Plus " + VERSION + "\GUI"
        Else
            defaultpath = Environment.GetEnvironmentVariable("ProgramFiles") + "\AspenTech\Aspen Plus " + VERSION + "\GUI"
        End If

        Dim path As String
        path = "C:\Users\57584\Documents\GitHub\Ditill\Distill\AspenActiveX\ActiveXVS\ActiveXVS\bin\Debug\AspenBKP\"

        ' open existing simulation
        AspenPlusRadFrac1 = GetObject(path & "RadFrac1.bkp")
        RadFrac_Standard = GetObject(path & "RadFrac.bkp")
        ihAPsim_RadFrac = AspenPlusRadFrac1.Application
        engine_RadFrac = AspenPlusRadFrac1.Engine

        ' display the GUI
        ihAPsim_RadFrac.Visible = True

        ' run the simulation
        ihAPsim_RadFrac.Run()

        ' return the Happ object for the AspenPlus simulator
        OpenRadFrac1 = ihAPsim_RadFrac
        Exit Function
ErrorHandler:
        MsgBox("OpenSimulation raised error " & Err.Description)
        End
    End Function

    Sub CloseSimulation(ByVal ihAPsim As IHapp)

        On Error GoTo ErrorHandler

        'Quit without saving

        ihAPsim.Quit()

        Exit Sub

ErrorHandler:

        MsgBox("CloseSimulation raised error " & Err.Description)

        End

    End Sub

    Sub ConnectivityExample(ByVal ihAPsim As IHapp)
        ' This example displays a table showing flowsheet connectivity
        Dim ihStreamList As IHNode
        Dim ihBlockList As IHNode
        Dim ihDestBlock As IHNode
        Dim ihSourceBlock As IHNode
        Dim ihStream As IHNode
        Dim strHeading As String
        Dim strTable As String
        Dim strDestBlock As String
        Dim strDestPort As String
        Dim strSourceBlock As String
        Dim strSourcePort As String
        Dim strStreamName As String
        Dim strStreamType As String

        strTable = ""

        On Error GoTo ErrorHandler
        ihStreamList = ihAPsim.Tree.Data.Streams
        ihBlockList = ihAPsim.Tree.Data.Blocks

        strHeading = "Stream" & Chr(9) & "From" _
                & Chr(9) & Chr(9) & Chr(9) & "To" & Chr(13)

        For Each ihStream In ihStreamList.Elements
            strStreamName = ihStream.Name
            strStreamType = ihStream.AttributeValue(Happ.HAPAttributeNumber.HAP_RECORDTYPE)
            ' get the destination connections
            ihDestBlock = ihStream.Elements("Ports").Elements("DEST")
            If (ihDestBlock.Elements.RowCount(0) > 0) Then
                ' there is a destination port
                strDestBlock = ihDestBlock.Elements(0).Value
                strDestPort = ihBlockList.Elements(strDestBlock).
                Connections.Elements(strStreamName).Value
            Else
                ' it's a flowsheet product
                strDestBlock = ""
                strDestPort = ""
            End If
            ' get the source connections
            ihSourceBlock = ihStream.Elements("Ports").Elements("SOURCE")
            If (ihSourceBlock.Elements.RowCount(0) > 0) Then
                ' there is a source port
                strSourceBlock = ihSourceBlock.Elements(0).Value
                strSourcePort = ihBlockList.Elements(strSourceBlock).
                Connections.Elements(strStreamName).Value
            Else
                ' it's a flowsheet feed
                strSourceBlock = ""
                strSourcePort = ""
            End If

            strTable = strTable & Chr(13) & strStreamName _
                      & Chr(9) & strSourceBlock _
                      & Chr(9) & strSourcePort & Chr(9) _
                      & Chr(9) & strDestBlock & Chr(9) _
                      & strDestPort
        Next ihStream
        MsgBox(strHeading & strTable, , "ConnectivityExample")
        Exit Sub

ErrorHandler:
        MsgBox("ConnectivityExample raised error" & Err.Description)

    End Sub

    Sub GetScalarValuesExample(ByRef ihAPsim As IHapp)

        ' This example retrieves scalar variables from a block

        Dim ihColumn As IHNode

        Dim nStages As Long

        Dim buratio As Double

        On Error GoTo ErrorHandler

        ' navigate the tree to the RADFRAC block

        ihColumn = ihAPsim.Tree.Data.Blocks.B1

        ' get the number of stages

        nStages = ihColumn.Input.Elements("NSTAGE").Value

        MsgBox("Number of Stages is: " & nStages)

        Exit Sub

ErrorHandler:

        MsgBox("GetScalarValuesExample raised error" & Err.Description)

    End Sub

    Sub ListBlocksExample(ByVal ihAPsim As IHapp)

        ' This example retrieves a list of blocks and their attributes

        Dim ihBlockList As IHNodeCol

        Dim ihBlock As IHNode

        Dim strOut As String

        On Error GoTo ErrorHandler



        ihBlockList = ihAPsim.Tree.Data.Blocks.Elements

        strOut = "Block" & Chr(9) & "Block Type" & Chr(9) & "Section  "

        For Each ihBlock In ihBlockList

            strOut = strOut & Chr(13) & ihBlock.Name & Chr(9) &
        ihBlock.AttributeValue(Happ.HAPAttributeNumber.HAP_RECORDTYPE) & "  " & Chr(9) & Chr(9) &
        ihBlock.AttributeValue(Happ.HAPAttributeNumber.HAP_SECTION)

        Next ihBlock

        MsgBox(strOut, , "ListBlocksExample")

        Exit Sub

ErrorHandler:

        MsgBox("ListBlocksExample raised error" & Err.Description)

    End Sub




End Module