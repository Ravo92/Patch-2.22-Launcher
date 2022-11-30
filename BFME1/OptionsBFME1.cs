﻿using PatchLauncher.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatchLauncher
{
    public partial class OptionsBFME1 : Form
    {
        bool FlagEAX = Properties.Settings.Default.EAXSupport;
        readonly bool FlagEAXFileExists = File.Exists(ConstStrings.GameInstallPath() + @"\dsound.dll");
        bool FlagThemeMusic = Properties.Settings.Default.PlayBackgroundMusic;
        bool FlagWindowed = Properties.Settings.Default.StartGameWindowed;
        bool FlagBrutalAI = Properties.Settings.Default.UseBrutalAI;

        readonly SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
        public OptionsBFME1()
        {
            InitializeComponent();

            #region Styles
            //Main Form style behaviour

            BackgroundImage = Image.FromFile(@"Images\bgMap.png");

            // Button-Styles
            BtnDefault.FlatAppearance.BorderSize = 0;
            BtnDefault.FlatStyle = FlatStyle.Flat;
            BtnDefault.BackColor = Color.Transparent;
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnDefault.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);

            BtnApply.FlatAppearance.BorderSize = 0;
            BtnApply.FlatStyle = FlatStyle.Flat;
            BtnApply.BackColor = Color.Transparent;
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnApply.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);

            BtnCancel.FlatAppearance.BorderSize = 0;
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.BackColor = Color.Transparent;
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnCancel.Font = ConstStrings.UseFont("Albertus Nova", 14);
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);

            //Label-Styles
            LblTheme.Text = "Play theme music in launcher at start";
            LblTheme.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblTheme.ForeColor = Color.FromArgb(192, 145, 69);
            LblTheme.BackColor = Color.Transparent;

            LblEAX.Text = "Activate support for the EAX-Sound-System";
            LblEAX.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblEAX.ForeColor = Color.FromArgb(192, 145, 69);
            LblEAX.BackColor = Color.Transparent;

            LblLauncherSettings.Text = "Launcher-specific Settings";
            LblLauncherSettings.Font = ConstStrings.UseFont("SachaWynterTight", 16);
            LblLauncherSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblLauncherSettings.BackColor = Color.Transparent;

            LblGameSettings.Text = "Game-specific Settings";
            LblGameSettings.Font = ConstStrings.UseFont("SachaWynterTight", 16);
            LblGameSettings.ForeColor = Color.FromArgb(192, 145, 69);
            LblGameSettings.BackColor = Color.Transparent;

            LblOptions.Text = "Settings";
            LblOptions.Font = ConstStrings.UseFont("SachaWynterTight", 20);
            LblOptions.ForeColor = Color.FromArgb(192, 145, 69);
            LblOptions.BackColor = Color.Black;

            LblVersion.Text = "Patch 2.22v.3.0";
            LblVersion.Font = ConstStrings.UseFont("Albertus Nova", 11);
            LblVersion.ForeColor = Color.FromArgb(136, 82, 46);
            LblVersion.BackColor = Color.Transparent;

            LblWindowed.Text = "Launch the game in windowed mode";
            LblWindowed.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblWindowed.ForeColor = Color.FromArgb(192, 145, 69);
            LblWindowed.BackColor = Color.Transparent;

            LblBrutalAI.Text = "Use the experimental brutal AI";
            LblBrutalAI.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblBrutalAI.ForeColor = Color.FromArgb(192, 145, 69);
            LblBrutalAI.BackColor = Color.Transparent;

            LblWarningAI.Text = "";
            LblWarningAI.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblWarningAI.ForeColor = Color.Red;
            LblWarningAI.BackColor = Color.Transparent;

            if (Properties.Settings.Default.IsGameInstalled == false)
            {
                ChkBrutalAI.Enabled = false;
                ChkEAX.Enabled = false;
                ChkAniTextureFiltering.Enabled = false;

                LblWarningAI.Text = "Some Settings are Disabled until the game is installed.";
                LblWarningAI.Show();
            }
            else
            {
                ChkBrutalAI.Enabled = true;
                ChkEAX.Enabled = true;
                ChkAniTextureFiltering.Enabled = true;

                LblWarningAI.Hide();
            }

            if (FlagBrutalAI && Properties.Settings.Default.IsGameInstalled)
            {
                LblWarningAI.Text = "WARNING: Brutal AI is activated. \n You may not be able to play online";
                LblWarningAI.Show();
            }
            else if (Properties.Settings.Default.IsGameInstalled)
            {
                LblWarningAI.Hide();
            }

            LblAniTextureFiltering.Text = "Anisotropic  Texture  Filtering";
            LblAniTextureFiltering.Font = ConstStrings.UseFont("Albertus Nova", 16);
            LblAniTextureFiltering.ForeColor = Color.FromArgb(192, 145, 69);
            LblAniTextureFiltering.BackColor = Color.Transparent;

            //Checkbox-Styles
            ChkAniTextureFiltering.FlatAppearance.BorderSize = 0;
            ChkAniTextureFiltering.FlatStyle = FlatStyle.Flat;
            ChkAniTextureFiltering.BackColor = Color.Transparent;
            ChkAniTextureFiltering.ForeColor = Color.FromArgb(192, 145, 69);

            ChkTheme.FlatAppearance.BorderSize = 0;
            ChkTheme.FlatStyle = FlatStyle.Flat;
            ChkTheme.BackColor = Color.Transparent;
            ChkTheme.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");

            ChkEAX.FlatAppearance.BorderSize = 0;
            ChkEAX.FlatStyle = FlatStyle.Flat;
            ChkEAX.BackColor = Color.Transparent;
            ChkEAX.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagEAXFileExists)
                FlagEAX = true;
            else
                FlagEAX = false;

            if (FlagEAXFileExists && FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");

            ChkWindowed.FlatAppearance.BorderSize = 0;
            ChkWindowed.FlatStyle = FlatStyle.Flat;
            ChkWindowed.BackColor = Color.Transparent;
            ChkWindowed.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkWindowed.Image = Image.FromFile("Images\\chkUnselected.png");

            ChkBrutalAI.FlatAppearance.BorderSize = 0;
            ChkBrutalAI.FlatStyle = FlatStyle.Flat;
            ChkBrutalAI.BackColor = Color.Transparent;
            ChkBrutalAI.ForeColor = Color.FromArgb(192, 145, 69);

            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkBrutalAI.Image = Image.FromFile("Images\\chkUnselected.png");

            #endregion
        }

        #region Buttons and checkboxes for launcher specific settings
        private void BtnDefault_MouseLeave(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnDefault_MouseEnter(object sender, EventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnDefault.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnDefault_MouseDown(object sender, MouseEventArgs e)
        {
            BtnDefault.Image = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnDefault.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }
        private void BtnApply_MouseLeave(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnApply_MouseEnter(object sender, EventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnApply.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnApply_MouseDown(object sender, MouseEventArgs e)
        {
            BtnApply.Image = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnApply.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }
        private void BtnCancel_MouseLeave(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_NEUTR;
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
        }

        private void BtnCancel_MouseEnter(object sender, EventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_HOVER;
            BtnCancel.ForeColor = Color.FromArgb(100, 53, 5);
            Task.Run(() => BFME1.PlaySoundHover());
        }

        private void BtnCancel_MouseDown(object sender, MouseEventArgs e)
        {
            BtnCancel.Image = ConstStrings.C_BUTTONIMAGE_CLICK;
            BtnCancel.ForeColor = Color.FromArgb(192, 145, 69);
            Task.Run(() => BFME1.PlaySoundClick());
        }

        private void ChkTheme_MouseEnter(object sender, EventArgs e)
        {
            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkTheme_MouseLeave(object sender, EventArgs e)
        {
            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void ChkTheme_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }
        private void ChkTheme_Click(object sender, EventArgs e)
        {
            if (FlagThemeMusic)
            {
                ChkTheme.Image = Image.FromFile("Images\\chkUnselectedHover.png");
                FlagThemeMusic = false;
            }
            else
            {
                ChkTheme.Image = Image.FromFile("Images\\chkSelectedHover.png");
                FlagThemeMusic = true;
            }
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PlayBackgroundMusic = FlagThemeMusic;
            Properties.Settings.Default.EAXSupport = FlagEAX;
            Properties.Settings.Default.StartGameWindowed = FlagWindowed;
            Properties.Settings.Default.UseBrutalAI = FlagBrutalAI;
            Properties.Settings.Default.Save();

            if (FlagThemeMusic)
            {
                SoundPlayer _theme = new(Properties.Settings.Default.BackgroundMusicFile);
                _theme.Play();
            }
            else
            {
                _theme.Stop();
                _theme.Dispose();
            }

            if (!FlagEAXFileExists && FlagEAX == true)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Copy(Path.Combine("Tools", file), Path.Combine(ConstStrings.GameInstallPath(), file), true);
                }
            }

            if (FlagEAXFileExists && FlagEAX == false)
            {
                List<string> _EAXFiles = new() { "dsoal-aldrv.dll", "dsound.dll", "dsound.ini", };

                foreach (var file in _EAXFiles)
                {
                    File.Delete(Path.Combine(ConstStrings.GameInstallPath(), file));
                }
            }

            if (FlagBrutalAI && ConstStrings.GameInstallPath() != null)
                File.Copy(Path.Combine("Tools", "_patch222LibrariesBrutalAI.big"), Path.Combine(ConstStrings.GameInstallPath(), "_patch222LibrariesBrutalAI.big"), true);
            else if (ConstStrings.GameInstallPath() != null && File.Exists(Path.Combine(ConstStrings.GameInstallPath(), "_patch222LibrariesBrutalAI.big")))
                File.Delete(Path.Combine(ConstStrings.GameInstallPath(), "_patch222LibrariesBrutalAI.big"));

            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PlayBackgroundMusic && FlagThemeMusic)
                ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkTheme.Image = Image.FromFile("Images\\chkUnselected.png");

            if (Properties.Settings.Default.EAXSupport)
                ChkEAX.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");

            if (Properties.Settings.Default.StartGameWindowed)
                ChkWindowed.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkWindowed.Image = Image.FromFile("Images\\chkUnselected.png");

            if (Properties.Settings.Default.UseBrutalAI)
                ChkBrutalAI.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkBrutalAI.Image = Image.FromFile("Images\\chkUnselected.png");

            Close();
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            FlagThemeMusic = true;
            FlagEAX = false;
            FlagWindowed = false;
            FlagBrutalAI = false;
            ChkTheme.Image = Image.FromFile("Images\\chkSelected.png");
            ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");
            ChkWindowed.Image = Image.FromFile("Images\\chkUnselected.png");
            ChkBrutalAI.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void OptionsBFME1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WindowMover.ReleaseCapture();
                _ = WindowMover.SendMessage(Handle, WindowMover.WM_NCLBUTTONDOWN, WindowMover.HT_CAPTION, 0);
            }
        }

        private void ChkEAX_Click(object sender, EventArgs e)
        {
            if (FlagEAX == true)
            {
                ChkEAX.Image = Image.FromFile("Images\\chkUnselectedHover.png");
                FlagEAX = false;
            }
            else
            {
                ChkEAX.Image = Image.FromFile("Images\\chkSelectedHover.png");
                FlagEAX = true;
            }
        }

        private void ChkEAX_MouseEnter(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkEAX_MouseLeave(object sender, EventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void ChkEAX_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagEAX)
                ChkEAX.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkEAX.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkWindowed_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagWindowed == true)
            {
                ChkWindowed.Image = Image.FromFile("Images\\chkUnselectedHover.png");
                FlagWindowed = false;
            }
            else
            {
                ChkWindowed.Image = Image.FromFile("Images\\chkSelectedHover.png");
                FlagWindowed = true;
            }
        }

        private void ChkWindowed_MouseEnter(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkWindowed.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkWindowed_MouseLeave(object sender, EventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkWindowed.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void ChkWindowed_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagWindowed)
                ChkWindowed.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkWindowed.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkBrutalAI_MouseClick(object sender, MouseEventArgs e)
        {
            if (FlagBrutalAI == true)
            {
                ChkBrutalAI.Image = Image.FromFile("Images\\chkUnselectedHover.png");
                LblWarningAI.Hide();
                FlagBrutalAI = false;
            }
            else
            {
                ChkBrutalAI.Image = Image.FromFile("Images\\chkSelectedHover.png");
                LblWarningAI.Show();
                FlagBrutalAI = true;
            }
        }

        private void ChkBrutalAI_MouseEnter(object sender, EventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkBrutalAI.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        private void ChkBrutalAI_MouseLeave(object sender, EventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile("Images\\chkSelected.png");
            else
                ChkBrutalAI.Image = Image.FromFile("Images\\chkUnselected.png");
        }

        private void ChkBrutalAI_MouseDown(object sender, MouseEventArgs e)
        {
            if (FlagBrutalAI)
                ChkBrutalAI.Image = Image.FromFile("Images\\chkSelectedHover.png");
            else
                ChkBrutalAI.Image = Image.FromFile("Images\\chkUnselectedHover.png");
        }

        #endregion

        #region Checkboxes for game specific settings

        private void ChkAniTextureFiltering_Click(object sender, EventArgs e)
        {

        }

        private void ChkAniTextureFiltering_MouseEnter(object sender, EventArgs e)
        {

        }

        private void ChkAniTextureFiltering_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ChkAniTextureFiltering_MouseLeave(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
