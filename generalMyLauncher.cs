using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace genericMyLauncher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
    public partial class MainForm : Form
    {
        // --------------------------------
        // variable and constant declaration
        // --------------------------------
        public const string c_settingListXMLFilePath = @"./settingList.xml";
        public string iconFilePath;
        public string targetSoftwareName;
        public string targetSoftwarePath;
        public List<ItemSet> argumentList;
        public const int c_titleBarHeight = 31;
        public const int c_getaHeight = 6;
        public const int c_getaWidth = 14;
        public string argumentsName;

        // --------------------------------
        // controller declaration
        // --------------------------------
        private Label lblArgumentsName;
        private Button btnLaunch;
        private Button btnCancel;
        private ComboBox cbxArgumentSelector;

        // --------------------------------
        // ItemSet class define
        // --------------------------------
        public class ItemSet
        {
            public string ItemLabel { get; set; }
            public string ItemValue { get; set; }

            public ItemSet(string label, string value)
            {
                this.ItemLabel = label;
                this.ItemValue = value;
            }
        }

        // --------------------------------
        // MainForm
        // --------------------------------
        public MainForm()
        {
            argumentList = new List<ItemSet>();

            if (!loadSettingXML(c_settingListXMLFilePath))
            {
                this.Close();
            }

            if (System.IO.File.Exists(iconFilePath))
            {
                Icon ic = new Icon(iconFilePath, 16, 16);
                this.Icon = ic;
            }

            this.Text = "generalMyLauncher";
            this.Height = 80 + c_getaHeight + c_titleBarHeight;
            this.Width = 480 + c_getaWidth;

            // set controll property
            lblArgumentsName = new Label();
            lblArgumentsName.Text = argumentsName;
            lblArgumentsName.Location = new Point(20, 8);
            this.Controls.Add(lblArgumentsName);

            cbxArgumentSelector = new ComboBox();
            cbxArgumentSelector.Location = new Point(20, 32);
            cbxArgumentSelector.Width = 160;
            this.Controls.Add(cbxArgumentSelector);

            cbxArgumentSelector.DataSource = argumentList;
            cbxArgumentSelector.DisplayMember = "ItemLabel";
            cbxArgumentSelector.ValueMember = "ItemValue";
            cbxArgumentSelector.SelectedIndex = 0;

            btnLaunch = new Button();
            btnLaunch.Text = targetSoftwareName + "起動";
            btnLaunch.Click += btnLaunch_Click;
            btnLaunch.Location = new Point(200, 20);
            btnLaunch.Height = 32;
            btnLaunch.Width = 100;
            this.Controls.Add(btnLaunch);

            btnCancel = new Button();
            btnCancel.Text = "キャンセル";
            btnCancel.Click += btnCancel_Click;
            btnCancel.Location = new Point(320, 20);
            btnCancel.Height = 32;
            btnCancel.Width = 100;
            this.Controls.Add(btnCancel);
        }

        // --------------------------------
        // load settingList.xml
        // --------------------------------
        public bool loadSettingXML(string settingXMLPath)
        {
            try
            {
                XDocument xDoc = XDocument.Load(settingXMLPath);

                XElement xelSettingElement = xDoc.Element("setting");
                iconFilePath = xelSettingElement.Element("icon").Value;

                XElement xelTargetSoftware = xelSettingElement.Element("targetSoftware");

                targetSoftwareName = xelTargetSoftware.Element("name").Value;
                targetSoftwarePath = xelTargetSoftware.Element("path").Value;

                XElement xelArguments = xelSettingElement.Element("arguments");
                argumentsName = xelArguments.Element("name").Value;

                foreach (XElement xelArg in xelArguments.Elements())
                {
                    if (xelArg.Name.ToString() != "name")
                    {
                        string argName = xelArg.Element("name").Value;
                        string argValue = xelArg.Element("value").Value;
                        argumentList.Add(new ItemSet(argName, argValue));
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("failed load setting." + "\r\n" + e.Message);
                return false;
            }
        }

        // .________________________________.
        // handlers
        // .________________________________.
        private void btnLaunch_Click(object sender, EventArgs e)
        {
            ItemSet item = (ItemSet)cbxArgumentSelector.SelectedItem;
            String argValue = item.ItemValue;

            Process.Start(targetSoftwarePath, argValue);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
