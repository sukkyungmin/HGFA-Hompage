using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Model
{
    public class WeatherModel
    {
        public Groupping Times = new Groupping(); // 현재시간
        public Groupping Pop = new Groupping(); // 강수확률
        public Groupping Pty = new Groupping(); // 강수형태
        public Groupping Reh = new Groupping(); // 습도
        public Groupping Sky = new Groupping(); // 하늘상태
        public Groupping T3h = new Groupping(); // 3시간 기온
        public Groupping Uuu = new Groupping(); // 풍속(동서성 분)
        public Groupping Vec = new Groupping(); // 풍향
        public Groupping Vvv = new Groupping(); // 풍속(남북성 분)
        public Groupping Wsd = new Groupping(); // 풍속
        public Groupping Weatherimage = new Groupping();
    }

    public class Groupping
    {
        public string Wid { get; set; }
        public string Wvalue { get; set; }
    }
}
