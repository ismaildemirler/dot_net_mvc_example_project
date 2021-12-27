using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Infrastructure.Tools
{
    public enum ExcelExportFormat
    {
        XML,
        CSV

    }
    public static class Office
    {
        public static object GetPropertyValue(this object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null) throw new ArgumentNullException($"invalid column name '{propertyName}'");

            return property.GetValue(obj, null);
        }

        public static string ExportToExcel(IEnumerable dataRows, string[] columnNames, string[] columnHeaders, float[] columnWidths)
        {
            ExcelExportFormat exportFormat = ExcelExportFormat.XML;

            if (dataRows == null)
                return null;

            StringBuilder sw = new StringBuilder();
            StringBuilder strBuilder = new StringBuilder();

            // rapor başlıkları ekleniyor
            List<string> lstFields = new List<string>();
            if (columnHeaders != null && columnHeaders.Length == columnNames.Length)
            {
                foreach (var columnHeader in columnHeaders)
                {
                    lstFields.Add(FormatField(columnHeader, exportFormat, "s62"));
                }
            }
            else
            {
                foreach (var columnName in columnNames)
                {
                    lstFields.Add(FormatField(columnName, exportFormat, "s62"));
                }
            }
            BuildStringOfRow(strBuilder, lstFields, exportFormat);

            // rapor dataları ekleniyor
            foreach (var item in dataRows)
            {
                lstFields.Clear();
                foreach (var columnName in columnNames)
                {
                    string columndata = item.GetPropertyValue(columnName) == null ? "" : item.GetPropertyValue(columnName).ToString();
                    lstFields.Add(FormatField(columndata, exportFormat, "s64"));
                }
                BuildStringOfRow(strBuilder, lstFields, exportFormat);
            }

            if (exportFormat == ExcelExportFormat.XML)
            {
                sw.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");
                sw.AppendLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
                sw.AppendLine(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
                sw.AppendLine(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
                sw.AppendLine(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                sw.AppendLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
                sw.AppendLine("<Author>ASPB BIDB Sistem Yönetimi</Author>");
                sw.AppendLine($"<Created>{DateTime.Now.ToLocalTime().ToLongDateString()}</Created>");
                sw.AppendLine($"<LastSaved>{DateTime.Now.ToLocalTime().ToLongDateString()}</LastSaved>");
                sw.AppendLine("<Company>ASPB BIDB</Company>");
                sw.AppendLine("<Version>12.00</Version>");
                sw.AppendLine("</DocumentProperties>");

                sw.AppendLine("<Styles>");

                sw.AppendLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
                sw.AppendLine("<Alignment ss:Vertical=\"Bottom\"/>");
                sw.AppendLine("<Font ss:FontName=\"Calibri\" x:CharSet=\"162\" x:Family=\"Swiss\" ss:Size=\"10\" ss:Color=\"#000000\"/>");
                sw.AppendLine("<Borders/><Interior/><NumberFormat/><Protection/>");
                sw.AppendLine("</Style>");

                sw.AppendLine("<Style ss:ID=\"s38\" ss:Name=\"Accent1\">");
                sw.AppendLine("<Font ss:FontName=\"Calibri\" x:CharSet=\"162\" x:Family=\"Swiss\" ss:Size=\"12\" ss:Color=\"#FFFFFF\"/>");
                sw.AppendLine("<Interior ss:Color=\"#971212\" ss:Pattern=\"Solid\"/>");
                sw.AppendLine("</Style>");

                sw.AppendLine("<Style ss:ID=\"s62\" ss:Parent=\"s38\">");
                sw.AppendLine("<Font ss:FontName=\"Calibri\" x:CharSet=\"162\" x:Family=\"Swiss\" ss:Size=\"12\" ss:Color=\"#FFFFFF\" ss:Bold=\"1\"/>");
                sw.AppendLine("</Style>");

                sw.AppendLine("<Style ss:ID=\"s63\" ss:Parent=\"s38\">");
                sw.AppendLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                sw.AppendLine("<Font ss:FontName=\"Calibri\" x:CharSet=\"162\" x:Family=\"Swiss\" ss:Size=\"12\" ss:Color=\"#FFFFFF\" ss:Bold=\"1\"/>");
                sw.AppendLine("</Style>");

                sw.AppendLine("<Style ss:ID=\"s64\">");
                sw.AppendLine("<Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>");
                sw.AppendLine("</Style>");

                sw.AppendLine("<Style ss:ID=\"s65\">");
                sw.AppendLine("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\"/>");
                sw.AppendLine("</Style>");

                sw.AppendLine("<Style ss:ID=\"s66\">");
                sw.AppendLine("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>");
                sw.AppendLine("</Style>");

                sw.AppendLine("</Styles>");

                sw.AppendLine("<Worksheet ss:Name=\"Rapor\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                sw.AppendLine("<Table>");

                if (columnWidths != null && columnWidths.Length == columnNames.Length)
                {
                    foreach (float item in columnWidths)
                    {
                        sw.AppendFormat("<Column ss:Width=\"{0}\"/>", item.ToString(CultureInfo.InvariantCulture));
                    }
                }

                sw.AppendLine(strBuilder.ToString());

                sw.AppendLine("</Table>");
                sw.AppendLine("</Worksheet>");
                sw.AppendLine("</Workbook>");
            }

            return sw.ToString();
        }

        private static void BuildStringOfRow(StringBuilder strBuilder, List<string> lstFields, ExcelExportFormat exportFormat)
        {
            if (exportFormat == ExcelExportFormat.XML)
            {
                strBuilder.AppendLine("<Row>");
                strBuilder.AppendLine(String.Join("\r\n", lstFields.ToArray()));
                strBuilder.AppendLine("</Row>");
            }
            else if (exportFormat == ExcelExportFormat.CSV)
            {
                strBuilder.AppendLine(String.Join(",", lstFields.ToArray()));
            }
        }

        private static string FormatField(string data, ExcelExportFormat exportFormat, string styleID)
        {
            string tmpStyle = "";
            if (!string.IsNullOrEmpty(styleID))
                tmpStyle = $"ss:StyleID=\"{styleID}\"";

            if (exportFormat == ExcelExportFormat.XML)
            {
                return $"<Cell {tmpStyle}><Data ss:Type=\"String\">{data}</Data></Cell>";
            }
            else if (exportFormat == ExcelExportFormat.CSV)
            {
                return $"\"{data.Replace("\"", "\"\"\"").Replace("\n", "").Replace("\r", "")}\"";
            }
            else
                return data;
        }

       
    }
}
