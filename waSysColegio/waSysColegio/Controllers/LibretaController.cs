using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using waSysColegio.Models;

namespace waSysColegio.Controllers
{
    public class LibretaController : Controller
    {
        // GET: Libreta
        // Acción para mostrar la selección de grado y sección
        public ActionResult SeleccionarGradoSeccion()
        {
            using (var db = new ColegioBDv2Entities())
            {
                // Obtener todos los grados
                var grados = db.Grado
                    .Select(g => new GradoSeccionViewModel
                    {
                        ID_Grado = g.ID_Grado,
                        Numero_Grado = g.Numero_Grado,
                        Secciones = db.Seccion
                            .Where(s => s.Estado_Registro == "Registrado") // No es necesario filtrar por ID_Grado
                            .Select(s => new SeccionViewModel
                            {
                                ID_Seccion = s.ID_Seccion,
                                Nombre_Seccion = s.Nombre_Seccion
                            }).ToList()
                    }).ToList();

                return View(grados);
            }
        }

        [HttpGet]
        public ActionResult ListaEstudiantes(int? ID_Grado = null, int? ID_Seccion = null)
        {
            using (var db = new ColegioBDv2Entities())
            {
                // Verificar si el grado y la sección existen
                if (!db.Grado.Any(g => g.ID_Grado == ID_Grado) || !db.Seccion.Any(s => s.ID_Seccion == ID_Seccion))
                {
                    return RedirectToAction("SeleccionarGradoSeccion");
                }

                // Obtener la lista de estudiantes filtrada por grado y sección
                var estudiantes = db.Estudiante
                    .Where(e => e.ID_Grado == ID_Grado && e.ID_Seccion == ID_Seccion) // Filtro por grado y sección
                    .Select(e => new EstudianteViewModel
                    {
                        ID_Estudiante = e.ID_Estudiante,
                        NombreCompleto = e.Apellido + ", " + e.Nombre,
                        DNI = e.DNI
                    })
                    .ToList();

                // Pasar los valores de grado y sección a la vista
                ViewBag.Grado = db.Grado.FirstOrDefault(g => g.ID_Grado == ID_Grado)?.Numero_Grado;
                ViewBag.Seccion = db.Seccion.FirstOrDefault(s => s.ID_Seccion == ID_Seccion)?.Nombre_Seccion;

                // Si no hay estudiantes, redirigir a la vista correspondiente
                if (estudiantes.Count == 0)
                {
                    ViewBag.NoEstudiantes = "No hay estudiantes en esta sección para este grado.";
                }

                return View(estudiantes);
            }
        }


        // Acción para ver el reporte de libreta para un estudiante específico
        public ActionResult ReporteLibreta(int estudianteID)
        {
            using (var db = new ColegioBDv2Entities())
            {
                var reporteData = (from e in db.Estudiante
                                   join g in db.Grado on e.ID_Grado equals g.ID_Grado
                                   join s in db.Seccion on e.ID_Seccion equals s.ID_Seccion // CORRECCIÓN AQUÍ
                                   join dc in db.Detalle_Curso on e.ID_Estudiante equals dc.ID_Estudiante
                                   join c in db.Curso on dc.ID_Curso equals c.ID_Curso
                                   where e.ID_Estudiante == estudianteID
                                   select new
                                   {
                                       Grado = g.Numero_Grado,
                                       Seccion = s.Nombre_Seccion,
                                       NombreEstudiante = e.Apellido + ", " + e.Nombre,
                                       DNI = e.DNI,
                                       Curso = c.Nombre_Curso,
                                       Competencia1 = dc.Competencia1 ?? 0,
                                       Competencia2 = dc.Competencia2 ?? 0,
                                       Competencia3 = dc.Competencia3 ?? 0,
                                       Competencia4 = dc.Competencia4 ?? 0,
                                       Proyecto = dc.Proyecto ?? 0,
                                       ExamenFinal = dc.ExamenFinal ?? 0,
                                       NotaFinal = ((dc.Competencia1 + dc.Competencia2 + dc.Competencia3 + dc.Competencia4 + dc.Proyecto + dc.ExamenFinal) / 6)
                                   }).ToList();

                string nombreArchivo = $"{reporteData[0].NombreEstudiante.Replace(" ", " ")}_{DateTime.Now.Year}.pdf";



                // Diccionario de competencias por curso
                var competenciasPorCurso = new Dictionary<string, List<string>>
        {
            { "Matemáticas", new List<string> { "Resuelve problemas de cantidad", "Resuelve problemas de regularidad, equivalencia y cambio", "Resuelve problemas de forma, movimiento y ubicación", "Resuelve problemas de gestión de datos e incertidumbre" } },
            { "Lenguaje", new List<string> { "Se comunica oralmente en su lengua materna", "Lee diversos tipos de textos en su lengua materna", "Escribe diversos tipos de textos en su lengua materna", "Desarrolla pensamiento crítico sobre los textos" } },
            { "Ciencia", new List<string> { "Explica el mundo físico basándose en conocimientos científicos", "Diseña y construye soluciones tecnológicas", "Investiga sobre el medio ambiente y la biodiversidad", "Analiza los efectos de la tecnología en la sociedad" } },
            { "Historia", new List<string> { "Construye interpretaciones históricas", "Reflexiona sobre la identidad y cultura de la sociedad", "Comprende el desarrollo de las civilizaciones", "Analiza los cambios sociales y políticos" } },
            { "Educación Física", new List<string> { "Se desarrolla de manera autónoma a través de su motricidad", "Asume una vida saludable", "Interactúa a través de sus habilidades motoras y sociomotrices", "Demuestra responsabilidad y autodisciplina en el ejercicio físico" } }
        };

                using (MemoryStream stream = new MemoryStream())
                {
                    Document pdfDoc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                    pdfDoc.Open();

                    // Agregar imagen en la esquina superior izquierda
                    var imagenIzquierda = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resources/minedu.png"));
                    imagenIzquierda.ScaleToFit(300, 300); // Ajusta el tamaño según sea necesario
                    imagenIzquierda.SetAbsolutePosition(30, pdfDoc.PageSize.Height - 100); // Posición en la esquina superior izquierda
                    pdfDoc.Add(imagenIzquierda);

                    // Agregar imagen en la esquina superior derecha
                    var imagenDerecha = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resources/colegio.jpg"));
                    imagenDerecha.ScaleToFit(80, 80); // Ajusta el tamaño según sea necesario
                    imagenDerecha.SetAbsolutePosition(pdfDoc.PageSize.Width - 110, pdfDoc.PageSize.Height - 100); // Posición en la esquina superior derecha
                    pdfDoc.Add(imagenDerecha);

                    // Espacio en blanco
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));

                    // Título del informe
                    Paragraph title = new Paragraph($"INFORME DE PROGRESO DEL APRENDIZAJE DEL ESTUDIANTE - {DateTime.Now.Year}",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK));
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    pdfDoc.Add(new Paragraph(" ")); // Espacio en blanco

                    // Información del estudiante
                    PdfPTable infoTable = new PdfPTable(4) { WidthPercentage = 100 };
                    infoTable.SetWidths(new float[] { 2, 2, 2, 2 });

                    infoTable.AddCell(new PdfPCell(new Phrase("Grado", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].Grado))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase("Sección", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].Seccion))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase("Nombre del Estudiante", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].NombreEstudiante))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase("DNI", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].DNI))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    pdfDoc.Add(infoTable);
                    pdfDoc.Add(new Paragraph(" ")); // Espacio en blanco

                    // Tabla de calificaciones
                    PdfPTable table = new PdfPTable(7) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 2, 3, 1, 1, 1, 1, 1 });

                    table.AddCell(new PdfPCell(new Phrase("ÁREA CURRICULAR", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Rowspan = 2,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("COMPETENCIA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Rowspan = 2,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("CALIFICATIVO POR PERIODO", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Colspan = 4,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("NOTA FINAL", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Rowspan = 2,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("1", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("2", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("3", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("4", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });

                    foreach (var curso in reporteData.GroupBy(r => r.Curso))
                    {
                        PdfPCell cellCurso = new PdfPCell(new Phrase(curso.Key, FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                        {
                            Rowspan = 4,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        table.AddCell(cellCurso);

                        var competencias = competenciasPorCurso.ContainsKey(curso.Key) ? competenciasPorCurso[curso.Key] : new List<string> { "Competencia 1", "Competencia 2", "Competencia 3", "Competencia 4" };
                        for (int i = 0; i < competencias.Count; i++)
                        {
                            table.AddCell(competencias[i]);
                            table.AddCell(new PdfPCell(new Phrase(curso.First().Competencia1.ToString("0.00"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase(curso.First().Competencia2.ToString("0.00"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase(curso.First().Competencia3.ToString("0.00"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase(curso.First().Competencia4.ToString("0.00"))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            var notaFinalRedondeada = Math.Round(curso.First().NotaFinal ?? 0, 0);
                            table.AddCell(new PdfPCell(new Phrase(notaFinalRedondeada.ToString("0"))) { HorizontalAlignment = Element.ALIGN_CENTER });


                        }
                    }

                    pdfDoc.Add(table);
                    // Espacio en blanco antes de la sección de firmas
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));


                    // Tabla de firma y sello
                    PdfPTable firmaTable = new PdfPTable(2) { WidthPercentage = 100 };
                    firmaTable.SetWidths(new float[] { 2, 1 });

                    // Celda para Tutor/Docente
                    PdfPCell docenteCell = new PdfPCell();
                    docenteCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    docenteCell.PaddingTop = 20;
                    docenteCell.HorizontalAlignment = Element.ALIGN_CENTER; // Alinear al centro
                    docenteCell.AddElement(new Paragraph("____________________________", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    docenteCell.AddElement(new Paragraph("Firma y Sello del Tutor o Docente", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    firmaTable.AddCell(docenteCell);

                    // Celda para Director
                    PdfPCell directorCell = new PdfPCell();
                    directorCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    directorCell.PaddingTop = 20;
                    directorCell.PaddingLeft = 30;
                    directorCell.HorizontalAlignment = Element.ALIGN_CENTER; // Alinear al centro
                    directorCell.AddElement(new Paragraph("_________________________", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    directorCell.AddElement(new Paragraph("Firma y Sello del Director", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    firmaTable.AddCell(directorCell);

                    // Agregar la tabla de firma y sello
                    pdfDoc.Add(firmaTable);

                    pdfDoc.Close();

                    byte[] pdfBytes = stream.ToArray();
                    return File(pdfBytes, "application/pdf", nombreArchivo);
                }
            }
        }

        public string ConvertirNotaALetra(decimal nota)
        {
            if (nota >= 20m)
            {
                return "AD"; // Excelente
            }
            else if (nota >= 15)
            {
                return "A"; // Muy Bueno
            }
            else if (nota >= 11)
            {
                return "B"; // Bueno
            }
            else
            {
                return "C"; // Regular
            }
        }

        public ActionResult ReporteLibretaLetras(int estudianteID)
        {
            using (var db = new ColegioBDv2Entities())
            {
                var reporteData = (from e in db.Estudiante
                                   join g in db.Grado on e.ID_Grado equals g.ID_Grado
                                   join s in db.Seccion on e.ID_Seccion equals s.ID_Seccion // CORRECCIÓN AQUÍ
                                   join dc in db.Detalle_Curso on e.ID_Estudiante equals dc.ID_Estudiante
                                   join c in db.Curso on dc.ID_Curso equals c.ID_Curso
                                   where e.ID_Estudiante == estudianteID
                                   select new
                                   {
                                       Grado = g.Numero_Grado,
                                       Seccion = s.Nombre_Seccion,
                                       NombreEstudiante = e.Apellido + ", " + e.Nombre,
                                       DNI = e.DNI,
                                       Curso = c.Nombre_Curso,
                                       Competencia1 = dc.Competencia1 ?? 0,
                                       Competencia2 = dc.Competencia2 ?? 0,
                                       Competencia3 = dc.Competencia3 ?? 0,
                                       Competencia4 = dc.Competencia4 ?? 0,
                                       Proyecto = dc.Proyecto ?? 0,
                                       ExamenFinal = dc.ExamenFinal ?? 0,
                                       NotaFinal = ((dc.Competencia1 + dc.Competencia2 + dc.Competencia3 + dc.Competencia4 + dc.Proyecto + dc.ExamenFinal) / 6)


                                   }).ToList();

                string nombreArchivo = $"{reporteData[0].NombreEstudiante.Replace(" ", " ")}_{DateTime.Now.Year}.pdf";


                // Diccionario de competencias por curso
                var competenciasPorCurso = new Dictionary<string, List<string>>
        {
            { "Matemáticas", new List<string> { "Resuelve problemas de cantidad", "Resuelve problemas de regularidad, equivalencia y cambio", "Resuelve problemas de forma, movimiento y ubicación", "Resuelve problemas de gestión de datos e incertidumbre" } },
            { "Lenguaje", new List<string> { "Se comunica oralmente en su lengua materna", "Lee diversos tipos de textos en su lengua materna", "Escribe diversos tipos de textos en su lengua materna", "Desarrolla pensamiento crítico sobre los textos" } },
            { "Ciencia", new List<string> { "Explica el mundo físico basándose en conocimientos científicos", "Diseña y construye soluciones tecnológicas", "Investiga sobre el medio ambiente y la biodiversidad", "Analiza los efectos de la tecnología en la sociedad" } },
            { "Historia", new List<string> { "Construye interpretaciones históricas", "Reflexiona sobre la identidad y cultura de la sociedad", "Comprende el desarrollo de las civilizaciones", "Analiza los cambios sociales y políticos" } },
            { "Educación Física", new List<string> { "Se desarrolla de manera autónoma a través de su motricidad", "Asume una vida saludable", "Interactúa a través de sus habilidades motoras y sociomotrices", "Demuestra responsabilidad y autodisciplina en el ejercicio físico" } }
        };

                using (MemoryStream stream = new MemoryStream())
                {
                    Document pdfDoc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;
                    pdfDoc.Open();

                    // Agregar imagen en la esquina superior izquierda
                    var imagenIzquierda = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resources/minedu.png"));
                    imagenIzquierda.ScaleToFit(300, 300); // Ajusta el tamaño según sea necesario
                    imagenIzquierda.SetAbsolutePosition(30, pdfDoc.PageSize.Height - 100); // Posición en la esquina superior izquierda
                    pdfDoc.Add(imagenIzquierda);

                    // Agregar imagen en la esquina superior derecha
                    var imagenDerecha = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Resources/colegio.jpg"));
                    imagenDerecha.ScaleToFit(80, 80); // Ajusta el tamaño según sea necesario
                    imagenDerecha.SetAbsolutePosition(pdfDoc.PageSize.Width - 110, pdfDoc.PageSize.Height - 100); // Posición en la esquina superior derecha
                    pdfDoc.Add(imagenDerecha);

                    // Espacio en blanco
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));

                    // Título del informe
                    Paragraph title = new Paragraph($"INFORME DE PROGRESO DEL APRENDIZAJE DEL ESTUDIANTE - {DateTime.Now.Year}",
                    new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK));
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    pdfDoc.Add(new Paragraph(" ")); // Espacio en blanco

                    // Información del estudiante
                    PdfPTable infoTable = new PdfPTable(4) { WidthPercentage = 100 };
                    infoTable.SetWidths(new float[] { 2, 2, 2, 2 });

                    infoTable.AddCell(new PdfPCell(new Phrase("Grado", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].Grado))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase("Sección", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].Seccion))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase("Nombre del Estudiante", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].NombreEstudiante))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase("DNI", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    infoTable.AddCell(new PdfPCell(new Phrase(reporteData[0].DNI))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    pdfDoc.Add(infoTable);
                    pdfDoc.Add(new Paragraph(" ")); // Espacio en blanco

                    // Tabla de calificaciones
                    PdfPTable table = new PdfPTable(7) { WidthPercentage = 100 };
                    table.SetWidths(new float[] { 2, 3, 1, 1, 1, 1, 1 });

                    table.AddCell(new PdfPCell(new Phrase("ÁREA CURRICULAR", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Rowspan = 2,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("COMPETENCIA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Rowspan = 2,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("CALIFICATIVO POR PERIODO", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Colspan = 4,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("NOTA FINAL", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        Rowspan = 2,
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("1", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("2", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("3", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                    table.AddCell(new PdfPCell(new Phrase("4", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });

                    foreach (var curso in reporteData.GroupBy(r => r.Curso))
                    {
                        PdfPCell cellCurso = new PdfPCell(new Phrase(curso.Key, FontFactory.GetFont(FontFactory.HELVETICA_BOLD)))
                        {
                            Rowspan = 4,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        table.AddCell(cellCurso);

                        // Definir las competencias en letras si no están ya definidas
                        var competencias = competenciasPorCurso.ContainsKey(curso.Key) ? competenciasPorCurso[curso.Key] : new List<string> { "Competencia 1", "Competencia 2", "Competencia 3", "Competencia 4" };

                        // Iterar sobre las competencias
                        for (int i = 0; i < competencias.Count; i++)
                        {
                            table.AddCell(competencias[i]);

                            // Aquí se aplican las conversiones a letras
                            table.AddCell(new PdfPCell(new Phrase(ConvertirNotaALetra(Math.Round(curso.First().Competencia1)))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase(ConvertirNotaALetra(Math.Round(curso.First().Competencia2)))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase(ConvertirNotaALetra(Math.Round(curso.First().Competencia3)))) { HorizontalAlignment = Element.ALIGN_CENTER });
                            table.AddCell(new PdfPCell(new Phrase(ConvertirNotaALetra(Math.Round(curso.First().Competencia4)))) { HorizontalAlignment = Element.ALIGN_CENTER });

                            // Si hay un proyecto, también convertirlo
                            table.AddCell(new PdfPCell(new Phrase(ConvertirNotaALetra(Math.Round(curso.First().NotaFinal ?? 0)))) { HorizontalAlignment = Element.ALIGN_CENTER });
                        }
                    }


                    pdfDoc.Add(table);
                    // Espacio en blanco antes de la sección de firmas
                    pdfDoc.Add(new Paragraph(" "));
                    pdfDoc.Add(new Paragraph(" "));


                    // Tabla de firma y sello
                    PdfPTable firmaTable = new PdfPTable(2) { WidthPercentage = 100 };
                    firmaTable.SetWidths(new float[] { 2, 1 });

                    // Celda para Tutor/Docente
                    PdfPCell docenteCell = new PdfPCell();
                    docenteCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    docenteCell.PaddingTop = 20;
                    docenteCell.HorizontalAlignment = Element.ALIGN_CENTER; // Alinear al centro
                    docenteCell.AddElement(new Paragraph("____________________________", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    docenteCell.AddElement(new Paragraph("Firma y Sello del Tutor o Docente", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    firmaTable.AddCell(docenteCell);

                    // Celda para Director
                    PdfPCell directorCell = new PdfPCell();
                    directorCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    directorCell.PaddingTop = 20;
                    directorCell.PaddingLeft = 30;
                    directorCell.HorizontalAlignment = Element.ALIGN_CENTER; // Alinear al centro
                    directorCell.AddElement(new Paragraph("_________________________", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    directorCell.AddElement(new Paragraph("Firma y Sello del Director", FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD)));
                    firmaTable.AddCell(directorCell);

                    // Agregar la tabla de firma y sello
                    pdfDoc.Add(firmaTable);

                    pdfDoc.Close();

                    byte[] pdfBytes = stream.ToArray();
                    return File(pdfBytes, "application/pdf", nombreArchivo);
                }
            }
        }


    }
}