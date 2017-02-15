'Call fnShowReport(CNST_RD_NORMAL, strMrdFileNm, "0", strPgm)
public Sub fnShowReport(PgmId, IsdirectPrint, strParams)
    on error resume next
    
    Dim objCom, strSvrIP, strMrdDownIP, IsReportDesigner, strDBInfo, strHomeDir

    strDBInfo    = ""
    strSvrIP     = "127.0.0.1"
    strMrdDownIP = "192.168.202.199"
    IsReportDesigner = "1"

    Redim strDBInfo(3)
    
    strDBInfo(0) = "192.168.102.1"
    strDBInfo(1) = "KNHLM"
    strDBInfo(2) = "sa"
    strDBInfo(3) = "1q2w3e4r1!"
    
'    if Trim(strSvrIP) = "" or strDBInfo = "" then 
'        messageboxshow("Web-Server-IP,DB Infomation not Found")
'        exit Function
'    end if

'    set objCom = CreateObject("YlwFileServiceDll.clsFileServiceDll")
'    strHomeDir = objCom.GetReg("","homedir","")
'    
'    if strHomeDir = "" then 
'        messageboxshow("HomeDir not Found")
'        exit Function
'    else
'        strHomeDir = strHomeDir + "\Controls"   '화일이 존재하는 경로
'    end if 
    
    'Call objCom.fnShowIISReportNET(strSvrIP, strMrdDownIP,strDBInfo,IsReportDesigner, PgmId, strParams, IsdirectPrint, m_strSiteInit)
    Call fnShowIISReportNET(strSvrIP, strMrdDownIP,strDBInfo,IsReportDesigner, PgmId, strParams, IsdirectPrint)

End Sub

Public Sub fnShowIISReportNET(strWebSvrIP, strMrdDownIP, strDBInfo, IsNET, ByVal strPgmID, strParams, IsdirectPrint)

    Dim strPgmIDSite, strExecString, strRetVal, arrDBInfo

    strPgmIDSite = ""
    
    Redim arrDBInfo(1)
    
    arrDBInfo(0) = strDBInfo(0)     ' 0: DB Server IP
    arrDBInfo(1) = strDBInfo(1)     ' 1: DB Name
    
    Call fnMrdIISFileDownloadNET(strMrdDownIP, strPgmID, strPgmIDSite)
    
    strExecString = fnGetRD_Normal(strWebSvrIP, strMrdDownIP, arrDBInfo(0), IsNET, strPgmID, strParams, IsdirectPrint)
    strParams = strExecString + strParams
    
    Call PrintString(CStr(strParams))
'    RetVal = Shell(strParams, vbMaximizedFocus)
    Exit Sub

End Sub

Private Function fnMrdIISFileDownloadNET(strWebSvrIP, strPgmID, strPgmIDSite)

    Dim strParam, strMrdFileLists
    
    strMrdFileList = CStr(strPgmID)
    
'    strParam = strParam + "C:\Ksystem.net\controls\YlwIISClient.exe"
    strParam = strParam + " /i 2"
    strParam = strParam + " /u " + strWebSvrIP + "/Common/FileDownload/FileService"
    strParam = strParam + " /m YlwIISClient.exe"
    strParam = strParam + " /r rdn"
    strParam = strParam + " /f " + strMrdFileList
    strParam = strParam + " /k ] ::::: "
    strParam = strParam + " /l 1"
    strParam = strParam + " /s "
    strParam = strParam + " /z ReportNET"
    strParam = strParam + " /d " + "C:\Ksystem.net\ReportNET"
    
    Call WaitShell(strParam, False)

End Function

Private Function fnGetRD_Normal(strWebSvrIP, strMrdDownIP, strDBName, IsNET, strFileNm, strParams, IsdirectPrint)

    Dim strPath, strFilePath, strHomeDir, strFileDir, strFilePath_N_0
    
    strHomeDir = "C:\Ksystem.net"
    
    If IsNET = "1" Then
        strPath = strHomeDir + "\Controls\rdviewer_u.exe"
    Else
        strPath = strHomeDir + "\Controls\rdviewer.exe"
    End If
    
    If strLanguageID = Empty Then strLanguageID = "0"
    
    strFileNm = Mid(strFileNm, 1, Len(strFileNm) - 4)
    
    If IsNET = "1" Then
        strFileDir = strHomeDir & "\ReportNET\"
    Else
        strFileDir = strHomeDir & "\Report\"
    End If

    strFilePath_N_0 = strFileNm & ".mrd"

    strFileNm = strFilePath_N_0
    
    ' /p /rcontype [NET] /rip [¼­¹oURL] /rsn [¿￢°a¹®AU¿­º°¸i]
    ' /rcontype [RDS] /rip [http://orion] /rsn [Data Source=pubtest]
    ' /rcontype [ODBC] /rip [http://orion] /rsn [Data Source=pubtest]
    
    ' NETAI°æ¿i
    If IsNET = "1" Then
        If IsdirectPrint = "1" Then
            fnGetRD_Normal = strPath + "  " + strHomeDir + "\reportNET\" + strFileNm + " /p /rcontype [NET] /rsn [" + strDBName + "] " & _
                   "/rip [" + strWebSvrIP + "/RDServer4Net/rdnetdsl.aspx] "
        Else
            fnGetRD_Normal = strPath + "  " + strHomeDir + "\reportNET\" + strFileNm + "  /rcontype [NET] /rsn [" + strDBName + "] " & _
                 "/rip [" + strWebSvrIP + "/RDServer4Net/rdnetdsl.aspx] "
        End If
    Else
        If IsdirectPrint = "1" Then
            fnGetRD_Normal = strPath + "  " + strHomeDir + "\report\" + strFileNm + " /p /rcontype [RDS] /rsn [Data Source=" + strDBName + "] " & _
                   "/rip [http://" + strWebSvrIP + "] "
        Else
            fnGetRD_Normal = strPath + "  " + strHomeDir + "\report\" + strFileNm + "  /rcontype [RDS] /rsn [Data Source=" + strDBName + "] " & _
                 "/rip [http://" + strWebSvrIP + "] "
        End If
    End If
    
    Exit Function

End Function

Private Sub PrintString(text)
    
'    If GetylwReg(CNST_KSystemDir + "\DefClientConfig", "IsRdCreateFile", "0") <> "1" Then
'        Exit Sub
'    End If
'    
'    Dim intFreeFile As Integer
'    intFreeFile = FreeFile

    'print the URL file
    dim filesys, filetxt
    Const ForReading = 1, ForWriting = 2, ForAppending = 8 
    Set filesys = CreateObject("Scripting.FileSystemObject")
    Set filetxt = filesys.OpenTextFile("C:\Ksystem.net" & "\crwReportout.bat", ForWriting, True) 

    filetxt.WriteLine(text)
    filetxt.Close
    
'    Print #intFreeFile, text
'    Close intFreeFile
End Sub

Public Sub WaitShell(CommandLine, bShowWindow)
    
    '==========================================================================
    'Call msiShellAndWait(strWinSysDir + "\msiexec.exe /x" + sPackageFile, True)
    'Source Code:
    '==========================================================================
'    Dim ReturnValue As Long
'    Dim Start As STARTUPINFO
'    Dim Process As PROCESS_INFORMATION
    
'    Dim ReturnValue,  Start, Process
'    ' Initialize the STARTUPINFO structure:
'    Start.cb = Len(Start)
'    
'    If bShowWindow = False Then
'        Start.dwFlags = STARTF_USESHOWWINDOW
'        Start.wShowWindow = SW_HIDE
'    End If
    
'    ' Start the shelled application:
'    ReturnValue = CreateProcessA(0&, CommandLine, 0&, 0&, 1&, NORMAL_PRIORITY_CLASS, 0&, 0&, Start, Process)
'    ' Wait for the shelled application to finish:
'    ReturnValue = WaitForSingleObject(Process.hProcess, INFINITE)
'    ReturnValue = CloseHandle(Process.hProcess)
    
    Exit Sub
    
End Sub