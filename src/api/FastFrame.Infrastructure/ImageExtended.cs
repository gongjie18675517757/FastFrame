using System;
using System.DrawingCore;
using System.DrawingCore.Drawing2D;
using System.DrawingCore.Imaging;
using System.IO;

namespace FastFrame.Infrastructure
{
    /// <summary>
    /// 图片处理扩展
    /// </summary>
    public sealed class ImageExtended
    {
        /// <summary>
        /// 压缩图片
        /// </summary>
        /// <param name="sFile"></param>
        /// <param name="getWidthHeightFunc"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static Stream GetPicThumbnail(Stream sFile, Func<Image, (int width, int height)> getWidthHeightFunc, int flag)
        {
            sFile.Position = 0;
            var iSource = Image.FromStream(sFile);
            var (dWidth, dHeight) = getWidthHeightFunc(iSource);
            ImageFormat tFormat = iSource.RawFormat;

            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            int sW;
            int sH;
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();
            //以下代码为保存图片时，设置压缩质量  
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100  
            EncoderParameter eParam = new EncoderParameter(Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }

                if (jpegICIinfo != null)
                {
                    var outputStream = new MemoryStream();
                    ob.Save(outputStream, jpegICIinfo, ep);//dFile是压缩后的新路径  
                    return outputStream;
                }
                else
                {
                    var outputStream = new MemoryStream();
                    ob.Save(outputStream, tFormat);
                    return outputStream;
                } 
            }
            catch
            {
                return null;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }

        /// 无损压缩图片  
        /// <param name="sFile">原图片</param>   
        /// <param name="dHeight">高度</param>  
        /// <param name="dWidth"></param>  
        /// <param name="flag">压缩质量(数字越小压缩率越高) 1-100</param>  
        /// <returns></returns>  
        public static Stream GetPicThumbnail(Stream sFile, int dHeight, int dWidth, int flag)
        {
            return GetPicThumbnail(sFile, x => (dWidth, dHeight), flag);
        } 
    }
}
