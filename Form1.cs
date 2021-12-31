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
using System.Security.Principal;



namespace DaysGone_patch
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            // Getting installation folder from user
            folderBrowserDialog1.Description = "Browse for Installation folder of Days Gone";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assigning operators
                string installation_folder = folderBrowserDialog1.SelectedPath;
                if (File.Exists(installation_folder + @"\steamclient64.dll"))
                {



                    // Backing up starts

                    if (File.Exists(installation_folder + @"\steamclient64.dll.bak"))
                    {
                        MessageBox.Show("Already Patched");


                        this.Close();
                    }

                    else
                        MessageBox.Show("Backing Up");
                    File.Copy(installation_folder + @"\steamclient64.dll", installation_folder + @"\steamclient64.dll.bak");




                    //Patching Starts

                    BinaryWriter bw = new BinaryWriter(File.Open("C:\\Games\\Days Gone\\Engine\\Binaries\\ThirdParty\\Steamworks\\Steamv148\\Win64\\steamclient64.dll", FileMode.Open));
                    bw.BaseStream.Seek(0x0020C99, SeekOrigin.Begin);
                    bw.Write(0x6E);
                    bw.BaseStream.Seek(0x0020C9A, SeekOrigin.Begin);
                    bw.Write(0x73);
                    bw.Close();

                    MessageBox.Show("Patched Successfully!");
                }

                this.Close();
            }

        }

        public bool get_admin_rights()
        {
            bool isElevated = false;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return isElevated;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            bool adminrights = get_admin_rights();
            if (adminrights == false)
            {
                button1.Enabled = false;
                button1.Text = "Please Run this with Admin Permission";
            }
        }
    }
}