Imports STKTVMain
Imports System.IO
Imports System.Threading

Public Class Driver
    Inherits STKTVMain.TVDriver
    Private mIsConnected As Boolean

    Dim POVer As String = "0.0"
    Dim isVKT4M As Boolean = False
    Public Structure MArchive
        Public DateArch As DateTime
        Public HC As Long
        Public MsgHC As String

        Public HCtv1 As Long
        Public MsgHC_1 As String

        Public HCtv2 As Long
        Public MsgHC_2 As String

        Public M1 As Single
        Public M2 As Single
        Public M3 As Single
        Public M4 As Single
        Public M5 As Single
        Public M6 As Single

        Public V1 As Single
        Public V2 As Single
        Public V3 As Single
        Public V4 As Single
        Public V5 As Single
        Public V6 As Single


        Public G1 As Single
        Public G2 As Single
        Public G3 As Single
        Public G4 As Single
        Public G5 As Single
        Public G6 As Single

        Public Q1 As Single
        Public Q2 As Single
        Public Q3 As Single
        Public Q4 As Single
        Public Q5 As Single
        Public Q6 As Single
        Public dQ1 As Single
        Public dQ2 As Single

        Public t1 As Single
        Public t2 As Single
        Public t3 As Single
        Public t4 As Single
        Public t5 As Single
        Public t6 As Single

        Public p1 As Single
        Public p2 As Single
        Public p3 As Single
        Public p4 As Single
        Public p5 As Single
        Public p6 As Single

        Public dt12 As Single
        Public dt45 As Single

        Public tx1 As Single
        Public tx2 As Single

        Public tair1 As Single
        Public tair2 As Single

        Public SP As Long
        Public SPtv1 As Long
        Public SPtv2 As Long


        Public OKTIME1 As Single
        Public OKTIME2 As Single
        Public ERRTIME1 As Single
        Public ERRTIME2 As Single
        Public WORKTIME1 As Single
        Public WORKTIME2 As Single

        Public archType As Short
    End Structure

    Public Structure Archive
        Public DateArch As DateTime

        Public HC As Long
        Public MsgHC As String

        Public HCtv1 As Long
        Public MsgHC_1 As String

        Public HCtv2 As Long
        Public MsgHC_2 As String

        Public V1 As Single
        Public V2 As Single
        Public V3 As Single
        Public v4 As Single
        Public v5 As Single
        Public v6 As Single

        Public V1H As Single
        Public V2H As Single
        Public V3H As Single
        Public v4H As Single
        Public v5H As Single
        Public v6H As Single

        Public P1 As Single
        Public P2 As Single
        Public P3 As Single
        Public P4 As Single
        Public P5 As Single
        Public P6 As Single

        Public T1 As Single
        Public T2 As Single
        Public T3 As Single
        Public T4 As Single
        Public T5 As Single
        Public T6 As Single


        Public Q1 As Single
        Public Q2 As Single
        Public Q3 As Single
        Public Q4 As Single
        Public Q5 As Single
        Public Q6 As Single

        Public Q1H As Single
        Public Q2H As Single
        Public Q3H As Single
        Public Q4H As Single
        Public Q5H As Single
        Public Q6H As Single

        'Public QG1 As Single
        'Public QG2 As Single

        Public M1 As Single
        Public M2 As Single
        Public M3 As Single
        Public M4 As Single
        Public M5 As Single
        Public M6 As Single

        Public SP As Long
        Public SPtv1 As Long
        Public SPtv2 As Long

        Public tx1 As Single
        Public tx2 As Single
        Public tair1 As Single
        Public tair2 As Single

        Public OKTIME1 As Single
        Public OKTIME2 As Single
        Public ERRTIME As Single
        Public ERRTIMEH As Single
        Public ERRTIME1 As Single
        Public ERRTIME2 As Single
        Public WORKTIME1 As Single
        Public WORKTIME2 As Single


        Public G1 As Single
        Public G2 As Single
        Public G3 As Single
        Public G4 As Single
        Public G5 As Single
        Public G6 As Single

        Public archType As Short
    End Structure


    Public Structure TArchive
        Public DateArch As DateTime

        Public HC As Long
        Public MsgHC As String

        Public HCtv1 As Long
        Public MsgHC_1 As String

        Public HCtv2 As Long
        Public MsgHC_2 As String

        Public V1 As Double
        Public V2 As Double
        Public V3 As Double
        Public V4 As Double
        Public V5 As Double
        Public V6 As Double

        Public M1 As Double
        Public M2 As Double
        Public M3 As Double
        Public M4 As Double
        Public M5 As Double
        Public M6 As Double

        Public P1 As Single
        Public P2 As Single
        Public P3 As Single
        Public P4 As Single
        Public P5 As Single
        Public P6 As Single


        Public T1 As Single
        Public T2 As Single
        Public T3 As Single
        Public T4 As Single
        Public T5 As Single
        Public T6 As Single

        Public Q1 As Double
        Public Q2 As Double
        Public Q3 As Double
        Public Q4 As Double
        Public Q5 As Double
        Public Q6 As Double

        Public OKTIME1 As Single
        Public OKTIME2 As Single

        Public ERRTIME1 As Single
        Public ERRTIME2 As Single

        Public WORKTIME1 As Single
        Public WORKTIME2 As Single

        Public archType As Short
    End Structure

    Protected Sub clearTArchive(ByRef arc As TArchive)
        arc.DateArch = DateTime.MinValue
        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHC_1 = ""

        arc.HCtv2 = 0
        arc.MsgHC_2 = ""


        arc.ERRTIME1 = 0
        arc.ERRTIME2 = 0
        arc.WORKTIME1 = 0
        arc.WORKTIME2 = 0
        arc.OKTIME1 = 0
        arc.OKTIME2 = 0



        arc.T1 = Single.NaN
        arc.T2 = Single.NaN
        arc.T3 = Single.NaN
        arc.T4 = Single.NaN
        arc.T5 = Single.NaN
        arc.T6 = Single.NaN

        arc.P1 = Single.NaN
        arc.P2 = Single.NaN
        arc.P3 = Single.NaN
        arc.P4 = Single.NaN
        arc.P5 = Single.NaN
        arc.P6 = Single.NaN


        arc.M1 = Single.NaN
        arc.M2 = Single.NaN
        arc.M3 = Single.NaN
        arc.M4 = Single.NaN
        arc.M5 = Single.NaN
        arc.M6 = Single.NaN

        arc.V1 = Single.NaN
        arc.V2 = Single.NaN
        arc.V3 = Single.NaN
        arc.V4 = Single.NaN
        arc.V5 = Single.NaN
        arc.V6 = Single.NaN

        arc.Q1 = Single.NaN
        arc.Q2 = Single.NaN
        arc.Q3 = Single.NaN
        arc.Q4 = Single.NaN
        arc.Q5 = Single.NaN
        arc.Q6 = Single.NaN
        arc.archType = 2
        isTArchToDBWrite = False
    End Sub
    Protected Sub clearArchive(ByRef arc As Archive)
        arc.DateArch = DateTime.MinValue
        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHC_1 = ""

        arc.HCtv2 = 0
        arc.MsgHC_2 = ""


        arc.ERRTIME1 = 0
        arc.ERRTIME2 = 0
        arc.WORKTIME1 = 0
        arc.WORKTIME2 = 0
        arc.OKTIME1 = 0
        arc.OKTIME2 = 0



        arc.T1 = Single.NaN
        arc.T2 = Single.NaN
        arc.T3 = Single.NaN
        arc.T4 = Single.NaN
        arc.T5 = Single.NaN
        arc.T6 = Single.NaN

        arc.P1 = Single.NaN
        arc.P2 = Single.NaN
        arc.P3 = Single.NaN
        arc.P4 = Single.NaN
        arc.P5 = Single.NaN
        arc.P6 = Single.NaN


        arc.M1 = Single.NaN
        arc.M2 = Single.NaN
        arc.M3 = Single.NaN
        arc.M4 = Single.NaN
        arc.M5 = Single.NaN
        arc.M6 = Single.NaN

        arc.V1 = Single.NaN
        arc.V2 = Single.NaN
        arc.V3 = Single.NaN
        arc.v4 = Single.NaN
        arc.v5 = Single.NaN
        arc.v6 = Single.NaN

        arc.V1H = Single.NaN
        arc.V2H = Single.NaN
        arc.V3H = Single.NaN
        arc.v4H = Single.NaN
        arc.v5H = Single.NaN
        arc.v6H = Single.NaN

        arc.Q1 = Single.NaN
        arc.Q2 = Single.NaN
        arc.Q3 = Single.NaN
        arc.Q4 = Single.NaN
        arc.Q5 = Single.NaN
        arc.Q6 = Single.NaN


        arc.Q1H = Single.NaN
        arc.Q2H = Single.NaN
        arc.Q3H = Single.NaN
        arc.Q4H = Single.NaN
        arc.Q5H = Single.NaN
        arc.Q6H = Single.NaN
        arc.archType = 0
        isArchToDBWrite = False
    End Sub

    Protected Sub clearMArchive(ByRef arc As MArchive)
        arc.DateArch = DateTime.MinValue
        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHC_1 = ""

        arc.HCtv2 = 0
        arc.MsgHC_2 = ""


        arc.ERRTIME1 = 0
        arc.ERRTIME2 = 0
        arc.WORKTIME1 = 0
        arc.WORKTIME2 = 0
        arc.OKTIME1 = 0
        arc.OKTIME2 = 0



        arc.t1 = Single.NaN
        arc.t2 = Single.NaN
        arc.t3 = Single.NaN
        arc.t4 = Single.NaN
        arc.t5 = Single.NaN
        arc.t6 = Single.NaN

        arc.p1 = Single.NaN
        arc.p2 = Single.NaN
        arc.p3 = Single.NaN
        arc.p4 = Single.NaN
        arc.p5 = Single.NaN
        arc.p6 = Single.NaN


        arc.M1 = Single.NaN
        arc.M2 = Single.NaN
        arc.M3 = Single.NaN
        arc.M4 = Single.NaN
        arc.M5 = Single.NaN
        arc.M6 = Single.NaN

        arc.V1 = Single.NaN
        arc.V2 = Single.NaN
        arc.V3 = Single.NaN
        arc.V4 = Single.NaN
        arc.V5 = Single.NaN
        arc.V6 = Single.NaN


        arc.G1 = Single.NaN
        arc.G2 = Single.NaN
        arc.G3 = Single.NaN
        arc.G4 = Single.NaN
        arc.G5 = Single.NaN
        arc.G6 = Single.NaN

        arc.Q1 = Single.NaN
        arc.Q2 = Single.NaN
        arc.Q3 = Single.NaN
        arc.Q4 = Single.NaN
        arc.Q5 = Single.NaN
        arc.Q6 = Single.NaN




        arc.archType = 1
        isMArchToDBWrite = False
    End Sub




    Dim archType_hour = 3
    Dim archType_day = 4

    Dim Arch As Archive
    Dim mArch As MArchive
    Dim tArch As TArchive

    Dim WillCountToRead As Short = 0
    '  Dim IsBytesToRead As Boolean = False
    'Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False
    Dim IsTArchToRead As Boolean = False
    Dim buffer(32767) As Byte
    Dim bufferindex As Short = 0

    Dim m_isArchToDBWrite As Boolean = False

    Public Overrides Property isArchToDBWrite() As Boolean

        Get
            Return m_isArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isArchToDBWrite = value
        End Set
    End Property

    Dim m_isMArchToDBWrite As Boolean = False
    Public Overrides Property isMArchToDBWrite() As Boolean

        Get
            Return m_isMArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isMArchToDBWrite = value
        End Set
    End Property
    Dim m_isTArchToDBWrite As Boolean = False

    Public Overrides Property isTArchToDBWrite() As Boolean

        Get
            Return m_isTArchToDBWrite
        End Get
        Set(ByVal value As Boolean)
            m_isTArchToDBWrite = value
        End Set
    End Property

    Public Overrides Function CounterName() As String
        Return "VKT4M_82"
    End Function

    Public Function GetAndProcessData() As String
        Dim buf(100) As Byte
        Dim i As Int16
        For i = 0 To 100
            buf(i) = 0
        Next

        Dim ret As Long = 1
        bufferindex = 0
        Dim nomore As Integer = 0
        While (bufferindex < WillCountToRead And nomore <= 20)
            Try
                ret = MyRead(buf, bufferindex, WillCountToRead - bufferindex, CalcInterval(WillCountToRead))
                If (ret > 0) Then
                    If (buf(2) > &HC1) Then
                        EraseInputQueue()
                        Return "Ошибка. Код ошибки:" + Hex(buf(4))
                    End If
                    nomore = 0
                    bufferindex += ret
                    If (bufferindex >= WillCountToRead) Then
                        Return ProcessReceivedData(buf, bufferindex)
                    End If
                Else
                    If nomore = 20 And bufferindex >= 7 Then
                        Return ProcessReceivedData(buf, bufferindex)
                    End If
                    nomore += 1
                End If
            Catch ex As Exception
                EraseInputQueue()
                Return "Ошибка."
            End Try
            System.Threading.Thread.Sleep(CalcInterval(10))
        End While
        If (bufferindex >= 7) Then
            Return ProcessReceivedData(buf, bufferindex)
        End If
        Return ""
    End Function

    Public Overrides Sub Connect()
        Dim i As Integer

        For i = 0 To 5
            If TryConnect() Then
                Return ' True
            End If
            Thread.Sleep(3000)
        Next
        Return 'False
    End Sub
    'Private seekIdx As Integer = 0

    Private Function TryConnect() As Boolean
        EraseInputQueue()

        Dim startBytes(0 To 15) As Byte
        Dim i As Int16



        For i = 0 To 15
            startBytes(i) = &HFF
        Next


        write(startBytes, 16)


        System.Threading.Thread.Sleep(CalcInterval(10))


        Dim bArr(0 To 10) As Byte
        Try

            bArr(0) = &H10
            bArr(1) = &H0
            bArr(2) = &H41
            bArr(3) = &H0
            bArr(4) = &H0
            bArr(5) = &H0
            bArr(6) = &H0
            bArr(7) = &H0
            bArr(8) = &H0
            bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
            bArr(10) = &H16


            EraseInputQueue()
            WillCountToRead = 8
            write(bArr, 11)


            Dim sret As String
            WaitForData()
            sret = GetAndProcessData()
            If (sret.Length > 5) Then
                If (sret.Substring(0, 6) = "Ошибка") Then
                    EraseInputQueue()
                    Return False
                End If
                Debug.Print("!connected !!!")

                mIsConnected = True


                bArr(0) = &H10
                bArr(1) = &H0
                bArr(2) = &H46
                bArr(3) = &H0
                bArr(4) = &H0
                bArr(5) = &H0
                bArr(6) = &H0
                bArr(7) = &H0
                bArr(8) = &H0
                bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
                bArr(10) = &H16

                EraseInputQueue()

                WillCountToRead = 17
                write(bArr, 11)

                WaitForData()
                sret = GetAndProcessData()
                If (sret.Length > 5) Then
                    If (sret.Substring(0, 6) = "Ошибка") Then
                        EraseInputQueue()
                        Return False
                    End If
                End If
                EraseInputQueue()

                Return True
            End If
            If sret.Length = 0 Then
                DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            End If

        Catch exc As Exception
            Return False
        End Try

    End Function

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String



        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next
        EraseInputQueue()
        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(15))

        clearArchive(Arch)
        Arch.archType = ArchType
        EraseInputQueue()
        Dim bArr(0 To 10) As Byte
        Dim ret As String = ""
        Dim retsum As String = ""
        Dim trycnt As Int32
        Dim tv1OK As Boolean = False
        trycnt = 5
tryagain1:


        If (ArchType = archType_hour) Then
            bArr(2) = &H42 'часовой архив
            bArr(3) = ArchYear \ 256
            bArr(4) = ArchYear Mod 256
            bArr(5) = ArchMonth Mod 13
            bArr(6) = ArchDay Mod 32
            bArr(7) = ArchHour Mod 24
            bArr(8) = &H1

            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)


            'If isVKT4M Then
            WillCountToRead = 82 + 7
            'Else
            '    WillCountToRead = 56 '44 + 7
            'End If
        End If

        If (ArchType = archType_day) Then
            bArr(2) = &H43 'суточный архив
            bArr(3) = ArchYear \ 256
            bArr(4) = ArchYear Mod 256
            bArr(5) = ArchMonth Mod 13
            bArr(6) = ArchDay Mod 32
            bArr(7) = &H0
            bArr(8) = &H1

            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)

            'If isVKT4M Then
            WillCountToRead = 82 + 7
            'Else
            '    WillCountToRead = 66 ' 54 + 7
            'End If

        End If

        If ArchType = archType_hour And Arch.DateArch > Date.Now.AddHours(-1) Then GoTo finalRet
        If ArchType = archType_day And Arch.DateArch > Date.Now.AddDays(-1) Then GoTo finalRet

        bArr(0) = &H10
        bArr(1) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16
        tv1OK = True



        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                retsum = retsum + ret
                If trycnt = 0 Then
                    'Return retsum
                    trycnt = 5
                    GoTo finalRet
                Else
                    trycnt -= 1
                    GoTo tryagain1
                End If
            Else
                tv1OK = True
            End If
        End If
        If (ret.Length = 0) Then
            EraseInputQueue()
            retsum = retsum & vbCrLf & "Ошибка чтения архива"
            tv1OK = False

        End If




finalRet:
        If tv1OK Then
            retsum = "Архив прочитан" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            isArchToDBWrite = True
            Return retsum
        Else
            retsum = "Ошибка чтения" & vbCrLf & retsum
            retsum = retsum & vbCrLf
            EraseInputQueue()
            Return retsum
        End If

    End Function

    Dim m_Param As String = ""

    Public Property Param() As String

        Get
            Return m_Param
        End Get
        Set(ByVal value As String)
            m_Param = value
        End Set
    End Property

    Public Function ProcessReceivedData(ByVal buf() As Byte, ByVal ret As Short) As String
        Dim retstring As String = ""

        Dim KC As Long = 0
        Try

            If (buf(2) = &H41) Then 'установка связи
                Dim hStr As String = ""
                Dim i As Integer
                For i = 0 To ret - 1
                    hStr += (buf(i).ToString("x02") & " ")
                Next


                For i = 0 To ret - 2
                    retstring += Hex(buf(i)) + " "
                Next
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка! Контрольная сумма не совпала! " & ret.ToString() & " байт. " & hStr
                    'Return ""
                End If
                Return retstring
            End If




            If (buf(2) = &H44 And IsTArchToRead = True) Then 'чтение тотального архива
                IsTArchToRead = False
                Dim i As Integer
                Dim str As String = ""

                Dim hStr As String = ""

                For i = 0 To ret - 1
                    hStr += (buf(i).ToString("x02") & " ")
                Next
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка! Контрольная сумма не совпала! " & ret.ToString() & " байт. " & hStr
                    'Return ""
                End If

                For i = 5 To ret - 2
                    str = str + Chr(buf(i))
                Next

                tArch.archType = 2
                Dim Adr As Long
                Adr = 1
                If str.Length > 24 Then

                    tArch.V1 = FloatExt(Mid(str, Adr + 4 * 0, 4))
                    tArch.V2 = FloatExt(Mid(str, Adr + 4 * 1, 4))
                    tArch.V3 = FloatExt(Mid(str, Adr + 4 * 2, 4))
                    tArch.V4 = FloatExt(Mid(str, Adr + 4 * 3, 4))
                    tArch.Q1 = FloatExt(Mid(str, Adr + 4 * 4, 4))
                    tArch.Q2 = FloatExt(Mid(str, Adr + 4 * 5, 4))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If
                If isVKT4M Then
                    Try
                        tArch.V5 = FloatExt(Mid(str, Adr + 4 * 6, 4))

                        'tArch.V3 = FloatExt(Mid(str, Adr + 4 * 8, 4))
                        'tArch.V4 = FloatExt(Mid(str, Adr + 4 * 9, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        tArch.V6 = FloatExt(Mid(str, Adr + 4 * 7, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        tArch.Q3 = FloatExt(Mid(str, Adr + 4 * 10, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        tArch.Q4 = FloatExt(Mid(str, Adr + 4 * 11, 4))
                    Catch ex As Exception

                    End Try


                End If

                m_isTArchToDBWrite = True
                Return "архив прочитан"
            End If

            If (buf(2) = &H45 And IsmArchToRead = True) Then 'чтение мгновенного архива
                IsmArchToRead = False
                Dim i As Integer
                Dim str As String = ""
                Dim hStr As String = ""

                For i = 0 To ret - 1
                    hStr += (buf(i).ToString("x02") & " ")
                Next
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка! Контрольная сумма не совпала! " & ret.ToString() & " байт. " & hStr
                    'Return ""
                End If

                For i = 5 To 14
                    str = str + Chr(buf(i))
                Next

                mArch.archType = 1
                Dim Adr As Long
                Adr = 1

                Dim DateArchYear As Integer
                Dim DateArchMonth As Integer
                Dim DateArchDay As Integer
                Dim DateArchHour As Integer

                Dim DateNowYear As Integer
                Dim DateNowMonth As Integer
                Dim DateNowDay As Integer
                Dim DateNowHour As Integer

                If str.Length >= 10 Then
                    DateArchYear = ExtLong2(Mid(str, Adr, 2))
                    DateArchMonth = Asc(Mid(str, Adr + 2, 1))
                    DateArchDay = Asc(Mid(str, Adr + 3, 1))
                    DateArchHour = Asc(Mid(str, Adr + 4, 1))

                    DateNowYear = ExtLong2(Mid(str, Adr + 5, 2))
                    DateNowMonth = Asc(Mid(str, Adr + 7, 1))
                    DateNowDay = Asc(Mid(str, Adr + 8, 1))
                    DateNowHour = Asc(Mid(str, Adr + 9, 1))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If

                If isVKT4M Then
                    Try
                        mArch.p1 = FloatExt(Mid(str, Adr + 10, 4)) / 100
                    Catch ex As Exception

                    End Try
                    Try
                        mArch.p2 = FloatExt(Mid(str, Adr + 10 + 4 * 1, 4)) / 100
                    Catch ex As Exception

                    End Try
                    Try
                        mArch.p3 = FloatExt(Mid(str, Adr + 10 + 4 * 1, 4)) / 100
                    Catch ex As Exception

                    End Try
                    Try
                        mArch.p4 = FloatExt(Mid(str, Adr + 10 + 4 * 1, 4)) / 100
                    Catch ex As Exception

                    End Try

                End If

                Try
                    mArch.DateArch = New DateTime(CType(DateNowYear, Long), CType(DateNowMonth, Long), CType(DateNowDay, Long), CType(DateNowHour, Long), 0, 0)
                Catch ex As Exception
                    mArch.DateArch = DateTime.Now
                End Try

                m_isMArchToDBWrite = True
                Return "архив прочитан"
            End If

            If (buf(2) = &H42) Then 'часовой архив

                Dim hourstr As String = ""
                Dim i As Int32
                Dim hStr As String = ""

                For i = 0 To ret - 1
                    hStr += (buf(i).ToString("x02") & " ")
                Next

                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next


                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка! Контрольная сумма не совпала! " & ret.ToString() & " байт. " & hStr
                    'Return ""
                End If

                For i = 10 To ret - 2
                    hourstr = hourstr + Chr(buf(i))
                Next
                Arch.archType = archType_hour
                Dim Adr As Long
                Adr = 1
                If hourstr.Length > &H2C Then
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + &H0, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + &H4, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + &H8, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + &HC, 4))

                    Arch.M1 = FloatExt(Mid(hourstr, Adr + &H10, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + &H14, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + &H18, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + &H1C, 4))

                    Arch.T1 = ExtLong2(Mid(hourstr, Adr + &H20, 2)) / 100.0
                    Arch.T2 = ExtLong2(Mid(hourstr, Adr + &H22, 2)) / 100.0
                    Arch.T3 = ExtLong2(Mid(hourstr, Adr + &H24, 2)) / 100.0
                    Arch.T4 = ExtLong2(Mid(hourstr, Adr + &H26, 2)) / 100.0



                    Arch.HC = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + &H29, 1))
                    Arch.ERRTIME1 = Asc(Mid(hourstr, Adr + &H2A, 1))
                    Arch.ERRTIME2 = Asc(Mid(hourstr, Adr + &H2C, 1))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If
                If isVKT4M Then
                    Try
                        Arch.Q1 = FloatExt(Mid(hourstr, Adr + &H2E, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.Q2 = FloatExt(Mid(hourstr, Adr + &H32, 4))
                    Catch ex As Exception

                    End Try

                    Try
                        Arch.P1 = FloatExt(Mid(hourstr, Adr + &H36, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P2 = FloatExt(Mid(hourstr, Adr + &H38, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P3 = FloatExt(Mid(hourstr, Adr + &H3A, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P4 = FloatExt(Mid(hourstr, Adr + &H3C, 4))
                    Catch ex As Exception

                    End Try
                End If


                m_isArchToDBWrite = True
                'Arch.DateArch
                Return "архив прочитан"
            End If
            If (buf(2) = &H43) Then 'суотчный архив
                'If (tv = 0) Then Return ""
                Dim hourstr As String = ""
                Dim i As Int32
                Dim hStr As String = ""

                For i = 0 To ret - 1
                    hStr += (buf(i).ToString("x02") & " ")
                Next

                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка! Контрольная сумма не совпала! " & ret.ToString() & " байт. " & hStr
                    'Return ""
                End If

                For i = 10 To ret - 2
                    hourstr = hourstr + Chr(buf(i))
                Next


                Arch.archType = archType_day
                Dim Adr As Long
                Adr = 1


                If hourstr.Length > &H2C Then
                    Arch.V1 = FloatExt(Mid(hourstr, Adr + &H0, 4))
                    Arch.V2 = FloatExt(Mid(hourstr, Adr + &H4, 4))
                    Arch.V3 = FloatExt(Mid(hourstr, Adr + &H8, 4))
                    Arch.v4 = FloatExt(Mid(hourstr, Adr + &HC, 4))

                    Arch.M1 = FloatExt(Mid(hourstr, Adr + &H10, 4))
                    Arch.M2 = FloatExt(Mid(hourstr, Adr + &H14, 4))
                    Arch.M3 = FloatExt(Mid(hourstr, Adr + &H18, 4))
                    Arch.M4 = FloatExt(Mid(hourstr, Adr + &H1C, 4))

                    Arch.T1 = ExtLong2(Mid(hourstr, Adr + &H20, 2)) / 100.0
                    Arch.T2 = ExtLong2(Mid(hourstr, Adr + &H22, 2)) / 100.0
                    Arch.T3 = ExtLong2(Mid(hourstr, Adr + &H24, 2)) / 100.0
                    Arch.T4 = ExtLong2(Mid(hourstr, Adr + &H26, 2)) / 100.0

                    Arch.HC = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv1 = Asc(Mid(hourstr, Adr + &H28, 1))
                    Arch.HCtv2 = Asc(Mid(hourstr, Adr + &H29, 1))
                    Arch.ERRTIME1 = Asc(Mid(hourstr, Adr + &H2A, 1))
                    Arch.ERRTIME2 = Asc(Mid(hourstr, Adr + &H2C, 1))
                Else
                    Return "Ошибка! Неверный размер записи"
                End If

                If hourstr.Length >= &H36 Then
                    Arch.Q1 = FloatExt(Mid(hourstr, Adr + &H2E, 4))
                    Arch.Q2 = FloatExt(Mid(hourstr, Adr + &H32, 4))
                End If

                If isVKT4M Then
                    Try
                        Arch.P1 = FloatExt(Mid(hourstr, Adr + &H36, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P2 = FloatExt(Mid(hourstr, Adr + &H38, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P3 = FloatExt(Mid(hourstr, Adr + &H3A, 4))
                    Catch ex As Exception

                    End Try
                    Try
                        Arch.P4 = FloatExt(Mid(hourstr, Adr + &H3C, 4))
                    Catch ex As Exception

                    End Try

                End If


                m_isArchToDBWrite = True
                'Arch.DateArch = DateTime.Now
                Return "Архив прочитан"
            End If
            If (buf(2) = &H46) Then 'чтение параметров

                Dim i As Integer
                Dim str As String = ""
                Dim hStr As String = ""

                For i = 0 To ret - 1
                    hStr += (buf(i).ToString("x02") & " ")
                Next
                KC = 0
                For i = 1 To ret - 3
                    KC = (KC + Int(buf(i))) Mod 256
                Next
                KC = (256 - KC) Mod 256
                If (KC <> buf(ret - 2)) Then
                    Return "Ошибка! Контрольная сумма не совпала! " & ret.ToString() & " байт. " & hStr
                    'Return ""
                End If

                If (buf(5) And &HF0) = &H10 Then
                    isVKT4M = False
                Else
                    isVKT4M = True
                End If
                POVer = BCD(buf(5))

                str = ""
                For i = 5 To ret - 2
                    str = str & Chr(buf(i))
                Next


                m_Param = str
                Return Param
                'Return "Параметры прочитаны"
            End If
            'MsgBox("Пакет распознан некорректно!", MsgBoxStyle.OkOnly, "Ошибка")
            retstring = "Ошибка"
            Return retstring
        Catch exc As Exception
            MsgBox(exc.Message + " " + exc.StackTrace)
        End Try
        Return "Ошибка! Пакет не распознан"
    End Function

    Private Function ExtLong4(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        For i = 0 To 3
            ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        Next i
    End Function

    Private Function ExtLong2(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong2 = 0
        For i = 0 To 1
            ExtLong2 = ExtLong2 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (1 - i))
        Next i
    End Function

    Private Function FloatExt(ByVal floatStr As String) As Single
        Dim tmpStr As String = ""
        Dim E As Long
        Dim Mantissa As Long
        Dim s As Long
        Dim f As Single
        Dim i As Long
        'If floatStr = "" Then Exit Function
        If floatStr.Length <> 4 Then Exit Function
        ' If floatStr = String(4, 0) Then Exit Function
        If floatStr = Chr(0) + Chr(0) + Chr(0) + Chr(0) Then
            Return 0.0
        End If
        'For i = 1 To 4
        '    tmpStr = Chr(Asc(Mid(floatStr, i, 1))) & tmpStr
        'Next i


        'floatStr = tmpStr
        '================ Float число========================
        'ст.байт                                 младший байт
        '====================================================
        'двоич.порядок |ст.байт                  младший байт
        '----------------------------------------------------
        ' xxxx xxxx     | sxxx xxxx | xxxx xxxx | xxxx xxxx |

        ' A = (-1)^s * f * 2^(e-127)
        ' f= сумма от 0 до 23 a(k)*2^(-k), где a(k) бит мантисы с номером k


        E = Asc(Mid(floatStr, 1, 1))
        If Asc(Mid(floatStr, 2, 1)) And (2 ^ 7) Then
            s = 1
        Else
            s = 0
        End If
        'Mantissa = ((Asc(Mid(floatStr, 2, 1)) And &H7F) << 16) _
        '             + (Asc(Mid(floatStr, 3, 1)) << 8) _
        '             + (Asc(Mid(floatStr, 4, 1)))

        Mantissa = (Asc(Mid(floatStr, 2, 1)) And &H7F) * (2 ^ 16) _
                             + Asc(Mid(floatStr, 3, 1)) * (2 ^ 8) _
                             + Asc(Mid(floatStr, 4, 1))

        f = 2 ^ 0
        For i = 22 To 0 Step -1
            If Mantissa And 2& ^ i Then
                f = f + 2 ^ (i - 23)
            End If
        Next i
        FloatExt = (-1) ^ s * f * 2.0! ^ (E - 127)
    End Function


    Public Overrides Function ReadMArch() As String

        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next

        EraseInputQueue()
        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(10))
        Dim ret As String
        Dim bArr(0 To 10) As Byte
        bArr(0) = &H10
        bArr(1) = &H0
        bArr(2) = &H45
        bArr(3) = &H0
        bArr(4) = &H0
        bArr(5) = &H0
        bArr(6) = &H0
        bArr(7) = &H0
        bArr(8) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16
        IsmArchToRead = True
        clearMArchive(mArch)
        EraseInputQueue()

        WillCountToRead = 18 + 7


        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret
                ret = ret & vbCrLf
                ret = ret + "Архив не прочитан"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If

        'mArch.DateArch = DateTime.Now
        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения даты мгновенного архива "
        End If
        m_isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function
    Public Overrides Function ReadTArch() As String




        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next
        EraseInputQueue()
        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(10))

        Dim ret As String
        Dim bArr(0 To 10) As Byte
        bArr(0) = &H10
        bArr(1) = &H0
        bArr(2) = &H44
        bArr(3) = &H0
        bArr(4) = &H0
        bArr(5) = &H0
        bArr(6) = &H0
        bArr(7) = &H0
        bArr(8) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16
        IsTArchToRead = True
        clearTArchive(tArch)
        EraseInputQueue()


        'If isVKT4M Then
        WillCountToRead = 48 + 7
        'Else
        '    WillCountToRead = 24 + 7
        'End If


        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then
                ret = ret
                ret = ret & vbCrLf
                ret = ret + "Архив не прочитан"
                ret = ret & vbCrLf
                EraseInputQueue()
                Return ret
            End If
        End If

        tArch.DateArch = DateTime.Now

        If (ret.Length = 0) Then
            EraseInputQueue()
            Return "Ошибка чтения даты тотального архива "
        End If
        isTArchToDBWrite = True
        Return "Тотальный архив прочитан"
    End Function





    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function

    Public Overrides Function ReadSystemParameters() As System.Data.DataTable
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")
        Dim dr As DataRow

        Dim startBytes(0 To 15) As Byte
        Dim i As Int16

        For i = 0 To 15
            startBytes(i) = &HFF
        Next

        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(10))

        Dim ret As String
        Dim bArr(0 To 10) As Byte
        bArr(0) = &H10
        bArr(1) = &H0
        bArr(2) = &H46
        bArr(3) = &H0
        bArr(4) = &H0
        bArr(5) = &H0
        bArr(6) = &H0
        bArr(7) = &H0
        bArr(8) = &H0
        bArr(9) = (256 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6)) + Int(bArr(7)) + Int(bArr(8)))) Mod 256) Mod 256
        bArr(10) = &H16

        EraseInputQueue()
        WillCountToRead = 10 + 7



        write(bArr, 11)
        WaitForData()
        ret = GetAndProcessData()
        If (ret.Length > 5) Then
            If (ret.Substring(0, 6) = "Ошибка") Then

                ret = ret & " "
                ret = ret + "Настройки прибора не прочитаны"
                ret = ret & " "
                EraseInputQueue()
                dr = dt.NewRow
                dr("Название") = "Ошибка: "
                dr("Значение") = ret
                dt.Rows.Add(dr)
                Return dt
            End If
        End If

        If ret.Length = 0 Then

            ret = ret & " "
            ret = ret + "Настройки прибора не прочитаны"
            ret = ret & " "
            EraseInputQueue()
            dr = dt.NewRow
            dr("Название") = "Ошибка: "
            dr("Значение") = ret
            dt.Rows.Add(dr)
            Return dt

        End If


        dr = dt.NewRow
        dr("Название") = "Версия ПО"
        dr("Значение") = BCD((Mid(Param, 1, 1)))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Тип схемы подключения"
        dr("Значение") = Asc(Mid(Param, 1 + 1, 1))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Код объекта"
        dr("Значение") = BCD3(Mid(Param, 1 + 2, 3))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "Отчетный день"
        dr("Значение") = Asc(Mid(Param, 1 + 5, 1))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "дог. P1"
        dr("Значение") = Asc(Mid(Param, 1 + 6, 1))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "дог. P2"
        dr("Значение") = Asc(Mid(Param, 1 + 7, 1))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "дог. P3"
        dr("Значение") = Asc(Mid(Param, 1 + 8, 1))
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Название") = "дог. P4"
        dr("Значение") = Asc(Mid(Param, 1 + 9, 1))
        dt.Rows.Add(dr)


        Return dt
    End Function

    Private Function BCD(ByVal B As Byte) As UInteger
        Dim i As UInteger
        Dim o As UInteger
        i = B
        If (i Mod 16) > 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 100)
        ElseIf (i Mod 16) <= 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 10)
        End If

        Return o And &HFF
    End Function

    Private Function BCD(ByVal B As String) As String
        Dim i As UInteger
        Dim o As UInteger
        i = Asc(Mid(B, 1, 1))
        If (i Mod 16) > 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 100)
        ElseIf (i Mod 16) <= 9 Then
            o = ((i Mod 16)) + ((i \ 16) * 10)
        End If

        Return o.ToString

    End Function

    Private Function BCD3(ByVal B As String) As String


        Return BCD(Mid(B, 3, 1)) & BCD(Mid(B, 2, 1)) & BCD(Mid(B, 1, 1))
    End Function

    Public Overrides Function WriteTArchToDB() As String
        Dim sOUt As String = ""
        sOUt = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t3,t4,t5,t6,p1,p2,p3,p4,p5,p6,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,q1,q2,q3,q4,q5,q6,TSUM1,TSUM2,errtime,errtime2,oktime,oktime2,hc_code,hc,hc_1,hc_2) values ("
        sOUt = sOUt + DeviceID.ToString() + ","
        sOUt = sOUt + "SYSDATE" + ","
        sOUt = sOUt + OracleDate(tArch.DateArch) + ","
        sOUt = sOUt + OracleDate(tArch.DateArch) + ","
        sOUt = sOUt + tArch.archType.ToString() + ","
        sOUt = sOUt + NanFormat(tArch.T1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.T6, "##############0.000000").Replace(",", ".") + ","

        sOUt = sOUt + NanFormat(tArch.P1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.P6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.V6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.M6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.Q6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.ERRTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.ERRTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.OKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.OKTIME2, "##############0.000000").Replace(",", ".") + ","



        If DeCodeHCNumber(tArch.HCtv1, 1) = "" And DeCodeHCNumber(tArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(tArch.HCtv1, 1) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(tArch.HCtv2, 2) + "','" + S180("К2:" + DeCodeHCText(tArch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(tArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(tArch.HCtv1, 1) + "','" + S180("К1:" + DeCodeHCText(tArch.HCtv1)) + "',"
        Else
            sOUt = sOUt + "'" + S180(DeCodeHCNumber(tArch.HCtv1, 1) + DeCodeHCNumber(tArch.HCtv2, 2)) + "','" + S180("К1:" + DeCodeHCText(tArch.HCtv1) + " К2:" + DeCodeHCText(tArch.HCtv2)) + "',"
        End If

        'sOUt = sOUt + "'" + DeCodeHCNumber(tArch.HCtv1, 1) + ";" + DeCodeHCNumber(tArch.HCtv2, 2) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(tArch.HCtv1)) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(tArch.HCtv2)) + "'"
        sOUt = sOUt + ")"
        Return sOUt
    End Function


    Public Overrides Function WriteArchToDB() As String
        Dim sOUt As String = ""
        sOUt = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,SP_TB1,SP_TB2,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,p5,p6,v1,v2,v3,v4,v5,v6,v1H,v2H,v4H,v5H,m1,m2,m3,m4,m5,m6,q1,q2,q3,q4,q5,q6,q1H,q2H,TSUM1,TSUM2,errtime,errtime2,oktime,oktime2,hc_code,hc,hc_1,hc_2) values ("
        sOUt = sOUt + DeviceID.ToString() + ","
        sOUt = sOUt + "SYSDATE" + ","
        sOUt = sOUt + OracleDate(Arch.DateArch) + ","
        sOUt = sOUt + OracleDate(Arch.DateArch) + ","
        sOUt = sOUt + Arch.archType.ToString() + ","
        sOUt = sOUt + Arch.SPtv1.ToString + ","
        sOUt = sOUt + Arch.SPtv2.ToString + ","
        sOUt = sOUt + NanFormat(Arch.T1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.T6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tx1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tx2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tair1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.tair2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.P6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V1H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V2H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v4H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.v5H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.M6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q1H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q2H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.ERRTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.ERRTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.OKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.OKTIME2, "##############0.000000").Replace(",", ".") + ","



        If DeCodeHCNumber(Arch.HCtv1, 1) = "" And DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(Arch.HCtv1, 1) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(Arch.HCtv2, 2) + "','" + S180("K2:" + DeCodeHCText(Arch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(Arch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + "','" + S180("К1:" + DeCodeHCText(Arch.HCtv1)) + "',"
        Else
            sOUt = sOUt + "'" + S180(DeCodeHCNumber(Arch.HCtv1, 1) + DeCodeHCNumber(Arch.HCtv2, 2)) + "','" + S180("К1:" + DeCodeHCText(Arch.HCtv1) + " К2: " + DeCodeHCText(Arch.HCtv2)) + "',"
        End If

        'sOUt = sOUt + "'" + DeCodeHCNumber(Arch.HCtv1, 1) + ";" + DeCodeHCNumber(Arch.HCtv2, 2) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(Arch.HCtv1)) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(Arch.HCtv2)) + "'"
        sOUt = sOUt + ")"
        Return sOUt
    End Function



    Public Overrides Function WriteMArchToDB() As String
        Dim sOUt As String = ""
        sOUt = "INSERT INTO " & DBTableName & "(id_bd,DCALL,DCOUNTER,DATECOUNTER,id_ptype,t1,t2,t3,t4,t5,t6,tce1,tce2,tair1,tair2,p1,p2,p3,p4,p5,p6,g1,g2,g3,g4,g5,g6,v1,v2,v3,v4,v5,v6,m1,m2,m3,m4,m5,m6,q1,q2,q3,q4,q5,q6,TSUM1,TSUM2,errtime,errtime2,oktime,oktime2,hc_code,hc,hc_1,hc_2) values ("
        sOUt = sOUt + DeviceID.ToString() + ","
        sOUt = sOUt + "SYSDATE" + ","
        sOUt = sOUt + OracleDate(mArch.DateArch) + ","
        sOUt = sOUt + OracleDate(mArch.DateArch) + ","
        sOUt = sOUt + mArch.archType.ToString() + ","
        sOUt = sOUt + NanFormat(mArch.t1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.t6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tx1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tx2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tair1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.tair2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.p6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.G6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.V6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.M6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q3, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.Q6, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.ERRTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.ERRTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.OKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(mArch.OKTIME2, "##############0.000000").Replace(",", ".") + ","



        If DeCodeHCNumber(mArch.HCtv1, 1) = "" And DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'-','Нет НС',"
        ElseIf DeCodeHCNumber(mArch.HCtv1, 1) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(mArch.HCtv2, 2) + "','" + S180("К2:" + DeCodeHCText(mArch.HCtv2)) + "',"
        ElseIf DeCodeHCNumber(mArch.HCtv2, 2) = "" Then
            sOUt = sOUt + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + "','" + S180("К1:" + DeCodeHCText(mArch.HCtv1)) + "',"
        Else
            sOUt = sOUt + "'" + S180(DeCodeHCNumber(mArch.HCtv1, 1) + DeCodeHCNumber(mArch.HCtv2, 2)) + "','" + S180("К1:" + DeCodeHCText(mArch.HCtv1) + " К2:" + DeCodeHCText(mArch.HCtv2)) + "',"
        End If

        'sOUt = sOUt + "'" + DeCodeHCNumber(mArch.HCtv1, 1) + ";" + DeCodeHCNumber(mArch.HCtv2, 2) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(mArch.HCtv1)) + "',"
        sOUt = sOUt + "'" + S180(DeCodeHCText(mArch.HCtv2)) + "'"
        sOUt = sOUt + ")"
        Return sOUt

    End Function


    Public Function DeCodeHCNumber(ByVal CodeHC As Long, Optional inputnumber As Integer = 0) As String
        If CodeHC = 0 Then Return ""
        Return CodeHC.ToString()

    End Function
    Public Function DeCodeHCText(ByVal CodeHC As Long) As String
        If CodeHC = 0 Then Return ""
        Return CodeHC.ToString()

    End Function
    Public Function DeCodeHC(ByVal CodeHC As Long) As String
        If CodeHC = 0 Then Return ""
        Return CodeHC.ToString()

    End Function

    Protected Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() +
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
    End Function

    Protected Function S180(ByVal s As String) As String

        Dim outs As String
        outs = s
        If outs.Length <= 180 Then
            Return outs
        End If
        outs = outs.Substring(0, 180)
        Return outs
    End Function





    Public Overrides Sub EraseInputQueue()

        bufferindex = 0
        System.Threading.Thread.Sleep(150)
        MyTransport.CleanPort()
    End Sub



End Class
