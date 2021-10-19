using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace savePic
{
    public partial class Form1 : Form
    {
       
        string image_url = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image_url = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(image_url);
            }
        }
        public static Image BianaryToImage(object binarydata)
        {
            if (binarydata == null)
            {
                return null;
            }
            else
            {
                var data = (byte[])binarydata;
                MemoryStream memoryStream = new MemoryStream(data);
                return Image.FromStream(memoryStream);
            }
        }
        public static byte[] imageToBinary(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                fileStream.Close();
                return buffer;   
        }

        private void button2_Click(object sender, EventArgs e)
        {

           
            string cmd_str = "insert into Picture(p_code,p_image)values('" + textBox1.Text + "',@pic)";
            DBclass dbclass = new DBclass(cmd_str);
            //check image size
            /*if (imageToBinary(image_url).Length>1000)
            {
                MessageBox.Show("image is so large ");
            }
            else
            {
                dbclass.Param = imageToBinary(image_url);
                string error = dbclass.execute_sql();
                if (error == "")
                {
                    MessageBox.Show("saved");
                    empty_field();

                }
                else
                {
                    MessageBox.Show("error" + error);
                }

            }*/

            dbclass.Param = imageToBinary(image_url);
            string error = dbclass.execute_sql();
            if (error == "")
            {
                MessageBox.Show("saved");
                empty_field();

            }
            else
            {
                MessageBox.Show("error" + error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cmd_str = "select * from Picture where p_code='" + textBox1.Text + "'";
            DataTable dt = new DataTable();
            DBclass dbclass = new DBclass(cmd_str);
            string error = dbclass.error_msg;
            dt = dbclass.search_info();
            
            if(error == "")
            {
                if (dt.Rows.Count > 0)
                {
                    pictureBox1.Image = BianaryToImage(dt.Rows[0]["p_image"]);

                }
                else
                {
                    MessageBox.Show("no record matched!");
                }
            }
            else
            {
                MessageBox.Show("error in searching process" +error);
                empty_field();
            }
          
            
        }
        public void empty_field()
        {
            textBox1.Text = "";
            pictureBox1.Image = null;
        }

    }
    
   
    }
   

