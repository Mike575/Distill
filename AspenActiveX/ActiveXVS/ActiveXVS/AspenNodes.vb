# node name
Application.Tree.Data.Blocks.B1.Input.LOWER     '90  
Application.Tree.Data.Blocks.B1.Input.NSTAGE    '200
Application.Tree.Data.Blocks.B1.Input.UPPER     '200
Application.Tree.Data.Blocks.B1.Input.HEAVYKEY  'BENYIXI
Application.Tree.Data.Blocks.B1.Input.LIGHTKEY  'YIBEN
Application.Tree.Data.Blocks.B1.Input.RECOVH    '0.001
Application.Tree.Data.Blocks.B1.Input.RECOVL    '0.998
Application.Tree.Data.Blocks.B1.Input.RR        '-1.5
Application.Tree.Data.Blocks.B1.Input.SIM_LEVEL '4
Application.Tree.Data.Streams.FEED.Input.MAXIT.MIXED    '30
Application.Tree.Data.Streams.FEED.Input.TEMP.MIXED     '30
Application.Tree.Data.Streams.FEED.Input.PRES.MIXED     '1.2
Application.Tree.Data.Streams.FEED.Input.TOTFLOW.MIXED  '1000

Application.Tree.Data.Blocks.B1.Input.Unit Set  'METCBAR    
'Metric units with temperature in degrees Centigrade and pressure in bars       

Application.Tree.Data.Blocks.B1.Input.DUTY_TOL  '#?#    23.88
Application.Tree.Data.Blocks.B1.Input.INPUT     'INPUT 
Application.Tree.Data.Blocks.B1.Input.MAXIT     '50
Application.Tree.Data.Blocks.B1.Input.OPSETNAME 'NRTL
Application.Tree.Data.Blocks.B1.Input.OPT_NTRR  'RR
Application.Tree.Data.Blocks.B1.Input.OPT_RDV   'LIQUID
Application.Tree.Data.Blocks.B1.Input.PBOT      '0.2
Application.Tree.Data.Blocks.B1.Input.PROP_LEVEL    '4
Application.Tree.Data.Blocks.B1.Input.PTOP      '0.18

#output 
Application.Tree.Data.Blocks.B1.Output.RR_OUT       '80~200 stages

Application.Tree.Data.Blocks.B1.Output.BOTTOM_TEMP  'Bottom temperature:92.94
Application.Tree.Data.Blocks.B1.Output.COND_DUTY    'Condenser cooling required:131783.434
Application.Tree.Data.Blocks.B1.Output.DISTIL_TEMP  'Distillate temperature:81.8517546
Application.Tree.Data.Blocks.B1.Output.DIST_VS_FEED 'Distillate to feed fraction:0.29610176
Application.Tree.Data.Blocks.B1.Output.FEED_LOCATN  'Feed stage:47.4289849
Application.Tree.Data.Blocks.B1.Output.MIN_REFLUX   'Minimum reflux ratio:11.3628666
Application.Tree.Data.Blocks.B1.Output.MIN_STAGES   'Minimum number of stages:53.3535077
Application.Tree.Data.Blocks.B1.Output.REB_DUTY     'Reboiler heating required:139444.64
Application.Tree.Data.Blocks.B1.Output.RECT_STAGES  'Number of actual stages above feed:46.4289849
Application.Tree.Data.Blocks.B1.Output.ACT_REFLUX   'Actual reflux ratio:17.0443
Application.Tree.Data.Blocks.B1.Output.ACT_STAGES   'Number of actual stages:80.9179894

Application.Tree.Data.Blocks.B1.Stream Results.Table
Application.Tree.Data.Blocks.B1.Stream Results.Table.  BENYIXI FEED     '6.720977
Application.Tree.Data.Blocks.B1.Stream Results.Table.  BENYIXI BOTTOM   '6.714256
Application.Tree.Data.Blocks.B1.Stream Results.Table.  YIBEN FEED       '2.825726
Application.Tree.Data.Blocks.B1.Stream Results.Table.  YIBEN BOTTOM     '0.00565145
Application.Tree.Data.Blocks.B1.Stream Results.Table.  YIBEN UP         '2.820075
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow kmol/hr FEED    '9.546703
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow kmol/hr BOTTOM  '6.719908
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow kmol/hr UP      '2.826796
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow kg/hr FEED      '1000
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow kg/hr BOTTOM    '699.9
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow kg/hr UP        '300.1
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow l/min FEED      '18.85769
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow l/min BOTTOM    '13.92351
Application.Tree.Data.Blocks.B1.Stream Results.Table.Total Flow l/min UP        '6.147346
Application.Tree.Data.Blocks.B1.Stream Results.Table.Temperature C FEED         '30

#RadFrac
Application.Tree.FindNode("\Data\Blocks\B1\Input\FEED_STAGE\FEED")      'feed stage
Application.Tree.FindNode("\Data\Blocks\B1\Input\STEFF_SEC\1")          'Murphree 0.45 section1
Application.Tree.FindNode("\Data\Blocks\B1\Input\STEFF_SEC\2")          'Murphree 0.55 section2
Application.Tree.FindNode("\Data\Blocks\B1\Input\STEFF_STAGE1\1")       'section1 starting stage
Application.Tree.FindNode("\Data\Blocks\B1\Input\STEFF_STAGE2\1")       'section1 ending stage
Application.Tree.FindNode("\Data\Blocks\B1\Input\STEFF_STAGE1\2")       'section2 starting stage
Application.Tree.FindNode("\Data\Blocks\B1\Input\STEFF_STAGE2\2")       'section2 ending stage


Temperature liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TL")
Temperature vapor to:    Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TVTO")
Mass flow liquid from:   Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LMF")
Mass flow vapor to:      Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VMF")
Volume flow liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LVF")
Volume flow vapor to:    Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VVF")
Molecular wt liquid from:Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWL")
Molecular wt vapor to:   Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWV")
Density liquid from:     Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOL")
Density vapor to:        Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOV")
Viscosity liquid from:   Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUL")
Viscosity vapor to:      Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUV")
Surface tension liquid from:    Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_STEN")
Foaming index:           Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FMIDX")
Flow parameter:          Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_PARM")
Reduced vapor:          Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_QR")
Reduced F factor        Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FFR")



tray sizing

Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Input\TS_STAGE2\1")     2
Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Tray Sizing\1\Input\TS_STAGE2\2")     90



#DSTWU
\Data\Blocks\B1\Output\MIN_REFLUX       最小回流比
\Data\Blocks\B1\Output\ACT_REFLUX       实际回流比
\Data\Blocks\B1\Output\MIN_STAGES       最小理论塔板数
\Data\Blocks\B1\Output\ACT_STAGES       实际塔板数
\Data\Blocks\B1\Output\FEED_LOCATN      进料塔板位置


