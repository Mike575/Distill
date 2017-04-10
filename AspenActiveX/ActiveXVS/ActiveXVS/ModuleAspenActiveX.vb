Option Explicit On



Module AspenActiveX

    Public AspenPlus As HappLS
    Public ihAPsim As IHapp


    Function OpenSimulation() As IHapp

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
        AspenPlus = GetObject(path & "DistilR.bkp")
        ihAPsim = AspenPlus.Application

        ' display the GUI
        ihAPsim.Visible = True

        ' run the simulation
        ihAPsim.Run()

        ' return the Happ object for the AspenPlus simulator
        OpenSimulation = ihAPsim
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