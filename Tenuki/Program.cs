using System;

namespace Tenuki
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            var sfen = "8l/1l+R2P3/p2pBG1pp/kps1p4/Nn1P2G2/P1P1P2PP/1PS6/1KSG3+r1/LN2+p3L w Sbgn3p 124";
            Position p = Position.Parse(sfen);
            Console.WriteLine(p.ToKi2());
            Console.WriteLine(p.ToSfen());

            Console.WriteLine("Hello World!");
            Console.WriteLine(sizeof(Position));
        }
    }
}
