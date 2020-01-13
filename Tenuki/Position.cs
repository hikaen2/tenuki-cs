using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Tenuki
{

    public enum Color : byte
    {
        Black = 0,
        White = 1,
    }

    public struct Square
    {
        private readonly byte _i;
        public Square(byte i) {
            _i = i;
        }
        public bool IsFriendOf(Color side) {
            return false;
        }
    }

    public unsafe struct Position
    {
        public fixed byte board[81];
        public fixed byte piecesInHand[16];
        public Color siteToMove;

        public static Position Parse(string sfen)
        {
            string[] tokens = sfen.Split(' ');
            if (tokens.Length != 4) {
                throw new FormatException(string.Format("sfen: \"{0}\"", sfen));
            }
            StringBuilder boardState = new StringBuilder(tokens[0]);
            string sideToMove = tokens[1];
            string pircesInHand = tokens[2];
            string moveCount = tokens[3];

            // Side to Move
            if (sideToMove != "b" && sideToMove != "w") {
                throw new ArgumentException(string.Format("sideToMove: \"{0}\"", sideToMove));
            }

            // Pieces in Hand
            if (pircesInHand != "-") {
                foreach(Match m in Regex.Matches(pircesInHand, @"(\d*)(\D)")) {
                    int num = m.Groups[1].Length == 0 ? 1 : int.Parse(m.Groups[1].ToString());
                    string p = m.Groups[2].ToString();
                }
            }

            // Board state
            for (int i = 2; i <= 9; i++) {
                boardState.Replace(i.ToString(), new string('1', i));
            }
            boardState.Replace("/", "");
            MatchCollection matches = Regex.Matches(boardState.ToString(), @"\+?.");
            int cur = 0;
            Position position;
            for (int rank = 0; rank <= 8; rank++) {
                for (int file = 8; file >= 0; file--) {
                    var s = matches[cur++].ToString();
                    position.board[file * 9 + rank] = s switch {
                        "P" =>  0, "L" =>  1, "N" =>  2, "S" =>  3, "G" =>  4, "B" =>  5, "R" =>  6, "K" =>  7, "+P" =>  8, "+L" =>  9, "+N" => 10, "+S" => 11, "+B" => 12, "+R" => 13,
                        "p" => 14, "l" => 15, "n" => 16, "s" => 17, "g" => 18, "b" => 19, "r" => 20, "k" => 21, "+p" => 22, "+l" => 23, "+n" => 24, "+s" => 25, "+b" => 26, "+r" => 27,
                        "1" => 28,
                        _   => throw new ArgumentException(s),
                    };
                }
            }

            // Move count
            int mc = int.Parse(moveCount);
            position.siteToMove = Color.Black;

            return position;
        }
    }

    public static class PositionExtensions
    {
        public static unsafe void LegalMoves(this Position p) {
            for (int from = 0; from < 81; from++) {
                if (!new Square(p.board[from]).IsFriendOf(p.siteToMove)) {
                    continue;
                }
            }
        }



        public static unsafe string ToSfen(this Position p)
        {
            //   歩,  香,  桂,  銀,  金,  角,  飛,  王,  と,   成香, 成桂, 成銀, 馬,   龍,
            string[] TO_SFEN = {
                "P", "L", "N", "S", "G", "B", "R", "K", "+P", "+L", "+N", "+S", "+B", "+R",
                "p", "l", "n", "s", "g", "b", "r", "k", "+p", "+l", "+n", "+s", "+b", "+r", "1",
            };

            StringBuilder line = new StringBuilder();
            for (int rank = 0; rank <= 8; rank++) {
                for (int file = 8; file >= 0; file--) {
                    line.Append(TO_SFEN[p.board[file * 9 + rank]]);
                }
                if (rank < 8) {
                    line.Append('/');
                }
            }
            for (int i = 9; i >= 2; i--) {
                line.Replace(new string('1', i), i.ToString()); // '1'をまとめる
            }
            return line.ToString();
        }

        public static unsafe string ToKi2(this Position p)
        {
            string[] BOARD = {
                " 歩", " 香", " 桂", " 銀", " 金", " 角", " 飛", " 玉", " と", " 杏", " 圭", " 全", " 馬", " 龍",
                "v歩", "v香", "v桂", "v銀", "v金", "v角", "v飛", "v玉", "vと", "v杏", "v圭", "v全", "v馬", "v龍", " ・",
            };
            string[] NUM = {"〇", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八"};

            StringBuilder sb = new StringBuilder();

            sb.Append("  ９ ８ ７ ６ ５ ４ ３ ２ １\n");
            sb.Append("+---------------------------+\n");
            for (int rank = 0; rank <= 8; rank++) {
                sb.Append("|");
                for (int file = 8; file >= 0; file--) {
                    sb.AppendFormat("{0}", BOARD[p.board[file * 9 + rank]]);
                }
                sb.AppendFormat("|{0}\n", NUM[rank + 1]);
            }
            sb.Append("+---------------------------+\n");

            return sb.ToString();
        }


    }   

}
