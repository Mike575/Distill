Imports System.Math
Public Class RadFrac1
    '确定NFeed进料板位置
    '访问每一板组分浓度，与进料最为相近的一板为进料板
    '初步选取的回流比为 ModifyR_DSTWU
    '初步选取的塔盘数为 ModifyStage_DSTWU
    '选取依据为回流比二次导数与塔盘数图中，取波峰左侧谷底点

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        transfer_Variable_from_DSTWU()          '传递DSTWU模型中物流信息，设计规定
        Call myRadFrac.Modify()
        Call ListView_TrayRating_init()

    End Sub


    Sub transfer_Variable_from_DSTWU()      '传递 进料feed温度、压力、流量，轻重组分名称、质量分数；

        myRadFrac.Lightkey_Row = myDSTWU.Lightkey_Row  '轻组分
        myRadFrac.Heavykey_Row = myDSTWU.Heavykey_Row  '重组分
        myRadFrac.Heavykey_feed_MassFrac = myDSTWU.Heavykey_feed_MassFrac      '0.7   进料中苯乙烯质量分数
        myRadFrac.Lightkey_feed_MassFrac = myDSTWU.Lightkey_feed_MassFrac        '0.3   进料中乙苯质量分数
        myRadFrac.Pressure_feed = myDSTWU.Pressure_feed    '1.2        进料feed压力
        myRadFrac.Temperature_feed = myDSTWU.Temperature_feed   '30          进料feed温度
        myRadFrac.velocity_feed = myDSTWU.velocity_feed   '1000     进料feed质量流量 kg/h

        '塔板数初值，回流比
        '默弗里板效设置对其有限制
        myRadFrac.NStage_Numbers = myDSTWU.ModifyStage_DSTWU
        myRadFrac.Initial_Reflux_Ratio = myDSTWU.ModifyR_DSTWU
        '进料板塔板数初值
        myRadFrac.Initial_Feed_Stage = Int(myDSTWU.ModifyStage_DSTWU / 2)
        '塔顶压力 及 塔设备压降
        myRadFrac.Pressure_Top = myDSTWU.Pressure_Top   '塔顶压力 1.1  
        myRadFrac.Pressure_Column_Drop = myDSTWU.Pressure_Bottom - myDSTWU.Pressure_Top

    End Sub


    Sub ListView_TrayRating_init()

        Dim CellIndex As Integer
        Dim Stage_data(0) As Multi_Result
        Dim Result_HYD_TL(0) As Triple_Result          'Temperature liquid from
        Dim Result_HYD_TVTO(0) As Triple_Result        'Temperature vapor to
        Dim Result_HYD_LMF(0) As Triple_Result         'Mass flow liquid from:
        Dim Result_HYD_VMF(0) As Triple_Result         'Mass flow vapor to
        Dim Result_HYD_LVF(0) As Triple_Result         'Volume flow liquid from
        Dim Result_HYD_VVF(0) As Triple_Result         'Volume flow vapor to
        Dim Result_HYD_MWL(0) As Triple_Result         'Molecular wt liquid from:
        Dim Result_HYD_MWV(0) As Triple_Result         'Molecular wt vapor to
        Dim Result_HYD_RHOL(0) As Triple_Result        'Density liquid from
        Dim Result_HYD_RHOV(0) As Triple_Result        'Density vapor to
        Dim Result_HYD_MUL(0) As Triple_Result         'Viscosity liquid from
        Dim Result_HYD_MUV(0) As Triple_Result         'Viscosity vapor to
        Dim Result_HYD_STEN(0) As Triple_Result        'Surface tension liquid from
        Dim Result_HYD_FMIDX(0) As Triple_Result       'Foaming Index
        Dim Result_HYD_PARM(0) As Triple_Result        'Flow parameter
        Dim Result_HYD_QR(0) As Triple_Result          'Reduced vapor
        Dim Result_HYD_FFR(0) As Triple_Result         'Reduced F factor 


        'Temperature liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TL")
        'Temperature vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TVTO")
        'Mass flow liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LMF")
        'Mass flow vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VMF")
        'Volume flow liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LVF")
        'Volume flow vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VVF")
        'Molecular wt liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWL")
        'Molecular wt vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWV")
        'Density liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOL")
        'Density vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOV")
        'Viscosity liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUL")
        'Viscosity vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUV")
        'Surface tension liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_STEN")
        'Foaming Index: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FMIDX")
        'Flow parameter: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_PARM")
        'Reduced vapor: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_QR")
        'Reduced F factor        Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FFR")
        Dim ihcolOffspring As IHNodeCol
        Dim ihOffspring As IHNode

        'Temperature liquid from
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TL")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_TL(CellIndex).value = ihOffspring.Value
            Result_HYD_TL(CellIndex).Name = ihOffspring.Name
            Result_HYD_TL(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_TL(CellIndex)
        Next
        'Temperature vapor to
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TVTO")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_TVTO(CellIndex).value = ihOffspring.Value
            Result_HYD_TVTO(CellIndex).Name = ihOffspring.Name
            Result_HYD_TVTO(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_TVTO(CellIndex)
        Next
        'Mass flow liquid from:
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LMF")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_LMF(CellIndex).value = ihOffspring.Value
            Result_HYD_LMF(CellIndex).Name = ihOffspring.Name
            Result_HYD_LMF(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_LMF(CellIndex)
        Next
        'Mass flow vapor to:
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VMF")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_VMF(CellIndex).value = ihOffspring.Value
            Result_HYD_VMF(CellIndex).Name = ihOffspring.Name
            Result_HYD_VMF(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_VMF(CellIndex)
        Next
        'Volume flow liquid from:
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LVF")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_LVF(CellIndex).value = ihOffspring.Value
            Result_HYD_LVF(CellIndex).Name = ihOffspring.Name
            Result_HYD_LVF(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_LVF(CellIndex)
        Next
        'Volume flow vapor to: 
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VVF")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_VVF(CellIndex).value = ihOffspring.Value
            Result_HYD_VVF(CellIndex).Name = ihOffspring.Name
            Result_HYD_VVF(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_VVF(CellIndex)
        Next
        'Molecular wt liquid from:
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWL")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_MWL(CellIndex).value = ihOffspring.Value
            Result_HYD_MWL(CellIndex).Name = ihOffspring.Name
            Result_HYD_MWL(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_MWL(CellIndex)
        Next
        'Molecular wt vapor to:
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWV")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_MWV(CellIndex).value = ihOffspring.Value
            Result_HYD_MWV(CellIndex).Name = ihOffspring.Name
            Result_HYD_MWV(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_MWV(CellIndex)
        Next
        'Density liquid from: 
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOL")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_RHOL(CellIndex).value = ihOffspring.Value
            Result_HYD_RHOL(CellIndex).Name = ihOffspring.Name
            Result_HYD_RHOL(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_RHOL(CellIndex)
        Next
        'Density vapor to: 
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOV")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_RHOV(CellIndex).value = ihOffspring.Value
            Result_HYD_RHOV(CellIndex).Name = ihOffspring.Name
            Result_HYD_RHOV(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_RHOV(CellIndex)
        Next
        'Viscosity liquid from:  
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUL")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_MUL(CellIndex).value = ihOffspring.Value
            Result_HYD_MUL(CellIndex).Name = ihOffspring.Name
            Result_HYD_MUL(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_MUL(CellIndex)
        Next
        'Viscosity vapor to: 
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUV")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_MUV(CellIndex).value = ihOffspring.Value
            Result_HYD_MUV(CellIndex).Name = ihOffspring.Name
            Result_HYD_MUV(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_MUV(CellIndex)
        Next
        'Surface tension liquid from:
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_STEN")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_STEN(CellIndex).value = ihOffspring.Value
            Result_HYD_STEN(CellIndex).Name = ihOffspring.Name
            Result_HYD_STEN(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_STEN(CellIndex)
        Next
        'Foaming index: 
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FMIDX")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_FMIDX(CellIndex).value = ihOffspring.Value
            Result_HYD_FMIDX(CellIndex).Name = ihOffspring.Name
            Result_HYD_FMIDX(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_FMIDX(CellIndex)
        Next
        'Flow parameter:  
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_PARM")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_PARM(CellIndex).value = ihOffspring.Value
            Result_HYD_PARM(CellIndex).Name = ihOffspring.Name
            Result_HYD_PARM(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_PARM(CellIndex)
        Next
        'Reduced vapor: 
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_QR")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_QR(CellIndex).value = ihOffspring.Value
            Result_HYD_QR(CellIndex).Name = ihOffspring.Name
            Result_HYD_QR(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_QR(CellIndex)
        Next
        'Reduced F factor
        ihOffspring = myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FFR")
        ihcolOffspring = ihOffspring.Elements
        CellIndex = 0
        For Each ihOffspring In ihcolOffspring
            Result_HYD_FFR(CellIndex).value = ihOffspring.Value
            Result_HYD_FFR(CellIndex).Name = ihOffspring.Name
            Result_HYD_FFR(CellIndex).UnitString = ihOffspring.UnitString

            CellIndex = CellIndex + 1
            ReDim Preserve Result_HYD_FFR(CellIndex)
        Next

        'record each Stage results
        CellIndex = 0
        Do While CellIndex < myRadFrac.NStage_Numbers
            Stage_data（CellIndex）.number = CellIndex + 1
            Stage_data（CellIndex）.HYD_TL = Result_HYD_TL（CellIndex）.value
            Stage_data（CellIndex）.HYD_TVTO = Result_HYD_TVTO（CellIndex）.value
            Stage_data（CellIndex）.HYD_LMF = Result_HYD_LMF（CellIndex）.value
            Stage_data（CellIndex）.HYD_VMF = Result_HYD_VMF（CellIndex）.value
            Stage_data（CellIndex）.HYD_LVF = Result_HYD_LVF（CellIndex）.value
            Stage_data（CellIndex）.HYD_VVF = Result_HYD_VVF（CellIndex）.value
            Stage_data（CellIndex）.HYD_MWL = Result_HYD_MWL（CellIndex）.value
            Stage_data（CellIndex）.HYD_MWV = Result_HYD_MWV（CellIndex）.value
            Stage_data（CellIndex）.HYD_RHOL = Result_HYD_RHOL（CellIndex）.value
            Stage_data（CellIndex）.HYD_RHOV = Result_HYD_RHOV（CellIndex）.value
            Stage_data（CellIndex）.HYD_MUL = Result_HYD_MUL（CellIndex）.value
            Stage_data（CellIndex）.HYD_MUV = Result_HYD_MUV（CellIndex）.value
            Stage_data（CellIndex）.HYD_STEN = Result_HYD_STEN（CellIndex）.value
            Stage_data（CellIndex）.HYD_FMIDX = Result_HYD_FMIDX（CellIndex）.value
            Stage_data（CellIndex）.HYD_PARM = Result_HYD_PARM（CellIndex）.value
            Stage_data（CellIndex）.HYD_QR = Result_HYD_QR（CellIndex）.value
            Stage_data（CellIndex）.HYD_FFR = Result_HYD_FFR（CellIndex）.value
            CellIndex = CellIndex + 1
            ReDim Preserve Stage_data(CellIndex)
        Loop

        'Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Rating\1\Output\FLOOD_FAC5\1")  液泛因子
        ListView2.Columns.Clear()
        ListView2.Items.Clear()
        ListView2.Columns.Add("Stage", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("Temperature liquid from", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Temperature vapor to", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Mass flow liquid from", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Mass flow vapor to", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Volume flow liquid from", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Volume flow vapor to", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Molecular wt liquid from", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Molecular wt vapor to", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Density liquid from", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Density vapor to", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Viscosity liquid from", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Viscosity vapor to", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Surface tension liquid from", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Foaming Index", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Flow parameter", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Reduced vapor", 200, HorizontalAlignment.Left)
        ListView2.Columns.Add("Reduced F factor", 200, HorizontalAlignment.Left)

        Dim Newitem As New ListViewItem
        CellIndex = 0
        '添加单位
        Newitem = New ListViewItem("Unit")
        Newitem.SubItems.Add(Result_HYD_TL(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_TVTO(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_LMF(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_VMF(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_LVF(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_VVF(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_MWL(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_MWV(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_RHOL(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_RHOV(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_MUL(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_MUV(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_STEN(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_FMIDX(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_PARM(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_QR(0).UnitString)
        Newitem.SubItems.Add(Result_HYD_FFR(0).UnitString)
        ListView2.Items.Add(Newitem)
        CellIndex = CellIndex + 1
        '添加数值
        Do While CellIndex < myRadFrac.NStage_Numbers + 1

            Newitem = New ListViewItem(Stage_data(CellIndex).number)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_TL)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_TVTO)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_LMF)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_VMF)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_LVF)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_VVF)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_MWL)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_MWV)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_RHOL)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_RHOV)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_MUL)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_MUV)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_STEN)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_FMIDX)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_PARM)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_QR)
            Newitem.SubItems.Add(Stage_data(CellIndex).HYD_FFR)
            ListView2.Items.Add(Newitem)
            CellIndex = CellIndex + 1
        Loop
        ListView2.Refresh()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Visible = False
        Result.Visible = True
    End Sub
End Class




'myRadFrac.ihAPsim.Tree.FindNode(" \ Data \ Blocks \ B1 \ Subobjects \ Vary \ 1 \ Input() \ UB \ 1").value = 0.32   'RadFrac distillate to feed ratio计算范围最大值
'myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\UB\2").value=200    'RadFrac 塔盘数计算范围最大值
'myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\LB\1").value=0.28   'RadFrac distillate to feed ratio计算范围最小值
'myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\LB\2").value=20     'RadFrac 塔盘数计算范围最小值    
'myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\PROD_STAGE\BOTTOM).Value = 89
'myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\FEED_STAGE\FEED).value=46
'myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\VALUE\1").value=0.998   'YiBen mass recovery rate
'myRadFrac.ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\VALUE\2")'BENYIXI Mass recovery
