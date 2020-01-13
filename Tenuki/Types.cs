namespace Tenuki
{
    public enum Color : byte
    {
        Black = 0, // 先手
        White = 1, // 後手
        None = 2,
    }

    public static class ColorExtensions
    {
        public static Color Invirt(this Color c)
        {
            return (Color)((byte)c ^ 1);
        }
    }

    public enum Dir : sbyte
    {
        N = 0,
        E = 1,
        W = 2,
        S = 3,
    }

    public static class DirExtensions
    {
        public static int Value(this Dir d)
        {
            return 0;
        }
        public static bool IsFly(this Dir d)
        {
            return false;
        }
    }

    public enum Type : byte
    {
        Pawn = 0,  // 歩
        Lance = 1,  // 香
        Knight = 2,  // 桂
        Silver = 3,  // 銀
        Gold = 4,  // 金
        Bishop = 5,  // 角
        Rook = 6,  // 飛
        King = 7,  // 王
        PromotedPawn = 8,  // と
        PromotedLance = 9,  // 成香
        PromotedKnight = 10, // 成桂
        PromotedSilver = 11, // 成銀
        PromotedBishop = 12, // 馬
        PromotedRook = 13, // 龍
        Empty = 14, // 空
    }

    public enum Address : int
    {
        SQ11 = 0,
        SQ99 = 81,
    }

    public enum Square : byte
    {
        BlackPawn = 0,
        BlackLance = 1,
        BlackKnight = 2,
        BlackSilver = 3,
        BlackGold = 4,
        BlackBishop = 5,
        BlackRook = 6,
        BlackKing = 7,
        BlackPromotedPawn = 8,
        BlackPromotedLance = 9,
        BlackPromotedKnight = 10,
        BlackPromotedSilver = 11,
        BlackPromotedBishop = 12,
        BlackPromotedRook = 13,
        WhitePawn = 14,
        WhiteLance = 15,
        WhiteKnight = 16,
        WhiteSilver = 17,
        WhiteGold = 18,
        WhiteBishop = 19,
        WhiteRook = 20,
        WhiteKing = 21,
        WhitePromotedPawn = 22,
        WhitePromotedLance = 23,
        WhitePromotedKnight = 24,
        WhitePromotedSilver = 25,
        WhitePromotedBishop = 26,
        WhitePromotedRook = 27,
        Empty = 28,
    }

    public static class SqaureExtensions
    {
        public static readonly Dir[] Dirs = { Dir.N, Dir.S };

        public static Color Side(this Square s) => (byte)s switch
        {
            byte n when (n <= 13) => Color.Black,
            byte n when (n <= 27) => Color.White,
            _ => Color.None,
        };

        public static bool Promotable(this Square s) => s switch
        {
            Square.BlackPawn => true,
            Square.BlackLance => true,
            Square.BlackKnight => true,
            Square.BlackSilver => true,
            Square.BlackBishop => true,
            Square.BlackRook => true,
            Square.WhitePawn => true,
            Square.WhiteLance => true,
            Square.WhiteKnight => true,
            Square.WhiteSilver => true,
            Square.WhiteBishop => true,
            Square.WhiteRook => true,
            _ => false,
        };

        public static Square Promote(this Square s) => s switch
        {
            Square.BlackPawn => Square.BlackPromotedPawn,
            Square.BlackLance => Square.BlackPromotedLance,
            Square.BlackKnight => Square.BlackPromotedKnight,
            Square.BlackSilver => Square.BlackPromotedSilver,
            Square.BlackBishop => Square.BlackPromotedBishop,
            Square.BlackRook => Square.BlackPromotedRook,
            Square.WhitePawn => Square.WhitePromotedPawn,
            Square.WhiteLance => Square.WhitePromotedLance,
            Square.WhiteKnight => Square.WhitePromotedKnight,
            Square.WhiteSilver => Square.WhitePromotedSilver,
            Square.WhiteBishop => Square.WhitePromotedBishop,
            Square.WhiteRook => Square.WhitePromotedRook,
            _ => s,
        };

        public static Dir[] GetDirs(this Square s)
        {
            return Dirs;
        }
    }

    public struct Move
    {
        ushort _i;   
    }
}
