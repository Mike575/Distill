VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "精馏图解——老赖制作"
   ClientHeight    =   7920
   ClientLeft      =   2625
   ClientTop       =   2955
   ClientWidth     =   6630
   FillStyle       =   2  'Horizontal Line
   LinkTopic       =   "Form1"
   ScaleHeight     =   7920
   ScaleWidth      =   6630
   Begin VB.CommandButton Command1 
      Caption         =   "输入数据"
      Height          =   1215
      Left            =   3720
      TabIndex        =   14
      Top             =   840
      Width           =   1815
   End
   Begin VB.TextBox Text7 
      Height          =   615
      Left            =   1320
      TabIndex        =   12
      Text            =   "0.5"
      Top             =   6480
      Width           =   1335
   End
   Begin VB.TextBox Text6 
      Height          =   615
      Left            =   1320
      TabIndex        =   8
      Text            =   "0.1"
      Top             =   5520
      Width           =   1335
   End
   Begin VB.TextBox Text5 
      Height          =   615
      Left            =   1320
      TabIndex        =   7
      Text            =   "0.9"
      Top             =   4560
      Width           =   1335
   End
   Begin VB.TextBox Text4 
      Height          =   615
      Left            =   1320
      TabIndex        =   6
      Text            =   "2"
      Top             =   3600
      Width           =   1335
   End
   Begin VB.TextBox Text3 
      Height          =   615
      Left            =   1320
      TabIndex        =   2
      Text            =   "2"
      Top             =   2640
      Width           =   1335
   End
   Begin VB.TextBox Text2 
      Height          =   615
      Left            =   1320
      TabIndex        =   1
      Text            =   "2"
      Top             =   1680
      Width           =   1335
   End
   Begin VB.TextBox Text1 
      Height          =   615
      Left            =   1320
      TabIndex        =   0
      Text            =   "2"
      Top             =   720
      Width           =   1335
   End
   Begin VB.Frame Frame1 
      Caption         =   "Tips"
      Height          =   4695
      Left            =   3600
      TabIndex        =   15
      Top             =   2760
      Width           =   2175
      Begin VB.Label Label10 
         Caption         =   "  3.点击计算可使图像获得刷新。"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Left            =   120
         TabIndex        =   18
         Top             =   3000
         Width           =   1800
      End
      Begin VB.Label Label9 
         Caption         =   "  2.点击线段交点可获得对应数据。"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Left            =   120
         TabIndex        =   17
         Top             =   1680
         Width           =   1800
      End
      Begin VB.Label Label8 
         Caption         =   "  1.输入数据不能为空，否则装入默认值。"
         BeginProperty Font 
            Name            =   "宋体"
            Size            =   10.5
            Charset         =   134
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   855
         Left            =   120
         TabIndex        =   16
         Top             =   480
         Width           =   1800
      End
   End
   Begin VB.Label Label7 
      Caption         =   "进料 Xf"
      Height          =   255
      Left            =   240
      TabIndex        =   13
      Top             =   6600
      Width           =   735
   End
   Begin VB.Label Label6 
      Caption         =   "塔釜 Xw"
      Height          =   255
      Left            =   240
      TabIndex        =   11
      Top             =   5640
      Width           =   735
   End
   Begin VB.Label Label5 
      Caption         =   "塔顶 Xd"
      Height          =   255
      Left            =   240
      TabIndex        =   10
      Top             =   4680
      Width           =   735
   End
   Begin VB.Label Label4 
      Caption         =   "进料量  q n,F"
      Height          =   495
      Left            =   240
      TabIndex        =   9
      Top             =   3720
      Width           =   735
   End
   Begin VB.Label Label3 
      Caption         =   $"化工1.1.frx":0000
      Height          =   495
      Left            =   240
      TabIndex        =   5
      Top             =   1800
      Width           =   735
   End
   Begin VB.Label Label1 
      Caption         =   "相平衡α"
      Height          =   255
      Left            =   240
      TabIndex        =   4
      Top             =   840
      Width           =   735
   End
   Begin VB.Label Label2 
      Caption         =   "回流比 R"
      Height          =   255
      Left            =   240
      TabIndex        =   3
      Top             =   2640
      Width           =   735
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Public Sub Command1_Click()
Form2.Show
End Sub

Public Sub Command2_Click()
Form3.Show
End Sub

Private Sub Form_Load()
a = 2
d = 2
R = 2
qnF = 2
xD = 0.9
xW = 0.1
xF = 0.5
End Sub

Public Sub Text1_Change()
a = Val(Text1.Text)
End Sub

Public Sub Text2_Change()
d = Val(Text2.Text)
End Sub

Public Sub Text3_Change()
R = Val(Text3.Text)
End Sub

Public Sub Text4_Change()
qnF = Val(Text4.Text)
End Sub
Public Sub Text5_Change()
xD = Val(Text5.Text)
End Sub

Public Sub Text6_Change()
xW = Val(Text6.Text)
End Sub

Public Sub Text7_Change()
xF = Val(Text7.Text)
End Sub

