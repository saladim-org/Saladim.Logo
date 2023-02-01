using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using Path = SixLabors.ImageSharp.Drawing.Path;
using PointF = SixLabors.ImageSharp.PointF;

namespace SaladimLogo;

public class Program
{
    public const float Width = 500;
    public const float Height = 500;

    public static void Main(string[] args)
    {
        using Image<Rgba32> image = new((int)Width, (int)Height, new Color(new Rgba32(0xEC, 0xEC, 0xEC)));
        image.Mutate(p =>
        {
            Color circleColor = new(new Rgba32(0x53, 0xB5, 0xEA));
            float r = Width / 10;
            float goldenRadio = 1f - ((MathF.Sqrt(5f) - 1f) / 2f);

            PointF leftTop = new(Width * goldenRadio - r, Height * goldenRadio - r);
            PointF rightTop = new(Width * (1f - goldenRadio) + r, Height * goldenRadio - r);
            PointF rightBottom = new(Width * (1f - goldenRadio) + r, Height * (1f - goldenRadio) + r);
            PointF leftBottom = new(Width * goldenRadio - r, Height * (1f - goldenRadio) + r);


            IPen bridgePen = Pens.Solid(new Rgba32(0x53, 0xB5, 0xEA), r * (1f - goldenRadio) / 2);
            p.DrawLines(bridgePen, leftTop, rightTop, rightBottom);
            bridgePen = Pens.Dash(new Rgba32(0xA8, 0xD0, 0xE4), r * (1f - goldenRadio) / 3);
            p.DrawLines(bridgePen, leftBottom, rightTop);
            bridgePen = Pens.Dash(new Rgba32(0x52, 0xB4, 0xE9), r * (1f - goldenRadio) / 4);
            p.DrawLines(bridgePen, leftBottom, leftTop);
            p.DrawLines(bridgePen, leftBottom, rightBottom);


            p.Fill(Brushes.Solid(circleColor), new EllipsePolygon(leftTop, r));
            p.Fill(Brushes.Solid(circleColor), new EllipsePolygon(rightTop, Width / 4 * goldenRadio));
            p.Fill(Brushes.Solid(circleColor), new EllipsePolygon(rightBottom, r));
            p.Fill(Brushes.Solid(circleColor), new EllipsePolygon(leftBottom, Width / 4 * goldenRadio));
        });
        image.SaveAsPng(@"D:\文档\temp\test.png");
    }
}
