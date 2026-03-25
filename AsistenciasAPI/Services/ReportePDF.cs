using AsistenciaAPI.Models.DTOs;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

public class ReportePDF
{
    public static byte[] GenerarPDF(ReporteDTO modelo)
    {
        using var ms = new MemoryStream();
        var writer = new PdfWriter(ms);
        var pdf = new PdfDocument(writer);
        var doc = new Document(pdf, PageSize.A4.Rotate());

        var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

        // Título
        doc.Add(new Paragraph($"Reporte de Asistencia - {modelo.Nombre}")
            .SetFont(font)
            .SetFontSize(16)

            .SetTextAlignment(TextAlignment.CENTER));

        doc.Add(new Paragraph("\n"));

        // Columnas dinámicas: NumCtrl, Nombre + Fechas
        int columnas = 2 + modelo.Fechas.Count;

        Table table = new Table(columnas).UseAllAvailableWidth();
        table.SetFontSize(6);
        table.AddHeaderCell(new Cell().Add(new Paragraph("Num Ctrl").SetFont(font)));
        table.AddHeaderCell(new Cell().Add(new Paragraph("Nombre").SetFont(font)));

        foreach (var fecha in modelo.Fechas)
        {
            table.AddHeaderCell(new Cell()
                .Add(new Paragraph(fecha.ToString("dd/MM"))
                .SetFont(font))
            );
        }

        // Filas de alumnos
        foreach (var alumno in modelo.ListaAlumnos)
        {
            table.AddCell(new Cell().Add(new Paragraph(alumno.NumCtrl).SetFont(font)));
            table.AddCell(new Cell().Add(new Paragraph(alumno.Nombre).SetFont(font)));

            foreach (var fecha in alumno.ListaFecha)
            {
                table.AddCell(new Cell()
                    .Add(new Paragraph(fecha.Estado).SetFont(font))
                    .SetTextAlignment(TextAlignment.CENTER)
                );
            }
        }

        doc.Add(table);
        doc.Close();

        return ms.ToArray();
    }
}

