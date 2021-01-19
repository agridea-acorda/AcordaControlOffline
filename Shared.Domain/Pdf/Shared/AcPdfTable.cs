using iTextSharp.text.pdf;
using System;
using iTextSharp.text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Shared
{
    public class AcPdfPTable : PdfPTable
    {
        #region Members


        #endregion

        #region Properties

        public Font FontNormal { get; set; }

        //public Font TableBoldColorFont { get; set; }
        public Font FontBold { get; set; }
        public BaseColor ColorDark { get; set; }

        public BaseColor ColorLight { get; set; }

        #endregion

        #region Initialization

        //public AcPdfPTable()
        //{
        //    InitializeValues();
        //}

        public AcPdfPTable(float[] relativeWidths) : base(relativeWidths)
        {
            InitializeValues();
        }

        //public AcPdfPTable(int numColumns) : base(numColumns)
        //{
        //    InitializeValues();
        //}

        //public AcPdfPTable(PdfPTable table) : base(table)
        //{
        //    InitializeValues();
        //}

        #endregion

        #region Services

        private void InitializeValues()
        {
            ColorDark = Colors.Black;
            ColorLight = Colors.White;
            FontNormal = Fonts.Helvetica6Black;
            FontBold = Fonts.Helvetica6BlackBold;
            //TableBoldColorFont = Fonts.Helvetica6BlackBold;
        }

        public void AddTitleCell(string text,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 0,
            float borderWidth = 0.5f,
            BaseColor backgroundColor = null)
        {
            backgroundColor = backgroundColor ?? ColorLight;
            AddCustomCell(text,
                FontBold,
                backgroundColor,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddDarkTitleCell(string text,
            BaseColor backgroundColor,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 1,
            float borderWidth = 0.5f)
        {
            AddCustomCell(text,
                FontBold,
                backgroundColor,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCodeCell(string text,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 1,
            float borderWidth = 0.5f)
        {
            AddCustomCell(text,
                FontBold,
                ColorLight,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(int value,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 2,
            float borderWidth = 0.5f)
        {
            AddCustomCell(value == 0 ? string.Empty : value.ToString(),
                null,
                BaseColor.WHITE,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(Decimal value,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 2,
            float borderWidth = 0.5f)
        {
            AddCustomCell(value == Decimal.Zero ? string.Empty : value.ToString(),
                null,
                BaseColor.WHITE,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(int value,
            Font font,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 2,
            float borderWidth = 0.5f)
        {
            AddCustomCell(value == 0 ? string.Empty : value.ToString(),
                font,
                BaseColor.WHITE,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(int value,
            BaseColor backgroundColor,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 2,
            float borderWidth = 0.5f)
        {
            AddCustomCell(value == 0 ? string.Empty : value.ToString(),
                null,
                backgroundColor,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(int value,
            Font font,
            BaseColor backgroundColor,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 2,
            float borderWidth = 0.5f)
        {
            AddCustomCell(value == 0 ? string.Empty : value.ToString(),
                font,
                backgroundColor,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(string text,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 0,
            float borderWidth = 0.5f)
        {
            AddCustomCell(text,
                null,
                BaseColor.WHITE,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(string text,
            Font font,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 0,
            float borderWidth = 0.5f)
        {
            AddCustomCell(text,
                font,
                BaseColor.WHITE,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(string text,
            BaseColor backgroundColor,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 0,
            float borderWidth = 0.5f)
        {
            AddCustomCell(text,
                null,
                backgroundColor,
                height,
                colspan,
                rowspan,
                verticalAlignment,
                horizontalAlignment,
                borderWidth);
        }

        public void AddCustomCell(string text,
            Font font,
            BaseColor backgroundColor,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 0,
            float borderWidth = 0.5f)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font ?? FontNormal))
            {
                MinimumHeight = height,
                BackgroundColor = backgroundColor,
                Colspan = colspan,
                Rowspan = rowspan,
                BorderWidth = borderWidth,
                BorderColor = ColorDark,
                VerticalAlignment = verticalAlignment,
                HorizontalAlignment = horizontalAlignment
            };
            AddCell(cell);
        }

        public void AddCustomCell(Phrase phrase,
            BaseColor backgroundColor = null,
            float height = 0.0f,
            int colspan = 1,
            int rowspan = 1,
            int verticalAlignment = 5,
            int horizontalAlignment = 0,
            float borderWidth = 0.5f)
        {
            backgroundColor = backgroundColor ?? BaseColor.WHITE;
            AddCell(new PdfPCell(phrase)
            {
                MinimumHeight = height,
                BackgroundColor = backgroundColor,
                Colspan = colspan,
                Rowspan = rowspan,
                BorderWidth = borderWidth,
                BorderColor = ColorDark,
                VerticalAlignment = verticalAlignment,
                HorizontalAlignment = horizontalAlignment
            });
        }

        public void AddCustomCell(Paragraph paragraph,
            BaseColor backgroundColor = null,
            int horizontalAlignment = 0,
            int colspan = 1,
            float borderWidth = 0.5f)
        {
            PdfPCell cell = new PdfPCell(paragraph)
            {
                MinimumHeight = 0.0f,
                BackgroundColor = backgroundColor ?? BaseColor.WHITE,
                Colspan = colspan,
                BorderWidth = borderWidth,
                BorderColor = ColorDark,
                HorizontalAlignment = horizontalAlignment
            };
            AddCell(cell);
        }

        public void AddCustomCell(Image image,
            int horizontalAlignment = 0,
            int verticalAlignment = 4,
            int colspan = 1,
            int rowspan = 1,
            float borderWidth = 0.0f)
        {
            PdfPCell cell = new PdfPCell(image)
            {
                MinimumHeight = 0.0f,
                Colspan = colspan,
                Rowspan = rowspan,
                BorderWidth = borderWidth,
                BorderColor = ColorDark,
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = verticalAlignment
            };
            AddCell(cell);
        }

        public void AddStrikeOutCell(string text,
            bool condition = true,
            Font font = null,
            BaseColor backgroundColor = null)
        {
            font = font ?? FontBold;
            backgroundColor = backgroundColor ?? BaseColor.CYAN;
            if (condition)
                AddCustomCell(text, font, backgroundColor);
            else
                AddCustomCell(text);
        }

        public void BuildCodeAndValue(int code, int value)
        {
            AddCustomCell(code,
                FontBold,
                ColorLight,
                0.0f,
                1,
                1,
                5,
                1);
            AddCustomCell(value);
        }

        public void BuildCodeAndValue(int code, string value)
        {
            AddCustomCell(code,
                FontBold,
                ColorLight,
                0.0f,
                1,
                1,
                5,
                1);
            AddCustomCell(value);
        }

        public void BuildStatusCell(EditionStates editionState, BaseColor backgroundColor = null)
        {
            string text = string.Empty;
            switch (editionState)
            {
                case EditionStates.New:
                    text = "N";
                    break;
                case EditionStates.Updated:
                    text = "M";
                    break;
                case EditionStates.Deleted:
                    text = "S";
                    break;
            }

            AddCustomCell(text,
                FontBold,
                backgroundColor ?? ColorLight,
                0.0f,
                1,
                1,
                5,
                1);
        }

        #endregion
    }

    public static class AcPdfPTableExtensions
    {
        #region Services

        public static string GetText(this decimal value, string format = "")
        {
            return value != decimal.Zero ? value.ToString(format) : string.Empty;
        }

        public static string GetText(this int value)
        {
            return value != 0 ? value.ToString() : string.Empty;
        }

        public static string GetText(this bool value)
        {
            return value ? "Oui" : "";
        }

        public static string GetText(this int? value)
        {
            return value.HasValue && value.Value != 0 ? value.Value.ToString() : string.Empty;
        }

        #endregion
    }
}
