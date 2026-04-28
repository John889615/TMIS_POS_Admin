using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace POS_Api.Helpers
{
    public class EscPosHelper
    {
        private readonly NetworkStream _stream;

        public EscPosHelper(NetworkStream stream)
        {
            _stream = stream;
        }

        public void Write(string text, bool center = false, bool bold = false)
        {
            var builder = new List<byte>();

            // Reset
            builder.Add(0x1B); builder.Add(0x40); // Initialize printer

            // Align
            builder.Add(0x1B); builder.Add(0x61); builder.Add((byte)(center ? 1 : 0));

            // Bold
            builder.Add(0x1B); builder.Add(0x45); builder.Add((byte)(bold ? 1 : 0));

            builder.AddRange(Encoding.UTF8.GetBytes(text));
            builder.Add(0x0A); // Line feed

            _stream.Write(builder.ToArray(), 0, builder.Count);
        }

        public void PrintBlankLines(int lines = 3)
        {
            for (int i = 0; i < lines; i++)
                _stream.WriteByte(0x0A); // Line feed
        }

        public void CutPaper()
        {
            byte[] cut = new byte[] { 0x1D, 0x56, 0x41, 0x10 }; // Partial cut
            _stream.Write(cut, 0, cut.Length);
        }

        public void PrintImage(string imagePath)
        {
            using var bitmap = SKBitmap.Decode(imagePath);
            using var resized = bitmap.Resize(new SKImageInfo(384, bitmap.Height * 384 / bitmap.Width), SKFilterQuality.Medium); // 384px for 80mm

            var bytes = GetRasterImageBytes(resized);
            _stream.Write(bytes, 0, bytes.Length);
        }

        private byte[] GetRasterImageBytes(SKBitmap bmp)
        {
            var commands = new List<byte>();
            commands.Add(0x1D); commands.Add(0x76); commands.Add(0x30); commands.Add(0x00);

            int width = bmp.Width;
            int height = bmp.Height;
            int bytesPerRow = (width + 7) / 8;

            commands.Add((byte)(bytesPerRow % 256));
            commands.Add((byte)(bytesPerRow / 256));
            commands.Add((byte)(height % 256));
            commands.Add((byte)(height / 256));

            for (int y = 0; y < height; y++)
            {
                for (int xByte = 0; xByte < bytesPerRow; xByte++)
                {
                    byte b = 0;
                    for (int bit = 0; bit < 8; bit++)
                    {
                        int x = xByte * 8 + bit;
                        if (x >= width) break;
                        var pixel = bmp.GetPixel(x, y);
                        var brightness = (pixel.Red + pixel.Green + pixel.Blue) / 3;
                        if (brightness < 127)
                            b |= (byte)(1 << (7 - bit));
                    }
                    commands.Add(b);
                }
            }

            return commands.ToArray();
        }
    }
}
