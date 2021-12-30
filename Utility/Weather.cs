using HangilFA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace HangilFA.Utility
{
    public class Weather
    {
        public WeatherModel Wmodel = new WeatherModel();
        public Weather()
        {

            //string Times = (DateTime.Now.Hour >= 2 && DateTime.Now.Hour < 5) ? "0200" :
            //               (DateTime.Now.Hour >= 5 && DateTime.Now.Hour < 8) ? "0500" :
            //               (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 11) ? "0800" :
            //               (DateTime.Now.Hour >= 11 && DateTime.Now.Hour < 14) ? "1100" :
            //               (DateTime.Now.Hour >= 14 && DateTime.Now.Hour < 17) ? "1400" :
            //               (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 20) ? "1700" :
            //               (DateTime.Now.Hour >= 20 && DateTime.Now.Hour < 23) ? "2000" : "2300";

            string Times = ((DateTime.Now.Hour - 1) < 10) ? (DateTime.Now.Minute < 30) ? "0" + (DateTime.Now.Hour - 1).ToString() + "00" : "0" + (DateTime.Now.Hour - 1).ToString() + "30"
                                                    : (DateTime.Now.Minute < 30) ? (DateTime.Now.Hour - 1).ToString() + "00" : (DateTime.Now.Hour - 1).ToString() + "30";

            string selectTimes = (DateTime.Now.Hour < 10) ? "0" + DateTime.Now.Hour.ToString() + "00" : DateTime.Now.Hour.ToString() + "00";

            Wmodel.Times.Wvalue = selectTimes;

            String strUrl = "http://apis.data.go.kr/1360000/VilageFcstInfoService/getUltraSrtFcst";
            //String strUrl = "http://newsky2.kma.go.kr/service/SecndSrtpdFrcstInfoService2/ForecastSpaceData";

            UriBuilder ub = new UriBuilder(strUrl)
            {
                //Query = "?ServiceKey=pyXblWAyn%2BIF%2BPoJ88%2FFsHp5bbnVholtuIUk090kXpMuL0owkmfbWNN98UwWLovUlcIGmUL134HktJAf2%2BJW9Q%3D%3D"
                //        // 날짜 안맞음 시간이 하루가 지나고 23시이면 그전 날짜로 계산되게 조건 추가할것.
                //        + "&base_date=" + DateTime.Now.ToString("yyyyMMdd")
                //        //+ "&base_date=20190714"
                //        + "&base_time=" + Times
                //        + "&nx=61&ny=120"
                //        + "&numOfRows=40"
                //        + "&pageNo=1"
                //        + "&_type=xml"
                Query = "?ServiceKey=pyXblWAyn%2BIF%2BPoJ88%2FFsHp5bbnVholtuIUk090kXpMuL0owkmfbWNN98UwWLovUlcIGmUL134HktJAf2%2BJW9Q%3D%3D"
                        // 날짜 안맞음 시간이 하루가 지나고 23시이면 그전 날짜로 계산되게 조건 추가할것.
                        + "&numOfRows=40"
                        + "&pageNo=1"
                        + "&base_date=" + DateTime.Now.ToString("yyyyMMdd")
                        //+ "&base_date=20190714"
                        + "&base_time=" + Times
                        + "&nx=61&ny=120"
            };

            //HttpWebRequest request;
            //request = HttpWebRequest.Create(ub.Uri) as HttpWebRequest;
            //request.BeginGetResponse(new AsyncCallback(GetResponse), request);
            try
            {
                String strRead;

                HttpWebRequest wr = HttpWebRequest.Create(ub.Uri) as HttpWebRequest;

                HttpWebResponse wp = (HttpWebResponse)wr.GetResponse();

                Stream stream = wp.GetResponseStream();

                using (StreamReader reader = new StreamReader(stream))
                {
                    strRead = reader.ReadToEnd();
                }

                XElement xmlMain = XElement.Parse(strRead);
                XElement xmlBody = xmlMain.Descendants("items").First();

                IEnumerable<XElement> childList = from el in xmlBody.Elements()
                                                  select el;
                foreach (XElement e in childList)
                {
                    //if (e.Element("category").Value == "POP")
                    //{
                    //    Wmodel.Pop.Wid = "강수확률";
                    //    Wmodel.Pop.Wvalue = string.Format("{0}%", e.Element("fcstValue").Value);
                    //}
                    if (e.Element("category").Value == "PTY" && e.Element("fcstTime").Value == selectTimes)
                    {
                        Wmodel.Pty.Wid = "강수형태";
                        Wmodel.Pty.Wvalue = (Convert.ToInt32(e.Element("fcstValue").Value) == 0) ? "없음/no" :
                                            (Convert.ToInt32(e.Element("fcstValue").Value) == 1) ? "비/rain" :
                                            (Convert.ToInt32(e.Element("fcstValue").Value) == 2) ? "비&눈/snow" :
                                            (Convert.ToInt32(e.Element("fcstValue").Value) == 3) ? "눈/snow" : "소나/rain";
                    }
                    if (e.Element("category").Value == "REH" && e.Element("fcstTime").Value == selectTimes)
                    {
                        Wmodel.Reh.Wid = "습도";
                        Wmodel.Reh.Wvalue = string.Format("{0}%", e.Element("fcstValue").Value);
                    }
                    if (e.Element("category").Value == "SKY" && e.Element("fcstTime").Value == selectTimes)
                    {
                        Wmodel.Sky.Wid = "하늘상태";
                        Wmodel.Sky.Wvalue = (Convert.ToInt32(e.Element("fcstValue").Value) == 1) ? (DateTime.Now.Hour > 17 || DateTime.Now.Hour < 7) ? "맑음/moon" : "맑음/sun" :
                                            //(Convert.ToInt32(e.Element("fcstValue").Value) == 2) ? (DateTime.Now.Hour > 19 || DateTime.Now.Hour < 7) ? "구름조금/mooncloud" : "구름조금/suncloud" :
                                            (Convert.ToInt32(e.Element("fcstValue").Value) == 3) ? (DateTime.Now.Hour > 17 || DateTime.Now.Hour < 7) ? "구름많음/mooncloud" : "구름많음/suncloud"
                                            : (DateTime.Now.Hour > 17 || DateTime.Now.Hour < 7) ? "구름많음/mooncloud" : "구름많음/suncloud";
                    }
                    if (e.Element("category").Value == "T1H" && e.Element("fcstTime").Value == selectTimes)
                    {
                        Wmodel.T3h.Wid = "기온";
                        Wmodel.T3h.Wvalue = string.Format("{0}º", e.Element("fcstValue").Value);
                    }
                }

                Wmodel.Weatherimage.Wvalue = (Wmodel.Pty.Wvalue == "없음/no") ? Wmodel.Sky.Wvalue : Wmodel.Pty.Wvalue;
            }
            catch (Exception ex)
            {
                Wmodel.Pop.Wvalue = ex.ToString();
            }

        }
    }
}
