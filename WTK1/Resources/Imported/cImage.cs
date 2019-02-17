using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WinToolkit
{
	 public static class cImage
	 {
		  public static void CompressImage(string Target, string SaveTo, long TargetSizeByte)
		  {
				try
				{
					 var F = new FileInfo(Target);
					 int S = 50;
					 double NS = F.Length;

					 using (Image a = Image.FromFile(Target))
					 {
						  while (NS > TargetSizeByte && S >= 0)
						  {
								var F2 = new FileInfo(SaveTo);
								if (SaveJPGWithCompressionSetting(a, SaveTo, S) == false) { break; }
								NS = F2.Length;
								S--;
						  }
					 }
				}
				catch (Exception Ex)
				{
					LargeError LE = new LargeError("Image Compression Error","Unable to compress image.","Target: " + Target + " | SaveTo: " + SaveTo,Ex);
					LE.Upload();
					LE.ShowDialog();
				}
				cMain.FreeRAM();
		  }

		  public static void ChangeRes(string Source, string SaveTo, int W, int H)
		  {
				Graphics g = null;
				try
				{
					 using (var bmp = new Bitmap(Source))
					 {
						  using (var bmp2 = new Bitmap(W, H, PixelFormat.Format24bppRgb))
						  {
								g = Graphics.FromImage(bmp2);

								g.InterpolationMode = InterpolationMode.HighQualityBicubic;
								g.DrawImage(bmp, 0, 0, bmp2.Width, bmp2.Height);

								bmp2.Save(SaveTo + W + "x" + H + ".jpg");
						  }
					 }

				}
				catch (Exception Ex)
				{

					new SmallError("Unable to change resolution.", Ex, "Source: " + Source + " | SaveTo: " + SaveTo + " | Resolution: w" + W.ToString() + " h" + H.ToString()).Upload();
				}
				if (g != null) { g.Dispose(); }
				cMain.FreeRAM();
		  }

		  private static bool SaveJPGWithCompressionSetting(Image image, string szFileName, long lCompression)
		  {
				try
				{
					 var eps = new EncoderParameters(1);
					 eps.Param[0] = new EncoderParameter(Encoder.Quality, (long)lCompression);
					 ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
					 image.Save(szFileName, ici, eps);
					 return true;
				}
				catch (Exception Ex)
				{

					 cMain.WriteLog(null, "Unable to save as JPG.", Ex.Message, "Filename: " + szFileName + " | Compression: " + lCompression.ToString());
					 return false;
				}
				cMain.FreeRAM();
		  }

		  private static ImageCodecInfo GetEncoderInfo(string mimeType)
		  {
				int j;

				ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
				for (j = 0; j <= encoders.Length; j++)
				{
					 if (encoders[j].MimeType == mimeType)
					 {
						  return encoders[j];
					 }
				}
				cMain.FreeRAM();
				return null;
		  }
	 }
}