using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OthelloLogic;
using B19Ex01_LidorTevet_312465040_SaharHagbi_308145853;

namespace B19Ex05_LidorTevet_312465040_SaharHagbi_308145853
{
    public partial class GameForm : Form
    {
        private Controller m_Controller = new Controller();
        private char m_CurrentPlayerShape;
        private PictureBox[,] m_buttonMatrix;
        private string[,] m_PaintGameBoard;
        private int m_BoardSize;
        private int m_scorePlayerOne = 0, m_ScorePlayerTwo = 0;
        private bool m_isTheSecondPlayerIsComputer;

        public GameForm(int i_BoardSize)
        {
            getBoardSet(i_BoardSize);
        }

        private void getBoardSet(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;

            InitializeComponent();
            generateButtonMatrix(m_BoardSize);
            this.ClientSize = new Size((int)Utilities.eButtonSize.HEIGHT * m_BoardSize + 8,
                (int)Utilities.eButtonSize.WIDTH * m_BoardSize + 8);
        }

        internal void StartPlay(bool i_isTheSecondPlayerIsComputer)
        {
            m_isTheSecondPlayerIsComputer = i_isTheSecondPlayerIsComputer;
            setPlayers(i_isTheSecondPlayerIsComputer);
            initialzeGame();

            this.ShowDialog();
            this.Close();
        }

        private void generateButtonMatrix(int i_BoardSize)
        {
            m_buttonMatrix = new PictureBox[i_BoardSize, i_BoardSize];
            m_PaintGameBoard = m_Controller.m_StartGame(m_BoardSize);

            for (int y = 0; y < i_BoardSize; y++)
            {
                for (int x = 0; x < i_BoardSize; x++)
                {
                    m_buttonMatrix[y, x] = new PictureBox();
                    m_buttonMatrix[y, x].Location = new Point(y * 50 + 4, x * 50 + 4);
                    m_buttonMatrix[y, x].Width = 50;
                    m_buttonMatrix[y, x].Height = 50;
                    m_buttonMatrix[y, x].BackColor = Color.WhiteSmoke;
                    m_buttonMatrix[y, x].BorderStyle = BorderStyle.Fixed3D;
                    m_buttonMatrix[y, x].SizeMode = PictureBoxSizeMode.Zoom;
                    updateStateButton(y, x);
                    m_buttonMatrix[y, x].Tag = string.Format("{0}{1}", x, y);
                    m_buttonMatrix[y, x].Enabled = false;
                    m_buttonMatrix[y, x].Click += buttonMatrix_Click;
                    this.Controls.Add(m_buttonMatrix[y, x]);
                }
            }
        }

        private void updateMatrixButton()
        {
            for (int y = 0; y < m_BoardSize; y++)
            {
                for (int x = 0; x < m_BoardSize; x++)
                {
                    m_buttonMatrix[y, x].BackColor = Color.WhiteSmoke;
                    m_buttonMatrix[y, x].Image = null;
                    updateStateButton(y, x);
                    m_buttonMatrix[y, x].Enabled = false;
                }
            }
        }

        private void initialzeGame()
        {
            m_CurrentPlayerShape = 'O';
            playTurn();
        }

        private void setPlayers(bool i_isTheSecondPlayerIsComputer)
        {
            m_Controller.SetPlayerOnGameManager("Player 1", true, 'X', 1);
            m_Controller.SetPlayerOnGameManager("Player 2", !i_isTheSecondPlayerIsComputer, 'O', 2);
        }

        private void playTurn()
        {
            List<OthelloLogic.Utilities.ValidStep> validSteps;

            changeShape();
            if (!m_Controller.isTurnPossible(out validSteps, m_CurrentPlayerShape))
            {
                changeShape();
                if (!m_Controller.isTurnPossible(out validSteps, m_CurrentPlayerShape))
                {
                    endGame();
                }
            }

            markAllValidStepsInGreen(validSteps);
            bool check = m_Controller.IsItComputerTurn(m_CurrentPlayerShape);
            if (m_Controller.IsItComputerTurn(m_CurrentPlayerShape) && validSteps.Count != 0)
            {
                ChoosePlayerStep(validSteps);
            }
        }

        private void buttonMatrix_Click(object sender, EventArgs e)
        {
            doActionOfPlayer(((PictureBox)sender).Tag.ToString());
        }

        private void parseStringToNumber(string i_PlayerStep, out OthelloLogic.Utilities.Point o_ParseStep)
        {
            int x, y;
            y = (int)(char.Parse(i_PlayerStep.Remove(1, 1)) - '0');
            x = (int)(char.Parse(i_PlayerStep.Remove(0, 1)) - '0');

            o_ParseStep = new OthelloLogic.Utilities.Point(x, y);
        }

        private void ChoosePlayerStep(List<OthelloLogic.Utilities.ValidStep> i_validSteps)
        {
            int amountOfValidSteps = i_validSteps.Count;
            int x, y;
            Random randomPlay = new Random();
            int randomStepIndex = randomPlay.Next(0, amountOfValidSteps);
            OthelloLogic.Utilities.ValidStep currentStep = i_validSteps[randomStepIndex];
            x = currentStep.GetPoint().X();
            y = currentStep.GetPoint().Y();

            doActionOfPlayer(m_buttonMatrix[x, y].Tag.ToString());
        }

        private void doActionOfPlayer(string i_stepToparse)
        {
            OthelloLogic.Utilities.Point parseStep;
            parseStringToNumber(i_stepToparse, out parseStep);

            m_Controller.UpdateBoardInController(parseStep, m_CurrentPlayerShape.ToString());
            updateMatrixButton();
            playTurn();
        }

        private void changeShape()
        {
            if (m_CurrentPlayerShape == 'X')
            {
                m_CurrentPlayerShape = 'O';
                this.Text = "Othello-  Black's Turn";
            }
            else
            {
                m_CurrentPlayerShape = 'X';
                this.Text = "Othello-  White's Turn";
            }
        }

        private void markAllValidStepsInGreen(List<OthelloLogic.Utilities.ValidStep> i_ValidSteps)
        {
            int x, y;
            foreach (OthelloLogic.Utilities.ValidStep currentStep in i_ValidSteps)
            {
                x = currentStep.GetPoint().X();
                y = currentStep.GetPoint().Y();

                m_buttonMatrix[x, y].BackColor = Color.LimeGreen;
                m_buttonMatrix[x, y].Enabled = true;
            }
        }

        public void GetScore(out int o_PlayerOneScore, out int o_PlayerTwoScore)
        {
            m_Controller.GetScore(out o_PlayerOneScore, out o_PlayerTwoScore);
        }

        private void endGame()
        {
            int i_PlayerOneScore, i_PlayerTwoScore;
            GetScore(out i_PlayerOneScore, out i_PlayerTwoScore);
            StringBuilder msg = setTheScoreText(i_PlayerOneScore, i_PlayerTwoScore);
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Question;
            DialogResult result = MessageBox.Show(msg.ToString(), "Othello", buttons, icon);
            if (result == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                Yes_Button();
            }
        }

        private StringBuilder setTheScoreText(int i_PlayerOneScore, int i_PlayerTwoScore)
        {
            StringBuilder msg = new StringBuilder();

            if (i_PlayerOneScore > i_PlayerTwoScore)
            {
                m_scorePlayerOne++;
                msg.Append(string.Format(@"White Won!! ({1}\{2}) ({3},{4}){0}",
                            Environment.NewLine, i_PlayerOneScore, i_PlayerTwoScore, m_scorePlayerOne, m_ScorePlayerTwo));
            }
            else if (i_PlayerOneScore < i_PlayerTwoScore)
            {
                m_ScorePlayerTwo++;
                msg.Append(string.Format(@"Black Won!! ({1}\{2}) ({3},{4}){0}",
                            Environment.NewLine, i_PlayerTwoScore, i_PlayerOneScore, m_scorePlayerOne, m_ScorePlayerTwo));
            }
            else
            {
                msg.Append(string.Format(@"It's A tie!! ({1}\{2}) ({3},{4}){0}",
                            Environment.NewLine, i_PlayerTwoScore, i_PlayerOneScore, m_scorePlayerOne, m_ScorePlayerTwo));
            }

            msg.Append("Would you like another round?");

            return msg;
        }

        private void updateStateButton(int i_Y, int i_X)
        {
            if (m_PaintGameBoard[i_Y, i_X] == "O")
            {
                m_buttonMatrix[i_Y, i_X].Image =
                    global::B19Ex01_LidorTevet_312465040_SaharHagbi_308145853.Properties.Resources.CoinRed;
                m_buttonMatrix[i_Y, i_X].BackColor = Color.Black;
            }

            if (m_PaintGameBoard[i_Y, i_X] == "X")
            {
                m_buttonMatrix[i_Y, i_X].Image =
                     global::B19Ex01_LidorTevet_312465040_SaharHagbi_308145853.Properties.Resources.CoinYellow;
                m_buttonMatrix[i_Y, i_X].BackColor = Color.NavajoWhite;
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        private void Yes_Button()
        {
            m_Controller = new Controller();
            m_PaintGameBoard = m_Controller.m_StartGame(m_BoardSize);
            updateMatrixButton();
            setPlayers(m_isTheSecondPlayerIsComputer);
            initialzeGame();
        }
    }
}