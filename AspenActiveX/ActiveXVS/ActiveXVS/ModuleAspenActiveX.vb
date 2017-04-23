Option Explicit On
Module AspenActiveX

    Public AspenPlusDistilR As HappLS
    Public AspenPlusRadFrac1 As HappLS
    Public ModifyR As Double
    Public ModifyStage As Double
    Public ihAPsim As IHapp
    Public R As New ArrayList
    Public Stage As New ArrayList
    Public ihNStage As IHNodeCol
    Public ihStage As IHNode
    Public LowStage As Double
    Public HighStage As Double
    Public engine As Happ.IHAPEngine


    Function OpenDistilR() As IHapp

        Dim ihAPsim As IHapp
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
        ihAPsim = AspenPlusDistilR.Application
        engine = AspenPlusDistilR.Engine

        ' display the GUI
        ihAPsim.Visible = True

        ' run the simulation
        ihAPsim.Run()

        ' return the Happ object for the AspenPlus simulator
        OpenDistilR = ihAPsim
        Exit Function
ErrorHandler:
        MsgBox("OpenSimulation raised error " & Err.Description)
        End
    End Function

    Function OpenRadFrac1() As IHapp

        Dim ihAPsim As IHapp
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
        ihAPsim = AspenPlusRadFrac1.Application
        engine = AspenPlusRadFrac1.Engine

        ' display the GUI
        ihAPsim.Visible = True

        ' run the simulation
        ihAPsim.Run()

        ' return the Happ object for the AspenPlus simulator
        OpenRadFrac1 = ihAPsim
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

    Sub TransferNTR(ByVal ihApsim As IHapp)
        On Error GoTo ErrorHandler
        Dim i As Integer
        Dim strout As String
        strout = ""
        i = 0
        ihNStage = ihApsim.Tree.Data.Blocks.B1.Output.RR_OUT.Elements
        For Each ihStage In ihNStage
            Stage.Add(ihStage.Name)
            R.Add(ihStage.Value)

            strout = strout & Chr(13) & ihStage.Name _
            & Chr(9) & Format(ihStage.Value, "###.00") _
            & Chr(9) & ihStage.UnitString
            i = i + 1
        Next ihStage


        Exit Sub
ErrorHandler:
        MsgBox("TransferNTR raised error " & Err.Description)

    End Sub

    Sub ChangeNTR(ByVal ihapsim As IHapp)
        ihapsim.Tree.Data.Blocks.B1.Input.LOWER.value = LowStage    '90  
        ihapsim.Tree.Data.Blocks.B1.Input.UPPER.value = HighStage    '200
        Exit Sub
ErrorHandler:
        MsgBox("ChangeNTR raised error " & Err.Description)
    End Sub
End Module