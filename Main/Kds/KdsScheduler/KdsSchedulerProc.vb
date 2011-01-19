Imports KdsDataImport
Imports System.Data
Imports System.Configuration
Imports System.Threading
Imports KdsBatch.HrWorkersChanges
Imports System.IO
Imports KdsLibrary

Module KdsSchedulerProc

    Sub Main()
        Dim oBatch As KdsLibrary.BL.clBatch
        Dim oKDs As KdsDataImport.ClKds
        oKDs = New KdsDataImport.ClKds
        oBatch = New KdsLibrary.BL.clBatch
        Dim KdsSchedulerNumber As Integer
        Try

            ''** oKDs.KdsWriteProcessLog(1, 1, 1, "start KdsSchedulerProc")
            KdsSchedulerNumber = oBatch.InsertProcessLog(1, 1, KdsLibrary.BL.RecordStatus.Wait, "KdsSchedulerProc", 0)
            Refresh_N_Sdrn()

            oKDs.Chk_ThreadHrChainges()

            'todo: since the refresh finishes by 3:11, 
            'if the threadHrSeparation will last after 5 or 6, when to run this delete.

            ''תהליך המוחק דוחות ישנים
            If Now.Hour = 3 Then
                Call DeleteHeavyReport()
            End If
            ''כל ראשון בחודש מעבירים לטבלאות הסטוריה נתונים ישנים
            If Now.Day = 1 And Now.Hour = 23 Then
                ''**oKDs.KdsWriteProcessLog(99, 0, 3, "before MoveRecordsToHistory", 9)
                Call MoveRecordsToHistory()
                Call DeleteLogTahalich()
                ''**oKDs.KdsWriteProcessLog(99, 0, 3, "after MoveRecordsToHistory", 9)
            End If

            oBatch.UpdateProcessLog(KdsSchedulerNumber, KdsLibrary.BL.RecordStatus.Finish, "KdsSchedulerProc", 0)
            ''** oKDs.KdsWriteProcessLog(1, 1, 2, "end KdsSchedulerProc")
        Catch ex As Exception
            clGeneral.LogMessage(ex.Message, EventLogEntryType.Error)
            oBatch.UpdateProcessLog(KdsSchedulerNumber, KdsLibrary.BL.RecordStatus.Faild, "KdsSchedulerProc abort", 1)
            ''**oKDs.KdsWriteProcessLog(1, 1, 3, "KdsSchedulerProc abort", 1)
        End Try

    End Sub
   
   
    Sub Refresh_N_Sdrn()
        Dim oKDs As KdsDataImport.ClKds = New KdsDataImport.ClKds
        Dim oCalc As KdsBatch.clCalculation = New KdsBatch.clCalculation
        Dim oDal As KdsLibrary.DAL.clDal
        Dim dt As DataTable
        Dim WhrStr As String
        Dim RetSql As String = ""
        Dim p_TAARICH As String
        Dim tahalich As Integer
        Dim sub_tahalich As Integer
        Dim lRequestNum As Integer
        Dim p_date_str As String
        Dim p_date As Date
        Dim BodyMail As String
        Dim ToMail As String
        Dim runRefresh As Boolean
        Dim SdrnStrtHour As String
        Dim numFailed, numSucceed As Integer
        Dim iProcessRefreshSdrn, iProcessPremiyot, iRefreshMvAndInsYemey As Integer
        oDal = New KdsLibrary.DAL.clDal
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            SdrnStrtHour = ConfigurationSettings.AppSettings("SdrnStrtHour") '4
            If SdrnStrtHour = "" Then
                SdrnStrtHour = "4"
            End If

            iProcessRefreshSdrn = oBatch.InsertProcessLog(1, 1, KdsLibrary.BL.RecordStatus.Wait, "check Refresh_N_Sdrn", 0)
            ''**oKDs.KdsWriteProcessLog(1, 1, 1, "check Refresh_N_Sdrn")

            p_TAARICH = oKDs.getFullDateString(Now)
            
            'the sadran run on yesterday
            p_date = Now.AddDays(-1)
            p_date_str = oKDs.getFullDateString(p_date)
       

            dt = oKDs.GetRowKds("dual", "", "to_char(sysdate,'hh24:mi') kuku", RetSql)
            If dt.Rows.Count = 0 Then
                oBatch.UpdateProcessLog(iProcessRefreshSdrn, KdsLibrary.BL.RecordStatus.Faild, "database not responding", 13)
                ''**oKDs.KdsWriteProcessLog(1, 1, 3, "database not responding", 13)
                'data base not responding
                ToMail = ConfigurationSettings.AppSettings("miri")
                ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                BodyMail = "database not responding"
                oKDs.SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                oBatch.InsertProcessLog(1, 1, KdsLibrary.BL.RecordStatus.SendMail, "mail db", 0)
                ''**  oKDs.KdsWriteProcessLog(1, 1, 6, "mail db")
            Else
                If Now.Hour < 1 Then
                    If Not Now.Hour = CInt(Mid(dt.Rows(0).Item("kuku").ToString, 1, 2)) Then
                        'data base not synchronized
                        'todo: sleep 1 minute
                        oBatch.UpdateProcessLog(iProcessRefreshSdrn, KdsLibrary.BL.RecordStatus.PartialFinish, "sleep 2 synchronized=" & Mid(dt.Rows(0).Item("kuku").ToString, 1, 2) & " now=" & Now.Hour.ToString, 0)
                        ''** oKDs.KdsWriteProcessLog(1, 1, 4, "sleep 2 synchronized=" & Mid(dt.Rows(0).Item("kuku").ToString, 1, 2) & " now=" & Now.Hour.ToString)
                        Thread.Sleep(60000) '= 60 seconds =1 minute
                        dt = oKDs.GetRowKds("dual", "", "to_char(sysdate,'hh24:mi') kuku", RetSql)
                        oBatch.UpdateProcessLog(iProcessRefreshSdrn, KdsLibrary.BL.RecordStatus.PartialFinish, "after sleep " & Mid(dt.Rows(0).Item("kuku").ToString, 1, 2) & " now=" & Now.Hour.ToString, 0)
                        ''**oKDs.KdsWriteProcessLog(1, 1, 4, "after sleep " & Mid(dt.Rows(0).Item("kuku").ToString, 1, 2) & " now=" & Now.Hour.ToString)
                        If dt.Rows.Count > 0 Then
                            If Not Now.Hour = CInt(Mid(dt.Rows(0).Item("kuku").ToString, 1, 2)) Then
                                oBatch.UpdateProcessLog(iProcessRefreshSdrn, KdsLibrary.BL.RecordStatus.Faild, "database not synchronized", 13)
                                ''**  oKDs.KdsWriteProcessLog(1, 1, 3, "database not synchronized", 13)
                                ToMail = ConfigurationSettings.AppSettings("miri")
                                ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                                BodyMail = "database not synchronized sysdate=" & Mid(dt.Rows(0).Item("kuku").ToString, 1, 2) & " now=" & Now.Hour.ToString
                                oKDs.SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                                oBatch.InsertProcessLog(1, 1, KdsLibrary.BL.RecordStatus.SendMail, "mail synchronized=" & Mid(dt.Rows(0).Item("kuku").ToString, 1, 2) & " now=" & Now.Hour.ToString, 0)
                                ''**oKDs.KdsWriteProcessLog(1, 1, 6, "mail synchronized=" & Mid(dt.Rows(0).Item("kuku").ToString, 1, 2) & " now=" & Now.Hour.ToString)
                            End If
                        Else
                            'todo: rais db problems
                            '^'RaiseEvent("ex",Exception) 
                        End If
                    End If
                End If
            End If

            WhrStr = "taarich>trunc(sysdate) and kod_tahalich in ( 3,7)"
            dt = oKDs.GetRowKds("tb_log_tahalich", WhrStr, "*", RetSql)

            'if time is early and refresh not run yet, should run
            If dt.Rows.Count = 0 Then
                runRefresh = False
                If Now.Hour < 4 Then
                    runRefresh = True
                End If
                If runRefresh = True Then
                    'no need 4 date parameter since refresh has no date,
                    'and PKG_BATCH.pro_ins_yamey_avoda_ovdim is of no importance "backwards"
                    ''iRefreshMvAndInsYemey = oBatch.InsertProcessLog(3, 1, KdsLibrary.BL.RecordStatus.Wait, "RunRefresh", 0)
                    oKDs.RunRefresh()
                Else
                    oBatch.UpdateProcessLog(iRefreshMvAndInsYemey, KdsLibrary.BL.RecordStatus.Faild, "RefreshMv & ins_yamey did not run", 8)
                    ''**oKDs.KdsWriteProcessLog(3, sub_tahalich, 3, "RefreshMv & ins_yamey did not run", 8)
                    'write mail that RefreshMv & ins_yamey did not run, or try again?
                    ToMail = ConfigurationSettings.AppSettings("miri")
                    ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
                    BodyMail = "RefreshMv & ins_yamey did not run"
                    oKDs.SendMail(ToMail, "no peace 4 the wicked", BodyMail)
                    oBatch.InsertProcessLog(3, 1, KdsLibrary.BL.RecordStatus.SendMail, "mail ovdim", 0)
                    ''**oKDs.KdsWriteProcessLog(3, sub_tahalich, 6, "mail ovdim")
                    'todo: if/when need retro refresh will do matzav & ins_yom only
                    'If Now.Hour < 14 Then
                    '    oKDs.RunRefreshRetro()
                    'End If
                End If
            Else
                'read the datatable, check each row
                'to ask the ds: datatable.compute(...)
            End If


            If Now.Hour > CInt(SdrnStrtHour) And Now.Hour < 13 Then
                oKDs.RunSdrn(p_date_str) 'yyyymmdd
            End If
            ''  oKDs.KdsWriteProcessLog(98, 0, 1, "check PremiaCalc: " + Now.Hour.ToString())
            If Now.Hour = 21 Then
                numFailed = 0
                numSucceed = 0
                lRequestNum = KdsLibrary.clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.CalculationForPremiaPopulation, "KdsScheduler", -12)
                iProcessPremiyot = oBatch.InsertProcessLog(98, 0, KdsLibrary.BL.RecordStatus.Wait, "PremiaCalc", 0)
                ''**oKDs.KdsWriteProcessLog(98, 0, 1, "before PremiaCalc")
                oCalc.PremiaCalc(lRequestNum, numFailed, numSucceed)
                oBatch.UpdateProcessLog(iProcessPremiyot, KdsLibrary.BL.RecordStatus.Finish, "PremiaCalc NumRowsFailed=" & numFailed & " NumRowsSucceed=" & numSucceed, 0)
                ''**oKDs.KdsWriteProcessLog(98, 0, 1, "after PremiaCalc NumRowsFailed=" & numFailed & " NumRowsSucceed=" & numSucceed)
            End If
            ''רענון טבלאות של התנועה
            If Now.Hour > 21 Then
                oKDs.refresh_from_shmulik()
            End If

            ''קליטת נתונים מקבצי השעונים
            oKDs.TryKdsFile()

            oKDs.RunSdrnRetro("RunBatch")

            oBatch.UpdateProcessLog(iProcessRefreshSdrn, KdsLibrary.BL.RecordStatus.Finish, "Refresh_N_Sdrn", 0)
            '' oKDs.KdsWriteProcessLog(1, 1, 2, "end  Refresh_N_Sdrn")

        Catch ex As Exception
            'todo: get public from clgeneral EventLog.WriteEntry(KdsLibrary.clgeneral.
            clGeneral.LogMessage(ex.Message, EventLogEntryType.Error)
            'todo: is 9=abort?
            oBatch.UpdateProcessLog(iProcessRefreshSdrn, KdsLibrary.BL.RecordStatus.Faild, "Refresh_N_Sdrn abort: " & ex.Message, 0)
            ''**oKDs.KdsWriteProcessLog(tahalich, sub_tahalich, 3, "end " & ex.Message)
            ToMail = ConfigurationSettings.AppSettings("miri")
            ToMail = ToMail & "," & ConfigurationSettings.AppSettings("merav")
            BodyMail = "scheduler aborted"
            oKDs.SendMail(ToMail, "no peace 4 the wicked", BodyMail)
        End Try
    End Sub


    Sub DeleteHeavyReport()
        Dim dt As DataTable
        Dim oKDs As KdsLibrary.BL.clReport
        ' Dim oKDsData As KdsDataImport.ClKds
        Dim path, startPath As String
        Dim i, iDeleteHeavyReport As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            iDeleteHeavyReport = oBatch.InsertProcessLog(20, 1, KdsLibrary.BL.RecordStatus.Wait, "DeleteHeavyReport", 0)
            startPath = ConfigurationSettings.AppSettings("HeavyReportsPath")
            oKDs = New KdsLibrary.BL.clReport

            dt = oKDs.GetHeavyReportsToDelete()
            If dt.Rows.Count > 0 Then
                For i = 0 To (dt.Rows.Count - 1)
                    path = startPath & dt.Rows(i).Item("ReportName").ToString()
                    If File.Exists(path) Then
                        File.Delete(path)
                    End If
                Next
            End If
            oBatch.UpdateProcessLog(iDeleteHeavyReport, KdsLibrary.BL.RecordStatus.Finish, "DeleteHeavyReport", 0)
        Catch ex As Exception
            'oKDsData = New KdsDataImport.ClKds
            oBatch.UpdateProcessLog(iDeleteHeavyReport, KdsLibrary.BL.RecordStatus.Faild, "DeleteHeavyReport faild: " + ex.Message, 9)
            ''**oKDsData.KdsWriteProcessLog(20, 1, 3, "DeleteHeavyReport faild: " + ex.Message, 9)
        End Try
    End Sub
    Sub MoveRecordsToHistory()
        ' Dim oKDs As KdsLibrary.BL.clBatch
        'Dim oKDsData As KdsDataImport.ClKds
        Dim iMoveRecordsToHistory As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            iMoveRecordsToHistory = oBatch.InsertProcessLog(99, 0, KdsLibrary.BL.RecordStatus.Wait, "MoveRecordsToHistory", 0)
            '   oKDs = New KdsLibrary.BL.clBatch
            oBatch.MoveRecordsToHistory(Now.AddMonths(-11))
            oBatch.UpdateProcessLog(iMoveRecordsToHistory, KdsLibrary.BL.RecordStatus.Finish, "MoveRecordsToHistory", 0)
        Catch ex As Exception
            ''**oKDsData = New KdsDataImport.ClKds
            oBatch.UpdateProcessLog(iMoveRecordsToHistory, KdsLibrary.BL.RecordStatus.Faild, "MoveRecordsToHistory faild: " + ex.Message, 14)
            ''**oKDsData.KdsWriteProcessLog(99, 0, 3, "MoveRecordsToHistory faild: " + ex.Message, 9)
        End Try
    End Sub

    Sub DeleteLogTahalich()
        '  Dim oKDs As KdsLibrary.BL.clBatch
        Dim iDeleteSeq As Integer
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try
            iDeleteSeq = oBatch.InsertProcessLog(97, 0, KdsLibrary.BL.RecordStatus.Wait, "DeleteLogThalich", 0)
            '  oKDs = New KdsLibrary.BL.clBatch
            oBatch.DeleteLogTahalichRecords()
            oBatch.UpdateProcessLog(iDeleteSeq, KdsLibrary.BL.RecordStatus.Finish, "DeleteLogThalich", 0)
        Catch ex As Exception
            ''**oKDsData = New KdsDataImport.ClKds
            oBatch.UpdateProcessLog(iDeleteSeq, KdsLibrary.BL.RecordStatus.Faild, "MoveRecordsToHistory faild: " + ex.Message, 14)
            ''**oKDsData.KdsWriteProcessLog(99, 0, 3, "MoveRecordsToHistory faild: " + ex.Message, 9)
        End Try
    End Sub
    Sub Test4Tmp()
        'SdrnStatTimes = ConfigurationSettings.AppSettings("SdrnStatTimes") '3 in test, 2 in prd
        'If SdrnStatTimes = "" Then
        '    SdrnStatTimes = "2"
        'End If
        'If SdrnStatTimes = 3 Then
        'If Now.Month = 6 And Now.Day = 3 And Now.Hour < 12 Then
        '    oKDs = New KdsDataImport.ClKds
        '    oDal = New KdsLibrary.DAL.clDal
        '    Try
        '        sub_tahalich = 28
        '        oKDs.KdsWriteProcessLog(3, 28, 1, "start tmp_meafyeney_elementim")
        '        oDal.ClearCommand()
        '        oDal.ExecuteSP("PKG_ELEMENTS.calling_Pivot_Meafyeney_e")
        '        oKDs.KdsWriteProcessLog(3, 28, 2, "end ok tmp_meafyeney_elementim")
        '    Catch ex As Exception
        '        oKDs.KdsWriteProcessLog(3, 28, 3, "calling_Pivot_Meafyeney_e aborted " & ex.Message)
        '    End Try
        '    Try
        '        sub_tahalich = 29
        '        oKDs.KdsWriteProcessLog(3, 29, 1, "start tmp_meafyeney_sug_sidur")
        '        oDal.ClearCommand()
        '        oDal.ExecuteSP("PKG_SUG_SIDUR.calling_Pivot_Meafyeney_S")
        '        oKDs.KdsWriteProcessLog(3, 29, 2, "end ok tmp_meafyeney_sug_sidur")
        '    Catch ex As Exception
        '        oKDs.KdsWriteProcessLog(3, 29, 3, "calling_Pivot_Meafyeney_S aborted " & ex.Message)
        '    End Try
        '    Try
        '        sub_tahalich = 30
        '        oKDs.KdsWriteProcessLog(3, 30, 1, "start tmp_sidurim_meyuchadim")
        '        oDal.ClearCommand()
        '        oDal.ExecuteSP("PKG_SIDURIM.calling_Pivot_Sidurim_M")
        '        oKDs.KdsWriteProcessLog(3, 30, 2, "end ok tmp_sidurim_meyuchadim")
        '    Catch ex As Exception
        '        oKDs.KdsWriteProcessLog(3, 30, 3, "calling_Pivot_Sidurim_M aborted " & ex.Message)
        '    End Try
        '    'oKDs.RunRefresh()
        '    'Call Test4Tmp()
        'End If
        'End If
        Dim oKDs As KdsDataImport.ClKds
        Dim p_date_str As String
        Dim lRequestNum As Integer
        Dim dTaarich As Date
        Dim dDatestr As String
        Try
            oKDs = New KdsDataImport.ClKds
            p_date_str = "20100503"
            'oKDs.KdsWriteProcessLog(8, 1, 1, "before OpenBatchRequest")
            lRequestNum = KdsLibrary.clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12)
            dDatestr = Mid(p_date_str, 7, 2) & "/" & Mid(p_date_str, 5, 2) & "/" & Mid(p_date_str, 1, 4)
            dTaarich = New DateTime(Mid(p_date_str, 1, 4), Mid(p_date_str, 5, 2), Mid(p_date_str, 7, 2))
            'oKDs.KdsWriteProcessLog(8, 1, 1, "after OpenBatchRequest before shguyim")
            KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcess, KdsBatch.BatchExecutionType.All, dTaarich, lRequestNum)
            'oKDs.KdsWriteProcessLog(8, 1, 2, "after shguyim before ishurim")
            KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(dTaarich, False)
            'oKDs.KdsWriteProcessLog(6, 1, 2, "after ishurim")
        Catch ex As Exception
        End Try
    End Sub

End Module
