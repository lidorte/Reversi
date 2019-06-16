using System;
using System.Windows.Forms;
using B19Ex05_LidorTevet_312465040_SaharHagbi_308145853;

namespace B19Ex05_LidorTevet_312465040_SaharHagbi_308145853
{
    public partial class OpenForm : Form
    {
        private int m_BorderSize = (int)Utilities.eBoardSize.SIZE_6X6;
        private GameForm m_GameForm;

        public OpenForm()
        {
            InitializeComponent();
        }

        private void againstFriend_Click(object sender, EventArgs e)
        {
            openGameForm(Utilities.Human);
        }

        private void againstComputer_Click(object sender, EventArgs e)
        {
            openGameForm(Utilities.Computer);
        }

        private void size_Click(object sender, EventArgs e)
        {
            handleTextOnClick(this.b_Size);
        }

        private void openGameForm(bool i_IsTheSecondPlayerIsComputer)
        {
            this.Hide();
            m_GameForm = new GameForm(m_BorderSize);
            m_GameForm.StartPlay(i_IsTheSecondPlayerIsComputer);
            this.Close();
        }

        private void handleTextOnClick(Button i_Button)
        {
            string currentButtonString = i_Button.Text;

            if (currentButtonString.Contains("6"))
            {
                i_Button.Text = "Board Size: 8x8 (click to increase)";
                m_BorderSize = (int)Utilities.eBoardSize.SIZE_8X8;
            }
            else if (currentButtonString.Contains("8"))
            {
                i_Button.Text = "Board Size: 10x10 (click to increase)";
                m_BorderSize = (int)Utilities.eBoardSize.SIZE_10X10;
            }
            else if (currentButtonString.Contains("10"))
            {
                i_Button.Text = "Board Size: 12x12 (click to increase)";
                m_BorderSize = (int)Utilities.eBoardSize.SIZE_12X12;
            }
            else
            {
                i_Button.Text = "Board Size: 6x6 (click to increase)";
                m_BorderSize = (int)Utilities.eBoardSize.SIZE_6X6;
            }
        }

        private void OpenForm_Load(object sender, EventArgs e)
        {
        }
    }
}
