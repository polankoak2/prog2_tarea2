Imports System.Drawing
Imports System.Windows.Forms
Imports System.Text
Imports tarea2.System

Public Class Form1
    Private WithEvents pictureBox As New PictureBox
    Private WithEvents btnGenerateCoordinates As New BtnGenerateCoordinates
    Private destinations As New List(Of Point)()
    Private casaMatriz As New Point(200, 200)
    Private maxDestination As New Point()

    Public Sub New()

        InitializeComponent()
        pictureBox.Width = Me.ClientSize.Width
        pictureBox.Height = Me.ClientSize.Height
        Me.Controls.Add(pictureBox)
    End Sub

    Private Function ClientSize() As System.Object
        Throw New NotImplementedException()
    End Function

    Private Sub BtnGenerateCoordinates_Click(sender As Object, e As EventArgs) Handles btnGenerateCoordinates.Click
        Dim random As New Random()
        destinations.Clear()
        For i As Integer = 0 To 5
            Dim x As Integer = random.Next(200, 1000)
            Dim y As Integer = random.Next(200, 700)
            destinations.Add(New Point(x, y))
        Next

        Dim sb As New System.Text.StringBuilder()
        For i As Integer = 0 To 5
            sb.AppendLine($"Destino {i + 1}: ({destinations(i).X}, {destinations(i).Y})")
        Next
        Dim txtCoordinates As Object = Nothing
        txtCoordinates.Text = sb.ToString()

        maxDestination = destinations(0)
        For Each destination As Point In destinations
            If destination.X > maxDestination.X Then
                maxDestination = destination
            End If
        Next

        ' Dibujar rectángulo con gradiente
        Dim graphics As Graphics = pictureBox.CreateGraphics()
        Dim brush As New Drawing2D.LinearGradientBrush(New Point(0, 0), maxDestination, Color.Blue, Color.Green)
        graphics.FillRectangle(brush, 0, 0, pictureBox.Width, pictureBox.Height)

        ' Poner control PictureBox en cada destino con el logo/foto
        For i As Integer = 0 To 6
            Dim logoPictureBox As New PictureBox()
            logoPictureBox.Image = Image.FromFile($"C:\Images\Logo{i + 1}.png")
            logoPictureBox.Size = New Size(30, 30)
            logoPictureBox.Location = New Point(destinations(i).X - 15, destinations(i).Y - 15)
            Me.Controls.Add(logoPictureBox)
        Next

        ' Trazar la curva que une los 7 puntos
        graphics.DrawCurve(New Pen(Color.Red), destinations.ToArray())

        ' Agregar menú contextual al último destino
        Dim contextMenu As New ContextMenu()
        Dim saveItem As New MenuItem("Guardar Imagen")
        Dim loadItem As New MenuItem("Cargar Otra Imagen")
        AddHandler saveItem.Click, AddressOf SaveItemClick
        AddHandler loadItem.Click, AddressOf LoadItemClick
        contextMenu.MenuItems.Add(saveItem)
        contextMenu.MenuItems.Add(loadItem)
        pictureBox.ContextMenu = contextMenu
    End Sub

    Private Function Controls() As System.Object
        Throw New NotImplementedException()
    End Function

    Private Sub SaveItemClick(sender As Object, e As EventArgs)
        ' Lógica para guardar la imagen asociada al último destino
        ' (puedes implementarla según tus necesidades)
        MessageBox.Show("Guardar imagen")
    End Sub

    Private Sub LoadItemClick(sender As Object, e As EventArgs)
        ' Lógica para cargar otra imagen al último destino
        ' (puedes implementarla según tus necesidades)
        MessageBox.Show("Cargar otra imagen")
    End Sub


End Class

Friend Class BtnGenerateCoordinates
    Public Event Click(sender As [Object], e As EventArgs)
End Class
