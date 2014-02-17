
Imports KdsDataImport
Imports System.Data
Imports System.Net.Mail.MailMessage
Imports System.IO
Imports DalOraInfra.DAL


Partial Class test_vb_test_vb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load



        Dim oKDs As KdsDataImport.ClKds
        Dim oDal As clDal
        'Dim smail As System.Net.Mail.SmtpClient
        Dim dt As DataTable
        Dim tahalich As Integer
        Dim sub_tahalich As Integer
        Dim WhrStr As String
        Dim RetSql As String
        Dim if_strt As String
        Dim if_abort As String
        Dim if_mail As String
        Dim if_ok As String
        Dim dt_strt As Date
        Dim dt_abort As Date
        Dim dt_mail As Date
        Dim dt_ok As Date
        Dim p_TAARICH As String
        Dim p_status As Integer
        'Dim p_status_2 As String
        'Dim if_p_stat As Boolean
        Dim p_date_str As String
        Dim p_date As Date
        Dim nd As Double
        Dim iseq As Integer
        'Dim FromMail As String
        'Dim ToMail As String
        'Dim SubjMail As String
        'Dim BodyMail As String
        'Dim lRequestNum As Integer
        'Dim dTaarich As Date
        'Dim idkun_tb_e As String
        'Dim idkun_tmp_te As String
        'Dim idkun_tb_s As String
        'Dim idkun_tmp_ts As String
        'Dim idkun_tb_m As String
        'Dim idkun_tmp_tm As String


        oKDs = New KdsDataImport.ClKds
        Dim oBatch As KdsLibrary.BL.clBatch = New KdsLibrary.BL.clBatch
        Try

            oKDs.TryKdsFile()

            p_date_str = "20100426"
            'p_status = oKDs.ChkStatusSdrn(p_date_str)
            'oKDs.RunSdrn(p_date_str)

            iseq = oBatch.InsertProcessLog(28, 1, KdsLibrary.BL.RecordStatus.Wait, "start", 0)
            ''**oKDs.KdsWriteProcessLog(28, 1, 1, "start")
            oDal = New clDal




            'oKDs.KdsWriteProcessLog(3, 29, 1, "start tmp_meafyeney_sug_sidur")
            'oDal.ExecuteSP("PKG_SUG_SIDUR.calling_Pivot_Meafyeney_S")
            'oKDs.KdsWriteProcessLog(3, 29, 2, "end ok tmp_meafyeney_sug_sidur")


            'p_date_str = "20100421"
            'Dim lRequestNum As Integer
            'Dim dTaarich As Date
            'Dim dDatestr As String
            ''oKDs.KdsWriteProcessLog(8, 1, 1, "before OpenBatchRequest")
            ' lRequestNum = KdsLibrary.clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "KdsScheduler", -12)
            ' dDatestr = Mid(p_date_str, 7, 2) & "/" & Mid(p_date_str, 5, 2) & "/" & Mid(p_date_str, 1, 4)
            ' dTaarich = New DateTime(Mid(p_date_str, 1, 4), Mid(p_date_str, 5, 2), Mid(p_date_str, 7, 2))
            ''oKDs.KdsWriteProcessLog(8, 1, 1, "after OpenBatchRequest before shguyim")
            'KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcess, KdsBatch.BatchExecutionType.All, dTaarich, lRequestNum)
            ''oKDs.KdsWriteProcessLog(8, 1, 9, "after shguyim before ishurim")
            'KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(dTaarich, False)
            ''oKDs.KdsWriteProcessLog(6, 1, 9, "after ishurim")



            'p_date = New DateTime(Mid(p_date_str, 1, 4), Mid(p_date_str, 5, 2), Mid(p_date_str, 7, 2))
            'p_status_2 = oKDs.ReplicateSdrn(p_date)
            'oKDs.RunSdrn(p_date_str)
            'oKDs.RunRefresh()


            dt_strt = CDate("01/01/2001")
            dt_abort = CDate("01/01/2001")
            dt_mail = CDate("01/01/2001")
            dt_ok = CDate("01/01/2001")
            if_strt = ""
            if_abort = ""
            if_mail = ""
            if_ok = ""
            If Now.Month < 10 Then
                p_TAARICH = Now.Year.ToString & "0" & Now.Month.ToString
            Else
                p_TAARICH = Now.Year.ToString & Now.Month.ToString
            End If
            If Now.Day < 10 Then
                p_TAARICH = p_TAARICH & "0" & Now.Day.ToString
            Else
                p_TAARICH = p_TAARICH & Now.Day.ToString
            End If


            nd = 0 - 1
            p_date = Now.AddDays(nd)
            p_date = CDate("26/03/2010")
            nd = 1
            Do While p_date < CDate("30/03/2010")
                If p_date.Month < 10 Then
                    p_date_str = p_date.Year.ToString & "0" & p_date.Month.ToString
                Else
                    p_date_str = p_date.Year.ToString & p_date.Month.ToString
                End If
                If p_date.Day < 10 Then
                    p_date_str = p_date_str & "0" & p_date.Day.ToString
                Else
                    p_date_str = p_date_str & p_date.Day.ToString
                End If

                p_date = p_date.AddDays(nd)
            Loop

            'all refresh check 
            WhrStr = "taarich>trunc(sysdate)and kod_tahalich=3 "
            dt = oKDs.GetRowKds("tb_log_tahalich", WhrStr, "*", RetSql)

            If dt.Rows.Count > 0 Then
                '1:  refresh Matzav_Ovdim 
                if_strt = ""
                if_ok = ""
                if_abort = ""
                if_mail = ""
                if_strt = dt.Compute("max(taarich)", filter:="kod_peilut_tahalich=1 and status=1").ToString
                if_ok = dt.Compute("max(taarich)", filter:="kod_peilut_tahalich=1 and status=2").ToString
                if_abort = dt.Compute("max(taarich)", filter:="kod_peilut_tahalich=1 and status=9").ToString
                if_mail = dt.Compute("max(taarich)", filter:="kod_peilut_tahalich=1 and status=6").ToString
                If if_strt = "" Then
                    dt_strt = CDate("01/01/2001")
                Else
                    dt_strt = New DateTime(if_strt)
                End If
                If if_ok = "" Then
                    dt_ok = CDate("01/01/2001")
                Else
                    dt_ok = New DateTime(if_ok)
                End If
                If if_abort = "" Then
                    dt_abort = CDate("01/01/2001")
                Else
                    dt_abort = New DateTime(if_abort)
                End If
                If if_mail = "" Then
                    dt_mail = CDate("01/01/2001")
                Else
                    dt_mail = New DateTime(if_mail)
                End If
                If dt_ok > CDate("01/01/2001") Then
                    If Not dt_ok < dt_strt Then
                        If dt_ok > dt_mail Then
                            If dt_ok > dt_abort Then
                                'all ok
                            Else
                                'abort was fired without strt??
                                'todo:send mail
                            End If
                        Else
                            'mail was sent, without strt??
                        End If
                    Else
                        'matzav started again and did not finish
                    End If
                Else
                    'matzav not ok, check abort and mail
                End If
            End If



            oKDs.TryKdsFile()
            oBatch.UpdateProcessLog(iseq, KdsLibrary.BL.RecordStatus.Finish, "end", 0)
            ''**oKDs.KdsWriteProcessLog(28, 1, 9, "end ")

        Catch ex As Exception
            'todo...
            oBatch.UpdateProcessLog(iseq, KdsLibrary.BL.RecordStatus.Faild, "test aborted " & ex.Message, 0)
            ''**oKDs.KdsWriteProcessLog(tahalich, sub_tahalich, 0, "test aborted " & ex.Message)
            'oKDs.KdsWriteProcessLog(28, 5, 6, "test aborted " & ex.Message)

        End Try


    End Sub

End Class
