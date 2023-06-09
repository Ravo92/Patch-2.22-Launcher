﻿using Helper;
using PatchLauncher.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    partial class CreditsForm : Form
    {
        public CreditsForm()
        {
            switch (Settings.Default.Language)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                    break;
                default:
                    break;
            }

            InitializeComponent();

            KeyPreview = true;

            Uri _Wv2CreditsUri = new("file:///" + Path.Combine(Application.StartupPath, ConstStrings.C_HTMLFOLDER_NAME) + "/credits.html");
            Wv2Credits.Source = _Wv2CreditsUri;

            BackColor = Color.FromArgb(18, 18, 18);

            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.BackColor = Color.FromArgb(18, 18, 18);
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnClose.Font = FontHelper.GetFont(0, 16); ;
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnOptions_Click(object sender, EventArgs e)
        {
            Wv2Credits.Dispose();
            Close();
        }

        private void BtnClose_MouseLeave(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnClose_MouseEnter(object sender, EventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnClose.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => SoundPlayerHelper.PlaySoundHover());
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            BtnClose.BackgroundImage = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnClose.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => SoundPlayerHelper.PlaySoundClick());
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}