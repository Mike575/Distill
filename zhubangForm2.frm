VERSION 5.00
Begin VB.Form Form2 
   Caption         =   "图解法"
   ClientHeight    =   8160
   ClientLeft      =   2625
   ClientTop       =   2955
   ClientWidth     =   10635
   LinkTopic       =   "Form2"
   ScaleHeight     =   8160
   ScaleWidth      =   10635
   Begin VB.CommandButton Command2 
      Caption         =   "计算"
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
      Left            =   8040
      TabIndex        =   10
      Top             =   960
      Width           =   1695
   End
   Begin VB.TextBox Text4 
      Height          =   375
      Left            =   5040
      TabIndex        =   6
      Text            =   "Text4"
      Top             =   600
      Width           =   1815
   End
   Begin VB.TextBox Text3 
      Height          =   375
      Left            =   5040
      TabIndex        =   5
      Text            =   "Text3"
      Top             =   1200
      Width           =   1815
   End
   Begin VB.TextBox Text2 
      Height          =   375
      Left            =   1320
      TabIndex        =   1
      Text            =   "Text2"
      Top             =   1200
      Width           =   1815
   End
   Begin VB.TextBox Text1 
      Height          =   375
      Left            =   1320
      TabIndex        =   0
      Text            =   "Text1"
      Top             =   600
      Width           =   1815
   End
   Begin VB.Frame Frame1 
      Caption         =   "鼠标"
      Height          =   1695
      Left            =   360
      TabIndex        =   2
      Top             =   240
      Width           =   3135
      Begin VB.Label Label4 
         Caption         =   "Y 坐标"
         Height          =   255
         Left            =   240
         TabIndex        =   4
         Top             =   1080
         Width           =   615
      End
      Begin VB.Label Label3 
         Caption         =   "X 坐标"
         Height          =   255
         Left            =   240
         TabIndex        =   3
         Top             =   480
         Width           =   615
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "近似"
      Height          =   1695
      Left            =   3960
      TabIndex        =   7
      Top             =   240
      Width           =   3255
      Begin VB.Label Label2 
         Caption         =   "X 坐标"
         Height          =   255
         Left            =   240
         TabIndex        =   9
         Top             =   480
         Width           =   615
      End
      Begin VB.Label Label1 
         Caption         =   "Y 坐标"
         Height          =   255
         Left            =   240
         TabIndex        =   8
         Top             =   1080
         Width           =   615
      End
   End
End
Attribute VB_Name = "Form2"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Command1_Click()
Form3.Show
End Sub

Private Sub Command2_Click()

  Cls
  Form2.Scale (-100, 400)-(350, -50)
  Line (-100, 0)-(250, 0)
  Line (0, -100)-(0, 250)
  Line (0, 250)-(250, 250)
  Line (250, 0)-(250, 250)
  Line (0, 0)-(250, 250)
  
  CurrentX = -10: CurrentY = -10: Print "0"
  CurrentX = 250: CurrentY = -10: Print "x"
  CurrentX = 250: CurrentY = 0: Print "1.0"
  CurrentX = -10: CurrentY = 250: Print "Y"
  CurrentX = 0: CurrentY = 250: Print "1.0"
Ntzb = 0  '设置逐步计算起使为0
k = 1

dtx = (xF / (d - 1) + xD / (R + 1)) / (d / (d - 1) - R / (R + 1))
dty = d * dtx / (d - 1) - xF / (d - 1)
qnW = qnF * (xF - xD) / (xW - xD)
qnD = qnF * (xF - xW) / (xD - xW)
qnL = qnD * R


For i = 0 To 1 Step 0.00001       '相平衡方程
  X = i
  Y = a * i / (1 + (a - 1) * i)
  PSet (250 * X, 250 * Y)
Next i


For i = 0 To xD Step 0.0001        '精馏段方程
  X = i
  Y = R / (R + 1) * i + xD / (R + 1)
  PSet (250 * X, 250 * Y)
Next i

For i = xF To dtx Step 0.0001        'δ线方程
  X = i
  Y = d / (d - 1) * i - xF / (d - 1)
  PSet (250 * X, 250 * Y)
Next i
  
For i = 0 To dtx Step 0.0001        '提馏段方程
  X = i
  Y = i * (qnL + d * qnF) / ((qnL + d * qnF) - qnW) - qnW * xW / ((qnL + d * qnF) - qnW) '6.3PPt 第10页 公式
  PSet (250 * X, 250 * Y)
Next i

  ReDim Preserve zbx(4)              '准备
  ReDim Preserve zby(4)
  zbhy = xD
  zbhx = xD
  zbx(1) = zbhx
  zby(1) = zbhy

Do Until (zbhx < xW)
  zbzy = zbhy
  zbzx = zbhy / (a - zbhy * (a - 1))
  k = k + 1
  ReDim Preserve zbx(k)
  ReDim Preserve zby(k)
  zbx(k) = zbzx
  zby(k) = zbzy

  
  Line (250 * zbhx, 250 * zbhy)-(250 * zbzx, 250 * zbzy)  '画横线
  
  If (zbzx > dtx) Then
    zbhx = zbzx
    zbhy = R / (R + 1) * zbhx + xD / (R + 1)
  Else
    zbhx = zbzx
    zbhy = zbhx * (qnL + d * qnF) / ((qnL + d * qnF) - qnW) - qnW * xW / ((qnL + d * qnF) - qnW)
  End If
  Line (250 * zbhx, 250 * zbhy)-(250 * zbzx, 250 * zbzy)
  Ntzb = Ntzb + 1
  Print Ntzb

  k = k + 1
  ReDim Preserve zbx(k)
  ReDim Preserve zby(k)
  zbx(k) = zbhx
  zby(k) = zbhy
  
Loop
 
zbs = k
i2 = zbs

End Sub

Private Sub Form_MouseMove(Button As Integer, Shift As Integer, X As Single, Y As Single)
Text1.Text = Format(X / 250, "0.000")
Text2.Text = Format(Y / 250, "0.000")
End Sub





Private Sub Form_MouseUp(Button As Integer, Shift As Integer, X As Single, Y As Single)
Dim xzX As Single
Dim xzY As Single
ReDim Preserve zbx(k)              '准备
ReDim Preserve zby(k)
xzX = X
xzY = Y
i2 = zbs
Do Until (i2 <= 0)
    If (xzX - 250 * zbx(i2)) ^ 2 < 9 And (xzY - 250 * zby(i2)) ^ 2 < 9 Then
    Text4.Text = Format(zbx(i2), "0.000")
    Text3.Text = Format(zby(i2), "0.000")
    End If
    i2 = i2 - 1
Loop
i2 = zbs


tsdx(0) = xF: tsdy(0) = xF
tsdx(1) = dtx: tsdy(1) = dty
tsdx(2) = xW: tsdy(2) = xW
tsdx(3) = xD: tsdy(3) = xD
tsdx(4) = 1: tsdy(4) = 1
tsdx(5) = 0: tsdy(5) = 0
tsdx(6) = 0: tsdy(6) = -qnW * xW / ((qnL + d * qnF) - qnW)
tsdx(7) = 0: tsdy(7) = xD / (R + 1)
tss = 7
Do Until (tss < 0)
    If (xzX - 250 * tsdx(tss)) ^ 2 < 9 And (xzY - 250 * tsdy(tss)) ^ 2 < 9 Then
    Text4.Text = Format(tsdx(tss), "0.000")
    Text3.Text = Format(tsdy(tss), "0.000")
    End If
    tss = tss - 1
Loop




End Sub


