using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;

namespace WeatherApp
{


    public partial class Form1 : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public class WeatherLocation
        {
            public string name { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
            public string country { get; set; }
        }
        public class WeatherResponse
        {
            
            public List<Weather> weather { get; set; }
            public IDictionary<string, float> main { get; set; }
            public IDictionary<string, float> wind { get; set; }
        }

        public class ForecastResponse
        {

            public List<Forecast> list { get; set; }

        }

        public class Forecast
        {
            public double dt { get; set; }
            public IDictionary<string, float> main { get; set; }
            public List<Weather> weather { get; set; }
            public IDictionary<string, float> wind { get; set; }
        }

        public class Weather
        {
            public string main { get; set; }
            public string description { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnToday.Height;
            pnlNav.Top = btnToday.Top;
            pnlNav.Left = btnToday.Left;
            btnToday.BackColor = Color.FromArgb(46, 51, 73);

            RunLocationAsync("Lethbridge, CA");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RunLocationAsync(string loc)
        {
            HttpClient client = new HttpClient();
            string cityName = loc.Substring(0, loc.IndexOf(','));
            string countryCode = loc[(loc.IndexOf(',') + 1)..];
            string apiCode = "ea6ae377f714bf4420b379e5c0331429";
            string uriParams = "?q=" + cityName + ","+countryCode+"&limit=1&appid=" + apiCode;
            client.BaseAddress = new Uri("http://api.openweathermap.org/geo/1.0/direct");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uriParams).Result; // BLOCKING CALL *******************

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<WeatherLocation>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                if(dataObjects.Count() == 0)
                {
                    this.locFound.Text = "Not Found";
                    this.locFound.ForeColor = Color.Red;
                }
                else
                {
                    this.locFound.Text = "Found Successfully!";
                    this.locFound.ForeColor = Color.Chartreuse;
                }
                foreach (var d in dataObjects)
                {
                    RunWeatherWebclient(d.lat, d.lon);
                }
            }
            else
            {
                
            }
            client.Dispose();

        }

        private void RunWeatherWebclient(string lat, string lon)
        {

            string apiCode = "a527eb3eaeb949acc3b09adf3e958d59";
            string uriParams = "?lat=" + lat + "&lon=" + lon + "&appid=" + apiCode;
            string baseUri = "https://api.openweathermap.org/data/2.5/weather";
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(baseUri+uriParams);
                var obj = JsonConvert.DeserializeObject<WeatherResponse>(json);
                this.currentTemp.Text = (obj.main["temp"]-273.15).ToString("N0")+ "°C";
                this.currentHumid.Text = (obj.main["humidity"]).ToString() + "%";
                this.currentWind.Text = (obj.wind["speed"]*3.6).ToString("N0") + "km/h";
                foreach(var wea in obj.weather)
                {
                    this.currentCondition.Text = wea.main;
                    this.currentConditionImg.Image = assignConditionImg(wea.main);
                    this.sideConditionImg.Image = assignConditionImg(wea.main);
                }
            }
            baseUri = "https://api.openweathermap.org/data/2.5/forecast";
            uriParams = "?lat=" + lat + "&lon=" + lon + "&cnt=8&appid=" + apiCode;
            using (WebClient web = new WebClient())
            {
                var json = web.DownloadString(baseUri + uriParams);
                var obj = JsonConvert.DeserializeObject<ForecastResponse>(json);           
                IEnumerable<Panel> ctlPanels = hourForecast.Controls.OfType<Panel>();
                foreach ( var panel in ctlPanels)
                {
                    IEnumerable<Label> labels = panel.Controls.OfType<Label>();
                    foreach (var ctl in labels)
                    {
                        foreach (var fore in obj.list.Select((value, i) => (value, i)))
                        {
                            if (ctl.Name.StartsWith("hour") && ctl.Name.EndsWith(fore.i.ToString()))
                            {
                                ctl.Text = UnixTimeStampToHour(fore.value.dt);
                            }
                            else if (ctl.Name.StartsWith("temp") && ctl.Name.EndsWith(fore.i.ToString()))
                            {
                                ctl.Text = (fore.value.main["temp"] - 273.15).ToString("N0") + "°C";
                            }
                            else if (ctl.Name.StartsWith("humid") && ctl.Name.EndsWith(fore.i.ToString()))
                            {
                                ctl.Text = fore.value.main["humidity"].ToString() + "%";
                            }
                            else if (ctl.Name.StartsWith("wind") && ctl.Name.EndsWith(fore.i.ToString()))
                            {
                                ctl.Text = (fore.value.wind["speed"] * 3.6).ToString("N0") + "km/h";
                            }
                            foreach (var wea in fore.value.weather) {
                                panel.Controls.OfType<PictureBox>().First().Image = assignConditionImg(wea.main);
                            }
                        }
                        
                    }
                }
            }
           
        }

        public static string UnixTimeStampToHour(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime.ToString("h tt");
        }

        private Bitmap assignConditionImg(string cdn)
        {

            if (cdn == "Rain")
            {
                return WeatherApp.Properties.Resources._1530364_weather_rain_shower_storm_icon;
            }
            else if (cdn == "Clouds")
            {
                return WeatherApp.Properties.Resources._2995000_cloud_weather_cloudy_rain_sun_icon;
            }
            else if (cdn == "Drizzle")
            {
                return WeatherApp.Properties.Resources._1530365_weather_cloud_drizzel_rain_icon;
            }
            else if (cdn == "Thunderstorm")
            {
                return WeatherApp.Properties.Resources._1530363_weather_clouds_night_storm_icon;
            }
            else if (cdn == "Snow")
            {
                return WeatherApp.Properties.Resources._1530371_weather_clouds_snow_winter_icon;
            }
            else if (cdn == "Tornado")
            {
                return WeatherApp.Properties.Resources._1530366_weather_hurricane_storm_tornado_icon;
            }
            else if ((cdn == "Mist") || (cdn == "Haze") || (cdn == "Fog"))
            {
                return WeatherApp.Properties.Resources._1530368_weather_clouds_cloudy_fog_foggy_icon;
            }
            else if ((cdn == "Smoke") || (cdn == "Dust") || (cdn == "Sand") || (cdn == "Ash") || (cdn == "Squall"))
            {
                return WeatherApp.Properties.Resources._1530374_weather_day_sand_sandstorm_sun_icon;
            }

            return WeatherApp.Properties.Resources._1530392_weather_sun_sunny_temperature_icon;

        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnToday.Height;
            pnlNav.Top = btnToday.Top;
            pnlNav.Left = btnToday.Left;
            btnToday.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnWeek_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnWeek.Height;
            pnlNav.Top = btnWeek.Top;
            btnWeek.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnHist.Height;
            pnlNav.Top = btnHist.Top;
            btnHist.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void btnToday_Leave(object sender, EventArgs e)
        {
            btnToday.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnWeek_Leave(object sender, EventArgs e)
        {
            btnWeek.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnHist_Leave(object sender, EventArgs e)
        {
            btnHist.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RunLocationAsync(locBox.Text);
        }
    }
}
