using System.Collections.Generic;
using OthelloLogic;

namespace B19Ex05_LidorTevet_312465040_SaharHagbi_308145853
{
    public class Controller
    {
        private gameManager m_GameLogic = new gameManager();
        private List<OthelloLogic.Utilities.ValidStep> m_ValidSteps;

        public string[,] m_StartGame(int i_BoardSize)
        {
            return m_GameLogic.StartGame(i_BoardSize);
        }

        public void SetPlayerOnGameManager(string i_PlayerName, bool i_IsHuman, char i_Shape, int i_PlayerNumber)
        {
            m_GameLogic.SetPlayerOnGameManager(i_PlayerName, i_IsHuman, i_Shape, i_PlayerNumber);
        }

        public void UpdateBoardInController(OthelloLogic.Utilities.Point i_Step, string i_PlayerShape)
        {
            OthelloLogic.Utilities.ValidStep requestedValidStep = searchStepInValidSteps(i_Step);

            m_GameLogic.UpdateBoard(requestedValidStep, i_PlayerShape);
        }

        private OthelloLogic.Utilities.ValidStep searchStepInValidSteps(OthelloLogic.Utilities.Point i_Step)
        {
            OthelloLogic.Utilities.Point currentPoint;
            OthelloLogic.Utilities.ValidStep requestedValidStep = null;

            foreach (OthelloLogic.Utilities.ValidStep validStep in m_ValidSteps)
            {
                currentPoint = validStep.GetPoint();

                if ((currentPoint.X() == i_Step.X()) && (currentPoint.Y() == i_Step.Y()))
                {
                    requestedValidStep = validStep;
                }
            }

            return requestedValidStep;
        }

        public bool isTurnPossible(out List<OthelloLogic.Utilities.ValidStep> io_ValidSteps, char i_PlayerShape)
        {
            bool result = m_GameLogic.isTurnPossible(out io_ValidSteps, i_PlayerShape);
            m_ValidSteps = io_ValidSteps;
            return result;
        }

        public bool IsItComputerTurn(char i_PlayerShape)
        {
            return m_GameLogic.IsThePlayerComputer(i_PlayerShape);
        }

        public void GetScore(out int i_PlayerOneScore, out int i_PlayerTwoScore)
        {
            m_GameLogic.GetScore(out i_PlayerOneScore, out i_PlayerTwoScore);
        }
    }
}
