#region

using System.Globalization;
using Color = UnityEngine.Color;

#endregion

namespace Hax;

static partial class Helper {
    internal static Color HexToColor(string hexColor) {
        if (hexColor.IndexOf('#') != -1) hexColor = hexColor.Replace("#", "");

        float r = int.Parse(hexColor[..2], NumberStyles.AllowHexSpecifier) / 255f;
        float g = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier) / 255f;
        float b = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier) / 255f;
        return new Color(r, g, b);
    }


    internal static Color GetLootColor(GrabbableObject scrap) =>
        scrap switch {
            null => ExtraColors.Transparent,
            RagdollGrabbableObject => ExtraColors.HotPink,
            LungProp => ExtraColors.LightBlue,
            ShotgunItem => ExtraColors.Khaki,
            KnifeItem => ExtraColors.DarkKhaki,
            HauntedMaskItem => ExtraColors.MediumVioletRed,
            _ when scrap.itemProperties.isDefensiveWeapon => ExtraColors.LawnGreen,
            _ when scrap.GetScrapValue() > 50 => ExtraColors.Gold,
            _ when scrap.GetScrapValue() is > 15 and <= 35 => ExtraColors.LightSalmon,
            _ when scrap.GetScrapValue() >= 36 => ExtraColors.GreenYellow,
            _ when scrap.GetScrapValue() == 0 && scrap is GiftBoxItem => ExtraColors.PapayaWhip,
            _ => ExtraColors.Silver
        };


    internal readonly struct ExtraColors {
        internal static Color MediumPurple { get; } = new(0.5764706f, 0.4392157f, 0.8588235f, 1f);
        internal static Color MediumSeaGreen { get; } = new(0.2352941f, 0.7019608f, 0.4431373f, 1f);
        internal static Color MediumSlateBlue { get; } = new(0.4823529f, 0.4078431f, 0.9333333f, 1f);
        internal static Color MediumSpringGreen { get; } = new(0f, 0.9803922f, 0.6039216f, 1f);
        internal static Color MediumTurquoise { get; } = new(0.282353f, 0.8196079f, 0.8f, 1f);
        internal static Color MediumVioletRed { get; } = new(0.7803922f, 0.08235294f, 0.5215687f, 1f);
        internal static Color MidnightBlue { get; } = new(0.09803922f, 0.09803922f, 0.4392157f, 1f);
        internal static Color MediumOrchid { get; } = new(0.7294118f, 0.3333333f, 0.827451f, 1f);
        internal static Color MintCream { get; } = new(0.9607843f, 1f, 0.9803922f, 1f);
        internal static Color Moccasin { get; } = new(1f, 0.8941177f, 0.7098039f, 1f);
        internal static Color NavajoWhite { get; } = new(1f, 0.8705882f, 0.6784314f, 1f);
        internal static Color Navy { get; } = new(0f, 0f, 0.5019608f, 1f);
        internal static Color OldLace { get; } = new(0.9921569f, 0.9607843f, 0.9019608f, 1f);
        internal static Color Olive { get; } = new(0.5019608f, 0.5019608f, 0f, 1f);
        internal static Color OliveDrab { get; } = new(0.4196078f, 0.5568628f, 0.1372549f, 1f);
        internal static Color Orange { get; } = new(1f, 0.6470588f, 0f, 1f);
        internal static Color MistyRose { get; } = new(1f, 0.8941177f, 0.8823529f, 1f);
        internal static Color OrangeRed { get; } = new(1f, 0.2705882f, 0f, 1f);
        internal static Color MediumBlue { get; } = new(0f, 0f, 0.8039216f, 1f);
        internal static Color Maroon { get; } = new(0.5019608f, 0f, 0f, 1f);
        internal static Color LightBlue { get; } = new(0.6784314f, 0.8470588f, 0.9019608f, 1f);
        internal static Color LightCoral { get; } = new(0.9411765f, 0.5019608f, 0.5019608f, 1f);
        internal static Color LightGoldenrodYellow { get; } = new(0.9803922f, 0.9803922f, 0.8235294f, 1f);
        internal static Color LightGreen { get; } = new(0.5647059f, 0.9333333f, 0.5647059f, 1f);
        internal static Color LightGray { get; } = new(0.827451f, 0.827451f, 0.827451f, 1f);
        internal static Color LightPink { get; } = new(1f, 0.7137255f, 0.7568628f, 1f);
        internal static Color LightSalmon { get; } = new(1f, 0.627451f, 0.4784314f, 1f);
        internal static Color MediumAquamarine { get; } = new(0.4f, 0.8039216f, 0.6666667f, 1f);
        internal static Color LightSeaGreen { get; } = new(0.1254902f, 0.6980392f, 0.6666667f, 1f);
        internal static Color LightSlateGray { get; } = new(0.4666667f, 0.5333334f, 0.6f, 1f);
        internal static Color LightSteelBlue { get; } = new(0.6901961f, 0.7686275f, 0.8705882f, 1f);
        internal static Color LightYellow { get; } = new(1f, 1f, 0.8784314f, 1f);
        internal static Color Lime { get; } = new(0f, 1f, 0f, 1f);
        internal static Color LimeGreen { get; } = new(0.1960784f, 0.8039216f, 0.1960784f, 1f);
        internal static Color Linen { get; } = new(0.9803922f, 0.9411765f, 0.9019608f, 1f);
        internal static Color Magenta { get; } = new(1f, 0f, 1f, 1f);
        internal static Color LightSkyBlue { get; } = new(0.5294118f, 0.8078431f, 0.9803922f, 1f);
        internal static Color LemonChiffon { get; } = new(1f, 0.9803922f, 0.8039216f, 1f);
        internal static Color Orchid { get; } = new(0.854902f, 0.4392157f, 0.8392157f, 1f);
        internal static Color PaleGreen { get; } = new(0.5960785f, 0.9843137f, 0.5960785f, 1f);
        internal static Color SlateBlue { get; } = new(0.4156863f, 0.3529412f, 0.8039216f, 1f);
        internal static Color SlateGray { get; } = new(0.4392157f, 0.5019608f, 0.5647059f, 1f);
        internal static Color Snow { get; } = new(1f, 0.9803922f, 0.9803922f, 1f);
        internal static Color SpringGreen { get; } = new(0f, 1f, 0.4980392f, 1f);
        internal static Color SteelBlue { get; } = new(0.2745098f, 0.509804f, 0.7058824f, 1f);
        internal static Color Tan { get; } = new(0.8235294f, 0.7058824f, 0.5490196f, 1f);
        internal static Color Teal { get; } = new(0f, 0.5019608f, 0.5019608f, 1f);
        internal static Color SkyBlue { get; } = new(0.5294118f, 0.8078431f, 0.9215686f, 1f);
        internal static Color Thistle { get; } = new(0.8470588f, 0.7490196f, 0.8470588f, 1f);
        internal static Color Turquoise { get; } = new(0.2509804f, 0.8784314f, 0.8156863f, 1f);
        internal static Color Violet { get; } = new(0.9333333f, 0.509804f, 0.9333333f, 1f);
        internal static Color Wheat { get; } = new(0.9607843f, 0.8705882f, 0.7019608f, 1f);
        internal static Color White { get; } = new(1f, 1f, 1f, 1f);
        internal static Color WhiteSmoke { get; } = new(0.9607843f, 0.9607843f, 0.9607843f, 1f);
        internal static Color Yellow { get; } = new(1f, 1f, 0f, 1f);
        internal static Color YellowGreen { get; } = new(0.6039216f, 0.8039216f, 0.1960784f, 1f);
        internal static Color Tomato { get; } = new(1f, 0.3882353f, 0.2784314f, 1f);
        internal static Color PaleGoldenrod { get; } = new(0.9333333f, 0.9098039f, 0.6666667f, 1f);
        internal static Color Silver { get; } = new(0.7529412f, 0.7529412f, 0.7529412f, 1f);
        internal static Color SeaShell { get; } = new(1f, 0.9607843f, 0.9333333f, 1f);
        internal static Color PaleTurquoise { get; } = new(0.6862745f, 0.9333333f, 0.9333333f, 1f);
        internal static Color PaleVioletRed { get; } = new(0.8588235f, 0.4392157f, 0.5764706f, 1f);
        internal static Color PapayaWhip { get; } = new(1f, 0.9372549f, 0.8352941f, 1f);
        internal static Color PeachPuff { get; } = new(1f, 0.854902f, 0.7254902f, 1f);
        internal static Color Peru { get; } = new(0.8039216f, 0.5215687f, 0.2470588f, 1f);
        internal static Color Pink { get; } = new(1f, 0.7529412f, 0.7960784f, 1f);
        internal static Color Plum { get; } = new(0.8666667f, 0.627451f, 0.8666667f, 1f);
        internal static Color Sienna { get; } = new(0.627451f, 0.3215686f, 0.1764706f, 1f);
        internal static Color PowderBlue { get; } = new(0.6901961f, 0.8784314f, 0.9019608f, 1f);
        internal static Color Red { get; } = new(1f, 0f, 0f, 1f);
        internal static Color RosyBrown { get; } = new(0.7372549f, 0.5607843f, 0.5607843f, 1f);
        internal static Color RoyalBlue { get; } = new(0.254902f, 0.4117647f, 0.8823529f, 1f);
        internal static Color SaddleBrown { get; } = new(0.5450981f, 0.2705882f, 0.07450981f, 1f);
        internal static Color Salmon { get; } = new(0.9803922f, 0.5019608f, 0.4470588f, 1f);
        internal static Color SandyBrown { get; } = new(0.9568627f, 0.6431373f, 0.3764706f, 1f);
        internal static Color SeaGreen { get; } = new(0.1803922f, 0.5450981f, 0.3411765f, 1f);
        internal static Color Purple { get; } = new(0.5019608f, 0f, 0.5019608f, 1f);
        internal static Color LawnGreen { get; } = new(0.4862745f, 0.9882353f, 0f, 1f);
        internal static Color LightCyan { get; } = new(0.8784314f, 1f, 1f, 1f);
        internal static Color Lavender { get; } = new(0.9019608f, 0.9019608f, 0.9803922f, 1f);
        internal static Color DarkKhaki { get; } = new(0.7411765f, 0.7176471f, 0.4196078f, 1f);
        internal static Color DarkGreen { get; } = new(0f, 0.3921569f, 0f, 1f);
        internal static Color DarkGray { get; } = new(0.6627451f, 0.6627451f, 0.6627451f, 1f);
        internal static Color DarkGoldenrod { get; } = new(0.7215686f, 0.5254902f, 0.04313726f, 1f);
        internal static Color DarkCyan { get; } = new(0f, 0.5450981f, 0.5450981f, 1f);
        internal static Color DarkBlue { get; } = new(0f, 0f, 0.5450981f, 1f);
        internal static Color Cyan { get; } = new(0f, 1f, 1f, 1f);
        internal static Color Crimson { get; } = new(0.8627451f, 0.07843138f, 0.2352941f, 1f);
        internal static Color Cornsilk { get; } = new(1f, 0.972549f, 0.8627451f, 1f);
        internal static Color LavenderBlush { get; } = new(1f, 0.9411765f, 0.9607843f, 1f);
        internal static Color Coral { get; } = new(1f, 0.4980392f, 0.3137255f, 1f);
        internal static Color Chocolate { get; } = new(0.8235294f, 0.4117647f, 0.1176471f, 1f);
        internal static Color Chartreuse { get; } = new(0.4980392f, 1f, 0f, 1f);
        internal static Color DarkMagenta { get; } = new(0.5450981f, 0f, 0.5450981f, 1f);
        internal static Color CadetBlue { get; } = new(0.372549f, 0.6196079f, 0.627451f, 1f);
        internal static Color Brown { get; } = new(0.6470588f, 0.1647059f, 0.1647059f, 1f);
        internal static Color BlueViolet { get; } = new(0.5411765f, 0.1686275f, 0.8862745f, 1f);
        internal static Color Blue { get; } = new(0f, 0f, 1f, 1f);
        internal static Color BlanchedAlmond { get; } = new(1f, 0.9215686f, 0.8039216f, 1f);
        internal static Color Black { get; } = new(0f, 0f, 0f, 1f);
        internal static Color Bisque { get; } = new(1f, 0.8941177f, 0.7686275f, 1f);
        internal static Color Beige { get; } = new(0.9607843f, 0.9607843f, 0.8627451f, 1f);
        internal static Color Azure { get; } = new(0.9411765f, 1f, 1f, 1f);
        internal static Color Aquamarine { get; } = new(0.4980392f, 1f, 0.8313726f, 1f);
        internal static Color Aqua { get; } = new(0f, 1f, 1f, 1f);
        internal static Color AntiqueWhite { get; } = new(0.9803922f, 0.9215686f, 0.8431373f, 1f);
        internal static Color AliceBlue { get; } = new(0.9411765f, 0.972549f, 1f, 1f);
        internal static Color Transparent { get; } = new(1f, 1f, 1f, 1f);
        internal static Color BurlyWood { get; } = new(0.8705882f, 0.7215686f, 0.5294118f, 1f);
        internal static Color DarkOliveGreen { get; } = new(0.3333333f, 0.4196078f, 0.1843137f, 1f);
        internal static Color CornflowerBlue { get; } = new(0.3921569f, 0.5843138f, 0.9294118f, 1f);
        internal static Color DarkOrchid { get; } = new(0.6f, 0.1960784f, 0.8f, 1f);
        internal static Color Khaki { get; } = new(0.9411765f, 0.9019608f, 0.5490196f, 1f);
        internal static Color Ivory { get; } = new(1f, 1f, 0.9411765f, 1f);
        internal static Color DarkOrange { get; } = new(1f, 0.5490196f, 0f, 1f);
        internal static Color Indigo { get; } = new(0.2941177f, 0f, 0.509804f, 1f);
        internal static Color IndianRed { get; } = new(0.8039216f, 0.3607843f, 0.3607843f, 1f);
        internal static Color HotPink { get; } = new(1f, 0.4117647f, 0.7058824f, 1f);
        internal static Color Honeydew { get; } = new(0.9411765f, 1f, 0.9411765f, 1f);
        internal static Color GreenYellow { get; } = new(0.6784314f, 1f, 0.1843137f, 1f);
        internal static Color Green { get; } = new(0f, 0.5019608f, 0f, 1f);
        internal static Color Gray { get; } = new(0.5019608f, 0.5019608f, 0.5019608f, 1f);
        internal static Color Goldenrod { get; } = new(0.854902f, 0.6470588f, 0.1254902f, 1f);
        internal static Color GhostWhite { get; } = new(0.972549f, 0.972549f, 1f, 1f);
        internal static Color Gainsboro { get; } = new(0.8627451f, 0.8627451f, 0.8627451f, 1f);
        internal static Color Fuchsia { get; } = new(1f, 0f, 1f, 1f);
        internal static Color Gold { get; } = new(1f, 0.8431373f, 0f, 1f);
        internal static Color FloralWhite { get; } = new(1f, 0.9803922f, 0.9411765f, 1f);
        internal static Color DarkRed { get; } = new(0.5450981f, 0f, 0f, 1f);
        internal static Color DarkSalmon { get; } = new(0.9137255f, 0.5882353f, 0.4784314f, 1f);
        internal static Color DarkSeaGreen { get; } = new(0.5607843f, 0.7372549f, 0.5450981f, 1f);
        internal static Color ForestGreen { get; } = new(0.1333333f, 0.5450981f, 0.1333333f, 1f);
        internal static Color DarkSlateGray { get; } = new(0.1843137f, 0.3098039f, 0.3098039f, 1f);
        internal static Color DarkTurquoise { get; } = new(0f, 0.8078431f, 0.8196079f, 1f);
        internal static Color DarkSlateBlue { get; } = new(0.282353f, 0.2392157f, 0.5450981f, 1f);
        internal static Color DeepPink { get; } = new(1f, 0.07843138f, 0.5764706f, 1f);
        internal static Color DeepSkyBlue { get; } = new(0f, 0.7490196f, 1f, 1f);
        internal static Color DimGray { get; } = new(0.4117647f, 0.4117647f, 0.4117647f, 1f);
        internal static Color DodgerBlue { get; } = new(0.1176471f, 0.5647059f, 1f, 1f);
        internal static Color Firebrick { get; } = new(0.6980392f, 0.1333333f, 0.1333333f, 1f);
        internal static Color DarkViolet { get; } = new(0.5803922f, 0f, 0.827451f, 1f);
    }
}
