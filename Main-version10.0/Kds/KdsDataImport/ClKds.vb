﻿Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Math
Imports System.Net
Imports KdsLibrary
Imports KdsLibrary.DAL
Imports System.Threading
Imports DalOraInfra.DAL
Imports KDSCommon.Enums
Imports KdsLibrary.clGeneral






Public Class ClKds
    'todo: in all places use the kds web-config
    'todo: add getrow to utils instead of Getrowkds
    'todo: Application.DoEvents
#Region "Klitat Netunim From Files Shaonim"
    Public Sub TryKdsFile()

        'in web.config & app.config key="KdsInputFileName" value="DO1-KDS*.TXT"
        Dim FileName As String = ConfigurationSettings.AppSettings("KdsInputFileName")
        Dim InPath As String = ConfigurationSettings.AppSettings("KdsFilePath") '"\\kdstst01\Files\"
        Dim SubFolder As String = ConfigurationSettings.AppSettings("KdsFileSubPath") '"inkds_old\"
        Dim FileNameOld As String
        Dim MyFile As String
        Dim ShaonimNumber As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim strErrorOfFiles As String = String.Empty
        Dim ErrorCounter As Integer = 0
        Try
            MyFile = Dir(InPath & FileName)
            If Not MyFile = "" Then
                ShaonimNumber = oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Wait, "shaonim", 0)
                ''**KdsWriteProcessLog(2, 1, 1, "start shaonim")

                While Not MyFile = ""
                    Try
                        LoadKdsFile(MyFile)
                    Catch ex As Exception
                        ErrorCounter = ErrorCounter + 1
                        strErrorOfFiles = strErrorOfFiles & ErrorCounter.ToString() & "." & ex.Message & vbCr
                    End Try
                    FileNameOld = Left(MyFile, Len(MyFile) - 4) & ".old"
                    File.Copy(InPath & MyFile, InPath & SubFolder & FileNameOld, True)
                    File.Delete(InPath & MyFile)
                    MyFile = Dir()
                End While
                oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, "shaonim", 0)
                If (strErrorOfFiles <> String.Empty) Then
                    Throw New Exception(strErrorOfFiles)
                End If
                ''*KdsWriteProcessLog(2, 1, 2, "end shaonim")
            End If

        Catch ex As Exception
            oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "shaonim aborted" & ex.Message, 3)
            ''**KdsWriteProcessLog(2, 1, 3, "shaonim aborted" & ex.Message, "3")
            Throw ex
        End Try
    End Sub
    Public Sub LoadKdsFile(ByVal InFileName)

        Dim ErrFileName As String
        Dim oDal As clDal
        Dim sr As StreamReader
        Dim sw As StreamWriter
        Dim dt As DataTable
        Dim ds As DataSet
        Dim dr As DataRow
        Dim line As String
        Dim InPathNFile As String
        Dim SRV_D_ISHI As String
        Dim SRV_D_TAARICH As String
        Dim SRV_D_SUG_TNUA As String
        Dim SRV_D_SIBAT_DIVUACH_KNISA As String
        Dim SRV_D_KNISA_X As String
        Dim SRV_D_MIKUM_KNISA As String
        Dim SRV_D_SIBAT_DIVUACH_YETZIA As String
        Dim SRV_D_YETZIA_X As String
        Dim SRV_D_MIKUM_YETZIA As String
        Dim SRV_D_ISHI_MEADKEN As String
        'Dim SRV_D_KOD_PITZUL_X As String
        Dim SRV_D_KOD_BITUL_ZMAN_NESIA_X As String
        'Dim SRV_D_KOD_LINA_X As String
        Dim SRV_D_KOD_CHARIGA_X As String
        Dim SRV_D_KOD_HALBASHA_X As String
        'Dim SRV_D_KOD_MICHUTZ_LAMICHSA_X As String
        'Dim SRV_D_KOD_HAMARA_X As String
        Dim SRV_D_KOD_HAZMANA_X As String
        Dim SRV_D_new_sidur As String
        Dim calc_D_new_sidur As String
        Dim TAARICH_knisa_p24 As String
        Dim TAARICH_yetzia_p24 As String
        Dim efes As String
        Dim retsql As String
        Dim SQ99_COUNTER As Integer
        Dim WS99_EZER As Integer
        Dim ez1 As Integer
        Dim ez2 As Integer
        Dim ez3 As Integer
        Dim ez4 As Integer
        Dim Times_knisa_p24 As Integer
        Dim Times_yetzia_p24 As Integer
        Dim kpISHI As String
        Dim kpSidur As String
        Dim kpTAARICH As String
        Dim NumLInDS As Integer
        Dim i As Integer
        Dim thenextday As String
        Dim thenextdt As String
        Dim DatEfes As String
        Dim knisa_2chck As String
        Dim yetzia_2chck As String
        Dim FoundAMatch As Boolean
        Dim SwIsOpen As Boolean
        Dim KeepIin As Integer
        Dim SRV_D_KNISA_letashlum_musach_X As String
        Dim SRV_D_gmar_letashlum_musach_X As String
        Dim TAARICH_knisa_musach_p24 As String
        Dim TAARICH_gmar_musach_p24 As String
        Dim Times_knisa_musach_p24 As Integer
        Dim Times_gmar_musach_p24 As Integer
        Dim InpStr As String
        Dim FoundAchange As Boolean
        Dim check_chariga As String
        Dim check_lo_letashlum As String
        Dim check_nesiot As String
        Dim check_halbasha As String
        Dim ifUpdChariga As Boolean
        Dim ifUpdhashlama As Boolean
        Dim ifUpdhalbasha As Boolean
        Dim ifUpdnesiot As Boolean
        Dim ifUpdletashlum As Boolean
        Dim p_letashlum As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try

            '''KdsWriteProcessLog(2, 1, 1, "start")
            efes = "0000000000"
            DatEfes = "00010101"
            ErrFileName = ConfigurationSettings.AppSettings("KdsFilePath") & "Kds" & CStr(Now.Year) & CStr(Now.Month) & CStr(Now.Day) & CStr(Now.Hour) & CStr(Now.Minute) & CStr(Now.Second) & ".err"
            InPathNFile = ConfigurationSettings.AppSettings("KdsFilePath") & InFileName
            sr = New StreamReader(InPathNFile)
            kpISHI = ""
            kpSidur = ""
            kpTAARICH = ""
            SwIsOpen = False

            line = sr.ReadLine
            oDal = New clDal
            InpStr = ""
            While Not line Is Nothing
                Try
                    retsql = ""
                    If Len(line) < 85 Then
                        If SwIsOpen = False Then
                            sw = New StreamWriter(ErrFileName, False)
                            SwIsOpen = True
                        End If
                        sw.WriteLine("zevel " & line)
                    ElseIf CInt(Mid(line, 11, 2)) < 1 Or CInt(Mid(line, 11, 2)) > 12 Then
                        If SwIsOpen = False Then
                            sw = New StreamWriter(ErrFileName, False)
                            SwIsOpen = True
                        End If
                        sw.WriteLine("zevel " & line)
                    ElseIf CInt(Mid(line, 7, 4)) > 2054 Then
                        If SwIsOpen = False Then
                            sw = New StreamWriter(ErrFileName, False)
                            SwIsOpen = True
                        End If
                        sw.WriteLine("zevel " & line)
                    ElseIf CInt(Mid(line, 13, 2)) < 1 Or CInt(Mid(line, 13, 2)) > 31 Then
                        If SwIsOpen = False Then
                            sw = New StreamWriter(ErrFileName, False)
                            SwIsOpen = True
                        End If
                        sw.WriteLine("zevel " & line)
                    ElseIf Not Mid(line, 82, 2) = "99" Then
                        If SwIsOpen = False Then
                            sw = New StreamWriter(ErrFileName, False)
                            SwIsOpen = True
                        End If
                        sw.WriteLine("no99 " & line)
                    ElseIf Mid(line, 82, 5) = "99214" Then
                        If Not Mid(line, 20, 4) = "0000" And Not Mid(line, 20, 4) = "    " Then
                            If Mid(line, 38, 4) = "0000" Or Mid(line, 38, 4) = "    " Then
                                LoadPundakim(line)
                            Else
                                If SwIsOpen = False Then
                                    sw = New StreamWriter(ErrFileName, False)
                                    SwIsOpen = True
                                End If
                                sw.WriteLine("waiman:hityazvut with yetzia " & line)
                                oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.PartialFinish, "waiman:hityazvut with yetzia " & line, 0)
                                ''**  KdsWriteProcessLog(2, 1, 4, "waiman:hityazvut with yetzia " & line)
                            End If
                        Else
                            If SwIsOpen = False Then
                                sw = New StreamWriter(ErrFileName, False)
                                SwIsOpen = True
                            End If
                            sw.WriteLine("waiman:hityazvut knisa zero " & line)
                            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.PartialFinish, "waiman:hityazvut knisa zero " & line, 6)
                            ''** KdsWriteProcessLog(2, 1, 4, "waiman:hityazvut knisa zero " & line, 6)
                        End If
                    Else
                        SRV_D_ISHI = Mid(line, 1, 5)
                        SRV_D_TAARICH = Mid(line, 7, 8) 'format=yyyymmdd
                        SRV_D_SUG_TNUA = Mid(line, 15, 2)
                        If Mid(line, 82, 2) = "99" Then
                            SRV_D_new_sidur = Mid(line, 82, 5)
                        Else
                            SRV_D_new_sidur = ""
                        End If
                        calc_D_new_sidur = SRV_D_new_sidur
                        If calc_D_new_sidur = "99001" Then
                            dt = Nothing
                            dt = New DataTable
                            ds = Nothing
                            ds = New DataSet
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("PKG_BATCH.pro_IfSdrnManas", ds)
                            NumLInDS = ds.Tables(0).Rows.Count
                            If Not NumLInDS = 0 Then
                                If ds.Tables(0).Rows(0).Item("erech").ToString = "0420" Then
                                    calc_D_new_sidur = "99224"
                                ElseIf ds.Tables(0).Rows(0).Item("erech").ToString = "420" Then
                                    calc_D_new_sidur = "99224"
                                ElseIf ds.Tables(0).Rows(0).Item("erech").ToString = "0422" Then
                                    calc_D_new_sidur = "99225"
                                ElseIf ds.Tables(0).Rows(0).Item("erech").ToString = "422" Then
                                    calc_D_new_sidur = "99225"
                                ElseIf Not ds.Tables(0).Rows(0).Item("erech").ToString = "" Then
                                    If SwIsOpen = False Then
                                        sw = New StreamWriter(ErrFileName, False)
                                        SwIsOpen = True
                                    End If
                                    sw.WriteLine("sdrn manas:wrong kod " & ds.Tables(0).Rows(0).Item("erech").ToString & SRV_D_TAARICH & SRV_D_ISHI)
                                    oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.PartialFinish, "sdrn manas:wrong kod " & ds.Tables(0).Rows(0).Item("erech").ToString & SRV_D_TAARICH & SRV_D_ISHI, 6)
                                    ''**  KdsWriteProcessLog(2, 1, 4, "sdrn manas:wrong kod " & ds.Tables(0).Rows(0).Item("erech").ToString & SRV_D_TAARICH & SRV_D_ISHI, "6")
                                End If
                            End If
                            '20141116: sidur meshek
                            ds = Nothing
                            ds = New DataSet
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("PKG_BATCH.pro_IfSidurMeshek", ds)
                            NumLInDS = ds.Tables(0).Rows.Count
                            If Not NumLInDS = 0 Then
                                If ds.Tables(0).Rows(0).Item("cntMeshek").ToString = "1" Then
                                    'todo: calc_D_new_sidur = "99031"
                                    LoadRecMeshek(line)
                                End If
                            End If
                        End If
                        SRV_D_SIBAT_DIVUACH_KNISA = Mid(line, 18, 2)
                        If Mid(line, 20, 4) = "0000" Then
                            SRV_D_KNISA_X = ""
                        Else
                            SRV_D_KNISA_X = Mid(line, 20, 4) 'format=hhmm
                        End If
                        TAARICH_knisa_p24 = ""
                        Times_knisa_p24 = 0
                        If Not Trim(Mid(SRV_D_KNISA_X, 1, 2)) = "" Then
                            If CInt(Mid(SRV_D_KNISA_X, 1, 2)) >= 24 Then
                                'in case hour is=>24
                                Times_knisa_p24 = CInt(Abs(CInt(Mid(SRV_D_KNISA_X, 1, 2)) / 24))
                                SRV_D_KNISA_X = (CInt(SRV_D_KNISA_X) - Times_knisa_p24 * 2400).ToString
                                TAARICH_knisa_p24 = "+" & Times_knisa_p24.ToString
                                If Len(SRV_D_KNISA_X) < 4 Then
                                    SRV_D_KNISA_X = Left(efes, 4 - Len(SRV_D_KNISA_X)) & SRV_D_KNISA_X
                                End If
                            End If
                        End If
                        If Not (Mid(line, 32, 3) = "000" Or Trim(Mid(line, 32, 3)) = "") And Trim(Mid(line, 95, 2)) = "" Then
                            SRV_D_MIKUM_KNISA = Mid(line, 32, 3) & "00"
                        ElseIf ((Mid(line, 32, 3) = "000" Or Trim(Mid(line, 32, 3)) = "")) Then
                            SRV_D_MIKUM_KNISA = Mid(line, 32, 3) & "00"
                        Else
                            SRV_D_MIKUM_KNISA = Mid(line, 32, 3) & Mid(line, 95, 2)
                        End If
                        SRV_D_SIBAT_DIVUACH_YETZIA = Mid(line, 36, 2)
                        If Mid(line, 38, 4) = "0000" Then
                            SRV_D_YETZIA_X = ""
                        Else
                            SRV_D_YETZIA_X = Mid(line, 38, 4)
                        End If
                        If Not (Mid(line, 50, 3) = "000" Or Trim(Mid(line, 50, 3)) = "") And Trim(Mid(line, 97, 2)) = "" Then
                            SRV_D_MIKUM_YETZIA = Mid(line, 50, 3) & "00"
                        ElseIf ((Mid(line, 50, 3) = "000" Or Trim(Mid(line, 50, 3)) = "")) Then
                            SRV_D_MIKUM_YETZIA = Mid(line, 50, 3) & "00"
                        Else
                            SRV_D_MIKUM_YETZIA = Mid(line, 50, 3) & Mid(line, 97, 2)
                        End If
                        'todo: in case 95-96 or 97-98 is not numeric...
                        SRV_D_ISHI_MEADKEN = Mid(line, 61, 5)
                        FoundAchange = False
                        'SRV_D_KOD_PITZUL_X = Mid(line, 74, 1)
                        SRV_D_KOD_BITUL_ZMAN_NESIA_X = Mid(line, 75, 1)
                        If Not Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X) = "" Then
                            FoundAchange = True
                        End If
                        'SRV_D_KOD_LINA_X = Mid(line, 76, 1)
                        SRV_D_KOD_CHARIGA_X = Mid(line, 77, 1)
                        If Not Trim(SRV_D_KOD_CHARIGA_X) = "" Then
                            FoundAchange = True
                        End If
                        SRV_D_KOD_HALBASHA_X = Mid(line, 78, 1)
                        If Not Trim(SRV_D_KOD_HALBASHA_X) = "" Then
                            FoundAchange = True
                        End If
                        'SRV_D_KOD_MICHUTZ_LAMICHSA_X = Mid(line, 79, 1)
                        'SRV_D_KOD_HAMARA_X = Mid(line, 80, 1)
                        SRV_D_KOD_HAZMANA_X = Mid(line, 81, 1)
                        If Not Trim(SRV_D_KOD_HAZMANA_X) = "" Then
                            FoundAchange = True
                        End If
                        'SRV_D_new_sidur = Mid(line, 82, 5)
                        SRV_D_KNISA_letashlum_musach_X = Mid(line, 87, 4)
                        If SRV_D_KNISA_letashlum_musach_X = "0000" Then
                            SRV_D_KNISA_letashlum_musach_X = "  "
                        End If
                        SRV_D_gmar_letashlum_musach_X = Mid(line, 91, 4)
                        If SRV_D_gmar_letashlum_musach_X = "0000" Then
                            SRV_D_gmar_letashlum_musach_X = "  "
                        End If

                        TAARICH_yetzia_p24 = ""
                        Times_yetzia_p24 = 0
                        If Not Trim(Mid(SRV_D_YETZIA_X, 1, 2)) = "" Then
                            If CInt(Mid(SRV_D_YETZIA_X, 1, 2)) >= 24 Then
                                'in case hour is=>24
                                Times_yetzia_p24 = CInt(Abs(CInt(Mid(SRV_D_YETZIA_X, 1, 2)) / 24))
                                SRV_D_YETZIA_X = (CInt(SRV_D_YETZIA_X) - Times_yetzia_p24 * 2400).ToString
                                TAARICH_yetzia_p24 = "+" & Times_yetzia_p24.ToString
                                If Len(SRV_D_YETZIA_X) < 4 Then
                                    SRV_D_YETZIA_X = Left(efes, 4 - Len(SRV_D_YETZIA_X)) & SRV_D_YETZIA_X
                                End If
                            End If
                        End If
                        TAARICH_knisa_musach_p24 = ""
                        'Times_knisa_musach_p24 = 0
                        If Not Trim(Mid(SRV_D_KNISA_letashlum_musach_X, 1, 2)) = "" Then
                            If CInt(Mid(SRV_D_KNISA_letashlum_musach_X, 1, 2)) >= 24 Then
                                'in case hour is=>24
                                Times_knisa_musach_p24 = CInt(Abs(CInt(Mid(SRV_D_KNISA_letashlum_musach_X, 1, 2)) / 24))
                                SRV_D_KNISA_letashlum_musach_X = (CInt(SRV_D_KNISA_letashlum_musach_X) - Times_knisa_musach_p24 * 2400).ToString
                                TAARICH_knisa_musach_p24 = "+" & Times_knisa_musach_p24.ToString
                                If Len(SRV_D_KNISA_letashlum_musach_X) < 4 Then
                                    SRV_D_KNISA_letashlum_musach_X = Left(efes, 4 - Len(SRV_D_KNISA_letashlum_musach_X)) & SRV_D_KNISA_letashlum_musach_X
                                End If
                            End If
                        End If
                        TAARICH_gmar_musach_p24 = ""
                        'Times_gmar_musach_p24 = 0
                        If Not Trim(Mid(SRV_D_gmar_letashlum_musach_X, 1, 2)) = "" Then
                            If CInt(Mid(SRV_D_gmar_letashlum_musach_X, 1, 2)) >= 24 Then
                                'in case hour is=>24
                                Times_gmar_musach_p24 = CInt(Abs(CInt(Mid(SRV_D_gmar_letashlum_musach_X, 1, 2)) / 24))
                                SRV_D_gmar_letashlum_musach_X = (CInt(SRV_D_gmar_letashlum_musach_X) - Times_gmar_musach_p24 * 2400).ToString
                                TAARICH_gmar_musach_p24 = "+" & Times_gmar_musach_p24.ToString
                                If Len(SRV_D_gmar_letashlum_musach_X) < 4 Then
                                    SRV_D_gmar_letashlum_musach_X = Left(efes, 4 - Len(SRV_D_gmar_letashlum_musach_X)) & SRV_D_gmar_letashlum_musach_X
                                End If
                            End If
                        End If
                        If SRV_D_new_sidur = "99820" Then
                            If SRV_D_KNISA_X = "0000" Then
                                SRV_D_KNISA_X = "0001"
                            End If
                            If SRV_D_YETZIA_X = "0000" Then
                                SRV_D_YETZIA_X = "2359"
                            End If
                        End If
                        If Not (SRV_D_KOD_HAZMANA_X = "0" Or Trim(SRV_D_KOD_HAZMANA_X) = "" Or SRV_D_KOD_HAZMANA_X = "7") Then
                            If SwIsOpen = False Then
                                sw = New StreamWriter(ErrFileName, False)
                                SwIsOpen = True
                            End If
                            sw.WriteLine("waiman:hashlama " & line)
                            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.PartialFinish, "waiman:hashlama " & line, 6)
                            ''**   KdsWriteProcessLog(2, 1, 4, "waiman:hashlama " & line, "6")
                        End If

                        SQ99_COUNTER = 0
                        WS99_EZER = 0
                        ez1 = 0
                        ez2 = 0
                        ez3 = 0
                        ez4 = 0
                        InpStr = SRV_D_ISHI & "," & SRV_D_TAARICH & "," & calc_D_new_sidur
                        InpStr = InpStr & "," & SRV_D_KNISA_X & "," & SRV_D_MIKUM_KNISA & "," & SRV_D_SIBAT_DIVUACH_KNISA
                        InpStr = InpStr & "," & SRV_D_YETZIA_X & "," & SRV_D_MIKUM_YETZIA & "," & SRV_D_SIBAT_DIVUACH_YETZIA
                        InpStr = InpStr & "," & SRV_D_ISHI_MEADKEN & "," & SRV_D_KOD_BITUL_ZMAN_NESIA_X
                        InpStr = InpStr & "," & SRV_D_KOD_CHARIGA_X & "," & SRV_D_KOD_HALBASHA_X & "," & SRV_D_KOD_HAZMANA_X
                        InpStr = InpStr & "," & TAARICH_knisa_p24 & "," & TAARICH_yetzia_p24 & "," & DatEfes
                        InpStr = InpStr & "," & TAARICH_knisa_musach_p24 & "," & SRV_D_KNISA_letashlum_musach_X
                        InpStr = InpStr & "," & TAARICH_gmar_musach_p24 & "," & SRV_D_gmar_letashlum_musach_X
                        'find the day after
                        dt = Nothing
                        dt = New DataTable
                        oDal.ClearCommand()
                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                        oDal.ExecuteSP("PKG_BATCH.pro_GetRowDt", dt)
                        thenextday = dt.Rows(0).Item("thenextday").ToString

                        DatEfes = "00010101"
                        kpISHI = SRV_D_ISHI
                        kpSidur = calc_D_new_sidur
                        kpTAARICH = SRV_D_TAARICH
                        dt = New DataTable
                        oDal.ClearCommand()
                        oDal.AddParameter("pIshi", ParameterType.ntOracleInteger, CInt(SRV_D_ISHI), ParameterDir.pdInput)
                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                        '20120723 the following fields is overlooked. still being sent for integrity reasons.
                        If Trim(TAARICH_knisa_p24) = "" Then
                            oDal.AddParameter("pDt24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                        Else
                            oDal.AddParameter("pDt24", ParameterType.ntOracleInteger, CInt(TAARICH_knisa_p24), ParameterDir.pdInput)
                        End If
                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                        oDal.ExecuteSP("Pkg_clock.pro_GetRowKds", dt)

                        SQ99_COUNTER = CInt(dt.Rows(0).Item("ct99").ToString)
                        If SQ99_COUNTER = 0 Then
                            'insert tb_yamey_avoda_ovdim
                            oDal.ClearCommand()
                            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
                            '20120723If Trim(TAARICH_knisa_p24) = "" Then
                            oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                            '20120723Else
                            '20120723 oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, thenextday, ParameterDir.pdInput)
                            '20120723End If
                            oDal.ExecuteSP("PKG_BATCH.pro_ins_yamey_avoda_1oved")
                            'KdsSql = KdsSql & "insert into tb_yamey_avoda_ovdim ("
                            'KdsSql = KdsSql & " mispar_ishi,taarich  )     "
                            'KdsSql = KdsSql & " values  ( "
                            'KdsSql = KdsSql & SRV_D_ISHI & ","
                            'KdsSql = KdsSql & "to_date('" & SRV_D_TAARICH & "','yyyymmdd')" & TAARICH_knisa_p24 & " ); "
                        End If

                        'extract all records from sidurim_ovdim to a dataset
                        'clean parameters:
                        ds = Nothing
                        ds = New DataSet
                        oDal.ClearCommand()
                        '20120723If Times_knisa_p24 > 0 Then
                        '20120723oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, thenextday, ParameterDir.pdInput)
                        '20120723Else
                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                        '20120723End If
                        oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                        oDal.AddParameter("psidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                        oDal.ExecuteSP("PKG_clock.pro_GetListDs", ds)
                        NumLInDS = ds.Tables(0).Rows.Count
                        'End If

                        FoundAMatch = False
                        If NumLInDS = 0 Then
                            '1) this if is due to my estimation that 50% of the data is a solo insert.
                            'this is a new record, nothing to be checked.
                            'KdsSql2 = 
                            'If calc_D_new_sidur <> 99200 Then
                            '    pro_upd_yamey = True
                            'End If
                            FoundAchange = False
                            InpStr = "new_rec:" & line & ":" & InpStr
                            new_rec(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
        SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
        SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
        SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
        SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
        SRV_D_KOD_HAZMANA_X, _
        TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
        TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
        TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                            'todo: send datefes for new_rec
                            'add this new rec to the dataset for the next line.
                            dr = ds.Tables(0).NewRow
                            If SRV_D_KNISA_X = "" Then
                                dr.Item("shat_hatchala") = DatEfes
                                dt = Nothing
                                dt = New DataTable
                                oDal.ClearCommand()
                                oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
                                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtLong", dt)
                                'dt = GetRowKds("dual", "", "to_char(to_date('" & DatEfes & "' ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes", retsql)
                                DatEfes = dt.Rows(0).Item("thenextefes").ToString
                            Else
                                If Times_knisa_p24 > 0 Then
                                    dr.Item("shat_hatchala") = thenextday & SRV_D_KNISA_X
                                Else
                                    dr.Item("shat_hatchala") = SRV_D_TAARICH & SRV_D_KNISA_X
                                End If
                            End If
                            If Not SRV_D_YETZIA_X = "" Then
                                If Times_yetzia_p24 > 0 Then
                                    dr.Item("shat_gmar") = thenextday & SRV_D_YETZIA_X
                                Else
                                    dr.Item("shat_gmar") = SRV_D_TAARICH & SRV_D_YETZIA_X
                                End If
                            End If
                            If Not SRV_D_KNISA_letashlum_musach_X = "" Then
                                If Not TAARICH_knisa_musach_p24 = "" Then
                                    dr.Item("shat_hatchala_letashlum_musach") = thenextday & SRV_D_KNISA_letashlum_musach_X
                                Else
                                    dr.Item("shat_hatchala_letashlum_musach") = SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X
                                End If
                            End If
                            If Not SRV_D_gmar_letashlum_musach_X = "" Then
                                If Not TAARICH_gmar_musach_p24 = "" Then
                                    dr.Item("shat_gmar_letashlum_musach") = thenextday & SRV_D_gmar_letashlum_musach_X
                                Else
                                    dr.Item("shat_gmar_letashlum_musach") = SRV_D_TAARICH & SRV_D_gmar_letashlum_musach_X
                                End If
                            End If
                            ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                            NumLInDS = NumLInDS + 1
                            FoundAMatch = True
                        Else
                            'here comes trouble...
                            KeepIin = 0
                            For i = 0 To NumLInDS - 1
                                ez1 = 0
                                ez2 = 0
                                ez3 = 0
                                ez4 = 0
                                If Left(ds.Tables(0).Rows(i).Item("shat_hatchala").ToString, 8) = "00010101" Then
                                    ez1 = 1
                                    DatEfes = ds.Tables(0).Rows(i).Item("shat_hatchala").ToString
                                    dt = Nothing
                                    dt = New DataTable
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
                                    oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                    oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtLong", dt)
                                    'dt = GetRowKds("dual", "", "to_char(to_date('" & DatEfes & "' ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes", retsql)
                                    DatEfes = dt.Rows(0).Item("thenextefes").ToString
                                    If SRV_D_KNISA_X = "" Then
                                        ez3 = 1
                                    End If
                                End If
                                If ds.Tables(0).Rows(i).Item("shat_gmar").ToString = "" Then
                                    ez2 = 1
                                End If
                                If Times_knisa_p24 > 0 Then
                                    knisa_2chck = thenextday & SRV_D_KNISA_X
                                    If ds.Tables(0).Rows(i).Item("shat_hatchala").ToString = thenextday & SRV_D_KNISA_X Then
                                        ez3 = 1
                                        KeepIin = i + 1
                                    End If
                                Else
                                    knisa_2chck = SRV_D_TAARICH & SRV_D_KNISA_X
                                    If ds.Tables(0).Rows(i).Item("shat_hatchala").ToString = SRV_D_TAARICH & SRV_D_KNISA_X Then
                                        ez3 = 1
                                        KeepIin = i + 1
                                    End If
                                End If
                                If Times_yetzia_p24 > 0 Then
                                    yetzia_2chck = thenextday & SRV_D_YETZIA_X
                                    If ds.Tables(0).Rows(i).Item("shat_gmar").ToString = thenextday & SRV_D_YETZIA_X Then
                                        ez4 = 1
                                    End If
                                Else
                                    yetzia_2chck = SRV_D_TAARICH & SRV_D_YETZIA_X
                                    If ds.Tables(0).Rows(i).Item("shat_gmar").ToString = SRV_D_TAARICH & SRV_D_YETZIA_X Then
                                        ez4 = 1
                                    ElseIf ds.Tables(0).Rows(i).Item("shat_gmar").ToString = "" And SRV_D_YETZIA_X = "" Then
                                        ez4 = 1
                                    End If
                                End If

                                'phase1: only in
                                If (Not SRV_D_KNISA_X = "") And SRV_D_YETZIA_X = "" Then 'only in
                                    If ez3 = 1 Then
                                        '2) duplicate - exit
                                        If Not Trim(SRV_D_KNISA_letashlum_musach_X) = "" Then
                                            If Not ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach").ToString = SRV_D_KNISA_letashlum_musach_X Then
                                                'KdsSql2 = 
                                                FoundAchange = False
                                                InpStr = "upd_in_out_letashlum:" & line & ":" & InpStr
                                                upd_in_out_letashlum(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                 SRV_D_KNISA_X, SRV_D_YETZIA_X, SRV_D_ISHI_MEADKEN, _
                                 SRV_D_KOD_BITUL_ZMAN_NESIA_X, SRV_D_KOD_HALBASHA_X, _
                                 SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HAZMANA_X, _
                                 TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                                 TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                                 TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                                If Not TAARICH_knisa_musach_p24 = "" Then
                                                    ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach") = thenextday & SRV_D_KNISA_letashlum_musach_X
                                                Else
                                                    ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach") = SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X
                                                End If
                                            End If
                                        End If
                                        FoundAMatch = True
                                        i = NumLInDS - 1
                                    ElseIf ez1 = 1 Then
                                        If ds.Tables(0).Rows(i).Item("shat_gmar").ToString = "" Then
                                            'try next i, this record has 01/01/0001 as in and null as out
                                        Else
                                            If Not (CLng(knisa_2chck) > CLng(ds.Tables(0).Rows(i).Item("shat_gmar").ToString)) Then
                                                '3) found a record to update it's in
                                                'todo: check if this is the maximum-in 
                                                '      or if exists another record with its in the same
                                                'KdsSql2 = 
                                                'If calc_D_new_sidur <> 99200 Then
                                                '    pro_upd_yamey = True
                                                'End If
                                                'todo: 20091217 check if this in already exists then out
                                                FoundAchange = False
                                                InpStr = "upd_in_blank:" & line & ":" & InpStr
                                                upd_in_blank(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                                Mid(ds.Tables(0).Rows(i).Item("shat_gmar").ToString, 9, 4), "", "", _
                                SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                                SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                                SRV_D_KOD_HAZMANA_X, _
                                TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                                TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                                TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                                'upd this rec in the dataset.
                                                ds.Tables(0).Rows(i).Item("shat_hatchala") = knisa_2chck
                                                If Not SRV_D_KNISA_letashlum_musach_X = "" Then
                                                    If Not TAARICH_knisa_musach_p24 = "" Then
                                                        ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach") = thenextday & SRV_D_KNISA_letashlum_musach_X
                                                    Else
                                                        ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach") = SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X
                                                    End If
                                                End If
                                                i = NumLInDS - 1
                                                FoundAMatch = True
                                            End If
                                        End If
                                    ElseIf i = NumLInDS - 1 And FoundAMatch = False Then
                                        If KeepIin > 0 And Not KeepIin = i Then
                                            '4) there is a duplicate in the in
                                            'oDal.ClearCommand()
                                            'oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, "19540228" & SRV_D_KNISA_X, ParameterDir.pdInput)
                                            'oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                            'oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtVeryLong", dt)
                                            ''dt = GetRowKds("dual", "", "to_char(to_date('19540228" & SRV_D_KNISA_X & "' ,'yyyymmddhh24mi')+(1/86400),'yyyymmddhh24miss') thenextdt", retsql)
                                            'thenextdt = dt.Rows(0).Item("thenextdt").ToString
                                            oDal.ClearCommand()
                                            If Times_knisa_p24 > 0 Then
                                                oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, thenextday, ParameterDir.pdInput)
                                                oDal.AddParameter("phatchala", ParameterType.ntOracleVarchar, thenextday & SRV_D_KNISA_X, ParameterDir.pdInput)
                                            Else
                                                oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                                oDal.AddParameter("phatchala", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
                                            End If
                                            oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                                            oDal.AddParameter("psidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
                                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                            dt = Nothing
                                            dt = New DataTable
                                            oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtVeryLong2", dt)
                                            thenextdt = dt.Rows(0).Item("thenextdt").ToString
                                            'KdsSql2 = 
                                            'If calc_D_new_sidur <> 99200 Then
                                            '    pro_upd_yamey = True
                                            'End If
                                            FoundAchange = False
                                            InpStr = "new_rec:" & line & ":" & InpStr
                                            new_rec(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                Mid(thenextdt, 9, 6), SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                                SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                                SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                                SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                                SRV_D_KOD_HAZMANA_X, _
                                TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                                TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                                TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                        Else
                                            'KdsSql2 = 
                                            'If calc_D_new_sidur <> 99200 Then
                                            '    pro_upd_yamey = True
                                            'End If
                                            FoundAchange = False
                                            InpStr = "new_rec:" & line & ":" & InpStr
                                            new_rec(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                                SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                                SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                                SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                                SRV_D_KOD_HAZMANA_X, _
                                TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                                TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                                TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                        End If
                                        'add this new rec to the dataset for the next line.
                                        dr = ds.Tables(0).NewRow
                                        If SRV_D_KNISA_X = "" Then
                                            dr.Item("shat_hatchala") = DatEfes
                                            dt = Nothing
                                            dt = New DataTable
                                            oDal.ClearCommand()
                                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
                                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                            oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtLong", dt)
                                            'dt = GetRowKds("dual", "", "to_char(to_date('" & DatEfes & "' ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes", retsql)
                                            DatEfes = dt.Rows(0).Item("thenextefes").ToString
                                        Else
                                            dr.Item("shat_hatchala") = knisa_2chck
                                            'todo: keepin instead of knisa2check
                                        End If
                                        If Not SRV_D_YETZIA_X = "" Then
                                            dr.Item("shat_gmar") = yetzia_2chck
                                        End If
                                        If Not SRV_D_KNISA_letashlum_musach_X = "" Then
                                            If Not TAARICH_knisa_musach_p24 = "" Then
                                                dr.Item("shat_hatchala_letashlum_musach") = thenextday & SRV_D_KNISA_letashlum_musach_X
                                            Else
                                                dr.Item("shat_hatchala_letashlum_musach") = SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X
                                            End If
                                        End If
                                        ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                                        FoundAMatch = True
                                        NumLInDS = NumLInDS + 1
                                    End If

                                    'phase2: in and  out
                                ElseIf (Not SRV_D_KNISA_X = "") And (Not SRV_D_YETZIA_X = "") Then
                                    If ez3 = 0 And ez4 = 0 Then
                                        'check next line
                                    ElseIf ez3 = 1 And ez4 = 1 Then
                                        '5) complete duplicate
                                        If Not (Trim(SRV_D_KNISA_letashlum_musach_X) = "" And Trim(SRV_D_gmar_letashlum_musach_X) = "") Then
                                            If Not (ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach").ToString = SRV_D_KNISA_letashlum_musach_X _
                                                And ds.Tables(0).Rows(i).Item("shat_gmar_letashlum_musach").ToString = SRV_D_gmar_letashlum_musach_X) Then
                                                'KdsSql2 = 
                                                FoundAchange = False
                                                InpStr = "upd_in_out_letashlum:" & line & ":" & InpStr
                                                upd_in_out_letashlum(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                 SRV_D_KNISA_X, SRV_D_YETZIA_X, SRV_D_ISHI_MEADKEN, _
                                 SRV_D_KOD_BITUL_ZMAN_NESIA_X, SRV_D_KOD_HALBASHA_X, _
                                  SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HAZMANA_X, _
                                 TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                                 TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                                 TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                            End If
                                            If Not Trim(SRV_D_KNISA_letashlum_musach_X) = "" Then
                                                If Not TAARICH_knisa_musach_p24 = "" Then
                                                    ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach") = thenextday & SRV_D_KNISA_letashlum_musach_X
                                                Else
                                                    ds.Tables(0).Rows(i).Item("shat_hatchala_letashlum_musach") = SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X
                                                End If
                                            End If
                                            If Not Trim(SRV_D_gmar_letashlum_musach_X) = "" Then
                                                If Not TAARICH_gmar_musach_p24 = "" Then
                                                    ds.Tables(0).Rows(i).Item("shat_gmar_letashlum_musach") = thenextday & SRV_D_gmar_letashlum_musach_X
                                                Else
                                                    ds.Tables(0).Rows(i).Item("shat_gmar_letashlum_musach") = SRV_D_TAARICH & SRV_D_gmar_letashlum_musach_X
                                                End If
                                            End If
                                        End If
                                        FoundAMatch = True
                                        i = NumLInDS - 1
                                        If ez1 = 1 Or ez2 = 1 Then
                                            'could not be!
                                            If SwIsOpen = False Then
                                                sw = New StreamWriter(ErrFileName, False)
                                                SwIsOpen = True
                                            End If
                                            sw.WriteLine("impossible1 " & line)
                                            'KdsWriteProcessLog(2, 1, 4, "impossible1 " & Trim(line))
                                        End If
                                    ElseIf ez3 = 1 And ez4 = 0 Then
                                        If ez2 = 1 And Not (CLng(ds.Tables(0).Rows(i).Item("shat_hatchala").ToString) > CLng(yetzia_2chck)) Then
                                            '6) found the line with in =knisa and no out
                                            'KdsSql2 = 
                                            'If calc_D_new_sidur <> 99200 Then
                                            '    pro_upd_yamey = True
                                            'End If
                                            FoundAchange = False
                                            InpStr = "upd_out_blank:" & line & ":" & InpStr
                                            upd_out_blank(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                            SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                            SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                            SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                            SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                            SRV_D_KOD_HAZMANA_X, _
                            TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                            TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                            TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                            'upd this rec in the dataset.
                                            ds.Tables(0).Rows(i).Item("shat_gmar") = yetzia_2chck
                                            FoundAMatch = True
                                            i = NumLInDS - 1
                                        End If
                                    ElseIf ez3 = 0 And ez4 = 1 Then
                                        If ez1 = 1 And Not (CLng(knisa_2chck) > CLng(ds.Tables(0).Rows(i).Item("shat_gmar").ToString)) Then
                                            '7)
                                            'KdsSql2 = 
                                            'If calc_D_new_sidur <> 99200 Then
                                            '    pro_upd_yamey = True
                                            'End If
                                            'todo: 20091217 check if this in already exists
                                            FoundAchange = False
                                            InpStr = "upd_in_blank:" & line & ":" & InpStr
                                            upd_in_blank(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                            SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                            SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                            SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                            SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                            SRV_D_KOD_HAZMANA_X, _
                            TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                            TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                            TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                            'upd this rec in the dataset.
                                            ds.Tables(0).Rows(i).Item("shat_hatchala") = knisa_2chck
                                            FoundAMatch = True
                                            i = NumLInDS - 1
                                            'Else
                                            '    If ez1 = 0 Or ez2 = 1 Then
                                            '        'could not be!
                                            '        If SwIsOpen = False Then
                                            '            sw = New StreamWriter(ErrFileName, False)
                                            '            SwIsOpen = True
                                            '        End If
                                            '        sw.WriteLine("impossible2 " & line)
                                            '        'KdsWriteProcessLog(2, 1, 4, "impossible2 " & Trim(line))
                                            '    End If
                                        End If
                                    ElseIf i = NumLInDS - 1 And FoundAMatch = False Then
                                        '8)
                                        'KdsSql2 = 
                                        'If calc_D_new_sidur <> 99200 Then
                                        '    pro_upd_yamey = True
                                        'End If
                                        FoundAchange = False
                                        InpStr = "new_rec:" & line & ":" & InpStr
                                        new_rec(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                            SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                            SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                            SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                            SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                            SRV_D_KOD_HAZMANA_X, _
                            TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                            TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                            TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                        'add this new rec to the dataset for the next line.
                                        dr = ds.Tables(0).NewRow
                                        If SRV_D_KNISA_X = "" Then
                                            dr.Item("shat_hatchala") = DatEfes
                                            dt = Nothing
                                            dt = New DataTable
                                            oDal.ClearCommand()
                                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
                                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                            oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtLong", dt)
                                            'dt = GetRowKds("dual", "", "to_char(to_date('" & DatEfes & "' ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes", retsql)
                                            DatEfes = dt.Rows(0).Item("thenextefes").ToString
                                        Else
                                            dr.Item("shat_hatchala") = knisa_2chck
                                        End If
                                        If Not SRV_D_YETZIA_X = "" Then
                                            dr.Item("shat_gmar") = yetzia_2chck
                                        End If
                                        ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                                        FoundAMatch = True
                                        NumLInDS = NumLInDS + 1
                                    End If

                                    'phase3: only out
                                ElseIf SRV_D_KNISA_X = "" And (Not SRV_D_YETZIA_X = "") Then 'only out
                                    If ez4 = 1 Then
                                        '9) complete duplicate
                                        If Not Trim(SRV_D_gmar_letashlum_musach_X) = "" Then
                                            If Not ds.Tables(0).Rows(i).Item("shat_gmar_letashlum_musach").ToString = SRV_D_gmar_letashlum_musach_X Then
                                                'KdsSql2 = 
                                                FoundAchange = False
                                                InpStr = "upd_in_out_letashlum:" & line & ":" & InpStr
                                                upd_in_out_letashlum(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                 SRV_D_KNISA_X, SRV_D_YETZIA_X, SRV_D_ISHI_MEADKEN, _
                                 SRV_D_KOD_BITUL_ZMAN_NESIA_X, SRV_D_KOD_HALBASHA_X, _
                                 SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HAZMANA_X, _
                                 TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                                 TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                                 TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                                If Not TAARICH_gmar_musach_p24 = "" Then
                                                    dr.Item("shat_gmar_letashlum_musach") = thenextday & SRV_D_gmar_letashlum_musach_X
                                                Else
                                                    dr.Item("shat_gmar_letashlum_musach") = SRV_D_TAARICH & SRV_D_gmar_letashlum_musach_X
                                                End If
                                            End If
                                        End If
                                        FoundAMatch = True
                                        i = NumLInDS - 1
                                    ElseIf ez2 = 1 And ez4 = 0 And Not (CLng(ds.Tables(0).Rows(i).Item("shat_hatchala").ToString) > CLng(yetzia_2chck)) Then
                                        '10) found the line with no out
                                        'KdsSql2 = 
                                        'If calc_D_new_sidur <> 99200 Then
                                        '    pro_upd_yamey = True
                                        'End If
                                        FoundAchange = False
                                        InpStr = "upd_out_blank:" & line & ":" & InpStr
                                        upd_out_blank(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                            Mid(ds.Tables(0).Rows(i).Item("shat_hatchala").ToString, 9, 4), SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                            SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                            SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                            SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                            SRV_D_KOD_HAZMANA_X, _
                            TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                            TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                            TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                        'upd this rec in the dataset.
                                        ds.Tables(0).Rows(i).Item("shat_gmar") = yetzia_2chck
                                        FoundAMatch = True
                                        i = NumLInDS - 1
                                    ElseIf i = NumLInDS - 1 And FoundAMatch = False Then
                                        '11)
                                        'KdsSql2 = 
                                        'If calc_D_new_sidur <> 99200 Then
                                        '    pro_upd_yamey = True
                                        'End If
                                        FoundAchange = False
                                        InpStr = "new_rec:" & line & ":" & InpStr
                                        new_rec(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                            SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                            SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                            SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                            SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                            SRV_D_KOD_HAZMANA_X, _
                            TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                            TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                            TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                        'add this new rec to the dataset for the next line.
                                        dr = ds.Tables(0).NewRow
                                        If SRV_D_KNISA_X = "" Then
                                            dr.Item("shat_hatchala") = DatEfes
                                            dt = Nothing
                                            dt = New DataTable
                                            oDal.ClearCommand()
                                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
                                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                            oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtLong", dt)
                                            'dt = GetRowKds("dual", "", "to_char(to_date('" & DatEfes & "' ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes", retsql)
                                            DatEfes = dt.Rows(0).Item("thenextefes").ToString
                                        Else
                                            dr.Item("shat_hatchala") = knisa_2chck
                                        End If
                                        If Not SRV_D_YETZIA_X = "" Then
                                            dr.Item("shat_gmar") = yetzia_2chck
                                        End If
                                        ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                                        FoundAMatch = True
                                        NumLInDS = NumLInDS + 1
                                    End If

                                    'phase4: no in no out
                                ElseIf SRV_D_KNISA_X = "" And SRV_D_YETZIA_X = "" Then 'no in & no out
                                    If ez1 = 1 And ez2 = 1 And ez3 = 1 And ez4 = 1 Then
                                        'complete duplicate
                                        If Not (Trim(SRV_D_KNISA_letashlum_musach_X) = "" Or Trim(SRV_D_gmar_letashlum_musach_X) = "") Then
                                            'impossible4: no in and no out but letashlum???
                                            If SwIsOpen = False Then
                                                sw = New StreamWriter(ErrFileName, False)
                                                SwIsOpen = True
                                            End If
                                            sw.WriteLine("impossible5 " & line)
                                            'KdsWriteProcessLog(2, 1, 4, "sql " & Trim(KdsSql))
                                        End If
                                        FoundAMatch = True
                                        i = NumLInDS - 1
                                    End If
                                End If

                                'phase5: no match, insert!
                                If FoundAMatch = False And i = NumLInDS - 1 Then
                                    If KeepIin > 0 Then
                                        'dt = Nothing
                                        'dt = New DataTable
                                        'oDal.ClearCommand()
                                        'oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, "19540228" & SRV_D_KNISA_X, ParameterDir.pdInput)
                                        'oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                        'oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtVeryLong", dt)
                                        ''dt = GetRowKds("dual", "", "to_char(to_date('19540228" & SRV_D_KNISA_X & "' ,'yyyymmddhh24mi')+(1/86400),'yyyymmddhh24miss') thenextdt", retsql)
                                        oDal.ClearCommand()
                                        If Times_knisa_p24 > 0 Then
                                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, thenextday, ParameterDir.pdInput)
                                            oDal.AddParameter("phatchala", ParameterType.ntOracleVarchar, thenextday & SRV_D_KNISA_X, ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                            oDal.AddParameter("phatchala", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
                                        End If
                                        oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                                        oDal.AddParameter("psidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
                                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                        dt = Nothing
                                        dt = New DataTable
                                        oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtVeryLong2", dt)
                                        thenextdt = dt.Rows(0).Item("thenextdt").ToString
                                        'KdsSql2 = 
                                        'If calc_D_new_sidur <> 99200 Then
                                        '    pro_upd_yamey = True
                                        'End If
                                        FoundAchange = False
                                        InpStr = "new_rec:" & line & ":" & InpStr
                                        new_rec(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                            Mid(thenextdt, 9, 6), SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                                            SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                                            SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                                            SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, _
                                            SRV_D_KOD_HAZMANA_X, _
                        TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                        TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                        TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                    Else
                                        'KdsSql2 = 
                                        'If calc_D_new_sidur <> 99200 Then
                                        '    pro_upd_yamey = True
                                        'End If
                                        FoundAchange = False
                                        InpStr = "new_rec:" & line & ":" & InpStr
                                        new_rec(SRV_D_ISHI, SRV_D_TAARICH, calc_D_new_sidur, _
                                            SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, SRV_D_SIBAT_DIVUACH_KNISA, _
                                            SRV_D_YETZIA_X, SRV_D_MIKUM_YETZIA, SRV_D_SIBAT_DIVUACH_YETZIA, _
                                            SRV_D_ISHI_MEADKEN, SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
                                            SRV_D_KOD_CHARIGA_X, SRV_D_KOD_HALBASHA_X, SRV_D_KOD_HAZMANA_X, _
                        TAARICH_knisa_p24, TAARICH_yetzia_p24, DatEfes, _
                        TAARICH_knisa_musach_p24, SRV_D_KNISA_letashlum_musach_X, _
                        TAARICH_gmar_musach_p24, SRV_D_gmar_letashlum_musach_X)
                                    End If
                                    'add this new rec to the dataset for the next line.
                                    dr = ds.Tables(0).NewRow
                                    If SRV_D_KNISA_X = "" Then
                                        dr.Item("shat_hatchala") = DatEfes
                                        dt = Nothing
                                        dt = New DataTable
                                        oDal.ClearCommand()
                                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
                                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                        oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtLong", dt)
                                        'dt = GetRowKds("dual", "", "to_char(to_date('" & DatEfes & "' ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes", retsql)
                                        DatEfes = dt.Rows(0).Item("thenextefes").ToString
                                    Else
                                        dr.Item("shat_hatchala") = knisa_2chck
                                    End If
                                    If Not SRV_D_YETZIA_X = "" Then
                                        dr.Item("shat_gmar") = yetzia_2chck
                                    End If
                                    ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                                    FoundAMatch = True
                                    NumLInDS = NumLInDS + 1
                                End If

                                'phase6: match, and no update except chariga halbasha nesiot lo_letashlum
                                If FoundAMatch = True And FoundAchange = True Then
                                    ifUpdChariga = False
                                    ifUpdhalbasha = False
                                    ifUpdhashlama = False
                                    ifUpdletashlum = False
                                    ifUpdnesiot = False
                                    p_letashlum = 2
                                    dt = New DataTable
                                    oDal.ClearCommand()
                                    oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
                                    If SRV_D_KNISA_X = "" Then
                                        oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, "0000", ParameterDir.pdInput)
                                    Else
                                        oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, SRV_D_KNISA_X, ParameterDir.pdInput)
                                    End If
                                    If SRV_D_YETZIA_X = "" Then
                                        oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, "0000", ParameterDir.pdInput)
                                    Else
                                        oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, SRV_D_YETZIA_X, ParameterDir.pdInput)
                                    End If
                                    'TAARICH_knisa_musach_p24 knisa & yetzia
                                    If Trim(TAARICH_knisa_p24) = "" Then
                                        oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                                    Else
                                        oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                                    End If
                                    If Trim(TAARICH_yetzia_p24) = "" Then
                                        oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                                    Else
                                        oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                                    End If
                                    oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                    oDal.ExecuteSP("Pkg_clock.pro_checkCharigaEtc", dt)

                                    '20120822: if there are no lines (clKdsline There is no row at position 0.)
                                    If dt.Rows.Count > 0 Then
                                        check_chariga = dt.Rows(0).Item("chariga").ToString
                                        check_lo_letashlum = dt.Rows(0).Item("lo_letashlum").ToString
                                        check_nesiot = dt.Rows(0).Item("mezake_nesiot").ToString
                                        check_halbasha = dt.Rows(0).Item("mezake_halbasha").ToString
                                        If Not (Trim(SRV_D_KOD_CHARIGA_X) = "") Then
                                            If Not (Trim(check_chariga) = Trim(SRV_D_KOD_CHARIGA_X)) Then
                                                ifUpdChariga = True
                                            End If
                                        End If
                                        If Not (Trim(SRV_D_KOD_HALBASHA_X) = "") Then
                                            If Not (Trim(check_halbasha) = Trim(SRV_D_KOD_HALBASHA_X)) Then
                                                ifUpdhalbasha = True
                                            End If
                                        End If
                                        If Not (Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X) = "") Then
                                            If Not (Trim(check_nesiot) = Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X)) Then
                                                ifUpdnesiot = True
                                            End If
                                        End If
                                        If SRV_D_KOD_HAZMANA_X = "7" And (Not Trim(check_lo_letashlum) = "1") Then
                                            ifUpdletashlum = True
                                            p_letashlum = 1
                                        ElseIf SRV_D_KOD_HAZMANA_X = "0" And Trim(check_lo_letashlum) = "1" And Not (calc_D_new_sidur = 99200) Then
                                            ifUpdletashlum = True
                                            p_letashlum = 0
                                            ' hazmana = 0 and lo_letashlum null is the same.
                                        End If
                                        'Else
                                        'todo: check if it is ok to goon
                                    End If

                                    If ifUpdletashlum = True Or ifUpdnesiot = True Or ifUpdhalbasha = True Or ifUpdChariga = True Then
                                        'insert into trail before update
                                        oDal.ClearCommand()
                                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                        If SRV_D_KNISA_X = "" Then
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, "0000", ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, SRV_D_KNISA_X, ParameterDir.pdInput)
                                        End If
                                        oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
                                        oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
                                        'TAARICH_knisa_musach_p24 only knisa 
                                        If Trim(TAARICH_knisa_p24) = "" Then
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                                        End If
                                        oDal.ExecuteSP("Pkg_Clock.InsIntoTrail")
                                    End If

                                    If ifUpdletashlum = True Then
                                        'update lo_letashlum
                                        oDal.ClearCommand()
                                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                        If SRV_D_KNISA_X = "" Then
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, "0000", ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, SRV_D_KNISA_X, ParameterDir.pdInput)
                                        End If
                                        oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
                                        oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
                                        oDal.AddParameter("p_letashlum", ParameterType.ntOracleInteger, p_letashlum, ParameterDir.pdInput)
                                        oDal.AddParameter("menahel", ParameterType.ntOracleVarchar, SRV_D_ISHI_MEADKEN, ParameterDir.pdInput)
                                        'TAARICH_knisa_musach_p24 only knisa 
                                        If Trim(TAARICH_knisa_p24) = "" Then
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                                        End If
                                        oDal.ExecuteSP("Pkg_Clock.pro_Updletashlum")
                                        If Not (p_letashlum = 0 Or p_letashlum = 1) Then
                                            'todo: send an error to waiman
                                        End If
                                    End If
                                    If ifUpdChariga = True Then
                                        'update chariga
                                        oDal.ClearCommand()
                                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                        If SRV_D_KNISA_X = "" Then
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, "0000", ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, SRV_D_KNISA_X, ParameterDir.pdInput)
                                        End If
                                        oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
                                        oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
                                        oDal.AddParameter("p_chariga", ParameterType.ntOracleVarchar, SRV_D_KOD_CHARIGA_X, ParameterDir.pdInput)
                                        oDal.AddParameter("menahel", ParameterType.ntOracleVarchar, SRV_D_ISHI_MEADKEN, ParameterDir.pdInput)
                                        'TAARICH_knisa_musach_p24 only knisa 
                                        If Trim(TAARICH_knisa_p24) = "" Then
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                                        End If
                                        oDal.ExecuteSP("Pkg_Clock.pro_Updchariga")
                                    End If
                                    If ifUpdnesiot = True Then
                                        'update nesiot
                                        oDal.ClearCommand()
                                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                        If SRV_D_KNISA_X = "" Then
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, "0000", ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, SRV_D_KNISA_X, ParameterDir.pdInput)
                                        End If
                                        oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
                                        oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
                                        oDal.AddParameter("p_nesiot", ParameterType.ntOracleVarchar, SRV_D_KOD_BITUL_ZMAN_NESIA_X, ParameterDir.pdInput)
                                        oDal.AddParameter("menahel", ParameterType.ntOracleVarchar, SRV_D_ISHI_MEADKEN, ParameterDir.pdInput)
                                        'TAARICH_knisa_musach_p24 only knisa 
                                        If Trim(TAARICH_knisa_p24) = "" Then
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                                        End If
                                        oDal.ExecuteSP("Pkg_Clock.pro_Updnesiot")
                                    End If
                                    If ifUpdhalbasha = True Then
                                        'update halbasha
                                        oDal.ClearCommand()
                                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                                        If SRV_D_KNISA_X = "" Then
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, "0000", ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("p_KNISA", ParameterType.ntOracleVarchar, SRV_D_KNISA_X, ParameterDir.pdInput)
                                        End If
                                        oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
                                        oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
                                        oDal.AddParameter("p_halbasha", ParameterType.ntOracleVarchar, SRV_D_KOD_HALBASHA_X, ParameterDir.pdInput)
                                        oDal.AddParameter("menahel", ParameterType.ntOracleVarchar, SRV_D_ISHI_MEADKEN, ParameterDir.pdInput)
                                        'TAARICH_knisa_musach_p24 only knisa 
                                        If Trim(TAARICH_knisa_p24) = "" Then
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                                        Else
                                            oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                                        End If
                                        oDal.ExecuteSP("Pkg_Clock.pro_Updhalbasha")
                                    End If


                                End If

                            Next
                        End If


                        'If KdsSql2 <> "" Then
                        '    KdsSql = KdsSql & KdsSql2
                        '    KdsSql2 = ""
                        'End If
                    End If
                Catch ex As Exception
                    oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "clKdsline " & ex.Message, 6)
                    ''**KdsWriteProcessLog(2, 1, 3, "clKdsline " & ex.Message, "6")
                    If SwIsOpen = False Then
                        sw = New StreamWriter(ErrFileName, False)
                        SwIsOpen = True
                    End If
                    sw.WriteLine("sql " & InpStr)
                    'do not throw so the next line and/or the next file will be read
                    'Throw ex
                End Try
                line = sr.ReadLine
            End While

        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "clKds " & ex.Message, 3)
            ''**KdsWriteProcessLog(2, 1, 3, "clKds " & ex.Message, "3")
            If SwIsOpen = False Then
                sw = New StreamWriter(ErrFileName, False)
                SwIsOpen = True
            End If
            sw.WriteLine("sql " & InpStr)
            'do not throw so the next line and/or the next file will be read
            'Throw ex
        Finally
            'BaamFunctions.Release(oDb_Kds)
            sr.Close()
            If SwIsOpen = True Then
                'todo:sendmail
                'if need status=9 with errfile?
                oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "clKds " & Mid(InFileName, 9, 5) & " " & ErrFileName, 3)
                ''** KdsWriteProcessLog(2, 1, 3, "clKds " & Mid(InFileName, 9, 5) & " " & ErrFileName, "3")
                sw.Close()
                Throw New Exception("Failure in reading file :" & InFileName)
            End If
        End Try

    End Sub
    Private Sub new_rec(ByVal SRV_D_ISHI, ByVal SRV_D_TAARICH, ByVal calc_D_new_sidur, _
     ByVal SRV_D_KNISA_X, ByVal SRV_D_MIKUM_KNISA, ByVal SRV_D_SIBAT_DIVUACH_KNISA, _
     ByVal SRV_D_YETZIA_X, ByVal SRV_D_MIKUM_YETZIA, ByVal SRV_D_SIBAT_DIVUACH_YETZIA, _
     ByVal SRV_D_ISHI_MEADKEN, ByVal SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
     ByVal SRV_D_KOD_CHARIGA_X, ByVal SRV_D_KOD_HALBASHA_X, ByVal SRV_D_KOD_HAZMANA_X, _
     ByVal TAARICH_knisa_p24, ByVal TAARICH_yetzia_p24, ByVal DatEfes, _
     ByVal TAARICH_knisa_musach_p24, ByVal SRV_D_KNISA_letashlum_musach_X, _
     ByVal TAARICH_gmar_musach_p24, ByVal SRV_D_gmar_letashlum_musach_X)
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        'ByVal SRV_D_KOD_HAMARA_X, ByVal SRV_D_KOD_MICHUTZ_LAMICHSA_X, ByVal SRV_D_KOD_LINA_X, ByVal SRV_D_KOD_PITZUL_X, _

        'Dim KdsSql As String
        Dim oDal As clDal

        Try

            'todo:check if times_knisa_p24 should send thenextday
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
            If Trim(SRV_D_KNISA_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
            End If
            oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_SIBAT_DIVUACH_KNISA", ParameterType.ntOracleVarchar, SRV_D_SIBAT_DIVUACH_KNISA, ParameterDir.pdInput)
            If SRV_D_YETZIA_X = "" Then
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_YETZIA_X, ParameterDir.pdInput)
            End If
            oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_MIKUM_YETZIA), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_SIBAT_DIVUACH_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_SIBAT_DIVUACH_YETZIA), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_ISHI_MEADKEN", ParameterType.ntOracleVarchar, Trim(SRV_D_ISHI_MEADKEN), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_BITUL_ZMAN_NESIA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_CHARIGA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_CHARIGA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HALBASHA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HALBASHA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HAZMANA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HAZMANA_X), ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_yetzia_p24) = "" Then
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.AddParameter("DatEfes", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_KNISA_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_gmar_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_gmar_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_gmar_letashlum_musach_X, ParameterDir.pdInput)
            End If
            oDal.ExecuteSP("Pkg_clock.pro_new_rec")

            oDal.ClearCommand()
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If calc_D_new_sidur = 99200 Then
                'oDal.ClearCommand()
                'oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                'oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                'If Trim(TAARICH_knisa_p24) = "" Then
                '    oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                'Else
                '    oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                'End If
                oDal.ExecuteSP("PKG_BATCH.pro_lo_letashlum")
                'Else
                '    'oDal.ClearCommand()
                '    'oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                '    'oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                '    'If Trim(TAARICH_knisa_p24) = "" Then
                '    '    oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                '    'Else
                '    '    oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                '    'End If
                ' 2010/06/29 measher_o_mistayeg only after sadran
                '    oDal.ExecuteSP("PKG_BATCH.pro_measher_o_mistayeg")
            End If

        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "new_rec " & ex.Message, 3)
            ''** KdsWriteProcessLog(2, 1, 3, "new_rec " & ex.Message, "3")
            Throw ex
        End Try


    End Sub
    Private Sub upd_out_blank(ByVal SRV_D_ISHI, ByVal SRV_D_TAARICH, ByVal calc_D_new_sidur, _
    ByVal SRV_D_KNISA_X, ByVal SRV_D_MIKUM_KNISA, ByVal SRV_D_SIBAT_DIVUACH_KNISA, _
    ByVal SRV_D_YETZIA_X, ByVal SRV_D_MIKUM_YETZIA, ByVal SRV_D_SIBAT_DIVUACH_YETZIA, _
    ByVal SRV_D_ISHI_MEADKEN, ByVal SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
    ByVal SRV_D_KOD_CHARIGA_X, ByVal SRV_D_KOD_HALBASHA_X, ByVal SRV_D_KOD_HAZMANA_X, _
     ByVal TAARICH_knisa_p24, ByVal TAARICH_yetzia_p24, ByVal DatEfes, _
     ByVal TAARICH_knisa_musach_p24, ByVal SRV_D_KNISA_letashlum_musach_X, _
     ByVal TAARICH_gmar_musach_p24, ByVal SRV_D_gmar_letashlum_musach_X)
        'ByVal SRV_D_KOD_HAMARA_X, ByVal SRV_D_KOD_MICHUTZ_LAMICHSA_X, ByVal SRV_D_KOD_LINA_X, ByVal SRV_D_KOD_PITZUL_X, _

        'Dim KdsSql As String
        Dim oDal As clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            ' insert into trail before update
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("pDt_N_KNISA", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                oDal.AddParameter("P24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("P24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.ExecuteSP("Pkg_clock.InsIntoTrailKnisa")

            oDal.ClearCommand()
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
            If Trim(SRV_D_KNISA_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
            End If
            oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_SIBAT_DIVUACH_KNISA", ParameterType.ntOracleVarchar, SRV_D_SIBAT_DIVUACH_KNISA, ParameterDir.pdInput)
            If SRV_D_YETZIA_X = "" Then
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, "", ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_YETZIA_X, ParameterDir.pdInput)
            End If
            oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_MIKUM_YETZIA), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_SIBAT_DIVUACH_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_SIBAT_DIVUACH_YETZIA), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_ISHI_MEADKEN", ParameterType.ntOracleVarchar, Trim(SRV_D_ISHI_MEADKEN), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_BITUL_ZMAN_NESIA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_CHARIGA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_CHARIGA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HALBASHA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HALBASHA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HAZMANA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HAZMANA_X), ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                '20151015 to correct bug if in tow the in is also after midnight and before 04:00
                If calc_D_new_sidur = 99220 And CInt(SRV_D_KNISA_X) > 0 And CInt(SRV_D_KNISA_X) < 400 Then
                    oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                Else
                    oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
                End If
            Else
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_yetzia_p24) = "" Then
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.AddParameter("DatEfes", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_KNISA_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_gmar_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_gmar_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_gmar_letashlum_musach_X, ParameterDir.pdInput)
            End If
            'not in use:mikum_knisa,sibat_knisa,p24_yetzia,dateefes,p24_letashlum_knisa,p24_letashlum_yetzia
            oDal.ExecuteSP("Pkg_clock.pro_upd_out_blank")

            'If calc_D_new_sidur <> 99200 Then
            '    oDal.ClearCommand()
            '    oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            '    oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            '    If Trim(TAARICH_knisa_p24) = "" Then
            '        oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            '    Else
            '        oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            '    End If
            ' 2010/06/29 measher_o_mistayeg only after sadran
            '    oDal.ExecuteSP("PKG_BATCH.pro_measher_o_mistayeg")
            'End If

            'Return KdsSql
        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "upd_out_blank " & ex.Message, 3)
            ''**KdsWriteProcessLog(2, 1, 3, "upd_out_blank " & ex.Message, "3")
            Throw ex
        End Try

    End Sub
    Private Sub upd_in_blank(ByVal SRV_D_ISHI, ByVal SRV_D_TAARICH, ByVal calc_D_new_sidur, _
     ByVal SRV_D_KNISA_X, ByVal SRV_D_MIKUM_KNISA, ByVal SRV_D_SIBAT_DIVUACH_KNISA, _
     ByVal SRV_D_YETZIA_X, ByVal SRV_D_MIKUM_YETZIA, ByVal SRV_D_SIBAT_DIVUACH_YETZIA, _
     ByVal SRV_D_ISHI_MEADKEN, ByVal SRV_D_KOD_BITUL_ZMAN_NESIA_X, _
     ByVal SRV_D_KOD_CHARIGA_X, ByVal SRV_D_KOD_HALBASHA_X, ByVal SRV_D_KOD_HAZMANA_X, _
     ByVal TAARICH_knisa_p24, ByVal TAARICH_yetzia_p24, ByVal DatEfes, _
     ByVal TAARICH_knisa_musach_p24, ByVal SRV_D_KNISA_letashlum_musach_X, _
     ByVal TAARICH_gmar_musach_p24, ByVal SRV_D_gmar_letashlum_musach_X)

        'Dim KdsSql As String
        Dim oDal As clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            'insert into trail before update
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("pDt_N_YETZIA", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_YETZIA_X, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
            If Trim(TAARICH_yetzia_p24) = "" Then
                oDal.AddParameter("P24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("P24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.ExecuteSP("PKG_clock.InsIntoTrailYetzia")

            oDal.ClearCommand()
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
            If Trim(SRV_D_KNISA_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
            End If
            oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_SIBAT_DIVUACH_KNISA", ParameterType.ntOracleVarchar, SRV_D_SIBAT_DIVUACH_KNISA, ParameterDir.pdInput)
            If SRV_D_YETZIA_X = "" Then
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_YETZIA_X, ParameterDir.pdInput)
            End If
            oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_MIKUM_YETZIA), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_SIBAT_DIVUACH_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_SIBAT_DIVUACH_YETZIA), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_ISHI_MEADKEN", ParameterType.ntOracleVarchar, Trim(SRV_D_ISHI_MEADKEN), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_BITUL_ZMAN_NESIA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_CHARIGA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_CHARIGA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HALBASHA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HALBASHA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HAZMANA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HAZMANA_X), ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_yetzia_p24) = "" Then
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.AddParameter("DatEfes", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_KNISA_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_gmar_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_gmar_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_gmar_letashlum_musach_X, ParameterDir.pdInput)
            End If
            oDal.ExecuteSP("Pkg_clock.pro_upd_in_blank")

            'If calc_D_new_sidur <> 99200 Then
            '    oDal.ClearCommand()
            '    oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            '    oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            '    If Trim(TAARICH_knisa_p24) = "" Then
            '        oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            '    Else
            '        oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            '    End If
            ' 2010/06/29 measher_o_mistayeg only after sadran
            '    oDal.ExecuteSP("PKG_BATCH.pro_measher_o_mistayeg")
            'End If

            'Return KdsSql
        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "upd_in_blank " & ex.Message, 3)
            ''** KdsWriteProcessLog(2, 1, 3, "upd_in_blank " & ex.Message, "3")
            Throw ex
        End Try



    End Sub
    Private Sub upd_in_out_letashlum(ByVal SRV_D_ISHI, ByVal SRV_D_TAARICH, ByVal calc_D_new_sidur, _
   ByVal SRV_D_KNISA_X, ByVal SRV_D_YETZIA_X, ByVal SRV_D_ISHI_MEADKEN, _
   ByVal SRV_D_KOD_BITUL_ZMAN_NESIA_X, ByVal SRV_D_KOD_HALBASHA_X, _
   ByVal SRV_D_KOD_CHARIGA_X, ByVal SRV_D_KOD_HAZMANA_X, _
   ByVal TAARICH_knisa_p24, ByVal TAARICH_yetzia_p24, ByVal DatEfes, _
   ByVal TAARICH_knisa_musach_p24, ByVal SRV_D_KNISA_letashlum_musach_X, _
   ByVal TAARICH_gmar_musach_p24, ByVal SRV_D_gmar_letashlum_musach_X)


        'Dim KdsSql As String
        Dim oDal As clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            'insert into trail before update
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("pDt_N_KNISA", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleInteger, calc_D_new_sidur, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                oDal.AddParameter("P24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("P24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.ExecuteSP("Pkg_clock.InsIntoTrailKnisa")

            oDal.ClearCommand()
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
            If Trim(SRV_D_KNISA_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
            End If
            'oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
            'oDal.AddParameter("SRV_D_SIBAT_DIVUACH_KNISA", ParameterType.ntOracleVarchar, SRV_D_SIBAT_DIVUACH_KNISA, ParameterDir.pdInput)
            If SRV_D_YETZIA_X = "" Then
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_YETZIA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_YETZIA_X, ParameterDir.pdInput)
            End If
            'oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_MIKUM_YETZIA), ParameterDir.pdInput)
            'oDal.AddParameter("SRV_D_SIBAT_DIVUACH_YETZIA", ParameterType.ntOracleVarchar, Trim(SRV_D_SIBAT_DIVUACH_YETZIA), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_ISHI_MEADKEN", ParameterType.ntOracleVarchar, Trim(SRV_D_ISHI_MEADKEN), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_BITUL_ZMAN_NESIA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HALBASHA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HALBASHA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_CHARIGA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_CHARIGA_X), ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KOD_HAZMANA_X", ParameterType.ntOracleVarchar, Trim(SRV_D_KOD_HAZMANA_X), ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_yetzia_p24) = "" Then
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_yetzia_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.AddParameter("DatEfes", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_KNISA_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_KNISA_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_letashlum_musach_X, ParameterDir.pdInput)
            End If
            If Trim(TAARICH_gmar_musach_p24) = "" Then
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_gmar_musach_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            If Trim(SRV_D_gmar_letashlum_musach_X) = "" Then
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, Nothing, ParameterDir.pdInput)
            Else
                oDal.AddParameter("SRV_D_gmar_letashlum_musach_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_gmar_letashlum_musach_X, ParameterDir.pdInput)
            End If
            oDal.ExecuteSP("Pkg_clock.pro_upd_in_out_letashlum")


        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "upd_in_out_letashlum " & ex.Message, 3)
            ''**KdsWriteProcessLog(2, 1, 3, "upd_in_out_letashlum " & ex.Message, "3")
            Throw ex
        End Try

    End Sub

    Public Sub LoadPundakim(ByVal InLine)

        Dim oDal As clDal
        Dim dt As DataTable
        Dim ds As DataSet
        Dim dr As DataRow
        Dim SRV_D_ISHI As String
        Dim SRV_D_TAARICH As String
        Dim SRV_D_KNISA_X As String
        Dim SRV_D_MIKUM_KNISA As String
        Dim TAARICH_knisa_p24 As String
        Dim Times_knisa_p24 As Integer
        Dim efes As String
        Dim SQ99_COUNTER As Integer
        Dim WS99_EZER As Integer
        Dim ez1 As Integer
        Dim ez2 As Integer
        Dim ez3 As Integer
        Dim ez4 As Integer
        Dim NumLInDS As Integer
        Dim thenextday As String
        Dim thenextdt As String
        Dim DatEfes As String
        Dim InpStr As String
        Dim FoundAMatch As Boolean
        Dim KeepIin As Integer
        Dim knisa_2chck As String
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch

        Try
            efes = "0000000000"
            SRV_D_ISHI = Mid(InLine, 1, 5)
            SRV_D_TAARICH = Mid(InLine, 7, 8) 'format=yyyymmdd
            oDal = New clDal

            If Mid(InLine, 20, 4) = "0000" Then
                SRV_D_KNISA_X = ""
            Else
                SRV_D_KNISA_X = Mid(InLine, 20, 4) 'format=hhmm
            End If
            TAARICH_knisa_p24 = ""
            Times_knisa_p24 = 0
            If Not Trim(Mid(SRV_D_KNISA_X, 1, 2)) = "" Then
                If CInt(Mid(SRV_D_KNISA_X, 1, 2)) >= 24 Then
                    'in case hour is=>24
                    Times_knisa_p24 = CInt(Abs(CInt(Mid(SRV_D_KNISA_X, 1, 2)) / 24))
                    SRV_D_KNISA_X = (CInt(SRV_D_KNISA_X) - Times_knisa_p24 * 2400).ToString
                    TAARICH_knisa_p24 = "+" & Times_knisa_p24.ToString
                    If Len(SRV_D_KNISA_X) < 4 Then
                        SRV_D_KNISA_X = Left(efes, 4 - Len(SRV_D_KNISA_X)) & SRV_D_KNISA_X
                    End If
                End If
            End If
            If Not (Mid(InLine, 32, 3) = "000" Or Trim(Mid(InLine, 32, 3)) = "") And Trim(Mid(InLine, 95, 2)) = "" Then
                SRV_D_MIKUM_KNISA = Mid(InLine, 32, 3) & "00"
            ElseIf ((Mid(InLine, 32, 3) = "000" Or Trim(Mid(InLine, 32, 3)) = "")) Then
                SRV_D_MIKUM_KNISA = Mid(InLine, 32, 3) & "00"
            Else
                SRV_D_MIKUM_KNISA = Mid(InLine, 32, 3) & Mid(InLine, 95, 2)
            End If


            SQ99_COUNTER = 0
            WS99_EZER = 0
            ez1 = 0
            ez2 = 0
            ez3 = 0
            ez4 = 0
            DatEfes = "00010101"
            InpStr = SRV_D_ISHI & "," & SRV_D_TAARICH & ",99214,"
            InpStr = InpStr & SRV_D_KNISA_X & "," & SRV_D_MIKUM_KNISA
            InpStr = InpStr & "," & TAARICH_knisa_p24 & "," & DatEfes
            'find the day after
            dt = Nothing
            dt = New DataTable
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("PKG_BATCH.pro_GetRowDt", dt)
            thenextday = dt.Rows(0).Item("thenextday").ToString
            ds = Nothing
            ds = New DataSet
            oDal.ClearCommand()
            '20120723 If Times_knisa_p24 > 0 Then
            '20120723 oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, thenextday, ParameterDir.pdInput)
            '20120723 Else
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            '20120723 End If
            oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("PKG_BATCH.pro_GetListDsPundakim", ds)
            NumLInDS = ds.Tables(0).Rows.Count

            FoundAMatch = False
            If NumLInDS = 0 Then
                '1) this if is due to my estimation that 50% of the data is a solo insert.
                'this is a new record, nothing to be checked.
                InpStr = "new_rec pundakim:" & InpStr
                new_rec_pundakim(SRV_D_ISHI, SRV_D_TAARICH, _
                                 SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, TAARICH_knisa_p24)
                'todo: send datefes for new_rec
                'add this new rec to the dataset for the next line.
                dr = ds.Tables(0).NewRow
                If SRV_D_KNISA_X = "" Then
                    dr.Item("shat_hityazvut") = DatEfes
                    dt = Nothing
                    dt = New DataTable
                    oDal.ClearCommand()
                    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, DatEfes, ParameterDir.pdInput)
                    oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                    oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtLong", dt)
                    'dt = GetRowKds("dual", "", "to_char(to_date('" & DatEfes & "' ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes", retsql)
                    DatEfes = dt.Rows(0).Item("thenextefes").ToString
                    'todo:mail how come hityazvut without time?
                Else
                    If Times_knisa_p24 > 0 Then
                        dr.Item("shat_hityazvut") = thenextday & SRV_D_KNISA_X
                    Else
                        dr.Item("shat_hityazvut") = SRV_D_TAARICH & SRV_D_KNISA_X
                    End If
                End If
                ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                NumLInDS = NumLInDS + 1
                FoundAMatch = True
            Else
                'here comes trouble...
                KeepIin = 0
                For i = 0 To NumLInDS - 1
                    ez1 = 0
                    ez2 = 1
                    ez3 = 0
                    ez4 = 1
                    If Times_knisa_p24 > 0 Then
                        knisa_2chck = thenextday & SRV_D_KNISA_X
                        If ds.Tables(0).Rows(i).Item("shat_hityazvut").ToString = thenextday & SRV_D_KNISA_X Then
                            ez3 = 1
                            KeepIin = i + 1
                        End If
                    Else
                        knisa_2chck = SRV_D_TAARICH & SRV_D_KNISA_X
                        If ds.Tables(0).Rows(i).Item("shat_hityazvut").ToString = SRV_D_TAARICH & SRV_D_KNISA_X Then
                            ez3 = 1
                            KeepIin = i + 1
                        End If
                    End If
                    'since ez1 = 0 (knisa is not null), ez2=1 (yetzia is null),
                    '      ez4 = 0 (shat_gmar is null = yetzia),
                    '      ez3 = 0 or 1(knisa <>/= shat_hatchala) 
                    '      the only alternatives are: 0101, 0111

                    'phase1: only in
                    If Not SRV_D_KNISA_X = "" Then 'And SRV_D_YETZIA_X = "" Then 'only in
                        If ez3 = 1 Then
                            '2) duplicate - exit
                            FoundAMatch = True
                            i = NumLInDS - 1
                        End If
                    ElseIf i = NumLInDS - 1 And FoundAMatch = False Then
                        If KeepIin > 0 And Not KeepIin = i Then
                            '4) there is a duplicate in the in
                            dt = Nothing
                            dt = New DataTable
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, "19540228" & SRV_D_KNISA_X, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtVeryLong", dt)
                            'dt = GetRowKds("dual", "", "to_char(to_date('19540228" & SRV_D_KNISA_X & "' ,'yyyymmddhh24mi')+(1/86400),'yyyymmddhh24miss') thenextdt", retsql)
                            thenextdt = dt.Rows(0).Item("thenextdt").ToString
                            'todo:verylong2
                            'dt = Nothing
                            'dt = New DataTable
                            'oDal.ClearCommand()
                            'If Times_knisa_p24 > 0 Then
                            '    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, thenextday, ParameterDir.pdInput)
                            '    oDal.AddParameter("phatchala", ParameterType.ntOracleVarchar, thenextday & SRV_D_KNISA_X, ParameterDir.pdInput)
                            'Else
                            '    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                            '    oDal.AddParameter("phatchala", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
                            'End If
                            'oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                            'oDal.AddParameter("psidur", ParameterType.ntOracleVarchar, calc_D_new_sidur, ParameterDir.pdInput)
                            'oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            'oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtVeryLong2", dt)
                            'thenextdt = dt.Rows(0).Item("thenextdt").ToString
                            InpStr = "new_rec_pundakim:" & InpStr
                            new_rec_pundakim(SRV_D_ISHI, SRV_D_TAARICH, _
                Mid(thenextdt, 9, 6), SRV_D_MIKUM_KNISA, TAARICH_knisa_p24)
                        Else
                            InpStr = "new_rec_pundakim:" & InpStr
                            new_rec_pundakim(SRV_D_ISHI, SRV_D_TAARICH, _
                SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, TAARICH_knisa_p24)
                        End If
                        'add this new rec to the dataset for the next line.
                        dr = ds.Tables(0).NewRow
                        dr.Item("shat_hityazvut") = knisa_2chck
                        ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                        FoundAMatch = True
                        NumLInDS = NumLInDS + 1
                    End If

                    'phase5: no match, insert!
                    If FoundAMatch = False And i = NumLInDS - 1 Then
                        If KeepIin > 0 Then
                            dt = Nothing
                            dt = New DataTable
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, "19540228" & SRV_D_KNISA_X, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("PKG_BATCH.pro_GetRowDtVeryLong", dt)
                            'dt = GetRowKds("dual", "", "to_char(to_date('19540228" & SRV_D_KNISA_X & "' ,'yyyymmddhh24mi')+(1/86400),'yyyymmddhh24miss') thenextdt", retsql)
                            thenextdt = dt.Rows(0).Item("thenextdt").ToString
                            InpStr = "new_rec pundakim:" & InpStr
                            new_rec_pundakim(SRV_D_ISHI, SRV_D_TAARICH, _
                                Mid(thenextdt, 9, 6), SRV_D_MIKUM_KNISA, TAARICH_knisa_p24)
                        Else
                            InpStr = "new_rec pundakim:" & InpStr
                            new_rec_pundakim(SRV_D_ISHI, SRV_D_TAARICH, _
                                SRV_D_KNISA_X, SRV_D_MIKUM_KNISA, TAARICH_knisa_p24)
                        End If
                        'add this new rec to the dataset for the next line.
                        dr = ds.Tables(0).NewRow
                        dr.Item("shat_hityazvut") = knisa_2chck
                        ds.Tables(0).Rows.InsertAt(dr, NumLInDS)
                        FoundAMatch = True
                        NumLInDS = NumLInDS + 1
                    End If
                Next

            End If

        Catch ex As Exception
            oBatch.InsertProcessLog(1, 1, KdsLibrary.BL.RecordStatus.Faild, "LoadPundakim " & ex.Message, 3)
            ''**KdsWriteProcessLog(1, 1, 3, "LoadPundakim " & ex.Message, "3")
            'Throw ex
        End Try
    End Sub
    Private Sub new_rec_pundakim(ByVal SRV_D_ISHI, ByVal SRV_D_TAARICH, _
      ByVal SRV_D_KNISA_X, ByVal SRV_D_MIKUM_KNISA, ByVal TAARICH_knisa_p24)

        Dim oDal As clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try

            'todo:check if times_knisa_p24 should send thenextday
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_KNISA_X", ParameterType.ntOracleVarchar, SRV_D_TAARICH & SRV_D_KNISA_X, ParameterDir.pdInput)
            oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
            If Trim(TAARICH_knisa_p24) = "" Then
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 0, ParameterDir.pdInput)
            Else
                oDal.AddParameter("TAARICH_knisa_p24", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
            End If
            oDal.ExecuteSP("Pkg_clock.pro_new_rec_pundakim")


        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "new_rec_pundakim " & ex.Message, 3)
            ''**KdsWriteProcessLog(2, 1, 3, "new_rec_pundakim " & ex.Message, "3")
            Throw ex
        End Try

    End Sub
    Public Function change_knisot(ByVal p_dt As String) As DataTable

        'Dim KdsSql As String
        Dim oDal As clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim dt As DataTable
        Try
            'insert into trail before update
            oDal = New clDal
            dt = New DataTable

            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("PKG_sdrn.pro_change_Knisot_sdrm", dt)
            If dt.Rows.Count = 0 Then
                'todo:error or stop
            End If
            Return dt

        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "change_knisot " & ex.Message, 3)
            Throw ex
        End Try
    End Function
    'Public Sub fetch_knisot(ByVal p_dt As String, ByVal dt As DataTable)
    'for every row in dt from pro_change_Knisot_sdrm fetch GetKavDetails
    'End Sub
    Public Sub FetchKnisot(ByVal p_dt As String)

        Dim oDal As clTnua
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim Dt As DataTable
        Dim Ds As DataSet
        Dim Makat As Long
        Dim i As Integer
        Dim rc As Integer
        Dim DtMakat As Date
        Dim TnuaConn As String = ConfigurationSettings.AppSettings("KDS_TNPR_CONNECTION")


        Try
            'p_dt = "20120609"
            Dt = change_knisot(p_dt)
            DtMakat = New DateTime(Mid(p_dt, 1, 4), Mid(p_dt, 5, 2), Mid(p_dt, 7, 2))
            If Dt.Rows.Count > 0 Then
                For i = 0 To Dt.Rows.Count - 1
                    Makat = CInt(Dt.Rows(i).Item("makat_nesia").ToString)
                    If Makat > 0 Then
                        oDal = New clTnua(TnuaConn)
                        Try
                            oDal.ClearCommand()
                            Ds = New DataSet
                            rc = 1 'error=1, ok=0
                            oDal.AddParameter("p_date", ParameterType.ntOracleDate, DtMakat, ParameterDir.pdInput)
                            oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, Makat, ParameterDir.pdInput)
                            oDal.AddParameter("p_rc", ParameterType.ntOracleInteger, DBNull.Value, ParameterDir.pdOutput)
                            oDal.AddParameter("KavDetailsCrs", ParameterType.ntOracleRefCursor, DBNull.Value, ParameterDir.pdOutput)
                            oDal.AddParameter("EntryCrs", ParameterType.ntOracleRefCursor, DBNull.Value, ParameterDir.pdOutput)
                            oDal.AddParameter("VisutimCrs", ParameterType.ntOracleRefCursor, DBNull.Value, ParameterDir.pdOutput)
                            oDal.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput)
                            oDal.ExecuteSP(clGeneral.cGetKavDetails, Ds)
                            rc = CInt(oDal.GetValParam("p_rc").ToString)
                            If rc = 0 Then
                                'todo: call another function with the ds
                                DelNInsKnisot(Ds, p_dt)
                            Else
                                'todo: rc error
                            End If
                        Catch ex As Exception
                            'todo: getkavimdetails aborted
                            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "getkavimdetails aborted " & ex.Message, 3)
                            'Throw ex
                        End Try
                    End If
                Next

            Else
                'todo:error or stop and mail "no knisot for this date"
            End If

        Catch ex As Exception
            'todo: fetch aborted
        End Try

    End Sub
    Public Sub DelNInsKnisot(ByVal Ds As DataSet, ByVal p_dt As String)

        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim oDal As clDal
        Dim Dt As DataTable
        Dim DtPeilut As DataTable
        Dim DtKnisot As DataTable
        Dim PDt As String
        Dim PMisparKnisa As Integer
        Dim PMakat As Integer
        Dim PTeurNesia As String
        Dim DtMakat As Date
        'Dim NKnisot As Integer
        Dim PSugKnisa As Integer

        Try
            Dt = New DataTable
            DtPeilut = New DataTable
            DtKnisot = New DataTable
            DtPeilut = Ds.Tables(0)
            DtKnisot = Ds.Tables(1)
            DtMakat = New DateTime(Mid(p_dt, 1, 4), Mid(p_dt, 5, 2), Mid(p_dt, 7, 2))
            PDt = DtPeilut.Rows(0).Item(0).ToString
            'todo: compare PDt and p_dt 'DtMakat
            PMakat = CInt(DtPeilut.Rows(0).Item(1).ToString)
            'NKnisot = 0
            'oDal = New clDal
            'oDal.ClearCommand()
            'oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
            'oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, PMakat, ParameterDir.pdInput)
            'oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            'oDal.ExecuteSP("PKG_sdrn.pro_Count_Knisot_sdrm", Dt)
            'If Dt.Rows.Count = 0 Then
            '    'todo:error , no original kniot.
            'Else
            '    NKnisot = CInt(Dt.Rows(0).Item(0).ToString)
            'End If

            'If Not NKnisot < DtKnisot.Rows.Count Then
            For i = 0 To DtKnisot.Rows.Count - 1
                PMisparKnisa = CInt(DtKnisot.Rows(i).Item(0).ToString)
                PSugKnisa = CInt(DtKnisot.Rows(i).Item(5).ToString)
                PTeurNesia = DtKnisot.Rows(i).Item(7).ToString

                oDal.ClearCommand()
                oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, PMakat, ParameterDir.pdInput)
                oDal.AddParameter("pMisparKnisa", ParameterType.ntOracleInteger, PMisparKnisa, ParameterDir.pdInput)
                oDal.AddParameter("PSugKnisa", ParameterType.ntOracleInteger, PSugKnisa, ParameterDir.pdInput)
                oDal.AddParameter("PTeurNesia", ParameterType.ntOracleVarchar, PTeurNesia, ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_sdrn.pro_Ins_Knisot_sdrm")
            Next

            'If Not NKnisot = DtKnisot.Rows.Count Then
            '    oDal.ClearCommand()
            '    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
            '    oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, PMakat, ParameterDir.pdInput)
            '    oDal.ExecuteSP("PKG_sdrn.pro_Del_Knisot_sdrm", Dt)
            'End If
            'Else
            'For i = 0 To NKnisot - 1
            '    PMisparKnisa = CInt(DtKnisot.Rows(i).Item(0).ToString)
            '    PSugKnisa = CInt(DtKnisot.Rows(i).Item(5).ToString)
            '    PTeurNesia = DtKnisot.Rows(i).Item(7).ToString
            ''PMisparKnisa = CInt(DtKnisot.Rows(i).Item("Siduri").ToString)
            ''PSugKnisa = CInt(DtKnisot.Rows(i).Item("SugKnisa").ToString)
            ''PTeurNesia = DtKnisot.Rows(i).Item("TeurKnisa").ToString

            '    oDal.ClearCommand()
            '    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
            '    oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, PMakat, ParameterDir.pdInput)
            '    oDal.AddParameter("pMisparKnisa", ParameterType.ntOracleInteger, PMisparKnisa, ParameterDir.pdInput)
            '    oDal.AddParameter("PSugKnisa", ParameterType.ntOracleInteger, PSugKnisa, ParameterDir.pdInput)
            '    oDal.AddParameter("PTeurNesia", ParameterType.ntOracleVarchar, PTeurNesia, ParameterDir.pdInput)
            '    oDal.ExecuteSP("PKG_sdrn.pro_Upd_Knisot_sdrm")
            'Next
            '' insert more knisot than original

        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "DelNInsKnisot aborted " & ex.Message, 3)
        End Try


    End Sub

#End Region

#Region "Sadran"
    Public Sub UpdSdrnFromReplicate(ByVal p_date_str As String, ByVal UserBatch As String)

        Dim oDal As clDal
        'Dim clGenral As KdsLibrary.clGeneral
        'Dim clBatch As KdsBatch.clBatchFactory
        Dim tahalich As Integer
        Dim sub_tahalich As Integer
        Dim lRequestNum As Integer
        Dim dTaarich As Date
        Dim dDatestr As String
        Dim SdrnStatTimes, teur As String
        Dim iNumSeq, iSdrnNumSeq As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            iNumSeq = 0
            iSdrnNumSeq = oBatch.InsertProcessLog(4, 1, KdsLibrary.BL.RecordStatus.Wait, "sdrn", 0)
            oDal = New clDal

            tahalich = 4
            sub_tahalich = 3
            teur = "yamim"
            iNumSeq = oBatch.InsertProcessLog(4, 3, KdsLibrary.BL.RecordStatus.Wait, "yamim", 0)
            ''** KdsWriteProcessLog(4, 3, 1, "start yamim")
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str, ParameterDir.pdInput)
            oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "yamim", 0)
            oDal.ExecuteSP("PKG_sdrn.pro_ins_yamim_4_sidurim")

            sub_tahalich = 4
            teur = "sidurim"
            iNumSeq = oBatch.InsertProcessLog(4, 4, KdsLibrary.BL.RecordStatus.Wait, "sidurim", 0)
            ''**KdsWriteProcessLog(4, 4, 1, "after yamim, before sidurim")
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str, ParameterDir.pdInput)
            oDal.ExecuteSP("PKG_sdrn.pro_ins_sidurim_4_sidurim")

            oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "sidurim", 0)
            ''**KdsWriteProcessLog(4, 5, 1, "after sidurim, before peilut")

            sub_tahalich = 5
            teur = "peilut_4_sidurim"
            oDal.ClearCommand()
            iNumSeq = oBatch.InsertProcessLog(4, 5, KdsLibrary.BL.RecordStatus.Wait, "peilut_4_sidurim", 0)
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str, ParameterDir.pdInput)
            oDal.ExecuteSP("PKG_sdrn.pro_ins_peilut_4_sidurim")
            oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "peilut_4_sidurim", 0)

            sub_tahalich = 6
            teur = "min-max"
            iNumSeq = oBatch.InsertProcessLog(4, 6, KdsLibrary.BL.RecordStatus.Wait, "min-max", 0)
            ''** KdsWriteProcessLog(4, 6, 1, "after peilut, before min-max")
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str, ParameterDir.pdInput)
            oDal.ExecuteSP("PKG_BATCH.pro_upd_yamey_avoda_ovdim")
            oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "min-max", 0)

            sub_tahalich = 7
            teur = "upd_control"
            iNumSeq = oBatch.InsertProcessLog(4, 7, KdsLibrary.BL.RecordStatus.Wait, "upd_control", 0)
            ''**KdsWriteProcessLog(4, 7, 1, "after min-max,before upd_control")
            'todo: shmulik will add an index column and its value will come from web-config
            'todo: how to differ the web-config / app-config ?
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str, ParameterDir.pdInput)
            oDal.ExecuteSP("PKG_sdrn.pro_upd_sdrm_control")
            oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "upd control", 0)
            ''**KdsWriteProcessLog(4, 7, 1, "after upd control")
            If UserBatch = "RunBatch" Then
                teur = "OpenBatchRequest"
                iNumSeq = oBatch.InsertProcessLog(8, 1, KdsLibrary.BL.RecordStatus.Wait, "OpenBatchRequest", 0)
                ''** KdsWriteProcessLog(8, 1, 1, "before OpenBatchRequest")
                'todo: batchDescription= "KdsScheduler",iUserId= 77690 or -12 ?
                lRequestNum = KdsLibrary.clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12)
                'p_date_str = yyyymmdd
                dDatestr = Mid(p_date_str, 7, 2) & "/" & Mid(p_date_str, 5, 2) & "/" & Mid(p_date_str, 1, 4)
                'dTaarich = CDate(dDatestr)
                'dTaarich=new DateTime(year,month,date)
                dTaarich = New DateTime(Mid(p_date_str, 1, 4), Mid(p_date_str, 5, 2), Mid(p_date_str, 7, 2))
                oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "OpenBatchRequest", 0)
                ''**KdsWriteProcessLog(8, 1, 1, "after OpenBatchRequest before shguyim")
                'todo: when after sdrn should 
                teur = "shguyim"
                iNumSeq = oBatch.InsertProcessLog(8, 1, KdsLibrary.BL.RecordStatus.Wait, "shguyim", 0)
                KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcess, BatchExecutionType.All, dTaarich, lRequestNum)
                'todo: when after rfrsh should 
                'KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcessForChangesInHR , KdsBatch.BatchExecutionType.All, dTaarich, lRequestNum)
                'todo: check table tb_log_bakashot for errors etc
                oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "shguyim", 0)
                ''** KdsWriteProcessLog(8, 1, 2, "after shguyim")
                '2011/03/10 no ishurim till...
                '' 2010/07/27               no mail ishurim at all
                'teur = "ishurim"
                'iNumSeq = oBatch.InsertProcessLog(6, 1, KdsLibrary.BL.RecordStatus.Wait, "ishurim", 0)
                ' ''**KdsWriteProcessLog(8, 1, 2, "after shguyim before ishurim")
                'SdrnStatTimes = ConfigurationSettings.AppSettings("SdrnStatTimes") '2=test, 3=prod since 2010/07/08
                '' 2010/07/08               If SdrnStatTimes = "2" Then
                'If SdrnStatTimes = "3" Then
                '    KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(dTaarich, True)
                'Else
                '    KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(dTaarich, False)
                'End If
                'oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Finish, "ishurim", 0)
                ' ''**KdsWriteProcessLog(6, 1, 2, "after ishurim")
            Else
                oBatch.InsertProcessLog(8, 1, KdsLibrary.BL.RecordStatus.PartialFinish, "shguyim not run UserBatch=" & UserBatch, 0)
                ''**KdsWriteProcessLog(8, 1, 4, "shguyim not run UserBatch=" & UserBatch)
            End If
            oBatch.UpdateProcessLog(iSdrnNumSeq, KdsLibrary.BL.RecordStatus.Finish, "sadran", 0)
            ''**KdsWriteProcessLog(4, 1, 2, "end ok sadran")

        Catch ex As Exception
            oBatch.UpdateProcessLog(iNumSeq, KdsLibrary.BL.RecordStatus.Faild, teur & " abort: " & ex.Message, 10)
            oBatch.UpdateProcessLog(iSdrnNumSeq, KdsLibrary.BL.RecordStatus.Faild, "sadran " & ex.Message, 10)
            ''**KdsWriteProcessLog(4, 1, 3, "sadran " & ex.Message, "10")
            Throw ex
        End Try

    End Sub
    Public Sub NoticeStatusSadran()
        Dim Status As String
        Try
            Status = ChkStatusSdrn(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"))

            Select Case Status
                Case "1"
                    Throw New Exception("Status of Sadran : the procedure CreateActivitiesAtSdrn didn't finish properly.")
                Case "6"
                    Throw New Exception("Status of Sadran : the procedure PKG_sdrn.pro_GetStatusSdrn was aborted")
                Case "7"
                    Throw New Exception("Status of Sadran : the record exists but there was a problem in Sadran procedure in Tnua")
                Case "8"
                    Throw New Exception("Status of Sadran : the record does not exist/wasn't created by PKG_sdrn.pro_GetStatusSdrn")
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ChkStatusSdrn(ByVal p_date_str As String) As String

        Dim oDal As clDal
        Dim dt2 As DataTable = New DataTable()
        Dim p_status As Integer

        p_status = 6
        'the procedure aborted
        Try
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str, ParameterDir.pdInput)
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("PKG_sdrn.pro_GetStatusSdrn", dt2)
            If dt2.Rows.Count = 0 Then
                p_status = "8"
                'the record does not exist
            Else
                If dt2.Rows(0).Item("status").ToString = "" Then
                    p_status = "7"
                    'the record exists but there was an error while replicating
                Else
                    p_status = dt2.Rows(0).Item("status").ToString
                End If
            End If
            Return p_status

        Catch ex As Exception
            Return p_status
        End Try

    End Function
    Public Function ReplicateSdrn(ByVal p_date As Date) As String

        Dim oDal As clDal
        Dim p_status As String
        Dim p_date_str As String
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        p_status = 6
        'the procedure aborted
        Try
            'in order to differ between kdstst and kdsappl01 , in the app-config there is SdrnStrtHour as starting-hour.
            'this procedure is executed only according to the above parameter.

            'todo 20101003 : open a thread that will sleep for 30 minutes, then check the control status
            'if status = null -> send "status 7 tell someone/eden.
            p_date_str = getFullDateString(p_date)
            Dim Environment As String = ConfigurationSettings.AppSettings("Environment")
            If (Environment = "Production") Then
                Dim threadSdrn As New System.Threading.Thread(AddressOf ChkStatusSdrn4thread)
                threadSdrn.Start(p_date_str)
            End If

            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("p_date", ParameterType.ntOracleDate, p_date, ParameterDir.pdInput)
            oDal.AddParameter("p_return_code", ParameterType.ntOracleInteger, p_status, ParameterDir.pdOutput)
            oDal.ExecuteSP("KDS.KDS_Driver_Activities_Pack.create_driver_activities@kds2sdrm")

            oBatch.InsertProcessLog(4, 1, KdsLibrary.BL.RecordStatus.Wait, "after sdrn,before status", 0)
            ''**KdsWriteProcessLog(4, 1, 1, "after sdrn,before status")
            If oDal.GetValParam("p_return_code") = "" Then
                p_status = "7"
                'an error occured
            Else
                p_status = oDal.GetValParam("p_return_code")
            End If
            Return p_status

        Catch ex As Exception
            Return p_status
        End Try

    End Function
    Public Sub ChkStatusSdrn4thread(ByVal p_date_str As String)

        Dim oDal As clDal
        Dim dt2 As DataTable = New DataTable()
        Dim p_status As Integer
        Dim BodyMail As String
        Dim ToMail As String
        Dim iSeqChkSdrn As Integer
        p_status = 6
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        'the procedure aborted
        Try
            iSeqChkSdrn = oBatch.InsertProcessLog(4, 1, KdsLibrary.BL.RecordStatus.Wait, "ChkStatusSdrn4thread", 0)
            Thread.Sleep(900000) '15 minutes
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str, ParameterDir.pdInput)
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("PKG_sdrn.pro_GetStatusSdrn", dt2)
            If dt2.Rows.Count = 0 Then
                p_status = 8
                'the record does not exist 
            Else
                If dt2.Rows(0).Item("status").ToString = "" Then
                    p_status = 7
                    'the record exists but there was an error while replicating
                Else
                    p_status = dt2.Rows(0).Item("status").ToString
                End If
            End If
            Select Case p_status
                Case 0
                    'in progress, wait
                Case 1
                    'ready to update, wait
                Case 2
                    'ready for rerun, wait
                Case 6
                    'the procedure aborted
                    oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "sdrn stuck no db ", 13)
                    ''**KdsWriteProcessLog(4, 1, 3, "sdrn stuck no db ", "13")
                    ToMail = ConfigurationSettings.AppSettings("miri")
                    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                    BodyMail = "sdrn stuck no db"
                    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                Case 7
                    'status is null, problem
                    oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "sdrn stuck status 7 call someone ", 12)
                    ''**KdsWriteProcessLog(4, 1, 3, "sdrn stuck status 7 call someone ", "12")
                    ToMail = ConfigurationSettings.AppSettings("miri")
                    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                    BodyMail = "sdrn stuck status 7 call someone"
                    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                Case 8
                    'no rows returned, problem
                    oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "sdrn stuck status 8 check db ", 13)
                    ''**KdsWriteProcessLog(4, 1, 3, "sdrn stuck status 8 check db ", "13")
                    ToMail = ConfigurationSettings.AppSettings("miri")
                    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                    BodyMail = "sdrn stuck status 8 check db"
                    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                Case 9
                    'run and finished ok
                Case Else
                    oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "sdrn weired status " & p_status.ToString, 12)
                    ''** KdsWriteProcessLog(4, 1, 3, "sdrn weired status " & p_status.ToString, 12)
                    ToMail = ConfigurationSettings.AppSettings("miri")
                    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                    BodyMail = "sdrn weired status " & p_status.ToString
                    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
            End Select

        Catch ex As Exception
            oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "ChkStatusSdrn4thread abort " & ex.Message, 12)
            ''**KdsWriteProcessLog(4, 1, 3, "ChkStatusSdrn4thread " & ex.Message, "12")
            Throw ex
            'Finally
        End Try

    End Sub
    Public Sub CreateActivitiesAtSdrn()
        Dim oDal As clDal
        Dim Status As String
        Try
            Status = ChkStatusSdrn(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"))
            If (Status = 8) Then
                oDal = New clDal
                oDal.ClearCommand()
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Now.AddDays(-1), ParameterDir.pdInput)
                oDal.AddParameter("p_return_code", ParameterType.ntOracleInteger, Status, ParameterDir.pdOutput)
                oDal.ExecuteSP("KDS.KDS_Driver_Activities_Pack.create_driver_activities@kds2sdrm")
            End If
        Catch ex As Exception
            Throw New Exception("CreateActivitiesAtSdrn: " & ex.Message)
        End Try

    End Sub
    Public Sub ChkControlAtSdrn()
        Dim Status As String
        Dim BoolSuccess As Boolean = True
        Try
            Status = ChkStatusSdrn(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"))
            Dim Environment As String = ConfigurationSettings.AppSettings("Environment")
            If (Environment = "Production") Then
                If Not (Status = "1") Then
                    BoolSuccess = False
                End If
            Else ' test
                If Not (Status = "9") Then
                    BoolSuccess = False
                End If
            End If
            If Not BoolSuccess Then
                Throw New Exception("ChkControlAtSdrn : Status" & Status & " is not available.")
            End If
        Catch ex As Exception
            Throw New Exception("ChkControlAtSdrn : " & ex.Message)
        End Try
    End Sub
    Public Sub RunSdrnYesterday()
        RunSdrn(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"))
    End Sub
    Public Sub RunSdrn(ByVal In_TAARICH)
        Dim tahalich As Integer
        Dim sub_tahalich As Integer
        Dim p_status_2 As String
        Dim p_date_str As String
        Dim p_date As Date
        Dim p_status As Integer
        Dim if_p_stat As Boolean
        Dim SdrnStrtHour As String
        Dim SdrnStatTimes As String
        Dim BodyMail As String
        Dim ToMail As String
        Dim dt2 As DataTable
        Dim WhrStr As String
        Dim RetSql As String
        Dim iSeqChkSdrn As Integer
        Dim strErrors As String = String.Empty
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            SdrnStrtHour = ConfigurationSettings.AppSettings("SdrnStrtHour") '4
            If SdrnStrtHour = "" Then
                SdrnStrtHour = "4"
            End If
            SdrnStatTimes = ConfigurationSettings.AppSettings("SdrnStatTimes") '3
            If SdrnStatTimes = "" Then
                SdrnStatTimes = "3"
            End If
            tahalich = 4
            sub_tahalich = 1
            iSeqChkSdrn = oBatch.InsertProcessLog(4, 1, KdsLibrary.BL.RecordStatus.Wait, "check sdrn/RunSdrn", 0)
            ''** KdsWriteProcessLog(4, 1, 1, "check sdrn")
            if_p_stat = False
            p_status_2 = SdrnStatTimes '"3"
            p_date_str = In_TAARICH.ToString
            p_date = New DateTime(Mid(p_date_str, 1, 4), Mid(p_date_str, 5, 2), Mid(p_date_str, 7, 2))
            Do While if_p_stat = False
                p_status = ChkStatusSdrn(p_date_str)
                Select Case CInt(p_status)
                    Case 0
                        If p_status_2 = "3" Then
                            p_status_2 = ReplicateSdrn(p_date)
                        Else
                            if_p_stat = True
                            'record exist but not ready yet
                            'wait for next run or email if late
                        End If
                    Case 1
                        'record exist and ready  
                        if_p_stat = True
                        UpdSdrnFromReplicate(p_date_str, "RunBatch")
                        'todo: check if it was not tried yet
                    Case 2
                        If p_status_2 = "3" Then
                            'todo: is it ok to try again?
                            p_status_2 = ReplicateSdrn(p_date)
                        Else
                            if_p_stat = True
                            oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.PartialFinish, "status 2 rerun 4 sdrn", 0)
                            ''** KdsWriteProcessLog(tahalich, sub_tahalich, 4, "status 2 rerun 4 sdrn")
                            'rerun
                            ToMail = ConfigurationSettings.AppSettings("miri")
                            ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                            BodyMail = "status 2 rerun 4 sdrn"
                            SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                            oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.SendMail, "mail sdrn", 0)
                            ''**KdsWriteProcessLog(tahalich, sub_tahalich, 6, "mail sdrn")
                        End If
                    Case 6
                        if_p_stat = True
                        oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "status 6 abort during execution of pkg sdrn, check dblink", 2)
                        ''**KdsWriteProcessLog(tahalich, sub_tahalich, 3, "status 6 abort during execution of pkg sdrn, check dblink", "2")
                        'abort during execution of pkg
                        'ToMail = ConfigurationSettings.AppSettings("miri")
                        'ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                        'BodyMail = "status 6 abort during execution of pkg sdrn, check dblink"
                        'SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                        strErrors = strErrors & "status 6 abort during execution of pkg sdrn, check dblink" & vbCr
                        oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.SendMail, "mail sdrn", 0)
                        ''**KdsWriteProcessLog(tahalich, sub_tahalich, 6, "mail sdrn")
                    Case 7
                        If p_status_2 = "3" Then
                            p_status_2 = ReplicateSdrn(p_date)
                        Else
                            if_p_stat = True
                            oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "status 7 error during replication,tell someone there was an error while replicating", 12)
                            ''**KdsWriteProcessLog(tahalich, sub_tahalich, 3, "status 7 error during replication,tell someone there was an error while replicating", "12")
                            'record exist but an error occurd during replication
                            '                            ToMail = ConfigurationSettings.AppSettings("miri")
                            'ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                            '                           ToMail = ToMail & "," & ConfigurationSettings.AppSettings("DavidP")
                            '                          BodyMail = "status 7 error during replication,tell someone there was an error while replicating"
                            strErrors = strErrors & "status 7 error during replication,tell someone there was an error while replicating" & vbCr
                            '                         SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                            oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.SendMail, "mail sdrn", 0)
                            ''**KdsWriteProcessLog(tahalich, sub_tahalich, 6, "mail sdrn")
                        End If
                    Case 8
                        'record does not exist 
                        If p_status_2 = "3" Then
                            p_status_2 = ReplicateSdrn(p_date)
                        Else
                            if_p_stat = True
                            oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "status 8 replication activated but no status returned", 2)
                            ''**KdsWriteProcessLog(tahalich, sub_tahalich, 3, "status 8 replication activated but no status returned", "2")
                            'replication already activated and no status returned.
                            ToMail = ConfigurationSettings.AppSettings("miri")
                            ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                            BodyMail = "status 8 replication activated but no status returned"
                            SendMail(ToMail, "no peace 4 the wicked", BodyMail)

                            oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.SendMail, "mail sdrn", 0)
                            ''**KdsWriteProcessLog(tahalich, sub_tahalich, 6, "mail sdrn")
                        End If
                    Case 9
                        'check if run in prod and tst:
                        WhrStr = "taarich>trunc(sysdate) and kod_tahalich = 4 And kod_peilut_tahalich > 1 And kod_peilut_tahalich < 8"
                        dt2 = GetRowKds("tb_log_tahalich", WhrStr, "count(*) ct", RetSql)
                        If dt2.Rows.Count = 0 Then
                            'no access to db
                        ElseIf CInt(dt2.Rows(0).Item("ct").ToString) > 0 Then
                            'record already processed, exit
                        Else
                            UpdSdrnFromReplicate(p_date_str, "RunBatch")
                        End If
                        if_p_stat = True
                    Case Else
                        if_p_stat = True
                        oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "unknown status", 2)
                        ''**KdsWriteProcessLog(tahalich, sub_tahalich, 3, "unknown status", "2")
                        'unknown answer
                        strErrors = strErrors & "unknown status " & p_status.ToString & vbCr
                        '                        ToMail = ConfigurationSettings.AppSettings("miri")
                        '                       ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                        '                      BodyMail = "unknown status " & p_status.ToString
                        '                     SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                        oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.SendMail, "mail sdrn", 0)
                        ''**KdsWriteProcessLog(tahalich, sub_tahalich, 6, "mail sdrn")
                End Select
            Loop
            If (strErrors <> String.Empty) Then
                Throw New Exception(strErrors)
            End If
            oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Finish, "end check sdrn/RunSdrn", 0)
        Catch ex As Exception
            oBatch.UpdateProcessLog(iSeqChkSdrn, KdsLibrary.BL.RecordStatus.Faild, "RunSdrn " & ex.Message, 10)
            ''**KdsWriteProcessLog(4, 1, 3, "RunSdrn " & ex.Message, "10")
            Throw ex
        End Try


    End Sub
    Public Sub RunSdrnRetro(ByVal UserBatch As String)

        Dim oDal As clDal
        Dim dt As DataTable = New DataTable()
        Dim dt1 As DataTable = New DataTable()
        Dim dt2 As DataTable = New DataTable()
        Dim p_dt As String
        Dim SdrnReRunPar As String
        Dim BodyMail As String
        Dim ToMail As String
        Dim tahalich As Integer
        Dim sub_tahalich As Integer
        Dim lRequestNum As Integer
        Dim dTaarich As Date
        Dim SdrnStatTimes As String
        Dim iSeqChkSdrnRetro, iRunSdrn As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            iRunSdrn = oBatch.InsertProcessLog(4, 8, KdsLibrary.BL.RecordStatus.Wait, "RunSdrnRetro", 0)
            SdrnReRunPar = ConfigurationSettings.AppSettings("SdrnReRunPar") '10
            SdrnStatTimes = ConfigurationSettings.AppSettings("SdrnStatTimes") '3
            If SdrnStatTimes = "" Then
                SdrnStatTimes = "3"
            End If

            oDal = New clDal
            oDal.ClearCommand()
            'parameter pAr varchar is irrelevant because it is parameter 100 in tb_parametrim
            oDal.AddParameter("pAr", ParameterType.ntOracleVarchar, SdrnReRunPar, ParameterDir.pdInput)
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("PKG_sdrn.pro_GetStatus2Sdrn", dt2)
            If Not dt2.Rows.Count = 0 Then
                'todo: loop on all lines
                For i = 0 To dt2.Rows.Count - 1
                    p_dt = dt2.Rows(i).Item("start_dt").ToString
                    BodyMail = "sdrn rerun status 2 " & p_dt
                    ToMail = ConfigurationSettings.AppSettings("miri")
                    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                    dt = Nothing
                    dt = New DataTable
                    oDal.ClearCommand()
                    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                    oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                    oDal.ExecuteSP("PKG_sdrn.pro_GetDtReRunSdrn", dt)
                    If dt.Rows.Count = 0 Then
                        'rerun not started yet, fields: taarich,kod_peilut_tahalich
                        oDal.ClearCommand()
                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                        oDal.ExecuteSP("PKG_BATCH.pro_get_ovdim4rerunsdrn", dt1)
                        If dt1.Rows.Count = 0 Then
                            ToMail = ConfigurationSettings.AppSettings("miri")
                            ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                            BodyMail = "sdrn rerun status 2 no employees " & p_dt
                            SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                        Else
                            '2010/09/05: rerun after complete execution:
                            tahalich = 4
                            sub_tahalich = 11
                            iSeqChkSdrnRetro = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "retro yamim", 0)
                            ''**KdsWriteProcessLog(4, 11, 1, "start retro yamim")
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                            oDal.ExecuteSP("PKG_sdrn.pro_ins_yamim_4_sidurim")
                            oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "retro yamim", 0)

                            sub_tahalich = 12
                            iSeqChkSdrnRetro = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "delNtrail peilut", 0)
                            ''**KdsWriteProcessLog(4, 12, 1, "after yamim, before delNtrail peilut")
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                            oDal.ExecuteSP("PKG_sdrn.pro_TrailNDel_peilut_4retrSdrn")
                            oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "delNtrail peilut", 0)

                            sub_tahalich = 13
                            iSeqChkSdrnRetro = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "delNtrail sidurim", 0)
                            ''**KdsWriteProcessLog(4, 13, 1, "after peilut, before delNtrail sidurim")
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                            oDal.ExecuteSP("PKG_sdrn.pro_TrailNDel_sidurim_4reSdrn")
                            oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "delNtrail sidurim", 0)

                            sub_tahalich = 14
                            iSeqChkSdrnRetro = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "ins sidurim rerun", 0)
                            ''**KdsWriteProcessLog(4, 14, 1, "after sidurim, before ins sidurim rerun")
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                            oDal.ExecuteSP("PKG_sdrn.pro_ins_sidurim_retroSdrn")
                            oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "ins sidurim rerun", 0)

                            sub_tahalich = 15
                            iSeqChkSdrnRetro = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "ins peilut rerun", 0)
                            ''** KdsWriteProcessLog(4, 15, 1, "after sidurim,before ins peilut rerun")
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                            oDal.ExecuteSP("PKG_sdrn.pro_ins_peilut_retroSdrn")
                            oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "ins peilut rerun", 0)

                            sub_tahalich = 16
                            iSeqChkSdrnRetro = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "upd yamey etc", 0)
                            ''**KdsWriteProcessLog(4, 16, 1, "after rerun, before upd yamey etc")
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                            oDal.ExecuteSP("PKG_BATCH.pro_upd_yamey_avoda_ovdim")
                            oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "upd yamey etc", 0)

                            sub_tahalich = 17
                            iSeqChkSdrnRetro = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "upd control", 0)
                            ''**KdsWriteProcessLog(4, 17, 1, "after yamey rerun, before upd control")
                            oDal.ClearCommand()
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_dt, ParameterDir.pdInput)
                            oDal.ExecuteSP("PKG_sdrn.pro_upd_sdrnRerun_control")
                            oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "retro upd control", 0)
                            ''**sub_tahalich = 17
                            ''**KdsWriteProcessLog(4, 17, 1, "after retro upd control")
                            If UserBatch = "RunBatch" Then
                                iSeqChkSdrnRetro = oBatch.InsertProcessLog(8, 2, KdsLibrary.BL.RecordStatus.Wait, "retro OpenBatchRequest", 0)
                                ''**KdsWriteProcessLog(8, 2, 1, "before retro OpenBatchRequest")
                                'todo: batchDescription= "KdsScheduler",iUserId= 77690 or -12 ?
                                lRequestNum = KdsLibrary.clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12)
                                'p_date_str = yyyymmdd
                                ' no need: dDatestr = Mid(p_dt, 7, 2) & "/" & Mid(p_dt, 5, 2) & "/" & Mid(p_dt, 1, 4)
                                'dTaarich = CDate(dDatestr)
                                'dTaarich=new DateTime(year,month,date)
                                dTaarich = New DateTime(Mid(p_dt, 1, 4), Mid(p_dt, 5, 2), Mid(p_dt, 7, 2))
                                oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "retro OpenBatchRequest", 0)
                                ''**KdsWriteProcessLog(8, 2, 1, "after retro OpenBatchRequest before shguyim")
                                iSeqChkSdrnRetro = oBatch.InsertProcessLog(8, 2, KdsLibrary.BL.RecordStatus.Wait, "retro shguyim", 0)
                                KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcess, BatchExecutionType.All, dTaarich, lRequestNum)
                                'todo: check table tb_log_bakashot for errors etc
                                oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "retro shguyim", 0)
                                ''**KdsWriteProcessLog(8, 2, 2, "after retro shguyim")
                                '2011/03/10 no ishurim till...
                                '' 2010/07/27               no mail ishurim at all
                                'iSeqChkSdrnRetro = oBatch.InsertProcessLog(6, 2, KdsLibrary.BL.RecordStatus.Wait, "retro ishurim", 0)
                                ' ''**KdsWriteProcessLog(8, 2, 2, "after retro shguyim before ishurim")
                                'SdrnStatTimes = ConfigurationSettings.AppSettings("SdrnStatTimes") '2=test, 3=prod since 2010/07/08
                                '' 2010/07/08               If SdrnStatTimes = "2" Then
                                'If SdrnStatTimes = "3" Then
                                '    KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(dTaarich, True)
                                'Else
                                '    KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(dTaarich, False)
                                'End If
                                'oBatch.UpdateProcessLog(iSeqChkSdrnRetro, KdsLibrary.BL.RecordStatus.Finish, "retro ishurim ", 0)
                                ' ''**KdsWriteProcessLog(6, 2, 2, "after retro ishurim ")
                            Else
                                oBatch.InsertProcessLog(8, 2, KdsLibrary.BL.RecordStatus.PartialFinish, "shguyim retro not run UserBatch=" & UserBatch, 0)
                                ''** KdsWriteProcessLog(8, 2, 4, "shguyim retro not run UserBatch=" & UserBatch)
                            End If
                            oBatch.InsertProcessLog(tahalich, 19, KdsLibrary.BL.RecordStatus.Finish, "end ok rerun sadran", 0)
                            ''**KdsWriteProcessLog(4, 19, 2, "end ok rerun sadran")
                        End If
                        'Else
                        '?
                    End If
                Next



            End If

            oBatch.UpdateProcessLog(iRunSdrn, KdsLibrary.BL.RecordStatus.Finish, "RunSdrnRetro", 0)
        Catch ex As Exception
            oBatch.UpdateProcessLog(iRunSdrn, KdsLibrary.BL.RecordStatus.Faild, "RunSdrnRetro " & ex.Message, 10)
            ''**KdsWriteProcessLog(4, 8, 3, "RunSdrnRetro " & ex.Message, "10")
            Throw ex
        End Try

    End Sub
#End Region

#Region "Refresh"
    Public Sub RunRefresh()

        Dim oDal As clDal
        Dim obClManager As KdsBatch.HrWorkersChanges.clMain
        Dim tahalich As Integer
        Dim sub_tahalich As Integer
        Dim p_TAARICH As String
        Dim iSeqRefresh, iRunRefresh As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        'Dim SdrnStatTimes As String

        Try
            iRunRefresh = oBatch.InsertProcessLog(3, 1, KdsLibrary.BL.RecordStatus.Wait, "RunRefresh", 0)
            'no need 4 date parameter since refresh has no date,
            'and PKG_BATCH.pro_ins_yamey_avoda_ovdim is of no importance "backwards"
            oDal = New clDal
            tahalich = 3
            sub_tahalich = 1

            p_TAARICH = getFullDateString(Now)

            Dim threadMeafyenim As New System.Threading.Thread(AddressOf refrsh_meafyenim)
            threadMeafyenim.Start()

            Try
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start matzav", 0)
                ''**KdsWriteProcessLog(3, 1, 1, "start matzav")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "New_Matzav_Ovdim", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok matzav", 0)
                ''** KdsWriteProcessLog(3, 1, 2, "end ok matzav")
                'End If
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "New_Matzav_Ovdim aborted " & ex.Message, 8)
                ''** KdsWriteProcessLog(3, 1, 3, "New_Matzav_Ovdim aborted " & ex.Message, 8)
                'do not throw so the next refresh will be done
            End Try
            Try
                tahalich = 7
                iSeqRefresh = oBatch.InsertProcessLog(7, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start ins_yamey", 0)
                ''**KdsWriteProcessLog(7, 1, 1, "start ins_yamey")
                oDal.ClearCommand()
                oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_TAARICH, ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_ins_yamey_avoda_ovdim")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok ins_yamey", 0)
                ''**KdsWriteProcessLog(7, 1, 2, "end ok ins_yamey")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "pro_ins_yamey_avoda_ovdim aborted " & ex.Message, 8)
                ''**  KdsWriteProcessLog(7, 1, 3, "pro_ins_yamey_avoda_ovdim aborted " & ex.Message, 8)
            End Try

            Try
                tahalich = 3
                sub_tahalich = 2
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start ovdim", 0)
                ''**KdsWriteProcessLog(3, 2, 1, "start ovdim")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "Ovdim", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok ovdim", 0)
                ''**KdsWriteProcessLog(3, 2, 2, "end ok ovdim")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "ovdim aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 2, 3, "ovdim aborted " & ex.Message, 7)
            End Try

            'moved to beginning of this sub
            'Dim threadMeafyenim As New System.Threading.Thread(AddressOf refrsh_meafyenim)
            'threadMeafyenim.Start()

            sub_tahalich = 4
            'KdsWriteProcessLog(3, 4, 1, "start ovdim_im_shinuy")
            'oDal.ClearCommand()
            'oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "Ovdim_Im_Shinuy_HR", ParameterDir.pdInput)
            'oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
            'KdsWriteProcessLog(3, 4, 2, "end ok ovdim_im")
            Try
                sub_tahalich = 5
                'If SdrnStatTimes = 3 Then
                '    KdsWriteProcessLog(3, 5, 2, "no refresh new_pirtey_ovdim 4 karin")
                'Else
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start pirtey_ovdim", 0)
                ''** KdsWriteProcessLog(3, 5, 1, "start pirtey_ovdim")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "New_Pirtey_Ovdim", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok pirtey_ovdim", 0)
                ''**KdsWriteProcessLog(3, 5, 2, "end ok pirtey_ovdim")
                'End If
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "New_Pirtey_Ovdim aborted " & ex.Message, 7)
                ''**  KdsWriteProcessLog(3, 5, 3, "New_Pirtey_Ovdim aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 6
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start ez_nihuly", 0)
                ''**KdsWriteProcessLog(3, 6, 1, "start ez_nihuly")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "Ez_Nihuly", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok ez", 0)
                ''**KdsWriteProcessLog(3, 6, 2, "end ok ez")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "Ez_Nihuly aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 6, 3, "Ez_Nihuly aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 7
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start meashrim", 0)
                ''** KdsWriteProcessLog(3, 7, 1, "start meashrim")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "Meashrim", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok meashrim", 0)
                ''**KdsWriteProcessLog(3, 7, 2, "end ok meashrim")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "meashrim aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 7, 3, "meashrim aborted " & ex.Message, 7)
            End Try
            'Try
            '    sub_tahalich = 3
            '    'If SdrnStatTimes = 3 Then
            '    '    KdsWriteProcessLog(3, 3, 2, "no refresh new_meafyenim_ovdim 4 karin")
            '    'Else
            '    KdsWriteProcessLog(3, 3, 1, "start meafyenim_ovdim")
            '    oDal.ClearCommand()
            '    oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "New_meafyenim_ovdim", ParameterDir.pdInput)
            '    oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
            '    KdsWriteProcessLog(3, 3, 2, "end ok meafyenim_ovdim")
            '    'End If
            'Catch ex As Exception
            '    KdsWriteProcessLog(3, 3, 3, "New_meafyenim_ovdim aborted " & ex.Message)
            'End Try
            Try
                sub_tahalich = 16
                'If SdrnStatTimes = 3 Then
                '    KdsWriteProcessLog(3, 16, 2, "no refresh new_Brerot_Mechdal 4 karin")
                'Else
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start Brerot_Mechdal_Meafyenim", 0)
                ''**KdsWriteProcessLog(3, 16, 1, "start Brerot_Mechdal_Meafyenim")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "New_Brerot_Mechdal_Meafyenim", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end Brerot_Mechdal_Meafyenim", 0)
                ''**KdsWriteProcessLog(3, 16, 2, "end Brerot_Mechdal_Meafyenim")
                'End If
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "Brerot_Mechdal_Meafyenim aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 16, 3, "Brerot_Mechdal_Meafyenim aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 8
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start pivot_pirtey_ovdim", 0)
                ''** KdsWriteProcessLog(3, 8, 1, "start tmp_pirtey_ovdim")
                ' no need, in the procedure oDal.ExecuteSQL("truncate table tmp_pirtey_ovdim")
                oDal.ClearCommand()
                oDal.ExecuteSP("Create_pivot_Pirtey_Ovdim")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok pivot_pirtey_ovdim", 0)
                ''**KdsWriteProcessLog(3, 8, 2, "end ok tmp_pirtey_ovdim")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "pivot_pirtey_ovdim aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 8, 3, "tmp_pirtey_ovdim aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 9
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_EZOR", 0)
                ''** KdsWriteProcessLog(3, 9, 1, "start cTB_EZOR")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_EZOR", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok cTB_EZOR", 0)
                ''** KdsWriteProcessLog(3, 9, 2, "end ok cTB_EZOR")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_EZOR aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 9, 3, "cTB_EZOR aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 10
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_kod_Gil", 0)
                ''**KdsWriteProcessLog(3, 10, 1, "start cTB_kod_Gil")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_kod_Gil", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok cTB_kod_Gil", 0)
                ''**KdsWriteProcessLog(3, 10, 2, "end ok cTB_kod_Gil")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_kod_Gil aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 10, 3, "cTB_kod_Gil aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 11
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_hevra", 0)
                ''**KdsWriteProcessLog(3, 11, 1, "start cTB_hevra")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_hevra", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_hevra", 0)
                ''**KdsWriteProcessLog(3, 11, 2, "end cTB_hevra")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_hevra aborted " & ex.Message, 0)
                ''** KdsWriteProcessLog(3, 11, 3, "cTB_hevra aborted " & ex.Message)
            End Try
            Try
                sub_tahalich = 12
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_ISUK", 0)
                ''**KdsWriteProcessLog(3, 12, 1, "start cTB_ISUK")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_ISUK", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_ISUK", 0)
                ''**  KdsWriteProcessLog(3, 12, 2, "end cTB_ISUK")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_ISUK aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 12, 3, "cTB_ISUK aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 13
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_MAAMAD", 0)
                ''**KdsWriteProcessLog(3, 13, 1, "start cTB_MAAMAD")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_MAAMAD", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_MAAMAD", 0)
                ''** KdsWriteProcessLog(3, 13, 2, "end cTB_MAAMAD")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_MAAMAD aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 13, 3, "cTB_MAAMAD aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 14
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_MAHSANIM", 0)
                ''**KdsWriteProcessLog(3, 14, 1, "start cTB_MAHSANIM")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_MAHSANIM", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_MAHSANIM", 0)
                ''**KdsWriteProcessLog(3, 14, 2, "end cTB_MAHSANIM")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_MAHSANIM aborted " & ex.Message, 0)
                ''**KdsWriteProcessLog(3, 14, 3, "cTB_MAHSANIM aborted " & ex.Message)
            End Try
            Try
                sub_tahalich = 15
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_MEAFYEN_BITZUA", 0)
                ''** KdsWriteProcessLog(3, 15, 1, "start cTB_MEAFYEN_BITZUA")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_MEAFYEN_BITZUA", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_MEAFYEN_BITZUA", 0)
                ''**KdsWriteProcessLog(3, 15, 2, "end cTB_MEAFYEN_BITZUA")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_MEAFYEN_BITZUA aborted " & ex.Message, 0)
                ''** KdsWriteProcessLog(3, 15, 3, "cTB_MEAFYEN_BITZUA aborted " & ex.Message)
            End Try
            Try
                sub_tahalich = 17
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_Mutamut", 0)
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_Mutamut", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_Mutamut", 0)
                ''**KdsWriteProcessLog(3, 17, 2, "end cTB_Mutamut")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_Mutamut aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 17, 3, "cTB_Mutamut aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 18
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start CTB_Dargat_Rishayon", 0)
                ''KdsWriteProcessLog(3, 18, 1, "start CTB_Dargat_Rishayon")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "CTB_Dargat_Rishayon", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end CTB_Dargat_Rishayon", 0)
                ''**KdsWriteProcessLog(3, 18, 2, "end CTB_Dargat_Rishayon")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "CTB_Dargat_Rishayon aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 18, 3, "CTB_Dargat_Rishayon aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 19
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_sector_ISUK", 0)
                ''**KdsWriteProcessLog(3, 19, 1, "start cTB_sector_ISUK")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_sector_ISUK", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_sector_ISUK", 0)
                ''**KdsWriteProcessLog(3, 19, 2, "end cTB_sector_ISUK")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_sector_ISUK aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 19, 3, "cTB_sector_ISUK aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 20
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_SNIF_AV", 0)
                ''**KdsWriteProcessLog(3, 20, 1, "start cTB_SNIF_AV")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_SNIF_AV", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_SNIF_AV", 0)
                ''** KdsWriteProcessLog(3, 20, 2, "end cTB_SNIF_AV")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_SNIF_AV aborted " & ex.Message, 0)
                ''** KdsWriteProcessLog(3, 20, 3, "cTB_SNIF_AV aborted " & ex.Message)
            End Try
            Try
                sub_tahalich = 21
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_status", 0)
                ''**KdsWriteProcessLog(3, 21, 1, "start cTB_status")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_status", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_status", 0)
                ''** KdsWriteProcessLog(3, 21, 2, "end cTB_status")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_status aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 21, 3, "cTB_status aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 22
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_Sug_misra", 0)
                ''** KdsWriteProcessLog(3, 22, 1, "start cTB_Sug_misra")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_Sug_misra", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_Sug_misra", 0)
                ''**KdsWriteProcessLog(3, 22, 2, "end cTB_Sug_misra")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_Sug_misra aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 22, 3, "cTB_Sug_misra aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 23
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_Sug_misra", 0)
                ''**KdsWriteProcessLog(3, 23, 1, "start cTB_sug_yechida")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_sug_yechida", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_sug_yechida", 0)
                ''**KdsWriteProcessLog(3, 23, 2, "end cTB_sug_yechida")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_sug_yechida aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 23, 3, "cTB_sug_yechida aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 24
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start CTB_Tfkidim_Meashrim", 0)
                ''**KdsWriteProcessLog(3, 24, 1, "start CTB_Tfkidim_Meashrim")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "CTB_Tfkidim_Meashrim", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end CTB_Tfkidim_Meashrim", 0)
                ''**KdsWriteProcessLog(3, 24, 2, "end CTB_Tfkidim_Meashrim")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "CTB_Tfkidim_Meashrim aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 24, 3, "CTB_Tfkidim_Meashrim aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 25
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_yechida", 0)
                ''**KdsWriteProcessLog(3, 25, 1, "start cTB_yechida")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_yechida", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_yechida", 0)
                ''**KdsWriteProcessLog(3, 25, 2, "end cTB_yechida")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_yechida aborted " & ex.Message, 7)
                ''** KdsWriteProcessLog(3, 25, 3, "cTB_yechida aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 26
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_yechidat_meafyen", 0)
                ''** KdsWriteProcessLog(3, 26, 1, "start cTB_yechidat_meafyen")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_yechidat_meafyen", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_yechidat_meafyen", 0)
                ''**KdsWriteProcessLog(3, 26, 2, "end cTB_yechidat_meafyen")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "cTB_yechidat_meafyen aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 26, 3, "cTB_yechidat_meafyen aborted " & ex.Message, 7)
            End Try
            'Try
            '    sub_tahalich = 27
            '    KdsWriteProcessLog(3, 27, 1, "start  tmp_meafyenim_ovdim")
            '    oDal.ExecuteSQL("truncate table  tmp_meafyenim_ovdim")
            '    oDal.ClearCommand()
            '    oDal.ExecuteSP("cursor_Meafyenim_Ovdim")
            '    KdsWriteProcessLog(3, 27, 2, "end ok  tmp_meafyenim_ovdim")
            'Catch ex As Exception
            '    KdsWriteProcessLog(3, 27, 3, "tmp_meafyenim_ovdim aborted " & ex.Message)
            'End Try
            Try
                sub_tahalich = 28
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start pivot_meafyeney_elementim", 0)
                ''** KdsWriteProcessLog(3, 28, 1, "start tmp_meafyeney_elementim")
                oDal.ClearCommand()
                oDal.ExecuteSP("PKG_ELEMENTS.calling_Pivot_Meafyeney_e")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok pivot_meafyeney_elementim", 0)
                ''**KdsWriteProcessLog(3, 28, 2, "end ok tmp_meafyeney_elementim")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "calling_Pivot_Meafyeney_e aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 28, 3, "calling_Pivot_Meafyeney_e aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 29
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start pivot_meafyeney_sug_sidur", 0)
                ''** KdsWriteProcessLog(3, 29, 1, "start tmp_meafyeney_sug_sidur")
                oDal.ClearCommand()
                oDal.ExecuteSP("PKG_SUG_SIDUR.calling_Pivot_Meafyeney_S")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok pivot_meafyeney_sug_sidur", 0)
                ''**  KdsWriteProcessLog(3, 29, 2, "end ok tmp_meafyeney_sug_sidur")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "calling_Pivot_Meafyeney_S aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 29, 3, "calling_Pivot_Meafyeney_S aborted " & ex.Message, 7)
            End Try
            Try
                sub_tahalich = 30
                iSeqRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start pivot_sidurim_meyuchadim", 0)
                ''**KdsWriteProcessLog(3, 30, 1, "start tmp_sidurim_meyuchadim")
                oDal.ClearCommand()
                oDal.ExecuteSP("PKG_SIDURIM.calling_Pivot_Sidurim_M")
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok pivot_sidurim_meyuchadim", 0)
                ''** KdsWriteProcessLog(3, 30, 2, "end ok tmp_sidurim_meyuchadim")
            Catch ex As Exception
                oBatch.UpdateProcessLog(iSeqRefresh, KdsLibrary.BL.RecordStatus.Faild, "calling_Pivot_Sidurim_M aborted " & ex.Message, 7)
                ''**KdsWriteProcessLog(3, 30, 3, "calling_Pivot_Sidurim_M aborted " & ex.Message, 7)
            End Try
            obClManager = New KdsBatch.HrWorkersChanges.clMain
            obClManager.HRChangesMatzavPirteyBrerotmechdal()

            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "RunRefresh", 0)
        Catch ex As Exception
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Faild, "RunRefresh abort" & ex.Message, 7)
            ''**KdsWriteProcessLog(3, 1, 3, "RunRefresh " & ex.Message, "7")
            Throw ex
        End Try


    End Sub
    Sub refrsh_meafyenim()

        Dim oDal As clDal
        Dim obClManager As KdsBatch.HrWorkersChanges.clMain
        Dim sub_tahalich, iRunRefresh As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try

            oDal = New clDal
            sub_tahalich = 3
            'If SdrnStatTimes = 3 Then
            '    KdsWriteProcessLog(3, 3, 2, "no refresh new_meafyenim_ovdim 4 karin")
            'Else

            iRunRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start meafyenim_ovdim", 0)
            ''**  KdsWriteProcessLog(3, 3, 1, "start meafyenim_ovdim")
            oDal.ClearCommand()
            oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "New_meafyenim_ovdim", ParameterDir.pdInput)
            oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok meafyenim_ovdim", 0)
            ''** KdsWriteProcessLog(3, 3, 2, "end ok meafyenim_ovdim")
            'End If
            sub_tahalich = 27
            iRunRefresh = oBatch.InsertProcessLog(3, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start  pivot_meafyenim_ovdim", 0)
            ''**KdsWriteProcessLog(3, 27, 1, "start  tmp_meafyenim_ovdim")
            ' noneed in the prc,oDal.ExecuteSQL("truncate table  tmp_meafyenim_ovdim")
            oDal.ClearCommand()
            oDal.ExecuteSP("create_pivot_Meafyenim_Ovdim")
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok  pivot_meafyenim_ovdim", 0)
            ''** KdsWriteProcessLog(3, 27, 2, "end ok  tmp_meafyenim_ovdim")
            'sub_tahalich = 33
            'KdsWriteProcessLog(3, 33, 1, "start upd meafyenim_ovdim")
            'oDal.ExecuteSQL("truncate table meafyenim_ovdim")
            'oDal.ExecuteSQL("insert into meafyenim_ovdim select * from new_meafyenim_ovdim")
            'KdsWriteProcessLog(3, 33, 2, "end ok upd meafyenim_ovdim")
            obClManager = New KdsBatch.HrWorkersChanges.clMain
            obClManager.HRChangesMeafyenim()
        Catch ex As Exception
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Faild, "refrsh_meafyenim aborted " & ex.Message, 7)
            ''**  KdsWriteProcessLog(3, sub_tahalich, 3, "refrsh_meafyenim aborted " & ex.Message, "7")
        End Try

    End Sub
    Sub refresh_from_shmulik()

        Dim oDal As clDal
        Dim dt As DataTable
        Dim WhrStr As String
        Dim tahalich As Integer
        Dim sub_tahalich, iRunRefresh As Integer
        Dim RetSql As String = ""
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch

        oDal = New clDal
        Try

            WhrStr = "taarich>trunc(sysdate) and kod_tahalich = 9"
            dt = GetRowKds("tb_log_tahalich", WhrStr, "*", RetSql)
            If dt.Rows.Count = 0 Then
                tahalich = 9
                sub_tahalich = 1
                iRunRefresh = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_snifey_tnuaa", 0)
                ''** KdsWriteProcessLog(9, 1, 1, "start cTB_snifey_tnuaa")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_snifey_tnuaa", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_snifey_tnuaa", 0)
                ''** KdsWriteProcessLog(9, 1, 2, "end cTB_snifey_tnuaa")
            End If
            WhrStr = "taarich>trunc(sysdate) and kod_tahalich = 10"
            dt = GetRowKds("tb_log_tahalich", WhrStr, "*", RetSql)
            If dt.Rows.Count = 0 Then
                tahalich = 10
                sub_tahalich = 1
                iRunRefresh = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_elementim", 0)
                ''** KdsWriteProcessLog(10, 1, 1, "start cTB_elementim")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_elementim", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_elementim", 0)
                ''**KdsWriteProcessLog(10, 1, 2, "end cTB_elementim")

                sub_tahalich = 2
                iRunRefresh = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_nkudut_tifaul", 0)
                ''** KdsWriteProcessLog(10, 2, 1, "start cTB_nkudut_tifaul")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_nkudut_tifaul", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_nkudut_tifaul", 0)
                ''**   KdsWriteProcessLog(10, 2, 2, "end cTB_nkudut_tifaul")

                sub_tahalich = 3
                iRunRefresh = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_sug_sidur", 0)
                ''**   KdsWriteProcessLog(10, 3, 1, "start cTB_sug_sidur")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_sug_sidur", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_sug_sidur", 0)
                ''**  KdsWriteProcessLog(10, 3, 2, "end cTB_sug_sidur")
            End If
            WhrStr = "taarich>trunc(sysdate) and kod_tahalich = 11"
            dt = GetRowKds("tb_log_tahalich", WhrStr, "*", RetSql)
            If dt.Rows.Count = 0 Then
                tahalich = 11
                sub_tahalich = 1
                iRunRefresh = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start cTB_snifey_mashar", 0)
                ''**KdsWriteProcessLog(11, 1, 1, "start cTB_snifey_mashar")
                oDal.ClearCommand()
                oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "cTB_snifey_mashar", ParameterDir.pdInput)
                oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
                oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end cTB_snifey_mashar", 0)
                ''**KdsWriteProcessLog(11, 1, 2, "end cTB_snifey_mashar")
            End If
        Catch ex As Exception
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Faild, "refresh_from_shmulik aborted " & ex.Message, 7)
            ''** KdsWriteProcessLog(11, sub_tahalich, 3, "refresh_from_shmulik aborted " & ex.Message, "7")
        End Try

    End Sub
    Public Sub RunRefreshRetroMatzav()

        Dim oDal As clDal
        Dim tahalich As Integer
        Dim sub_tahalich, iRunRefresh As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try

            oDal = New clDal
            tahalich = 3
            sub_tahalich = 1
            iRunRefresh = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start Retromatzav", 0)
            ''** KdsWriteProcessLog(3, 1, 1, "start Retromatzav")
            oDal.ClearCommand()
            oDal.AddParameter("shem_mvew", ParameterType.ntOracleVarchar, "New_Matzav_Ovdim", ParameterDir.pdInput)
            oDal.ExecuteSP("PKG_BATCH.pro_RefreshMv")
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok Retromatzav", 0)
            ''** KdsWriteProcessLog(3, 1, 2, "end ok Retromatzav")
        Catch ex As Exception
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Faild, "Retro_Matzav_Ovdim aborted " & ex.Message, 8)
            ''** KdsWriteProcessLog(3, 1, 3, "Retro_Matzav_Ovdim aborted " & ex.Message, 8)
            Throw ex
        End Try

    End Sub
    Public Sub RunRefreshRetroYamim(ByVal p_TAARICH)

        Dim oDal As clDal
        Dim tahalich As Integer
        Dim sub_tahalich, iRunRefresh As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        oDal = New clDal()
        Try

            tahalich = 7
            sub_tahalich = 1
            iRunRefresh = oBatch.InsertProcessLog(tahalich, sub_tahalich, KdsLibrary.BL.RecordStatus.Wait, "start Retroins_yamey", 0)
            ''** KdsWriteProcessLog(7, 1, 1, "start Retroins_yamey")
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_TAARICH, ParameterDir.pdInput)
            oDal.ExecuteSP("PKG_BATCH.pro_ins_yamey_avoda_ovdim")
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Finish, "end ok Retroins_yamey", 0)
            ''**KdsWriteProcessLog(7, 1, 2, "end ok Retroins_yamey")
        Catch ex As Exception
            oBatch.UpdateProcessLog(iRunRefresh, KdsLibrary.BL.RecordStatus.Faild, "RunRefreshRetroYamim aborted " & ex.Message, 8)
            ''**  KdsWriteProcessLog(3, 1, 3, "RunRefreshRetroYamim " & ex.Message, "8")
            Throw ex
        End Try

    End Sub
#End Region

#Region "Thread Hr Chainges"
    Sub Chk_ThreadHrChainges()
        Dim oDal As clDal
        Dim dt As DataTable = New DataTable()
        Dim SdrnStrtHour As String
        Dim if_while As Boolean
        Dim non_stop_loop As Boolean
        Dim p_date_str As String
        Dim p_date As Date
        Dim BodyMail As String
        Dim ToMail As String
        Dim iThreadHrSeq, iloopSeq As Integer

        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        ''if_while is due to 2 parameters:
        ''1: run only between 2 and 6
        ''2: run only if the firts run is finished i.e do not run the first cycle
        ''non_stop_loop is due to 2 parameters:
        ''3: run until the thread is finished - should be checked online each loop
        ''4: run until sdrn is finished - should be checked online each loop

        Try

            oDal = New clDal
            SdrnStrtHour = ConfigurationSettings.AppSettings("SdrnStrtHour") '4
            If SdrnStrtHour = "" Then
                SdrnStrtHour = "4"
            End If
            if_while = False
            non_stop_loop = True
            '1) run only between 2 and 5/6 (sdrn)
            ''debug: 20101012:
            'SdrnStrtHour = "10"
            ''end debug
            If Now.Hour < CInt(SdrnStrtHour + 1) And Now.Hour > 1 Then
                if_while = True
                '2) check if scheduler in on 2nd cycle (for 4.5 hours time slice):pro_if_start
                oDal.ClearCommand()
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                oDal.ExecuteSP("PKG_BATCH.pro_if_start", dt)
                If dt.Rows.Count = 0 Then
                    if_while = False
                Else
                    If dt.Rows(0).Item("ct").ToString = "" Then
                        if_while = False
                    ElseIf CInt(dt.Rows(0).Item("ct").ToString) < 2 Then
                        if_while = False
                    Else
                        if_while = True
                    End If
                End If
            End If

            'the sadran run on yesterday
            p_date = Now.AddDays(-1)
            p_date_str = getFullDateString(p_date)

            non_stop_loop = check_non_stop_loop()

            If if_while And non_stop_loop Then
                iThreadHrSeq = oBatch.InsertProcessLog(3, 37, KdsLibrary.BL.RecordStatus.Wait, "start Chk_ThreadHrChainges", 0)
                '1st thread: RunThreadHrChainges
                Dim threadHrChainges As New Thread(New ParameterizedThreadStart(AddressOf RunThreadHrChainges))
                threadHrChainges.Start(New Object() {iThreadHrSeq})
                '2nd thread: loop 2 check if already 5 - 6 oclock to run sdrn
                While non_stop_loop
                    '*'  iloopSeq = oBatch.InsertProcessLog(8, 88, KdsLibrary.BL.RecordStatus.Wait, "inside loop hr", 0)
                    If Now.Hour > CInt(SdrnStrtHour) And Now.Hour < 13 Then
                        RunSdrn(p_date_str) 'yyyymmdd
                    End If
                    Thread.Sleep(300000) '=5 minutes
                    non_stop_loop = check_non_stop_loop()
                    '*' oBatch.UpdateProcessLog(iloopSeq, KdsLibrary.BL.RecordStatus.Finish, "inside loop hr", 0)
                End While
                oBatch.UpdateProcessLog(iThreadHrSeq, KdsLibrary.BL.RecordStatus.Finish, "end Chk_ThreadHrChainges", 0)
            End If
        Catch ex As Exception
            clGeneral.LogMessage(ex.Message, EventLogEntryType.Error)
            'todo: is 9=abort?
            oBatch.UpdateProcessLog(iThreadHrSeq, KdsLibrary.BL.RecordStatus.Faild, "Chk_ThreadHrChainges aborted " & ex.Message, 7)
            ''**KdsWriteProcessLog(8, 3, 3, "end " & ex.Message, 7)
            'oKDs.KdsWriteProcessLog(1, 1, 3, "end " & ex.Message)
            ToMail = ConfigurationSettings.AppSettings("miri")
            ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
            BodyMail = "scheduler Chk_ThreadHrChainges aborted"
            SendMail(ToMail, "no peace 4 the wicked", BodyMail)
        End Try

    End Sub
    Public Function check_non_stop_loop() As Boolean
        ' check params 3 & 4 and insert into the while non_stop_loop
        '3: run until the thread is finished - should be checked online each loop
        Dim WhrStr As String
        Dim dt As DataTable
        Dim RetSql As String
        Dim non_stop_loop As Boolean
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch

        Try
            non_stop_loop = True
            RetSql = ""
            WhrStr = "taarich>trunc(sysdate) and kod_tahalich=8 and kod_peilut_tahalich=3 and status=2"
            dt = GetRowKds("tb_log_tahalich", WhrStr, "count(*) ct", RetSql)
            If dt.Rows.Count = 0 Then
                ' something is wrong, but should go on trying leave non_stop_loop = True
            Else
                If CInt(dt.Rows(0).Item("ct").ToString) > 0 Then
                    non_stop_loop = False
                End If
            End If
            ''4: run until sdrn has started - should be checked online each loop
            WhrStr = "taarich>trunc(sysdate) and kod_tahalich=4 and kod_peilut_tahalich=4 and status>=1"
            dt = GetRowKds("tb_log_tahalich", WhrStr, "count(*) ct", RetSql)
            If dt.Rows.Count = 0 Then
                ' something is wrong, but should go on trying leave non_stop_loop = True
            Else
                If CInt(dt.Rows(0).Item("ct").ToString) > 0 Then
                    non_stop_loop = False
                End If
            End If
            Return non_stop_loop

        Catch ex As Exception
            oBatch.InsertProcessLog(3, 38, KdsLibrary.BL.RecordStatus.Faild, "check_non_stop_loop " & ex.Message, 7)
            ''**  KdsWriteProcessLog(3, 37, 3, "check_non_stop_loop " & ex.Message, "7")
            Throw ex
            'Finally
        End Try

    End Function
    Public Sub RunThreadHrChainges(ByVal objSekChk As Object) 'todo: 20101003(ByVal In_TAARICH As String)

        Dim oDal As clDal
        Dim dt As DataTable = New DataTable()
        Dim dt2 As DataTable = New DataTable()
        Dim dt3 As DataTable = New DataTable()
        Dim ct As Integer
        Dim BodyMail As String
        Dim ToMail As String
        Dim dTaarich As Date
        Dim st1 As String
        Dim st2 As String
        Dim p_date_str As String
        Dim p_date As Date
        Dim p_date_str_now As String
        Dim num, iSeqThreadHr, iSeqNum, iloopSeq As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim iSekChk As Integer
        Try

            iSekChk = CType(objSekChk(0), Integer)

            ' iSekChk = CInt(objSekChk)
            iloopSeq = oBatch.InsertProcessLog(8, 89, KdsLibrary.BL.RecordStatus.Wait, "bdika RunThreadHr", 0)

            iSeqThreadHr = -1
            oBatch = New KdsLibrary.BL.clBatch()
            'the sadran run on yesterday
            p_date = Now.AddDays(-1)
            p_date_str = getFullDateString(p_date)

            p_date_str_now = getFullDateString(Now)


            '1) check if meyrav's run and finished ok:PKG_BATCH.pro_sof_meafyenim
            '2) check if scheduler in on 2nd cycle (for 4.5 hours time slice):pro_if_start
            '3) check if this process did not run yet:pro_if_GalreadyRun

            '1) check if meyrav's run and finished ok:PKG_BATCH.pro_sof_meafyenim
            oDal = New clDal
            oDal.ClearCommand()
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, p_date_str_now, ParameterDir.pdInput)
            oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("PKG_BATCH.pro_sof_meafyenim", dt)
            If dt.Rows.Count = 0 Then
                oBatch.UpdateProcessLog(iSekChk, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy hr no db ", 13)
                ''** KdsWriteProcessLog(3, 37, 3, "thread after shinuy hr no db ", "13")
                'the record does not exist, something is wrong
            Else
                If dt.Rows(0).Item("ct").ToString = "" Then
                    oBatch.UpdateProcessLog(iSekChk, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy hr something's wrong ", 13)
                    ''**KdsWriteProcessLog(3, 37, 3, "thread after shinuy hr something's wrong ", "13")
                    'the record exists but something is wrong
                Else
                    ct = CInt(dt.Rows(0).Item("ct").ToString)
                    '*'  oBatch.InsertProcessLog(8, 87, KdsLibrary.BL.RecordStatus.Wait, "bdika RunThreadHr ct=" & ct, 0)

                    If ct = 0 Then
                        'should not run since the refresh is not ok
                        'todo: send email refresh not refreshed only once
                        oBatch.UpdateProcessLog(iSekChk, KdsLibrary.BL.RecordStatus.Faild, "refresh not refreshed", 13)
                        ''**  KdsWriteProcessLog(3, 37, 3, "refresh not refreshed", 13)
                        ToMail = ConfigurationSettings.AppSettings("miri")
                        ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                        BodyMail = "refresh not refreshed"
                        SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                        oBatch.InsertProcessLog(3, 37, KdsLibrary.BL.RecordStatus.SendMail, "mail db", 0)
                        ''**KdsWriteProcessLog(3, 37, 6, "mail db")
                    Else
                        '2) check if scheduler in on 2nd cycle (for 4.5 hours time slice):pro_if_start
                        oDal.ClearCommand()
                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                        oDal.ExecuteSP("PKG_BATCH.pro_if_start", dt2)
                        If dt2.Rows.Count = 0 Then
                            oBatch.UpdateProcessLog(iSekChk, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy,start kds no db ", 13)
                            ''** KdsWriteProcessLog(3, 37, 3, "thread after shinuy,start kds no db ", "13")
                            'the record does not exist, something is wrong
                        Else
                            If dt2.Rows(0).Item("ct").ToString = "" Then
                                oBatch.UpdateProcessLog(iSekChk, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy,start kds no db ", 13)
                                ''** KdsWriteProcessLog(3, 37, 3, "thread after shinuy,start kds no db ", "13")
                                'the record exists but something is wrong
                            ElseIf CInt(dt2.Rows(0).Item("ct").ToString) < 2 Then
                                'this is the first run of schedulaer today, finish
                            Else

                                '3) check if this process did not run yet: pro_if_GalreadyRun
                                oDal.ClearCommand()
                                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                oDal.ExecuteSP("PKG_BATCH.pro_if_GalreadyRun", dt3)
                                If dt3.Rows.Count = 0 Then
                                    oBatch.UpdateProcessLog(iSekChk, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy, GRun no db ", 13)
                                    ''** KdsWriteProcessLog(8, 3, 3, "thread after shinuy, GRun no db ", "13")
                                    'the record does not exist, something is wrong
                                Else
                                    ' st1 = dt3.Rows(0).Item("stat1").ToString
                                    st2 = dt3.Rows(0).Item("stat2").ToString
                                    '*'   oBatch.InsertProcessLog(8, 86, KdsLibrary.BL.RecordStatus.Wait, "bdika RunThreadHr st2=" & st2, 0)

                                    Select Case (st2)  '(st1 & st2)
                                        Case "0"
                                            iSeqThreadHr = oBatch.InsertProcessLog(8, 3, KdsLibrary.BL.RecordStatus.Wait, "start RunThreadHrChainges", 0)
                                            '(0,0)=no record at all ->run
                                            num = oBatch.GetNumChangesHrToShguim()
                                            If (num < 50000) Then
                                                Dim lRequestNum As Integer
                                                iSeqNum = oBatch.InsertProcessLog(8, 4, KdsLibrary.BL.RecordStatus.Wait, "before OpenBatchRequest hr", 0)
                                                ''**KdsWriteProcessLog(8, 3, 1, "before OpenBatchRequest")
                                                lRequestNum = KdsLibrary.clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12)
                                                dTaarich = New DateTime(Mid(p_date_str, 1, 4), Mid(p_date_str, 5, 2), Mid(p_date_str, 7, 2))
                                                oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after OpenBatchRequest hr", 0)
                                                ''** KdsWriteProcessLog(8, 3, 1, "after OpenBatchRequest before shguyim")
                                                iSeqNum = oBatch.InsertProcessLog(8, 4, KdsLibrary.BL.RecordStatus.Wait, "before shguyim hr", 0)
                                                KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcessForChangesInHR, BatchExecutionType.All, dTaarich, lRequestNum)
                                                oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after shguyim from hr", 0)
                                                oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Finish, "end RunThreadHrChainges", 0)
                                                ''**KdsWriteProcessLog(8, 3, 2, "after shguyim from hr")
                                            Else
                                                oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.PartialFinish, "ThreadHrChainges did not run.a lot of mispar_ishi: " & num.ToString(), 0)
                                                ''**  KdsWriteProcessLog(8, 3, 4, "ThreadHrChainges did not run.a lot of mispar_ishi: " & num.ToString())
                                            End If
                                        Case "1"
                                            '(1,0)=started but not finished, aborted? ->mail
                                            oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy, GRun only started ", 7)
                                            ''**KdsWriteProcessLog(8, 3, 3, "thread after shinuy, GRun only started ", "7")
                                            ToMail = ConfigurationSettings.AppSettings("miri")
                                            ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                                            BodyMail = "thread after shinuy, GRun started"
                                            SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                                            oBatch.InsertProcessLog(3, 8, KdsLibrary.BL.RecordStatus.SendMail, "mail db", 0)
                                            ''**KdsWriteProcessLog(3, 8, 6, "mail db")
                                        Case "2"
                                            '(1,2)=started and finished
                                            ''Case "11"
                                            ''    '(1,1)=started but not finished, aborted? ->mail
                                            ''    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy, GRun only started ", 7)
                                            ''    ''** KdsWriteProcessLog(8, 3, 3, "thread after shinuy, GRun only started ", "7")
                                            ''    ToMail = ConfigurationSettings.AppSettings("miri")
                                            ''    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                                            ''    BodyMail = "thread after shinuy, GRun started"
                                            ''    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                                            ''    oBatch.InsertProcessLog(3, 8, KdsLibrary.BL.RecordStatus.SendMail, "mail db", 0)
                                            ''    ''** KdsWriteProcessLog(3, 8, 6, "mail db")
                                            ''Case "22"
                                            ''    '(2,2)=finished but not started,weired? ->mail
                                            ''    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy, GRun only finished ", 7)
                                            ''    ''** KdsWriteProcessLog(8, 3, 3, "thread after shinuy, GRun only finished ", "7")
                                            ''    ToMail = ConfigurationSettings.AppSettings("miri")
                                            ''    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                                            ''    BodyMail = "thread after shinuy, GRun weired"
                                            ''    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                                            ''    oBatch.InsertProcessLog(3, 8, KdsLibrary.BL.RecordStatus.SendMail, "mail db", 0)
                                            ''    ''** KdsWriteProcessLog(3, 8, 6, "mail db")
                                            ''Case "21"
                                            ''    '(2,1)=started and finished,weired? ->mail
                                            ''    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy, GRun weired ", 7)
                                            ''    ''**KdsWriteProcessLog(8, 3, 3, "thread after shinuy, GRun weired ", "7")
                                            ''    ToMail = ConfigurationSettings.AppSettings("miri")
                                            ''    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                                            ''    BodyMail = "thread after shinuy, GRun weired"
                                            ''    SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                                            ''    oBatch.InsertProcessLog(3, 8, KdsLibrary.BL.RecordStatus.SendMail, "mail db", 0)
                                            ''    ''** KdsWriteProcessLog(3, 8, 6, "mail db")
                                        Case Else
                                            'weired? ->mail
                                            oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy, GRun weired ", 7)
                                            ''** KdsWriteProcessLog(8, 3, 3, "thread after shinuy, GRun weired ", "7")
                                            ToMail = ConfigurationSettings.AppSettings("miri")
                                            ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                                            BodyMail = "thread after shinuy, GRun weired"
                                            SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                                            oBatch.InsertProcessLog(3, 8, KdsLibrary.BL.RecordStatus.SendMail, "mail db", 0)
                                            ''** KdsWriteProcessLog(3, 8, 6, "mail db")
                                    End Select
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            oBatch.UpdateProcessLog(iloopSeq, KdsLibrary.BL.RecordStatus.Finish, "bdika RunThreadHr", 0)

        Catch ex As Exception
            If (iSeqThreadHr <> -1) Then
                oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Faild, "thread after shinuy hr aborted " & ex.Message, 13)
            End If
            If (iloopSeq > 0) Then
                oBatch.UpdateProcessLog(iloopSeq, KdsLibrary.BL.RecordStatus.Faild, "bdika RunThreadHr Exception: " & ex.Message, 0)
            End If
            ''** KdsWriteProcessLog(3, 37, 3, "thread after shinuy hr aborted " & ex.Message, "7")
            Throw ex
            'Finally

        End Try

    End Sub
#End Region

#Region "General Functions"
    Function GetRowKds(ByVal TableName As String, _
                  ByVal Criteria As String, _
                  ByVal FieldsList As String, _
                  ByRef RetSql As String) As DataTable

        Dim sql As String = "select "
        Dim part1 As String
        Dim part2 As String
        Dim part3 As String
        Dim dtbObject As DataTable = New DataTable
        Dim oDal As clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try

            dtbObject = New DataTable(TableName)

            If FieldsList = String.Empty Then
                part1 = " * "
            Else
                part1 = FieldsList & " "
            End If
            part2 = "from " & TableName & " "
            If Not Criteria = String.Empty Then
                part3 = "where " & Criteria & " "
            End If
            RetSql = String.Empty
            sql = sql & part1 & part2 & part3

            oDal = New clDal
            oDal.ExecuteSQL(sql, dtbObject)
        Catch ex As Exception 'todo: As Oracle.DataAccess.Client.OracleException
            RetSql = sql
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "GetRowKds " & ex.Message, 4)
            ''**KdsWriteProcessLog(2, 1, 3, "GetRowKds " & ex.Message, "4")
            Throw ex
        Finally
            'Release(oDB)
        End Try

        If dtbObject.Rows.Count = 0 Then
            'throw...
        End If
        If dtbObject.Rows.Count > 1 Then
            'throw...
        End If

        Return dtbObject

    End Function
    '' ''Public Sub KdsWriteProcessLog(ByVal KodTahalich As Integer, ByVal KodPeilut As Integer, ByVal KodStatus As Integer, ByVal TeurTech As String, Optional ByVal KodTakala As String = "")

    '' ''    Dim oDal As clDal
    '' ''    Try
    '' ''        oDal = New clDal
    '' ''        oDal.AddParameter("p_KodTahalich", ParameterType.ntOracleInt64, KodTahalich, ParameterDir.pdInput)
    '' ''        oDal.AddParameter("p_KodPeilut", ParameterType.ntOracleInt64, KodPeilut, ParameterDir.pdInput)
    '' ''        oDal.AddParameter("p_KodStatus", ParameterType.ntOracleInt64, KodStatus, ParameterDir.pdInput)
    '' ''        If Len(Trim(TeurTech)) > 100 Then
    '' ''            TeurTech = Left(Trim(TeurTech), 100)
    '' ''        Else
    '' ''            TeurTech = Trim(TeurTech)
    '' ''        End If
    '' ''        oDal.AddParameter("p_TeurTech", ParameterType.ntOracleVarchar, Trim(TeurTech), ParameterDir.pdInput)
    '' ''        If KodTakala = "" Then
    '' ''            oDal.ExecuteSP("PKG_BATCH.pro_ins_log_tahalich")
    '' ''        ElseIf KodTakala = "0" Then
    '' ''            oDal.ExecuteSP("PKG_BATCH.pro_ins_log_tahalich")
    '' ''        Else
    '' ''            oDal.AddParameter("p_KodTakala", ParameterType.ntOracleInt64, CInt(KodTakala), ParameterDir.pdInput)
    '' ''            oDal.ExecuteSP("PKG_BATCH.pro_ins_log_tahalich_takala")
    '' ''        End If
    '' ''    Catch ex As Exception
    '' ''        clGeneral.LogMessage(ex.Message, EventLogEntryType.Error)
    '' ''        Throw ex
    '' ''    Finally

    '' ''    End Try
    '' ''End Sub
    Function Execute_kds(ByVal KdsSql As String) As Boolean
        'my overlay with answere=boolean ifok
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim oDal As clDal
        Try
            oDal = New clDal
            oDal.ExecuteSQL(KdsSql)
            Return True
        Catch ex As Exception
            oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "Execute_kds " & ex.Message, 4)
            ''**  KdsWriteProcessLog(2, 1, 3, "Execute_kds " & ex.Message, "4")
            Return False
        End Try

    End Function
    Sub SendMail(ByVal ToMail As String, ByVal SubjMail As String, ByVal BodyMail As String)
        Dim smail As System.Net.Mail.SmtpClient
        Dim FromMail As String

        smail = New System.Net.Mail.SmtpClient
        Try
            smail.Host = ConfigurationSettings.AppSettings("SmtpHost") '"Mail01"
            FromMail = "kds_" & ConfigurationSettings.AppSettings("NoRep") 'donotreply@egged.co.il"

            smail.Send(FromMail, ToMail, SubjMail, BodyMail)
        Catch ex As Exception
        End Try
    End Sub
    Function getFullDateString(ByVal sDate As Date) As String
        Dim taarich As String

        taarich = sDate.Year.ToString
        If sDate.Month < 10 Then
            taarich += "0"
        End If
        taarich += sDate.Month.ToString

        If sDate.Day < 10 Then
            taarich += "0"
        End If
        taarich += sDate.Day.ToString

        Return taarich
    End Function
    Public Sub TryKdsFilePundakim()

        'in web.config & app.config key="KdsInputFileNamePundakim" default value="Pundakim*.TXT"
        Dim FileName As String = ConfigurationSettings.AppSettings("KdsInputFileNamePundakim") 'collectmodem.dat
        If Trim(FileName) = "" Then
            FileName = "collectmodem.dat" 'Pundakim*.DAT"
        End If
        'Dim InPath As String = ConfigurationSettings.AppSettings("KdsFilePath") '"\\kdstst01\Files\"
        'Dim SubFolder As String = ConfigurationSettings.AppSettings("KdsFileSubPath") '"inkds_old\"
        Dim InPath As String = ConfigurationSettings.AppSettings("KdsFilePathPundakim") '"\\kiwi\"
        'Dim SubFolder As String = ConfigurationSettings.AppSettings("KdsFileSubPathPundakim") '"synel\"
        Dim SubFolder As String = ConfigurationSettings.AppSettings("KdsFileSubPath") '"inkds_old\"
        Dim FileNameOld As String
        Dim MyFile As String
        Dim ShaonimNumber As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim strErrorOfFiles As String = String.Empty
        Dim ErrorCounter As Integer = 0

        Try
            MyFile = Dir(InPath & FileName)
            If Not MyFile = "" Then
                ShaonimNumber = oBatch.InsertProcessLog(2, 2, KdsLibrary.BL.RecordStatus.Wait, "Pundakim", 0)

                While Not MyFile = ""
                    Try
                        LoadKdsFilePundakim(MyFile, ShaonimNumber)
                    Catch ex As Exception
                        ErrorCounter = ErrorCounter + 1
                        strErrorOfFiles = strErrorOfFiles & ErrorCounter.ToString() & "." & ex.Message & vbCr
                    End Try
                    FileNameOld = Left(MyFile, Len(MyFile) - 4) & ".old"
                    File.Copy(InPath & MyFile, InPath & SubFolder & FileNameOld, True)
                    'oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after copy Pundakim " & MyFile, 0)
                    File.Delete(InPath & MyFile)
                    'oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after delete Pundakim " & MyFile, 0)
                    MyFile = Dir()
                End While
                oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, "Pundakim", 0)
                If (strErrorOfFiles <> String.Empty) Then
                    Throw New Exception(strErrorOfFiles)
                End If
            End If

        Catch ex As Exception
            oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Pundakim aborted" & ex.Message, 3)
            Throw ex
        End Try
    End Sub
    Public Sub LoadKdsFilePundakim(ByVal InFileName, ByVal ShaonimNumber)

        Dim oDal As clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim sr As StreamReader
        Dim dt As DataTable
        Dim ErrFileName As String
        Dim line As String
        Dim InPathNFile As String
        Dim SRV_D_ISHI As String
        Dim SRV_D_TAARICH As String
        Dim SRV_D_KNISA_X As String
        Dim SRV_D_MIKUM_KNISA As String
        Dim Restline As String
        Dim Outline As String
        Dim i As Integer
        Dim previousday As String
        Dim LineErrCnt As Integer

        Try

            ErrFileName = ConfigurationSettings.AppSettings("KdsFilePath") & "Pundakim" & CStr(Now.Year) & CStr(Now.Month) & CStr(Now.Day) & CStr(Now.Hour) & CStr(Now.Minute) & CStr(Now.Second) & ".err"
            'InPathNFile = ConfigurationSettings.AppSettings("KdsFilePath") & InFileName
            InPathNFile = ConfigurationSettings.AppSettings("KdsFilePathPundakim") & InFileName 'collectmodem.dat
            sr = New StreamReader(InPathNFile)

            line = sr.ReadLine
            LineErrCnt = 0
            If Trim(line) = "" Then
                line = sr.ReadLine
                LineErrCnt = LineErrCnt + 1
            End If
            oDal = New clDal
            While Not ((Trim(line) Is Nothing) Or (Trim(line) = ""))
                Try
                    SRV_D_MIKUM_KNISA = Mid(line, 1, 3)
                    If Not CInt(SRV_D_MIKUM_KNISA) = 147 Then
                        SRV_D_TAARICH = "20" & Mid(line, 16, 2) & Mid(line, 14, 2) & Mid(line, 12, 2)  'convert from format=ddmmyy 
                        previousday = SRV_D_TAARICH

                        dt = Nothing
                        dt = New DataTable
                        oDal.ClearCommand()
                        oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                        oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                        oDal.ExecuteSP("PKG_BATCH.pro_GetRowDt", dt)
                        previousday = dt.Rows(0).Item("previousday").ToString

                        i = 0
                        Restline = Mid(line, 18 + 14 * i, 14)
                        While Not ((Trim(Restline) Is Nothing) Or (Trim(Restline) = "") Or i > 7)
                            'prepare line for old pundakim:
                            '7769062013111846100074902106115180000000000000000000000000000000000000000        99001000000000100  
                            Outline = ""
                            SRV_D_ISHI = Mid(Restline, 5, 5)
                            SRV_D_KNISA_X = Mid(Restline, 11, 4) 'format=hhmm
                            Outline = SRV_D_ISHI & Mid(Restline, 10, 1)
                            If CInt(Mid(Restline, 11, 2)) < 5 Then
                                Outline = Outline & previousday & "00000"
                                Outline = Outline & (CInt(SRV_D_KNISA_X) + 2400).ToString & "00000000"
                            Else
                                Outline = Outline & SRV_D_TAARICH & "00000"
                                Outline = Outline & SRV_D_KNISA_X & "00000000"
                            End If
                            Outline = Outline & SRV_D_MIKUM_KNISA & "00"
                            Outline = Outline & "000000000000000000000000000000000000000        "
                            '                    12345678901234567890123456789012345678901234567
                            'Outline = Outline & "99214" & "000000000" & Mid(line, 5, 1) & "00  "
                            Outline = Outline & "99214" & "000000010000  "
                            LoadPundakim(Outline)
                            i = i + 1
                            Restline = Mid(line, 18 + 14 * i, 14)
                        End While
                    Else
                        'todo: send the record to sidur hityazvut 99200
                    End If

                Catch ex As Exception
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Pundakim aborted line " & LineErrCnt.ToString & ex.Message, 3)
                    Throw ex
                End Try
                line = sr.ReadLine
                LineErrCnt = LineErrCnt + 1
                While Trim(line) = "" And Not sr.EndOfStream
                    line = sr.ReadLine
                    LineErrCnt = LineErrCnt + 1
                End While
            End While
        Catch ex As Exception
            oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Pundakim aborted line " & LineErrCnt.ToString & ex.Message, 3)
            Throw ex
        Finally
            sr.Close()
        End Try
    End Sub
    Public Sub TryKdsFileAgtan()

        'in web.config & app.config key="KdsInputFileNamePundakim" default value="Agtan*.TXT"
        Dim FileName As String = ConfigurationSettings.AppSettings("KdsInputFileNameAgtan")
        If Trim(FileName) = "" Then
            FileName = "A01SN_AGTAN.DAT"
        End If
        Dim InPath As String = ConfigurationSettings.AppSettings("KdsFilePath") '"\\KDSTEST\Files"
        Dim SubFolder As String = ConfigurationSettings.AppSettings("KdsFileSubPath") '"inkds_old\"
        Dim FileNameOld As String
        Dim MyFile As String
        Dim ShaonimNumber As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim strErrorOfFiles As String = String.Empty
        Dim ErrorCounter As Integer = 0
        Dim NewMyFile As String


        Try
            MyFile = Dir(InPath & FileName)
            If Not MyFile = "" Then
                ShaonimNumber = oBatch.InsertProcessLog(2, 2, KdsLibrary.BL.RecordStatus.Wait, "Agtan", 0)

                While Not MyFile = ""
                    Try
                        NewMyFile = ""
                        NewMyFile = LoadKdsFileAgtan(MyFile, ShaonimNumber)
                    Catch ex As Exception
                        ErrorCounter = ErrorCounter + 1
                        strErrorOfFiles = strErrorOfFiles & ErrorCounter.ToString() & "." & ex.Message & vbCr
                    End Try
                    FileNameOld = Left(MyFile, Len(MyFile) - 4) & ".old"
                    File.Copy(InPath & MyFile, InPath & SubFolder & FileNameOld, True)
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after copy Agtan " & MyFile, 0)
                    File.Delete(InPath & MyFile)
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after delete Agtan " & MyFile, 0)

                    FileNameOld = Left(NewMyFile, Len(NewMyFile) - 4) & ".old"
                    File.Copy(InPath & NewMyFile, InPath & SubFolder & FileNameOld, True)
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after copy NewMyFile " & NewMyFile, 0)
                    File.Delete(InPath & NewMyFile)
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after delete NewMyFile " & NewMyFile, 0)

                    MyFile = Dir()
                End While
                oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, "Agtan", 0)
                If (strErrorOfFiles <> String.Empty) Then
                    Throw New Exception(strErrorOfFiles)
                End If
            End If

        Catch ex As Exception
            oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Agtan aborted" & ex.Message, 3)
            Throw ex
        End Try
    End Sub

    Public Function LoadKdsFileAgtan(ByVal InFileName, ByVal ShaonimNumber) As String

        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim sr As StreamReader
        Dim sw As StreamWriter
        Dim ErrFileName As String
        Dim OutFileName As String
        Dim line As String
        Dim InPathNFile As String
        Dim SRV_D_TAARICH As String
        Dim srv_d_in_out As String
        Dim SRV_D_ISHI As String
        Dim SRV_D_KNISA_X As String
        'Dim srv_d_maamad As String
        Dim SRV_D_MIKUM_Kod As String
        Dim SRV_D_MIKUM As String
        Dim SRV_D_MIKUM_KNISA As String
        Dim SRV_D_YETZIA_X As String
        Dim SRV_D_MIKUM_YETZIA As String
        Dim Outline As String
        Dim OutFile As String


        Try

            ErrFileName = ConfigurationSettings.AppSettings("KdsFilePath") & "Agtan" & CStr(Now.Year) & CStr(Now.Month) & CStr(Now.Day) & CStr(Now.Hour) & CStr(Now.Minute) & CStr(Now.Second) & ".err"
            InPathNFile = ConfigurationSettings.AppSettings("KdsFilePath") & InFileName
            OutFileName = ConfigurationSettings.AppSettings("KdsFilePath") & "Agtan" & CStr(Now.Year) & CStr(Now.Month) & CStr(Now.Day) & CStr(Now.Hour) & CStr(Now.Minute) & ".TXT"
            OutFile = "Agtan" & CStr(Now.Year) & CStr(Now.Month) & CStr(Now.Day) & CStr(Now.Hour) & CStr(Now.Minute) & ".TXT"

            sr = New StreamReader(InPathNFile)
            sw = New StreamWriter(OutFileName, False)

            line = sr.ReadLine
            If Trim(line) = "" Then
                line = sr.ReadLine
            End If
            While Not ((Trim(line) Is Nothing) Or (Trim(line) = ""))
                Try
                    SRV_D_TAARICH = Mid(line, 5, 4) & Mid(line, 3, 2) & Mid(line, 1, 2) 'convert from format=ddmmyyyy 
                    srv_d_in_out = Mid(line, 9, 3) 'A10=in, B20=out
                    SRV_D_ISHI = Mid(line, 12, 5)
                    SRV_D_MIKUM_Kod = Mid(line, 22, 2) 'M0=19501, P0=20101
                    If SRV_D_MIKUM_Kod = "M0" Then
                        SRV_D_MIKUM = "195"
                    ElseIf SRV_D_MIKUM_Kod = "P0" Then
                        SRV_D_MIKUM = "201"
                    Else
                        SRV_D_MIKUM = "000"
                        'todo: Error mikum
                    End If
                    If srv_d_in_out = "A10" Then
                        SRV_D_KNISA_X = Mid(line, 17, 4) 'format=hhmm
                        SRV_D_MIKUM_KNISA = SRV_D_MIKUM
                        SRV_D_YETZIA_X = "0000"
                        ' 20150208:SRV_D_MIKUM_YETZIA = SRV_D_MIKUM
                        SRV_D_MIKUM_YETZIA = "000"
                    ElseIf srv_d_in_out = "B20" Then
                        SRV_D_YETZIA_X = Mid(line, 17, 4) 'format=hhmm
                        SRV_D_MIKUM_YETZIA = SRV_D_MIKUM
                        SRV_D_KNISA_X = "0000"
                        ' 20150208:SRV_D_MIKUM_KNISA = SRV_D_MIKUM
                        SRV_D_MIKUM_KNISA = "000"
                    Else
                        SRV_D_KNISA_X = "0000"
                        ' 20150208:SRV_D_MIKUM_KNISA = SRV_D_MIKUM
                        SRV_D_MIKUM_KNISA = "000"
                        SRV_D_YETZIA_X = "0000"
                        ' 20150208:SRV_D_MIKUM_YETZIA = SRV_D_MIKUM
                        SRV_D_MIKUM_YETZIA = "000"
                        'todo: error in-out kod
                    End If
                    'prepare line for Agtan:
                    '7769062014100646100095902106115180100150702106115180000000000000000000000        99001000000000101  
                    '7769062015020846100072901106115180000000000000000000000000000000000000000        99001000000000100  
                    Outline = ""
                    Outline = SRV_D_ISHI & "0"
                    Outline = Outline & SRV_D_TAARICH & "00000"
                    Outline = Outline & SRV_D_KNISA_X & "00000000"
                    Outline = Outline & SRV_D_MIKUM_KNISA & "000"
                    Outline = Outline & SRV_D_YETZIA_X & "00000000"
                    Outline = Outline & SRV_D_MIKUM_YETZIA
                    Outline = Outline & "000000000000000000000        9900100000000"
                    If Not SRV_D_MIKUM_KNISA = "000" Then
                        Outline = Outline & "01"
                    Else
                        Outline = Outline & "00"
                    End If
                    If Not SRV_D_MIKUM_YETZIA = "000" Then
                        Outline = Outline & "01"
                    Else
                        Outline = Outline & "00"
                    End If
                    Outline = Outline & "  "
                    sw.WriteLine(Outline)
                Catch ex As Exception
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Agtan aborted" & ex.Message, 3)
                    Throw ex
                End Try
                line = sr.ReadLine
                While Trim(line) = "" And Not sr.EndOfStream
                    line = sr.ReadLine
                End While
            End While
            sw.Close()

            LoadKdsFile(OutFile)
            Return OutFile

        Catch ex As Exception
            sw.Close()
            oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Agtan aborted line " & ex.Message, 3)
            Throw ex
        Finally
            sr.Close()
        End Try
    End Function


    '20141116:
    Public Sub LoadRecMeshek(ByVal line)

        Dim oDal As clDal
        Dim ds As DataSet
        Dim ds1 As DataSet
        Dim ds2 As DataSet
        Dim ds3 As DataSet
        Dim ds4 As DataSet
        Dim SRV_D_ISHI As String
        Dim SRV_D_TAARICH As String
        Dim SRV_D_KNISA_X As String
        Dim SRV_D_MIKUM_KNISA As String
        Dim SRV_D_YETZIA_X As String
        Dim SRV_D_MIKUM_YETZIA As String
        Dim NumLInDS As Integer
        Dim KY As Boolean
        Dim KNull As Boolean
        Dim EfesY As Boolean
        Dim K As Boolean
        Dim Y As Boolean
        Dim UpdK As Boolean
        Dim UpdY As Boolean
        Dim UpdKY As Boolean
        Dim UpdYK As Boolean
        Dim Knisa As Integer
        Dim Yetzia As Integer
        Dim NewK As Boolean
        Dim NewY As Boolean


        Try
            SRV_D_ISHI = Mid(line, 1, 5)
            SRV_D_TAARICH = Mid(line, 7, 8) 'format=yyyymmdd
            SRV_D_KNISA_X = Mid(line, 20, 4) 'format=hhmm
            SRV_D_YETZIA_X = Mid(line, 38, 4)
            If Not (Mid(line, 32, 3) = "000" Or Trim(Mid(line, 32, 3)) = "") And Trim(Mid(line, 95, 2)) = "" Then
                SRV_D_MIKUM_KNISA = Mid(line, 32, 3) & "00"
            ElseIf ((Mid(line, 32, 3) = "000" Or Trim(Mid(line, 32, 3)) = "")) Then
                SRV_D_MIKUM_KNISA = Mid(line, 32, 3) & "00"
            Else
                SRV_D_MIKUM_KNISA = Mid(line, 32, 3) & Mid(line, 95, 2)
            End If
            If Not (Mid(line, 50, 3) = "000" Or Trim(Mid(line, 50, 3)) = "") And Trim(Mid(line, 97, 2)) = "" Then
                SRV_D_MIKUM_YETZIA = Mid(line, 50, 3) & "00"
            ElseIf ((Mid(line, 50, 3) = "000" Or Trim(Mid(line, 50, 3)) = "")) Then
                SRV_D_MIKUM_YETZIA = Mid(line, 50, 3) & "00"
            Else
                SRV_D_MIKUM_YETZIA = Mid(line, 50, 3) & Mid(line, 97, 2)
            End If

            ds = New DataSet
            ds1 = New DataSet
            ds2 = New DataSet
            ds3 = New DataSet
            ds4 = New DataSet
            oDal = New clDal

            oDal.ClearCommand()
            oDal.AddParameter("pIshi", ParameterType.ntOracleInteger, CInt(SRV_D_ISHI), ParameterDir.pdInput)
            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
            oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
            oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
            oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
            oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("Pkg_clock.pro_GetRowMeshek", ds)

            NumLInDS = 0
            KY = False
            K = False
            Y = False
            EfesY = False
            KNull = False
            UpdK = False
            UpdY = False
            UpdKY = False
            UpdYK = False
            NewK = False
            NewY = False

            NumLInDS = ds.Tables(0).Rows.Count
            If Not NumLInDS = 0 Then
                If ds.Tables(0).Rows(0).Item("KY").ToString = "1" Then
                    KY = True
                End If
                If ds.Tables(0).Rows(0).Item("KNull").ToString = "1" Then
                    KNull = True
                End If
                If ds.Tables(0).Rows(0).Item("EfesY").ToString = "1" Then
                    EfesY = True
                End If
                If ds.Tables(0).Rows(0).Item("K").ToString = "1" Then
                    K = True
                End If
                If ds.Tables(0).Rows(0).Item("Y").ToString = "1" Then
                    Y = True
                End If
            Else
                'totd: error!
            End If

            If Trim(SRV_D_KNISA_X) = "" Then
                Knisa = 0
            Else
                Knisa = CInt(Trim(SRV_D_KNISA_X))
            End If

            If Trim(SRV_D_YETZIA_X) = "" Then
                Yetzia = 0
            Else
                Yetzia = CInt(Trim(SRV_D_YETZIA_X))
            End If

            If Knisa > 0 And Yetzia > 0 Then
                If KY Then
                    'exit, complete dup
                ElseIf KNull Then
                    ' upd out
                    UpdYK = True
                ElseIf EfesY Then
                    ' upd in
                    UpdKY = True
                ElseIf K And (Not Y) Then
                    ' new in & out, err dup in
                    NewK = True
                    NewY = True
                ElseIf Y And (Not K) Then
                    'new in & out, err dup out
                    NewK = True
                    NewY = True
                ElseIf (Not K) And (Not Y) Then
                    'new in & out
                    NewK = True
                    NewY = True
                Else
                    'totd: error!
                End If
            ElseIf Knisa > 0 And Yetzia = 0 Then
                If KNull Then
                    'exit, complete dup
                ElseIf K Then
                    'exit, complete dup
                ElseIf (Not K) Then
                    oDal.ClearCommand()
                    oDal.AddParameter("pIshi", ParameterType.ntOracleInteger, CInt(SRV_D_ISHI), ParameterDir.pdInput)
                    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                    oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                    oDal.ExecuteSP("Pkg_clock.pro_GetRowEfesYMeshek", ds1)
                    'check efes & y>knisa -> upd in,
                    'else new in, out=null
                    If ds1.Tables(0).Rows.Count > 0 Then
                        If Not ds1.Tables(0).Rows(0).Item("ct").ToString = "0" Then
                            UpdK = True
                        Else
                            oDal.ClearCommand()
                            oDal.AddParameter("pIshi", ParameterType.ntOracleInteger, CInt(SRV_D_ISHI), ParameterDir.pdInput)
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                            oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("Pkg_clock.pro_GetYNullMeshek", ds3)
                            If ds3.Tables(0).Rows.Count > 0 Then
                                If Len(ds3.Tables(0).Rows(0).Item("gmar").ToString) = 12 Then
                                    SRV_D_YETZIA_X = Mid(ds3.Tables(0).Rows(0).Item("gmar").ToString, 9, 4)
                                    UpdKY = True
                                Else
                                    NewK = True
                                End If
                            Else
                                'err, 
                                NewK = True
                            End If
                        End If
                    Else
                        'todo: err
                    End If
                Else
                    'totd: error!
                End If
            ElseIf Knisa = 0 And Yetzia > 0 Then
                If EfesY Then
                    'exit, complete dup
                ElseIf Y Then
                    'exit, complete dup
                ElseIf (Not Y) Then
                    oDal.ClearCommand()
                    oDal.AddParameter("pIshi", ParameterType.ntOracleInteger, CInt(SRV_D_ISHI), ParameterDir.pdInput)
                    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                    oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                    oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                    oDal.ExecuteSP("Pkg_clock.pro_GetRowKNullMeshek", ds2)
                    'todo: check null & k<yetzia -> upd out,
                    'else new out, in=00010101
                    If ds2.Tables(0).Rows.Count > 0 Then
                        If Not ds2.Tables(0).Rows(0).Item("ct").ToString = "0" Then
                            UpdY = True
                        Else
                            oDal.ClearCommand()
                            oDal.AddParameter("pIshi", ParameterType.ntOracleInteger, CInt(SRV_D_ISHI), ParameterDir.pdInput)
                            oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                            oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("Pkg_clock.pro_GetKNullMeshek", ds4)
                            If ds4.Tables(0).Rows.Count > 0 Then
                                If Len(ds4.Tables(0).Rows(0).Item("knisa").ToString) = 12 Then
                                    SRV_D_KNISA_X = Mid(ds4.Tables(0).Rows(0).Item("knisa").ToString, 9, 4)
                                    UpdYK = True
                                Else
                                    NewY = True
                                End If
                            Else
                                NewY = True
                                'todo: err
                            End If
                        End If
                    End If
                End If
            End If




            If NewK And NewY Then
                'new both
                oDal.ClearCommand()
                oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, "99031", ParameterDir.pdInput)
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_YETZIA, ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_clock.pro_new_recMeshek")
            ElseIf NewK Then
                'new in, out=null
                oDal.ClearCommand()
                oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, "99031", ParameterDir.pdInput)
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_clock.pro_new_recMeshekIN")
            ElseIf NewY Then
                'new out, in=00010101
                oDal.ClearCommand()
                oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("calc_D_new_sidur", ParameterType.ntOracleVarchar, "99031", ParameterDir.pdInput)
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_YETZIA, ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_clock.pro_new_recMeshekOUT")
            ElseIf UpdK Then
                'update knisa where knisa < 00020101 
                oDal.ClearCommand()
                oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_clock.pro_UpdEfesKMeshek")
            ElseIf UpdKY Then
                'update knisa where knisa < 00020101 
                oDal.ClearCommand()
                oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_KNISA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_KNISA, ParameterDir.pdInput)
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_clock.pro_UpdEfesKYMeshek")
            ElseIf UpdY Then
                'update yetzia where gmar is null
                oDal.ClearCommand()
                oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_YETZIA, ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_clock.pro_UpdYNullMeshek")
            ElseIf UpdYK Then
                'update yetzia where gmar is null
                oDal.ClearCommand()
                oDal.AddParameter("SRV_D_ISHI", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_TAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA_X, 3, 2)), ParameterDir.pdInput)
                oDal.AddParameter("SRV_D_MIKUM_YETZIA", ParameterType.ntOracleVarchar, SRV_D_MIKUM_YETZIA, ParameterDir.pdInput)
                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 1, 2)), ParameterDir.pdInput)
                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_KNISA_X, 3, 2)), ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_clock.pro_UpdKYNullMeshek")

            Else
                'todo: err
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Sub TryKdsFileMeshek()

        Dim FileName As String = "DO1-KDS*.OLD"
        Dim InPath As String = "\\KDSTEST\Files\inkds_old\201411.old\"
        Dim MyFile As String
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim strErrorOfFiles As String = String.Empty
        Dim ErrorCounter As Integer = 0


        Try
            MyFile = Dir(InPath & FileName)
            If Not MyFile = "" Then

                While Not MyFile = ""
                    Try
                        LoadKdsFileMeshek(MyFile)

                        'File.Copy(InPath & MyFile, InPath & "201411.old\" & MyFile, True)
                        'File.Delete(InPath & MyFile)
                    Catch ex As Exception
                        ErrorCounter = ErrorCounter + 1
                        strErrorOfFiles = strErrorOfFiles & ErrorCounter.ToString() & "." & ex.Message & vbCr
                    End Try

                    MyFile = Dir()
                End While
                If (strErrorOfFiles <> String.Empty) Then
                    Throw New Exception(strErrorOfFiles)
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub LoadKdsFileMeshek(ByVal MyFile)

        Dim oDal As clDal
        Dim sr As StreamReader
        Dim ds As DataSet
        Dim InPathNFile As String
        Dim line As String
        Dim SRV_D_ISHI As String
        Dim SRV_D_new_sidur As String
        Dim SRV_D_TAARICH As String
        Dim NumLInDS As Integer

        Try
            'InPathNFile = "\\KDSTEST\Files\inkds_old\201411.old\" & MyFile
            InPathNFile = ConfigurationSettings.AppSettings("KdsFilePath") & "inkds_old\201411.old\" & MyFile
            oDal = New clDal
            sr = New StreamReader(InPathNFile)
            line = sr.ReadLine
            While Not line Is Nothing
                SRV_D_ISHI = Mid(line, 1, 5)
                SRV_D_new_sidur = Mid(line, 82, 5)
                SRV_D_TAARICH = Mid(line, 7, 8) 'format=yyyymmdd
                If SRV_D_new_sidur = "99001" Then
                    '20141119: sidur meshek
                    ds = Nothing
                    ds = New DataSet
                    oDal.ClearCommand()
                    oDal.AddParameter("pDt", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                    oDal.AddParameter("pIshi", ParameterType.ntOracleVarchar, SRV_D_ISHI, ParameterDir.pdInput)
                    oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                    oDal.ExecuteSP("PKG_BATCH.pro_IfSidurMeshek", ds)
                    NumLInDS = ds.Tables(0).Rows.Count
                    If Not NumLInDS = 0 Then
                        If ds.Tables(0).Rows(0).Item("cntMeshek").ToString = "1" Then
                            'todo: calc_D_new_sidur = "99031"
                            LoadRecMeshek(line)
                        End If
                    End If
                End If
                line = sr.ReadLine
            End While

        Catch ex As Exception
            Throw ex
        Finally
            sr.Close()
        End Try
    End Sub

    Public Sub TryKdsFileHarmony()

        'in web.config & app.config key="KdsInputFileNameHarmony" default value="A01sn_buffer*.TXT"
        Dim FileName As String = ConfigurationSettings.AppSettings("KdsInputFileNameHarmony")
        If Trim(FileName) = "" Then
            FileName = "A01SN_BUFFER_CLOCK_*.DAT"
        End If
        Dim InPath As String = ConfigurationSettings.AppSettings("KdsFilePath") '"\\KDSTEST\Files"
        Dim SubFolder As String = ConfigurationSettings.AppSettings("KdsFileSubPath") '"inkds_old\"
        Dim oDal As clDal
        Dim FileNameOld As String
        Dim MyFile As String
        Dim ShaonimNumber As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim strErrorOfFiles As String = String.Empty
        Dim ErrorCounter As Integer = 0
        'Dim NewMyFile As String


        Try
            MyFile = Dir(InPath & FileName)
            If Not MyFile = "" Then
                ShaonimNumber = oBatch.InsertProcessLog(2, 2, KdsLibrary.BL.RecordStatus.Wait, "Harmony", 0)

                While Not MyFile = ""
                    Try
                        LoadKdsFileHarmony(MyFile, ShaonimNumber)
                    Catch ex As Exception
                        ErrorCounter = ErrorCounter + 1
                        strErrorOfFiles = strErrorOfFiles & ErrorCounter.ToString() & "." & ex.Message & vbCr
                    End Try
                    FileNameOld = Left(MyFile, Len(MyFile) - 4) & ".old"
                    File.Copy(InPath & MyFile, InPath & SubFolder & FileNameOld, True)
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after copy Harmony " & MyFile, 0)
                    File.Delete(InPath & MyFile)
                    oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after delete Harmony " & MyFile, 0)

                    oDal = New clDal
                    oDal.ClearCommand()
                    oDal.AddParameter("pfilename", ParameterType.ntOracleVarchar, Left(MyFile, Len(MyFile) - 4), ParameterDir.pdInput)
                    oDal.ExecuteSP("Pkg_Attendance.Ins_TahalichHarmony")

                    'FileNameOld = Left(MyFile, Len(MyFile) - 4) & ".old"
                    'File.Copy(InPath & MyFile, InPath & SubFolder & FileNameOld, True)
                    'oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after copy HarmonyNewMyFile " & MyFile, 0)
                    'File.Delete(InPath & MyFile)
                    'oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, " after delete HarmonyNewMyFile " & MyFile, 0)

                    MyFile = Dir()
                End While
                oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Finish, "Harmony", 0)
                If (strErrorOfFiles <> String.Empty) Then
                    Throw New Exception(strErrorOfFiles)
                End If
            End If

        Catch ex As Exception
            oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Harmony aborted" & ex.Message, 3)
            Throw ex
        End Try
    End Sub

    Public Sub LoadKdsFileHarmony(ByVal InFileName, ByVal ShaonimNumber)

        Dim oDal As clDal
        Dim sr As StreamReader
        'Dim sw As StreamWriter
        Dim ds As DataSet
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Dim InPathNFile As String
        'Dim ErrFileName As String
        Dim line As String
        Dim Status_data As String
        Dim clock_num As String
        Dim status_ans As String
        Dim clock_inner_num As String
        Dim date_data As String
        Dim SwIsOpen As Boolean
        Dim Restline As String
        Dim i As Integer
        Dim action_kod As String
        Dim tbl_num As String
        Dim SRV_D_ISHI As String
        Dim SRV_D_ISHI_chk As String
        Dim SRV_D_time As String
        Dim site_kod As String
        Dim clock_num_in_site As String
        Dim clock_name As String
        Dim rec_time_stmp As String
        Dim EndOfline As String
        Dim LineErrCnt As Integer
        Dim SwBakara As Boolean
        Dim SwOk As Boolean
        Dim Err_kod As Integer
        Dim Err_filename As String
        Dim Err_pref As String
        Dim Err_line As String
        Dim Err_suff As String


        Try
            'ErrFileName = ConfigurationSettings.AppSettings("KdsFilePath") & "Harmony" & CStr(Now.Year) & CStr(Now.Month) & CStr(Now.Day) & CStr(Now.Hour) & CStr(Now.Minute) & CStr(Now.Second) & ".err"
            InPathNFile = ConfigurationSettings.AppSettings("KdsFilePath") & InFileName
            Err_filename = Mid(InFileName, 20, Len(InFileName) - 23)
            oDal = New clDal
            sr = New StreamReader(InPathNFile)
            SwIsOpen = False
            SwBakara = False
            SwOk = False
            Err_kod = 0
            Err_pref = ""
            Err_line = ""
            Err_suff = ""
            LineErrCnt = 0
            line = sr.ReadLine
            If Trim(line) = "" Then
                line = sr.ReadLine
                LineErrCnt = LineErrCnt + 1
            End If
            'd0080051160615F4606056181130A1006056181130                                                                                          10601ASHKLN2015061611304368<  
            '123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
            '         1         2         3         4         5         6         7         8         9         1         1         1         1         1         1        
            '                                                                                                   0         1         2         3         4         5
            While Not ((Trim(line) Is Nothing) Or (Trim(line) = ""))
                If line.Length < 155 Then
                    'todo: line is not complete!
                Else
                    'prefix:
                    Status_data = Mid(line, 1, 1)
                    clock_num = Mid(line, 2, 1)
                    status_ans = Mid(line, 3, 1)    'z=not ok
                    clock_inner_num = Mid(line, 4, 5)
                    date_data = Mid(line, 9, 6)     'ddmmyy check validity
                    If (status_ans = "Z" Or status_ans = "z") Then
                        'z=not ok, do not check * אם המערכת של סינאל זיהתה זבל וסימנה z בתו השלישי
                        'If SwIsOpen = False Then
                        '    sw = New StreamWriter(ErrFileName, False)
                        '    SwIsOpen = True
                        'End If
                        'sw.WriteLine("status_ans zevel " & date_data & " LineErrCnt=" & LineErrCnt & " ," & line)
                        oDal.ClearCommand()
                        Err_kod = 1
                        Err_pref = Mid(line, 1, 14)
                        Err_line = Mid(line, 15, 98)
                        Err_suff = Mid(line, 15 + 118, 23)
                        oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                        oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                        oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                    ElseIf clGeneral.IsNumeric(clock_inner_num.ToString) = False Then
                        '* אם המספר הפנימי של השעון והתאריך (תווים 4 – 13) אינם נומרים.
                        oDal.ClearCommand()
                        Err_kod = 2
                        Err_pref = Mid(line, 1, 14)
                        Err_line = Mid(line, 15, 98)
                        Err_suff = Mid(line, 15 + 118, 23)
                        oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                        oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                        oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                        'sw.WriteLine("clock_inner_num not numeric " & date_data & " LineErrCnt=" & LineErrCnt & " ," & line)
                    ElseIf clGeneral.IsNumeric(date_data.ToString) = False Then
                        oDal.ClearCommand()
                        Err_kod = 3
                        Err_pref = Mid(line, 1, 14)
                        Err_line = Mid(line, 15, 98)
                        Err_suff = Mid(line, 15 + 118, 23)
                        oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                        oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                        oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                        'sw.WriteLine("date_data not numeric " & date_data & " LineErrCnt=" & LineErrCnt & " ," & line)
                        'check date validity:
                    ElseIf CInt(Mid(date_data, 1, 2)) > 31 Then
                            'date not valid
                        oDal.ClearCommand()
                        Err_kod = 4
                        Err_pref = Mid(line, 1, 14)
                        Err_line = Mid(line, 15, 98)
                        Err_suff = Mid(line, 15 + 118, 23)
                        oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                        oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                        oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                        'sw.WriteLine("dd not valid " & date_data & " LineErrCnt=" & LineErrCnt & " ," & line)
                    ElseIf CInt(Mid(date_data, 3, 2)) > 12 Then
                        oDal.ClearCommand()
                        Err_kod = 5
                        Err_pref = Mid(line, 1, 14)
                        Err_line = Mid(line, 15, 98)
                        Err_suff = Mid(line, 15 + 118, 23)
                        oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                        oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                        oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                        'sw.WriteLine("mm not valid " & date_data & " LineErrCnt=" & LineErrCnt & " ," & line)
                    ElseIf CInt(date_data) < 10115 Then
                        oDal.ClearCommand()
                        Err_kod = 6
                        Err_pref = Mid(line, 1, 14)
                        Err_line = Mid(line, 15, 98)
                        Err_suff = Mid(line, 15 + 118, 23)
                        oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                        oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                        oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                        oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                        'sw.WriteLine("date not valid " & date_data & " LineErrCnt=" & LineErrCnt & " ," & line)

                        'todo: check line 184 for empty line (almost)
                    Else
                        'suffix:
                        EndOfline = Mid(line, 15 + (14 * 7) + 20, 23)
                        site_kod = Mid(EndOfline, 1, 3)
                        clock_num_in_site = Mid(EndOfline, 4, 2)
                        clock_name = Mid(EndOfline, 6, 6)
                        rec_time_stmp = Mid(EndOfline, 12, 12) 'yyyymmddhhmmssss    'date not checked due to irrelevancy!!

                        Try
                            i = 0
                            Restline = Mid(line, 15 + 14 * i, 14)
                            While Not ((Trim(Restline) Is Nothing) Or (Trim(Restline) = "") Or i > 7)
                                SwBakara = False
                                SwOk = False
                                action_kod = Mid(Restline, 1, 1)
                                tbl_num = Mid(Restline, 2, 3)

                                If clGeneral.IsNumeric(Mid(Restline, 5, 5)) = False Then
                                    'mispar_ishi not valid
                                    SwOk = True
                                    oDal.ClearCommand()
                                    Err_kod = 7
                                    Err_pref = Mid(line, 1, 14)
                                    Err_line = Mid(line, 15, 98)
                                    Err_suff = Mid(line, 15 + 118, 23)
                                    oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                                    oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                                    'sw.WriteLine("mispar_ishi not valid " & SRV_D_ISHI & " LineErrCnt=" & LineErrCnt & " ," & line)
                                Else
                                    SRV_D_ISHI = Mid(Restline, 5, 5)
                                End If
                                SRV_D_ISHI_chk = Mid(Restline, 10, 1) ' bikoret
                                If SRV_D_ISHI_chk = "-" Then
                                    SwBakara = True
                                End If
                                SRV_D_time = Mid(Restline, 11, 4) 'format=hhmm
                                'check date validity:
                                If CInt(Mid(SRV_D_time, 1, 2)) > 23 Then
                                    'time not valid
                                    SwOk = True
                                    oDal.ClearCommand()
                                    Err_kod = 8
                                    Err_pref = Mid(line, 1, 14)
                                    Err_line = Mid(line, 15, 98)
                                    Err_suff = Mid(line, 15 + 118, 23)
                                    oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                                    oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                                    'sw.WriteLine("hh not valid " & SRV_D_time & " LineErrCnt=" & LineErrCnt & " ," & line)
                                ElseIf CInt(Mid(SRV_D_time, 3, 2)) > 59 Then
                                    'time not valid
                                    SwOk = True
                                    oDal.ClearCommand()
                                    Err_kod = 9
                                    Err_pref = Mid(line, 1, 14)
                                    Err_line = Mid(line, 15, 98)
                                    Err_suff = Mid(line, 15 + 118, 23)
                                    oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                                    oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                                    oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                                    'sw.WriteLine("mm not valid " & SRV_D_time & " LineErrCnt=" & LineErrCnt & " ," & line)
                                End If

                                If SwOk = False And SwBakara = False Then
                                    'LoadHarmony(prepare s.p.):
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, SRV_D_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, Mid(date_data, 1, 4) + "20" + Mid(date_data, 5, 2), ParameterDir.pdInput)
                                    oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, Mid(SRV_D_time, 1, 4), ParameterDir.pdInput)
                                    oDal.AddParameter("pMispar_shaon", ParameterType.ntOracleInteger, clock_num, ParameterDir.pdInput)
                                    oDal.AddParameter("pMISPAR_ISHI_chk", ParameterType.ntOracleInteger, SRV_D_ISHI_chk, ParameterDir.pdInput)
                                    oDal.AddParameter("pSTATUS_data", ParameterType.ntOracleVarchar, Status_data, ParameterDir.pdInput)
                                    oDal.AddParameter("pstatus_ans", ParameterType.ntOracleInteger, status_ans, ParameterDir.pdInput)
                                    oDal.AddParameter("pclock_inner_num", ParameterType.ntOracleInteger, clock_inner_num, ParameterDir.pdInput)
                                    oDal.AddParameter("psite_kod", ParameterType.ntOracleInteger, site_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pclock_num_in_site", ParameterType.ntOracleInteger, clock_num_in_site, ParameterDir.pdInput)
                                    oDal.AddParameter("pclock_name", ParameterType.ntOracleVarchar, clock_name, ParameterDir.pdInput)
                                    oDal.AddParameter("prec_time_stmp", ParameterType.ntOracleVarchar, rec_time_stmp, ParameterDir.pdInput)
                                    oDal.AddParameter("paction_kod", ParameterType.ntOracleVarchar, action_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("ptbl_num", ParameterType.ntOracleVarchar, tbl_num, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony")
                                End If
                                i = i + 1
                                Restline = Mid(line, 15 + 14 * i, 14)
                            End While
                        Catch ex As Exception
                            'oBatch.UpdateProcessLog(ShaonimNumber, KdsLibrary.BL.RecordStatus.Faild, "Harmony aborted line " & LineErrCnt.ToString & ex.Message, 3)
                            'Throw ex
                            oDal.ClearCommand()
                            Err_kod = 10
                            Err_pref = Mid(line, 1, 14)
                            Err_line = Mid(line, 15, 98)
                            Err_suff = Mid(line, 15 + 118, 23)
                            oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, Err_filename, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, Err_pref, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, Err_line, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, Err_suff, ParameterDir.pdInput)
                            oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, LineErrCnt, ParameterDir.pdInput)
                            oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                            'sw.WriteLine("Restline not valid LineErrCnt=" & LineErrCnt & " ,Restline=" & Restline)
        End Try

                    End If
                End If
                line = sr.ReadLine
                LineErrCnt = LineErrCnt + 1
                While Trim(line) = "" And Not sr.EndOfStream
                    line = sr.ReadLine
                    LineErrCnt = LineErrCnt + 1
                End While
            End While

        Catch ex As Exception
            Throw ex
        Finally
            sr.Close()
            'If SwIsOpen = True Then
            '    oBatch.InsertProcessLog(2, 1, KdsLibrary.BL.RecordStatus.Faild, "Harmony " & Mid(InFileName, 19, Len(InFileName) - 4) & " " & ErrFileName, 3)
            '    sw.Close()
            '    Throw New Exception("Failure Harmony :" & InFileName)
            'End If
        End Try

    End Sub

    Public Sub MatchAttendHarmony()

        Dim oDal As clDal
        Dim ds As DataSet
        Dim ds_h As DataSet
        Dim ds_k As DataSet
        Dim ds_y As DataSet
        Dim ds_kg As DataSet
        Dim ds_yg As DataSet
        Dim ds2 As DataSet
        Dim ds3 As DataSet
        Dim ds4 As DataSet
        Dim ds5 As DataSet
        Dim SRV_D_TAARICH As String
        Dim SRV_D_YETZIA As String
        Dim SRV_D_knisA As String
        Dim ISTATUS As Integer
        Dim Iteur As String
        Dim NumLInDS As Integer
        Dim inMISPAR_ISHI As String
        Dim inTAARICH As String
        Dim inShaa As String
        Dim inaction_kod As String
        Dim intbl_num As String
        Dim SugRec As Integer
        Dim isite_kod As String
        Dim istm As String
        Dim efes As String
        Dim pmispar_sidur As String
        Dim iclock_num_in_site As String
        Dim sw_ok As Boolean
        Dim p24 As Integer
        'Dim Err_kod As Integer

        Try
            oDal = New clDal
            ds = New DataSet
            NumLInDS = 0
            efes = "0000"
            sw_ok = True
            'Err_kod = 0

            SRV_D_TAARICH = getFullDateString(Now) & "0000"
            ISTATUS = 0
            If Now.Hour < 10 Then
                SRV_D_TAARICH = getFullDateString(Now) & "0" & Now.Hour.ToString
            Else
                SRV_D_TAARICH = getFullDateString(Now) & Now.Hour.ToString
            End If
            If Now.Minute < 10 Then
                SRV_D_TAARICH = SRV_D_TAARICH & "0" & Now.Minute.ToString
            Else
                SRV_D_TAARICH = SRV_D_TAARICH & Now.Minute.ToString
            End If


            oDal.ClearCommand()
            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
            oDal.ExecuteSP("Pkg_Attendance.pro_fetch_AttendHarmony", ds)
            NumLInDS = ds.Tables(0).Rows.Count
            If NumLInDS > 0 Then
                Iteur = "AttendHarmony now"
                oDal.ClearCommand()
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("pSTATUS", ParameterType.ntOracleVarchar, ISTATUS, ParameterDir.pdInput)
                oDal.AddParameter("pteur", ParameterType.ntOracleVarchar, Iteur, ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_Attendance.pro_new_control_Attend")
                For i = 0 To NumLInDS - 1

                    'extracted fields: MISPAR_ISHI ,  TAARICH   ,  Shaa , Mispar_shaon,  MISPAR_ISHI_chk ,moed,
                    'STATUS_data, Status_ans, clock_inner_num, site_kod, clock_num_in_site,
                    'clock_name, rec_time_stmp, action_kod, tbl_num, taarich_spare, spare1, spare2,
                    'MEADKEN_ACHARON, taarich_klita
                    ds_h = New DataSet
                    ds_k = New DataSet
                    ds_y = New DataSet
                    ds_kg = New DataSet
                    ds_yg = New DataSet
                    ds2 = New DataSet
                    ds3 = New DataSet
                    ds4 = New DataSet
                    ds5 = New DataSet
                    SugRec = 0
                    pmispar_sidur = 99200
                    sw_ok = True
                    p24 = 0
                    'Err_kod = 0


                    inMISPAR_ISHI = ds.Tables(0).Rows(i).Item("MISPAR_ISHI").ToString
                    'If CInt(inMISPAR_ISHI) = 0 Then
                    '    sw_ok = False
                    '    Err_kod = 78
                    'End If
                    inTAARICH = ds.Tables(0).Rows(i).Item("TAARICH").ToString 'dd/mm/yyyy
                    'If CInt(Mid(inTAARICH, 7, 4)) > Now.Year Then
                    '    sw_ok = False
                    '    Err_kod = 79
                    'End If
                    inShaa = ds.Tables(0).Rows(i).Item("Shaa").ToString
                    If Len(inShaa) < 4 Then
                        inShaa = Left(efes, (4 - Len(inShaa))) & inShaa
                    End If
                    inaction_kod = ds.Tables(0).Rows(i).Item("action_kod").ToString
                    intbl_num = ds.Tables(0).Rows(i).Item("tbl_num").ToString

                    iclock_num_in_site = Trim(ds.Tables(0).Rows(i).Item("clock_num_in_site").ToString)
                    If Len(iclock_num_in_site) < 2 Then
                        iclock_num_in_site = Left(efes, (2 - Len(iclock_num_in_site))) & iclock_num_in_site
                    End If
                    isite_kod = ds.Tables(0).Rows(i).Item("site_kod").ToString & iclock_num_in_site
                    istm = ds.Tables(0).Rows(i).Item("rec_time_stmp").ToString

                    'If sw_ok = False Then
                    '    'todo: send (mail?) error code
                    '    oDal.ClearCommand()
                    '    oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, Err_kod, ParameterDir.pdInput)
                    '    oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                    '    oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, inaction_kod & " " & intbl_num, ParameterDir.pdInput)
                    '    oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, inTAARICH & " " & inShaa & " " & iclock_num_in_site & " " & isite_kod, ParameterDir.pdInput)
                    '    oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                    '    oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, 77, ParameterDir.pdInput)
                    '    oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                    'End If


                    If ((inaction_kod = "D" And Not intbl_num = "460" And Not intbl_num = "440") Or inaction_kod = "G") Then 'hityazvut.
                        SugRec = 1
                        pmispar_sidur = 99200
                    ElseIf inaction_kod = "A" Then 'knisa. check if intbl_num = "100"?
                        SugRec = 2
                        pmispar_sidur = 99001
                    ElseIf inaction_kod = "B" Then 'yetzia. check if intbl_num = "200"?
                        SugRec = 3
                        pmispar_sidur = 99001
                    ElseIf (inaction_kod = "E" Or inaction_kod = "C" Or inaction_kod = "D") And intbl_num = "440" Then 'inGrr.
                        SugRec = 4
                        pmispar_sidur = 99220
                    ElseIf (inaction_kod = "F" Or inaction_kod = "E" Or inaction_kod = "D") And intbl_num = "460" Then 'outGrr.
                        SugRec = 5
                        pmispar_sidur = 99220
                    End If

                    Select Case SugRec
                        Case 1 'hityazvut.
                            oDal.ClearCommand()
                            oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                            oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("Pkg_Attendance.pro_check_In", ds_h)
                            If ds_h.Tables(0).Rows(0).Item(0).ToString = "0" Then
                                'ins hityazvut
                                oDal.ClearCommand()
                                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                                oDal.AddParameter("pMikum", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                oDal.ExecuteSP("Pkg_Attendance.pro_ins_In")
                                'Else  already exists, exit
                            End If
                        Case 2 'knisa.
                            oDal.ClearCommand()
                            oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                            oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("Pkg_Attendance.pro_check_In", ds_k)
                            If ds_k.Tables(0).Rows(0).Item(0).ToString = "0" Then
                                'ins or upd knisa 
                                oDal.ClearCommand()
                                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, inMISPAR_ISHI, ParameterDir.pdInput)
                                oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                oDal.ExecuteSP("Pkg_Attendance.pro_GetOutNull", ds2)
                                If ds2.Tables(0).Rows.Count > 0 Then
                                    If Len(ds2.Tables(0).Rows(0).Item("gmar").ToString) = 12 Then
                                        SRV_D_YETZIA = Mid(ds2.Tables(0).Rows(0).Item("gmar").ToString, 9, 4)
                                    Else
                                        SRV_D_YETZIA = Left(ds2.Tables(0).Rows(0).Item("gmar").ToString, 4)
                                        'todo: ??
                                    End If
                                    'update knisa where knisa is null 
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pMIKUM", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_UpdKnisa")
                                Else
                                    'ins knisa
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                                    oDal.AddParameter("pMikum", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_ins_In")
                                End If
                                'Else  already exists, exit 
                            End If
                        Case 3 'yetzia.
                            p24 = 0
                            oDal.ClearCommand()
                            oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                            oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                            oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("Pkg_Attendance.pro_check_Yetzia", ds_y)
                            'todo: yetzia today after 24:00 before 04:00,  taarich should be yesterday

                            If ds_y.Tables(0).Rows(0).Item(0).ToString = "0" Then
                                'todo: ins or upd yetzia
                                oDal.ClearCommand()
                                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, inMISPAR_ISHI, ParameterDir.pdInput)
                                oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                oDal.ExecuteSP("Pkg_Attendance.pro_GetInNull", ds3)
                                If ds3.Tables(0).Rows.Count > 0 Then
                                    If Len(ds3.Tables(0).Rows(0).Item("knisa").ToString) = 12 Then
                                        SRV_D_knisA = Mid(ds3.Tables(0).Rows(0).Item("knisa").ToString, 9, 4)
                                    Else
                                        SRV_D_knisA = Left(ds3.Tables(0).Rows(0).Item("knisa").ToString, 4)
                                        'todo: ??
                                    End If
                                    'tocheck: update yetzia where yetzia is null 
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_knisA, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_knisA, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pMIKUM", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_UpdYetzia")
                                Else
                                    'ins yetzia
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                                    oDal.AddParameter("pMikum", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_ins_Yetzia")
                                End If
                                'Else  already exists, exit 
                            End If
                        Case 4 'inGrr.
                            oDal.ClearCommand()
                            oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                            oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("Pkg_Attendance.pro_check_In", ds_kg)
                            If ds_kg.Tables(0).Rows(0).Item(0).ToString = "0" Then
                                'todo: ins or upd inGrr
                                oDal.ClearCommand()
                                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, inMISPAR_ISHI, ParameterDir.pdInput)
                                oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                oDal.ExecuteSP("Pkg_Attendance.pro_GetOutNull", ds4)
                                If ds4.Tables(0).Rows.Count > 0 Then
                                    If Len(ds4.Tables(0).Rows(0).Item("gmar").ToString) = 12 Then
                                        SRV_D_YETZIA = Mid(ds4.Tables(0).Rows(0).Item("gmar").ToString, 9, 4)
                                    Else
                                        SRV_D_YETZIA = Left(ds4.Tables(0).Rows(0).Item("gmar").ToString, 4)
                                        'todo: ??
                                    End If
                                    'update knisaGrr where knisa is null 
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pMIKUM", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_YETZIA, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_UpdKnisa")
                                Else
                                    'ins knisaGrr
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                                    oDal.AddParameter("pMikum", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_ins_In")
                                    'Else  already exists, exit 
                                End If
                            End If
                        Case 5 'outGrr.
                            'todo: outGrr today after 24:00 before 04:00,  taarich should be yesterday
                            '31/08/2015:
                            If CInt(inShaa) < 400 Then
                                p24 = 1
                            End If
                            oDal.ClearCommand()
                            oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                            oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                            oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                            oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                            oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                            oDal.ExecuteSP("Pkg_Attendance.pro_check_Yetzia", ds_yg)
                            If ds_yg.Tables(0).Rows(0).Item(0).ToString = "0" Then
                                'todo: ins or upd OutGrr
                                oDal.ClearCommand()
                                oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleInteger, inMISPAR_ISHI, ParameterDir.pdInput)
                                oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, Nothing, ParameterDir.pdOutput)
                                oDal.ExecuteSP("Pkg_Attendance.pro_GetInNull", ds5)
                                If ds5.Tables(0).Rows.Count > 0 Then
                                    If Len(ds5.Tables(0).Rows(0).Item("knisa").ToString) = 12 Then
                                        SRV_D_knisA = Mid(ds5.Tables(0).Rows(0).Item("knisa").ToString, 9, 4)
                                    Else
                                        SRV_D_knisA = Left(ds5.Tables(0).Rows(0).Item("knisa").ToString, 4)
                                        'todo: ??
                                    End If
                                    'update yetziaGrr where yetzia is null 
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaHH", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_knisA, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pKnisaMM", ParameterType.ntOracleInteger, CInt(Mid(SRV_D_knisA, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pMIKUM", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaHH", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 1, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pYetziaMM", ParameterType.ntOracleInteger, CInt(Mid(inShaa, 3, 2)), ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_UpdYetzia")
                                Else
                                    'ins yetziaGrr
                                    oDal.ClearCommand()
                                    oDal.AddParameter("pMISPAR_ISHI", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                                    oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, inTAARICH, ParameterDir.pdInput)
                                    oDal.AddParameter("pShaa", ParameterType.ntOracleVarchar, inShaa, ParameterDir.pdInput)
                                    oDal.AddParameter("pMikum", ParameterType.ntOracleVarchar, isite_kod, ParameterDir.pdInput)
                                    oDal.AddParameter("pmispar_sidur", ParameterType.ntOracleVarchar, pmispar_sidur, ParameterDir.pdInput)
                                    oDal.AddParameter("p24", ParameterType.ntOracleVarchar, p24, ParameterDir.pdInput)
                                    oDal.AddParameter("pStm", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                                    oDal.ExecuteSP("Pkg_Attendance.pro_ins_Yetzia")

                                    'Else  already exists, exit 
                                End If
                            End If
                        Case Else
                            'todo: send (mail?) error code
                            oDal.ClearCommand()
                            oDal.AddParameter("pErr_kod", ParameterType.ntOracleInteger, 77, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_filename", ParameterType.ntOracleVarchar, inMISPAR_ISHI, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_pref", ParameterType.ntOracleVarchar, inaction_kod & " " & intbl_num, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_line", ParameterType.ntOracleVarchar, inTAARICH & " " & inShaa & " " & iclock_num_in_site & " " & isite_kod, ParameterDir.pdInput)
                            oDal.AddParameter("pErr_suff", ParameterType.ntOracleVarchar, istm, ParameterDir.pdInput)
                            oDal.AddParameter("pLineErrCnt", ParameterType.ntOracleInteger, 77, ParameterDir.pdInput)
                            oDal.ExecuteSP("Pkg_Attendance.pro_new_recHarmony_err")
                    End Select
                Next
                'finish: update control
                Iteur = "AttendHarmony finished"
                oDal.ClearCommand()
                oDal.AddParameter("pTAARICH", ParameterType.ntOracleVarchar, SRV_D_TAARICH, ParameterDir.pdInput)
                oDal.AddParameter("pSTATUS", ParameterType.ntOracleVarchar, 2, ParameterDir.pdInput)
                oDal.AddParameter("pteur", ParameterType.ntOracleVarchar, Iteur, ParameterDir.pdInput)
                oDal.ExecuteSP("Pkg_Attendance.pro_Upd_control_Attend")
            End If


        Catch ex As Exception
            Throw ex
            'Finally
        End Try

    End Sub

    Public Sub testvb()
        Dim p_date_str_now As String
        Dim p_stam As String

        p_date_str_now = getFullDateString(Now) & "0000"
  
    End Sub

#End Region

End Class
