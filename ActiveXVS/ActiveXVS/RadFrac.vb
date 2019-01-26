Imports System.Math
Public Class RadFrac
    '确定NFeed进料板位置
    '访问每一板组分浓度，与进料最为相近的一板为进料板
    '初步选取的回流比为 ModifyR_DSTWU
    '初步选取的塔盘数为 ModifyStage_DSTWU
    '选取依据为回流比二次导数与塔盘数图中，取波峰左侧谷底点
    Public ihAPsim As IHapp
    Public Lightkey_Row As Integer '轻组分
    Public Heavykey_Row As Integer '重组分
    Public Lightkey_Mass_Recovery As Double  '轻组分乙苯回流质量分数规定 0.998
    Public Heavykey_Mass_Recovery As Double  '重组分苯乙烯回流质量分数规定 0.999

    Public Heavykey_feed_MassFrac As Double  '进料中苯乙烯质量分数
    Public Lightkey_feed_MassFrac As Double  '进料中乙苯质量分数
    Public Pressure_feed As Double           '进料feed压力
    Public Temperature_feed As Double        '进料feed温度
    Public velocity_feed As Double           '进料feed质量流量 kg/h

    Public Initial_Feed_Stage As Integer     '初始进料位置


    Public NStage_Numbers As Double          '塔板数
    Public Pressure_Top As Double            '塔顶压力
    Public Pressure_Column_Drop As Double    '塔设备压降

    Public Distillate_Rate As Double        '初始精馏速率 300
    Public Distillate_Rate_LB As Double     '精馏速率收敛下界 280
    Public Distillate_Rate_UB As Double     '精馏速率收敛上界 320

    Public Initial_Reflux_Ratio As Double    '初始质量回流比
    Public Reflux_Ratio_LB As Double         '质量回流比下界
    Public Reflux_Ratio_UB As Double         '质量回流比上界


    '输出
    Public eachStage_LightKey_MassFrac As New ArrayList         '储存每一塔盘中轻组分乙苯的质量分数
    Public eachStage_MassFrac_StageNumber As New ArrayList      '储存每一塔盘中重组分苯乙烯的质量分数
    Public eachStage_HeavyKey_MassFrac As New ArrayList
    Public Modify_Distill_Rate As Double     '优化精馏速率 300
    Public Modify_Reflux_Ratio As Double      '优化质量回流比
    Public Modify_Feed_Stage As Integer      '优化进料位置

    Sub Modify()

        transfer_Variable_from_DSTWU()          '传递DSTWU模型中物流信息，设计规定
        Modify_Feed_Stage = Feed_Stage()      '计算最佳进料板
        Call Vary_RadFrac() '传递Vary参数，计算 Modify_Distill_Rate 和 Modify_Reflux_Ratio

    End Sub



    Sub transfer_Variable_from_DSTWU()      '传递 进料feed温度、压力、流量，轻重组分名称、质量分数；
        '输入
        'Public Lightkey_Row As Integer '轻组分
        'Public Heavykey_Row As Integer '重组分
        'Public Heavykey_feed_MassFrac As Double  '进料中苯乙烯质量分数
        'Public Lightkey_feed_MassFrac As Double  '进料中乙苯质量分数
        'Public Pressure_feed As Double           '进料feed压力
        'Public Temperature_feed As Double        '进料feed温度
        'Public velocity_feed As Double           '进料feed质量流量 kg/h
        'Public Initial_Feed_Stage As Integer     '初始进料位置
        'Public Modify_Feed_Stage As Integer      '优化进料位置
        'Public NStage_Numbers As Double          '塔板数
        'Public Pressure_Top As Double            '塔顶压力
        'Public Pressure_Column_Drop As Double    '塔设备压降
        ihAPsim.Tree.Data.Components.Specifications.Input.ANAME.YIBEN.value = Database.Compone(Me.Lightkey_Row).ChemicalFormula   '轻组分
        ihAPsim.Tree.Data.Components.Specifications.Input.CASN.YIBEN.value = Database.Compone(Me.Lightkey_Row).Index
        ihAPsim.Tree.Data.Components.Specifications.Input.DBNAME.YIBEN.value = Database.Compone(Me.Lightkey_Row).EnglishName
        ihAPsim.Tree.Data.Components.Specifications.Input.ANAME.BENYIXI.value = Database.Compone(Me.Heavykey_Row).ChemicalFormula   '重组分
        ihAPsim.Tree.Data.Components.Specifications.Input.CASN.BENYIXI.value = Database.Compone(Me.Heavykey_Row).Index
        ihAPsim.Tree.Data.Components.Specifications.Input.DBNAME.BENYIXI.value = Database.Compone(Me.Heavykey_Row).EnglishName
        ihAPsim.Tree.Data.Streams.FEED.Input.FLOW.MIXED.BENYIXI.value = Me.Heavykey_feed_MassFrac      '0.7   进料中苯乙烯质量分数
        ihAPsim.Tree.Data.Streams.FEED.Input.FLOW.MIXED.YIBEN.value = Me.Lightkey_feed_MassFrac        '0.3   进料中乙苯质量分数
        ihAPsim.Tree.Data.Streams.FEED.Input.PRES.MIXED.value = Me.Pressure_feed    '1.1        进料feed压力
        ihAPsim.Tree.Data.Streams.FEED.Input.TEMP.MIXED.value = Me.Temperature_feed   '30          进料feed温度
        ihAPsim.Tree.Data.Streams.FEED.Input.TOTFLOW.MIXED.value = Me.velocity_feed   '1000     进料feed质量流量 kg/h
        '塔板数初值，回流比
        '默弗里板效设置对其有限制
        ihAPsim.Tree.Data.Blocks.B1.Input.NSTAGE.value = Me.NStage_Numbers
        ihAPsim.Tree.Data.Blocks.B1.Input.BASIS_RR.value = Me.Initial_Reflux_Ratio
        '进料板塔板数初值
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Input\FEED_STAGE\FEED").Value = Initial_Feed_Stage
        '塔顶压力 及 塔设备压降
        ihAPsim.Tree.Data.Blocks.B1.Input.PRES1.value = Me.Pressure_Top   '塔顶压力 1.1  

        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Input\DP_COL").Value = Me.Pressure_Column_Drop    '塔设备压降
    End Sub


    '传递Vary参数，计算 Modify_Distill_Rate 和 Modify_Reflux_Ratio
    Private Sub Vary_RadFrac()
        '输入
        'Public Lightkey_Mass_Recovery As Double  '轻组分乙苯回流质量分数规定 0.998
        'Public Heavykey_Mass_Recovery As Double  '重组分苯乙烯回流质量分数规定 0.999
        'Public Distillate_Rate As Double        '初始精馏速率 300
        'Public Distillate_Rate_LB As Double     '精馏速率收敛下界 280
        'Public Distillate_Rate_UB As Double     '精馏速率收敛上界 320
        'Public Initial_Reflux_Ratio As Double    '初始质量回流比
        'Public Reflux_Ratio_LB As Double         '质量回流比下界
        'Public Reflux_Ratio_UB As Double         '质量回流比上界
        '输出
        'Public Modify_Distill_Rate As Double    '优化精馏速率 300.1
        'Public Modify_Reflux_Ratio As Double     '优化质量回流比
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\VALUE\1").Value = 0.998  '轻组分乙苯回流质量分数规定
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\2\Input\VALUE\2").Value = 0.999  '重组分苯乙烯回流质量分数规定
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\BASIS_D").Value = 300       'distillate rate
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\LB\1").Value = 280          '280 D:F
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\UB\1").Value = 320          '320 D:F
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\BASIS_RR").Value = Initial_Reflux_Ratio
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\LB\2").Value = 20          '20 RR
        ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\UB\2").Value = 200          '200 RR
        ihAPsim.Engine.Run2()
        Me.Modify_Distill_Rate = ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\1").Value               '获取RadFrac精馏出料速率_Modify
        Me.Modify_Reflux_Ratio = ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\VAR_VAL\2").Value               '获取RadFrac精馏回流比_Modify

        '备用Node点
        'ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\D:F").Value = 0.3
        'ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\FEED_STAGE\FEED").Value = RadFrac_Nstage_feed
        ' ihAPsim.Tree.FindNode("\Data\Blocks\B1\Input\D:F").Value = 0.3      'distillate to feed ratio

    End Sub

    Function Feed_Stage() As Integer    '全模型模拟，获取最佳进料板

        '选定进料塔盘数初值，其为DSTWU 模拟获得的ModifyStage
        Feed_Stage = Initial_Feed_Stage
        '运行RadFrac 模拟引擎
        ihAPsim.Engine.Run2()
        '获取每一塔盘中轻重组分的质量分数
        Get_eachStage_MassFrac()

        '设置一循环数，防止模拟计算过程不收敛，进入死循环后无法退出
        Dim i As Integer = 0
        Do While (Feed_Stage <> Compared_MassFrac_Stage()) AndAlso (i < 100)

            i = i + 1
            '获取与进料轻重组分组成最为接近的塔盘数，并以其为新的进料板
            Feed_Stage = Compared_MassFrac_Stage()
            ihAPsim.Tree.FindNode("\Data\Blocks\B1\Input\FEED_STAGE\FEED").Value = Feed_Stage
            ' ihAPsim.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\FEED_STAGE\FEED").Value = Compared_MassFrac_Stage()
            '运行RadFrac 模拟引擎
            ihAPsim.Engine.Run2()
        Loop
        If i >= 100 Then
            MsgBox("RradFrac模拟不收敛")
            i = 0
        End If
        Return Feed_Stage

    End Function

    Private Function Compared_MassFrac_Stage() As Integer     '单模型模拟，获取最佳进料板

        Dim i As Integer = 0
        Dim delta As Double = 0
        Compared_MassFrac_Stage = Initial_Feed_Stage
        delta = Abs_double(eachStage_LightKey_MassFrac(0) - Lightkey_feed_MassFrac)      '获取该塔盘中轻组分与进料中轻组分的质量分数差值
        Do While (i < eachStage_MassFrac_StageNumber.Count - 1)

            If Abs_double(eachStage_LightKey_MassFrac(i) - Lightkey_feed_MassFrac) < delta Then
                delta = Abs_double(eachStage_LightKey_MassFrac(i) - Lightkey_feed_MassFrac)
                Compared_MassFrac_Stage = i + 1     '实际板数在动态数组角标的基础上需要加1，因为动态数组角标是从0开始的
            End If
            i = i + 1
        Loop
        Return Compared_MassFrac_Stage
    End Function

    Private Sub Get_eachStage_MassFrac()     '获取每一塔盘中轻重组分的质量分数
        On Error GoTo ErrorHandler
        Dim ihNStage As IHNodeCol
        Dim ihStage As IHNode
        Dim str As String
        str = ""
        ihNStage = ihAPsim.Tree.FindNode("\Data\Blocks\B1\Output\Y_MS").Elements
        Me.eachStage_MassFrac_StageNumber.Clear()
        Me.eachStage_HeavyKey_MassFrac.Clear()
        Me.eachStage_LightKey_MassFrac.Clear()

        For Each ihStage In ihNStage
            Me.eachStage_MassFrac_StageNumber.Add(ihStage.Name)                           '质量分数对应的塔盘数编号
            str = "\Data\Blocks\B1\Output\X_MS\" + ihStage.Name + "\BENYIXI"
            Me.eachStage_HeavyKey_MassFrac.Add(ihAPsim.Tree.FindNode(str).Value)  '重组分苯乙烯在某一塔盘中的质量分数
            str = "\Data\Blocks\B1\Output\X_MS\" + ihStage.Name + "\YIBEN"
            Me.eachStage_LightKey_MassFrac.Add(ihAPsim.Tree.FindNode(str).Value)  '轻组分乙苯在某一塔盘中的质量分数

        Next ihStage
        Exit Sub
ErrorHandler:
        MsgBox("Get_eachStage_MassFrac " & Err.Description)

    End Sub

    Public Function Abs_double(X As Double) As Double       '取double类型变量的绝对值
        If X < 0 Then
            Abs_double = -X
        ElseIf X > 0 Then
            Abs_double = X
        Else
            Abs_double = 0
        End If
        Return Abs_double
    End Function
End Class
