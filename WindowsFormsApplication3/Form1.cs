using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;


namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        private Encoding converter;
        private HMACSHA256 hasher;
        private SHA1 sha1compute;
       

        public Form1()
        {
            InitializeComponent();
            this.converter = Encoding.Default;
            this.comboBox1.SelectedIndex = 0;
            this.radioButton1.Checked = true;


        }
        private HashAlgorithm Set(int h)
        {
            if (h == 0)
            {
                return new MD5CryptoServiceProvider();
            }
            else if (h == 1)
            {
                return new SHA1CryptoServiceProvider();
            }
            else if (h == 2)
            {
                return new SHA256CryptoServiceProvider();
            }
            else
            {
                return null;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {


            int m = this.comboBox1.SelectedIndex;
            HashAlgorithm hash = Set(m);

            this.sha1compute = new SHA1CryptoServiceProvider();
            this.hasher = new HMACSHA256();
            




            byte[] x = converter.GetBytes(this.textBox1.Text);
            byte[] result = hash.ComputeHash(x);
            this.textBox2.Text = BitConverter.ToString(result);
            

            
            if (radioButton2.Checked)
            {
                this.textBox2.Text = Convert.ToBase64String(hash.ComputeHash(result));

            }
            if (radioButton1.Checked)
            {
                string s = textBox1.Text;
                byte[] bytes = Encoding.ASCII.GetBytes(s);
                byte[] resultBytes = this.hasher.ComputeHash(bytes);
                byte[] sha1Bytes = this.sha1compute.ComputeHash(resultBytes);
                textBox2.Text = BitConverter.ToString(sha1Bytes);

               


            }
            if(radioButton3.Checked)
            {
                Class1 str = new Class1();
                textBox2.Text = str.Encode(textBox2.Text);

            }

            hash.Dispose();

            textBox3.Text = textBox2.Text.Length.ToString();



        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            this.textBox1.Text = "";
            try
            {

                Stream myStream;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                this.Activate();
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog1.InitialDirectory = path;
                openFileDialog1.Title = "Chose a file:";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        string pathtofile = Path.GetFullPath(openFileDialog1.FileName);
                        myStream.Close();
                      
                        
                        int m = this.comboBox1.SelectedIndex;
                        HashAlgorithm hash = Set(m);

                        if (hash != null)
                        {
                            hash.Initialize();

                            byte[] x = File.ReadAllBytes(pathtofile);
                            byte[] result = hash.ComputeHash(x);
                            this.textBox2.Text = BitConverter.ToString(result).Replace("-", "");
                            this.textBox3.Text = Convert.ToString(textBox2.Text.Length);

                            hash.Dispose();
                        }
                        myStream.Close();
                                            
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error! "+ Environment.NewLine +"Error Message: " + ex.Message.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            textBox1.Text = textBox2.Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Please generate hash first!");
            }
            else
            {
                try
                {

                    Stream myStream;
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                    this.Activate();
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    saveFileDialog1.InitialDirectory = path;
                    saveFileDialog1.Title = "Chose where the file will be saved:";
                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog1.DefaultExt = ".txt";
                    saveFileDialog1.FilterIndex = 1;
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.FileName = "hashfile";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if ((myStream = saveFileDialog1.OpenFile()) != null)
                        {
                            StreamWriter s = new StreamWriter(myStream, Encoding.GetEncoding(converter.CodePage));
                            s.WriteLine("----------------------------------------------------------------------------------------");
                            s.WriteLine("Encoding: " + converter.EncodingName.ToString());
                            s.WriteLine("Hash Algorithm: " + this.comboBox1.Text);
                            string osbitword = (Environment.Is64BitOperatingSystem) ? "x64" : "x86";
                            s.WriteLine("Generatet on OS: " + Environment.OSVersion + " " + osbitword);
                            s.WriteLine("----------------------------------------------------------------------------------------");
                            s.WriteLine("Text: " + this.textBox1.Text);
                            s.WriteLine("----------------------------------------------------------------------------------------");
                            s.WriteLine("Hash Length: " + this.textBox2.Text.Replace("-","").Length);
                            s.WriteLine("----------------------------------------------------------------------------------------");
                           s.WriteLine("Hash: " + Environment.NewLine + this.textBox2.Text);
                           s.WriteLine("----------------------------------------------------------------------------------------");
                            s.Close();

                            MessageBox.Show("File has been successfully saved.");
                        }

                        myStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There are an error! " + Environment.NewLine + "Error Message: " + ex.Message.ToString());
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void д_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
