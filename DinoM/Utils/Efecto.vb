﻿
Imports System.ComponentModel
Imports System.Text
Imports DevComponents.DotNetBar
Public Class Efecto
    Public _archivo As String
    Public band As Boolean = False
    Public Header As String = ""
    Public tipo As Integer = 0
    Public Context As String = ""
    Public listEstCeldas As List(Of Modelo.Celda)
    Public dt As DataTable
    Public alto As Integer
    Public ancho As Integer
    Public Row As Janus.Windows.GridEX.GridEXRow
    Public SeleclCol As Integer = -1
    Public Stock As Double = 0
    Public Cantidad As Double = 0
    Public NameProducto As String = ""
    Public CategoriaPrecio As Integer = 0
    Public IdProducto As Integer
    Public Precio As Double

    Private Sub Efecto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        Select Case tipo
            Case 1
                _prMostrarMensaje()
            Case 2
                _prMostrarMensajeDelete()
            Case 3
                _prMostrarFormAyuda()
            Case 4
                _prLogin()
            Case 7
                _prMostrarAyudaVentaCantidad()
        End Select
    End Sub

    Sub _prMostrarAyudaVentaCantidad()

        Dim frmAyuda As F1_Cantidad
        frmAyuda = New F1_Cantidad

        frmAyuda.Stock = Stock
        frmAyuda.Cantidad = Cantidad
        frmAyuda.Producto = NameProducto
        frmAyuda.CategoriaPrecio = CategoriaPrecio
        frmAyuda.idProducto = IdProducto
        frmAyuda.ShowDialog()
        If frmAyuda.Bandera = True Then

            Cantidad = frmAyuda.Cantidad
            Precio = frmAyuda.Precio
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If

    End Sub
    Public Sub _prLogin()
        Dim Frm As New Login
        Me.Close()
        Frm.ShowDialog()
        'Me.Close()
    End Sub
    Sub _prMostrarFormAyuda()

        Dim frmAyuda As Modelo.ModeloAyuda
        frmAyuda = New Modelo.ModeloAyuda(alto, ancho, dt, Context.ToUpper, listEstCeldas)
        If (SeleclCol >= 0) Then
            frmAyuda.Columna = SeleclCol
            frmAyuda._prSeleccionar()

        End If
        frmAyuda.ShowDialog()
        If frmAyuda.seleccionado = True Then
            Row = frmAyuda.filaSelect
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()
        End If
       
    End Sub
    Sub _prMostrarMensaje()
        Dim blah As Bitmap = My.Resources.cuestion
        Dim ico As Icon = Icon.FromHandle(blah.GetHicon())

        If (MessageBox.Show(Context, Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            'Process.Start(_archivo)
            band = True
            Me.Close()
        Else
            band = False
            Me.Close()


        End If
    End Sub
    Sub _prMostrarMensajeDelete()

        Dim info As New TaskDialogInfo(Context, eTaskDialogIcon.Delete, "advertencia".ToUpper, Header, eTaskDialogButton.Yes Or eTaskDialogButton.Cancel, eTaskDialogBackgroundColor.Default)
        Dim result As eTaskDialogResult = TaskDialog.Show(info)
        If result = eTaskDialogResult.Yes Then
            Dim mensajeError As String = ""
            band = True
            Me.Close()

        Else
            band = False
            Me.Close()

        End If
    End Sub
End Class