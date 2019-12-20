using System;
using Marvel.models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;

namespace MarvelWindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadMarvelData();
        }
        //Hosted web API REST Service base url
        string Baseurl = "http://localhost:8005/Help";
        // GET: StudentGroups
        private async void LoadMarvelData()
        {
            List<Character> StudentInfo = new List<Character>();

            using (var client = new System.Net.Http.HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                HttpResponseMessage Res = await client.GetAsync("api/Values/GetMarvelCharacters");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Emp
                    StudentInfo = JsonConvert.DeserializeObject<List<Character>>(EmpResponse);

                }
            }
            //returning the employee list to view
            gvMarvel.DataSource = StudentInfo;
            gvMarvel.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
