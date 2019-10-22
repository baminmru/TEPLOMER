Imports STKTVMain
Imports System.IO
Imports System.Threading



Public Class driver
    Inherits STKTVMain.TVDriver

    Private Enum TegM4
        teg_OCTET_STRING = &H4 'Строка октетов
        teg_NULL = &H5 'Нет данных
        teg_ASCIIString = &H16 'Строка ASCII-символов
        teg_SEQUENCE = &H30 'Последовательность
        teg_IntU = &H41 'Беззнаковое целое (unsigned int)
        teg_IntS = &H42 'Целое со знаком (int)
        teg_IEEFloat = &H43 'Число с плавающей точкой IEEE 754 Float
        teg_MIXED = &H44 'Параметр с комбинированным значением int+float
        teg_Operative = &H45 'Оперативный параметр настроечной БД
        teg_ACK = &H46 'Подтверждение
        teg_TIME = &H47 'Текущее время
        teg_DATE = &H48 'Текущая календарная дата
        teg_ARCHDATE = &H49 'Дата архивной записи
        teg_PNUM = &H4A 'Номер параметра
        teg_FLAGS = &H4B 'Сборка флагов
        teg_ERR = &H55 'Ошибка
    End Enum

    Private Enum cmdM4
        cmd_ERROR = &H21 'Ошибка
        cmd_CONNECT = &H3F 'Запрос сеанса связи
        cmd_CHANGE_SPEED = &H42 'Запрос изменения скорости обмена
        cmd_COUNT_CONTROL = &H4F 'Запрос управления счетом
        cmd_ARCHIV = &H61 'Запрос поиска архивной записи
        cmd_PARAM = &H72 'Запрос чтения параметра
        cmd_WRITEPARAM = &H77 'Запрос записи параметра
    End Enum


    Private Class blockM4
        Public teg As TegM4
        Public dl As Integer
        Public data(1) As Byte
        Public Sub New(ByVal _teg As TegM4, _dl As Integer)
            teg = _teg
            dl = _dl
            ReDim data(dl)
        End Sub
    End Class

    Private Class messageM4
        Public ID As Byte
        Public cmd As cmdM4
        Public size As Integer
        Public Tegs As List(Of blockM4)

        Public Sub New(_cmd As Integer, _size As Integer)
            cmd = _cmd
            size = _size
            Tegs = New List(Of blockM4)
        End Sub

        Public Function BuildMessage(ByVal Id As Byte) As Byte()
            Dim bArr(4096) As Byte
            Dim res() As Byte
            Dim sz As Integer
            Dim pos As Integer
            Dim i As Integer
            Dim crc As UShort
            bArr(0) = &H10 ' not in crc !
            bArr(1) = &HFF
            bArr(2) = &H90
            bArr(3) = Id
            bArr(4) = &H0

            ' data size
            bArr(5) = &H0
            bArr(6) = &H0

            'data
            bArr(7) = cmd
            sz = 1
            pos = 7
            For Each b As blockM4 In Tegs
                pos += 1
                sz += 1
                bArr(pos) = b.teg
                pos += 1
                sz += 1
                bArr(pos) = b.dl 
                For i = 0 To b.dl - 1
                    pos += 1
                    sz += 1
                    bArr(pos) = b.data(i)
                Next
            Next

            bArr(5) = sz Mod 256
            bArr(6) = sz \ 256


            'crc
            crc = M4CRC(bArr, 1, pos)
            bArr(pos + 1) = crc \ 256
            bArr(pos + 2) = crc Mod 256
            ReDim res(0 To pos + 2)
            For i = 0 To pos + 2
                res(i) = bArr(i)
            Next
            Return res
        End Function

    End Class

    Private Function ParseM4Sequence(buf() As Byte, sz As Integer) As List(Of blockM4)

        Dim Tegs As List(Of blockM4)
        Dim ok As Boolean = True
        Dim pos As Integer
        Dim blen As Integer
        Dim block As blockM4
        blen = buf.Length - 1

        Tegs = New List(Of blockM4)()

        pos = 0
        Dim t As TegM4
        Dim dl As Integer
        Dim i As Integer
        Dim q As Integer
        While pos < sz
            t = CType(buf(pos), TegM4)
            pos += 1

            If (buf(pos) And &H80) = &H80 Then
                q = (buf(pos) And &H7F)
                dl = 0
                pos += 1

                If q = 1 Then
                    dl = buf(pos)
                End If
                If q = 2 Then
                    dl = buf(pos) * 256 + buf(pos + 1)
                End If
                If q = 3 Then
                    dl = buf(pos) * 256 * 256 + buf(pos + 1) * 256 + buf(pos + 2)
                End If

                pos += q
            Else
                dl = buf(pos)
                pos = pos + 1
            End If


            If pos + dl < sz Then
                block = New blockM4(t, dl)
                'pos = pos + 2
                For i = 0 To dl - 1
                    block.data(i) = buf(pos + i)
                Next
                pos = pos + dl
                Tegs.Add(block)
            Else
                Exit While
            End If

        End While
        Debug.Print("Tegs Found: " + Tegs.Count.ToString())
        Return Tegs

    End Function

    Private Function CheckHeader(buf() As Byte) As Boolean
        If buf(0) <> &H10 Then Return False
        If buf(1) <> &HFF Then Return False
        If buf(2) <> &H90 Then Return False


        If buf(4) <> &H0 Then Return vbFalse
        Return True

        
    End Function

    Private Function ParseM4Message(buf() As Byte) As messageM4

        Dim msg As messageM4
        Dim sz As Integer
        Dim ID As Byte
        Dim ok As Boolean = True
        Dim cmd As cmdM4
        'Dim crc As UShort
        Dim pos As Integer
        Dim blen As Integer
        Dim block As blockM4
        blen = buf.Length

        If blen < 9 Then Return Nothing
        If buf(0) <> &H10 Then Return Nothing

        If buf(1) <> &HFF Then Return Nothing
        If buf(2) <> &H90 Then Return Nothing

        ID = buf(3)
        If buf(4) <> &H0 Then Return Nothing

        ' data size
        sz = buf(6) * 256 + buf(5)


        'command
        cmd = CType(buf(7), cmdM4)


        
        'crc
        'crc = M4CRC(buf, 1, buf.Length - 3)
        'If buf(buf.Length - 2) <> crc \ 256 Then Return Nothing
        'If buf(buf.Length - 1) <> crc Mod 256 Then Return Nothing

        msg = New messageM4(cmd, sz)
        msg.ID = ID
        If cmd = cmdM4.cmd_CONNECT Or cmd = cmdM4.cmd_ERROR Then
            Return msg
        End If


        pos = 8
        Dim t As TegM4
        Dim dl As Integer
        Dim q As Integer
        Dim i As Integer
        While pos < sz + 7

            t = CType(buf(pos), TegM4)
            pos = pos + 1
            If (buf(pos) And &H80) = &H80 Then
                q = (buf(pos) And &H7F)
                dl = 0
                pos += 1

                If q = 1 Then
                    dl = buf(pos)
                End If
                If q = 2 Then
                    dl = buf(pos) * 256 + buf(pos + 1)
                End If

                If q = 3 Then
                    dl = buf(pos) * 256 * 256 + buf(pos + 1) * 256 + buf(pos + 2)
                End If

                pos += q
            Else
                dl = buf(pos)
                pos = pos + 1
            End If

            If pos + dl < blen + 2 Then
                block = New blockM4(t, dl)

                For i = 0 To dl - 1
                    block.data(i) = buf(pos + i)
                Next
                pos = pos + dl
                msg.Tegs.Add(block)
            End If

        End While

        Return msg

    End Function

    Private Function GetDeviceDate() As Date
        Dim d As Date
        Dim barr() As Byte
        Dim inbuf(1024) As Byte
        Dim msg As messageM4
        Dim block As blockM4
        Dim i As Integer
        Dim mID As Byte
        d = DateTime.Now
        msg = New messageM4(cmdM4.cmd_PARAM, 0)
        block = New blockM4(TegM4.teg_PNUM, 3)
        block.data(0) = 0  ' chanel=0
        block.data(1) = 1025 Mod 256
        block.data(2) = 1025 \ 256

        msg.Tegs.Add(block)

        block = New blockM4(TegM4.teg_PNUM, 3)
        block.data(0) = 0 ' chanel=0
        block.data(1) = 1024 Mod 256
        block.data(2) = 1024 \ 256

        msg.Tegs.Add(block)

        mID = NextID()

        barr = msg.BuildMessage(mID)



        write(barr, barr.Length)


        WaitForData()



        i = MyRead(inbuf, 0, 22, 200)

        If CheckCRC16(inbuf, 1, i - 3) Then

            msg = ParseM4Message(inbuf)
            If msg.cmd = cmdM4.cmd_PARAM Then 'And msg.ID = mID Then
                block = msg.Tegs(0)
                d = DateSerial(block.data(2) + 2000, block.data(1), block.data(0))
                block = msg.Tegs(1)
                d = d.AddTicks(TimeSerial(block.data(3), block.data(2), block.data(1)).Ticks)
            End If

            If msg.cmd = cmdM4.cmd_ERROR Then
                Debug.Print("Error at GetDeviceDate")
            End If

        End If

        Return d

    End Function

    Private mIsConnected As Boolean

    Private PacketID As Byte = 0

    Private Function NextID() As Byte
        PacketID += 1
        If PacketID = 255 Then
            PacketID = 1
        End If
        Return PacketID
    End Function




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

    Dim tArch As TArchive
    Dim IsTArchToRead As Boolean = False
    ' Dim WithEvents tim As System.Timers.Timer

    Dim tv As Short

    Public Const archType_moment As Integer = 1
    Public Const archType_total As Integer = 2
    Public Const archType_hour As Integer = 3
    Public Const archType_day As Integer = 4


    Dim Arch As Archive
    Dim mArch As MArchive

    Dim WillCountToRead As Short = 0
    Dim IsBytesToRead As Boolean = False
    Dim pagesToRead As Short = 0
    Dim curtime As DateTime
    Dim IsmArchToRead As Boolean = False
    Dim ispackageError As Boolean = False

    Dim buffer(0 To 32000) As Byte
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
        Return "SPT944"
    End Function

    Private m_serverip As String



    Private Sub Set9600()
        Dim bArr(0 To 20) As Byte
        Dim crc As UShort
        Dim i As Integer

        '10 FF 90 00 00 05 00 42 03 00 00 00 7E 39

        bArr(0) = &H10 ' not in crc !
        bArr(1) = &HFF
        bArr(2) = &H90
        bArr(3) = NextID()
        bArr(4) = &H0

        ' data size
        bArr(5) = &H5
        bArr(6) = &H0

        'data
        bArr(7) = &H42
        bArr(8) = &H2
        bArr(9) = &H0
        bArr(10) = &H0
        bArr(11) = &H0

        'crc
        crc = M4CRC(bArr, 1, 4 + 2 + 5)
        bArr(12) = crc \ 256
        bArr(13) = crc Mod 256

        EraseInputQueue()
        WillCountToRead = 13
        IsBytesToRead = True

        write(bArr, 14)



        WaitForData()



        i = MyRead(bArr, 0, 10, 200)


    End Sub

    Private Function TryConnect() As Boolean
        EraseInputQueue()
        Dim inbuf(64) As Byte
        Dim startBytes(0 To 30) As Byte
        Dim i As Int16




        For i = 0 To 20
            startBytes(i) = &HFF
        Next


        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(16))
        System.Threading.Thread.Sleep(1000)

        Dim bArr(0 To 20) As Byte
        Try

            bArr(0) = &H10
            bArr(1) = &HFF
            bArr(2) = &H3F
            bArr(3) = &H0
            bArr(4) = &H0
            bArr(5) = &H0
            bArr(6) = &H0
            'bArr(7) = &HC1
            bArr(7) = 255 - ((Int(bArr(1)) + Int(bArr(2)) + Int(bArr(3)) + Int(bArr(4)) + Int(bArr(5)) + Int(bArr(6))) Mod 256)
            bArr(8) = &H16
            EraseInputQueue()
            WillCountToRead = 8
            IsBytesToRead = True

            write(bArr, 9)
            tv = 1
        Catch exc As Exception
        End Try

        WaitForData()
        i = MyRead(inbuf, 0, 8, 200)
        If i = 8 Then
            If CheckCRC8(inbuf, 1, 5) = False Then
                Return False
            End If
        Else
            If i = 0 Then
                DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
            End If

            Return False

        End If


        For i = 0 To 20
            startBytes(i) = &HFF
        Next


        write(startBytes, 16)
        System.Threading.Thread.Sleep(CalcInterval(16))
        System.Threading.Thread.Sleep(1000)
        Dim crc As UShort

        '10 FF 90 00 00 05 00 3F 00 00 00 00 D9 19 
        Try
            ' header
            bArr(0) = &H10 ' not in crc !
            bArr(1) = &HFF
            bArr(2) = &H90
            bArr(3) = NextID()
            bArr(4) = &H0

            ' data size
            bArr(5) = &H5
            bArr(6) = &H0

            'data
            bArr(7) = &H3F
            bArr(8) = &H0
            bArr(9) = &H0
            bArr(10) = &H0
            bArr(11) = &H0

            'crc
            crc = M4CRC(bArr, 1, 4 + 2 + 5)
            bArr(12) = crc \ 256
            bArr(13) = crc Mod 256

            EraseInputQueue()
            WillCountToRead = 13
            IsBytesToRead = True

            write(bArr, 14)

            tv = 0



            WaitForData()



            i = MyRead(inbuf, 0, 13, 200)
            If i = 13 Then
                If CheckCRC16(inbuf, 1, 10) = False Then
                    Return False
                End If
            Else
                If i = 0 Then
                    DriverTransport.SendEvent(UnitransportAction.LowLevelStop, "Данные не получены")
                End If

                Return False

            End If


            mIsConnected = True
            Return True



        Catch exc As Exception
            Return False
        End Try

    End Function


    Private m_readRAMByteCount As Short

    Public Overrides Function ReadArch(ByVal ArchType As Short, ByVal ArchYear As Short,
    ByVal ArchMonth As Short, ByVal ArchDay As Short, ByVal ArchHour As Short) As String
        Dim mID As Byte
        Dim barr() As Byte
        Dim inbuf(1024) As Byte
        Dim msg As messageM4
        Dim block As blockM4
        Dim i As Integer
        Dim ok As Boolean = False
        Dim Seq As List(Of blockM4)
        Dim dok As Boolean
        Dim sz As Integer
        Dim trycnt As Integer

        cleararchive(Arch)

        Dim d As Date

        d = GetDeviceDate()


        ''''''''''''''''''  chanel_0 '''''''''''''''''''''''''
        msg = New messageM4(cmdM4.cmd_ARCHIV, 0)

        block = New blockM4(TegM4.teg_OCTET_STRING, 5)
        block.data(0) = &HFF
        block.data(1) = &HFF
        block.data(2) = 0 ' chanel
        If (ArchType = archType_hour) Then
            block.data(3) = 0
        Else
            block.data(3) = 1
        End If

        block.data(4) = 1

        msg.Tegs.Add(block)


        block = New blockM4(TegM4.teg_ARCHDATE, 8)


        If (ArchType = archType_hour) Then
            block.data(0) = ArchYear - 2000
            block.data(1) = ArchMonth Mod 13
            block.data(2) = ArchDay Mod 32
            block.data(3) = ArchHour Mod 24
            block.data(4) = 0
            block.data(5) = 0
            block.data(6) = 0
            block.data(7) = 0
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
            Arch.DateArch = Arch.DateArch.AddMilliseconds(-1000)

            Arch.archType = archType_hour

            If Arch.DateArch > d Then
                isArchToDBWrite = False
                Return "Ошибка даты архива"

            End If
        End If

        If (ArchType = archType_day) Then
            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
            Arch.DateArch = Arch.DateArch.AddMilliseconds(-1000)
            block.data(0) = ArchYear - 2000
            block.data(1) = ArchMonth Mod 13
            block.data(2) = ArchDay Mod 32
            block.data(3) = 0
            block.data(4) = 0
            block.data(5) = 0
            block.data(6) = 0
            block.data(7) = 0

            Arch.archType = archType_day

            If Arch.DateArch > d Then
                isArchToDBWrite = False
                Return "Ошибка даты архива"

            End If
        End If

        msg.Tegs.Add(block)


        trycnt = 5
tv0_get:
        trycnt -= 1
        mID = NextID()

        barr = msg.BuildMessage(mID)

        EraseInputQueue()

        write(barr, barr.Length)
        WaitForData()


        i = MyRead(inbuf, 0, 7, 1000)

        If i = 7 Then
            If CheckHeader(inbuf) Then
                sz = inbuf(5) + inbuf(6) * 256
                i = MyRead(inbuf, 7, sz + 2, 3000)
            Else
                EraseInputQueue()
                i = 0
            End If
        End If



        dok = False
        If i > 0 Then
            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
                msg = ParseM4Message(inbuf)
                If msg.cmd = cmdM4.cmd_ARCHIV Then 'msg.ID = mID And
                    block = msg.Tegs(0)
                    If block.teg = TegM4.teg_ARCHDATE And block.dl >= 3 Then
                        If (ArchType = archType_hour) Then
                            If block.data(0) = ArchYear - 2000 And block.data(1) = ArchMonth And block.data(2) = ArchDay And block.data(3) = ArchHour Then
                                dok = True
                            End If
                        Else
                            If block.data(0) = ArchYear - 2000 And block.data(1) = ArchMonth And block.data(2) = ArchDay Then
                                dok = True
                            End If
                        End If

                    End If
                    block = msg.Tegs(1)
                    If block.teg = TegM4.teg_SEQUENCE And dok Then
                        Seq = ParseM4Sequence(block.data, block.dl)
                        If Seq.Count >= 71 Then
                            Try


                                block = Seq(12)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.Q1H = BToSingle(block.data, 0)
                                block = Seq(13)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.WORKTIME1 = BToSingle(block.data, 0)
                                block = Seq(14)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.ERRTIME1 = BToSingle(block.data, 0)
                                block = Seq(15)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.OKTIME1 = BToSingle(block.data, 0)
                                block = Seq(36)
                                If block.teg = TegM4.teg_FLAGS Then Arch.HC = block.data(1) * 256 + block.data(0)
                                Arch.MsgHC = DeCodeHC(Arch.HC)


                                '''''''''''''' TV1
                                block = Seq(38 + 1)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.T1 = BToSingle(block.data, 0)
                                block = Seq(38 + 2)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.T2 = BToSingle(block.data, 0)
                                block = Seq(38 + 4)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.T3 = BToSingle(block.data, 0)

                                block = Seq(38 + 6)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.P1 = BToSingle(block.data, 0)
                                block = Seq(38 + 7)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.P2 = BToSingle(block.data, 0)
                                block = Seq(38 + 8)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.P3 = BToSingle(block.data, 0)



                                block = Seq(38 + 9)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.V1 = BToSingle(block.data, 0)
                                block = Seq(38 + 10)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.V2 = BToSingle(block.data, 0)
                                block = Seq(38 + 11)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.V3 = BToSingle(block.data, 0)

                                block = Seq(38 + 12)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.M1 = BToSingle(block.data, 0)
                                block = Seq(38 + 13)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.M2 = BToSingle(block.data, 0)
                                block = Seq(38 + 14)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.M3 = BToSingle(block.data, 0)

                                block = Seq(38 + 15)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.Q1 = BToSingle(block.data, 0)
                                block = Seq(38 + 16)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.Q2 = BToSingle(block.data, 0)

                                ''''''''''''''''''''' TV2
                                block = Seq(55 + 1)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.T4 = BToSingle(block.data, 0)
                                block = Seq(55 + 2)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.T5 = BToSingle(block.data, 0)
                                block = Seq(55 + 4)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.T6 = BToSingle(block.data, 0)

                                block = Seq(55 + 6)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.P4 = BToSingle(block.data, 0)
                                block = Seq(55 + 7)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.P5 = BToSingle(block.data, 0)
                                block = Seq(55 + 8)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.P6 = BToSingle(block.data, 0)



                                block = Seq(55 + 9)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.v4 = BToSingle(block.data, 0)
                                block = Seq(55 + 10)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.v5 = BToSingle(block.data, 0)
                                block = Seq(55 + 11)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.v6 = BToSingle(block.data, 0)

                                block = Seq(55 + 12)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.M4 = BToSingle(block.data, 0)
                                block = Seq(55 + 13)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.M5 = BToSingle(block.data, 0)
                                block = Seq(55 + 14)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.M6 = BToSingle(block.data, 0)

                                block = Seq(55 + 15)
                                If block.teg = TegM4.teg_IEEFloat Then Arch.Q4 = BToSingle(block.data, 0)
                                If Seq.Count > 71 Then
                                    block = Seq(55 + 16)
                                    If block.teg = TegM4.teg_IEEFloat Then Arch.Q5 = BToSingle(block.data, 0)
                                End If


                            Catch ex As Exception
                                Debug.Print(ex.Message)
                            End Try

                        End If

                        ok = True
                        GoTo tv1_arch
                    End If


                End If
            End If
        End If
        If trycnt > 0 Then GoTo tv0_get

        EraseInputQueue()


tv1_arch:
        '' 18.10.2017 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '        ''''''''''''''''''  chanel 1 '''''''''''''''''''''''''
        '        msg = New messageM4(cmdM4.cmd_ARCHIV, 0)

        '        block = New blockM4(TegM4.teg_OCTET_STRING, 5)
        '        block.data(0) = &HFF
        '        block.data(1) = &HFF
        '        block.data(2) = 1 ' chanel 1
        '        If (ArchType = archType_hour) Then
        '            block.data(3) = 0
        '        Else
        '            block.data(3) = 1
        '        End If

        '        block.data(4) = 1

        '        msg.Tegs.Add(block)


        '        block = New blockM4(TegM4.teg_ARCHDATE, 8)


        '        If (ArchType = archType_hour) Then
        '            block.data(0) = ArchYear - 2000
        '            block.data(1) = ArchMonth Mod 13
        '            block.data(2) = ArchDay Mod 32
        '            block.data(3) = ArchHour Mod 24
        '            block.data(4) = 0
        '            block.data(5) = 0
        '            block.data(6) = 0
        '            block.data(7) = 0
        '            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
        '            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)

        '            Arch.archType = archType_hour

        '            If Arch.DateArch > d Then
        '                isArchToDBWrite = False
        '                Return "Ошибка даты архива"

        '            End If
        '        End If

        '        If (ArchType = archType_day) Then
        '            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
        '            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)
        '            block.data(0) = ArchYear - 2000
        '            block.data(1) = ArchMonth Mod 13
        '            block.data(2) = ArchDay Mod 32
        '            block.data(3) = 0
        '            block.data(4) = 0
        '            block.data(5) = 0
        '            block.data(6) = 0
        '            block.data(7) = 0

        '            Arch.archType = archType_day

        '            If Arch.DateArch > d Then
        '                isArchToDBWrite = False
        '                Return "Ошибка даты архива"

        '            End If
        '        End If

        '        msg.Tegs.Add(block)

        '        trycnt = 5
        'tv1_get:
        '        trycnt -= 1
        '        mID = NextID()

        '        barr = msg.BuildMessage(mID)

        '        EraseInputQueue()

        '        write(barr, barr.Length)
        '        WaitForData()


        '        i = MyRead(inbuf, 0, 7, 1000)

        '        If i = 7 Then
        '            If CheckHeader(inbuf) Then
        '                sz = inbuf(5) + inbuf(6) * 256
        '                i = MyRead(inbuf, 7, sz + 2, 3000)
        '            Else
        '                EraseInputQueue()
        '                i = 0
        '            End If
        '        End If



        '        dok = False
        '        If i > 0 Then
        '            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
        '                msg = ParseM4Message(inbuf)
        '                If msg.cmd = cmdM4.cmd_ARCHIV Then 'msg.ID = mID And
        '                    block = msg.Tegs(0)
        '                    If block.teg = TegM4.teg_ARCHDATE And block.dl >= 3 Then
        '                        If (ArchType = archType_hour) Then
        '                            If block.data(0) = ArchYear - 2000 And block.data(1) = ArchMonth And block.data(2) = ArchDay And block.data(3) = ArchHour Then
        '                                dok = True
        '                            End If
        '                        Else
        '                            If block.data(0) = ArchYear - 2000 And block.data(1) = ArchMonth And block.data(2) = ArchDay Then
        '                                dok = True
        '                            End If
        '                        End If

        '                    End If
        '                    block = msg.Tegs(1)
        '                    If block.teg = TegM4.teg_SEQUENCE And dok Then
        '                        Seq = ParseM4Sequence(block.data, block.dl)
        '                        If Seq.Count >= 17 Then
        '                            block = Seq(1)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.T1 = BToSingle(block.data, 0)
        '                            block = Seq(2)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.T2 = BToSingle(block.data, 0)
        '                            block = Seq(4)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.T3 = BToSingle(block.data, 0)

        '                            block = Seq(6)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.P1 = BToSingle(block.data, 0)
        '                            block = Seq(7)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.P2 = BToSingle(block.data, 0)
        '                            block = Seq(8)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.P3 = BToSingle(block.data, 0)



        '                            block = Seq(9)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.V1 = BToSingle(block.data, 0)
        '                            block = Seq(10)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.V2 = BToSingle(block.data, 0)
        '                            block = Seq(11)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.V3 = BToSingle(block.data, 0)

        '                            block = Seq(12)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.M1 = BToSingle(block.data, 0)
        '                            block = Seq(13)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.M2 = BToSingle(block.data, 0)
        '                            block = Seq(14)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.M3 = BToSingle(block.data, 0)

        '                            block = Seq(15)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.Q1 = BToSingle(block.data, 0)
        '                            block = Seq(16)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.Q2 = BToSingle(block.data, 0)

        '                        End If

        '                        ok = True
        '                        GoTo tv2_arch
        '                    End If


        '                End If
        '            End If
        '        End If
        '        If trycnt > 0 Then GoTo tv1_get

        '        EraseInputQueue()

        '        ok = False
        '        GoTo arch_final

        'tv2_arch:
        '        '''''''''''''''''''''''' chanel 2 ''''''''''''''''''

        '        msg = New messageM4(cmdM4.cmd_ARCHIV, 0)

        '        block = New blockM4(TegM4.teg_OCTET_STRING, 5)
        '        block.data(0) = &HFF
        '        block.data(1) = &HFF
        '        block.data(2) = 2
        '        If (ArchType = archType_hour) Then
        '            block.data(3) = 0
        '        Else
        '            block.data(3) = 1
        '        End If

        '        block.data(4) = 1

        '        msg.Tegs.Add(block)


        '        block = New blockM4(TegM4.teg_ARCHDATE, 8)


        '        If (ArchType = archType_hour) Then

        '            block.data(0) = ArchYear - 2000
        '            block.data(1) = ArchMonth Mod 13
        '            block.data(2) = ArchDay Mod 32
        '            block.data(3) = ArchHour Mod 24
        '            block.data(4) = 0
        '            block.data(5) = 0
        '            block.data(6) = 0
        '            block.data(7) = 0
        '            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, ArchHour, 0, 0)
        '            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)
        '            Arch.archType = archType_hour
        '        End If

        '        If (ArchType = archType_day) Then
        '            block.data(0) = ArchYear - 2000
        '            block.data(1) = ArchMonth Mod 13
        '            block.data(2) = ArchDay Mod 32
        '            block.data(3) = 0
        '            block.data(4) = 0
        '            block.data(5) = 0
        '            block.data(6) = 0
        '            block.data(7) = 0
        '            Arch.DateArch = New DateTime(ArchYear, ArchMonth, ArchDay, 0, 0, 0)
        '            Arch.DateArch = Arch.DateArch.AddMilliseconds(Me.AddMS)
        '            Arch.archType = archType_day
        '        End If

        '        msg.Tegs.Add(block)

        '        trycnt = 5
        'tv2_get:

        '        trycnt -= 1
        '        mID = NextID()

        '        barr = msg.BuildMessage(mID)

        '        EraseInputQueue()

        '        write(barr, barr.Length)
        '        WaitForData()


        '        i = MyRead(inbuf, 0, 7, 1000)

        '        If i = 7 Then
        '            If CheckHeader(inbuf) Then
        '                sz = inbuf(5) + inbuf(6) * 256
        '                i = MyRead(inbuf, 7, sz + 2, 3000)
        '            Else
        '                EraseInputQueue()
        '                i = 0
        '            End If
        '        End If




        '        dok = False
        '        If i > 0 Then
        '            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
        '                msg = ParseM4Message(inbuf)
        '                If msg.cmd = cmdM4.cmd_ARCHIV Then 'msg.ID = mID And
        '                    block = msg.Tegs(0)
        '                    If block.teg = TegM4.teg_ARCHDATE And block.dl >= 3 Then
        '                        If (ArchType = archType_hour) Then
        '                            If block.data(0) = ArchYear - 2000 And block.data(1) = ArchMonth And block.data(2) = ArchDay And block.data(3) = ArchHour Then
        '                                dok = True
        '                            End If
        '                        Else
        '                            If block.data(0) = ArchYear - 2000 And block.data(1) = ArchMonth And block.data(2) = ArchDay Then
        '                                dok = True
        '                            End If
        '                        End If
        '                    End If
        '                    block = msg.Tegs(1)
        '                    If block.teg = TegM4.teg_SEQUENCE And dok Then
        '                        Seq = ParseM4Sequence(block.data, block.dl)
        '                        If Seq.Count >= 17 Then
        '                            block = Seq(1)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.T4 = BToSingle(block.data, 0)
        '                            block = Seq(2)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.T5 = BToSingle(block.data, 0)
        '                            block = Seq(4)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.T6 = BToSingle(block.data, 0)

        '                            block = Seq(6)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.P4 = BToSingle(block.data, 0)
        '                            block = Seq(7)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.P5 = BToSingle(block.data, 0)
        '                            block = Seq(8)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.P6 = BToSingle(block.data, 0)



        '                            block = Seq(9)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.v4 = BToSingle(block.data, 0)
        '                            block = Seq(10)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.v5 = BToSingle(block.data, 0)
        '                            block = Seq(11)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.v6 = BToSingle(block.data, 0)

        '                            block = Seq(12)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.M4 = BToSingle(block.data, 0)
        '                            block = Seq(13)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.M5 = BToSingle(block.data, 0)
        '                            block = Seq(14)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.M6 = BToSingle(block.data, 0)

        '                            block = Seq(15)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.Q4 = BToSingle(block.data, 0)
        '                            block = Seq(16)
        '                            If block.teg = TegM4.teg_IEEFloat Then Arch.Q5 = BToSingle(block.data, 0)
        '                        End If
        '                        ok = True
        '                        GoTo arch_final
        '                    End If


        '                End If
        '            End If
        '        End If
        '        If trycnt > 0 Then
        '            GoTo tv2_get
        '        End If
        '' 18.10.2017 ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
arch_final:
        EraseInputQueue()
        If ok = False Then


            isArchToDBWrite = False
            Return "Ошибка чтения архива"

        Else
            isArchToDBWrite = True
            Return "Архива прочитан"
        End If


    End Function


    Private Function CheckCRC8(ByVal buf() As Byte, ByVal offset As Integer, ByVal len As Integer) As Boolean
        Dim KC As Long, i As Integer
        KC = 0
        For i = offset To offset + len - 1
            KC = KC + Int(buf(i))
        Next
        KC = 255 - (KC Mod 256)
        If KC = buf(offset + len) Then
            Return True
        Else
            Return False
        End If
    End Function


    Private Function CheckCRC16(ByVal buf() As Byte, ByVal offset As Integer, ByVal len As Integer) As Boolean
        Dim crc As UShort
        If len <=0 Then Return False
        crc = M4CRC(buf, offset, len)
        If (buf(offset + len) = crc \ 256) And (buf(offset + len + 1) = crc Mod 256) Then
            Return True
        Else
            Return False
        End If
    End Function



    Public Function DeCodeHCNumber(ByVal CodeHC As Long, Optional tv As Integer = 0) As String

        DeCodeHCNumber = ""

        If CodeHC And 2 ^ 0 Then
            DeCodeHCNumber = "TB" + tv.ToString + ":НС00" & ";"
        End If

        If CodeHC And 2 ^ 1 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС01" & ";"
        End If

        If CodeHC And 2 ^ 2 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС02" + ";"
        End If
        If CodeHC And 2 ^ 3 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС03" + ";"
        End If
        If CodeHC And 2 ^ 4 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС04" + ";"
        End If
        If CodeHC And 2 ^ 5 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС05" + ";"
        End If
        If CodeHC And 2 ^ 6 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС06" + ";"
        End If
        If CodeHC And 2 ^ 7 Then
            DeCodeHCNumber = DeCodeHCNumber + "TB" + tv.ToString + ":НС07" + ";"
        End If



        If CodeHC And 2 ^ 8 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС08" & ";"
        End If

        If CodeHC And 2 ^ 9 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС09" & ";"
        End If

        If CodeHC And 2 ^ 10 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС10" & ";"
        End If

        If CodeHC And 2 ^ 11 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС11 " & ";"
        End If

        If CodeHC And 2 ^ 12 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС12" & ";"
        End If

        If CodeHC And 2 ^ 13 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС13" & ";"
        End If

        If CodeHC And 2 ^ 14 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС14" & ";"
        End If

        If CodeHC And 2 ^ 15 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС15" & ";"
        End If

        If CodeHC And 2 ^ 16 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС16" & ";"
        End If

        If CodeHC And 2 ^ 17 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС17 " & ";"
        End If

        If CodeHC And 2 ^ 18 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС18" & ";"
        End If

        If CodeHC And 2 ^ 19 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС19" & ";"
        End If
        If CodeHC And 2 ^ 20 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС20" & ";"
        End If
        If CodeHC And 2 ^ 21 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС21" & ";"
        End If
        If CodeHC And 2 ^ 22 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС22" & ";"
        End If
        If CodeHC And 2 ^ 23 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС23" & ";"
        End If

        If CodeHC And 2 ^ 24 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС24" & ";"
        End If
        If CodeHC And 2 ^ 25 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС25" & ";"
        End If
        If CodeHC And 2 ^ 26 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС26" & ";"
        End If
        If CodeHC And 2 ^ 27 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС27" & ";"
        End If
        If CodeHC And 2 ^ 28 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС28" & ";"
        End If
        If CodeHC And 2 ^ 29 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС29" & ";"
        End If
        If CodeHC And 2 ^ 30 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС30" & ";"
        End If
        If CodeHC And 2 ^ 31 Then
            DeCodeHCNumber = DeCodeHCNumber _
                    + "TB" + tv.ToString() + ":НС31" & ";"
        End If

    End Function

    Private Function AddERR(ByVal Code As Long, ByVal msg As String, ByVal varname As String, ByVal checkcode As Long) As String
        If Code And checkcode Then
            Return msg
        End If
        Return ""
    End Function

    Private Function AddERR2(ByVal Code As Long, ByVal msg As String, ByVal varname As String, ByVal checkcode As Long) As String
        If Code And checkcode Then
            Return varname
        End If
        Return ""
    End Function


    Public Function DeCodeHCText(ByVal Code As Long) As String

        DeCodeHCText = ""
        DeCodeHCText += AddERR(Code, "Разряд батареи (Uб < 3,2 В). Следует в течение месяца заменить батарею", "BatteryLow", 1)
        DeCodeHCText += AddERR(Code, "Отсутствие напряжения на разъеме X1 тепловычислителя", "NoVoltage", 2)
        DeCodeHCText += AddERR(Code, "Перегрузка по цепям питания датчиков расхода", "OverSupply", 4)
        DeCodeHCText += AddERR(Code, "Изменение сигнала на порту D1 (разъем X4)", "D1ChangeSignal", 8)
        DeCodeHCText += AddERR(Code, "Изменение сигнала на порту D2 (разъем X6)", "D2ChangeSignal", 16)
        DeCodeHCText += AddERR(Code, "Изменение сигнала на порту D3 (разъем X5)", "D3ChangeSignal", 32)
        DeCodeHCText += AddERR(Code, "Изменение сигнала на порту D4 (разъем X7)", "D4ChangeSignal", 64)
        DeCodeHCText += AddERR(Code, "Датчик ТС1 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS1OutOfRange", 128)
        DeCodeHCText += AddERR(Code, "Датчик ТС2 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS2OutOfRange", 256)
        DeCodeHCText += AddERR(Code, "Датчик ТС3 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS3OutOfRange", 512)
        DeCodeHCText += AddERR(Code, "Датчик ТС4 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS4OutOfRange", 1024)
        DeCodeHCText += AddERR(Code, "Датчик ТС5 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS5OutOfRange", 2048)
        DeCodeHCText += AddERR(Code, "Датчик ТС6 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS6OutOfRange", 4096)
        DeCodeHCText += AddERR(Code, "Датчик ПД1 вне диапазона 0…1,03ВП1", "PD1OutOfRange", 8192)
        DeCodeHCText += AddERR(Code, "Датчик ПД2 вне диапазона 0…1,03ВП2", "PD2OutOfRange", 16384)
        DeCodeHCText += AddERR(Code, "Датчик ПД3 вне диапазона 0…1,03ВП3", "PD3OutOfRange", 32768)
        DeCodeHCText += AddERR(Code, "Датчик ПД4 вне диапазона 0…1,03ВП4", "PD4OutOfRange", 65536)
        DeCodeHCText += AddERR(Code, "Датчик ПД5 вне диапазона 0…1,03ВП5", "PD5OutOfRange", 131072)
        DeCodeHCText += AddERR(Code, "Датчик ПД6 вне диапазона 0…1,03ВП6", "PD6OutOfRange", 262144)
        DeCodeHCText += AddERR(Code, "Расход через ВС1 выше верхнего предела Gв1", "VS1OverRun", 524288)
        DeCodeHCText += AddERR(Code, "Расход через ВС1 ниже нижнего предела Gн1", "VS1UnderRun", 1048576)
        DeCodeHCText += AddERR(Code, "Расход через ВС1 ниже отсечки самохода Gотс1", "VS1UnderCutOff", 2097152)
        DeCodeHCText += AddERR(Code, "Расход через ВС2 выше верхнего предела Gв2", "VS2OverRun", 4194304)
        DeCodeHCText += AddERR(Code, "Расход через ВС2 ниже нижнего предела Gн2", "VS2UnderRun", 8388608)
        DeCodeHCText += AddERR(Code, "Расход через ВС2 ниже отсечки самохода Gотс2", "VS2UnderCutOff", 16777216)
        DeCodeHCText += AddERR(Code, "Расход через ВС3 выше верхнего предела Gв3", "VS3OverRun", 33554432)
        DeCodeHCText += AddERR(Code, "Расход через ВС3 ниже нижнего предела Gн3", "VS3UnderRun", 67108864)
        DeCodeHCText += AddERR(Code, "Расход через ВС3 ниже отсечки самохода Gотс3", "VS3UnderCutOff", 134217728)
        DeCodeHCText += AddERR(Code, "Расход через ВС4 выше верхнего предела Gв4", "VS4OverRun", 268435456)
        DeCodeHCText += AddERR(Code, "Расход через ВС4 ниже нижнего предела Gн4", "VS4UnderRun", 536870912)
        DeCodeHCText += AddERR(Code, "Расход через ВС4 ниже отсечки самохода Gотс4", "VS4UnderCutOff", 1073741824)
        DeCodeHCText += AddERR(Code, "Расход через ВС5 выше верхнего предела Gв5", "VS5OverRun", 2147483648)
        DeCodeHCText += AddERR(Code, "Расход через ВС5 ниже нижнего предела Gн5", "VS5UnderRun", 4294967296)
        DeCodeHCText += AddERR(Code, "Расход через ВС5 ниже отсечки самохода Gотс5", "VS5UnderCutOff", 8589934592)
        DeCodeHCText += AddERR(Code, "Расход через ВС6 выше верхнего предела Gв6", "VS6OverRun", 17179869184)
        DeCodeHCText += AddERR(Code, "Расход через ВС6 ниже нижнего предела Gн6", "VS6UnderRun", 34359738368)
        DeCodeHCText += AddERR(Code, "Расход через ВС6 ниже отсечки самохода Gотс6", "VS6UnderCutOff", 68719476736)
        DeCodeHCText += AddERR(Code, "Значение контролируемого параметра, определяемого КУ1 вне диапазона УН1…УВ1", "KU1OutOfRange", 137438953472)
        DeCodeHCText += AddERR(Code, "Значение контролируемого параметра, определяемого КУ2 вне диапазона УН2…УВ2", "KU2OutOfRange", 274877906944)
        DeCodeHCText += AddERR(Code, "Значение контролируемого параметра, определяемого КУ3 вне диапазона УН3…УВ3", "KU3OutOfRange", 549755813888)
        DeCodeHCText += AddERR(Code, "Значение контролируемого параметра, определяемого КУ4 вне диапазона УН4…УВ4", "KU4OutOfRange", 1099511627776)
        DeCodeHCText += AddERR(Code, "Значение контролируемого параметра, определяемого КУ5 вне диапазона УН5…УВ5", "KU5OutOfRange", 2199023255552)
        DeCodeHCText += AddERR(Code, "Ошибка описания температурного графика", "TempGraph", 4398046511104)
        DeCodeHCText += AddERR(Code, "Ошибка связи с сервером", "ServerConnection", 8796093022208)
        DeCodeHCText += AddERR(Code, "Используется альтернативная схема учета, назначенная параметром СА1", "AS1Used", 17592186044416)
        DeCodeHCText += AddERR(Code, "Используется альтернативная схема учета, назначенная параметром СА2", "AS2Used", 35184372088832)
    End Function
    Public Function DeCodeHC(ByVal Code As Long) As String

        DeCodeHC = ""
        DeCodeHC += AddERR2(Code, "Разряд батареи (Uб < 3,2 В). Следует в течение месяца заменить батарею", "BatteryLow", 1)
        DeCodeHC += AddERR2(Code, "Отсутствие напряжения на разъеме X1 тепловычислителя", "NoVoltage", 2)
        DeCodeHC += AddERR2(Code, "Перегрузка по цепям питания датчиков расхода", "OverSupply", 4)
        DeCodeHC += AddERR2(Code, "Изменение сигнала на порту D1 (разъем X4)", "D1ChangeSignal", 8)
        DeCodeHC += AddERR2(Code, "Изменение сигнала на порту D2 (разъем X6)", "D2ChangeSignal", 16)
        DeCodeHC += AddERR2(Code, "Изменение сигнала на порту D3 (разъем X5)", "D3ChangeSignal", 32)
        DeCodeHC += AddERR2(Code, "Изменение сигнала на порту D4 (разъем X7)", "D4ChangeSignal", 64)
        DeCodeHC += AddERR2(Code, "Датчик ТС1 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS1OutOfRange", 128)
        DeCodeHC += AddERR2(Code, "Датчик ТС2 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS2OutOfRange", 256)
        DeCodeHC += AddERR2(Code, "Датчик ТС3 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS3OutOfRange", 512)
        DeCodeHC += AddERR2(Code, "Датчик ТС4 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS4OutOfRange", 1024)
        DeCodeHC += AddERR2(Code, "Датчик ТС5 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS5OutOfRange", 2048)
        DeCodeHC += AddERR2(Code, "Датчик ТС6 вне диапазона 0…176 °C или -50…176°C (при измерении t4, t5, t6)", "TS6OutOfRange", 4096)
        DeCodeHC += AddERR2(Code, "Датчик ПД1 вне диапазона 0…1,03ВП1", "PD1OutOfRange", 8192)
        DeCodeHC += AddERR2(Code, "Датчик ПД2 вне диапазона 0…1,03ВП2", "PD2OutOfRange", 16384)
        DeCodeHC += AddERR2(Code, "Датчик ПД3 вне диапазона 0…1,03ВП3", "PD3OutOfRange", 32768)
        DeCodeHC += AddERR2(Code, "Датчик ПД4 вне диапазона 0…1,03ВП4", "PD4OutOfRange", 65536)
        DeCodeHC += AddERR2(Code, "Датчик ПД5 вне диапазона 0…1,03ВП5", "PD5OutOfRange", 131072)
        DeCodeHC += AddERR2(Code, "Датчик ПД6 вне диапазона 0…1,03ВП6", "PD6OutOfRange", 262144)
        DeCodeHC += AddERR2(Code, "Расход через ВС1 выше верхнего предела Gв1", "VS1OverRun", 524288)
        DeCodeHC += AddERR2(Code, "Расход через ВС1 ниже нижнего предела Gн1", "VS1UnderRun", 1048576)
        DeCodeHC += AddERR2(Code, "Расход через ВС1 ниже отсечки самохода Gотс1", "VS1UnderCutOff", 2097152)
        DeCodeHC += AddERR2(Code, "Расход через ВС2 выше верхнего предела Gв2", "VS2OverRun", 4194304)
        DeCodeHC += AddERR2(Code, "Расход через ВС2 ниже нижнего предела Gн2", "VS2UnderRun", 8388608)
        DeCodeHC += AddERR2(Code, "Расход через ВС2 ниже отсечки самохода Gотс2", "VS2UnderCutOff", 16777216)
        DeCodeHC += AddERR2(Code, "Расход через ВС3 выше верхнего предела Gв3", "VS3OverRun", 33554432)
        DeCodeHC += AddERR2(Code, "Расход через ВС3 ниже нижнего предела Gн3", "VS3UnderRun", 67108864)
        DeCodeHC += AddERR2(Code, "Расход через ВС3 ниже отсечки самохода Gотс3", "VS3UnderCutOff", 134217728)
        DeCodeHC += AddERR2(Code, "Расход через ВС4 выше верхнего предела Gв4", "VS4OverRun", 268435456)
        DeCodeHC += AddERR2(Code, "Расход через ВС4 ниже нижнего предела Gн4", "VS4UnderRun", 536870912)
        DeCodeHC += AddERR2(Code, "Расход через ВС4 ниже отсечки самохода Gотс4", "VS4UnderCutOff", 1073741824)
        DeCodeHC += AddERR2(Code, "Расход через ВС5 выше верхнего предела Gв5", "VS5OverRun", 2147483648)
        DeCodeHC += AddERR2(Code, "Расход через ВС5 ниже нижнего предела Gн5", "VS5UnderRun", 4294967296)
        DeCodeHC += AddERR2(Code, "Расход через ВС5 ниже отсечки самохода Gотс5", "VS5UnderCutOff", 8589934592)
        DeCodeHC += AddERR2(Code, "Расход через ВС6 выше верхнего предела Gв6", "VS6OverRun", 17179869184)
        DeCodeHC += AddERR2(Code, "Расход через ВС6 ниже нижнего предела Gн6", "VS6UnderRun", 34359738368)
        DeCodeHC += AddERR2(Code, "Расход через ВС6 ниже отсечки самохода Gотс6", "VS6UnderCutOff", 68719476736)
        DeCodeHC += AddERR2(Code, "Значение контролируемого параметра, определяемого КУ1 вне диапазона УН1…УВ1", "KU1OutOfRange", 137438953472)
        DeCodeHC += AddERR2(Code, "Значение контролируемого параметра, определяемого КУ2 вне диапазона УН2…УВ2", "KU2OutOfRange", 274877906944)
        DeCodeHC += AddERR2(Code, "Значение контролируемого параметра, определяемого КУ3 вне диапазона УН3…УВ3", "KU3OutOfRange", 549755813888)
        DeCodeHC += AddERR2(Code, "Значение контролируемого параметра, определяемого КУ4 вне диапазона УН4…УВ4", "KU4OutOfRange", 1099511627776)
        DeCodeHC += AddERR2(Code, "Значение контролируемого параметра, определяемого КУ5 вне диапазона УН5…УВ5", "KU5OutOfRange", 2199023255552)
        DeCodeHC += AddERR2(Code, "Ошибка описания температурного графика", "TempGraph", 4398046511104)
        DeCodeHC += AddERR2(Code, "Ошибка связи с сервером", "ServerConnection", 8796093022208)
        DeCodeHC += AddERR2(Code, "Используется альтернативная схема учета, назначенная параметром СА1", "AS1Used", 17592186044416)
        DeCodeHC += AddERR2(Code, "Используется альтернативная схема учета, назначенная параметром СА2", "AS2Used", 35184372088832)

    End Function


    Private Function GetLng(ByVal SI() As Byte, ByVal Pos As Integer) As Long

        Dim h As ULong
        h = 0
        Dim b1 As Integer, b2 As Integer, b3 As Integer, b0 As Integer
        Try
            b0 = SI(Pos + 3)
            b1 = SI(Pos + 2)
            b2 = SI(Pos + 1)
            b3 = SI(Pos + 0)
            h = (b0 << 24) + (b1 << 16) + (b2 << 8) + b3
        Catch ex As Exception

            h = 0
        End Try
        Return h
    End Function
    Private Function GetInt(ByVal SI() As Byte, ByVal Pos As Integer) As Integer
        Dim h As Integer
        Dim b1 As Integer, b0 As Integer
        b0 = SI(Pos)
        b1 = SI(Pos + 1)
        h = (b0 << 8) + b1
        Return h
    End Function

    Private Function BToSingle(ByVal hexValue() As Byte, ByVal index As Int16) As Single

        Try

            Dim iInputIndex As Integer = 0

            Dim iOutputIndex As Integer = 0

            Dim bArray(3) As Byte



            For iInputIndex = 0 To 3

                bArray(iOutputIndex) = hexValue(index + iInputIndex)

                iOutputIndex += 1

            Next
            'Array.Reverse(bArray)
            Return BitConverter.ToSingle(bArray, 0)

        Catch ex As Exception
            Return Single.NaN
        End Try
    End Function

    Public Function FloatExt(ByVal floatStr As String) As Single
        Dim tmpStr As String = ""
        Dim E As Long
        Dim Mantissa As Long
        Dim s As Long
        Dim f As Single
        Dim i As Long
        If floatStr = "" Then Exit Function
        If floatStr.Length <> 4 Then Exit Function
        ' If floatStr = String(4, 0) Then Exit Function
        If floatStr = Chr(0) + Chr(0) + Chr(0) + Chr(0) Then
            Return 0.0
        End If
        For i = 1 To 4
            tmpStr = Chr(Asc(Mid(floatStr, i, 1))) & tmpStr
        Next i


        floatStr = tmpStr
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
        Mantissa = ((Asc(Mid(floatStr, 2, 1)) And &H7F) << 16) _
                     + (Asc(Mid(floatStr, 3, 1)) << 8) _
                     + (Asc(Mid(floatStr, 4, 1)))

        'Mantissa = (Asc(Mid(floatStr, 2, 1)) And &H7F) * (2 ^ 16) _
        '                     + Asc(Mid(floatStr, 3, 1)) * (2 ^ 8) _
        '                     + Asc(Mid(floatStr, 4, 1))

        f = 2 ^ 0
        For i = 22 To 0 Step -1
            If Mantissa And 2& ^ i Then
                f = f + 2 ^ (i - 23)
            End If
        Next i
        FloatExt = ((-1) ^ s) * f * (2.0! ^ (E - 127))
    End Function
    Private Function OracleDate(ByVal d As Date) As String
        Return "to_date('" + d.Year.ToString() + "-" + d.Month.ToString() + "-" + d.Day.ToString() + _
            " " + d.Hour.ToString() + ":" + d.Minute.ToString() + ":" + d.Second.ToString() + "','YYYY-MM-DD HH24:MI:SS')"
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
        sOUt = sOUt + NanFormat(tArch.errtime1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.errtime2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.oktime1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(tArch.oktime2, "##############0.000000").Replace(",", ".") + ","



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
        sOUt = sOUt + NanFormat(Arch.V4, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V5, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.V6, "##############0.000000").Replace(",", ".") + ","
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
        sOUt = sOUt + NanFormat(Arch.Q1h, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.Q2H, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.WORKTIME1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.WORKTIME2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.errtime1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.errtime2, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.oktime1, "##############0.000000").Replace(",", ".") + ","
        sOUt = sOUt + NanFormat(Arch.oktime2, "##############0.000000").Replace(",", ".") + ","



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




    Public Overrides Sub EraseInputQueue()
        If (IsBytesToRead = True) Then
            IsBytesToRead = False
        End If
        bufferindex = 0

        MyTransport.CleanPort()
        System.Threading.Thread.Sleep(150)
        Dim buffer(256) As Byte
        Dim sz As Integer
        While MyTransport.BytesToRead
            sz = MyTransport.BytesToRead
            If sz > 256 Then sz = 256
            MyTransport.Read(buffer, 0, sz)
            System.Threading.Thread.Sleep(100)
        End While

    End Sub
    Private Sub cleararchive(ByRef arc As Archive)
        arc.DateArch = DateTime.MinValue

        arc.HC = 0
        arc.MsgHC = ""

        arc.HCtv1 = 0
        arc.MsgHC_1 = ""

        arc.HCtv2 = 0
        arc.MsgHC_2 = ""



        arc.P1 = 0
        arc.T1 = 0
        arc.M2 = 0
        arc.V1 = 0

        arc.P2 = 0
        arc.T2 = 0
        arc.M3 = 0
        arc.V2 = 0

        arc.V3 = 0
        arc.M1 = 0

        arc.Q1 = 0
        arc.Q2 = 0



        arc.SP = 0
        arc.SPtv1 = 0
        arc.SPtv2 = 0

        arc.tx1 = 0
        arc.tx2 = 0
        arc.tair1 = 0
        arc.tair2 = 0

        arc.T3 = 0
        arc.T4 = 0
        arc.T5 = 0
        arc.T6 = 0
        arc.P3 = 0
        arc.P4 = 0
        arc.v4 = 0
        arc.v5 = 0
        arc.v6 = 0
        arc.M4 = 0
        arc.M5 = 0
        arc.M6 = 0

        arc.archType = 0
        isArchToDBWrite = False
    End Sub
    Private Sub clearMarchive(ByRef marc As MArchive)
        marc.DateArch = DateTime.MinValue
        marc.HC = 0
        marc.MsgHC = ""

        marc.HCtv1 = 0
        marc.MsgHC_1 = ""

        marc.HCtv2 = 0
        marc.MsgHC_2 = ""

        marc.G1 = 0
        marc.G2 = 0
        marc.G3 = 0
        marc.G4 = 0
        marc.G5 = 0
        marc.G6 = 0

        marc.t1 = 0
        marc.t2 = 0
        marc.t3 = 0
        marc.t4 = 0
        marc.t5 = 0
        marc.t6 = 0

        marc.p1 = 0
        marc.p2 = 0
        marc.p3 = 0
        marc.p4 = 0

        marc.dt12 = 0
        marc.dt45 = 0

        marc.tx1 = 0
        marc.tx2 = 0

        marc.tair1 = 0
        marc.tair2 = 0

        marc.SP = 0
        marc.SPtv1 = 0
        marc.SPtv2 = 0


        marc.archType = 0
        isMArchToDBWrite = False
    End Sub
    Public Overrides Function ReadMArch() As String

        Dim d As Date
        d = GetDeviceDate()

        Dim mID As Byte
        Dim barr() As Byte
        Dim inbuf(1024) As Byte
        Dim msg As messageM4
        Dim block As blockM4
        Dim i As Integer
        Dim ok As Boolean = False
        Dim sz As Integer
        Dim trycnt As Integer

        clearMarchive(mArch)
        mArch.archType = 1
        mArch.DateArch = d

        ' common params
        msg = New messageM4(cmdM4.cmd_PARAM, 0)
        For i = 1036 To 1037
            block = New blockM4(TegM4.teg_PNUM, 3)
            block.data(0) = 0  ' chanel=1
            block.data(1) = i Mod 256
            block.data(2) = i \ 256
            msg.Tegs.Add(block)
        Next

        trycnt = 5
tv0_again:
        trycnt -= 1

        mID = NextID()

        barr = msg.BuildMessage(mID)

        EraseInputQueue()

        write(barr, barr.Length)
        WaitForData()

        i = MyRead(inbuf, 0, 7, 1000)

        If i = 7 Then
            If CheckHeader(inbuf) Then
                sz = inbuf(5) + inbuf(6) * 256
                i = MyRead(inbuf, 7, sz + 2, 3000)
            Else
                i = 0
                EraseInputQueue()
            End If
        End If

        If i > 0 Then
            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
                msg = ParseM4Message(inbuf)
                If msg.cmd = cmdM4.cmd_PARAM Then 'msg.ID = mID And 
                    block = msg.Tegs(0)
                    If block.teg = TegM4.teg_FLAGS Then mArch.HC = block.data(1) * 256 + block.data(0)
                    mArch.MsgHC = DeCodeHC(mArch.HC)
                    ok = True
                    GoTo tv1_march
                End If
            End If
        End If

        If trycnt > 0 Then GoTo tv0_again


tv1_march:

        msg = New messageM4(cmdM4.cmd_PARAM, 0)
        For i = 1025 To 1038
            block = New blockM4(TegM4.teg_PNUM, 3)
            block.data(0) = 1  ' chanel=1
            block.data(1) = i Mod 256
            block.data(2) = i \ 256
            msg.Tegs.Add(block)
        Next

        trycnt = 5

tv1_again:
        trycnt -= 1

        mID = NextID()

        barr = msg.BuildMessage(mID)

        EraseInputQueue()

        write(barr, barr.Length)
        WaitForData()


        i = MyRead(inbuf, 0, 7, 1000)

        If i = 7 Then
            If CheckHeader(inbuf) Then
                sz = inbuf(5) + inbuf(6) * 256
                i = MyRead(inbuf, 7, sz + 2, 3000)
            Else
                i = 0
                EraseInputQueue()
            End If
        End If

        If i > 0 Then
            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
                msg = ParseM4Message(inbuf)
                If msg.cmd = cmdM4.cmd_PARAM Then 'msg.ID = mID And 
                    block = msg.Tegs(0)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.G1 = BToSingle(block.data, 0)
                    block = msg.Tegs(1)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.G2 = BToSingle(block.data, 0)
                    block = msg.Tegs(2)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.G3 = BToSingle(block.data, 0)

                    block = msg.Tegs(3)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.M1 = BToSingle(block.data, 0)
                    block = msg.Tegs(4)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.M2 = BToSingle(block.data, 0)
                    block = msg.Tegs(5)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.M3 = BToSingle(block.data, 0)

                    block = msg.Tegs(6)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.t1 = BToSingle(block.data, 0)
                    block = msg.Tegs(7)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.t2 = BToSingle(block.data, 0)
                    block = msg.Tegs(9)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.t3 = BToSingle(block.data, 0)

                    block = msg.Tegs(11)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.p1 = BToSingle(block.data, 0)
                    block = msg.Tegs(12)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.p2 = BToSingle(block.data, 0)
                    block = msg.Tegs(13)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.p3 = BToSingle(block.data, 0)
                    ok = True
                    GoTo tv2_march
                End If
            End If
        End If

        If trycnt > 0 Then GoTo tv1_again


tv2_march:

        msg = New messageM4(cmdM4.cmd_PARAM, 0)
        For i = 1025 To 1038
            block = New blockM4(TegM4.teg_PNUM, 3)
            block.data(0) = 2  ' chanel=2
            block.data(1) = i Mod 256
            block.data(2) = i \ 256
            msg.Tegs.Add(block)
        Next

        trycnt = 5
tv2_again:
        trycnt -= 1

        mID = NextID()

        barr = msg.BuildMessage(mID)

        EraseInputQueue()
        write(barr, barr.Length)


        WaitForData()


        i = MyRead(inbuf, 0, 7, 1000)

        If i = 7 Then
            If CheckHeader(inbuf) Then
                sz = inbuf(5) + inbuf(6) * 256
                i = MyRead(inbuf, 7, sz + 2, 3000)
            Else
                i = 0
                EraseInputQueue()
            End If
        End If

        If i > 0 Then
            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
                If msg.cmd = cmdM4.cmd_PARAM Then 'msg.ID = mID And
                    msg = ParseM4Message(inbuf)
                    block = msg.Tegs(0)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.G4 = BToSingle(block.data, 0)
                    block = msg.Tegs(1)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.G5 = BToSingle(block.data, 0)
                    block = msg.Tegs(2)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.G6 = BToSingle(block.data, 0)

                    block = msg.Tegs(3)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.M4 = BToSingle(block.data, 0)
                    block = msg.Tegs(4)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.M5 = BToSingle(block.data, 0)
                    block = msg.Tegs(5)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.M6 = BToSingle(block.data, 0)

                    block = msg.Tegs(6)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.t4 = BToSingle(block.data, 0)
                    block = msg.Tegs(7)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.t5 = BToSingle(block.data, 0)
                    block = msg.Tegs(9)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.t6 = BToSingle(block.data, 0)

                    block = msg.Tegs(11)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.p4 = BToSingle(block.data, 0)
                    block = msg.Tegs(12)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.p5 = BToSingle(block.data, 0)
                    block = msg.Tegs(13)
                    If block.teg = TegM4.teg_IEEFloat Then mArch.p6 = BToSingle(block.data, 0)
                    ok = True
                    GoTo march_final
                End If

            End If
        End If
        If trycnt > 0 Then GoTo tv2_again

march_final:

        EraseInputQueue()
        If ok = False Then
            EraseInputQueue()
            isMArchToDBWrite = False
            Return "Ошибка чтения мгновенного архива "
        End If

        isMArchToDBWrite = True
        Return "Мгновенный архив прочитан"
    End Function




    Private Sub clearTarchive(ByRef marc As TArchive)
        marc.DateArch = DateTime.MinValue


        marc.V1 = 0
        marc.V2 = 0
        marc.V3 = 0
        marc.V4 = 0
        marc.V5 = 0
        marc.V6 = 0
        marc.M1 = 0
        marc.M2 = 0
        marc.M3 = 0
        marc.M4 = 0
        marc.M5 = 0
        marc.M6 = 0
        marc.Q1 = 0
        marc.Q2 = 0


        marc.archType = 2
        isTArchToDBWrite = False
    End Sub

    '        msg = New messageM4(cmdM4.cmd_PARAM, 0)
    '        For i = 2048 To 2055
    '            block = New blockM4(TegM4.teg_PNUM, 3)
    '            block.data(0) = 1  ' chanel=1
    '            block.data(1) = i Mod 256
    '            block.data(2) = i \ 256
    '            msg.Tegs.Add(block)
    '        Next
    '        block = New blockM4(TegM4.teg_PNUM, 3)
    '        block.data(0) = 1  ' chanel=1
    '        block.data(1) = 2062 Mod 256
    '        block.data(2) = 2062 \ 256
    '        msg.Tegs.Add(block)

    '        block = New blockM4(TegM4.teg_PNUM, 3)
    '        block.data(0) = 1  ' chanel=1
    '        block.data(1) = 2063 Mod 256
    '        block.data(2) = 2063 \ 256
    '        msg.Tegs.Add(block)

    '        block = New blockM4(TegM4.teg_PNUM, 3)
    '        block.data(0) = 1  ' chanel=1
    '        block.data(1) = 2056 Mod 256
    '        block.data(2) = 2056 \ 256
    '        msg.Tegs.Add(block)


    '        trycnt = 5

    'tv1_tagain:

    '        trycnt -= 1
    '        mID = NextID()

    '        barr = msg.BuildMessage(mID)

    '        EraseInputQueue()

    '        write(barr, barr.Length)
    '        WaitForData()



    '        i = MyRead(inbuf, 0, 7, 1000)

    '        If i = 7 Then
    '            If CheckHeader(inbuf) Then
    '                sz = inbuf(5) + inbuf(6) * 256
    '                i = MyRead(inbuf, 7, sz + 2, 3000)
    '            Else
    '                i = 0
    '                EraseInputQueue()
    '            End If
    '        End If

    '        If i > 0 Then
    '            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
    '                msg = ParseM4Message(inbuf)
    '                If msg.cmd = cmdM4.cmd_PARAM Then ' msg.ID = mID And
    '                    block = msg.Tegs(0)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.V1 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(1)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.V2 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(2)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.V3 = BToSingle(block.data, 4) + GetLng(block.data, 0)

    '                    block = msg.Tegs(3)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.M1 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(4)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.M2 = BToSingle(block.data, 4) + GetLng(block.data, 0)

    '                    block = msg.Tegs(5)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.M3 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(6)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.Q1 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(7)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.Q2 = BToSingle(block.data, 4) + GetLng(block.data, 0)


    '                    block = msg.Tegs(8)
    '                    If block.teg = TegM4.teg_IEEFloat Then tArch.ERRTIME1 = BToSingle(block.data, 0)
    '                    block = msg.Tegs(9)
    '                    If block.teg = TegM4.teg_IEEFloat Then tArch.WORKTIME1 = BToSingle(block.data, 0)
    '                    block = msg.Tegs(10)
    '                    If block.teg = TegM4.teg_IEEFloat Then tArch.WORKTIME1 = BToSingle(block.data, 0)

    '                    ok = True
    '                    GoTo tv2_tarch
    '                End If
    '            End If
    '        End If
    '        If trycnt > 0 Then
    '            GoTo tv1_tagain
    '        End If



    'tv2_tarch:
    '        msg = New messageM4(cmdM4.cmd_PARAM, 0)
    '        For i = 2048 To 2055
    '            block = New blockM4(TegM4.teg_PNUM, 3)
    '            block.data(0) = 2  ' chanel=1
    '            block.data(1) = i Mod 256
    '            block.data(2) = i \ 256
    '            msg.Tegs.Add(block)
    '        Next
    '        block = New blockM4(TegM4.teg_PNUM, 3)
    '        block.data(0) = 2  ' chanel=1
    '        block.data(1) = 2062 Mod 256
    '        block.data(2) = 2062 \ 256
    '        msg.Tegs.Add(block)

    '        block = New blockM4(TegM4.teg_PNUM, 3)
    '        block.data(0) = 2  ' chanel=1
    '        block.data(1) = 2063 Mod 256
    '        block.data(2) = 2063 \ 256
    '        msg.Tegs.Add(block)

    '        block = New blockM4(TegM4.teg_PNUM, 3)
    '        block.data(0) = 2  ' chanel=1
    '        block.data(1) = 2056 Mod 256
    '        block.data(2) = 2056 \ 256
    '        msg.Tegs.Add(block)

    '        trycnt = 5

    'tv2_again:
    '        trycnt -= 1
    '        mID = NextID()

    '        barr = msg.BuildMessage(mID)

    '        EraseInputQueue()
    '        write(barr, barr.Length)


    '        WaitForData()


    '        i = MyRead(inbuf, 0, 7, 1000)

    '        If i = 7 Then
    '            If CheckHeader(inbuf) Then
    '                sz = inbuf(5) + inbuf(6) * 256
    '                i = MyRead(inbuf, 7, sz + 2, 3000)
    '            Else
    '                i = 0
    '                EraseInputQueue()
    '            End If
    '        End If
    '        If i > 0 Then
    '            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
    '                If msg.cmd = cmdM4.cmd_PARAM Then
    '                    block = msg.Tegs(0)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.V4 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(1)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.V5 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(2)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.V6 = BToSingle(block.data, 4) + GetLng(block.data, 0)

    '                    block = msg.Tegs(3)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.M4 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(4)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.M5 = BToSingle(block.data, 4) + GetLng(block.data, 0)

    '                    block = msg.Tegs(5)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.M6 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(6)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.Q3 = BToSingle(block.data, 4) + GetLng(block.data, 0)
    '                    block = msg.Tegs(7)
    '                    If block.teg = TegM4.teg_MIXED Then tArch.Q4 = BToSingle(block.data, 4) + GetLng(block.data, 0)


    '                    block = msg.Tegs(8)
    '                    If block.teg = TegM4.teg_IEEFloat Then tArch.ERRTIME2 = BToSingle(block.data, 0)
    '                    block = msg.Tegs(9)
    '                    If block.teg = TegM4.teg_IEEFloat Then tArch.WORKTIME2 = BToSingle(block.data, 0)
    '                    block = msg.Tegs(10)
    '                    If block.teg = TegM4.teg_IEEFloat Then tArch.WORKTIME2 = BToSingle(block.data, 0)

    '                    ok = True
    '                    GoTo tarch_final
    '                End If

    '            End If
    '        End If
    '        If trycnt > 0 Then
    '            GoTo tv2_again
    '        End If

    'tarch_final:

    '        EraseInputQueue()
    '        If ok = False Then

    '            isTArchToDBWrite = False
    '            Return "Ошибка чтения итогового архива "
    '        End If

    '        isTArchToDBWrite = True
    '        Return "Итоговый архив прочитан"

    '    End Function
    Public Overrides Function ReadTArch() As String
        Dim d As Date
        d = GetDeviceDate()

        Dim mID As Byte
        Dim barr() As Byte
        Dim inbuf(1024) As Byte
        Dim msg As messageM4
        Dim block As blockM4
        Dim i As Integer
        Dim ok As Boolean = False
        Dim sz As Integer
        Dim trycnt As Integer

        clearTArchive(tArch)
        tArch.archType = 2
        tArch.DateArch = d



        ' common params
        msg = New messageM4(cmdM4.cmd_PARAM, 0)
        For i = 2049 To 2051
            block = New blockM4(TegM4.teg_PNUM, 3)
            block.data(0) = 0  ' chanel=1
            block.data(1) = i Mod 256
            block.data(2) = i \ 256
            msg.Tegs.Add(block)
        Next

        trycnt = 5

tv0_again:
        trycnt -= 1

        mID = NextID()

        barr = msg.BuildMessage(mID)

        EraseInputQueue()

        write(barr, barr.Length)
        WaitForData()

        i = MyRead(inbuf, 0, 7, 1000)

        If i = 7 Then
            If CheckHeader(inbuf) Then
                sz = inbuf(5) + inbuf(6) * 256
                i = MyRead(inbuf, 7, sz + 2, 3000)
            Else
                i = 0
                EraseInputQueue()
            End If
        End If

        If i > 0 Then
            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
                msg = ParseM4Message(inbuf)
                If msg.cmd = cmdM4.cmd_PARAM Then 'msg.ID = mID And 
                    block = msg.Tegs(0)
                    If block.teg = TegM4.teg_IEEFloat Then tArch.WORKTIME1 = BToSingle(block.data, 0)
                    block = msg.Tegs(1)
                    If block.teg = TegM4.teg_IEEFloat Then tArch.ERRTIME1 = BToSingle(block.data, 0)
                    block = msg.Tegs(2)
                    If block.teg = TegM4.teg_IEEFloat Then tArch.OKTIME1 = BToSingle(block.data, 0)
                    ok = True
                    GoTo tv1_tarch
                End If
            End If
        End If

        If trycnt > 0 Then GoTo tv0_again

tv1_tarch:

        msg = New messageM4(cmdM4.cmd_PARAM, 0)
        For i = 2048 To 2055
            block = New blockM4(TegM4.teg_PNUM, 3)
            block.data(0) = 1  ' chanel=1
            block.data(1) = i Mod 256
            block.data(2) = i \ 256
            msg.Tegs.Add(block)
        Next

        trycnt = 5

tv1_tagain:

        trycnt -= 1
        mID = NextID()

        barr = msg.BuildMessage(mID)

        EraseInputQueue()

        write(barr, barr.Length)
        WaitForData()



        i = MyRead(inbuf, 0, 7, 1000)

        If i = 7 Then
            If CheckHeader(inbuf) Then
                sz = inbuf(5) + inbuf(6) * 256
                i = MyRead(inbuf, 7, sz + 2, 3000)
            Else
                i = 0
                EraseInputQueue()
            End If
        End If

        If i > 0 Then
            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
                msg = ParseM4Message(inbuf)
                If msg.cmd = cmdM4.cmd_PARAM Then ' msg.ID = mID And
                    block = msg.Tegs(0)
                    If block.teg = TegM4.teg_MIXED Then tArch.V1 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(1)
                    If block.teg = TegM4.teg_MIXED Then tArch.V2 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(2)
                    If block.teg = TegM4.teg_MIXED Then tArch.V3 = BToSingle(block.data, 4) + GetLng(block.data, 0)

                    block = msg.Tegs(3)
                    If block.teg = TegM4.teg_MIXED Then tArch.M1 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(4)
                    If block.teg = TegM4.teg_MIXED Then tArch.M2 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(5)
                    If block.teg = TegM4.teg_MIXED Then tArch.M3 = BToSingle(block.data, 4) + GetLng(block.data, 0)

                    block = msg.Tegs(6)
                    If block.teg = TegM4.teg_MIXED Then tArch.Q1 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(7)
                    If block.teg = TegM4.teg_MIXED Then tArch.Q2 = BToSingle(block.data, 4) + GetLng(block.data, 0)

                    ok = True
                    GoTo tv2_tarch
                End If
            End If
        End If
        If trycnt > 0 Then
            GoTo tv1_tagain
        End If



tv2_tarch:
        msg = New messageM4(cmdM4.cmd_PARAM, 0)
        For i = 2048 To 2055
            block = New blockM4(TegM4.teg_PNUM, 3)
            block.data(0) = 2  ' chanel=1
            block.data(1) = i Mod 256
            block.data(2) = i \ 256
            msg.Tegs.Add(block)
        Next




        trycnt = 5

tv2_again:
        trycnt -= 1
        mID = NextID()

        barr = msg.BuildMessage(mID)

        EraseInputQueue()
        write(barr, barr.Length)


        WaitForData()


        i = MyRead(inbuf, 0, 7, 1000)

        If i = 7 Then
            If CheckHeader(inbuf) Then
                sz = inbuf(5) + inbuf(6) * 256
                i = MyRead(inbuf, 7, sz + 2, 3000)
            Else
                i = 0
                EraseInputQueue()
            End If
        End If
        If i > 0 Then
            If CheckCRC16(inbuf, 1, i + 7 - 3) Then
                msg = ParseM4Message(inbuf)
                If msg.cmd = cmdM4.cmd_PARAM Then
                    block = msg.Tegs(0)
                    If block.teg = TegM4.teg_MIXED Then tArch.V4 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(1)
                    If block.teg = TegM4.teg_MIXED Then tArch.V5 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(2)
                    If block.teg = TegM4.teg_MIXED Then tArch.V6 = BToSingle(block.data, 4) + GetLng(block.data, 0)

                    block = msg.Tegs(3)
                    If block.teg = TegM4.teg_MIXED Then tArch.M4 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(4)
                    If block.teg = TegM4.teg_MIXED Then tArch.M5 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(5)
                    If block.teg = TegM4.teg_MIXED Then tArch.M6 = BToSingle(block.data, 4) + GetLng(block.data, 0)

                    block = msg.Tegs(6)
                    If block.teg = TegM4.teg_MIXED Then tArch.Q3 = BToSingle(block.data, 4) + GetLng(block.data, 0)
                    block = msg.Tegs(7)
                    If block.teg = TegM4.teg_MIXED Then tArch.Q4 = BToSingle(block.data, 4) + GetLng(block.data, 0)

                    ok = True
                    GoTo tarch_final
                End If

            End If
        End If
        If trycnt > 0 Then
            GoTo tv2_again
        End If

tarch_final:

        EraseInputQueue()
        If ok = False Then

            isTArchToDBWrite = False
            Return "Ошибка чтения итогового архива "
        End If

        isTArchToDBWrite = True
        Return "Итоговый архив прочитан"

    End Function



    Private Function ExtLong4(ByVal extStr As String) As Double
        Dim i As Long
        On Error Resume Next
        ExtLong4 = 0
        For i = 0 To 3
            ExtLong4 = ExtLong4 + Asc(Mid(extStr, 1 + i, 1)) * (256 ^ (i))
        Next i
    End Function



    Public Overrides Function IsConnected() As Boolean
        If MyTransport Is Nothing Then Return False
        Return mIsConnected And MyTransport.IsConnected
    End Function
    Private Function S180(ByVal s As String) As String

        Dim outs As String
        outs = s
        If outs.Length <= 180 Then
            Return outs
        End If
        outs = outs.Substring(0, 180)
        Return outs
    End Function

    Private Sub AddCN(cn As Dictionary(Of Integer, String), key As Integer, Name As String)
        Try
            cn.Add(key, Name)
        Catch ex As Exception

        End Try

    End Sub

    Public Overrides Function ReadSystemParameters() As System.Data.DataTable

        TryConnect()
        EraseInputQueue()
        Dim dt As DataTable
        Dim dr As DataRow
        Dim cn As Dictionary(Of Integer, String)
        Dim sn As Dictionary(Of Integer, String)



        cn = New Dictionary(Of Integer, String)
        sn = New Dictionary(Of Integer, String)

        dt = New DataTable
        dt.Columns.Add("Название")
        dt.Columns.Add("Значение")

        Dim d As Date
        d = GetDeviceDate()

        Dim mID As Byte
        Dim barr() As Byte
        Dim inbuf(1024) As Byte
        Dim msg As messageM4
        Dim block As blockM4
        Dim i As Integer
        Dim ok As Boolean = False
        Dim s As String
        Dim j As Integer

        AddCN(cn, 0, "СП")
        AddCN(cn, 1, "СA1")
        AddCN(cn, 2, "АСА1")
        AddCN(cn, 3, "CA2")
        AddCN(cn, 4, "АСА2")
        AddCN(cn, 5, "ЕИ/P")
        AddCN(cn, 6, "ЕИ/Q")
        AddCN(cn, 7, "ТО")
        AddCN(cn, 8, "ДО")
        AddCN(cn, 9, "PКЧ")
        AddCN(cn, 10, "СР")
        AddCN(cn, 11, "ЧР")
        AddCN(cn, 12, "ПЛ")
        AddCN(cn, 13, "tхк")
        AddCN(cn, 14, "Pxк")
        AddCN(cn, 15, "ТС")
        AddCN(cn, 16, "ТС1")
        AddCN(cn, 17, "ТС2")
        AddCN(cn, 18, "ТС3")
        AddCN(cn, 19, "ТС4")
        AddCN(cn, 20, "ТС5")
        AddCN(cn, 21, "ТС6")
        AddCN(cn, 22, "ПД1")
        AddCN(cn, 23, "ВП1")
        AddCN(cn, 24, "ПД2")
        AddCN(cn, 25, "ВП2")
        AddCN(cn, 26, "ПД3")
        AddCN(cn, 27, "ВП3")
        AddCN(cn, 28, "ПД4")
        AddCN(cn, 29, "ВП4")
        AddCN(cn, 30, "ПД5")
        AddCN(cn, 31, "ВП5")
        AddCN(cn, 32, "ПД6")
        AddCN(cn, 33, "ВП6")
        AddCN(cn, 34, "С1")
        AddCN(cn, 35, "Gв1")
        AddCN(cn, 36, "Gн1")
        AddCN(cn, 37, "Gотс1")
        AddCN(cn, 38, "С2")
        AddCN(cn, 39, "Gв2")
        AddCN(cn, 40, "Gн2")
        AddCN(cn, 41, "Gотс2")
        AddCN(cn, 42, "С3")
        AddCN(cn, 43, "Gв3")
        AddCN(cn, 44, "Gн3")
        AddCN(cn, 45, "Gотс3")
        AddCN(cn, 46, "С4")
        AddCN(cn, 47, "Gв4")
        AddCN(cn, 48, "Gн4")
        AddCN(cn, 49, "Gотс4")
        AddCN(cn, 50, "С5")
        AddCN(cn, 51, "Gв5")
        AddCN(cn, 52, "Gн5")
        AddCN(cn, 53, "Gотс5")
        AddCN(cn, 54, "С6")
        AddCN(cn, 55, "Gв6")
        AddCN(cn, 56, "Gн6")
        AddCN(cn, 57, "Gотс6")
        AddCN(cn, 58, "NT")
        AddCN(cn, 59, "ИД")
        AddCN(cn, 60, "КИ1")
        AddCN(cn, 61, "КИ2")
        AddCN(cn, 62, "КИ3")
        AddCN(cn, 63, "КД1")
        AddCN(cn, 64, "КД2")
        AddCN(cn, 65, "КД3")
        AddCN(cn, 66, "КД4")
        AddCN(cn, 67, "AКД1")
        AddCN(cn, 68, "AКД2")
        AddCN(cn, 69, "АНС")
        AddCN(cn, 70, "АСТ1")
        AddCN(cn, 71, "АСТ2")
        AddCN(cn, 72, "АСТ3")
        AddCN(cn, 73, "АСТ4")
        AddCN(cn, 74, "АСТ5")
        AddCN(cn, 75, "АСТ6")
        AddCN(cn, 76, "АСТ7")
        AddCN(cn, 77, "АСТ8")
        AddCN(cn, 78, "АСТ9")
        AddCN(cn, 79, "АСТ10")
        AddCN(cn, 80, "АСТ11")
        AddCN(cn, 81, "АСТ12")
        AddCN(cn, 82, "АСТ13")
        AddCN(cn, 83, "АСТ14")
        AddCN(cn, 84, "АСТ15")
        AddCN(cn, 85, "АСТ16")
        AddCN(cn, 85, "АСТ17")
        AddCN(cn, 87, "АСТ18")
        AddCN(cn, 88, "АСТ19")
        AddCN(cn, 89, "АСТ20")
        AddCN(cn, 90, "КТГ")
        AddCN(cn, 91, "tп1")
        AddCN(cn, 92, "tп2")
        AddCN(cn, 93, "tп3")
        AddCN(cn, 94, "tп4")
        AddCN(cn, 95, "tп5")
        AddCN(cn, 96, "tо1")
        AddCN(cn, 97, "tо2")
        AddCN(cn, 98, "tо3")
        AddCN(cn, 99, "tо4")
        AddCN(cn, 100, "tо5")
        AddCN(cn, 101, "КУ1")
        AddCN(cn, 102, "УВ1")
        AddCN(cn, 103, "УН1")
        AddCN(cn, 104, "КУ2")
        AddCN(cn, 105, "УВ2")
        AddCN(cn, 106, "УН2")
        AddCN(cn, 107, "КУ3")
        AddCN(cn, 108, "УВ3")
        AddCN(cn, 109, "УН3")
        AddCN(cn, 110, "КУ4")
        AddCN(cn, 111, "УВ4")
        AddCN(cn, 112, "УН4")
        AddCN(cn, 113, "КУ5")
        AddCN(cn, 114, "УВ5")
        AddCN(cn, 115, "УН5")
        AddCN(cn, 116, "AQC")
        AddCN(cn, 117, "КВС")
        AddCN(cn, 150, "PLG")
        AddCN(cn, 151, "PPW")
        AddCN(cn, 152, "AT1")
        AddCN(cn, 153, "ОТВ1")
        AddCN(cn, 154, "AT2")
        AddCN(cn, 155, "ОТВ2")
        AddCN(cn, 156, "AT3")
        AddCN(cn, 157, "ОТВ3")
        AddCN(cn, 158, "AT4")
        AddCN(cn, 159, "ОТВ4")
        AddCN(cn, 160, "AT5")
        AddCN(cn, 161, "ОТВ5")
        AddCN(cn, 162, "IP")
        AddCN(cn, 163, "PORT")
        AddCN(cn, 164, "SLG")
        AddCN(cn, 165, "SPW")
        AddCN(cn, 166, "Tka")



        AddCN(sn, 0, "ДВ")
        AddCN(sn, 1, "tк1")
        AddCN(sn, 2, "tк2")
        AddCN(sn, 3, "tк3")
        AddCN(sn, 4, "Pк1")
        AddCN(sn, 5, "Pк2")
        AddCN(sn, 6, "Pк3")
        AddCN(sn, 7, "Gкв1")
        AddCN(sn, 8, "Gкн1")
        AddCN(sn, 9, "AGв1")
        AddCN(sn, 10, "AGн1")
        AddCN(sn, 11, "Gкв2")
        AddCN(sn, 12, "Gкн2")
        AddCN(sn, 13, "AGв2")
        AddCN(sn, 14, "AGн2")
        AddCN(sn, 15, "Gкв3")
        AddCN(sn, 16, "Gкн3")
        AddCN(sn, 17, "AGв3")
        AddCN(sn, 18, "AGн3")
        AddCN(sn, 19, "НМ")
        AddCN(sn, 20, "Mк")
        AddCN(sn, 21, "АМк")
        AddCN(sn, 22, "АrV")
        AddCN(sn, 23, "Qк")
        AddCN(sn, 24, "АQк")
        AddCN(sn, 25, "Уdt")
        AddCN(sn, 26, "ПС")
        AddCN(sn, 27, "ПМ")
        AddCN(sn, 50, "XG1")
        AddCN(sn, 51, "XG2")
        AddCN(sn, 52, "XG3")
        AddCN(sn, 53, "Xt1")
        AddCN(sn, 54, "Xt2")
        AddCN(sn, 55, "Xt3")
        AddCN(sn, 56, "XP1")
        AddCN(sn, 57, "XP2")
        AddCN(sn, 58, "XP3")
        AddCN(sn, 59, "АV3")
        AddCN(sn, 60, "Adt")
        AddCN(sn, 61, "АС1")
        AddCN(sn, 62, "АС2")
        AddCN(sn, 63, "АС3")
        AddCN(sn, 64, "АМ1")
        AddCN(sn, 65, "АМ2")
        AddCN(sn, 66, "АМ3")
        AddCN(sn, 67, "АQ")
        AddCN(sn, 68, "АQг")




        dr = dt.NewRow
        dr("Название") = "Время прибора"
        dr("Значение") = d.ToString()
        dt.Rows.Add(dr)



        Dim stpos As Integer
        Dim curpos As Integer
        Dim t4portion As Integer
        Dim pair As KeyValuePair(Of Integer, String)
        Dim ikey As Integer
        t4portion = 25
        For stpos = 0 To cn.Count Step t4portion
            msg = New messageM4(cmdM4.cmd_PARAM, 0)
            curpos = 0
            For Each pair In cn
                If curpos >= stpos Then
                    ikey = (pair.Key)
                    block = New blockM4(TegM4.teg_PNUM, 3)
                    block.data(0) = 0 ' chanel
                    block.data(1) = ikey Mod 256
                    block.data(2) = ikey \ 256
                    msg.Tegs.Add(block)
                End If
                curpos += 1
                If curpos = stpos + t4portion Then
                    Exit For
                End If
            Next




            mID = NextID()

            barr = msg.BuildMessage(mID)

            EraseInputQueue()

            write(barr, barr.Length)
            WaitForData()


            i = MyRead(inbuf, 0, 1024, 2000)
            If i > 0 Then
                If CheckCRC16(inbuf, 1, i - 3) Then
                    msg = ParseM4Message(inbuf)
                    If msg.cmd = cmdM4.cmd_PARAM Then 'msg.ID = mID And
                        i = 0
                        curpos = 0
                        Dim sName As String
                        For Each pair In cn
                            sName = pair.Value
                            If curpos >= stpos Then
                                block = msg.Tegs(i)
                                i += 1
                                If block.teg = TegM4.teg_ASCIIString Then
                                    dr = dt.NewRow
                                    dr("Название") = sName

                                    s = ""
                                    For j = 0 To block.dl - 1
                                        s = s + Chr(block.data(j))
                                    Next
                                    dr("Значение") = s
                                    dt.Rows.Add(dr)
                                Else
                                    Debug.Print(block.teg.ToString)
                                End If
                            End If
                            curpos += 1
                            If curpos = stpos + t4portion Then
                                Exit For
                            End If
                        Next

                    End If
                End If
            End If
            Debug.Print(stpos.ToString())
        Next  ' stpos


        For stpos = 0 To sn.Count Step t4portion

            msg = New messageM4(cmdM4.cmd_PARAM, 0)
            curpos = 0
            For Each pair In sn
                If curpos >= stpos Then
                    ikey = (pair.Key)
                    block = New blockM4(TegM4.teg_PNUM, 3)
                    block.data(0) = 1 ' chanel
                    block.data(1) = ikey Mod 256
                    block.data(2) = ikey \ 256
                    msg.Tegs.Add(block)
                End If
                curpos += 1
                If curpos >= stpos + t4portion Then
                    Exit For
                End If
            Next



            mID = NextID()

            barr = msg.BuildMessage(mID)

            EraseInputQueue()

            write(barr, barr.Length)
            WaitForData()


            i = MyRead(inbuf, 0, 1024, 2000)
            If i > 0 Then
                If CheckCRC16(inbuf, 1, i - 3) Then
                    msg = ParseM4Message(inbuf)
                    If msg.cmd = cmdM4.cmd_PARAM Then 'msg.ID = mID And
                        i = 0
                        curpos = 0
                        Dim sName As String
                        For Each pair In sn
                            sName = pair.Value
                            If curpos >= stpos Then
                                block = msg.Tegs(i)
                                i += 1
                                If block.teg = TegM4.teg_ASCIIString Then
                                    dr = dt.NewRow
                                    dr("Название") = "TV1. " + sName

                                    s = ""
                                    For j = 0 To block.dl - 1
                                        s = s + Chr(block.data(j))
                                    Next
                                    dr("Значение") = s
                                    dt.Rows.Add(dr)
                                End If
                            End If
                            curpos += 1
                            If curpos >= stpos + t4portion Then
                                Exit For
                            End If
                        Next


                        ok = True
                    End If
                End If
            End If
        Next


        For stpos = 0 To sn.Count Step t4portion

            msg = New messageM4(cmdM4.cmd_PARAM, 0)
            curpos = 0
            For Each pair In sn
                If curpos >= stpos Then
                    ikey = (pair.Key)
                    block = New blockM4(TegM4.teg_PNUM, 3)
                    block.data(0) = 2 ' chanel
                    block.data(1) = ikey Mod 256
                    block.data(2) = ikey \ 256
                    msg.Tegs.Add(block)
                End If
                curpos += 1
                If curpos >= stpos + t4portion Then
                    Exit For
                End If
            Next



            mID = NextID()

            barr = msg.BuildMessage(mID)

            EraseInputQueue()

            write(barr, barr.Length)
            WaitForData()


            i = MyRead(inbuf, 0, 1024, 2000)
            If i > 0 Then
                If CheckCRC16(inbuf, 1, i - 3) Then
                    msg = ParseM4Message(inbuf)
                    If msg.cmd = cmdM4.cmd_PARAM Then 'msg.ID = mID And
                        i = 0
                        curpos = 0
                        Dim sName As String
                        For Each pair In sn
                            sName = pair.Value
                            If curpos >= stpos Then
                                block = msg.Tegs(i)
                                i += 1
                                If block.teg = TegM4.teg_ASCIIString Then
                                    dr = dt.NewRow
                                    dr("Название") = "TV2. " + sName

                                    s = ""
                                    For j = 0 To block.dl - 1
                                        s = s + Chr(block.data(j))
                                    Next
                                    dr("Значение") = s
                                    dt.Rows.Add(dr)
                                End If
                            End If
                            curpos += 1
                            If curpos >= stpos + t4portion Then
                                Exit For
                            End If
                        Next


                        ok = True
                    End If
                End If
            End If
        Next


        Return dt
    End Function

  




    Public Overrides Sub Connect()
        Dim i As Integer

        For i = 0 To 5
            If TryConnect() Then
                If BaudRate = 9600 Then
                    Set9600()
                End If
                Return ' True
            End If
        Next
        Return 'False

    End Sub



    Public Sub New()

    End Sub
End Class
