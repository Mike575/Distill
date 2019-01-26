Public Class Result


    Private Sub DSTWU_Result()
        Dim Results(4) As Triple_Result
        Dim CellIndex As Integer

        Results(0).Name = "最小回流比"
        Results(0).value = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\MIN_REFLUX").Value    '最小回流比
        Results(0).UnitString = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\MIN_REFLUX").UnitString    '最小回流比
        Results(1).Name = "实际回流比"
        Results(1).value = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\ACT_REFLUX").Value    '实际回流比
        Results(1).UnitString = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\ACT_REFLUX").UnitString    '实际回流比
        Results(2).Name = "最小理论板数"
        Results(2).value = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\MIN_STAGES").Value    '最小理论板数
        Results(2).UnitString = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\MIN_STAGES").UnitString    '最小理论板数
        Results(3).Name = "实际理论板数"
        Results(3).value = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\ACT_STAGES").Value    '实际理论板数
        Results(3).UnitString = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\ACT_STAGES").UnitString    '实际理论板数
        Results(4).Name = "进料位置"
        Results(4).value = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\FEED_LOCATN").Value   '进料位置
        Results(4).UnitString = myDSTWU.ihApsim.Tree.FindNode("\Data\Blocks\B1\Output\FEED_LOCATN").UnitString   '进料位置

        ListView2.Columns.Clear()
        ListView2.Items.Clear()
        ListView2.Columns.Add("名称", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("数值", 200, HorizontalAlignment.Left)
        Dim Newitem As New ListViewItem
        CellIndex = 0
        Do While CellIndex < Results.Count

            Newitem = New ListViewItem(Results(CellIndex).Name)
            Newitem.SubItems.Add(Results(CellIndex).value)
            ListView2.Items.Add(Newitem)
            CellIndex = CellIndex + 1
        Loop
        ListView2.Refresh()

    End Sub

    Private Sub RadFrac_Result()

        '输入

        myRadFrac.Lightkey_Row = myDSTWU.Lightkey_Row   '轻组分
        myRadFrac.Heavykey_Row = myDSTWU.Heavykey_Row   '重组分
        myRadFrac.Heavykey_feed_MassFrac = myDSTWU.Heavykey_feed_MassFrac   '进料中苯乙烯质量分数
        myRadFrac.Lightkey_feed_MassFrac = myDSTWU.Lightkey_feed_MassFrac   '进料中乙苯质量分数
        myRadFrac.Pressure_feed = myDSTWU.Pressure_feed                     '进料feed压力
        myRadFrac.Temperature_feed = myDSTWU.Temperature_feed               '进料feed温度
        myRadFrac.velocity_feed = myDSTWU.velocity_feed                     '进料feed质量流量 kg/h
        myRadFrac.NStage_Numbers = myDSTWU.ModifyStage_DSTWU                '塔板数初值，默弗里板效设置对其有限制
        myRadFrac.Initial_Reflux_Ratio = myDSTWU.ModifyR_DSTWU              '回流比
        myRadFrac.Initial_Feed_Stage = Int(myDSTWU.ModifyStage_DSTWU / 2)       '进料板塔板数初值
        myRadFrac.Pressure_Top = myDSTWU.Pressure_Top                           '塔顶压力  
        myRadFrac.Pressure_Column_Drop = myDSTWU.Pressure_Bottom - myDSTWU.Pressure_Top '塔设备压降

        '优化
        Call myRadFrac.Modify()

        '输出
        Dim CellIndex As Integer
        Dim Results(8) As Triple_Result

        Results(0).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DIAM4\1").Value        '精馏塔内径
        Results(0).Name = "精馏塔内径"
        Results(0).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DIAM4\1").UnitString

        Results(1).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCAREA\1").Value        '降液管截面积
        Results(1).Name = "降液管截面积"
        Results(1).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCAREA\1").UnitString

        Results(2).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCVELOC3\1").Value        '侧降液管流速
        Results(2).Name = "侧降液管流速"
        Results(2).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCVELOC3\1").UnitString        '侧降液管流速

        Results(3).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\FLOWPATH\1").Value        '流道长
        Results(3).Name = "流道长"
        Results(3).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\FLOWPATH\1").UnitString        '流道长


        Results(4).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCWIDTH1\1").Value        '侧降液管宽
        Results(4).Name = "侧降液管宽"
        Results(4).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCWIDTH1\1").UnitString        '侧降液管宽


        Results(5).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCLENG1\1").Value        '侧堰长
        Results(5).Name = "侧堰长"
        Results(5).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Output\DCLENG1\1").UnitString        '侧堰长


        Results(6).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\1").Value          '精馏速率
        Results(6).Name = "精馏速率"
        Results(6).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\1").UnitString        '精馏速率


        Results(7).value = myRadFrac.Modify_Feed_Stage         '进料板位置
        Results(7).Name = "进料板位置"
        Results(7).UnitString = ""


        Results(8).value = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\2").Value
        Results(8).Name = "最佳回流比"
        Results(8).UnitString = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\2").UnitString


        ListView1.Columns.Clear()
        ListView1.Items.Clear()
        ListView1.Columns.Add("名称", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("数值", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("单位", 200, HorizontalAlignment.Left)
        Dim Newitem As New ListViewItem
        CellIndex = 0
        Do While CellIndex < Results.Count

            Newitem = New ListViewItem(Results(CellIndex).Name)
            Newitem.SubItems.Add(Results(CellIndex).value)
            Newitem.SubItems.Add(Results(CellIndex).UnitString)
            ListView1.Items.Add(Newitem)
            CellIndex = CellIndex + 1
        Loop
        ListView1.Refresh()
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RadFrac1.Visible = True
    End Sub

    Private Sub Result_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call DSTWU_Result()
        Call RadFrac_Result()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Visible = False
        DSTWU2.Visible = False
    End Sub
End Class