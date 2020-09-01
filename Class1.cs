using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using sc = System.Console;

namespace MyRound{                                      //version1      4/12        例外対応なし
    public class Class1{

        public static double round(double target, int decpla) {

            string bunnkai = target.ToString();         //1.2*10^(-5)→1.2E-05
                                                        //1.2*10^(-4)→0.00012
                                                        //0.03001    →0.03001
            

            int point = 0;
            int place = 0;
            int number = 0;
            string matchword = null;


            if(target >= 0.0001) {
                point = bunnkai.IndexOf(".");   //四捨五入される位を求めるスタート位置を特定
                place = point + decpla;         //四捨五入される位を特定
                number = int.Parse(bunnkai[place].ToString());      //位の数値をint化
                if(number >= 5) {                                   //char → string → int
                    target += Math.Pow(10, -(decpla - 1));          //切り上げを行う(正確には違う)
                    bunnkai = target.ToString().Substring(0, place);//切り捨てを行う(切り上げ完了)
                } else {
                    bunnkai = target.ToString().Substring(0, place);
                }
            } else if(target < 0.0001) {
                var reg = new Regex(@"E-\d{2}");
                var match = reg.Match(bunnkai);
                place = int.Parse(match.Value.Substring(2, 2));     //Substring(何個目から,何個拾う)
                number = int.Parse(reg.Replace(bunnkai, "")[decpla + 1 - place].ToString());    //「.」にあわせるため、+1。bunnkaiはE-○○を失っているため、decplaにplaceで引いて、位置調整。
                if(number >= 5) {                                   
                    target += Math.Pow(10, -(decpla - 1));
                    bunnkai = reg.Replace(target.ToString(), "").Substring(0, decpla + 1 - place) + match;      //targetもstring化し、
                } else {
                    bunnkai = reg.Replace(target.ToString(), "").Substring(0, decpla + 1 - place) + match;
                }
            }


            //sc.WriteLine(number);
            //sc.ReadKey();
            return double.Parse(bunnkai);
            
        }


        public static Func<double, int, double> func = (tar, pla) => {

            return round(tar, pla);
            
        };

    }
}
