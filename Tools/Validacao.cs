using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Tools
{
    public class Validacao
    {
        public static Image ResizeImage(Image img, double maxWidth, double maxHeight)
        {
            double resizeWidth = img.Width;
            double resizeHeight = img.Height;

            double aspect = resizeWidth / resizeHeight;

            if (resizeWidth > maxWidth)
            {
                resizeWidth = maxWidth;
                resizeHeight = resizeWidth / aspect;
            }
            if (resizeHeight > maxHeight)
            {
                aspect = resizeWidth / resizeHeight;
                resizeHeight = maxHeight;
                resizeWidth = resizeHeight * aspect;
            }

            return img.GetThumbnailImage(Convert.ToInt32(resizeWidth), Convert.ToInt32(resizeHeight), null, IntPtr.Zero);
        }

        public static String MaskFloatMoeda(String valor)
        {
            string str = "";
            if (valor != "")
            {
                String valorFormatado = valor.Replace(".", ",");
                valorFormatado = Convert.ToDouble(valorFormatado).ToString("C");
                str = valorFormatado;
            }
            return str;
        }

        public static String MaskFloatMoeda2(String valor)
        {
            String valorFormatado = Convert.ToDouble(valor).ToString("C");
            Double Formatado = Convert.ToDouble(valorFormatado.Replace("R$", ""));
            return Formatado.ToString();
        }

        public static String MaskMoedaFloat(String valor)
        {
            string str = "";
            Double valorFormatado;
            if (valor != "")
            {
                str = valor.Replace("R$", "");
                if (Double.TryParse(str, out valorFormatado))
                {
                    str = valorFormatado.ToString();
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }
    }
}
