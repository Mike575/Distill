Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel

Public Class Find

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Search(TextBox1.Text)   '轻组分乙苯
        ListView1_init()

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Search(TextBox2.Text)   '重组分苯乙烯
        ListView2_init()

        'Dim lstv As ListViewItem
        'If ListView2.CheckedItems.Count > 1 Then
        '    MsgBox("只能选择一种物质")
        'Else
        '    For Each lstv In ListView2.CheckedItems
        '        TextBox2.Text = lstv.Text         '重组分苯乙烯
        '    Next
        'End If
    End Sub
    '此步将确定Lightkey_Row和Heavykey_Row
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (ListView1.CheckedItems.Count = 1) And (ListView2.CheckedItems.Count = 1) Then     '确保只输入两种组分

            If Search(TextBox1.Text) * Search(TextBox2.Text) <> 0 Then  'Transfer components
                Lightkey_Row = 0
                Heavykey_Row = 0
                Lightkey_Row = Search(TextBox1.Text)     '以文本框内容为最终选择结果
                Heavykey_Row = Search(TextBox2.Text)     '以文本框内容为最终选择结果
                ihApsim_DSTWU.Tree.Data.Components.Specifications.Input.ANAME.YIBEN.value = Database.Compone(Lightkey_Row).ChemicalFormula   '轻组分
                ihApsim_DSTWU.Tree.Data.Components.Specifications.Input.CASN.YIBEN.value = Database.Compone(Lightkey_Row).Index
                ihApsim_DSTWU.Tree.Data.Components.Specifications.Input.DBNAME.YIBEN.value = Database.Compone(Lightkey_Row).EnglishName
                DSTWU1.Label1.Text = "轻组分-" & Database.Compone(Lightkey_Row).ChineseName

                ihApsim_DSTWU.Tree.Data.Components.Specifications.Input.ANAME.BENYIXI.value = Database.Compone(Heavykey_Row).ChemicalFormula   '重组分
                ihApsim_DSTWU.Tree.Data.Components.Specifications.Input.CASN.BENYIXI.value = Database.Compone(Heavykey_Row).Index
                ihApsim_DSTWU.Tree.Data.Components.Specifications.Input.DBNAME.BENYIXI.value = Database.Compone(Heavykey_Row).EnglishName
                DSTWU1.Label2.Text = "重组分-" & Database.Compone(Heavykey_Row).ChineseName
            Else
                MsgBox("Please input right information")
            End If
            Me.Visible = False
            DSTWU1.Visible = True
        Else
            MsgBox("只能选择一种物质")
        End If

    End Sub



    Sub ListView1_init()

        Dim CellIndex As Integer

        ListView1.Columns.Clear()
        ListView1.Items.Clear()

        ListView1.Columns.Add("分子式", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("英文名", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("索引", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("中文名", 100, HorizontalAlignment.Left)


        Dim Newitem As New ListViewItem
        CellIndex = 0
        Do While CellIndex < UBound(CellList)
            CellIndex = CellIndex + 1
            Newitem = New ListViewItem(Database.Compone(CellList(CellIndex)).ChemicalFormula)
            Newitem.SubItems.Add(Database.Compone(CellList(CellIndex)).EnglishName)
            Newitem.SubItems.Add(Database.Compone(CellList(CellIndex)).Index)
            Newitem.SubItems.Add(Database.Compone(CellList(CellIndex)).ChineseName)
            ListView1.Items.Add(Newitem)
        Loop
        ListView1.Refresh()
    End Sub

    Sub ListView2_init()

        Dim CellIndex As Integer

        ListView2.Columns.Clear()
        ListView2.Items.Clear()

        ListView2.Columns.Add("分子式", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("英文名", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("索引", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("中文名", 100, HorizontalAlignment.Left)

        Dim Newitem As New ListViewItem
        CellIndex = 0
        Do While CellIndex < UBound(CellList)
            CellIndex = CellIndex + 1
            Newitem = New ListViewItem(Database.Compone(CellList(CellIndex)).ChemicalFormula)
            Newitem.SubItems.Add(Database.Compone(CellList(CellIndex)).EnglishName)
            Newitem.SubItems.Add(Database.Compone(CellList(CellIndex)).Index)
            Newitem.SubItems.Add(Database.Compone(CellList(CellIndex)).ChineseName)
            ListView2.Items.Add(Newitem)
        Loop

        ListView2.Refresh()
    End Sub
    Public Function Search(Clue As String) As Integer
        Search = 0
        Clue = Trim(Clue)
        Dim ChemicalFormula As String = ""
        Dim EnglishName As String = ""
        Dim Index As String = ""
        Dim ChineseName As String = ""
        Dim Row As Integer
        Dim CellIndex As Integer

        Row = 0
        CellIndex = 0
        Do While Row < 2114
            Row = Row + 1
            ChemicalFormula = Database.Compone(Row).ChemicalFormula
            EnglishName = Database.Compone(Row).EnglishName
            Index = Database.Compone(Row).Index
            ChineseName = Database.Compone(Row).ChineseName
            ChemicalFormula = Database.Compone(Row).ChemicalFormula

            If (InStr(ChemicalFormula， Clue) > 0 And ChemicalFormula <> "") Or (InStr(EnglishName， Clue) > 0 And EnglishName <> "") Or (InStr(Index， Clue) > 0 And Index <> "") Or (InStr(ChineseName, Clue) > 0 And ChineseName <> "") Then
                CellIndex = CellIndex + 1
                ReDim Preserve CellList(CellIndex)
                CellList(CellIndex) = Row           'CellList(） store compared cell
            End If

            If ((Clue = ChemicalFormula) And ChemicalFormula <> "") Or ((Clue = EnglishName) And EnglishName <> "") Or ((Clue = Index) And Index <> "") Or ((Clue = ChineseName) And ChineseName <> "") Then
                Search = Row           'Search store strict compared cell position
            End If
        Loop
        Return Search
    End Function


    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click

        Dim lstv As ListViewItem
        For Each lstv In ListView1.SelectedItems
            If lstv.Selected = True Then
                lstv.Checked = True
                TextBox1.Text = lstv.Text
            Else
                lstv.Checked = False
            End If
        Next

    End Sub
    Private Sub ListView2_Click(sender As Object, e As EventArgs) Handles ListView2.Click

        Dim lstv As ListViewItem
        For Each lstv In ListView2.SelectedItems
            If lstv.Selected = True Then
                lstv.Checked = True
                TextBox2.Text = lstv.Text
            Else
                lstv.Checked = False
            End If
        Next

    End Sub

    Private Sub Find_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
