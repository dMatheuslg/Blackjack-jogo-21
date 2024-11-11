using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJackGame
{
    public partial class Form1 : Form
    {
        private int scorePlayer1 = 0;
        private int scorePlayer2 = 0;
        private bool player1Finished = false;
        private bool player2Finished = false;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            StartNewGame();
            panelinicio.Visible = true;
        }

        private void StartNewGame()
        {
          
            scorePlayer1 = 0;
            scorePlayer2 = 0;
            player1Finished = false;
            player2Finished = false;

      
            labelScorePlayer1.Text = "0";
            labelScorePlayer2.Text = "0";

            pictureBoxPlayer1.Image = null;
            pictureBoxPlayer2.Image = null;


            panelVencedor.Visible = false;

            btnPedirCartaPlayer1.Enabled = true;
            btnPedirCartaPlayer2.Enabled = true;
            btnEncerrarPlayer1.Enabled = true;
            btnEncerrarPlayer2.Enabled = true;
        }

        private void btnPedirCartaPlayer1_Click(object sender, EventArgs e)
        {
            int cardValue = DrawCard();
            UpdateScore(1, cardValue);
            pictureBoxPlayer1.Image = GetCardImage(cardValue);

            CheckGameStatus();
        }

        private void btnPedirCartaPlayer2_Click(object sender, EventArgs e)
        {
            int cardValue = DrawCard();
            UpdateScore(2, cardValue);
            pictureBoxPlayer2.Image = GetCardImage(cardValue);

            CheckGameStatus();
        }

        private void btnEncerrarPlayer1_Click(object sender, EventArgs e)
        {
            player1Finished = true;
            btnPedirCartaPlayer1.Enabled = false;
            btnEncerrarPlayer1.Enabled = false;

            CheckGameStatus();
        }

        private void btnEncerrarPlayer2_Click(object sender, EventArgs e)
        {
            player2Finished = true;
            btnPedirCartaPlayer2.Enabled = false;
            btnEncerrarPlayer2.Enabled = false;

            CheckGameStatus();
        }
        private int DrawCard()
        {
            
            int cardValue = random.Next(1, 13);
            return cardValue;
        }

        private void UpdateScore(int player, int cardValue)
        {
            
            if (cardValue >= 10) cardValue = 10; 
            if (cardValue == 1) cardValue = (player == 1 && scorePlayer1 <= 10) || (player == 2 && scorePlayer2 <= 10) ? 11 : 1;

            if (player == 1)
            {
                scorePlayer1 += cardValue;
                labelScorePlayer1.Text = scorePlayer1.ToString();
            }
            else
            {
                scorePlayer2 += cardValue;
                labelScorePlayer2.Text = scorePlayer2.ToString();
            }
        }

        private void CheckGameStatus()
        {
            
            if (scorePlayer1 >= 21 || scorePlayer2 >= 21 || (player1Finished && player2Finished))
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            string winnerMessage;

            if (scorePlayer1 > 21 && scorePlayer2 > 21)
            {
                winnerMessage = "Ambos perderam!";
            }
            else if (scorePlayer1 == 21 || (scorePlayer1 < 21 && scorePlayer2 > 21) ||
                     (scorePlayer1 <= 21 && scorePlayer1 > scorePlayer2))
            {
                winnerMessage = "Jogador 1 venceu!";
            }
            else if (scorePlayer2 == 21 || (scorePlayer2 < 21 && scorePlayer1 > 21) ||
                     (scorePlayer2 <= 21 && scorePlayer2 > scorePlayer1))
            {
                winnerMessage = "Jogador 2 venceu!";
            }
            else
            {
                winnerMessage = "Empate!";
            }

            labelVencedor1.Text = winnerMessage;
            panelVencedor.Visible = true;

            btnPedirCartaPlayer1.Enabled = false;
            btnPedirCartaPlayer2.Enabled = false;
            btnEncerrarPlayer1.Enabled = false;
            btnEncerrarPlayer2.Enabled = false;
        }

        private Image GetCardImage(int cardValue)
        {
            string cardName = cardValue.ToString();
            return (Image)Properties.Resources.ResourceManager.GetObject(cardName);
        }

        private void btnreiniciar_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelinicio.Visible = false;
        }
    }
}
