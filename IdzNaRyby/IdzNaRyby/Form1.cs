using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdzNaRyby
{
    public partial class Form1 : Form
    {

        Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textName.Text))
            {
                MessageBox.Show("Wpisz swoje imię", "Nie można jeszcze rozpocząć gry.");
                return;
            }
            game = new Game(textName.Text, new List<string> { "Janek", "Bartek" }, textProgress);
            buttonStart.Enabled = false;
            textName.Enabled = false;
            buttonAsk.Enabled = true;
            UpdateForm();
        }

        private void UpdateForm()
        {
            listHand.Items.Clear();
            foreach (String cardName in game.GetPlayerCardNames())
                listHand.Items.Add(cardName);
            textBooks.Text = game.DescribeBooks();
            textProgress.Text += game.DescribePlayerHands();
            textProgress.SelectionStart = textProgress.Text.Length;
            textProgress.ScrollToCaret();
        }

        private void buttonAsk_Click(object sender, EventArgs e)
        {
            textProgress.Text = "";
            if (listHand.SelectedIndex < 0)
            {
                MessageBox.Show("Wybierz kartę.");
                return;
            }
            if (game.PlayOneRound(listHand.SelectedIndex))
            {
                textProgress.Text += "Zwycięzcą jest... " + game.GetWinnerName() + Environment.NewLine;
                textBooks.Text = game.DescribeBooks();
                buttonAsk.Enabled = false;
            }
            else
                UpdateForm();
        }

        private void listHand_DoubleClick(object sender, EventArgs e)
        {
            buttonAsk_Click(sender, e);
        }
    }



}
