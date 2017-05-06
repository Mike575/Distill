Imports System.Math
Public Class RadFrac1
    '确定NFeed进料板位置
    '访问每一板组分浓度，与进料最为相近的一板为进料板
    '初步选取的回流比为 ModifyR_DSTWU
    '初步选取的塔盘数为 ModifyStage_DSTWU
    '选取依据为回流比二次导数与塔盘数图中，取波峰左侧谷底点

    Dim Lightkey_MassFrac As New ArrayList      '储存每一塔盘中轻组分乙苯的质量分数
    Dim Heavykey_MassFrac As New ArrayList      '储存每一塔盘中重组分苯乙烯的质量分数


    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        transfer_Variable_from_DSTWU()          '传递DSTWU模型中物流信息，设计规定
        RadFrac_Nstage_feed = Feed_Stage()      '计算最佳进料板
        Call Vary_RadFrac() '传递Vary参数，计算 Modify_DistillRate_RadFrac 和 ModifyR_RadFrac

        MsgBox(Modify_DistillRate_RadFrac)
    End Sub


    Sub transfer_Variable_from_DSTWU()      '传递 进料feed温度、压力、流量，轻重组分名称、质量分数；

        ihAPsim_RadFrac.Tree.Data.Components.Specifications.Input.ANAME.YIBEN.value = Database.Compone(Lightkey_Row).ChemicalFormula   '轻组分
        ihAPsim_RadFrac.Tree.Data.Components.Specifications.Input.CASN.YIBEN.value = Database.Compone(Lightkey_Row).Index
        ihAPsim_RadFrac.Tree.Data.Components.Specifications.Input.DBNAME.YIBEN.value = Database.Compone(Lightkey_Row).EnglishName
        ihAPsim_RadFrac.Tree.Data.Components.Specifications.Input.ANAME.BENYIXI.value = Database.Compone(Heavykey_Row).ChemicalFormula   '重组分
        ihAPsim_RadFrac.Tree.Data.Components.Specifications.Input.CASN.BENYIXI.value = Database.Compone(Heavykey_Row).Index
        ihAPsim_RadFrac.Tree.Data.Components.Specifications.Input.DBNAME.BENYIXI.value = Database.Compone(Heavykey_Row).EnglishName
        ihAPsim_RadFrac.Tree.Data.Streams.FEED.Input.FLOW.MIXED.BENYIXI.value = Heavykey_feed_MassFrac      '0.7   进料中苯乙烯质量分数
        ihAPsim_RadFrac.Tree.Data.Streams.FEED.Input.FLOW.MIXED.YIBEN.value = Lightkey_feed_MassFrac        '0.3   进料中乙苯质量分数
        ihAPsim_RadFrac.Tree.Data.Streams.FEED.Input.PRES.MIXED.value = Pressure_feed    '1.2        进料feed压力
        ihAPsim_RadFrac.Tree.Data.Streams.FEED.Input.TEMP.MIXED.value = Temperature_feed   '30          进料feed温度
        ihAPsim_RadFrac.Tree.Data.Streams.FEED.Input.TOTFLOW.MIXED.value = velocity_feed   '1000     进料feed质量流量 kg/h
        '塔板数初值，回流比
        ihAPsim_RadFrac.Tree.Data.Blocks.B1.Input.NSTAGE.value = ModifyStage_DSTWU
        ihAPsim_RadFrac.Tree.Data.Blocks.B1.Input.BASIS_RR.value = ModifyR_DSTWU
        '进料板塔板数初值
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Input\FEED_STAGE\FEED").Value = Int(ModifyStage_DSTWU / 2)
        '塔顶压力 及 塔设备压降
        ihAPsim_RadFrac.Tree.Data.Blocks.B1.Input.PRES1.value = Pressure_Top   '塔顶压力 1.1  
        Pressure_Column_Drop = Pressure_Bottom - Pressure_Top
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Input\DP_COL").Value = Pressure_Column_Drop    '塔设备压降
    End Sub

    '传递Vary参数，计算 Modify_DistillRate_RadFrac 和 ModifyR_RadFrac
    Sub Vary_RadFrac()

        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\VALUE\1").Value = 0.998  '轻组分乙苯回流质量分数规定
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\2\Input\VALUE\2").Value = 0.999  '重组分苯乙烯回流质量分数规定
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\BASIS_D").Value = 300       'distillate rate
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\LB\1").Value = 280          '280 D:F
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\UB\1").Value = 320          '320 D:F
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\BASIS_RR").Value = ModifyR_DSTWU
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\LB\2").Value = 20          '20 RR
        ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\UB\2").Value = 200          '200 RR
        Modify_DistillRate_RadFrac = ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\1").Value    '获取RadFrac精馏出料速率_Modify
        ModifyR_RadFrac = ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\2").Value               '获取RadFrac精馏回流比_Modify

        '备用Node点
        'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\D:F").Value = 0.3
        'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\FEED_STAGE\FEED").Value = RadFrac_Nstage_feed
        ' ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Input\D:F").Value = 0.3      'distillate to feed ratio

    End Sub

    Function Feed_Stage() As Integer    '全模型模拟，获取最佳进料板

        '选定进料塔盘数初值，其为DSTWU 模拟获得的ModifyStage
        Feed_Stage = Int(ModifyStage_DSTWU / 2)
        '运行RadFrac 模拟引擎
        engine_RadFrac.Run2()
        '获取每一塔盘中轻重组分的质量分数
        Get_eachStage_MassFrac()

        '设置一循环数，防止模拟计算过程不收敛，进入死循环后无法退出
        Dim i As Integer = 0
        Do While (Feed_Stage <> Compared_MassFrac_Stage()) AndAlso (i < 100)

            i = i + 1
            '获取与进料轻重组分组成最为接近的塔盘数，并以其为新的进料板
            Feed_Stage = Compared_MassFrac_Stage()
            ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Input\FEED_STAGE\FEED").Value = Compared_MassFrac_Stage()
            ' ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\FEED_STAGE\FEED").Value = Compared_MassFrac_Stage()
            '运行RadFrac 模拟引擎
            engine_RadFrac.Run2()
        Loop
        If i >= 100 Then
            MsgBox("RradFrac模拟不收敛")
            i = 0
        End If
        Return Feed_Stage

    End Function

    Function Compared_MassFrac_Stage() As Integer     '单模型模拟，获取最佳进料板

        Dim i As Integer = 0
        Dim delta As Double = 0
        RadFrac_Nstage_feed = Int(ModifyStage_DSTWU / 2)

        delta = Abs_double(eachStage_LightKey_MassFrac(0) - Lightkey_feed_MassFrac)      '获取该塔盘中轻组分与进料中轻组分的质量分数差值
        Do While (i < eachStage_MassFrac_StageNumber.Count - 1)

            If Abs_double(eachStage_LightKey_MassFrac(i) - Lightkey_feed_MassFrac) < delta Then
                delta = Abs_double(eachStage_LightKey_MassFrac(i) - Lightkey_feed_MassFrac)
                RadFrac_Nstage_feed = i + 1     '实际板数在动态数组角标的基础上需要加1，因为动态数组角标是从0开始的
            End If
            i = i + 1
        Loop
        Compared_MassFrac_Stage = RadFrac_Nstage_feed
        Return Compared_MassFrac_Stage
    End Function


    Public Sub Get_eachStage_MassFrac()     '获取每一塔盘中轻重组分的质量分数
        On Error GoTo ErrorHandler
        Dim ihNStage As IHNodeCol
        Dim ihStage As IHNode
        Dim str As String
        str = ""
        ihNStage = ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Output\Y_MS").Elements
        eachStage_MassFrac_StageNumber.Clear()
        eachStage_HeavyKey_MassFrac.Clear()
        eachStage_LightKey_MassFrac.Clear()

        For Each ihStage In ihNStage
            eachStage_MassFrac_StageNumber.Add(ihStage.Name)                           '质量分数对应的塔盘数编号
            str = "\Data\Blocks\B1\Output\Y_MS\" + ihStage.Name + "\BENYIXI"
            eachStage_HeavyKey_MassFrac.Add(ihAPsim_RadFrac.Tree.FindNode(str).Value)  '重组分苯乙烯在某一塔盘中的质量分数
            str = "\Data\Blocks\B1\Output\Y_MS\" + ihStage.Name + "\YIBEN"
            eachStage_LightKey_MassFrac.Add(ihAPsim_RadFrac.Tree.FindNode(str).Value)  '轻组分乙苯在某一塔盘中的质量分数

        Next ihStage
        Exit Sub
ErrorHandler:
        MsgBox("Transfer_eachStage_MassFrac " & Err.Description)

    End Sub
End Class




'ihAPsim_RadFrac.Tree.FindNode(" \ Data \ Blocks \ B1 \ Subobjects \ Vary \ 1 \ Input() \ UB \ 1").value = 0.32   'RadFrac distillate to feed ratio计算范围最大值
'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\UB\2").value=200    'RadFrac 塔盘数计算范围最大值
'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\LB\1").value=0.28   'RadFrac distillate to feed ratio计算范围最小值
'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\LB\2").value=20     'RadFrac 塔盘数计算范围最小值    
'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\PROD_STAGE\BOTTOM).Value = 89
'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\FEED_STAGE\FEED).value=46
'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\VALUE\1").value=0.998   'YiBen mass recovery rate
'ihAPsim_RadFrac.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\VALUE\2")'BENYIXI Mass recovery
