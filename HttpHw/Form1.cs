using System.Net.Http;
namespace HttpHw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
 

                HttpResponseMessage response = await client.GetAsync("https://www.gutenberg.org/cache/epub/1524/pg1524-images.html");

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    List<string> dataList = new List<string>();

                    
                    bool isInsideTargetScene = false;

                    
                    foreach (var data in responseData.Split('\n'))
                    {
                        
                        string trimmedData = data.Trim();

                        
                        if (trimmedData.Contains("SCENE. Elsinore."))
                        {
                            isInsideTargetScene = true;
                        }

                        
                        if (isInsideTargetScene)
                        {
                            if (data.Contains("<p>") || data.Contains("<h2>") || data.Contains("<h3>") || data.Contains("<br>") || data.Contains("</br>") || data.Contains("<a>") || data.Contains("</a>") || data.Contains("<b>") || data.Contains("</b>"))
                            {
                                dataList.Add(data.Replace("<p>", "").Replace("</p>", "").Replace("<h2>", "").Replace("</h2>", "").Replace("<h3>", "").Replace("</h3>", "").Replace("<br>", "").Replace("</br>", "").Replace("<a>", "").Replace("</a>", "").Replace("<b>", "").Replace("</b>", ""));
                            }
                            
                        }

                        
                        if (trimmedData.Contains("[Exeunt, bearing off the bodies, after which a peal of ordnance is shot off.]"))
                        {
                            isInsideTargetScene = false;
                            break; 
                        }
                    }
                    
                    richTextBox1.Text = string.Join(Environment.NewLine, dataList);
                }
                else
                {
                    MessageBox.Show("Error: " + response.StatusCode);
                }
            }
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}