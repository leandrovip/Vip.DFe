﻿using System;
using System.Drawing;
using System.Text;
using org.pdfclown.documents.contents.composition;
using Vip.DFe.Danfe.Enum;

namespace Vip.DFe.Extensions
{
    internal static class DanfeExtensions
    {
        private const float PointFactor = 72F / 25.4F;

        /// <summary>
        ///     Converts Millimeters to Point
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        public static float ToPoint(this float mm) => PointFactor * mm;

        /// <summary>
        ///     Converts Point to Millimeters
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static float ToMm(this float point) => point / PointFactor;

        /// <summary>
        ///     Converts Point to Millimeters
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static SizeF ToMm(this SizeF s) => new SizeF(s.Width.ToMm(), s.Height.ToMm());

        /// <summary>
        ///     Converts Point to Millimeters
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static SizeF ToPointMeasure(this SizeF s) => new SizeF(s.Width.ToPoint(), s.Height.ToPoint());

        /// <summary>
        ///     Converts Millimeters to Point
        /// </summary>
        /// <param name="mm"></param>
        /// <returns></returns>
        public static double ToPoint(this double mm) => PointFactor * mm;

        /// <summary>
        ///     Converts Point to Millimeters
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static double ToMm(this double point) => point / PointFactor;

        public static RectangleF InflatedRetangle(this RectangleF rect, float top, float button, float horizontal) => new RectangleF(rect.X + horizontal, rect.Y + top, rect.Width - 2 * horizontal, rect.Height - top - button);

        public static RectangleF InflatedRetangle(this RectangleF rect, float value) => rect.InflatedRetangle(value, value, value);

        public static RectangleF ToPointMeasure(this RectangleF r) => new RectangleF(r.X.ToPoint(), r.Y.ToPoint(), r.Width.ToPoint(), r.Height.ToPoint());

        public static RectangleF CutTop(this RectangleF r, float height) => new RectangleF(r.X, r.Y + height, r.Width, r.Height - height);
        public static RectangleF CutBottom(this RectangleF r, float height) => new RectangleF(r.X, r.Y, r.Width, r.Height - height);
        public static RectangleF CutLeft(this RectangleF r, float width) => new RectangleF(r.X + width, r.Y, r.Width - width, r.Height);

        public static PointF ToPointMeasure(this PointF r) => new PointF(r.X.ToPoint(), r.Y.ToPoint());

        public static StringBuilder AppendChaveValor(this StringBuilder sb, string chave, string valor)
        {
            if (sb.Length > 0) sb.Append(' ');
            if (chave.IsNotNullOrEmpty()) sb.Append(chave).Append(": ");
            return sb.Append(valor);
        }

        public static XAlignmentEnum ToPdfClownAlignment(this AlinhamentoHorizontal ah)
        {
            switch (ah)
            {
                case AlinhamentoHorizontal.Esquerda:
                    return XAlignmentEnum.Left;
                case AlinhamentoHorizontal.Centro:
                    return XAlignmentEnum.Center;
                case AlinhamentoHorizontal.Direita:
                    return XAlignmentEnum.Right;
            }

            throw new InvalidOperationException();
        }

        public static YAlignmentEnum ToPdfClownAlignment(this AlinhamentoVertical av)
        {
            switch (av)
            {
                case AlinhamentoVertical.Topo:
                    return YAlignmentEnum.Top;
                case AlinhamentoVertical.Centro:
                    return YAlignmentEnum.Middle;
                case AlinhamentoVertical.Base:
                    return YAlignmentEnum.Bottom;
            }

            throw new InvalidOperationException();
        }
    }
}